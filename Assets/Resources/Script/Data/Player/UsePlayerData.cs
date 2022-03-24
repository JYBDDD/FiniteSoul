using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �÷��̾ ����ϴ� ��ü ������
/// </summary>
[Serializable]
public class UsePlayerData : PlayerData
{
    public int level;
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

    /// <summary>
    /// �÷��̾� �ֹ߼� ������ (������ �ε�� ���)
    /// </summary>
    public PlayerVolatilityData playerVolatility;

    public UsePlayerData() { }

    public UsePlayerData(GrowthStatData growthStatData,UsePlayerData usePlayerData,PlayerVolatilityData playerVolatilityData)
    {
        growthStat = growthStatData;

        index = usePlayerData.index;
        name = usePlayerData.name;
        type = usePlayerData.type;
        atkType = usePlayerData.atkType;
        maxStamina = usePlayerData.maxStamina;
        moveSpeed = usePlayerData.moveSpeed;
        jumpForce = usePlayerData.jumpForce;
        growthRef = usePlayerData.growthRef;
        resourcePath = usePlayerData.resourcePath;

        playerVolatility = playerVolatilityData;
    }

}
