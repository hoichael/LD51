using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_lvselect_manager : MonoBehaviour
{
    [SerializeField] List<SO_menu_worldinfo> worldInfoList;
    [SerializeField] menu_lvselect_bootstrap bootstrapper;
    [SerializeField] menu_lvselect_nav navigator;

    SO_menu_worldinfo currentWorld;

    private void Start()
    {
        navigator.enabled = false;
    }

    public void Init(int worldIDX)
    {
        currentWorld = worldInfoList[worldIDX];
        navigator.sprArr = bootstrapper.Init(currentWorld); // the whole sprite arr thing is rather... f u n c t i o n a l (read: scuffed as fuck) but whtv. its fine ***for now***
        navigator.enabled = true;
    }
}