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
        // �ѹ��� �����Ű���� ���� TODO
        StartCoroutine(ChangeRandA());

        // RandA���� �������� ��������ִ� �ڷ�ƾ
        IEnumerator ChangeRandA()
        {
            float duration = 4f;    // ���� ����
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;

                yield return null;
            }

            // duration �� ����ߴٸ� ����
            if(time >= duration)
            {
                // Animator Parameter / RandA �� (0f ~ -1f)���� ������ ����
                anim.SetFloat("RandA", UnityEngine.Random.Range(0f, -1f));
                walkStateMultiple += duration;

                yield return null;
            }

            // �ش� ����� (���� ���� * 2)�� �������� ��, �������� 0�̶�� Walk ���·� ����
            if(walkStateMultiple % (duration * 2) == 0)
            {
                State = Define.State.Walk;
                yield break;
            }

            yield return null;
        }
    }

    protected virtual void WalkState()
    {
        // ¥�� ���� TODO
        /*int layer = NavMesh.GetAreaFromName("Walkable");

        // ���� ������ ����
        Vector3 arrivalLocation = RandDestinationSet(transform.position, 5f, layer);

        
        Vector3 RandDestinationSet(Vector3 originPos, float dist, int mask)
        {
            // ó�� ���Ͱ� ������ ��ġ���� ���� �� ������������ ��� ���
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
            randDirection += originPos;

            NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, mask);
            return navHit.position;
        }*/
    }

    protected virtual void RunningState()
    {
        // �i�� Ÿ���� �������� �ʴ´ٸ�, ���� Idle ����
        if(target == null)
        {
            State = Define.State.Idle;
        }
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
                        // Ÿ�� ����
                        target = hit.transform.gameObject;
                        // ���� ����
                        State = Define.State.Running;
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


    
}
