using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_button_config_volume : menu_selectable
{
    [SerializeField] int value;
    [SerializeField] menu_config_audio audioSubscreenManager;
    [SerializeField] menu_config_audio_volume_button_type type;
    [SerializeField] SpriteRenderer sprRenderer;
    [SerializeField] Sprite sprDefault, sprActive;
    [SerializeField] string descText;

    protected override void Enter()
    {
        base.Enter();
        sprRenderer.sprite = sprActive;

        if (type == menu_config_audio_volume_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in CONFIG AUDIO MENU");
            return;
        }

        audioSubscreenManager.HandleButtonSelect(descText);
    }

    public override void Activate()
    {
        base.Activate();

        if (type == menu_config_audio_volume_button_type.UNDEFINED)
        {
            print("UNDEFINED BUTTON TYPE in CONFIG AUDIO MENU");
            return;
        }
        audioSubscreenManager.HandleButtonActivate(type, value);
    }

    protected override void Exit()
    {
        base.Exit();
        sprRenderer.sprite = sprDefault;
    }
}
