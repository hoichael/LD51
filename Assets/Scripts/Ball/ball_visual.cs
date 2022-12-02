using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_visual : MonoBehaviour
{
    [SerializeField] ball_refs refs;
    [SerializeField] Transform spriteContainer;
    Vector3 rotDir = new Vector3(0, 0, 1);

    bool currentlyThrown;

    public void Reset()
    {
        currentlyThrown = false;
    }

    public void InitThrow()
    {
        refs.rb.transform.localScale = Vector3.one;
        currentlyThrown = true;
    }

    private void Update()
    {
        if (!currentlyThrown) return;
        RotateSprite();
    }

    private void RotateSprite()
    {
        spriteContainer.Rotate((rotDir * GetRotSpeed()) * Time.deltaTime);
    }

    private float GetRotSpeed()
    {
        return -refs.rb.velocity.x * 24f;
    }

    private void OnDisable()
    {
        Reset();
    }
}
