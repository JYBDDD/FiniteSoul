using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �����̴� ��ü�� �θ� Ŭ����
/// </summary>
public class MoveableObject : MonoBehaviour
{
    public StaticData mainData;

    // ĳ���� ����
    public Define.State State = Define.State.Idle;

    // ĳ���� �ݶ��̴�
    protected Collider coll;

    // ĳ���� ����
    protected Rigidbody rigid;

    // ĳ���� �ִϸ�����
    protected Animator anim;

    /// <summary>
    /// �ش� ĳ���Ϳ� �´� ������ �ʱ�ȭ
    /// </summary>
    public virtual void Initialize()
    {
        // ������ �Ǿ����� �ʴٸ� �߰�
        coll ??= GetComponent<Collider>();
        rigid ??= GetComponent<Rigidbody>();
        anim ??= GetComponent<Animator>();

        SetStat();
    }

    /// <summary>
    /// ���� ����� ����� ������ �缳�� ���ִ� �޼ҵ�
    /// </summary>
    public virtual void SetStat()
    {
        



    }
}