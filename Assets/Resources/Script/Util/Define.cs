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
        Projectile,
        Particle,
        Rune,
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
    /// 상호작용 대상
    /// </summary>
    public enum InteractionTarget
    {
        /// <summary>
        /// 모닥불 -> 데이터 저장 및 스테이터스 설정
        /// </summary>
        Save,
        /// <summary>
        /// 상점 -> 아이템 구매 및 판매
        /// </summary>
        Shop,
        /// <summary>
        /// 룬 -> 플레이어 사망시 드랍하는 룬
        /// </summary>
        Rune,
        /// <summary>
        /// 아무 상호작용도 하지않는 상태
        /// </summary>
        None,
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

    /// <summary>
    /// 파티클 이펙트 위치값을 모아둔 클래스
    /// </summary>
    public class ParticleEffectPath
    {
        public class PlayerParticle
        {
            /// <summary>
            /// 발사체(화살) 공격 이펙트  (Resources.Load)
            /// </summary>
            public const string arrowShot = "Prefabs/ParticleEffect/ArrowAttackEffect";

            /// <summary>
            /// (검) 공격 이펙트  (Resources.Load)
            /// </summary>
            public const string swordAttack = "Prefabs/ParticleEffect/SwordAttackEffect";

            /// <summary>
            /// 룬 획득 이펙트    (Resources.Load)
            /// </summary>
            public const string runeEffect = "Prefabs/ParticleEffect/Rune/GetRune";

        }

        public class MonsterParticle
        {
            /// <summary>
            /// 몬스터 공격 이펙트 (Resources.Load)
            /// </summary>
            public const string monsterAttack = "Prefabs/ParticleEffect/MonsterAttackEffect";
        }
    }


    public class ChoiseCharacterPos
    {
        /// <summary>
        /// 팔라딘 선택 위치값
        /// </summary>
        public static Vector3 paladinPos = new Vector3(4f, 2.5f, 3f);
        /// <summary>
        /// 아처 선택 위치값
        /// </summary>
        public static Vector3 archerPos = new Vector3(-4f, 2.5f, 3f);
    }

    public class CameraPath
    {
        /// <summary>
        /// 메인 카메라 Path (Resources.Load 전용)
        /// </summary>
        public const string mainCamPath = "Prefabs/Camera/MainCamera";

        /// <summary>
        /// 플레이어를 바라보는 VirtualCamera Path (Resources.Load 전용)
        /// </summary>
        public const string playerVirtualCamPath = "Prefabs/Camera/LookPlayerVirtualCam";
    }


    public class ProjectilePath
    {
        /// <summary>
        /// 발사체 경로 : 화살 (Resources.Load 전용)
        /// </summary>
        public const string arrowPath = "Prefabs/Projectile/Arrow";
    }

    public class DropRunePath
    {
        /// <summary>
        /// 드랍할 프리팹 룬 경로 (Resources.Load)
        /// </summary>
        public const string dropRunePath = "Prefabs/ParticleEffect/Rune/Rune";
    }

    public class SlotUIPath
    {
        /// <summary>
        /// 인벤토리 슬롯 경로  (Resources.Load)
        /// </summary>
        public const string invenSlotPath = "Prefabs/UI/Slot/InvenSlot";

        /// <summary>
        /// 상점 슬롯 경로 (Resources.Load)
        /// </summary>
        public const string shopSlotPath = "Prefabs/UI/Slot/ShopSlot";
    }

}
