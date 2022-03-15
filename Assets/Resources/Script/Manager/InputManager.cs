using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� �̵�, UIȰ��ȭ �Է��� ������ ����� Ŭ����
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
