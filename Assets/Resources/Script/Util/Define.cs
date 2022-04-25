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
        Projectile,
        Particle,
        Rune,
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
    /// ��ȣ�ۿ� ���
    /// </summary>
    public enum InteractionTarget
    {
        /// <summary>
        /// ��ں� -> ������ ���� �� �������ͽ� ����
        /// </summary>
        Save,
        /// <summary>
        /// ���� -> ������ ���� �� �Ǹ�
        /// </summary>
        Shop,
        /// <summary>
        /// �� -> �÷��̾� ����� ����ϴ� ��
        /// </summary>
        Rune,
        /// <summary>
        /// �ƹ� ��ȣ�ۿ뵵 �����ʴ� ����
        /// </summary>
        None,
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

    /// <summary>
    /// ��ƼŬ ����Ʈ ��ġ���� ��Ƶ� Ŭ����
    /// </summary>
    public class ParticleEffectPath
    {
        public class PlayerParticle
        {
            /// <summary>
            /// �߻�ü(ȭ��) ���� ����Ʈ  (Resources.Load)
            /// </summary>
            public const string arrowShot = "Prefabs/ParticleEffect/ArrowAttackEffect";

            /// <summary>
            /// (��) ���� ����Ʈ  (Resources.Load)
            /// </summary>
            public const string swordAttack = "Prefabs/ParticleEffect/SwordAttackEffect";

            /// <summary>
            /// �� ȹ�� ����Ʈ    (Resources.Load)
            /// </summary>
            public const string runeEffect = "Prefabs/ParticleEffect/Rune/GetRune";

        }

        public class MonsterParticle
        {
            /// <summary>
            /// ���� ���� ����Ʈ (Resources.Load)
            /// </summary>
            public const string monsterAttack = "Prefabs/ParticleEffect/MonsterAttackEffect";
        }
    }


    public class ChoiseCharacterPos
    {
        /// <summary>
        /// �ȶ�� ���� ��ġ��
        /// </summary>
        public static Vector3 paladinPos = new Vector3(4f, 2.5f, 3f);
        /// <summary>
        /// ��ó ���� ��ġ��
        /// </summary>
        public static Vector3 archerPos = new Vector3(-4f, 2.5f, 3f);
    }

    public class CameraPath
    {
        /// <summary>
        /// ���� ī�޶� Path (Resources.Load ����)
        /// </summary>
        public const string mainCamPath = "Prefabs/Camera/MainCamera";

        /// <summary>
        /// �÷��̾ �ٶ󺸴� VirtualCamera Path (Resources.Load ����)
        /// </summary>
        public const string playerVirtualCamPath = "Prefabs/Camera/LookPlayerVirtualCam";
    }


    public class ProjectilePath
    {
        /// <summary>
        /// �߻�ü ��� : ȭ�� (Resources.Load ����)
        /// </summary>
        public const string arrowPath = "Prefabs/Projectile/Arrow";
    }

    public class DropRunePath
    {
        /// <summary>
        /// ����� ������ �� ��� (Resources.Load)
        /// </summary>
        public const string dropRunePath = "Prefabs/ParticleEffect/Rune/Rune";
    }

    public class SlotUIPath
    {
        /// <summary>
        /// �κ��丮 ���� ���  (Resources.Load)
        /// </summary>
        public const string invenSlotPath = "Prefabs/UI/Slot/InvenSlot";

        /// <summary>
        /// ���� ���� ��� (Resources.Load)
        /// </summary>
        public const string shopSlotPath = "Prefabs/UI/Slot/ShopSlot";
    }

}
