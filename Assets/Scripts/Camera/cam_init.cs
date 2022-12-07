using UnityEngine;

public class cam_init : MonoBehaviour
{
    [SerializeField] Camera cam;
    const float aspectRatio = 1.777777777777778f;

    private void Awake()
    {
        cam.aspect = aspectRatio;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
}
