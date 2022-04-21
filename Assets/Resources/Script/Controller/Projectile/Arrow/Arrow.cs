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

    /// <summary>
    /// Ÿ�ٿ� ��Ҵ��� üũ
    /// </summary>
    bool hitCheck = false;

    AttackController attackController;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        attackController = GetComponent<AttackController>();
    }

    private void OnEnable()
    {
        // �߷°� �ʱ�ȭ
        rigid.useGravity = false;
        // ������ ������ ����
        rigid.isKinematic = true;

        // Ÿ�� üũ Bool�� �缳��
        hitCheck = false;

        //rigid.AddForce(InGameManager.Instance.Player.transform.forward * 50f, ForceMode.Impulse);     TODO ����

    }

    public void movePoint(Vector3 hitPoint)
    {
        StartCoroutine(AccelArrow(hitPoint));
    }

    IEnumerator AccelArrow(Vector3 hitPoint)
    {
        while(true)
        {
            // Ÿ�ٿ� �������¶�� ����
            if(hitCheck == true)
            {
                yield break;
            }

            // �Ÿ��� �ٸ����� ������ �ӷ����� ���
            transform.position = Vector3.MoveTowards(transform.position, hitPoint, 0.3f);


            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���Ϳ��� �¾Ҵٸ� ����
        if(other.gameObject.CompareTag("Monster"))
        {
            // �߷°� ����
            rigid.useGravity = true;
            // ������ ���� ����
            rigid.isKinematic = false;

            // Ÿ�� üũ
            hitCheck = true;

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Untagged"))
        {
            // �߷°� ����
            rigid.useGravity = true;
            // ������ ���� ����
            rigid.isKinematic = false;

            // Ÿ�� üũ
            hitCheck = true;

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

        while(true)
        {
            if(time > checkTime)
            {
                // ������Ʈ ��ȯ
                ReturnObject();
                yield break;
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
