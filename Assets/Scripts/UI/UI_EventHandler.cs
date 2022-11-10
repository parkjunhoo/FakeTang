using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler , IDragHandler
{
    public Action<PointerEventData> OnPointerClickHandler = null;
    public Action<PointerEventData> OnPointerEnterHandler = null;
    public Action<PointerEventData> OnPointerExitHandler = null;

    public Action<PointerEventData> OnDragHandler = null;




    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnPointerClickHandler != null) OnPointerClickHandler.Invoke(eventData);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterHandler != null) OnPointerEnterHandler.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitHandler != null) OnPointerExitHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null) OnDragHandler.Invoke(eventData);
    }




}
