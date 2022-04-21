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



    /// <summary>
    /// �߻�ü�� �����ϴ� �޼ҵ�(Shoot �ִϸ��̼ǿ��� ���  -> Animation Event)
    /// </summary>
    public void ArrowSpawn()
    {
        // �߻�ü ����
        var arrowObject = ObjectPoolManager.Instance.GetPool<Arrow>(Define.ProjectilePath.arrowPath, Resources.Load(Define.ProjectilePath.arrowPath).name);
        // �߻�ü ���� ��ġ��
        arrowObject.transform.position = SpawnPos.transform.position + InGameManager.Instance.Player.transform.forward;
        // �߻�ü Forward ��
        var parentBowPos = InGameManager.Instance.Player.transform.position;
        arrowObject.transform.rotation = Quaternion.LookRotation(new Vector3(parentBowPos.x,0,parentBowPos.z));
        // �ش� �߻�ü�� �� ���� 
        var arrowAttackC = arrowObject.GetComponent<AttackController>();
        arrowAttackC.staticData = mainData;
        arrowAttackC.checkBool = true;

    }
}
