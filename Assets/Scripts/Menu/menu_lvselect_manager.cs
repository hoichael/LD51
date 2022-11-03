using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_lvselect_manager : MonoBehaviour
{
    [SerializeField] List<SO_menu_worldinfo> worldInfoList;
    [SerializeField] menu_lvselect_bootstrap bootstrapper;
    [SerializeField] menu_lvselect_nav navigator;

    [SerializeField] SpriteRenderer thumbnailRenderer;
    [SerializeField] TextMeshPro titleRenderer;

    SO_menu_worldinfo currentWorld;

    [SerializeField] InputServer input;
    [SerializeField] menu_worldselect worldSelectManager;
    [SerializeField] menu_lvselect_data data;

    private void Start()
    {
        navigator.enabled = false;
    }

    private void Update()
    {
        if(input.I.Menu.Exit.WasPressedThisFrame())
        {
            Exit();
        }
    }

    public void Init(int worldIDX)
    {
        currentWorld = worldInfoList[worldIDX];
        navigator.sprArr = bootstrapper.Init(currentWorld); // the whole sprite arr thing is rather... f u n c t i o n a l (read: scuffed as fuck) but whtv. its fine ***for now***
        navigator.enabled = true;
    }

    private void Exit()
    {
        navigator.enabled = false;
        worldSelectManager.enabled = true;
        worldSelectManager.LeaveWorld();
    }

    public void HandleSelectionSwitch(int idx)
    {
        // replace currently shown thumbnail image
        thumbnailRenderer.sprite = currentWorld.levelInfoList[idx].thumbnail;

        // replace currently shown name text
        titleRenderer.text = currentWorld.levelInfoList[idx].name;
    }

    public void EnterLevel(int idx)
    {

    }
}