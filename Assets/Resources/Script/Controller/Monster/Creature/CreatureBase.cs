using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Warrok, Mutant 몬스터 이동을 구현하는 클래스(공통)
/// </summary>
public class CreatureBase : MonsterController
{
    int hitCall = 0;
    int originCall = 1;

    // 시작 HitCount
    int startCount = 1;

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
        hitCall++;


        anim.SetInteger("HitCount",startCount);


        agent.SetDestination(transform.position);

        StartCoroutine(ChainHit());

        IEnumerator ChainHit()
        {
            // Hit1 이 재생중인 상태에서 피격시 Hit2 재생, Hit2 가 재생중인 상태에서 재생시 Hit3 재생
            while (true)
            {
                if(EndAnim("Hit3"))
                {
                    EndCall();
                }
                if (HitAnimationing("Hit2") == 0)
                {
                    // Hit3 애니메이션으로 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    transform.rotation = Quaternion.LookRotation(target.transform.position);

                }
                if (HitAnimationing("Hit1") == 0)
                {
                    // Hit2 애니메이션으로 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    transform.rotation = Quaternion.LookRotation(target.transform.position);
                    anim.SetTrigger("NotHitTrigger");
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
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && hitCall > originCall)
                {
                    originCall = hitCall;
                    anim.SetInteger("HitCount", ++startCount);
                    agent.SetDestination(transform.position);
                    return 0;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    // 시간안에 피격당하지 않았을시, 블랜드 트리로 다시 넘긴다
                    target ??= InGameManager.Instance.Player.gameObject;
                    FSM.ChangeState(Define.State.Running, RunningState);
                    EndCall();
                    return 2;
                }
            }

            return 1;
        }

        // 마지막 애니메이션이 실행중일때 호출
        bool EndAnim(string animName)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    return true;
                }
            }
            return false;
        }

        // 종료시 호출
        void EndCall()
        {
            anim.SetInteger("HitCount", 0);
            anim.SetTrigger("NotHitTrigger");
            hitCall = 0;
            originCall = 1;
            startCount = 1;
        }
    }

    protected override void DieState()
    {
        base.DieState();
    }

}
