using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 사용하는 전체 데이터
/// </summary>
[Serializable]
public class UsePlayerData : PlayerData
{
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

    public UsePlayerData(GrowthStatData growthStatData)
    {
        growthStat = growthStatData;
    }
}
