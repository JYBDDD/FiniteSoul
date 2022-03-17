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

        // ���� �� �÷��̾� �ڽĿ� ����ī�޶� �������� �ʴ´ٸ� ����ī�޶� �־��� �� TODO
        //  -> ���� �ӽ������� �ȶ�򿡰� �־���

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

        Vector3 posVec = new Vector3(floatX, 0, floatZ).normalized * Time.deltaTime;

        transform.Translate(posVec.x, 0, posVec.z);
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


}
