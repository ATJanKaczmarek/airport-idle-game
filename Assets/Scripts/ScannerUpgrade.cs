using UnityEngine;

public class ScannerUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public bool canBeTriggered = true;
    public ScannerUpgrade[] otherTriggers;

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
        otherTriggers = FindObjectsOfType<ScannerUpgrade>();
        foreach (ScannerUpgrade clickListener in otherTriggers)
        {
            clickListener.canBeTriggered = false;
            _spriteRenderer.enabled = false;
        }

        GameObject upgradeUIPanel = GameObject.Find("OverlayCanvas").transform.GetChild(3).gameObject;
        UIManager.Instance.ActivateUpgradeScannerPanel(upgradeUIPanel, transform.GetChild(0).GetComponent<Scanner>());
    }
}
