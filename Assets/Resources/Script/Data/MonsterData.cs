using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 데이터
/// </summary>
[Serializable]
public class MonsterData : StaticData
{
    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    public string name;

    /// <summary>
    /// 공격 타입
    /// </summary>
    public Define.AtkType atkType;

    /// <summary>
    /// 공격력
    /// </summary>
    public float atk;

    /// <summary>
    /// 방어력
    /// </summary>
    public float def;

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHp;

    /// <summary>
    /// 감지 범위
    /// </summary>
    public float detectionRange;

    /// <summary>
    /// 공격 범위
    /// </summary>
    public float atkRange;

    /// <summary>
    /// 드랍하는 룬의 양
    /// </summary>
    public float dropRune;

    /// <summary>
    /// 드랍하는 아이템
    /// </summary>
    public int dropItem;

    /// <summary>
    /// 아이템 드랍 확률
    /// </summary>
    public float dropItemPer;

    /// <summary>
    /// 리소스 경로
    /// </summary>
    public string ResourcePath;
}
