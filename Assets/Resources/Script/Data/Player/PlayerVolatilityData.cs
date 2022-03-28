using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 저장하지 않으면 사라지는 휘발성 데이터
/// </summary>
[Serializable]
public class PlayerVolatilityData
{
    /// <summary>
    /// 플레이어 인덱스
    /// </summary>
    public int index;

    /// <summary>
    /// 플레이어 레벨
    /// </summary>
    public int level;

    /// <summary>
    /// 마지막에 있던 스테이지 인덱스
    /// </summary>
    public int stageIndex;

    /// <summary>
    /// 위치 X값
    /// </summary>
    public float posX;

    /// <summary>
    /// 위치 Y값
    /// </summary>
    public float posY;

    /// <summary>
    /// 위치 Z값
    /// </summary>
    public float posZ;

    /// <summary>
    /// 소유중인 룬
    /// </summary>
    public float rune;

    /// <summary>
    /// 능력치를 올리지 않았거나, 올린 최대 체력
    /// </summary>
    public float raiseHp;

    /// <summary>
    /// 저장 전, 현재 체력
    /// </summary>
    public float currentHp;

    /// <summary>
    /// 능력치를 올리지 않았거나, 올린 공격력
    /// </summary>
    public float raiseAtk;

    /// <summary>
    /// 능력치를 올리지 않았거나, 올린 방어력
    /// </summary>
    public float raiseDef;

    public PlayerVolatilityData(int index, int level, int stageIndex, Vector3 pos, float rune, float raiseHp, float currentHp, float raiseAtk, float raiseDef)
    {
        this.index = index;
        this.level = level;
        this.stageIndex = stageIndex;
        this.posX = pos.x;
        this.posY = pos.y;
        this.posZ = pos.z;
        this.rune = rune;
        this.raiseHp = raiseHp;
        this.currentHp = currentHp;
        this.raiseAtk = raiseAtk;
        this.raiseDef = raiseDef;
    }
}
