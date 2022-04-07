using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 플레이어 직업의 부모 클래스 
/// </summary>
public class PlayerController : MoveableObject
{
    public UsePlayerData playerData;

    // 캐릭터가 바라보는 방향을 MouseX (좌,우)
    float mouseX = 0;

    // 플레이어 이동값
    float moveX = 0;
    float moveZ = 0;

    /// <summary>
    /// 스텟 변경시 변경된 스탯을 재설정 해주는 메소드
    /// </summary>
    public void SetStat()
    {
        // 룬으로 레벨업시 교체되는 데이터
        playerData.maxHp = playerData.growthStat.maxHp;
        playerData.atk = playerData.growthStat.atk;
        playerData.def = playerData.growthStat.def;
        playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;

        // 첫설정일 경우 최대 Hp로 설정
        if(playerData.playerVolatility.currentHp == 0)
        {
            playerData.currentHp = playerData.maxHp;
        }

        // 변동없는 데이터
        playerData.currentMana = playerData.maxMana;
        playerData.currentStamina = playerData.maxStamina;

        // 휘발성데이터가 추가해야되는 상황이라면 값을 여기서 다시 재설정 TODO
    }

    /// <summary>
    /// 플레이어들만 사용하는 Awake()
    /// </summary>
    protected virtual void Awake()
    {
        InsertComponent();

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;

        FSM.ChangeState(FSM.State, IdleState, true);
    }

    /// <summary>
    /// (상위 부모) Update() -> FSM (플레이어 용)
    /// </summary>
    public virtual void Update()
    {
        PlayerLookingMouse();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("RunBool", false);
        }

        // 상태머신에서 Update시켜야하는 값이라면 실행, 아니라면 실행중지
        FSM.UpdateMethod();
    }

    /*/// <summary>
    /// 게임 종료시 저장
    /// </summary>
    private void OnApplicationQuit()
    {
        ResoureUtil.SaveData(playerData.index, playerData.level, StageManager.Instance.stageData.index, transform.position, playerData.currentRune,
            playerData.maxHp, playerData.currentHp, playerData.atk, playerData.def);
    }*/


    // Paladin, Archer 공통으로 사용되는 부분만 작성되었음

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
        if (Input.GetKey(KeyCode.W))         // 전진키
        {
            anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 4f));
        }
        if(Input.GetKey(KeyCode.S))                                     // 후진키
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

    /// ------------------------------------------------------------  현재 FSM  NormalAttack 까지 구현 , 밑에 부분은 미구현 상태 TODO

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

    #region 캐릭터 움직임 구현부
    private void Move()
    {
        Vector3 posVec = new Vector3(moveX, 0, moveZ).normalized;

        // 왼 시프트를 누른상태라면 이동속도 2f 곱
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
    /// 캐릭터 회전, 카메라 회전 (마우스 위치)
    /// </summary>
    private void PlayerLookingMouse()
    {
        mouseX += Input.GetAxis("Mouse X") * 10f;     // 좌,우

        float mouseY = Input.GetAxis("Mouse Y");     // 상,하

        Vector3 camAngle = Camera.main.transform.rotation.eulerAngles;
        float x = camAngle.x - mouseY;
        

        if (!(mouseX == 0 && mouseY == 0))
        {
            transform.eulerAngles = new Vector3(0, mouseX, 0);  // 좌, 우 회전

            //Camera.main.transform.rotation = Quaternion.Euler(x, transform.eulerAngles.y, 0);   // 상, 하 회전

        }
    }

        #region 애니메이션에 들어가는 메서드
        /// <summary>
        /// 회피 애니메이션에 들어가는 메서드
        /// </summary>
        private void AnimEvasionEnd()
    {
        anim.SetBool("Evasion", false);
    }
    /// <summary>
    /// 공격 애니메이션에 들어가는 메서드
    /// </summary>
    /// <returns></returns>
    private void AnimAttackEnd()
    {
        anim.SetBool("Attack", false);
    }
    #endregion
}
