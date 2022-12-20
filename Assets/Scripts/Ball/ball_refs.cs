using UnityEngine;

public class ball_refs : MonoBehaviour
{
    public SO_ball_settings settings;
    public ball_manager manager;
    public ball_visual visual;
    public Transform trans;
    public Rigidbody2D rb;
    public Collider2D colSolid, colTrigger;
    public TrailRenderer trail;
}
