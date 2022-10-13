using UnityEngine;

public class ball_manager : MonoBehaviour
{
    [SerializeField] ball_throw thrower;
    bool currentlyHeld;

    private void Update()
    {
        if (currentlyHeld && (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0) )) HandleThrow(); 
    }

    public void HandlePickup()
    {
        currentlyHeld = true;
    }

    private void HandleThrow()
    {
        currentlyHeld = false;

        Vector2 throwDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            ).normalized;

        if(throwDir == Vector2.zero)
        {
            throwDir = new Vector2(refs_global.Instance.playerDir, 0);
        }

        thrower.Init(throwDir);
    }
}
