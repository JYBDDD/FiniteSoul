using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 데이터
/// </summary>
[Serializable]
public class PlayerData : StaticData
{
    /// <summary>
    /// 캐릭터 이름
    /// </summary>
    public string name;

    /// <summary>
    /// 캐릭터 타입
    /// </summary>
    public Define.CharacterType characterType;

    /// <summary>
    /// 공격 타입
    /// </summary>
    public Define.AtkType atkType;

    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHp;

    /// <summary>
    /// 최대 스테미너
    /// </summary>
    public float maxStamina;

    /// <summary>
    /// 해당 레벨의 상승에 필요한 룬 -> 해당 룬 이상이라면 능력치 상승 가능
    /// </summary>
    public float maxRune;

    /// <summary>
    /// 최대 마나
    /// </summary>
    public float maxMana;

    /// <summary>
    /// 공격력
    /// </summary>
    public float atk;

    /// <summary>
    /// 방어력
    /// </summary>
    public float def;

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 점프 값
    /// </summary>
    public float jumpForce;

    /// <summary>
    /// 성장시킬 수 있는 능력치 참조인덱스
    /// </summary>
    public int growthRef;

    /// <summary>
    /// 리소스 경로
    /// </summary>
    public string resourcePath;
}
