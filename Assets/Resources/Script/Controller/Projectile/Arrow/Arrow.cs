using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �߻�ü(ȭ��)�� Ŭ����
/// </summary>
public class Arrow : ProjectileBase
{
    public GameObject Target = null;
    Vector3 dir;

    private void OnEnable()
    {
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //var angle = Mathf.Atan2(dir.z - transform.position.z, dir.x - transform.position.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        // ���Ⱚ ���� �ʿ��� TODO

    }

    private void Update()
    {
        //gameObject.GetComponent<Transform>().transform.Translate(dir * 2f * Time.deltaTime);
    }

    /// <summary>
    /// Ÿ�ټ���
    /// </summary>
    public void TargetSetting(GameObject target = null)
    {
        if(target == null)
        {
            // �߻�� Arrow�� �ӽ÷� �÷��̾��� �չ��Ⱚ�� �޾ƿ� �չ������� ���̰� ����, ���ư��°��� Vector3.forward ������ �ٽ� �ٲ۴� TODO
        }
        if(target != null)
        {
            // Ÿ�� ����
            Target = target;

            // �ش� ���� ���� ����� ȸ���ϸ� ���ư���  TODO
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���Ϳ��� �¾Ҵٸ� ����
        if(other.gameObject.CompareTag("Monster"))
        {
            // �ش� ������ �ڽ����� ������ ȭ����ġ�� ���� TODO

            // ������ 20�ʵ� ȭ���� ReturnObject()�� ��ȯ��Ű�� �ڷ�ƾ ���� TODO
        }
        // �ٸ� ���� �¾Ҵٸ� ����
        else
        {
            // ƨ���� 3�ʵ� ReturnObject() ��ȯ ��ų�� TODO
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
