using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lv_manager : MonoBehaviour
{
    [SerializeField] List<lv_info> levelInfoList;
    [SerializeField] GameObject ballPrefab;
    public TextMeshProUGUI timerText;

    int currentLevelIDX;

    private void Start()
    {
        InitLevel(0);
    }

    private void Update()
    {
        //ListenForNumInput();
        HandleDevLevelSwitch();
        HandleReload();
    }

    private void HandleReload()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        if (refs_global.Instance.ip.I.Play.Restart.WasPressedThisFrame())
        {
            InitLevel(currentLevelIDX);
        }
    }

    private void HandleDevLevelSwitch()
    {
        int incr = 0;
        if (refs_global.Instance.ip.I.DEV.One.WasPressedThisFrame())
        {
            incr = -1;
        }
        else if(refs_global.Instance.ip.I.DEV.Two.WasPressedThisFrame())
        {
            incr = 1;
        }

        if(incr != 0)
        {
            int newLevelIDX = currentLevelIDX + incr;

            if(newLevelIDX == levelInfoList.Count)
            {
                newLevelIDX = 0;
            }
            else if (newLevelIDX == -1)
            {
                newLevelIDX = levelInfoList.Count - 1;
            }

            InitLevel(newLevelIDX);
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


    private void InitLevel(int idx)
    {
        // dispose of currently active level
        //refs_global.Instance.currentBallTrans = null;
        Destroy(refs_global.Instance.currentBallRefs?.trans.gameObject);
        refs_global.Instance.currentBallRefs = null;
        refs_global.Instance.ballInHand = false;
        levelInfoList[currentLevelIDX].levelContainer.SetActive(false);

        // init new level
        levelInfoList[idx].levelContainer.SetActive(true);
        currentLevelIDX = idx;

        // handle player
        refs_global.Instance.playerTrans.localPosition = levelInfoList[idx].plSpawnPos;
        refs_global.Instance.playerRB.velocity = Vector2.zero;

        // handle balls container
        Destroy(levelInfoList[idx].ballsContainer.gameObject);
        GameObject newBallsContainer = new GameObject();
        newBallsContainer.name = "BallsContainer";
        newBallsContainer.transform.SetParent(levelInfoList[idx].levelContainer.transform);
        levelInfoList[idx].ballsContainer = newBallsContainer.transform;

        // instantiate and position balls
        for(int i = 0; i < levelInfoList[idx].ballSpawnPosArr.Length; i++)
        {
            GameObject newBall = Instantiate(ballPrefab, levelInfoList[idx].ballSpawnPosArr[i], Quaternion.identity);
            newBall.transform.SetParent(newBallsContainer.transform);
        }
    }
}
