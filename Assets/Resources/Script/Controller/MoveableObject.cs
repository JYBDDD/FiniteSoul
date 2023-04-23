using Util.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �����̴� ��ü�� �θ� Ŭ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]   // Rigidbody �� �ȳִ� ��� ����
public class MoveableObject : MonoBehaviour,RecyclePooling
{
    public StaticData mainData;

    // ĳ����, ���� ���� �ӽ�
    public StateMachine<Define.State> FSM = new StateMachine<Define.State>();

    // ĳ����,���� �ݶ��̴�
    protected Collider coll;

    // ĳ����,���� ����
    protected Rigidbody rigid;

    // ĳ����,���� �ִϸ�����
    protected Animator anim;

    // AttackController ����
    protected AttackController Ac;

    // ������ �� �ִ��� üũ
    public bool NotToMove = true;

    /// <summary>
    /// �ش� ĳ���Ϳ� �´� ������ �ʱ�ȭ
    /// </summary>
    public virtual void Initialize(StaticData staticData)
    {
        mainData = staticData;
    }

    /// <summary>
    /// �ʿ��� ������Ʈ �� �߰� (��� �ڽ��� ���) / ���� �ʿ��� ����� override �ؼ� ���
    /// </summary>
    public virtual void InsertComponent()
    {
        // ������ �Ǿ����� �ʴٸ� �߰�
        coll ??= GetComponent<Collider>();
        rigid ??= GetComponent<Rigidbody>();
        anim ??= GetComponent<Animator>();
    }

    /// <summary>
    /// �������� Collider ����
    /// </summary>
    public virtual void AttackColliderSet() {}
}