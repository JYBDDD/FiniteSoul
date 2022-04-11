using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warrok, Mutant 몬스터 이동을 구현하는 클래스(공통)
/// </summary>
public class CreatureBase : MonsterController
{
    int hitCount = 0;
    int originCount = 1;

    public override void Update()
    {
        base.Update();
    }

    protected override void IdleState()
    {
        base.IdleState();
    }

    protected override void WalkState()
    {
        base.WalkState();
    }

    protected override void RunningState()
    {
        base.RunningState();

    }

    protected override void AttackState()
    {
        base.AttackState();
    }

    public override void HurtState()
    {
        // 재호출 감지 카운트
        hitCount++;

        anim.SetBool("NotHit", false);          // HitTrigger 를 Hit{번호}로 교체함   확인 할것    TODO

        // Hit 애니메이션을 실행시키는 트리거 호출
        anim.SetTrigger($"Hit{hitCount}");

        // 움직일 수 있는 상태라면 도착지점을 현재 자신의 위치로 변경
        if (NotToMove == true)
        {
            agent.SetDestination(transform.position);
        }

        StartCoroutine(ChainHit());

        IEnumerator ChainHit()
        {
            // Hit1 이 재생중인 상태에서 피격시 Hit2 재생, Hit2 가 재생중인 상태에서 재생시 Hit3 재생
            while (true)
            {
                if (HitAnimationing("Hit2") == 0)
                {
                    // Hit3 애니메이션으로 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    anim.SetTrigger($"Hit{hitCount}");
                }
                else if (HitAnimationing("Hit1") == 0)
                {
                    // Hit2 애니메이션으로 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    anim.SetTrigger($"Hit{hitCount}");
                }

                yield return null;
            }
        }

        // Hit 애니메이션이 실행중이라면 0, 아니라면 1
        int HitAnimationing(string animationName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                // 애니메이션이 끝나지 않은상태에서 재호출시 실행
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && hitCount > originCount)
                {
                    originCount = hitCount;
                    return 0;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    // 시간안에 피격당하지 않았을시, 블랜드 트리로 다시 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    FSM.ChangeState(Define.State.Running, RunningState);
                    anim.SetBool("NotHit",true);
                    hitCount = 0;
                    originCount = 1;
                    return 2;
                }
            }

            return 1;
        }
    }

    protected override void DieState()
    {
        base.DieState();
    }

}
