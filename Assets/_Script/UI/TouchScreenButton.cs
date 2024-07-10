using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchScreenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler
{


    public bool isPressed
    {
        get; protected set;
    }

    


    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isPressed = false;
    }

}
