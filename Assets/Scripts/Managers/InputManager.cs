using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action JoyAction = null;

    float _joyX = 0f;
    float _joyY = 0f;
    bool zeroCheck = false;

    public float joyX
    {
        get { return _joyX; }
        set { _joyX = value; }
    }
    public float joyY
    {
        get { return _joyY; }
        set { _joyY = value; }
    }

    public void OnUpdate()
    {
        if (JoyAction != null)
        {

            if (_joyX == 0 && _joyY == 0) { if(!zeroCheck) { zeroCheck = true; JoyAction.Invoke(); } }
            else zeroCheck = false;

            if(!zeroCheck) JoyAction.Invoke();
        }
    }

    public void Clear()
    {
        JoyAction = null;
    }
}
