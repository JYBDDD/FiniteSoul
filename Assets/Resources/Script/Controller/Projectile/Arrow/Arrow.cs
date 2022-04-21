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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
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
        StartCoroutine(AccelArrow());
    }

    IEnumerator AccelArrow()
    {
        var playerForward = InGameManager.Instance.Player.transform.forward;

        Ray ray = Camera.main.ScreenPointToRay(playerForward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        while(true)
        {
            // Ÿ�ٿ� �������¶�� ����
            if(hitCheck == true)
            {
                yield break;
            }

            // �Ÿ��� �ٸ����� ������ �ӵ��� ��µɼ� �ֵ��� ���� �ʿ� TODO
            transform.position = Vector3.Lerp(playerForward, hit.point, Time.deltaTime);


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

            // Ÿ�� üũ
            hitCheck = true;

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // �߷°� ����
            rigid.useGravity = true;

            // Ÿ�� üũ
            hitCheck = true;

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
            // ������ ���� ����
            rigid.isKinematic = false;

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // �߷°� ����
            rigid.useGravity = true;
            // ������ ���� ����
            rigid.isKinematic = false;

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
