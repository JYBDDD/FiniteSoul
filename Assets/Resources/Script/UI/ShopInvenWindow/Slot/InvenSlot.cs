using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� Ŭ����
/// </summary>
public class InvenSlot : SlotBase
{
    
    /// <summary>
    /// ������ ������ ������ �����ؾ��� ��� ����
    /// </summary>
    public void ItemCountSetting(UseItemData useItemData)
    {
        itemData.currentHandCount += useItemData.currentHandCount;
    }

    /// <summary>
    /// �κ��丮 ���嵥���͸� �ҷ��ý� ���Ǵ� �޼���
    /// </summary>
    /// <param name="invenSaveData"></param>
    public void ItemCountSetting(InvenSaveData invenSaveData)
    {
        itemData.currentHandCount = invenSaveData.currentHandCount;
    }

}
