using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MoveableObject
{
    /// <summary>
    /// 해당 몬스터가 사용하는 데이터
    /// </summary>
    public UseMonsterData monsterData;

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
        // 몬스터의 데이터(시야각, 시야 거리)가 존재한다면 실행
        if (monsterData?.viewingAngle != null && monsterData?.viewDistance != null)
        {
            MonsterViewAngle(monsterData.viewingAngle, monsterData.viewDistance);
        }


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

    /// <summary>
    /// 몬스터 스텟 설정
    /// </summary>
    public void SetStat()
    {
        monsterData.currentHp = monsterData.maxHp;
    }

    protected virtual void IdleState()
    {

    }

    protected virtual void WalkState()
    {
        
    }

    protected virtual void RunningState()
    {

    }

    protected virtual void AttackState()
    {
        
    }

    protected virtual void HurtState()
    {
        
    }

    protected virtual void DieState()
    {
        
    }

    /// <summary>
    /// 몬스터의 시야각, 시야 거리
    /// </summary>
    /// <param name="viewAngle">시야 각도</param>
    /// <param name="viewDistance">시야 거리</param>
    protected void MonsterViewAngle(float viewAngle,float viewDistance)
    {
        // 몬스터가 아니라면 리턴시킨다
        if (monsterData.type != Define.CharacterType.Monster)
            return;

        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary, Color.red);

        // 타겟을 구한다
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, LayerMask.NameToLayer("Player"));

        // 타겟이 범위에 들어오지 않았다면 리턴
        if (targets.Length == 0)
            return;

        for(int i =0; i < targets.Length; ++i)
        {
            // 타겟의 위치
            Vector3 targetPos = targets[i].transform.position;
            // 거리 (타겟과의 거리)
            Vector3 direction = (targetPos - transform.position).normalized;
            // 각도 (타겟과의 각도)
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + Vector3.up, direction,out hit,viewDistance))
                {
                    // 타깃의 레이어가 Player 일경우 실행
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        // 플레이어를  향해 달려간다 TODO
                    }
                }
            }
        }

        // 경계 각도
        Vector3 BoundaryAngle(float _angle)
        {
            _angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
        }
    }


    
}
