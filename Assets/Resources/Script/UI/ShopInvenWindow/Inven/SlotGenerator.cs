using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ Ŭ����
/// </summary>
public class SlotGenerator : MonoBehaviour
{

    private void Start()
    {
        // �κ��丮 ���� 42���� ����� ����Ʈ�� ����
        for(int i = 0; i < 42; ++i)
        {
            // �κ��丮 ���� ����
            GameObject invenObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.invenSlotPath);
            // �κ��丮 ���� ��ġ ����
            invenObj.transform.SetParent(transform, false);
            // �κ��丮 ���� ������ �� �̹��� ����
            invenObj.GetComponent<InvenSlot>().ImageDataSetting();
        }
    }
}
