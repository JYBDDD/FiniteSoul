using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� Ŭ����
/// </summary>
public class ShopSlot : SlotBase
{
    /// <summary>
    /// ������ ���� ����� TMPro
    /// </summary>
    TextMeshProUGUI itemPrice;

    [SerializeField,Tooltip("�ݾ� �̹���")]
    Image priceImg;

    /// <summary>
    /// ������ ���� ����
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
    /// ������ ������ ���İ� ����
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
