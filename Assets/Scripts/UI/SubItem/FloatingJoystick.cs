using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FloatingJoystick : MonoBehaviour , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
    public RectTransform Background;
    public RectTransform Handle;
    [Range(0,2f)] public float HandleLimit = 1f;
    Vector2 input = Vector2.zero;
    Vector2 JoyPosition = Vector2.zero;

    void Start()
    {
       Background.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData evt)
    {
        Background.gameObject.SetActive(true);
        Handle.anchoredPosition = Vector2.zero;
        JoyPosition = evt.position;
        Background.position = evt.position;
        OnDrag(evt);
    }
    public void OnDrag(PointerEventData evt)
    {
        Handle.anchoredPosition = (input * Background.sizeDelta.x / 2f) * HandleLimit;
        Vector2 JoyDir = evt.position - RectTransformUtility.WorldToScreenPoint(new Camera(), Background.position);
        input = (JoyDir.magnitude > Background.sizeDelta.x / 2f) ? JoyDir.normalized : JoyDir / (Background.sizeDelta.x / 2f);
        Managers.Input.joyX = input.x;
        Managers.Input.joyY = input.y;

    }
    public void OnPointerUp(PointerEventData evt)
    {
        Background.gameObject.SetActive(false);
        Handle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
        Managers.Input.joyX = 0f;
        Managers.Input.joyY = 0f;
    }

}
