using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� Ŭ����
/// </summary>
public class InvenSlot : SlotBase
{
    [SerializeField, Tooltip("�κ��丮 ������ ����")]
    TextMeshProUGUI itemCountText; 

    /// <summary>
    /// ������ ������ ������ �����ؾ��� ��� ����
    /// </summary>
    public void ItemCountSetting(UseItemData useItemData)
    {
        itemData.currentHandCount += useItemData.currentHandCount;
        TextCountAlpha();
    }    

    /// <summary>
    /// �κ��丮 ���嵥���͸� �ҷ��ý� ���Ǵ� �޼���
    /// </summary>
    /// <param name="invenSaveData"></param>
    public void ItemCountSetting(InvenSaveData invenSaveData)
    {
        itemData.currentHandCount = invenSaveData.currentHandCount;
        TextCountAlpha();
    }

    /// <summary>
    /// ������ ���� �ؽ�Ʈ ���İ� ����
    /// </summary>
    public void TextCountAlpha()
    {
        // ������ 0���� ũ�ٸ� ���� 1
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
