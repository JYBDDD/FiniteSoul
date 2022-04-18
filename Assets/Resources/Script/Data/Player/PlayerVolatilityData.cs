using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������ ������� �ֹ߼� ������
/// </summary>
[Serializable]
public class PlayerVolatilityData
{
    /// <summary>
    /// �÷��̾� �ε���
    /// </summary>
    public int index;

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public int level;

    /// <summary>
    /// �������� �ִ� �������� �ε���
    /// </summary>
    public int stageIndex;

    /// <summary>
    /// ��ġ X��
    /// </summary>
    public float posX;

    /// <summary>
    /// ��ġ Y��
    /// </summary>
    public float posY;

    /// <summary>
    /// ��ġ Z��
    /// </summary>
    public float posZ;

    /// <summary>
    /// �������� ��
    /// </summary>
    public float rune;

    /// <summary>
    /// �ɷ�ġ�� �ø��� �ʾҰų�, �ø� �ִ� ü��
    /// </summary>
    public float raiseHp;

    /// <summary>
    /// ���� ��, ���� ü��
    /// </summary>
    public float currentHp;

    /// <summary>
    /// �ɷ�ġ�� �ø��� �ʾҰų�, �ø� ���ݷ�
    /// </summary>
    public float raiseAtk;

    /// <summary>
    /// �ɷ�ġ�� �ø��� �ʾҰų�, �ø� ����
    /// </summary>
    public float raiseDef;

    public PlayerVolatilityData() { }

    /// <summary>
    /// SaveData�� ���Ǵ� �ֹ߼� ������
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
    /// NewDataReturn�� ���Ǵ� ������ (����� ������ �ʱⰪ���� ����)
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
