using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MenuWorldInfo", fileName = "menu_worldinfo_", order = 0)]
public class SO_menu_worldinfo : ScriptableObject
{
    [SerializeField] public List<menu_levelinfo> levelInfoList;
}

[System.Serializable]
public class menu_levelinfo
{
    public string name;
    public Sprite thumbnail;
}
