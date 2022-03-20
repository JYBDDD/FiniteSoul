using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �÷��̾� ������ �θ� Ŭ���� 
/// </summary>
public class PlayerController : MoveableObject
{
    public UsePlayerData playerData;

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

    #region ĳ���� ������ ������
    private void Move()
    {
        // �̵� �� ����������� TODO
        float floatX = Input.GetAxisRaw("Horizontal") * 3f;
        float floatZ = Input.GetAxisRaw("Vertical") * 3f;

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized;

        transform.Translate(posVec * Time.deltaTime);
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


    /*protected void PlayerLookAtMouse()
    {


        if (isBoolRay)
        {
            transform.LookAt(hit.point);
            var tempRot = transform.eulerAngles;
            tempRot.x = 0;
            tempRot.z = 0;
            transform.eulerAngles = tempRot;
            // õõ�� ���ƺ����� Lerp �ֱ� TODO
            
        }
    }*/

    /// <summary>
    /// �÷��̾ ���콺��ġ�� �ٶ󺸵��� �ϴ� �޼���
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
            // ������ �ð��� 0.5�� ������ �ְ� �ش� �ð���ŭ Lerp�� 0.1�� ������ �ϸ� �ɵ� TODO
        }



        yield return null;
    }


}
