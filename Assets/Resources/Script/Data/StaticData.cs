using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 Moveable이 가지고 있는 데이터
/// </summary>
[Serializable]
public class StaticData
{
    /// <summary>
    /// 캐릭터 인덱스
    /// </summary>
    public int index;

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
    /// 이동 속도
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// 리소스 경로
    /// </summary>
    public string resourcePath;
}
