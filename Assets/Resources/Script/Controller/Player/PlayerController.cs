using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �÷��̾� ������ �θ� Ŭ���� 
/// </summary>
public class PlayerController : MoveableObject
{
    public UsePlayerData playerData;

    Ray originRay = new Ray();

    /// <summary>
    /// �÷��̾�鸸 ����ϴ� Awake()
    /// </summary>
    protected virtual void Awake()
    {
        Initialize();

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;
    }

    /// <summary>
    /// (���� �θ�) Update() -> FSM
    /// </summary>
    public virtual void Update()    // FSM �� PlayerController �� MonsterController �� ���� ������ �ֵ��� ���� ����
    {
        PlayerLookRotate();

        if (!Input.anyKey)
            State = Define.State.Idle;

        if (Input.GetKey(KeyCode.Space))          // ȸ��
            State = Define.State.Evasion;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))      // �ٱ�
            State = Define.State.Running;

        switch (State)
        {
            case Define.State.Idle:
                IdleState();
                break;
            case Define.State.Walk:
                WalkState();
                break;
            case Define.State.Evasion:
                EvasionState();
                break;
            case Define.State.Running:
                RunningState();
                break;
            case Define.State.Attack:
                AttackState();
                break;
            case Define.State.Jump:
                JumpState();
                break;
            case Define.State.Hurt:
                HurtState();
                break;
            case Define.State.Die:
                DieState();
                break;
        }
    }


    // Paladin, Archer �������� ���Ǵ� �κи� �ۼ��Ǿ���

    protected virtual void IdleState()
    {
        anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 2f));
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime * 2f));
    }

    protected virtual void WalkState()
    {
        if (Input.GetKey(KeyCode.W))         // ����Ű
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 4f));

        if(Input.GetKey(KeyCode.S))                                     // ����Ű
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), -1, Time.deltaTime * 4f));

    }

    private void EvasionState()
    {
        anim.Play("Evasion");
        // �ش� �ִϸ��̼��� �������϶� �Ͻ������� �̵��ӵ� * 2  TODO
    }

    protected virtual void RunningState()
    {
        // �ش� �ִϸ��̼��� �������϶� �̵��ӵ� * 2  TODO
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 4f));
    }

    protected virtual void AttackState()
    {
       
    }

    protected virtual void JumpState()
    {
        
    }

    protected virtual void HurtState()
    {
        
    }

    protected virtual void DieState()
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void SetStat()
    {
        base.SetStat();
    }

    #region ĳ���� ������ ������
    private void Move()
    {
        // �̵� �� ����������� TODO
        float floatX = Input.GetAxisRaw("Horizontal") * 3f;
        float floatZ = Input.GetAxisRaw("Vertical") * 3f;

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized;

        transform.Translate(posVec * Time.deltaTime);
        State = Define.State.Walk;
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            // ���� �� ����������� TODO
            rigid.AddForce(Vector3.up * 7f,ForceMode.Impulse);
        }
    }


    #endregion

    /// <summary>
    /// �÷��̾ ���콺��ġ�� �ٶ󺸵��� �ϴ� �޼���
    /// </summary>
    private void PlayerLookRotate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ray.direction != originRay.direction)    // ���콺 ��ġ�� ����Ǿ��ٸ� ����
        {
            originRay = ray;

            Physics.Raycast(ray, out hit); 

            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;


            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(hit.point), Time.deltaTime * 2f);
        }
    }





}
