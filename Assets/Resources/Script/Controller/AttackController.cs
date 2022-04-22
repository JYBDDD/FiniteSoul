using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 공격 및 데미지 처리시 사용할 클래스 / 해당 스크립트는 각각 Controller의 InsertComponent에서 AddComponent를 시켜주고 있음
/// </summary>
public class AttackController : MonoBehaviour
{
    /// <summary>
    /// 임시로 값을 들고있을 데이터 (MoveableObject 초기화시 값을 넘겨준다)
    /// </summary>
    public StaticData staticData;
    public Define.AtkType atkType;

    /// <summary>
    /// 기본값 True    -> 발사체일 경우 땅에 맞았을시 값을 체크하지 않는 용도
    /// </summary>
    public bool checkBool = true;

    /// <summary>
    /// 어택 컨트롤러 데이터값 초기화
    /// </summary>
    /// <param name="staticData"></param>
    /// <param name="atkType"></param>
    public void AttackControllerInit(StaticData staticData,Define.AtkType atkType)
    {
        this.staticData = staticData;
        this.atkType = atkType;
        checkBool = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 값을 들고있는 데이터가 플레이어라면 실행 
        if (staticData.characterType == Define.CharacterType.Player)
        {
            // 몬스터와 닿았다면
            if(other.gameObject.CompareTag("Monster") && checkBool == true)
            {
                

                var monsterC = other.gameObject.GetComponent<MonsterController>();
                var playerAtk = InGameManager.Instance.Player.playerData.atk;
                var damage = playerAtk - monsterC.monsterData.def;

                // 근거리라면 실행
                if (atkType == Define.AtkType.Normal && monsterC.monsterData.currentHp > 0)
                {
                    // 충돌 위치값
                    var closetPos = other.bounds.ClosestPoint(transform.position);

                    // 플레이어 SwordAttackEffect 출력
                    ResourceUtil.ParticleInit(Define.ParticleEffectPath.PlayerParticle.swordAttack, Define.CharacterType.Particle, this,
                        closetPos, gameObject.transform.parent.rotation);

                }

                // 데미지가 0보다 작거나 같다면 데미지를 1로 고정
                if (damage <= 0)
                {
                    damage = 1;
                }
                // 몬스터 현재 체력 - (플레이어 공격력 - 몬스터 방어력)
                monsterC.monsterData.currentHp -= damage;

                // 몬스터 상태 Hurt로 변경
                monsterC.FSM.ChangeState(Define.State.Hurt, monsterC.HurtState);

                // TargetMonsterUI 의 타깃 컨트롤러에 타겟값 설정
                TargetMonsterUI.targetMonsterC = monsterC;

                // 타깃이 된 몬스터 체력(0보다 클때),이름 UI 즉시 활성화
                if (monsterC.monsterData.currentHp > 0)
                {
                    TargetMonsterUI.TargetUIState = Define.UIDraw.Activation;
                }
            }

            // 원거리라면 실행 / 몬스터에 맞지 않았더라도 데미지처리 불가하게 변경
            if (atkType == Define.AtkType.Projectile)
            {
                checkBool = false;
            }
        }

        // 값을 들고있는 데이터가 몬스터라면 실행
        if (staticData.characterType == Define.CharacterType.Monster)
        {
            if(other.gameObject.CompareTag("Player") && InGameManager.Instance.Player.playerData.currentHp > 0)
            {
                // 충돌 위치값
                var closetPos = other.bounds.ClosestPoint(transform.position);

                // 몬스터 AttackEffect 출력
                ResourceUtil.ParticleInit(Define.ParticleEffectPath.MonsterParticle.monsterAttack, Define.CharacterType.Particle, this,
                    closetPos, Quaternion.identity);

                // Where 의 SingleOrDefault으로 값을 추출하면 두개이상의 값을 찾아 오류가 발생할수 있기에 
                // FirstOrDefault 로 첫번째 값만 가져오도록 설정
                var monsterC = InGameManager.Instance.Monsters.FirstOrDefault(_ => _.monsterData.index == staticData.index);
                var monsterAtk = monsterC.monsterData.atk;
                var playerC = InGameManager.Instance.Player;

                // 플레이어가 회피 상태라면 데미지를 받지 않음
                if (playerC.FSM.State == Define.State.Evasion)
                    return;

                // 플레이어 현재 체력 - (몬스터 공격력 - 플레이어 방어력)
                var damage = monsterAtk - playerC.playerData.def;
                playerC.playerData.currentHp -= damage;
                // 플레이어 상태 Hurt로 변경
                playerC.FSM.ChangeState(Define.State.Hurt, playerC.HurtState);

                // 플레이어의 체력이 0보다 낮다면 Diying UI Window를 출력시킨다
                if(playerC.playerData.currentHp <= 0)
                {
                    DyingWindowUI.DyingUIState = Define.UIDraw.SlowlyActivation;
                    UIManager.Instance.SwitchWindowOption(ref DyingWindowUI.DyingUIState, ref DyingWindowUI.dyingUIOriginState,
                        DyingWindowUI.dyingCanvas,DyingWindowUI.DyingWindowStart);
                    return;
                }
            }
        }
    }
}
