using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Warrok, Mutant ���� �̵��� �����ϴ� Ŭ����(����)
/// </summary>
public class CreatureBase : MonsterController
{
    // ���� HitCount
    int startCount = 0;

    // ������ originCount;
    int originCount = 0;

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
        // ���� ī��Ʈ ���
        ++startCount;

        // �ִϸ������� HitCount int�� �ҷ���
        anim.SetInteger("HitCount",startCount);

        // ������ -> �ڽ��� ��ġ (��� �̵� �Ұ��ϴ°�ó�� �����Ұ�)
        agent.SetDestination(transform.position);

        StartCoroutine(ChainHit());

        IEnumerator ChainHit()
        {
            // Hit1 �� ������� ���¿��� �ǰݽ� Hit2 ���, Hit2 �� ������� ���¿��� ����� Hit3 ���
            while (true)
            {
                if (EndAnim("Hit3"))
                {
                    EndCall();
                }
                if (HitAnimationing("Hit2") == 0 && startCount > 2)
                {
                    // Hit3 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                }
                if (HitAnimationing("Hit1") == 0 && startCount > 1)
                {
                    // Hit2 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
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
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && startCount > originCount)
                {
                    originCount = startCount;
                    anim.SetInteger("HitCount", startCount);
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
            startCount = 0;
        }
    }

    public override void DieState()
    {
        base.DieState();
    }

}
