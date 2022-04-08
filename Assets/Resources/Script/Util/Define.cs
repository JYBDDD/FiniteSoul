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
        Hurt,
        Die,

    }

    public class HitPath
    {
        /// <summary>
        /// Mutant Hit 애니메이션 Path   (Resources.LoadAll 전용)
        /// </summary>
        public const string mutantPath = "Art/Anim/Animation/Monster/Mutant/Hit";
        /// <summary>
        /// Warrok Hit 애니메이션 Path   (Resources.LoadAll 전용)
        /// </summary>
        public const string warrokPath = "Art/Anim/Animation/Monster/Warrok/Hit";
    }
}
