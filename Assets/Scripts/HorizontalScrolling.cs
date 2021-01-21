using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScrolling : MonoBehaviour
{
    public void LeftButton()
    {
        if (Camera.main.transform.position.x > (2 * Camera.main.orthographicSize * Camera.main.aspect))
            Camera.main.transform.position = new Vector3(
                Camera.main.transform.position.x - (2 * Camera.main.orthographicSize * Camera.main.aspect),
                Camera.main.transform.position.y,
                Camera.main.transform.position.z
            );
        else
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    public void RightButton()
    {
        if (Camera.main.transform.position.x < 2 * (2 * Camera.main.orthographicSize * Camera.main.aspect))
            Camera.main.transform.position = new Vector3(
                Camera.main.transform.position.x + (2 * Camera.main.orthographicSize * Camera.main.aspect),
                Camera.main.transform.position.y,
                Camera.main.transform.position.z
            );
    }
}
