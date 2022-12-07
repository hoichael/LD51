using UnityEngine;

[System.Serializable]
public class lv_info : MonoBehaviour
{
    public GameObject levelContainer;
    public Transform ballPosObjContainer;
    public Vector3 plSpawnPos;
    public Vector3[] ballSpawnPosArr;
    public Transform ballsContainer;
    public lv_goal goalManager;
    public lv_orb_manager orbManager;
    public bool initPlayerFacingLeft;
}
