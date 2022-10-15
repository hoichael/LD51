using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refs_global : ut_singleton<refs_global>
{
    public Transform playerTrans;
    public Rigidbody2D playerRB;
    public Transform ballHolderTrans;

    public int playerDir = 1; // kinda ugly but whtv

    public Transform currentBallTrans;
    public GameObject currentHeldBallObj;
}
