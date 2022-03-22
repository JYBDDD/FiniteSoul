using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    /// <summary>
    /// ��ü Ÿ��
    /// </summary>
    public enum CharacterType
    {
        Player,
        Monster,
        BossMonster,
        None,
    }

    /// <summary>
    /// ���� Ÿ��
    /// </summary>
    public enum AtkType
    {
        Normal,
        Projectile,
    }

    /// <summary>
    /// ����
    /// </summary>
    public enum State
    {
        Idle,
        Walk,
        Evasion,
        Running,
        Attack,
        Jump,
        Hurt,
        Die,

    }
}
