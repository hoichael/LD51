using UnityEngine;

public class ball_pickup : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        refs.manager.HandlePickup();
    }
}
