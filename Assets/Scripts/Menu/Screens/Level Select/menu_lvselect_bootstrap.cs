using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_lvselect_bootstrap : MonoBehaviour
{
    [SerializeField] menu_lvselect_data data;

    public SpriteRenderer[] Init(SO_menu_worldinfo worldInfo)
    {
        return GenerateElements(worldInfo.levelInfoList.Count);
    }

    private SpriteRenderer[] GenerateElements(int count)
    {
        Vector3 nextElementPos = data.firstElementPos;
        int currentRow = 1;
        SpriteRenderer[] sprArr = new SpriteRenderer[count];

        // "dispose" of existing selectables. not great but will do
        foreach (Transform lvSelectElement in data.lvElcontainer)
        {
            Destroy(lvSelectElement.gameObject);
        }

        for (int i = 1; i < count + 1; i++)
        {
            // create and position element
            GameObject obj = new GameObject();
            obj.transform.SetParent(data.lvElcontainer);
            obj.transform.localPosition = nextElementPos;

            // handle sprite
            SpriteRenderer spr = obj.AddComponent<SpriteRenderer>();
            spr.sprite = data.sprIconDefault;
            spr.sortingOrder = 1;
            sprArr[i - 1] = spr;

            // determine next element position
            if(i == data.lvElRowSize * currentRow)
            {
                nextElementPos = new Vector3(data.firstElementPos.x, nextElementPos.y - data.lvElSpacing, data.firstElementPos.z);
                currentRow++;
            }
            else
            {
                nextElementPos.x += data.lvElSpacing;
            }
        }

        return sprArr;
    }
}
