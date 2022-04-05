using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Warrok, Mutant 몬스터 이동을 구현하는 클래스(공통)
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
