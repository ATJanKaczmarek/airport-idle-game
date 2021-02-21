using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public Transform bgBottom;
    private bool hasNotBeenCalled = true;

    private void Update()
    {
        if (transform.position.y <= -12.94266f && hasNotBeenCalled == true)
        {
            SetBottomBackground();
        }
    }

    private void SetBottomBackground()
    {
        hasNotBeenCalled = false;
        bgBottom.SetParent(transform);
        bgBottom.localPosition = new Vector3(0f, 0f, 10f);
    }
}
