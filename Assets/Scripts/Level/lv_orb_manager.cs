using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lv_orb_manager : MonoBehaviour
{
    [SerializeField] lv_goal goal;
    [SerializeField] int totalCount; // hardcoding this is kinda ugly. the final implementation will depend on architectural questions yet unanswered

    int currentCount;

    public void HandleCollect()
    {
        currentCount++;
        print(currentCount);
        print(totalCount);

        if(currentCount >= totalCount)
        {
            goal.Unlock();
        }
    }
}
