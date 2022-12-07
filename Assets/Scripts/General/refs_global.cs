using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refs_global : ut_singleton<refs_global>
{
    public Camera cam;

    public Transform playerTrans;
    public Rigidbody2D playerRB;
    public Transform ballHolderTrans;
    public Transform plFlipContainerTrans;

    public int playerDir = 1; // kinda ugly but whtv

    public ball_refs currentBallRefs;
    public bool ballInHand;

    public Transform xHairTrans;

    public InputServer ip;

    public pl_events playerEvents;

    public lv_manager levelManager;

    public lv_pool pool;
}
