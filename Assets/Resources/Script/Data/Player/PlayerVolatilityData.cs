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

    public PlayerVolatilityData() { }

    /// <summary>
    /// SaveData에 사용되는 휘발성 데이터
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="pos"></param>
    /// <param name="stageData"></param>
    public PlayerVolatilityData(UsePlayerData playerData, Vector3 pos, StageData stageData)
    {
        if (playerData == null)
            return;

        this.index = playerData.index;
        this.level = playerData.level;
        this.stageIndex = stageData.index;
        this.posX = pos.x;
        this.posY = pos.y;
        this.posZ = pos.z;
        this.rune = playerData.currentRune;
        this.raiseHp = playerData.maxHp;
        this.currentHp = playerData.currentHp;
        this.raiseAtk = playerData.atk;
        this.raiseDef = playerData.def;
    }

    /// <summary>
    /// NewDataReturn에 사용되는 데이터 (저장된 데이터 초기값으로 리턴)
    /// </summary>
    /// <param name="characterIndex"></param>
    public PlayerVolatilityData(int characterIndex)
    {
        this.index = characterIndex;
        this.level = 1;
        this.stageIndex = 0;
        this.posX = 0;
        this.posY = 0;
        this.posZ = 0;
        this.rune = 0;
        this.raiseHp = 0;
        this.currentHp = 0;
        this.raiseAtk = 0;
        this.raiseDef = 0;
    }
}
