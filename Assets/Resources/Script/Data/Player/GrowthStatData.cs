using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� ���� ������
/// </summary>
[Serializable]
public class GrowthStatData : StaticData
{
    // growth.. -> �������� �ڵ����� �����ϴ� ������
    // extra.. -> ����Ʈ ��н� �ø� �� �ִ� ������

    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float maxHp;

    /// <summary>
    /// �ø� �� �ִ� ü��
    /// </summary>
    public float extraHp;

    /// <summary>
    /// �������� �Ϸ����Ҷ� ����ؾ��� ��
    /// </summary>
    public float maxRune;

    /// <summary>
    /// �� * ���� * growthExp
    /// </summary>
    public float growthExp;

    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float atk;

    /// <summary>
    /// �ø� �� �ִ� ���ݷ�
    /// </summary>
    public float extraAtk;

    /// <summary>
    /// ����
    /// </summary>
    public float def;

    /// <summary>
    /// �ø� �� �ִ� ����
    /// </summary>
    public float extraDef;
}
