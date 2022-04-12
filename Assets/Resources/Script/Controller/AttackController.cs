using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 공격 및 데미지 처리시 사용할 클래스 / 해당 스크립트는 각각 Controller의 InsertComponent에서 AddComponent를 시켜주고 있음
/// </summary>
public class AttackController : MonoBehaviour
{
    // 임시로 값을 들고있을 데이터 (MoveableObject 초기화시 값을 넘겨준다)
    public StaticData staticData;

    private void OnTriggerEnter(Collider other)
    {
        // 값을 들고있는 데이터가 플레이어라면 실행 
        if (staticData.characterType == Define.CharacterType.Player)
        {
            // 몬스터와 닿았다면
            if(other.gameObject.CompareTag("Monster"))
            {
                var monsterC = other.gameObject.GetComponent<MonsterController>();
                var playerData = InGameManager.Instance.Player.playerData;
                // 몬스터 현재 체력 - (플레이어 공격력 - 몬스터 방어력)
                monsterC.monsterData.currentHp = monsterC.monsterData.currentHp - (playerData.atk - monsterC.monsterData.def);
                // 몬스터 상태 Hurt로 변경
                monsterC.FSM.ChangeState(Define.State.Hurt, monsterC.HurtState);

                // TargetMonsterUI 의 타깃 컨트롤러에 타겟값 설정
                TargetMonsterUI.targetMonsterC = monsterC;
            }
        }

        // 값을 들고있는 데이터가 몬스터라면 실행
        if (staticData.characterType == Define.CharacterType.Monster)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                var monsterC = InGameManager.Instance.Monsters.Where(_ => _.monsterData.index == staticData.index).SingleOrDefault();
                var monsterData = monsterC.monsterData;
                var playerC = InGameManager.Instance.Player;

                // 플레이어가 회피 상태라면 데미지를 받지 않음
                if (playerC.FSM.State == Define.State.Evasion)
                    return;

                // 플레이어 현재 체력 - (몬스터 공격력 - 플레이어 방어력)
                playerC.playerData.currentHp = playerC.playerData.currentHp - (monsterData.atk - playerC.playerData.def);
                // 플레이어 상태 Hurt로 변경
                playerC.FSM.ChangeState(Define.State.Hurt, playerC.HurtState);
            }
        }
    }
}
