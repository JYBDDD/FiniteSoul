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

    // �÷��̾� ȸ�� �����̵���
    float evasionMoveX = 0;
    float evasionMoveZ = 0;

    // �÷��̾ ���� ����ִ��� üũ
    bool IsGround = false;

    // �÷��̾� ������ �ݶ��̴� (Attack �ִϸ��̼� �����, �ش� �ִϸ��̼ǿ��� ��ü������ On/Off ��ȯ)
    [SerializeField]
    private Collider[] playerAtkColl;

    /// <summary>
    /// �ʱ� ���� ����� ����� ������ �缳�� ���ִ� �޼ҵ�
    /// </summary>
    public void SetStat(PlayerVolatilityData volData)
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

        // �̾��ϱ⸦ �����Ͽ��ٸ� �� �缳��
        if(volData.currentHp > 0)
        {
            playerData.level = volData.level;
            playerData.currentRune = volData.rune;
            playerData.currentHp = volData.currentHp;
            playerData.maxHp = volData.raiseHp;
            playerData.atk = volData.raiseAtk;
            playerData.def = volData.raiseDef;
        }

        NotToMove = true;
    }

    public override void InsertComponent()
    {
        base.InsertComponent();
    }

    public override void AttackColliderSet()
    {
        base.AttackColliderSet();

        // ����Ÿ���� ���Ÿ��ϰ�� ����, ���Ÿ��� ���� �ش� �߻�ü ������ ������Ʈ�� �߰�
        if (playerData.atkType == Define.AtkType.Projectile)
            return;

        // �ش� �ݶ��̴��� ������ �ִ� ������Ʈ�� AttackController ��ũ��Ʈ�� �߰�
        for (int i = 0; i < playerAtkColl.Length; ++i)
        {
            AttackController attackController = playerAtkColl[i].gameObject.AddComponent<AttackController>();
            attackController.AttackControllerInit(playerData, playerData.atkType);
        }
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

        // ���¸ӽſ��� Update���Ѿ��ϴ� ���̶�� ����, �ƴ϶�� ��������
        FSM.UpdateMethod();
    }


    // Paladin, Archer �������� �ۼ�

    protected virtual void IdleState()
    {
        if(NotToMove)
        {
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))         // Walk
            {
                FSM.ChangeState(Define.State.Walk, WalkState, true);
                return;
            }
            if (Input.GetKey(KeyCode.Space) && !anim.GetBool("Evasion") && !Input.GetKey(KeyCode.S))     // Evasion
            {
                anim.SetBool("Evasion", true);
                playerData.currentStamina -= 30f;
                FSM.ChangeState(Define.State.Evasion, EvasionState, true);
                return;
            }
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))      // Running
            {
                FSM.ChangeState(Define.State.Running, RunningState, true);
                return;
            }
            if (Input.GetKey(KeyCode.Mouse0))    // Attack
            {
                FSM.ChangeState(Define.State.Attack, NormalAttackState);
                return;
            }
            
        }

        anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 4f));
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime * 4f));
        FSM.State = Define.State.Idle;
    }

    protected virtual void WalkState()
    {
        if(NotToMove)
        {
            // ��, ����Ű�� ������ �ʾ��� ��, MoveX �� 0���� �缳��
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.W))         // ����Ű
            {
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.A))         // ���� ����Ű
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), -1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.D))         // ���� ����Ű
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.S))                                     // ����Ű
            {
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), -1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.Space) && !anim.GetBool("Evasion") && !Input.GetKey(KeyCode.S))     // Evasion
            {
                anim.SetBool("Evasion", true);
                playerData.currentStamina -= 30f;
                FSM.ChangeState(Define.State.Evasion, EvasionState, true);
                return;
            }
            if (!Input.anyKey)  // Idle
            {
                FSM.ChangeState(Define.State.Idle, IdleState, true);
                return;
            }
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))      // Running
            {
                FSM.ChangeState(Define.State.Running, RunningState, true);
                return;
            }
            if (Input.GetKey(KeyCode.Mouse0))    // Attack
            {
                FSM.ChangeState(Define.State.Attack, NormalAttackState);
                return;
            }
        }
        

        FSM.State = Define.State.Walk;
    }

    protected virtual void EvasionState()
    {
        if(!anim.GetBool("Evasion"))
        {
            // ȸ�ǰ� ���� ���� ���׹̳� �ڿ�ȸ�� ����
            evasionMoveX = 0;
            evasionMoveZ = 0;
            StartCoroutine(NatureRecovery.NatureRecoveryStamina());
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            return;
        }

        evasionMoveX = moveX;
        evasionMoveZ = moveZ;
        anim.SetBool("Evasion", true);
        FSM.State = Define.State.Evasion;
        //  -> Evasion�� ����� �ش� �ִϸ��̼ǿ� �����Ͽ���
    }

    protected virtual void RunningState()
    {
        if (Input.GetKey(KeyCode.Space) && !anim.GetBool("Evasion") && !Input.GetKey(KeyCode.S))     // Evasion
        {
            anim.SetBool("Evasion", true);
            playerData.currentStamina -= 30f;
            FSM.ChangeState(Define.State.Evasion, EvasionState, true);
            return;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            FSM.ChangeState(Define.State.Idle, IdleState, true);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            FSM.ChangeState(Define.State.Attack, NormalAttackState);
            return;
        }

        // ��,��Ű�� �Է����� ������ MoveX�� ���� 0���� ������
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 4f));
        }
        // ForwardRun
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 4f));
        }
        // LeftRun
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), -2, Time.deltaTime * 4f));
        }
        // BackRun
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), -2, Time.deltaTime * 4f));
        }
        // RightRun
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 2, Time.deltaTime * 4f));
        }

        FSM.State = Define.State.Running;
    }

    protected virtual void NormalAttackState()
    {
        //  Attack �ִϸ��̼��� �����Ű�� Ʈ���� ȣ��
        anim.SetTrigger("AttackTrigger");

        StartCoroutine(ChainAttack());

        IEnumerator ChainAttack()
        {
            // Attack1 �� ������� ���¿��� ���콺 Ŭ���� Attack2 ���, Attack2 �� ������� ���¿��� ����� Attack3 ���
            while (true)
            {
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))         // �ٱ�
                {
                    FSM.ChangeState(Define.State.Running, RunningState, true);
                    yield break;
                }
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))         // �̵�
                {
                    FSM.ChangeState(Define.State.Walk, WalkState, true);
                    yield break;
                }

                // ������ Attack3 ��� ����Ʈ���Ÿ� ����
                if(EndAttackAnimationing())
                {
                    anim.ResetTrigger("AttackTrigger");
                }
                if (AttackAnimationing("Attack2") == 0)
                {
                    anim.SetTrigger("AttackTrigger");
                }
                if (AttackAnimationing("Attack1") == 0)
                {
                    anim.SetTrigger("AttackTrigger");
                }

                yield return null;
            }
        }

        // Attack �ִϸ��̼��� �������̶�� 0, �ƴ϶�� 1
        int AttackAnimationing(string animationName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                // ���Ÿ��� �ƴϰ� ���� �ð��ȿ� ����Ű Ŭ����, ���Ӱ��� ����
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f && 
                    Input.GetKey(KeyCode.Mouse0) && playerData.atkType != Define.AtkType.Projectile)
                {
                    return 0;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    anim.ResetTrigger("AttackTrigger");
                    FSM.ChangeState(Define.State.Idle, IdleState, true);
                    return 2;
                }
            }

            return 1;
        }

        bool EndAttackAnimationing(string animName = "Attack3")
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
                {
                    FSM.ChangeState(Define.State.Idle, IdleState, true);
                    return true;
                }
            }

            return false;
        }

        FSM.State = Define.State.Attack;
    }

    public virtual void HurtState()
    {
        // ü���� 0���� ���ٸ� ����
        if(playerData.currentHp <= 0)
        {
            FSM.ChangeState(Define.State.Die, DieState);
            return;
        }

        anim.SetTrigger("HitTrigger");
        anim.ResetTrigger("AttackTrigger");
        FSM.ChangeState(Define.State.Idle, IdleState, true);
        // ��� �̵� �Ұ� -> �ش� ������ �ִϸ��̼� �̺�Ʈ�� ����

    }

    protected virtual void DieState()
    {
        NotToMove = false;
        //// Die �ִϸ��̼� ��� ��Ű��... TODO
    }

    #region ĳ���� ������ ������
    private void Move()
    {
        Vector3 posVec = new Vector3(moveX, 0, moveZ).normalized;

        // ȸ�� �������Ͻ� �̵����� ������ ������ �Ͻ� ����
        if(evasionMoveX != 0 | evasionMoveZ !=0)
        {
            moveX = evasionMoveX / 100f;
            moveZ = evasionMoveZ / 100f;
            transform.Translate(posVec * (playerData.moveSpeed) * Time.deltaTime);
        }

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
        // ���� ������� ���డ��
        if(Input.GetKeyDown(KeyCode.C) && IsGround)
        {
            IsGround = false;
            rigid.AddForce(Vector3.up * playerData.jumpForce,ForceMode.Impulse);
            // ���� �ִϸ��̼� ���
            anim.SetTrigger("JumpTrigger");
        }
    }


    #endregion

    /// <summary>
    /// ĳ���� ȸ��, ī�޶� ȸ�� (���콺 ��ġ)
    /// </summary>
    private void PlayerLookingMouse()
    {
        if (!NotToMove)
            return;

        mouseX += Input.GetAxis("Mouse X") * 10f;     // ��,��

        float mouseY = Input.GetAxis("Mouse Y");     // ��,��

        if (!(mouseX == 0 && mouseY == 0))
        {
            transform.eulerAngles = new Vector3(0, mouseX, 0);  // ��, �� ȸ��
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!IsGround)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsGround = true;
            }
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
    /// �÷��̾ �����ϼ� ������ ����
    /// </summary>
    private void AnimHitDontMove()
    {
        NotToMove = false;
    }
    /// <summary>
    /// �÷��̾ �����ϼ� �ֵ��� ����
    /// </summary>
    private void AnimHitCanMove()
    {
        NotToMove = true;
    }
    #endregion
}
