using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menu_lvselect_manager : menu_screen_base
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

    [SerializeField] SO_pd_session sessionData;

    private void Start()
    {
        navigator.enabled = false;
        this.enabled = false;
    }

    private void Update()
    {
        if(input.I.Menu.Exit.WasPressedThisFrame())
        {
            menuManager.SwitchScreen(worldSelectManager);
            return;
        }
    }

    public override void OnSwitchToInit()
    {
        base.OnSwitchToInit();
        //Init(sessionData.selectedWorldIDX);
        currentWorld = worldInfoList[sessionData.selectedWorldIDX];
        navigator.sprArr = bootstrapper.Init(currentWorld); // the whole sprite arr thing is rather... f u n c t i o n a l (read: scuffed as fuck) but whtv. its fine ***for now***
        navigator.enabled = true;
    }

    public override void OnSwitchToComplete()
    {
        base.OnSwitchToComplete();
        this.enabled = true;
    }

    public override void OnSwitchFromInit()
    {
        base.OnSwitchFromInit();
        navigator.enabled = false;
        //worldSelectManager.enabled = true;
        this.enabled = false;
    }

    public void HandleSelectionSwitch(int idx)
    {
        // replace currently shown thumbnail image
        thumbnailRenderer.sprite = currentWorld.levelInfoList[idx].thumbnail;

        // replace currently shown name text
        titleRenderer.text = currentWorld.levelInfoList[idx].name;
    }

    public void EnterLevel(int idx) // called from update of menu_lvselect_nav
    {
        // save level index to session data - will be read be level manager upon scene load
        sessionData.selectedLevelIDX = idx;

        // switch scenes
        SceneManager.LoadScene(currentWorld.worldSceneName);
    }
}