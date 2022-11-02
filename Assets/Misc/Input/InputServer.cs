using UnityEngine;

public class InputServer : MonoBehaviour
{
    public InputActions I;

    private void Awake()
    {
        I = new InputActions();
    }

    private void OnEnable()
    {
        I.Enable();
    }

    private void OnDisable()
    {
        I.Disable();
    }
}
