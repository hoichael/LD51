using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_lvselect_manager : MonoBehaviour
{
    [SerializeField] List<SO_menu_worldinfo> worldInfoList;
    [SerializeField] menu_lvselect_bootstrap bootstrapper;

    SO_menu_worldinfo currentWorld;
    int currentSelectedLevelIDX;

    public void Init(int worldIDX)
    {
        currentSelectedLevelIDX = 0;
        currentWorld = worldInfoList[worldIDX];
        bootstrapper.Init(currentWorld);
    }
}