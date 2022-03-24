using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 플레이어가 사용하는 전체 데이터
/// </summary>
[Serializable]
public class UsePlayerData : PlayerData
{
    public int level;
    public float currentHp;
    public float currentMana;
    public float currentStamina;

    /// <summary>
    /// 현재 들고 있는 룬 -> 무한대로 들고 있을 수 있음
    /// </summary>
    public float currentRune;

    /// <summary>
    /// 플레이어 성장, 추가 데이터
    /// </summary>
    public GrowthStatData growthStat;

    /// <summary>
    /// 플레이어 휘발성 데이터 (데이터 로드시 사용)
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
