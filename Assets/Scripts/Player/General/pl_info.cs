using UnityEngine;

public class pl_info : MonoBehaviour
{
    public bool grounded;

    public float moveForceCurrent;

    public int dir = 1; // 1 == right, -1 == left

    public int slope; // 1 == slope facing right, -1 == slope facing left, 0 == flat ground

    public bool recentWalljump;
}
