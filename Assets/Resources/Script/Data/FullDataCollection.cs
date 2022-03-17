using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어, 몬스터 등 관련 모든 데이터를 들고 있는 클래스
/// </summary>
[Serializable]
public class FullDataCollection
{
    public List<UsePlayerData> playersData;
    public List<GrowthStatData> growthsData;
    public List<UseMonsterData> monstersData;
}

