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

    RaycastHit hit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void NormalAttackState()
    {
        // ���� ���콺 ��ġ�� Ray�� ���� ����, ���� �¾Ҵٸ� Ÿ���� �߻�ü�� Target������ �Ѱ��ش�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Monster") + 1 << LayerMask.NameToLayer("Default");
        if (Physics.Raycast(ray,out hit,layerMask))
        {
            // ���� �¾Ҵٸ� ����
            if(!hit.transform.CompareTag("Monster"))
            {
                // ��ġ�� ����
                hitPos = hit.point;
                // ���Ͱ� �ƴϴ��� ���� �ӽ÷� ����ִ´�
                lockOnTarget = hit.transform.gameObject;
            }
            // ���Ϳ� �¾Ҵٸ�
            if(hit.transform.CompareTag("Monster"))
            {
                // Ÿ�� ����
                lockOnTarget = hit.transform.gameObject;
                // ��ġ�� �ʱ�ȭ
                hitPos = Vector3.zero;
            }

        }
        // �ƴҰ�� Ÿ�ٰ��� Null �� ����
        else
        {
            lockOnTarget = null;
            hitPos = Vector3.zero;
        }

        base.NormalAttackState();

    }





    /// <summary>
    /// �߻�ü�� �����ϴ� �޼ҵ�(Shoot �ִϸ��̼ǿ��� ���  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // �߻�ü ����
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath, Resources.Load(Define.ProjectilePath.arrowPath).name);
        // �߻�ü ���� ��ġ��
        arrowObject.transform.position = SpawnPos.transform.position;
        // �߻�ü Forward ��
        var parentBowPos = SpawnPos.transform.parent.position;
        arrowObject.transform.rotation = Quaternion.LookRotation(new Vector3(parentBowPos.x,0,parentBowPos.z));
        // �߻�ü Ÿ�� ����(Ÿ���� null �ϰ�쵵 �߻�ü���� ����)
        arrowObject.GetComponent<Arrow>().TargetSetting(lockOnTarget,hitPos);
        // �ش� �߻�ü�� �� ���� 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.staticData = mainData;
        arrowAttackC.checkBool = true;

    }
}
