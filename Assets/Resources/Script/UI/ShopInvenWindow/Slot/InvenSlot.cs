using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 슬롯 클래스
/// </summary>
public class InvenSlot : SlotBase
{
    
    /// <summary>
    /// 별도로 아이템 갯수를 조정해야할 경우 실행
    /// </summary>
    public void ItemCountSetting(UseItemData useItemData)
    {
        itemData.currentHandCount += useItemData.currentHandCount;
    }

    /// <summary>
    /// 인벤토리 저장데이터를 불러올시 사용되는 메서드
    /// </summary>
    /// <param name="invenSaveData"></param>
    public void ItemCountSetting(InvenSaveData invenSaveData)
    {
        itemData.currentHandCount = invenSaveData.currentHandCount;
    }

}
