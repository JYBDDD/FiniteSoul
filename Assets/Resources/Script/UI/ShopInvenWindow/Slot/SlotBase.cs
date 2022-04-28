using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� ������ ���̽�
/// </summary>
public class SlotBase : MonoBehaviour
{
    /// <summary>
    /// ������ �����͸� ��Ƶ� ����
    /// </summary>
    public UseItemData itemData = new UseItemData();

    [SerializeField,Tooltip("������ �̹����� ��½����� �ڽ� �̹���")]
    protected Image itemImg;

    /// <summary>
    /// ���� ������ �����Ͱ� ���´ٸ� �̹����� ������ �޼ҵ� (ȣ���)
    /// </summary>
    public void ImageDataSetting(UseItemData useItemData = null)
    {
        // �������� �����Ͱ� ��������� ������ ���� �� �̹��� ����
        if (useItemData != null)
        {
            itemData = new UseItemData(useItemData);

            itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        }

        ItemAlphaSet(useItemData,itemImg);
    }

    /// <summary>
    /// ������ �̹��� ���İ� ����
    /// </summary>
    protected void ItemAlphaSet(UseItemData useItemData,Image setImage)
    {
        var itemColor = setImage.color;
        if (useItemData?.index < 1000)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 0f / 0f);
        }
        if (useItemData?.index > 1000)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 255f / 255f);
        }

    }
}
