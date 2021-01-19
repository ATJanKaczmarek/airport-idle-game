using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueAdding : MonoBehaviour
{
    public GameObject queuePrefab;


    public GameObject canvas;

    private Vector3 oldQueuePosition;

    private void Start()
    {
        oldQueuePosition = new Vector3(0, 2.5f, 0);

        Instantiate(queuePrefab, oldQueuePosition, new Quaternion(0, 0, 0, 1));


    }

    public void AddQueueButtonPress()
    {
        oldQueuePosition.y -= 2.5f;
        Instantiate(queuePrefab, oldQueuePosition, new Quaternion(0, 0, 0, 1));

        Vector3 newButtonPos = new Vector3(0, canvas.transform.position.y - 2.5f, 0);
        canvas.transform.position = newButtonPos;
        //canvas.transform.position.y 
    }


}
