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

    // 밑에 것들은 몬스터 데이터, 플레이어 데이터에 붙여 쓰셈 , 여기선 필요 없음 TODO
    // 각각 데이터들은 해당 리소스 경로로 해당 객체에 데이터를 주입시켜주는 방식으로 사용할 것임
/*
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
    public string resourcePath;*/
}
