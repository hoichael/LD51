using UnityEngine;

public class lv_goal : MonoBehaviour
{
    [SerializeField] lv_controller controller;
    [SerializeField] Sprite sprLocked, sprUnlocked; // having a ref to these on every goal instance is needlessly inefficient
    [SerializeField] SpriteRenderer sprRenderer;

    [SerializeField] bool locked;


    private void Start()
    {
        sprRenderer.sprite = locked ? sprLocked : sprUnlocked;
    }

    // temp solution. should be checking for general col, not just for enter (in case goal is unlocked while player is already in collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (locked) return;

        controller.HandleCompletion();
    }

    public void Unlock()
    {
        locked = false;
        sprRenderer.sprite = sprUnlocked;
    }
}
