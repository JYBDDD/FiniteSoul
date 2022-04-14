using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터가 사용하는 전체 데이터
/// </summary>
[Serializable]
public class UseMonsterData : MonsterData
{
    public float currentHp;

    public UseMonsterData() { }

    // 클래스로 인하여 값이 연결되지 않게 하기위해 값으로 삽입 (데이터를 삽입할때 사용)
    public UseMonsterData(UseMonsterData monsterData)
    {
        index = monsterData.index;
        name = monsterData.name;
        characterType = monsterData.characterType;
        atkType = monsterData.atkType;
        moveSpeed = monsterData.moveSpeed;
        viewingAngle = monsterData.viewingAngle;
        viewDistance = monsterData.viewDistance;
        atkRange = monsterData.atkRange;
        maxHp = monsterData.maxHp;
        atk = monsterData.atk;
        def = monsterData.def;
        dropRune = monsterData.dropRune;
        dropItemPer = monsterData.dropItemPer;
        resourcePath = monsterData.resourcePath;
    }
}

