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

    // ������ ���� ��ġ
    public Vector3 monsterStartPos = Vector3.zero;

    // ������ Ÿ��
    GameObject target = null;

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
        if (monsterData?.viewingAngle != null && monsterData?.viewDistance != null)
        {
            MonsterViewAngle(monsterData.viewingAngle, monsterData.viewDistance);
        }

        // ���¸ӽſ��� Update���Ѿ��ϴ� ���̶�� ����, �ƴ϶�� ��������
        FSM.UpdateMethod();

        State = FSM.State;
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void SetStat()
    {
        monsterData.currentHp = monsterData.maxHp;
    }

    public override void InsertComponent()
    {
        base.InsertComponent();
        agent ??= GetComponent<NavMeshAgent>();
        target = null;  // Ÿ�� ����
    }

    protected virtual void IdleState()
    {
        float rand = UnityEngine.Random.Range(0f, -1f);
        StartCoroutine(ChangeRandA());

        // RandA���� �������� ��������ִ� �ڷ�ƾ
        IEnumerator ChangeRandA()
        {
            float duration = 4f;    // ���� ����
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;

                // �ִϸ����� �̵� ���� �Ķ���Ͱ� ����

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

            // duration �� ����ߴٸ� ����
            if (time >= duration)
            {
                // Animator Parameter / RandA �� (0f ~ -1f)���� ������ ����
                walkStateMultiple += Mathf.RoundToInt(duration);
                time = 0;

                yield return null;
            }

            // �ش� ����� (���� ���� * 2)�� �������� ��, �������� 0�̶�� Walk ���·� ����
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
            // ���� ������ ����
            Vector3 arrivalLocation = RandDestinationSet(monsterStartPos, 5f);

            // ������ ����
            bool TruePath = agent.SetDestination(arrivalLocation);
            if(!TruePath)
            {
                yield break;
            }

            while(TruePath)
            {
                // �ִϸ����� �̵� ���� �Ķ���Ͱ� ����
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // agent �� �̵����̰�, �������� �����ߴٸ� ����
                if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 1f)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState, false);
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

                // �ִϸ����� �̵� ���� �Ķ���Ͱ� ����
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 2f));
                anim.SetFloat("RandA", Mathf.Lerp(anim.GetFloat("RandA"), 0, Time.deltaTime * 2f));

                // �i�� Ÿ���� �������� �ʴ´ٸ�, ���� Idle ����
                if (target == null)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState, false);
                }

                // �÷��̾� ��ǥ ����, �̵�
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
    /// ������ �þ߰�, �þ� �Ÿ�
    /// </summary>
    /// <param name="viewAngle">�þ� ����</param>
    /// <param name="viewDistance">�þ� �Ÿ�</param>
    protected void MonsterViewAngle(float viewAngle,float viewDistance)
    {
        // ���Ͱ� �ƴ϶�� ���Ͻ�Ų��
        if (monsterData.characterType != Define.CharacterType.Monster)
            return;

        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary, Color.red);

        // Ÿ���� ���Ѵ�
        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, LayerMask.NameToLayer("Player"));

        // Ÿ���� ������ ������ �ʾҴٸ� ����
        if (targets.Length == 0)
            return;

        for(int i =0; i < targets.Length; ++i)
        {
            // Ÿ���� ��ġ
            Vector3 targetPos = targets[i].transform.position;
            // �Ÿ� (Ÿ�ٰ��� �Ÿ�)
            Vector3 direction = (targetPos - transform.position).normalized;
            // ���� (Ÿ�ٰ��� ����)
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < viewAngle * 0.5f)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position + Vector3.up, direction,out hit,viewDistance))
                {
                    // Ÿ���� ���̾ Player �ϰ�� ����
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        Debug.Log("�÷��̾� ã��");
                        // Ÿ�� ����
                        target = hit.transform.gameObject;
                        // ���� ����
                        FSM.ChangeState(Define.State.Running, RunningState, true);
                    }
                }
            }
        }

        // ��� ����
        Vector3 BoundaryAngle(float angle)
        {
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        }
    }

    #region ���� �ִϸ��̼� Event�� ���� ���� �޼ҵ�

    public void MonsterAttackEnd()
    {
        anim.SetBool("Attack", false);
    }

    #endregion

}
