using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MoveableObject
{
    /// <summary>
    /// 해당 몬스터가 사용하는 데이터
    /// </summary>
    UseMonsterData monsterData;

    /// <summary>
    /// 몬스터들만 사용하는 Awake()
    /// </summary>
    protected virtual void Awake()
    {
        InsertComponent();
    }

    /// <summary>
    /// (상위 부모) Update() -> FSM (몬스터 용)
    /// </summary>
    public virtual void Update()
    {

        switch (State)
        {
            case Define.State.Idle:
                IdleState();
                break;
            case Define.State.Walk:
                WalkState();
                break;
            case Define.State.Running:
                RunningState();
                break;
            case Define.State.Attack:
                AttackState();
                break;
            case Define.State.Hurt:
                HurtState();
                break;
            case Define.State.Die:
                DieState();
                break;
        }
    }

    private void IdleState()
    {
        // 일정 간격을 주고 anim. 의 파라미터 RandA 값을 Random으로 조정한다
        //    Idle -> RandA 가 1
        //    TurnRight90 -> RandA 가 - 1

    }

    private void WalkState()
    {
        
    }

    private void RunningState()
    {
        // 타깃이 일정 사정거리

    }

    private void AttackState()
    {
        
    }

    private void HurtState()
    {
        // Hurt는 뒤로 밀리는것으로 처리할 예정
        // Idle상태 유지중에서 밀리는것으로 TODO
    }

    private void DieState()
    {
        
    }

    /*Mutant 애니메이터 셋팅 TODO

    RandA 가 양수일 경우 -> Attack1
    RandA 가 음수일 경우 -> Attack2

    Mutant 의 공격은 파라미터 불값으로 체크하지 않고
    공격 사정거리안에 플레이어가 접촉하였을경우 실행하는것으로..

    몬스터에게 시야각 -> viewingAngle
              시야거리 -> viewingDistance 

    설정을 해서 시야에 닿았을 경우 Run 애니메이션 출력후 쫒아가는것으로 설정할 것
    */

 
}
