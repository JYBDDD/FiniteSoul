using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ������
/// </summary>
[Serializable]
public class PlayerData : StaticData
{
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
    /// �ִ� ü��
    /// </summary>
    public float maxHp;

    /// <summary>
    /// �ִ� ���׹̳�
    /// </summary>
    public float maxStamina;

    /// <summary>
    /// �ش� ������ ��¿� �ʿ��� �� -> �ش� �� �̻��̶�� �ɷ�ġ ��� ����
    /// </summary>
    public float maxRune;

    /// <summary>
    /// �ִ� ����
    /// </summary>
    public float maxMana;

    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float atk;

    /// <summary>
    /// ����
    /// </summary>
    public float def;

    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// ���� ��
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// �����ų �� �ִ� �ɷ�ġ �����ε���
    /// </summary>
    public int growthRef;

    /// <summary>
    /// ���ҽ� ���
    /// </summary>
    public string resourcePath;
}
