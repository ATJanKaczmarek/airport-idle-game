using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QueueCount : MonoBehaviour
{
    public static int queueCount = 1;

    public void IncreaseQueueCount()
    {
        queueCount++;
        UIManager.Instance.ResizeScrollbar(queueCount);
        UIManager.Instance.SetLaneText(queueCount);
    }
}
