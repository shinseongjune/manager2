using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WindowMovement : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform windowRectTransform;
    private Canvas canvas;

    private void Awake()
    {
        if (windowRectTransform == null)
        {
            windowRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        if (canvas == null)
        {
            Transform tempTransform = transform.parent;
            while (tempTransform != null)
            {
                canvas = tempTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                tempTransform = tempTransform.parent;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        windowRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        windowRectTransform.SetAsLastSibling();
    }
}
