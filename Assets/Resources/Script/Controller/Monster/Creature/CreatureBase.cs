using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Warrok, Mutant ���� �̵��� �����ϴ� Ŭ����(����)
/// </summary>
public class CreatureBase : MonsterController
{
    int hitCall = 0;
    int originCall = 1;

    // ���� HitCount
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
        // ��ȣ�� ���� ī��Ʈ
        hitCall++;


        anim.SetInteger("HitCount",startCount);


        agent.SetDestination(transform.position);

        StartCoroutine(ChainHit());

        IEnumerator ChainHit()
        {
            // Hit1 �� ������� ���¿��� �ǰݽ� Hit2 ���, Hit2 �� ������� ���¿��� ����� Hit3 ���
            while (true)
            {
                if(EndAnim("Hit3"))
                {
                    EndCall();
                }
                if (HitAnimationing("Hit2") == 0)
                {
                    // Hit3 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                    transform.rotation = Quaternion.LookRotation(target.transform.position);

                }
                if (HitAnimationing("Hit1") == 0)
                {
                    // Hit2 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                    transform.rotation = Quaternion.LookRotation(target.transform.position);
                    anim.SetTrigger("NotHitTrigger");
                }

                yield return null;
            }
        }

        // Hit �ִϸ��̼��� �������̶�� 0, �ƴ϶�� 1
        int HitAnimationing(string animationName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                // �ִϸ��̼��� ������ �������¿��� ��ȣ��� ����
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && hitCall > originCall)
                {
                    originCall = hitCall;
                    anim.SetInteger("HitCount", ++startCount);
                    agent.SetDestination(transform.position);
                    return 0;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    // �ð��ȿ� �ǰݴ����� �ʾ�����, ���� Ʈ���� �ٽ� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                    FSM.ChangeState(Define.State.Running, RunningState);
                    EndCall();
                    return 2;
                }
            }

            return 1;
        }

        // ������ �ִϸ��̼��� �������϶� ȣ��
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

        // ����� ȣ��
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
