using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_manager : MonoBehaviour
{
    [SerializeField] List<lv_info> levelInfoList;
    [SerializeField] GameObject ballPrefab;

    int currentActiveLV;

    private void Start()
    {
        InitLevel(0);
    }

    private void Update()
    {
        ListenForNumInput();
    }

    private void ListenForNumInput()
    {
        for (int i = 0; i < levelInfoList.Count; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                InitLevel(i);
            }
        }
    }


    private void InitLevel(int idx)
    {
        // dispose of currently active level
        refs_global.Instance.currentBallTrans = null;
        Destroy(refs_global.Instance.currentHeldBallObj);
        levelInfoList[currentActiveLV].levelContainer.SetActive(false);

        // init new level
        levelInfoList[idx].levelContainer.SetActive(true);
        currentActiveLV = idx;

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

[System.Serializable]
public class lv_info
{
    public GameObject levelContainer;
    public Vector3 plSpawnPos;
    public Vector3[] ballSpawnPosArr;
    public Transform ballsContainer;
}