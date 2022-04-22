using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� (�ü�)Ŭ����
/// </summary>
public class Archer : PlayerController
{
    [SerializeField, Tooltip("�߻�ü ���� ��ġ��")]
    GameObject SpawnPos;

    /// <summary>
    /// ���µ� Ÿ�ٰ��� ������Ų �߻�ü�� �Ѱ��ִ� ����
    /// </summary>
    public GameObject lockOnTarget;

    /// <summary>
    /// ���µ� ��ġ��
    /// </summary>
    public Vector3 hitPos;

    public static RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void NormalAttackState()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if(hit.point == null)
        {
            hit.point = Vector3.forward;
        }

        base.NormalAttackState();
    }

    /// <summary>
    /// �߻�ü�� �����ϴ� �޼ҵ�(Shoot �ִϸ��̼ǿ��� ���  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // �߻�ü ����
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath,Define.CharacterType.Projectile);
        // �߻�ü ���� ��ġ��, ���Ⱚ ����
        ResourceUtil.PosDirectionDesign(arrowObject, SpawnPos.transform.position, Quaternion.LookRotation(hit.point - transform.position)); 
        // �߻�ü �ش� �������� �̵� ����
        arrowObject.GetComponent<Arrow>().movePoint(hit.point);
        // �ش� �߻�ü�� �� ���� 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.AttackControllerInit(mainData, playerData.atkType);
    }
}
