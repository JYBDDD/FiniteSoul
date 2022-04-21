using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߻�ü(ȭ��)�� Ŭ����
/// </summary>
[RequireComponent(typeof(AttackController))]
public class Arrow : ProjectileBase
{
    /// <summary>
    /// �߻�ü(ȭ��)�� ��ü
    /// </summary>
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // �߷°� �ʱ�ȭ
        rigid.useGravity = false;

        rigid.AddForce(InGameManager.Instance.Player.transform.forward * 50f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���Ϳ��� �¾Ҵٸ� ����
        if(other.gameObject.CompareTag("Monster"))
        {
            // �߷°� ����
            rigid.useGravity = true;

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // �߷°� ����
            rigid.useGravity = true;

            // �ѹ� ������ üũ���� �ʵ��� ����
            gameObject.GetComponent<AttackController>().checkBool = false;

            // 3�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(3f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���Ϳ��� �¾Ҵٸ� ����
        if (other.gameObject.CompareTag("Monster"))
        {
            // �߷°� ����
            rigid.useGravity = true;

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // �߷°� ����
            rigid.useGravity = true;

            // �ѹ� ������ üũ���� �ʵ��� ����
            gameObject.GetComponent<AttackController>().checkBool = false;

            // 3�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(3f));
        }
    }

    /// <summary>
    /// �־��� �ð���ŭ �߻�ü Ȱ��ȭ
    /// </summary>
    /// <param name="checkTime">�ش� �ð���ŭ Ȱ��ȭ</param>
    /// <returns></returns>
    IEnumerator ArrowCheckTime(float checkTime)
    {
        float time = 0;
        bool notCheck = false;

        while(true)
        {
            if(time > checkTime)
            {
                // ������Ʈ ��ȯ
                ReturnObject();
                yield break;
            }

            if(time > 0.3f && notCheck == false)
            {
                // �ѹ� ������ üũ���� �ʵ��� ����
                gameObject.GetComponent<AttackController>().checkBool = false;
                notCheck = true;
            }

            time += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// �ش� ������Ʈ Ǯ���Ŵ����� ��ȯ
    /// </summary>
    private void ReturnObject()
    {
        ObjectPoolManager.Instance.GetPush(gameObject, ObjectPoolManager.ParentProjectile.transform);
    }
}
