using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_button_lvcompletion : menu_selectable
{
    //[SerializeField] Color colorDefault, colorActive;
    [SerializeField] Sprite sprDefault, sprActive;
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Type buttonType;
    [SerializeField] lv_manager levelManager;

    protected override void Enter()
    {
        base.Enter();
        //sprRenderer.color = colorActive;
        sprRenderer.sprite = sprActive;
    }

    protected override void Exit()
    {
        base.Exit();
        //sprRenderer.color = colorDefault;
        sprRenderer.sprite = sprDefault;
    }

    public override void Activate()
    {
        base.Activate();
        if(buttonType == Type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE");
            return;
        }
        HandleActivation();
    }

    private void HandleActivation()
    {
        switch(buttonType)
        {
            case Type.Menu:
                SceneManager.LoadScene("sc_menu_1");
                break;
            case Type.Restart:
                levelManager.InitLevel(levelManager.currentLevelIDX);
                break;
            case Type.Next:
                levelManager.InitLevel(levelManager.currentLevelIDX + 1);
                break;
        }
    }

    enum Type
    {
        UNDEFINED,
        Menu,
        Restart,
        Next
    }
}
