using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MoveableObject
{
    /// <summary>
    /// �ش� ���Ͱ� ����ϴ� ������
    /// </summary>
    UseMonsterData monsterData;

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
        // ���� ������ �ְ� anim. �� �Ķ���� RandA ���� Random���� �����Ѵ�
        //    Idle -> RandA �� 1
        //    TurnRight90 -> RandA �� - 1

    }

    private void WalkState()
    {
        
    }

    private void RunningState()
    {
        // Ÿ���� ���� �����Ÿ�

    }

    private void AttackState()
    {
        
    }

    private void HurtState()
    {
        // Hurt�� �ڷ� �и��°����� ó���� ����
        // Idle���� �����߿��� �и��°����� TODO
    }

    private void DieState()
    {
        
    }

    /*Mutant �ִϸ����� ���� TODO

    RandA �� ����� ��� -> Attack1
    RandA �� ������ ��� -> Attack2

    Mutant �� ������ �Ķ���� �Ұ����� üũ���� �ʰ�
    ���� �����Ÿ��ȿ� �÷��̾ �����Ͽ������ �����ϴ°�����..

    ���Ϳ��� �þ߰� -> viewingAngle
              �þ߰Ÿ� -> viewingDistance 

    ������ �ؼ� �þ߿� ����� ��� Run �ִϸ��̼� ����� �i�ư��°����� ������ ��
    */

 
}
