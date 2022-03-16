using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ����ϴ� ��ü ������
/// </summary>
[Serializable]
public class UsePlayerData : PlayerData
{
    public float currentHp;
    public float currentMana;
    public float currentStamina;

    /// <summary>
    /// ���� ��� �ִ� �� -> ���Ѵ�� ��� ���� �� ����
    /// </summary>
    public float currentRune;   

    /// <summary>
    /// �÷��̾� ����, �߰� ������
    /// </summary>
    public GrowthStatData growthStat;

    public UsePlayerData(GrowthStatData growthStatData)
    {
        growthStat = growthStatData;
    }
}
