using UnityEngine;

public class menu_button_TEST : menu_selectable
{
    [SerializeField] Color colorDefault, colorActive;
    [SerializeField] SpriteRenderer sprRenderer;

    protected override void Enter()
    {
        base.Enter();
        sprRenderer.color = colorActive;
    }

    protected override void Exit()
    {
        base.Exit();
        sprRenderer.color = colorDefault;
    }
}
