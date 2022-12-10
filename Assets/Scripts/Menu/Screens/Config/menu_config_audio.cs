using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu_config_audio : menu_config_subscreen
{
    [SerializeField] SO_config configData;

    // todo: generate these procedurally and get refs in the process
    [SerializeField] List<GameObject> visualTogglesMaster, visualTogglesMusic, visualTogglesSFX;

    int volumeMax = 10;

    private void Awake()
    {
        SetVisualToggles(visualTogglesMaster, configData.audioVolumeMaster);
        SetVisualToggles(visualTogglesMusic, configData.audioVolumeMusic);
        SetVisualToggles(visualTogglesSFX, configData.audioVolumeSFX);
    }

    public void HandleButtonSelect(string descToSet)
    {
        descTextEl.text = descToSet;
    }

    // would obv prefer to handle this non conditionally but that would get fucky regarding the structure of the config data class as the json serialization module doesnt like dicts (kind of, maybe, i think?)
    // will look into this. for now: quick n dirty.
    public void HandleButtonActivate(menu_config_audio_volume_button_type type, int value) // value is either 1 or -1
    {
        switch (type)
        {
            case menu_config_audio_volume_button_type.Master:
                configData.audioVolumeMaster = Mathf.Clamp(configData.audioVolumeMaster + value, 0, volumeMax);
                SetVisualToggles(visualTogglesMaster, configData.audioVolumeMaster);
                break;
            case menu_config_audio_volume_button_type.Music:
                configData.audioVolumeMusic = Mathf.Clamp(configData.audioVolumeMusic + value, 0, volumeMax);
                SetVisualToggles(visualTogglesMusic, configData.audioVolumeMusic);
                break;
            case menu_config_audio_volume_button_type.SFX:
                configData.audioVolumeSFX = Mathf.Clamp(configData.audioVolumeSFX + value, 0, volumeMax);
                SetVisualToggles(visualTogglesSFX, configData.audioVolumeSFX);
                break;
            default:
                break;
        }
    }

    // performance optimizations can wait
    private void SetVisualToggles(List<GameObject> togglesList, int amount)
    {
        for(int i = 0; i < togglesList.Count; i++)
        {
            if(i < amount)
            {
                togglesList[i].SetActive(true);
            }
            else
            {
                togglesList[i].SetActive(false);
            }
        }
    }
}


public enum menu_config_audio_volume_button_type
{
    UNDEFINED,

    //MasterUp,
    //MasterDown,
    Master,

    //MusicUp,
    //MusicDown,
    Music,

    //sfxUp,
    //sfxDown
    SFX
}
