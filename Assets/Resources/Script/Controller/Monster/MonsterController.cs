using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]    // NavMeshAgent�� �ȳִ� ��� ����
public class MonsterController : MoveableObject
{
    /// <summary>
    /// �ش� ���Ͱ� ����ϴ� ������
    /// </summary>
    public UseMonsterData monsterData;

    // ��� ���Ͱ� ������ �ִ� NavMeshAgent
    protected NavMeshAgent agent;

    // Idle -> Walk �� ���º����� �Ҷ� ���Ǵ� ����
    float walkStateMultiple = 0;

    // ������ ü���� 0�� �Ǿ����� ȣ��Ǵ� BoolŸ�� ����
    public bool deadMonster = false;

    // ������ ���� ��ġ
    public Vector3 monsterStartPos = Vector3.zero;

    // ������ Ÿ��
    protected GameObject target = null;

    [SerializeField]
    private List<Collider> atkCollider = new List<Collider>();

    /// <summary>
    /// ���͵鸸 ����ϴ� Awake()
    /// </summary>
    protected virtual void Awake()
    {
        InsertComponent();

        FSM.ChangeState(FSM.State, IdleState, false);
    }

    /// <summary>
    /// (���� �θ�) Update() -> FSM (���� ��)
    /// </summary>
    public virtual void Update()
    {
        // ������ ������(�þ߰�, �þ� �Ÿ�)�� �����Ѵٸ� ����
        if (monsterData != null)
        {
            MonsterViewAngle(monsterData.viewingAngle, monsterData.viewDistance);
        }

        // ���¸ӽſ��� Update���Ѿ��ϴ� ���̶�� ����, �ƴ϶�� ��������
        FSM.UpdateMethod();
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void SetStat()
    {
        monsterData.currentHp = monsterData.maxHp;

        // NavMeshAgent �̵��ӵ� = ���͵����� �̵��ӵ� ����
        agent.speed = monsterData.moveSpeed;

        deadMonster = false;

        // ���� �Ķ���Ͱ� �ʱⰪ���� �缳��
        anim.SetFloat("MoveZ", 0);
        anim.SetFloat("RandA", 0);
        anim.SetBool("Attack", false);
        anim.SetBool("Die", false);
    }

    public override void InsertComponent()
    {
        base.InsertComponent();
        agent ??= GetComponent<NavMeshAgent>();
        target = null;  // Ÿ�� ����
    }

    public override void AttackColliderSet()
    {
        base.AttackColliderSet();
        // �ش� �ݶ��̴��� ������ �ִ� ������Ʈ�� AttackController ��ũ��Ʈ�� �߰�
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

        // RandA���� �������� ��������ִ� �ڷ�ƾ
        IEnumerator ChangeRandA()
        {
            float duration = randDuration;    // ���� ����
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;

                // Ÿ���� ã�Ҵٸ� RunningState() �� ��ȯ
                if(target != null)
                {
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), rand, Time.deltaTime * 2f));



                yield return null;
            }

            // duration �� ����ߴٸ� ����
            if (time >= duration)
            {
                walkStateMultiple += Mathf.RoundToInt(duration);
                time = 0;

                yield return null;
            }

            // ���� 10�� �ʰ������� ���� ��ȯ
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
            // ���� ������ ����
            Vector3 arrivalLocation = RandDestinationSet(monsterStartPos, 5f);

            // ������ ����
            bool TruePath = agent.SetDestination(arrivalLocation);

            // ������ ���� false��� ����
            if(!TruePath)
            {
                FSM.ChangeState(Define.State.Idle, IdleState);
                yield break;
            }

            while(FSM.State == Define.State.Walk)
            {
                // �ִϸ����� �̵� ���� �Ķ���Ͱ� ����
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 3f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // Ÿ���� �����Ѵٸ� Running���� ���� ��ȯ
                if (target != null)
                {
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                // agent �� �̵����̰�, �������� �����ߴٸ� ����
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 1f)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState);
                    yield break;
                }

                yield return null;
            }
        }

        // ���� ������ ���� ���� �޼ҵ�
        Vector3 RandDestinationSet(Vector3 originPos, float dist)
        {
            // ó�� ���Ͱ� ������ ��ġ���� ���� �� ������������ ��� ���
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
            randDirection += originPos;

            NavMeshPath path = new NavMeshPath();
            bool TruePath = NavMesh.CalculatePath(transform.position, randDirection,NavMesh.AllAreas, path);

            // �ùٸ� ��ΰ� ��ȯ���� �ʾҴٸ� Idle ���·� ��ȯ
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
        // �̵��ӵ� ����
        agent.speed = (monsterData.moveSpeed * 2f);

        // �÷��̾� ��ǥ ����, �̵�
        agent.SetDestination(target.transform.position);

        StartCoroutine(LockTarget());

        IEnumerator LockTarget()
        {
            while(true)
            {
                // �÷��̾�� ������ �Ÿ�
                float distance = Vector3.Distance(target.transform.position, transform.position);

                // �÷��̾ ���ݹ��� ������ ���ð�� AttackState() �� �����Ų��
                if (distance <= monsterData.atkRange)
                {
                    // ���ǵ� ���������� �缳��
                    agent.speed = monsterData.moveSpeed;

                    // ���ݰ� �������� �缳��
                    anim.SetFloat("RandA", UnityEngine.Random.Range(0f, -1f));

                    // ���ݻ��·� ����
                    FSM.ChangeState(Define.State.Attack, AttackState);
                    yield break;
                }

                // �ִϸ����� �̵� ���� �Ķ���Ͱ� ����
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // �Ÿ��� ���� ��Ÿ����� �ְ�, Hurt,Die ���°� �ƴ϶�� ����
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

            // ���ݻ����϶� ����
            while (true)
            {
                // ���ݻ��°� �����Ǿ�����, RandA��(2�� ���� �ִϸ��̼�) �缳��
                if (!anim.GetBool("Attack"))
                {
                    anim.SetFloat("RandA", 0);
                    FSM.ChangeState(Define.State.Running, RunningState);
                    yield break;
                }

                // Ÿ���� �ٶ󺸵��� ����
                transform.LookAt(target.transform);

                // ������ ü���� 0�� �Ǿ����� ����
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
    /// Hurt �ִϸ��̼��� ������ ��ӹ��� �ڽĿ��� ������
    /// </summary>
    public virtual void HurtState()
    {
        // ���� CreatureBase �Ѱ������� �����
    }

    public virtual void DieState()
    {
        anim.SetBool("Die", true);
    }

    /// <summary>
    /// ������ �þ߰�, �þ� �Ÿ�
    /// </summary>
    /// <param name="viewAngle">�þ� ����</param>
    /// <param name="viewDistance">�þ� �Ÿ�</param>
    protected void MonsterViewAngle(float viewAngle,float viewDistance)
    {
        // Ÿ���� �����Ѵٸ� ����
        if (target != null)
            return;

        // ���Ͱ� �ƴ϶�� ���Ͻ�Ų��
        if (monsterData.characterType != Define.CharacterType.Monster)
            return;

        // ���� �ֺ��� �ִ� Ÿ���� ���Ѵ�
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, 1 << LayerMask.NameToLayer("Player"));

        for (int i =0; i < targets.Length; ++i)
        {
            // Ÿ���� ��ġ
            Vector3 targetPos = targets[i].transform.position;
            // �Ÿ� (Ÿ�ٰ��� �Ÿ�)
            Vector3 direction = (targetPos - transform.position).normalized;
            // ���� (Ÿ�ٰ��� ����)
            float angle = Vector3.Angle(direction, transform.forward);

            // �÷��̾��� �þ� ���������� �������� ����
            if (angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, viewDistance))
                {
                    // Ÿ���� ���̾ Player �ϰ�� ����
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        // Ÿ�� ����
                        target = hit.transform.gameObject;
                    }
                }
            }
        }
    }

    #region ���� �ִϸ��̼� Event�� ���� ���� �޼ҵ�

    public void MonsterAttackEnd()
    {
        anim.SetBool("Attack", false);
    }
    #endregion

}
