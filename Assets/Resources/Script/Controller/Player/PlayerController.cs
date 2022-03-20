using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 플레이어 직업의 부모 클래스 
/// </summary>
public class PlayerController : MoveableObject
{
    public UsePlayerData playerData;

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


    /*protected void PlayerLookAtMouse()
    {


        if (isBoolRay)
        {
            transform.LookAt(hit.point);
            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;
            // 천천히 돌아보도록 Lerp 넣기 TODO
            
        }
    }*/

    /// <summary>
    /// 플레이어가 마우스위치를 바라보도록 하는 메서드
    /// </summary>
    IEnumerator PlayerLookRotate()
    {
        float _time = 0;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isBoolRay = Physics.Raycast(ray, out hit/*, mask*/);

        while (_time < 0.5f)
        {
            _time += Time.deltaTime;
            transform.LookAt(hit.point);
            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;
            // 돌리는 시간을 0.5초 정도로 주고 해당 시간만큼 Lerp로 0.1씩 돌리게 하면 될듯 TODO
        }



        yield return null;
    }


}
