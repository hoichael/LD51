using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_config_audio : menu_config_subscreen
{    
    public void HandleButtonSelect(string descToSet)
    {
        descTextEl.text = descToSet;
    }

    public void HandleButtonActivate(menu_config_audio_volume_button_type type)
    {

    }
}

public enum menu_config_audio_volume_button_type
{
    UNDEFINED,

    MasterUp,
    MasterDown,

    MusicUp,
    MusicDown,

    sfxUp,
    sfxDown
}
