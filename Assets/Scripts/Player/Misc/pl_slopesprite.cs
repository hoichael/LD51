using UnityEngine;

public class pl_slopesprite : MonoBehaviour
{
    [SerializeField] pl_refs refs;
    [SerializeField] float transitionSpeed;

    float sprSlopeOffsetY = -0.5f;
    int sprSlopeRotZ = 45;

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
        currentTransitionFactor = Mathf.MoveTowards(currentTransitionFactor, currentTransitionTarget, transitionSpeed * Time.fixedDeltaTime);

        refs.FlipContainerTrans.localRotation = Quaternion.Euler(Vector3.Lerp(
            Vector3.zero,
            new Vector3(0, 0, sprSlopeRotZ * -mostRecentSlope),
            currentTransitionFactor
            ));

        refs.FlipContainerTrans.localPosition = Vector3.Lerp(
            Vector3.zero,
            new Vector3(0, sprSlopeOffsetY, 0),
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
