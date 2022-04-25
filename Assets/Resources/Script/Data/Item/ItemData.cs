using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������
/// </summary>
[Serializable]
public class ItemData : StaticData
{
    /// <summary>
    /// ������ �̸�
    /// </summary>
    public string name;

    /// <summary>
    /// ��밡���� ���� �ε��� (1000 �Ͻ� ����)
    /// </summary>
    public int jobIndex;

    /// <summary>
    /// ������ Ÿ�� (���,�Һ�,��Ÿ)
    /// </summary>
    public Define.ItemMixEnum.ItemType itemType;

    /// <summary>
    /// ������ ����Ÿ�� (�Ѽ�, �μ�)
    /// </summary>
    public Define.ItemMixEnum.ItemHandedType handed;

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public int currentHandCount;

    /// <summary>
    /// ��� Ȯ��
    /// </summary>
    public float dropPer;

    /// <summary>
    /// ������ ����
    /// </summary>
    public float salePrice;

    /// <summary>
    /// ������ ����
    /// </summary>
    public string description;

    /// <summary>
    /// �̹���(�ý���) ���
    /// </summary>
    public string resourcePath;
}
