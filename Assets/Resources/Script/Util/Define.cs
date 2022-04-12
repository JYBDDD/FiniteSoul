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
        Hurt,
        Die,

    }

    

    public class ItemMixEnum
    {
        /// <summary>
        /// ������ Ÿ��
        /// </summary>
        public enum ItemType
        {
            /// <summary>
            /// ���
            /// </summary>
            Equipment,

            /// <summary>
            /// �Һ�
            /// </summary>
            Consumption,

            /// <summary>
            /// ��Ÿ
            /// </summary>
            Etc,
        }

        /// <summary>
        /// ������ ���� Ÿ�� (�Ѽ�, �μ�)
        /// </summary>
        public enum ItemHandedType
        {
            /// <summary>
            /// �Ѽ�
            /// </summary>
            One,

            /// <summary>
            /// �μ�
            /// </summary>
            Two,

            None,
        }
    }

    /// <summary>
    /// UI Ȱ��/��Ȱ�� ����
    /// </summary>
    public enum UIDraw
    {
        /// <summary>
        /// ��� Ȱ��
        /// </summary>
        Activation,

        /// <summary>
        /// ������ Ȱ��
        /// </summary>
        SlowlyActivation,

        /// <summary>
        /// ��� ��Ȱ��
        /// </summary>
        Inactive,

        /// <summary>
        /// ������ ��Ȱ��
        /// </summary>
        SlowlyInactive,
    }

    public class HitPath
    {
        /// <summary>
        /// Mutant Hit �ִϸ��̼� Path   (Resources.LoadAll ����)
        /// </summary>
        public const string mutantPath = "Art/Anim/Animation/Monster/Mutant/Hit";
        /// <summary>
        /// Warrok Hit �ִϸ��̼� Path   (Resources.LoadAll ����)
        /// </summary>
        public const string warrokPath = "Art/Anim/Animation/Monster/Warrok/Hit";
    }
}
