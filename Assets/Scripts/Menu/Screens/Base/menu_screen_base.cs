using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class menu_screen_base : MonoBehaviour
{
    public Transform uiContainer;

    public virtual void OnSwitchToInit()
    {

    }

    public virtual void OnSwitchToComplete()
    {

    }

    public virtual void OnSwitchFromInit()
    {

    }

    public virtual void OnSwitchFromComplete()
    {

    }
}
