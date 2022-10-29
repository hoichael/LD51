using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_selectable : MonoBehaviour
{
    public menu_selectable connectorLeft, connectorRight, connectorTop, connectorBottom;
    protected bool active;

    private void OnEnable()
    {
        Enter();
    }

    private void OnDisable()
    {
        Exit();
    }

    protected virtual void Update()
    {

    }

    protected virtual void Enter()
    {

    }

    protected virtual void Exit()
    {

    }
}
