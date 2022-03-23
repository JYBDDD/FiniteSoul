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

    // 캐릭터가 바라보는 방향을 결정하는 Ray
    Ray originRay = new Ray();

    /// <summary>
    /// 플레이어들만 사용하는 Awake()
    /// </summary>
    protected virtual void Awake()
    {
        Initialize();

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;
    }

    /// <summary>
    /// (상위 부모) Update() -> FSM
    /// </summary>
    public virtual void Update()    // FSM 을 PlayerController 와 MonsterController 가 각각 가지고 있도록 나눌 것임
    {
        Debug.Log(State);
        PlayerLookRotate();

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("RunBool", false);
        }

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
                if (playerData.atkType == Define.AtkType.Normal)    
                {
                    // 근접 공격이라면
                    NormalAttackState();

                }
                else
                {
                    // 원거리 공격이라면
                    ProjectileAttackState();

                }
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


    // Paladin, Archer 공통으로 사용되는 부분만 작성되었음

    protected virtual void IdleState()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))         // Walk
        {
            State = Define.State.Walk;
            return;
        }
        if (Input.GetKey(KeyCode.Space))     // Evasion
        {
            anim.SetBool("Evasion", true);
            State = Define.State.Evasion;
            return;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))      // Running
        {
            State = Define.State.Running;
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            State = Define.State.Attack;
            return;
        }

        anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 2f));
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 0, Time.deltaTime * 2f));
        State = Define.State.Idle;
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
            State = Define.State.Evasion;
            return;
        }
        if (!Input.anyKey)  // Idle
        {
            State = Define.State.Idle;
            return;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))      // Running
        {
            State = Define.State.Running;
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            State = Define.State.Attack;
            return;
        }


        State = Define.State.Walk;
    }

    protected virtual void EvasionState()
    {
        // 해당 애니메이션이 실행중일때 일시적으로 이동속도 * 2  TODO

        if(!anim.GetBool("Evasion"))
        {
            State = Define.State.Idle;
            return;
        }

        anim.SetBool("Evasion", true);
        State = Define.State.Evasion;
    }

    protected virtual void RunningState()
    {
        // 해당 애니메이션이 실행중일때 이동속도 * 2  TODO

        if(!Input.GetKey(KeyCode.LeftShift))
        {
            State = Define.State.Idle;
            anim.SetBool("RunBool", false);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0))    // Attack
        {
            anim.SetBool("Attack", true);
            State = Define.State.Attack;
            return;
        }

        anim.SetBool("RunBool", true);
        anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 2, Time.deltaTime * 4f));
        State = Define.State.Running;
    }

    protected virtual void NormalAttackState()
    {
        if(!anim.GetBool("Attack"))
        {
            State = Define.State.Idle;
            return;
        }

        anim.SetBool("Attack", true);
        State = Define.State.Attack;
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

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void SetStat()
    {
        base.SetStat();
    }

    #region 캐릭터 움직임 구현부
    private void Move()
    {
        // 이동 값 지정해줘야함 TODO
        float floatX = Input.GetAxisRaw("Horizontal") * 3f;
        float floatZ = Input.GetAxisRaw("Vertical") * 3f;

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized;

        transform.Translate(posVec * Time.deltaTime);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            // 점프 값 지정해줘야함 TODO
            rigid.AddForce(Vector3.up * 7f,ForceMode.Impulse);
        }
    }


    #endregion

    /// <summary>
    /// 플레이어가 마우스위치를 바라보도록 하는 메서드
    /// </summary>
    private void PlayerLookRotate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ray.direction != originRay.direction)    // 마우스 위치가 변경되었다면 실행
        {
            originRay = ray;

            Physics.Raycast(ray, out hit);

            if (hit.point == Vector3.zero)
                return;

            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;


            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(hit.point), Time.deltaTime * 2f);
        }
    }

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
}
