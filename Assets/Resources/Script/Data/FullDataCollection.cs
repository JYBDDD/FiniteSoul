using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�, ���� �� ���� ��� �����͸� ��� �ִ� Ŭ����
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

