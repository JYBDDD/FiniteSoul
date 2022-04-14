using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ͱ� ����ϴ� ��ü ������
/// </summary>
[Serializable]
public class UseMonsterData : MonsterData
{
    public float currentHp;

    public UseMonsterData() { }

    // Ŭ������ ���Ͽ� ���� ������� �ʰ� �ϱ����� ������ ���� (�����͸� �����Ҷ� ���)
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

