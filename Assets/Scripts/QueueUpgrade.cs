using UnityEngine;

public class QueueUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public bool interactable = true;
    public QueueUpgrade[] otherTriggers;

    private void OnEnable()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        interactable = true;
    }

    private void OnMouseEnter()
    {
        if (interactable == true)
            _spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        if (interactable == true)
            ClickEvent();
    }

    public void ClickEvent()
    {
        otherTriggers = FindObjectsOfType<QueueUpgrade>();
        foreach (QueueUpgrade clickListener in otherTriggers)
        {
            clickListener.interactable = false;
            _spriteRenderer.enabled = false;
        }

        GameObject upgradeUIPanel = GameObject.Find("OverlayCanvas").transform.GetChild(2).gameObject;
        UIManager.Instance.ActivateUpgradeQueuePanel(upgradeUIPanel, transform.parent.GetComponent<Queue>());
    }
}
