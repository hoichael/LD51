using UnityEngine;

public class menu_selectable : MonoBehaviour
{
    public menu_selectable connectorLeft, connectorRight, connectorTop, connectorBottom;
    protected bool active;

    private void Start()
    {
        //Exit(); // quick n dirty. maybe doesnt work for all button types
        this.enabled = false;
    }

    private void OnEnable()
    {
        Enter();
    }

    private void OnDisable()
    {
        Exit();
    }

    public virtual void Activate()
    {

    }

    protected virtual void Enter()
    {

    }

    protected virtual void Exit()
    {

    }
}
