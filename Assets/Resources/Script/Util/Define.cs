using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    /// <summary>
    /// 객체 타입
    /// </summary>
    public enum CharacterType
    {
        Player,
        Monster,
        BossMonster,
        None,
    }

    /// <summary>
    /// 공격 타입
    /// </summary>
    public enum AtkType
    {
        Normal,
        Projectile,
    }

    /// <summary>
    /// 상태
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
