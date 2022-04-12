using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� Moveable�� ������ �ִ� ������
/// </summary>
[Serializable]
public class StaticData
{
    /// <summary>
    /// ��� �����Ͱ� ������ �ִ� �ε���
    /// </summary>
    public int index;

    /// <summary>
    /// �÷��̾����� �������� �������ִ� Ÿ��
    /// </summary>
    public Define.CharacterType characterType = Define.CharacterType.None;
}
