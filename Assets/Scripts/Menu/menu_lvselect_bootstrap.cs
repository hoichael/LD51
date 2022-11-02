using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_lvselect_bootstrap : MonoBehaviour
{
    [SerializeField] Transform lvElcontainer;
    [SerializeField] float lvElSpacing;
    //[SerializeField] float lvELSize;
    [SerializeField] int lvElRowSize;
    [SerializeField] Vector3 firstElementPos;
    [SerializeField] Sprite iconSprite;

    public void Init(SO_menu_worldinfo worldInfo)
    {
        GenerateElements(worldInfo.levelInfoList.Count);
    }

    private void GenerateElements(int count)
    {
        Vector3 nextElementPos = firstElementPos;
        int currentRow = 1;

        for(int i = 1; i < count + 1; i++)
        {
            // create and position element
            GameObject obj = new GameObject();
            obj.transform.SetParent(lvElcontainer);
            obj.transform.localPosition = nextElementPos;

            // handle sprite
            SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
            spr.sprite = iconSprite;
            spr.sortingOrder = 1;

            // determine next element position
            if(i == lvElRowSize * currentRow)
            {
                nextElementPos = new Vector3(firstElementPos.x, nextElementPos.y - lvElSpacing, firstElementPos.z);
                currentRow++;
            }
            else
            {
                nextElementPos.x += lvElSpacing;
            }
        }
    }
}
