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
    [SerializeField,Tooltip("������ ���� ����� TMPro")]
    TextMeshProUGUI itemPrice;

    [SerializeField,Tooltip("�ݾ� �̹���")]
    Image priceImg;

    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public void ItemPriceSetting()
    {
        if(itemData?.index > 1000)
        {
            itemPrice.text = $"{itemData.salePrice}";
            ItemPriceAlphaSetting();
        }
        if(itemData?.index <= 1000)
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
        if (itemData?.index <= 1000)
        {
            itemPrice.color = new Color(priceColor.r, priceColor.g, priceColor.b, 0);
        }
        if (itemData?.index > 1000)
        {
            itemPrice.color = new Color(priceColor.r, priceColor.g, priceColor.b, 255f / 255f);
        }
    }
}
