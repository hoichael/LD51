using UnityEngine;

public class lv_forcepad_col : MonoBehaviour
{
    [SerializeField] lv_forcepad manager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            manager.HandlePlayerContact();
        }
        else
        {
            Rigidbody2D rb = col.GetComponentInParent<Rigidbody2D>();
            manager.HandleBallContact(rb);
        }
    }
}
