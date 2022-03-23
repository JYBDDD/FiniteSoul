using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������
/// </summary>
[Serializable]
public class MonsterData : StaticData
{
    /// <summary>
    /// ĳ���� �̸�
    /// </summary>
    public string name;

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
    /// ���� ����
    /// </summary>
    public float detectionRange;

    /// <summary>
    /// ���� ����
    /// </summary>
    public float atkRange;

    /// <summary>
    /// ����ϴ� ���� ��
    /// </summary>
    public float dropRune;

    /// <summary>
    /// ����ϴ� ������
    /// </summary>
    public int dropItem;

    /// <summary>
    /// ������ ��� Ȯ��
    /// </summary>
    public float dropItemPer;

    /// <summary>
    /// ���ҽ� ���
    /// </summary>
    public string ResourcePath;
}
