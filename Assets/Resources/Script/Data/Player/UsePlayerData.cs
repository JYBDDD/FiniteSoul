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

    /// <summary>
    /// �ʱ� �÷��̾� ������ �߰��ϴ� ������
    /// </summary>
    /// <param name="growthStatData"></param>
    /// <param name="usePlayerData"></param>
    /// <param name="playerVolatilityData"></param>
    public UsePlayerData(GrowthStatData growthStatData,UsePlayerData usePlayerData,PlayerVolatilityData playerVolatilityData)
    {
        growthStat = growthStatData;

        index = usePlayerData.index;
        level = playerVolatilityData.level;
        name = usePlayerData.name;
        characterType = usePlayerData.characterType;
        atkType = usePlayerData.atkType;
        maxStamina = usePlayerData.maxStamina;
        maxMana = usePlayerData.maxMana;
        moveSpeed = usePlayerData.moveSpeed;
        jumpForce = usePlayerData.jumpForce;
        growthRef = usePlayerData.growthRef;
        resourcePath = usePlayerData.resourcePath;

        playerVolatility = playerVolatilityData;
    }

    /// <summary>
    /// �������ͽ� ���� �� �÷��̾� ���� ������ ����ϴ� ������
    /// </summary>
    /// <param name="usePlayerData"></param>
    public UsePlayerData(UsePlayerData usePlayerData,GrowthStatData growthStatData)
    {
        if (usePlayerData == null)
            return;

        growthStat = growthStatData;

        currentRune = usePlayerData.currentRune;
        maxRune = usePlayerData.growthStat.maxRune * usePlayerData.level * usePlayerData.growthStat.growthRune;
        maxHp = usePlayerData.maxHp;
        currentHp = usePlayerData.currentHp;
        currentMana = usePlayerData.currentMana;
        currentStamina = usePlayerData.currentStamina;
        level = usePlayerData.level;
        atk = usePlayerData.atk;
        def = usePlayerData.def;
    }

}
