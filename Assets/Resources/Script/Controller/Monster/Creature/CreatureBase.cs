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
        // 일정 간격을 주고 anim. 의 파라미터 RandA 값을 Random으로 조정한다
        //    Idle -> RandA 가 0
        //    TurnRight90 -> RandA 가 - 1


    }

    protected override void WalkState()
    {

    }

    protected override void RunningState()
    {
        // 타깃이 일정 사정거리

    }

    protected override void AttackState()
    {

    }

    protected override void HurtState()
    {
        // Hurt는 뒤로 밀리는것으로 처리할 예정
        // Idle상태 유지중에서 밀리는것으로 TODO
    }

    protected override void DieState()
    {

    }

    /*Mutant 애니메이터 셋팅 TODO

    RandA 가 -0.5 ~ -1 -> Attack1
    RandA 가 -0.01 ~ -0.49 -> Attack2

    Mutant 의 공격은 파라미터 불값으로 체크하지 않고
    공격 사정거리안에 플레이어가 접촉하였을경우 실행하는것으로..

    몬스터에게 시야각 -> viewingAngle
              시야거리 -> viewingDistance 

    설정을 해서 시야에 닿았을 경우 Run 애니메이션 출력후 쫒아가는것으로 설정할 것
    */

}
