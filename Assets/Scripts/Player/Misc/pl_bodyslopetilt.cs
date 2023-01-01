using UnityEngine;

public class pl_bodyslopetilt : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    //[SerializeField] float transitionSpeed;

    //float sprSlopeOffsetY = -0.5f;
    //int sprSlopeRotZ = 45;

    int currentTransitionTarget;
    float currentTransitionFactor;

    int mostRecentSlope;

    private void FixedUpdate()
    {
        //SnapSpriteTrans();

        if(refs.info.slope == 0)
        {
            currentTransitionTarget = 0;
        }
        else
        {
            currentTransitionTarget = 1;
            mostRecentSlope = refs.info.slope; // serves as mult for rotation lerp (needs to be saved for duration of transition after player leaves slope)
        }

        if (currentTransitionFactor != currentTransitionTarget)
        {
            HandleSpriteTransition();
        }
    }


    private void HandleSpriteTransition()
    {
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, currentTransitionTarget, refs.settings.visual.transitionSpeed * Time.fixedDeltaTime);

        refs.FlipContainerTrans.localRotation = Quaternion.Euler(Vector3.Lerp(
            Vector3.zero,
            new Vector3(0, 0, refs.settings.visual.bodyRotZ * -mostRecentSlope),
            currentTransitionFactor
            ));

        refs.FlipContainerTrans.localPosition = Vector3.Lerp(
            Vector3.zero,
            new Vector3(0, refs.settings.visual.bodyOffsetY, 0),
            currentTransitionFactor
            );
    }


    //private void SnapSpriteTrans()
    //{
    //    if (refs.info.slope == 0)
    //    {
    //        refs.FlipContainerTrans.localRotation = Quaternion.Euler(Vector3.zero);
    //        refs.FlipContainerTrans.localPosition = Vector3.zero;
    //    }
    //    else
    //    {
    //        refs.FlipContainerTrans.localRotation = Quaternion.Euler(0, 0, sprSlopeRotZ * -refs.info.slope);
    //        refs.FlipContainerTrans.localPosition = new Vector3(0, sprSlopeOffsetY, 0);
    //    }
    //}
}
