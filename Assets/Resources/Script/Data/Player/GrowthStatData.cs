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

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHp;

    /// <summary>
    /// 올릴 수 있는 체력
    /// </summary>
    public float extraHp;

    /// <summary>
    /// 레벨업을 하려고할때 사용해야할 룬
    /// </summary>
    public float maxRune;

    /// <summary>
    /// 룬 * 레벨 * growthExp
    /// </summary>
    public float growthExp;

    /// <summary>
    /// 공격력
    /// </summary>
    public float atk;

    /// <summary>
    /// 올릴 수 있는 공격력
    /// </summary>
    public float extraAtk;

    /// <summary>
    /// 방어력
    /// </summary>
    public float def;

    /// <summary>
    /// 올릴 수 있는 방어력
    /// </summary>
    public float extraDef;
}
