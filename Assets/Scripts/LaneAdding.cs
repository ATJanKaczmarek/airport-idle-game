using TMPro;
using UnityEngine;

public class LaneAdding : MonoBehaviour
{
    public GameObject queuePrefab;
    public GameObject canvas;
    public Transform queueParent;
    private Vector3 oldQueuePosition;

    private void Start()
    {
        oldQueuePosition = new Vector3(0, 2.5f, 0);

        Instantiate(queuePrefab, oldQueuePosition, queueParent.rotation, queueParent);

    }

    public void AddLane()
    {
        oldQueuePosition.y -= 2.5f;
        Instantiate(queuePrefab, oldQueuePosition, queueParent.rotation, queueParent);

        Vector3 newButtonPos = new Vector3(0, canvas.transform.position.y - 2.5f, 0);
        canvas.transform.position = newButtonPos;
    }
}
