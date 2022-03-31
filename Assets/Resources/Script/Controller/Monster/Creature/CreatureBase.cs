using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warrok, Mutant ���� �̵��� �����ϴ� Ŭ����(����)
/// </summary>
public class CreatureBase : MonsterController
{
    public override void Update()
    {
        base.Update();
    }

    protected override void IdleState()
    {
        // ���� ������ �ְ� anim. �� �Ķ���� RandA ���� Random���� �����Ѵ�
        //    Idle -> RandA �� 0
        //    TurnRight90 -> RandA �� - 1


    }

    protected override void WalkState()
    {

    }

    protected override void RunningState()
    {
        // Ÿ���� ���� �����Ÿ�

    }

    protected override void AttackState()
    {

    }

    protected override void HurtState()
    {
        // Hurt�� �ڷ� �и��°����� ó���� ����
        // Idle���� �����߿��� �и��°����� TODO
    }

    protected override void DieState()
    {

    }

    /*Mutant �ִϸ����� ���� TODO

    RandA �� -0.5 ~ -1 -> Attack1
    RandA �� -0.01 ~ -0.49 -> Attack2

    Mutant �� ������ �Ķ���� �Ұ����� üũ���� �ʰ�
    ���� �����Ÿ��ȿ� �÷��̾ �����Ͽ������ �����ϴ°�����..

    ���Ϳ��� �þ߰� -> viewingAngle
              �þ߰Ÿ� -> viewingDistance 

    ������ �ؼ� �þ߿� ����� ��� Run �ִϸ��̼� ����� �i�ư��°����� ������ ��
    */

}
