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
        oldQueuePosition = new Vector3(0, -2.25f, 0);

        GameObject current = Instantiate(queuePrefab, oldQueuePosition, queueParent.rotation, queueParent);
        current.GetComponent<Queue>().queueId = 1;
    }

    public void AddLane()
    {
        oldQueuePosition.y -= 3f;
        GameObject current = Instantiate(queuePrefab, oldQueuePosition, queueParent.rotation, queueParent);
        current.GetComponent<Queue>().queueId = QueueCount.queueCount + 1;

        Vector3 newButtonPos = new Vector3(0, canvas.transform.position.y - 3f, 0);
        canvas.transform.position = newButtonPos;
    }
}
