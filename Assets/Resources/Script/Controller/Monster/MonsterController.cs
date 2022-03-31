using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MoveableObject
{
    /// <summary>
    /// �ش� ���Ͱ� ����ϴ� ������
    /// </summary>
    public UseMonsterData monsterData;

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
    /// ������ �þ߰�, �þ� �Ÿ�
    /// </summary>
    /// <param name="viewAngle">�þ� ����</param>
    /// <param name="viewDistance">�þ� �Ÿ�</param>
    protected void MonsterViewAngle(float viewAngle,float viewDistance)
    {
        // ���Ͱ� �ƴ϶�� ���Ͻ�Ų��
        if (monsterData.type != Define.CharacterType.Monster)
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
                        // �÷��̾  ���� �޷����� TODO
                    }
                }
            }
        }

        // ��� ����
        Vector3 BoundaryAngle(float _angle)
        {
            _angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
        }
    }


    
}
