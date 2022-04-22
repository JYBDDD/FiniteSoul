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

    // 몬스터의 체력이 0이 되었을시 호출되는 Bool타입 변수
    public bool deadMonster = false;

    // 몬스터의 생성 위치
    public Vector3 monsterStartPos = Vector3.zero;

    // 몬스터의 타겟
    protected GameObject target = null;

    [SerializeField]
    private List<Collider> atkCollider = new List<Collider>();

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
        if (monsterData != null)
        {
            MonsterViewAngle(monsterData.viewingAngle, monsterData.viewDistance);
        }

        // 상태머신에서 Update시켜야하는 값이라면 실행, 아니라면 실행중지
        FSM.UpdateMethod();
    }

    /// <summary>
    /// 몬스터 스텟 설정
    /// </summary>
    public void SetStat()
    {
        monsterData.currentHp = monsterData.maxHp;

        // NavMeshAgent 이동속도 = 몬스터데이터 이동속도 지정
        agent.speed = monsterData.moveSpeed;

        deadMonster = false;

        // 몬스터 파라미터값 초기값으로 재설정
        anim.SetFloat("MoveZ", 0);
        anim.SetFloat("RandA", 0);
        anim.SetBool("Attack", false);
        anim.SetBool("Die", false);
    }

    public override void InsertComponent()
    {
        base.InsertComponent();
        agent ??= GetComponent<NavMeshAgent>();
        target = null;  // 타겟 비우기
    }

    public override void AttackColliderSet()
    {
        base.AttackColliderSet();
        // 해당 콜라이더를 가지고 있는 오브젝트에 AttackController 스크립트를 추가
        for (int i = 0; i < atkCollider.Count; ++i)
        {
            AttackController attackController = atkCollider[i].gameObject.AddComponent<AttackController>();
            attackController.AttackControllerInit(monsterData, monsterData.atkType);
        }
    }

    protected virtual void IdleState()
    {
        float rand = UnityEngine.Random.Range(0f, -1f);
        float randDuration = UnityEngine.Random.Range(2, 6);
        StartCoroutine(ChangeRandA());

        // RandA값을 무작위로 변경시켜주는 코루틴
        IEnumerator ChangeRandA()
        {
            float duration = randDuration;    // 변경 간격
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;

                // 타겟을 찾았다면 RunningState() 로 전환
                if(target != null)
                {
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), rand, Time.deltaTime * 2f));



                yield return null;
            }

            // duration 이 경과했다면 실행
            if (time >= duration)
            {
                walkStateMultiple += Mathf.RoundToInt(duration);
                time = 0;

                yield return null;
            }

            // 값이 10을 초과했을시 상태 전환
            if (Mathf.RoundToInt(walkStateMultiple) > 10)
            {
                FSM.ChangeState(Define.State.Walk, WalkState);
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

            // 목적지 값이 false라면 실행
            if(!TruePath)
            {
                FSM.ChangeState(Define.State.Idle, IdleState);
                yield break;
            }

            while(FSM.State == Define.State.Walk)
            {
                // 애니메이터 이동 블랜드 파라미터값 설정
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 3f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // 타겟이 존재한다면 Running으로 상태 전환
                if (target != null)
                {
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                // agent 가 이동중이고, 목적지에 도착했다면 실행
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 1f)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState);
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
                FSM.ChangeState(Define.State.Idle, IdleState);
                return transform.position;
            }
            return randDirection;
        }
    }

    protected virtual void RunningState()
    {
        // 이동속도 증가
        agent.speed = (monsterData.moveSpeed * 2f);

        // 플레이어 목표 설정, 이동
        agent.SetDestination(target.transform.position);

        StartCoroutine(LockTarget());

        IEnumerator LockTarget()
        {
            while(true)
            {
                // 플레이어와 몬스터의 거리
                float distance = Vector3.Distance(target.transform.position, transform.position);

                // 플레이어가 공격범위 안으로 들어올경우 AttackState() 로 변경시킨다
                if (distance <= monsterData.atkRange)
                {
                    // 스피드 본래값으로 재설정
                    agent.speed = monsterData.moveSpeed;

                    // 공격값 랜덤으로 재설정
                    anim.SetFloat("RandA", UnityEngine.Random.Range(0f, -1f));

                    // 공격상태로 변경
                    FSM.ChangeState(Define.State.Attack, AttackState);
                    yield break;
                }

                // 애니메이터 이동 블랜드 파라미터값 설정
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // 거리가 공격 사거리보다 멀고, Hurt,Die 상태가 아니라면 실행
                if(distance > monsterData.atkRange && FSM.State != Define.State.Hurt && FSM.State != Define.State.Die)
                {
                    agent.SetDestination(target.transform.position);
                }

                yield return null;
            }
        }
    }

    protected virtual void AttackState()
    {
        StartCoroutine(StateChange());

        IEnumerator StateChange()
        {
            anim.SetBool("Attack", true);
            agent.SetDestination(transform.position);

            // 공격상태일때 실행
            while (true)
            {
                // 공격상태가 해제되었을때, RandA값(2개 공격 애니메이션) 재설정
                if (!anim.GetBool("Attack"))
                {
                    anim.SetFloat("RandA", 0);
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                // 타겟을 바라보도록 설정
                transform.LookAt(target.transform);

                // 몬스터의 체력이 0이 되었을때 실행
                if (monsterData.currentHp <= 0)
                {
                    FSM.ChangeState(Define.State.Die, DieState);
                    yield break;
                }

                yield return null;
            }
        }
    }

    /// <summary>
    /// Hurt 애니메이션은 각각의 상속받은 자식에서 적용중
    /// </summary>
    public virtual void HurtState()
    {
        // 현재 CreatureBase 한곳에서만 사용중
    }

    public virtual void DieState()
    {
        anim.SetBool("Die", true);
    }

    /// <summary>
    /// 몬스터의 시야각, 시야 거리
    /// </summary>
    /// <param name="viewAngle">시야 각도</param>
    /// <param name="viewDistance">시야 거리</param>
    protected void MonsterViewAngle(float viewAngle,float viewDistance)
    {
        // 타겟이 존재한다면 리턴
        if (target != null)
            return;

        // 몬스터가 아니라면 리턴시킨다
        if (monsterData.characterType != Define.CharacterType.Monster)
            return;

        // 몬스터 주변에 있는 타겟을 구한다
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, 1 << LayerMask.NameToLayer("Player"));

        for (int i =0; i < targets.Length; ++i)
        {
            // 타겟의 위치
            Vector3 targetPos = targets[i].transform.position;
            // 거리 (타겟과의 거리)
            Vector3 direction = (targetPos - transform.position).normalized;
            // 각도 (타겟과의 각도)
            float angle = Vector3.Angle(direction, transform.forward);

            // 플레이어의 시야 각도안으로 들어왓을시 실행
            if (angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, viewDistance))
                {
                    // 타깃의 레이어가 Player 일경우 실행
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        // 타겟 설정
                        target = hit.transform.gameObject;
                    }
                }
            }
        }
    }

    #region 몬스터 애니메이션 Event에 직접 들어가는 메소드

    public void MonsterAttackEnd()
    {
        anim.SetBool("Attack", false);
    }
    #endregion

}
