using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상점 슬롯 클래스
/// </summary>
public class ShopSlot : SlotBase
{
    /// <summary>
    /// 아이템 값을 출력할 TMPro
    /// </summary>
    TextMeshProUGUI itemPrice;

    [SerializeField,Tooltip("금액 이미지")]
    Image priceImg;

    /// <summary>
    /// 아이템 가격 셋팅
    /// </summary>
    public void ItemPriceSetting()
    {
        itemPrice ??= GetComponentInChildren<TextMeshProUGUI>();

        if(itemData != null)
        {
            itemPrice.text = $"{itemData.salePrice}";
            ItemPriceAlphaSetting();
            ItemAlphaSet(priceImg);
        }
        if(itemData == null)
        {
            ItemPriceAlphaSetting();
            ItemAlphaSet(priceImg);
        }

    }

    /// <summary>
    /// 아이템 가격의 알파값 셋팅
    /// </summary>
    private void ItemPriceAlphaSetting()
    {
        var priceColor = itemPrice.color;
        if (itemData == null)
        {
            itemPrice.color = new Color(priceColor.r, priceColor.g, priceColor.b, 0);
        }
        if (itemData != null)
        {
            itemPrice.color = new Color(priceColor.r, priceColor.g, priceColor.b, 255f / 255f);
        }
    }
}
