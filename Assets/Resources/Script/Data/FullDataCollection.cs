using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�, ���� �� ���� ��� �����͸� ��� �ִ� Ŭ����
/// </summary>
[Serializable]
public class FullDataCollection
{
    public List<UsePlayerData> playersData = new List<UsePlayerData>();
    public List<GrowthStatData> growthsData = new List<GrowthStatData>();
    public List<UseMonsterData> monstersData = new List<UseMonsterData>();
}

