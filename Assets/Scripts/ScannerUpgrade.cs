using UnityEngine;

public class ScannerUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public bool interactable = true;
    public ScannerUpgrade[] otherTriggers;

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
        otherTriggers = FindObjectsOfType<ScannerUpgrade>();
        foreach (ScannerUpgrade clickListener in otherTriggers)
        {
            clickListener.interactable = false;
            _spriteRenderer.enabled = false;
        }

        GameObject upgradeUIPanel = GameObject.Find("OverlayCanvas").transform.GetChild(3).gameObject;
        UIManager.Instance.ActivateUpgradeScannerPanel(upgradeUIPanel, transform.GetChild(0).GetComponent<Scanner>());
    }
}
