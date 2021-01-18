using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueAdding : MonoBehaviour
{
    public GameObject queuePrefab;

    
    private void Start()
    {
        

        Instantiate(queuePrefab);
            
    }

    public void AddqueueButtonPress()
    {

    }
   
}
