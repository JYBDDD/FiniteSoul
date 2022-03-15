using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 이동, UI활성화 입력을 받을때 사용할 클래스
/// </summary>
public class InputManager : Singleton<InputManager>
{
    public Action KeyAction = null;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        KeyActionUpdate();
    }

    private void KeyActionUpdate()
    {
        if(Input.anyKey)
        {
            KeyAction?.Invoke();
        }
    }

}
