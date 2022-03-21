using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 플레이어 직업의 부모 클래스 
/// </summary>
public class PlayerController : MoveableObject
{
    public UsePlayerData playerData;

    // 플레이어가 바라보는 방향값을 담고 있는 RayCastHit
    RaycastHit GetRayHit;

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
        PlayerLookRotate();

        switch (State)
        {
            case Define.State.Idle:
                StartCoroutine(IdleState());
                break;
            case Define.State.Walk:
                StartCoroutine(WalkState());
                break;
            case Define.State.Running:
                StartCoroutine(RunningState());
                break;
            case Define.State.Attack:
                StartCoroutine(AttackState());
                break;
            case Define.State.Jump:
                StartCoroutine(JumpState());
                break;
            case Define.State.Hurt:
                StartCoroutine(HurtState());
                break;
            case Define.State.Die:
                StartCoroutine(DieState());
                break;
        }
    }

    protected virtual IEnumerator IdleState()
    {

        yield return null;
    }

    protected virtual IEnumerator WalkState()
    {
        yield return null;
    }

    protected virtual IEnumerator RunningState()
    {
        yield return null;
    }

    protected virtual IEnumerator AttackState()
    {
        yield return null;
    }

    protected virtual IEnumerator JumpState()
    {
        yield return null;
    }

    protected virtual IEnumerator HurtState()
    {
        yield return null;
    }

    protected virtual IEnumerator DieState()
    {
        yield return null;
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
        bool isBoolRay = Physics.Raycast(ray, out hit/*, mask*/);


        if (isBoolRay)    // 마우스 위치가 변경되었다면 실행
        {
            GetRayHit = hit;
            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;

            // 방향 벡터
            Vector3 dir = hit.transform.position - transform.position;
            dir.y = 0;

            // 방향 쿼터니언 값
            Quaternion rot = Quaternion.LookRotation(dir.normalized);

            // 방향 쿼터니언 값을 전달 TODO

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetRayHit.point), Time.deltaTime * 2f);
        }
    }

    private void PPP()
    {
        
    }



}
