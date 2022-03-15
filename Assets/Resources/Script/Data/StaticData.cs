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
    /// ĳ���� �ε���
    /// </summary>
    public int index;

    /// <summary>
    /// ĳ���� �̸�
    /// </summary>
    public string name;

    /// <summary>
    /// ĳ���� Ÿ��
    /// </summary>
    public Define.CharacterType characterType;

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public Define.AtkType atkType;

    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float atk;

    /// <summary>
    /// ����
    /// </summary>
    public float def;

    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float maxHp;

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// ���ҽ� ���
    /// </summary>
    public string resourcePath;
}
