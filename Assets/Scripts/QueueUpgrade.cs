using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueueUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public bool canBeTriggered = true;
    public QueueUpgrade[] otherTriggers;

    private void OnEnable()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        canBeTriggered = true;
    }

    private void OnMouseEnter()
    {
        if (canBeTriggered == true)
            _spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        if (canBeTriggered == true)
            ClickEvent();
    }

    public void ClickEvent()
    {
        otherTriggers = FindObjectsOfType<QueueUpgrade>();
        foreach (QueueUpgrade clickListener in otherTriggers)
        {
            clickListener.canBeTriggered = false;
            _spriteRenderer.enabled = false;
        }

        GameObject upgradeUIPanel = GameObject.Find("OverlayCanvas").transform.GetChild(2).gameObject;
        UIManager.Instance.ActivateUpgradeQueuePanel(upgradeUIPanel, transform.parent.GetComponent<Queue>());
    }
}
