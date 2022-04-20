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
        arrowObject.transform.position = SpawnPos.transform.position;
        // �߻�ü Forward ��
        // TODO
    }
}
