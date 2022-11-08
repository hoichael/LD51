using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pl_xhair : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    Vector2 currentMouseInput;
    Vector2 newPosClamped;

    private void Update()
    {
        //currentMouseInput = new Vector2(
        //    Input.GetAxisRaw("Mouse X"),
        //    Input.GetAxisRaw("Mouse Y")
        //    );

        ////newPosClamped = new Vector2(
        ////    Mathf.Clamp(refs.xHairTrans.localPosition.x + Input.GetAxisRaw("Mouse X"), -refs.settings.xHairClamp, refs.settings.xHairClamp),
        ////    Mathf.Clamp(refs.xHairTrans.localPosition.y + Input.GetAxisRaw("Mouse Y"), - refs.settings.xHairClamp, refs.settings.xHairClamp)
        ////    );

        //newPosClamped = (Vector2)refs.xHairTrans.localPosition + currentMouseInput;
        //newPosClamped = Vector2.ClampMagnitude(newPosClamped, 9);

        ////refs.xHairTrans.position = (Vector2)refs.xHairTrans.position + currentMouseInput;
        //refs.xHairTrans.localPosition = newPosClamped;
    }
}
