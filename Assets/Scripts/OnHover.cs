using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent onHover;
    private bool isHovering = false;

    private void Update()
    {
        if (isHovering == true)
        {
            onHover.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            isHovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
