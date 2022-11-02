using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_lvselect_bootstrap : MonoBehaviour
{
    [SerializeField] menu_lvselect_settings settings;

    public SpriteRenderer[] Init(SO_menu_worldinfo worldInfo)
    {
        return GenerateElements(worldInfo.levelInfoList.Count);
    }

    private SpriteRenderer[] GenerateElements(int count)
    {
        Vector3 nextElementPos = settings.firstElementPos;
        int currentRow = 1;
        SpriteRenderer[] sprArr = new SpriteRenderer[count];

        for(int i = 1; i < count + 1; i++)
        {
            // create and position element
            GameObject obj = new GameObject();
            obj.transform.SetParent(settings.lvElcontainer);
            obj.transform.localPosition = nextElementPos;

            // handle sprite
            SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
            spr.sprite = settings.sprIconDefault;
            spr.sortingOrder = 1;
            sprArr[i - 1] = spr;

            // determine next element position
            if(i == settings.lvElRowSize * currentRow)
            {
                nextElementPos = new Vector3(settings.firstElementPos.x, nextElementPos.y - settings.lvElSpacing, settings.firstElementPos.z);
                currentRow++;
            }
            else
            {
                nextElementPos.x += settings.lvElSpacing;
            }
        }

        return sprArr;
    }
}
