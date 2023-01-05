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

    [SerializeField] pl_refs plRefs;
    [SerializeField] pl_events plEvents;

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

    public lv_info GetCurrentLevelInfo()
    {
        return levelInfoList[currentLevelIDX];
    }

    private void CheckForRelaod()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        if (refs_global.Instance.ip.I.Play.Restart.WasPressedThisFrame())
        {
            refs_global.Instance.playerEvents.OnDeath();
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

        if(idx != currentLevelIDX) // currently this check only impacts spike col anim shenanigans - a lil fucky but doing this instead of disabling and re-enabling the same level container is more proper anyway
        {
            levelInfoList[currentLevelIDX].levelContainer.SetActive(false);
        }

        // init new level
        levelInfoList[idx].levelContainer.SetActive(true);
        currentLevelIDX = idx;

        // handle player
        refs_global.Instance.playerTrans.localPosition = levelInfoList[idx].plSpawnPos;
        refs_global.Instance.playerRB.velocity = Vector2.zero;
        //refs_global.Instance.plFlipContainerTrans.localScale = new Vector3(levelInfoList[idx].initPlayerFacingLeft ? -1 : 1, 1, 1);
        plRefs.info.dir = levelInfoList[idx].initPlayerFacingLeft ? -1 : 1; // this is annoying. only use of pl_refs in entire script. leaving it like this for now but future me, REFACTOR THIS ! ! ! pls. . .
        plEvents.OnLevelLoad();

        // temp solution to provide legacy level support by also handling ballSpawnPosArr
        if (levelInfoList[idx].ballSpawnPosArr.Length != 0) // if-check so that accidental mix of legacy/new data format doesnt manifest in level
        {
            for (int i = 0; i < levelInfoList[idx].ballSpawnPosArr.Length; i++)
            {
                pool.Dispatch(lv_pool.PoolType.Ball, levelInfoList[idx].ballSpawnPosArr[i]);
            }
        }
        else
        {
            foreach (Transform sub in levelInfoList[idx].ballPosObjContainer)
            {
                pool.Dispatch(lv_pool.PoolType.Ball, sub.transform.position);
            }
        }

        // reset goal lock/orb system of current level idx
        levelInfoList[idx].goalManager.Reset();
        levelInfoList[idx].orbManager.Reset();

        // reset global level systems
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
