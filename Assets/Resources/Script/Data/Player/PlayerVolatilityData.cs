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
