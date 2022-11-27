using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] Texture2D texCursor;

    private void Start()
    {
        // probably shouldnt happen here
        //Cursor.SetCursor(texCursor, Vector2.zero, CursorMode.Auto);
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }

    void Update()
    {
        // reloading currently handled in lv_manager
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}


        // temp dev. currently only entry point to menu scene from gameplay scene
        if(refs_global.Instance.ip.I.Play.Pause.WasPressedThisFrame())
        {
            SceneManager.LoadScene("sc_levelselect_V2");
        }
    }
}
