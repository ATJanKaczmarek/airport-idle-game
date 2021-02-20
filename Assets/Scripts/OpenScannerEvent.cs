using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScannerEvent : MonoBehaviour
{
    public bool interactable = true;
    private SpriteRenderer _sprite;
    private Color _hoverColor = new Color(1f, 1f, 1f, 0.6f);

    private void Awake() => _sprite = GetComponent<SpriteRenderer>();

    private void OnMouseOver()
    {
        if (interactable == true)
            _sprite.color = _hoverColor;
    }

    private void OnMouseDown()
    {
        if (interactable == true && UIManager.Instance.hasActiveUIPanel == false)
        {
            UIManager.Instance.OpenScannerEvent(Constants.ScannerRewards.MONEY, gameObject);
        }
    }

    private void OnMouseExit()
    {
        _sprite.color = Color.white;
    }
}
