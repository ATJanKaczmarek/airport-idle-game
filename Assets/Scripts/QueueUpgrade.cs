using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

    }

    private void OnMouseEnter()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        ClickEvent();
    }

    public void ClickEvent()
    {
        GameObject upgradeUIPanel = GameObject.Find("OverlayCanvas").transform.GetChild(2).gameObject;
        UIManager.Instance.ActivateUpgradeQueuePanel(upgradeUIPanel, transform.parent.GetComponent<Queue>());
    }
}
