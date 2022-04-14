using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어, 몬스터 등 관련 모든 데이터를 들고 있는 클래스
/// </summary>
[Serializable]
public struct FullDataCollection
{
    public UsePlayerData[] playersData;
    public GrowthStatData[] growthsData;
    public UseMonsterData[] monstersData;
    public StageData[] stagesData;
    public ItemData[] itemsData;

    public FullDataCollection(UsePlayerData[] usePlayers,GrowthStatData[] growths,UseMonsterData[] useMonsters,StageData[] stages,ItemData[] items)
    {
        playersData = usePlayers;
        growthsData = growths;
        monstersData = useMonsters;
        stagesData = stages;
        itemsData = items;
    }
}

