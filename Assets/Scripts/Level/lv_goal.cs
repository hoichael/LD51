using UnityEngine;

public class lv_goal : MonoBehaviour
{
    [SerializeField] lv_controller controller;

    bool locked;

    // temp solution. should be checking for general col, not just for enter (in case goal is unlocked while player is already in collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (locked) return;

        controller.HandleCompletion();
    }
}
