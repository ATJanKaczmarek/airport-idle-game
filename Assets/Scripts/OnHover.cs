using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public UnityEvent onHover;
    public UnityEvent hoverEnd;
    public UnityEvent onClick;
    private bool isHovering = false;

    private void Update()
    {
        if (isHovering == true)
        {
            onHover.Invoke();
        }
        else
        {
            hoverEnd.Invoke();
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

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}
