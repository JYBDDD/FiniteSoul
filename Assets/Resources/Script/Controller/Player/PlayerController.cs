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

    // ĳ���Ͱ� �ٶ󺸴� ������ MouseX (��,��)
    float mouseX = 0;

    // �÷��̾� �̵���
    float moveX = 0;
    float moveZ = 0;

    /// <summary>
    /// ���� ����� ����� ������ �缳�� ���ִ� �޼ҵ�
    /// </summary>
    public void SetStat()
    {
        // ������ �������� ��ü�Ǵ� ������
        playerData.maxHp = playerData.growthStat.maxHp;
        playerData.atk = playerData.growthStat.atk;
        playerData.def = playerData.growthStat.def;
        playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;

        // ù������ ��� �ִ� Hp�� ����
        if(playerData.playerVolatility.currentHp == 0)
        {
            playerData.currentHp = playerData.maxHp;
        }

        // �������� ������
        playerData.currentMana = playerData.maxMana;
        playerData.currentStamina = playerData.maxStamina;

        // �ֹ߼������Ͱ� �߰��ؾߵǴ� ��Ȳ�̶�� ���� ���⼭ �ٽ� �缳�� TODO
    }

    /// <summary>
    /// �÷��̾�鸸 ����ϴ� Awake()
    /// </summary>
    protected virtual void Awake()
    {
        InsertComponent();

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;

        FSM.ChangeState(FSM.State, IdleState, true);
    }

    /// <summary>
    /// (���� �θ�) Update() -> FSM (�÷��̾� ��)
    /// </summary>
    public virtual void Update()
    {
        PlayerLookingMouse();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("RunBool", false);
        }

        // ���¸ӽſ��� Update���Ѿ��ϴ� ���̶�� ����, �ƴ϶�� ��������
        FSM.UpdateMethod();
    }

    /*/// <summary>
    /// ���� ����� ����
    /// </summary>
    private void OnApplicationQuit()
    {
        ResoureUtil.SaveData(playerData.index, playerData.level, StageManager.Instance.stageData.index, transform.position, playerData.currentRune,
            playerData.maxHp, playerData.currentHp, playerData.atk, playerData.def);
    }*/


    // Paladin, Archer �������� ���Ǵ� �κи� �ۼ��Ǿ���

    protected virtual void IdleState()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))         // Walk
        {
            FSM.ChangeState(Define.State.Walk, WalkState, true);
            return;
        }
        if (Input.GetKey(KeyCode.Space))     // Evasion
        {
            anim.SetBool("Evasion", true);
            FSM.ChangeState(Define.State.Evasion, EvasionState, true);
            return;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))      // Running
        {
            FSM.ChangeState(Define.State.Running, RunningState, true);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            FSM.ChangeState(Define.State.Attack, NormalAttackState, true);
            return;
        }

        anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 2f));
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime * 2f));
        FSM.State = Define.State.Idle;
    }

    protected virtual void WalkState()
    {
        if (Input.GetKey(KeyCode.W))         // ����Ű
        {
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 4f));
        }
        if(Input.GetKey(KeyCode.S))                                     // ����Ű
        {
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), -1, Time.deltaTime * 4f));
        }
        if(Input.GetKey(KeyCode.Space))     // Evasion;
        {
            anim.SetBool("Evasion", true);
            FSM.ChangeState(Define.State.Evasion, EvasionState, true);
            return;
        }
        if (!Input.anyKey)  // Idle
        {
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            return;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))      // Running
        {
            FSM.ChangeState(Define.State.Running, RunningState, true);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            FSM.ChangeState(Define.State.Attack, NormalAttackState, true);
            return;
        }


        FSM.State = Define.State.Walk;
    }

    protected virtual void EvasionState()
    {
        if(!anim.GetBool("Evasion"))
        {
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            return;
        }

        anim.SetBool("Evasion", true);
        FSM.State = Define.State.Evasion;
    }

    protected virtual void RunningState()
    {
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            anim.SetBool("RunBool", false);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            FSM.ChangeState(Define.State.Attack, NormalAttackState, true);
            return;
        }

        anim.SetBool("RunBool", true);
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 4f));
        FSM.State = Define.State.Running;
    }

    protected virtual void NormalAttackState()
    {
        if(!anim.GetBool("Attack"))
        {
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            return;
        }

        anim.SetBool("Attack", true);
        FSM.State = Define.State.Attack;
    }

    /// ------------------------------------------------------------  ���� FSM  NormalAttack ���� ���� , �ؿ� �κ��� �̱��� ���� TODO

    protected virtual void ProjectileAttackState()
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

    #region ĳ���� ������ ������
    private void Move()
    {
        Vector3 posVec = new Vector3(moveX, 0, moveZ).normalized;

        // �� ����Ʈ�� �������¶�� �̵��ӵ� 2f ��
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
            transform.Translate(posVec * (playerData.moveSpeed * 2f) * Time.deltaTime);
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
            transform.Translate(posVec * playerData.moveSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            rigid.AddForce(Vector3.up * playerData.jumpForce,ForceMode.Impulse);
        }
    }


    #endregion

    /// <summary>
    /// ĳ���� ȸ��, ī�޶� ȸ�� (���콺 ��ġ)
    /// </summary>
    private void PlayerLookingMouse()
    {
        mouseX += Input.GetAxis("Mouse X") * 10f;     // ��,��

        float mouseY = Input.GetAxis("Mouse Y");     // ��,��

        Vector3 camAngle = Camera.main.transform.rotation.eulerAngles;
        float x = camAngle.x - mouseY;
        

        if (!(mouseX == 0 && mouseY == 0))
        {
            transform.eulerAngles = new Vector3(0, mouseX, 0);  // ��, �� ȸ��

            //Camera.main.transform.rotation = Quaternion.Euler(x, transform.eulerAngles.y, 0);   // ��, �� ȸ��

        }
    }

        #region �ִϸ��̼ǿ� ���� �޼���
        /// <summary>
        /// ȸ�� �ִϸ��̼ǿ� ���� �޼���
        /// </summary>
        private void AnimEvasionEnd()
    {
        anim.SetBool("Evasion", false);
    }
    /// <summary>
    /// ���� �ִϸ��̼ǿ� ���� �޼���
    /// </summary>
    /// <returns></returns>
    private void AnimAttackEnd()
    {
        anim.SetBool("Attack", false);
    }
    #endregion
}
