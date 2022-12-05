using UnityEngine;
using TMPro;

public class menu_button_main : menu_selectable
{
    [SerializeField] TextMeshPro textElement;
    [SerializeField] menu_main mainMenuManager;
    [SerializeField] menu_main_button_type buttonType;
    [SerializeField] Color colorDefault, colorActive;

    private void Awake()
    {
        textElement.color = colorDefault;    
    }

    protected override void Enter()
    {
        base.Enter();
        textElement.color = colorActive;
    }

    protected override void Exit()
    {
        base.Exit();
        textElement.color = colorDefault;
    }

    public override void Activate()
    {
        base.Activate();
        if (buttonType == menu_main_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in MAIN MENU");
            return;
        }
        mainMenuManager.HandleButton(buttonType);
    }
}
