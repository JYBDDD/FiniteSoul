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

    public float maxHp;
    public float extraHp;
    public float maxExp;
    public float growthExp;
    public float atk;
    public float extraAtk;
    public float def;
    public float extraDef;
}
