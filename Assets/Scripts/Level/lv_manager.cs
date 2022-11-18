using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lv_manager : MonoBehaviour
{
    [SerializeField] lv_controller levelController;
    [SerializeField] lv_completed completionController;
    [SerializeField] menu_navigate uiNavigator;
    [SerializeField] List<lv_info> levelInfoList;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] SO_pd_session sessionData;
    [SerializeField] lv_pool pool;
    public TextMeshPro timerText;

    public int currentLevelIDX;

    private void Start()
    {
        //InitLevel(0);
        InitLevel(sessionData.selectedLevelIDX);
    }

    private void Update()
    {
        //ListenForNumInput();
        HandleDevLevelSwitch();
        CheckForRelaod();
    }

    private void CheckForRelaod()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        if (refs_global.Instance.ip.I.Play.Restart.WasPressedThisFrame())
        {
            InitLevel(currentLevelIDX);
        }
    }

    private void HandleDevLevelSwitch()
    {
        if (refs_global.Instance.ip.I.DEV.One.WasPressedThisFrame())
        {
            InitLevel(currentLevelIDX - 1);
        }
        else if(refs_global.Instance.ip.I.DEV.Two.WasPressedThisFrame())
        {
            InitLevel(currentLevelIDX + 1);
        }
    }

    //private void ListenForNumInput()
    //{
    //    for (int i = 0; i < levelInfoList.Count; i++)
    //    {
    //        if (Input.GetKeyDown(i.ToString()))
    //        {
    //            InitLevel(i);
    //        }
    //    }
    //}


    public void InitLevel(int idx)
    {
        if (idx >= levelInfoList.Count) idx = 0;
        if (idx < 0) idx = levelInfoList.Count - 1;


        // dispose of currently active level

        //Destroy(refs_global.Instance.currentBallRefs?.trans.gameObject);
        pool.DisableAll(lv_pool.PoolType.Ball);

        if (refs_global.Instance.currentBallRefs != null)
        {
            refs_global.Instance.currentBallRefs.rb.gravityScale = 0;
            pool.Return(lv_pool.PoolType.Ball, refs_global.Instance.currentBallRefs.trans, false);
        }
        refs_global.Instance.currentBallRefs = null;
        refs_global.Instance.ballInHand = false;
        levelInfoList[currentLevelIDX].levelContainer.SetActive(false);

        // init new level
        levelInfoList[idx].levelContainer.SetActive(true);
        currentLevelIDX = idx;

        // handle player
        refs_global.Instance.playerTrans.localPosition = levelInfoList[idx].plSpawnPos;
        refs_global.Instance.playerRB.velocity = Vector2.zero;

        //// handle balls container
        //Destroy(levelInfoList[idx].ballsContainer.gameObject);
        //GameObject newBallsContainer = new GameObject();
        //newBallsContainer.name = "BallsContainer";
        //newBallsContainer.transform.SetParent(levelInfoList[idx].levelContainer.transform);
        //levelInfoList[idx].ballsContainer = newBallsContainer.transform;

        //// instantiate and position balls
        //for (int i = 0; i < levelInfoList[idx].ballSpawnPosArr.Length; i++)
        //{
        //    GameObject newBall = Instantiate(ballPrefab, levelInfoList[idx].ballSpawnPosArr[i], Quaternion.identity);
        //    newBall.transform.SetParent(newBallsContainer.transform);
        //}

        for (int i = 0; i < levelInfoList[idx].ballSpawnPosArr.Length; i++)
        {
            pool.Dispatch(lv_pool.PoolType.Ball, levelInfoList[idx].ballSpawnPosArr[i]);
        }


        levelController.Reset();
        completionController.Reset();

        // handle menu navigator (currently only used for level completion screen) ugly af but whtv
        if(uiNavigator.currentSelection != null)
        {
            uiNavigator.currentSelection.enabled = false;
            uiNavigator.currentSelection = null;
        }
    }
}
