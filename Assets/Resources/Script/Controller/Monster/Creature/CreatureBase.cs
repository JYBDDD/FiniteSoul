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

    protected override void HurtState()
    {
        base.HurtState();
    }

    protected override void DieState()
    {
        base.DieState();
    }


}
