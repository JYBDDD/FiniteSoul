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
    /// �i�� ���� Ÿ�� (MoveableObject ���)
    /// </summary>
    public GameObject Target;

    /// <summary>
    /// �ش� Ÿ�꿡 �¾Ҵ��� Ȯ���ϴ� ��
    /// </summary>
    bool hitTarget;

    private void OnEnable()
    {
        // Ÿ�� üũ�� �ʱ�ȭ
        hitTarget = false;
    }

    /// <summary>
    /// Ÿ�ټ���
    /// </summary>
    /// <param name="target">���� Ÿ��(���Ͷ��)</param>
    /// <param name="hitPos">(���̶��) Vec ��ġ</param>
    public void TargetSetting(GameObject target,Vector3 hitPos)
    {
        if(target == null)
        {
            Target = null;

            // RigidBody.AddForce�� �������� ����
            StartCoroutine(NullTarget());

        }
        if(target != null)
        {
            // Ÿ�� ����
            Target = target;

            StartCoroutine(LockTargetTracking(target,hitPos));
        }

        IEnumerator LockTargetTracking(GameObject target,Vector3 hitPos)
        {
            // ���� �¾���   -> hitPos������ ȭ�� ��ô
            if(hitPos != Vector3.zero)
            {
                while (true)
                {
                    // Ÿ�ٿ� �¾Ҵٸ� ����
                    if (hitTarget == true)
                    {
                        yield break;
                    }

                    // Ÿ���� ���� ȸ��
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(hitPos), Time.deltaTime * 3f);

                    // Ÿ���� ���� �̵�
                    transform.position = Vector3.Lerp(transform.position, hitPos, Time.deltaTime * 5f);

                    yield return null;
                }
            }

            // ���Ϳ� �¾��� -> target���� ����
            if(hitPos == Vector3.zero)
            {
                while (true)
                {
                    // Ÿ�ٿ� �¾Ҵٸ� ����
                    if(hitTarget == true)
                    {
                        yield break;
                    }

                    // Ÿ���� ���� ȸ��
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.deltaTime * 3f);

                    // Ÿ���� ���� �̵�
                    transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 5f);

                    yield return null;
                }
            }

            yield return null;
        }

        IEnumerator NullTarget()
        {
            float duraction = 3f;
            float time = 0;

            // Ÿ���� ���� �̵�
            transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10f, ForceMode.Impulse);

            while (true)
            {
                if(time > duraction)
                {
                    // �߻�ü Ǯ���Ŵ����� ��ȯ
                    ReturnObject();
                    yield break;
                }

                time += Time.deltaTime;

                yield return null;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // ���Ϳ��� �¾Ҵٸ� ����
        if(other.gameObject.CompareTag("Monster"))
        {
            // Ÿ�ٿ� �¾����Ƿ� Ȱ��ȭ
            hitTarget = true;

            // �ش� ������ �ڽ����� ������ ȭ����ġ�� ����
            gameObject.transform.SetParent(other.gameObject.transform);

            // 20�� Ȱ��ȭ�� Ǯ���Ŵ��� ����
            StartCoroutine(ArrowCheckTime(20f));
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // Ÿ�ٿ� �¾����Ƿ� Ȱ��ȭ
            hitTarget = true;

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

            if(time > 0.1f && notCheck == false)
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
