using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]    // NavMeshAgent를 안넣는 경우 방지
public class MonsterController : MoveableObject
{
    /// <summary>
    /// 해당 몬스터가 사용하는 데이터
    /// </summary>
    public UseMonsterData monsterData;

    // 모든 몬스터가 가지고 있는 NavMeshAgent
    protected NavMeshAgent agent;

    // Idle -> Walk 로 상태변경을 할때 사용되는 변수
    float walkStateMultiple = 0;

    // 몬스터의 생성 위치
    public Vector3 monsterStartPos = Vector3.zero;

    // 몬스터의 타겟
    GameObject target = null;

    /// <summary>
    /// 몬스터들만 사용하는 Awake()
    /// </summary>
    protected virtual void Awake()
    {
        InsertComponent();

        FSM.ChangeState(FSM.State, IdleState, false);
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

        // 상태머신에서 Update시켜야하는 값이라면 실행, 아니라면 실행중지
        FSM.UpdateMethod();

        State = FSM.State;
    }

    /// <summary>
    /// 몬스터 스텟 설정
    /// </summary>
    public void SetStat()
    {
        monsterData.currentHp = monsterData.maxHp;
    }

    public override void InsertComponent()
    {
        base.InsertComponent();
        agent ??= GetComponent<NavMeshAgent>();
        target = null;  // 타겟 비우기
    }

    protected virtual void IdleState()
    {
        float rand = UnityEngine.Random.Range(0f, -1f);
        StartCoroutine(ChangeRandA());

        // RandA값을 무작위로 변경시켜주는 코루틴
        IEnumerator ChangeRandA()
        {
            float duration = 4f;    // 변경 간격
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;

                // 애니메이터 이동 블랜드 파라미터값 설정

                if(anim.GetFloat("MoveZ") >= -0.1f && anim.GetFloat("MoveZ") <= 0.1f)
                {
                    anim.SetFloat("MoveZ", 0);
                }
                else 
                {
                    anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime * 3f));
                }

                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), rand, Time.deltaTime * 2f));

                yield return null;
            }

            // duration 이 경과했다면 실행
            if (time >= duration)
            {
                // Animator Parameter / RandA 값 (0f ~ -1f)사이 값으로 변경
                walkStateMultiple += Mathf.RoundToInt(duration);
                time = 0;

                yield return null;
            }

            // 해당 배수가 (변경 간격 * 2)로 나누었을 때, 나머지가 0이라면 Walk 상태로 변경
            if (Mathf.RoundToInt(walkStateMultiple) % (duration * 2) == 0)
            {
                FSM.ChangeState(Define.State.Walk, WalkState, false);
                yield break;
            }

            StartCoroutine(ChangeRandA());
            yield return null;
        }
    }

    protected virtual void WalkState()
    {
        StartCoroutine(WalkPathSet());

        IEnumerator WalkPathSet()
        {
            // 랜덤 목적지 설정
            Vector3 arrivalLocation = RandDestinationSet(monsterStartPos, 5f);

            // 목적지 설정
            bool TruePath = agent.SetDestination(arrivalLocation);
            if(!TruePath)
            {
                yield break;
            }

            while(TruePath)
            {
                // 애니메이터 이동 블랜드 파라미터값 설정
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // agent 가 이동중이고, 목적지에 도착했다면 실행
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 1f)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState, false);
                    yield break;
                }
                yield return null;
            }
        }

        // 랜덤 목적지 생성 내부 메소드
        Vector3 RandDestinationSet(Vector3 originPos, float dist)
        {
            // 처음 몬스터가 생성된 위치에서 일정 값 떨어진곳으로 경로 계산
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
            randDirection += originPos;

            NavMeshPath path = new NavMeshPath();
            bool TruePath = NavMesh.CalculatePath(transform.position, randDirection,NavMesh.AllAreas, path);

            // 올바른 경로가 반환되지 않았다면 Idle 상태로 전환
            if(TruePath == false)
            {
                FSM.ChangeState(Define.State.Idle, IdleState, false);
                return transform.position;
            }
            return randDirection;
        }
    }

    protected virtual void RunningState()
    {
        StartCoroutine(LockTarget());

        IEnumerator LockTarget()
        {



            while(true)
            {

                // 애니메이터 이동 블랜드 파라미터값 설정
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // 쫒는 타겟이 존재하지 않는다면, 상태 Idle 변경
                if (target == null)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState, false);
                }

                // 플레이어 목표 설정, 이동
                agent.SetDestination(target.transform.position);


                yield return null;
            }
        }
    }

    protected virtual void AttackState()
    {
        anim.SetBool("Attack", true);
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
        if (monsterData.characterType != Define.CharacterType.Monster)
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
                        Debug.Log("플레이어 찾음");
                        // 타겟 설정
                        target = hit.transform.gameObject;
                        // 상태 변경
                        FSM.ChangeState(Define.State.Running, RunningState, true);
                    }
                }
            }
        }

        // 경계 각도
        Vector3 BoundaryAngle(float angle)
        {
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
    }

    #region 몬스터 애니메이션 Event에 직접 들어가는 메소드

    public void MonsterAttackEnd()
    {
        anim.SetBool("Attack", false);
    }

    #endregion

}
