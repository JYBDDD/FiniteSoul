using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 성장 스텟 데이터
/// </summary>
[Serializable]
public class GrowthStatData : StaticData
{
    // growth.. -> 레벨업시 자동으로 증가하는 데이터
    // extra.. -> 포인트 배분시 올릴 수 있는 데이터

    public float maxHp;
    public float extraHp;
    public float maxExp;
    public float growthExp;
    public float atk;
    public float extraAtk;
    public float def;
    public float extraDef;
}
