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

    /// <summary>
    /// 초기 플레이어 데이터 추가하는 생성자
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
    /// 스테이터스 수정 및 플레이어 스탯 설정시 사용하는 생성자
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
