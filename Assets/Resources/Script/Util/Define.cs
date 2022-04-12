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

    

    public class ItemMixEnum
    {
        /// <summary>
        /// 아이템 타입
        /// </summary>
        public enum ItemType
        {
            /// <summary>
            /// 장비
            /// </summary>
            Equipment,

            /// <summary>
            /// 소비
            /// </summary>
            Consumption,

            /// <summary>
            /// 기타
            /// </summary>
            Etc,
        }

        /// <summary>
        /// 아이템 착용 타입 (한손, 두손)
        /// </summary>
        public enum ItemHandedType
        {
            /// <summary>
            /// 한손
            /// </summary>
            One,

            /// <summary>
            /// 두손
            /// </summary>
            Two,

            None,
        }
    }

    /// <summary>
    /// UI 활성/비활성 상태
    /// </summary>
    public enum UIDraw
    {
        /// <summary>
        /// 즉시 활성
        /// </summary>
        Activation,

        /// <summary>
        /// 서서히 활성
        /// </summary>
        SlowlyActivation,

        /// <summary>
        /// 즉시 비활성
        /// </summary>
        Inactive,

        /// <summary>
        /// 서서히 비활성
        /// </summary>
        SlowlyInactive,
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
