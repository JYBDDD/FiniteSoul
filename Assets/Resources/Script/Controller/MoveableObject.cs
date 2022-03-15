using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �����̴� ��ü�� �θ� Ŭ����
/// </summary>
public class MoveableObject : MonoBehaviour
{
    // �Ѱ� ������


    // ĳ���� �ݶ��̴�
    protected Collider coll;

    // ĳ���� ����
    protected Rigidbody rigid;

    /// <summary>
    /// �ش� ĳ���Ϳ� �´� ������ �ʱ�ȭ
    /// </summary>
    public virtual void Initialize()
    {
        
    }

    protected virtual void Awake()
    {
        coll = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
    }
}
