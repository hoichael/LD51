using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_orb : lv_colhandler_base
{
    [SerializeField] lv_orb_manager manager;
    [SerializeField] Transform activeContainer;

    // OnEnable is triggered on level init because orb container is set active - orb objects themselves ALWAYS stay enabled
    private void OnEnable()
    {
        Reset();
    }

    private void Reset()
    {
        activeContainer.gameObject.SetActive(true);
    }

    public override void HandleCol()
    {
        activeContainer.gameObject.SetActive(false);
        manager.HandleCollect();
    }

    // called from ed_lv_gen
    public void SetManager(lv_orb_manager mng)
    {
        manager = mng;
    }
}
