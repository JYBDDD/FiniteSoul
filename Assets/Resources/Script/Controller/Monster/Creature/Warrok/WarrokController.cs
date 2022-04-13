using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 몬스터(Warrok) 단일 클래스
/// </summary>
public class WarrokController : CreatureBase
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

    public override void HurtState()
    {
        base.HurtState();
    }

    protected override void DieState()
    {
        base.DieState();
    }
}
