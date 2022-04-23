using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� Ŭ����
/// </summary>
public class InvenSlot : MonoBehaviour
{
    /// <summary>
    /// ������ �����͸� ��Ƶ� ����
    /// </summary>
    [HideInInspector]
    public UseItemData itemData = null;

    /// <summary>
    /// ������ �̹����� ��½����� �ڽ� �̹���
    /// </summary>
    Image itemImg;

    private void Awake()
    {
        itemImg = GetComponentInChildren<Image>();
    }

    /// <summary>
    /// ���� ������ �����Ͱ� ���´ٸ� �̹����� ������ �޼ҵ� (ȣ���)
    /// </summary>
    public void ImageDataSetting(UseItemData useItemData = null)
    {
        // ���� �����Ͱ��� Null �ϰ��, Null�� ������ ����
        if(useItemData == null)
        {
            itemData = null;
        }

        // �������� �����Ͱ� ��������� ������ ���� �� �̹��� ����
        if(useItemData != null)
        {
            itemData = new UseItemData(useItemData);

            // ������ �����Ͱ� Null�� �ƴҰ�� �̹��� ����
            if (itemData != null)
            {
                itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
                ItemAlphaSet(itemData);
            }
            if (itemData == null)
            {
                itemImg.sprite = null;
                ItemAlphaSet(itemData);
            }
        }
        
    }

    /// <summary>
    /// ������ �̹��� ���İ� ����
    /// </summary>
    private void ItemAlphaSet(UseItemData useItem)
    {
        var itemColor = itemImg.color;
        if(useItem == null)
        {
            itemImg.color = new Color(itemColor.r, itemColor.g, itemColor.b, 0);
        }
        if(useItem != null)
        {
            itemImg.color = new Color(itemColor.r, itemColor.g, itemColor.b, 255f / 255f);
        }

    }

}
