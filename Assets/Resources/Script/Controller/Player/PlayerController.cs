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

    // 플레이어 회피 고정이동값
    float evasionMoveX = 0;
    float evasionMoveZ = 0;

    // 플레이어가 땅에 닿아있는지 체크
    bool IsGround = false;

    [SerializeField, Tooltip("플레이어 무기의 콜라이더 (Attack 애니메이션 재생시, 해당 애니메이션에서 자체적으로 On/Off 전환)")]
    private Collider[] playerAtkColl;

    [SerializeField,Tooltip("플레이어 오른손 포션 (Mat 변경용)")]
    MeshRenderer leftHandPotion;
    /// <summary>
    /// 포션이 사용중인지 체크하는 변수
    /// </summary>
    bool potionUseBool = true;

    /// <summary>
    /// 초기 스텟 변경시 변경된 스탯을 재설정 해주는 메소드
    /// </summary>
    public void SetStat(PlayerVolatilityData volData)
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

        // 이어하기를 설정하였다면 값 재설정
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
        FSM.ChangeState(FSM.State, IdleState, true);

        InputManager.Instance.KeyAction += Move;
        InputManager.Instance.KeyAction += Jump;
        InputManager.Instance.KeyAction += ConsumAction;

        InsertComponent();
    }

    public override void AttackColliderSet()
    {
        base.AttackColliderSet();

        // 공격타입이 원거리일경우 리턴, 원거리인 경우는 해당 발사체 생성시 컴포넌트를 추가
        if (playerData.atkType == Define.AtkType.Projectile)
            return;

        // 해당 콜라이더를 가지고 있는 오브젝트에 AttackController 스크립트를 추가
        for (int i = 0; i < playerAtkColl.Length; ++i)
        {
            if(playerAtkColl[i].GetComponent<AttackController>() == null)
            {
                AttackController attackController = playerAtkColl[i].gameObject.AddComponent<AttackController>();
                attackController.AttackControllerInit(playerData, playerData.atkType);
            }
        }
    }

    protected void OnDisable()
    {
        // 오류 방지
        if(playerData.currentHp <= 0)
        {
            InputManager.Instance.KeyAction -= Move;
            InputManager.Instance.KeyAction -= Jump;
            InputManager.Instance.KeyAction -= ConsumAction;
        }
    }

    /// <summary>
    /// (상위 부모) Update() -> FSM (플레이어 용)
    /// </summary>
    public virtual void Update()
    {
        PlayerLookingMouse();

        // 상태머신에서 Update시켜야하는 값이라면 실행, 아니라면 실행중지
        FSM.UpdateMethod();
    }


    // Paladin, Archer 공통으로 작성

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
            // 좌, 우측키를 누르지 않았을 시, MoveX 값 0으로 재설정
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 0, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.W))         // 전진키
            {
                anim.SetFloat("MoveZ", Mathf.Lerp(anim.GetFloat("MoveZ"), 1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.A))         // 좌측 전진키
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), -1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.D))         // 우측 전진키
            {
                anim.SetFloat("MoveX", Mathf.Lerp(anim.GetFloat("MoveX"), 1, Time.deltaTime * 4f));
            }
            if (Input.GetKey(KeyCode.S))                                     // 후진키
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
            // 회피가 끝난 이후 스테미너 자연회복 실행
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
        //  -> Evasion의 종료는 해당 애니메이션에 삽입하였음
    }

    protected virtual void RunningState()
    {
        if(playerData.currentHp <= 0)
        {
            FSM.ChangeState(Define.State.Die, DieState);
            return;
        }
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

        // 좌,우키를 입력하지 않을시 MoveX의 값을 0으로 돌린다
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
        //  Attack 애니메이션을 실행시키는 트리거 호출
        anim.SetTrigger("AttackTrigger");

        StartCoroutine(ChainAttack());

        IEnumerator ChainAttack()
        {
            // Attack1 이 재생중인 상태에서 마우스 클릭시 Attack2 재생, Attack2 가 재생중인 상태에서 재생시 Attack3 재생
            while (true)
            {
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))         // 뛰기
                {
                    FSM.ChangeState(Define.State.Running, RunningState, true);
                    yield break;
                }
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))         // 이동
                {
                    FSM.ChangeState(Define.State.Walk, WalkState, true);
                    yield break;
                }

                // 마지막 Attack3 라면 공격트리거를 리셋
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

        // Attack 애니메이션이 실행중이라면 0, 아니라면 1
        int AttackAnimationing(string animationName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            {
                // 원거리가 아니고 일정 시간안에 공격키 클릭시, 연속공격 실행
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
        // 체력이 0보다 낮다면 실행
        if(playerData.currentHp <= 0)
        {
            FSM.ChangeState(Define.State.Die, DieState);
            return;
        }

        anim.SetTrigger("HitTrigger");
        anim.ResetTrigger("AttackTrigger");
        FSM.ChangeState(Define.State.Idle, IdleState, true);
        // 잠시 이동 불가 -> 해당 구문은 애니메이션 이벤트로 삽입

    }

    protected virtual void DieState()
    {
        NotToMove = false;
        anim.SetTrigger("DeathTrigger");

        // 룬을 먹지 못한상태라면 삭제
        if(ObjectPoolManager.ParentRune.transform.childCount > 0)
        {
            var runeObject = ObjectPoolManager.ParentRune.transform.GetChild(0).gameObject;
            if (runeObject.activeSelf)
            {
                ObjectPoolManager.Instance.GetPush(runeObject);
            }
        }

        // 플레이어 사망시 드랍룬 설정
        Rune.DropRuneDataSetting(playerData.currentRune,transform.position);
        // 플레이어 사망시 인벤토리 데이터 저장
        ResourceUtil.InvenSaveData();
    }

    #region 캐릭터 움직임 구현부, 소지품 변경
    private void Move()
    {
        Vector3 posVec = new Vector3(moveX, 0, moveZ).normalized;

        // 회피 상태중일시 이동값을 현재의 값으로 일시 고정
        if(evasionMoveX != 0 | evasionMoveZ !=0)
        {
            moveX = evasionMoveX / 100f;
            moveZ = evasionMoveZ / 100f;
            transform.Translate(posVec * (playerData.moveSpeed) * Time.deltaTime);
        }

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
        // 땅에 닿았을때 실행가능
        if(Input.GetKeyDown(KeyCode.C) && IsGround)
        {
            IsGround = false;
            rigid.AddForce(Vector3.up * playerData.jumpForce,ForceMode.Impulse);
            // 점프 애니메이션 재생
            anim.SetTrigger("JumpTrigger");
        }
    }

    private void ConsumAction()
    {
        // 소비 아이템 변경 (인벤토리에 있는 모든 소비아이템으로 변경가능)
        if(Input.GetKeyDown(KeyCode.Z) && IsGround)
        {
            BelongingsUI.TempInstance.ConsumDataChange();   
        }

        // 물약이 1개이상 존재한다면 실행
        if (Input.GetKeyDown(KeyCode.Q) && IsGround && BelongingsUI.TempInstance.ConsumBool() && potionUseBool == true)
        {
            // 현재 장착중인 소비아이템 사용
            var potion = BelongingsUI.TempInstance.ConsumUse();
            potionUseBool = false;

            anim.SetTrigger("DrinkTrigger");

            // 쓰는 포션에따라 Mat값 변경
            DrinkPotionMatSetting(potion.name);

            // 두번에 걸쳐 회복하도록 설정
            StartCoroutine(PotionHeal(potion.name));
        }

        void DrinkPotionMatSetting(string name)
        {
            if (name == "HpPotion")
            {
                leftHandPotion.material = Resources.Load<Material>(Define.ItemMixEnum.hpPotionMat);
            }
            else
            {
                leftHandPotion.material = Resources.Load<Material>(Define.ItemMixEnum.mpPotionMat);
            }
        }

        IEnumerator PotionHeal(string name)
        {
            float duraction = 1f;
            float time = 0;

            int cutTime = 0;

            while(time < duraction)
            {
                // 물약을 마시는 도중 피해를 입는다면 회복을 중지
                if (FSM.State == Define.State.Hurt)
                {
                    potionUseBool = true;
                    yield break;
                }

                time += Time.deltaTime;

                if(time > 0.2f && cutTime == 0)
                {
                    ++cutTime;
                    PotionSet(name);
                }

                if(time > 0.9f && cutTime == 1)
                {
                    PotionSet(name);
                    potionUseBool = true;
                    yield break;
                }

                yield return null;
            }
        }

        void PotionSet(string name)
        {
            if (name == "HpPotion")
            {
                // 포션 사운드
                SoundPotionDrink();

                playerData.currentHp += 50;
                var healObj = ObjectPoolManager.Instance.GetPool<ParticleChild>(Define.ParticleEffectPath.PotionHeal.hpPotionHeal, 
                    transform.position + Vector3.up, Define.CharacterType.Particle);
                
                // 최대 체력이상으로 회복 불가
                if(playerData.currentHp > playerData.maxHp)
                {
                    playerData.currentHp = playerData.maxHp;
                }
            }
            else
            {
                playerData.currentMana += 30;
                var healObj = ObjectPoolManager.Instance.GetPool<ParticleChild>(Define.ParticleEffectPath.PotionHeal.manaPotionHeal,
                    transform.position + Vector3.up, Define.CharacterType.Particle);

                // 최대 마나이상으로 회복 불가
                if (playerData.currentMana > playerData.maxMana)
                {
                    playerData.currentMana = playerData.maxMana;
                }
            }
        }
    }

    #endregion

    /// <summary>
    /// 캐릭터 회전, 카메라 회전 (마우스 위치)
    /// </summary>
    private void PlayerLookingMouse()
    {
        if (!NotToMove)
            return;

        mouseX += Input.GetAxis("Mouse X") * 10f;     // 좌,우

        float mouseY = Input.GetAxis("Mouse Y");     // 상,하

        if (!(mouseX == 0 && mouseY == 0))
        {
            transform.eulerAngles = new Vector3(0, mouseX, 0);  // 좌, 우 회전
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

    #region 애니메이션에 들어가는 메서드
    /// <summary>
    /// 회피 애니메이션에 들어가는 메서드
    /// </summary>
    private void AnimEvasionEnd()
    {
        anim.SetBool("Evasion", false);
    }

    /// <summary>
    /// 플레이어가 움직일수 없도록 설정
    /// </summary>
    private void AnimHitDontMove()
    {
        NotToMove = false;
    }
    /// <summary>
    /// 플레이어가 움직일수 있도록 설정
    /// </summary>
    private void AnimHitCanMove()
    {
        NotToMove = true;
    }
    #endregion

    #region 애니메이션에 들어가는 사운드 Event  (public)
    /// <summary>
    /// 플레이어가 걷거나 뛸때 재생되는 사운드
    /// </summary>
    private void SoundWalkAndRun()
    {
        SoundManager.Instance.PlayAudio("PlayerWalk",SoundPlayType.Multi);
    }
    /// <summary>
    /// 플레이어 회피 이동시 재생되는 사운드
    /// </summary>
    private void SoundEvasion()
    {
        SoundManager.Instance.PlayAudio("Evasion", SoundPlayType.Single);
    }
    #endregion
    #region 애니메이션에 삽입하지 않고 사용할 사운드 Event
    /// <summary>
    /// 플레이어 포션 사용시 재생되는 사운드
    /// </summary>
    private void SoundPotionDrink()
    {
        SoundManager.Instance.PlayAudio("PotionDrink", SoundPlayType.Single);
    }
    #endregion
}
