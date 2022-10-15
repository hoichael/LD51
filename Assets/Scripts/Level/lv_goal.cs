using UnityEngine;

public class lv_goal : MonoBehaviour
{
    [SerializeField] lv_controller controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.HandleCompletion();
    }
}
