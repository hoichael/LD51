using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_orb : lv_colhandler_base
{
    [SerializeField] lv_orb_manager manager;
    [SerializeField] Transform activeContainer;
    [SerializeField] lv_orb_anim anim;
    [SerializeField] Collider2D col;

    // OLD: OnEnable is triggered on level init because orb container is set active - orb objects themselves ALWAYS stay enabled
    // -> NOW: Reset() is called from lv_orb_manager to allow for reset without disabling level container/orb container
    //private void OnEnable()
    //{
    //    Reset();
    //}

    private void Start()
    {
        manager.AddToOrbList(this);
    }

    public void Reset()
    {
        activeContainer.gameObject.SetActive(true);
        col.enabled = true;
        anim.enabled = false;
        transform.localScale = Vector3.one;
    }

    public override void HandleCol()
    {
        print("asdfsdf");
        //activeContainer.gameObject.SetActive(false);
        manager.HandleCollect();
        col.enabled = false;
        print(anim.enabled);
        anim.enabled = true;
        print(anim.enabled);
    }

    // called from ed_lv_gen
    public void SetManager(lv_orb_manager mng)
    {
        manager = mng;
    }
}
