using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FixedJoystick : MonoBehaviour , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
    public RectTransform Background;
    public RectTransform Handle;
    [Range(0,2f)] public float HandleLimit = 1f;
    Vector2 input = Vector2.zero;

    public void OnPointerDown(PointerEventData evt)
    {
        OnDrag(evt);
    }
    public void OnDrag(PointerEventData evt)
    {
        Vector2 JoyDir = evt.position - RectTransformUtility.WorldToScreenPoint(new Camera(), Background.position);
        input = (JoyDir.magnitude > Background.sizeDelta.x / 2f) ? JoyDir.normalized : JoyDir / (Background.sizeDelta.x / 2f);
        Managers.Input.joyX = input.x;
        Managers.Input.joyY = input.y;
        Handle.anchoredPosition = (input * Background.sizeDelta.x / 2f) * HandleLimit;
        

    }
    public void OnPointerUp(PointerEventData evt)
    {
        input = Vector2.zero;
        Managers.Input.joyX = input.x;
        Managers.Input.joyY = input.y;
        Handle.anchoredPosition = Vector2.zero;
    }

}
