using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Config", fileName = "Config", order = 0)]
public class SO_config : ScriptableObject
{
    public bool fullscreen;

    public int audioVolumeMaster;
    public int audioVolumeMusic;
    public int audioVolumeSFX;
}
