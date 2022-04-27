using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �κ��丮 ���嵥���� Ŭ����
/// </summary>
[Serializable]
public class InvenSaveData : StaticData
{
    /// <summary>
    /// �κ��丮 �ε���
    /// </summary>
    public int invenIndex;

    /// <summary>
    /// ����ִ� �������� ����
    /// </summary>
    public int currentHandCount;

    /// <summary>
    /// �κ��丮 ������ ����� ���Ǵ� ������
    /// </summary>
    /// <param name="invenIndex"></param>
    /// <param name="invenSlotData"></param>
    public InvenSaveData(int invenIndex,InvenSlot invenSlotData)
    {
        index = invenSlotData.itemData.index;
        this.invenIndex = invenIndex;
        currentHandCount = invenSlotData.itemData.currentHandCount;
    }

    /// <summary>
    /// �κ��丮 ������ �ʱ�ȭ�� ���Ǵ� ������
    /// </summary>
    /// <param name="returnData"></param>
    public InvenSaveData(int returnData)
    {
        index = returnData;
        invenIndex = 0;
        currentHandCount = 0;
    }
}
