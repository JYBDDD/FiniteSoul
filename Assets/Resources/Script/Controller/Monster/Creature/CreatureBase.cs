using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warrok, Mutant ���� �̵��� �����ϴ� Ŭ����(����)
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
        // ��ȣ�� ���� ī��Ʈ
        hitCount++;

        anim.SetBool("NotHit", false);          // HitTrigger �� Hit{��ȣ}�� ��ü��   Ȯ�� �Ұ�    TODO

        // Hit �ִϸ��̼��� �����Ű�� Ʈ���� ȣ��
        anim.SetTrigger($"Hit{hitCount}");

        // ������ �� �ִ� ���¶�� ���������� ���� �ڽ��� ��ġ�� ����
        if (NotToMove == true)
        {
            agent.SetDestination(transform.position);
        }

        StartCoroutine(ChainHit());

        IEnumerator ChainHit()
        {
            // Hit1 �� ������� ���¿��� �ǰݽ� Hit2 ���, Hit2 �� ������� ���¿��� ����� Hit3 ���
            while (true)
            {
                if (HitAnimationing("Hit2") == 0)
                {
                    // Hit3 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                    anim.SetTrigger($"Hit{hitCount}");
                }
                else if (HitAnimationing("Hit1") == 0)
                {
                    // Hit2 �ִϸ��̼����� �ѱ��
                    target ??= InGameManager.Instance.Player.gameObject;
                    anim.SetTrigger($"Hit{hitCount}");
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
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f && hitCount > originCount)
                {
                    originCount = hitCount;
                    return 0;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    // �ð��ȿ� �ǰݴ����� �ʾ�����, ���� Ʈ���� �ٽ� �ѱ��
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
