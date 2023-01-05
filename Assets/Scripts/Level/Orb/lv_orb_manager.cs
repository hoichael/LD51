using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_orb_manager : MonoBehaviour
{
    [SerializeField] lv_goal goal;
    [SerializeField] int totalCount; // hardcoding this is kinda ugly. the final implementation will depend on architectural questions yet unanswered

    List<lv_orb> orbList = new List<lv_orb>();

    int currentCount;

    public void Reset()
    {
        currentCount = 0;

        foreach(lv_orb orb in orbList)
        {
            orb.Reset();
        }
    }

    public void AddToOrbList(lv_orb orbSystem)
    {
        orbList.Add(orbSystem);
    }

    public void HandleCollect()
    {
        currentCount++;

        if(currentCount >= totalCount)
        {
            goal.Unlock();
        }
    }
}
