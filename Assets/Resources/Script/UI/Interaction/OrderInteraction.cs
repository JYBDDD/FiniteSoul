using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 들고있으며 Interaction을 들고있는 오브젝트와 상호작용의 메인이 되는 클래스
/// </summary>
public class OrderInteraction : MonoBehaviour
{
    /// <summary>
    /// 플레이어가 상호작용을 시도하려하는지 확인하는 Bool 타입 변수
    /// </summary>
    public static bool playerInteractionTrue;

    private void Start()
    {
        InputManager.Instance.KeyAction += OnInteraction;
    }

    /// <summary>
    /// 플레이어가 상호작용 On/Off를 하려할경우 실행되는 메서드
    /// </summary>
    private void OnInteraction()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            playerInteractionTrue = true;
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            playerInteractionTrue = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Interaction")
        {
            // 모닥불과 닿았고 상호작용하려 한다면, 데이터 저장 및 스테이터스창 활성화
            if (other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Save && playerInteractionTrue)
            {
                // Esc 키를 누를시 스테이터스창 탈출
                if (Input.GetKey(KeyCode.Escape))
                {
                    // 이동 가능 지정
                    InGameManager.Instance.Player.NotToMove = true;

                    // 플레이어 상호작용 해제
                    playerInteractionTrue = false;

                    // 스테이터스창 천천히 비활성화
                    StatEquipWindowUI.StatEquipState = Define.UIDraw.SlowlyInactive;

                    // 데이터 저장
                    ResourceUtil.SaveData(new PlayerVolatilityData(InGameManager.Instance.Player.playerData, transform.position, StageManager.stageData));

                    return;
                }

                // 이동 불가 지정
                InGameManager.Instance.Player.NotToMove = false;

                // 스테이터스창 활성화
                StatEquipWindowUI.StatEquipState = Define.UIDraw.SlowlyActivation;
            }

            // 룬과 상호작용하려 한다면 해당 룬 회수
            if(other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Rune && playerInteractionTrue)
            {
                // 플레이어에게 룬을 넘김
                Rune.DropRuneHandOver();

                // 룬을 풀링매니저로 넘김
                ObjectPoolManager.Instance.GetPush(other.gameObject);

                // 룬 획득 이펙트 활성화
                var runeEffect = ObjectPoolManager.Instance.GetPool<ParticleChild>(Define.ParticleEffectPath.PlayerParticle.runeEffect, Define.CharacterType.Particle);
                runeEffect.transform.position = other.gameObject.transform.position + Vector3.up;
            }

            // 상점과 상호작용하려 한다면 실행
            if(other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Shop && playerInteractionTrue)
            {
                // Esc 키를 누를시 상점창 탈출
                if (Input.GetKey(KeyCode.Escape))
                {
                    // 이동 가능 지정
                    InGameManager.Instance.Player.NotToMove = true;

                    // 플레이어 상호작용 해제
                    playerInteractionTrue = false;

                    // 상점창 천천히 비활성화
                    ShopInvenWindowUI.ShopInvenUIState = Define.UIDraw.SlowlyInactive;

                    return;
                }

                // 이동 불가 지정
                InGameManager.Instance.Player.NotToMove = false;

                // 상점 + 인벤토리 창 활성화
                ShopInvenWindowUI.ShopInvenUIState = Define.UIDraw.SlowlyActivation;
            }

        }

    }
}
