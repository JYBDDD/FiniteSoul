using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 슬롯 클래스
/// </summary>
public class InvenSlot : SlotBase
{
    [SerializeField, Tooltip("인벤토리 아이템 갯수")]
    TextMeshProUGUI itemCountText; 

    /// <summary>
    /// 별도로 아이템 갯수를 조정해야할 경우 실행
    /// </summary>
    public void ItemCountSetting(UseItemData useItemData)
    {
        itemData.currentHandCount += useItemData.currentHandCount;
        TextCountAlpha();
    }    

    /// <summary>
    /// 인벤토리 저장데이터를 불러올시 사용되는 메서드
    /// </summary>
    /// <param name="invenSaveData"></param>
    public void ItemCountSetting(InvenSaveData invenSaveData)
    {
        itemData.currentHandCount = invenSaveData.currentHandCount;
        TextCountAlpha();
    }

    /// <summary>
    /// 아이템 갯수 텍스트 알파값 조정
    /// </summary>
    public void TextCountAlpha()
    {
        // 갯수가 0보다 크다면 알파 1
        if (itemData?.currentHandCount > 0)
        {
            itemCountText.text = $"{itemData.currentHandCount}";
            itemCountText.color = new Color(itemCountText.color.r, itemCountText.color.g, itemCountText.color.b, 255f / 255f);
        }
        else
        {
            itemCountText.text = $"{itemData.currentHandCount}";
            itemCountText.color = new Color(itemCountText.color.r, itemCountText.color.g, itemCountText.color.b, 0f / 0f);
        }
    }
}
