using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��񽽷� Ŭ����
/// </summary>
public class EquipSlot : MonoBehaviour
{
    /// <summary>
    /// �ش� ���Կ� ������ �ִ� ������ Ÿ��
    /// </summary>
    [SerializeField]
    public string AbleToItemtype;

    /// <summary>
    /// ������ �������� �̹���
    /// </summary>
    Image itemImg;

    /// <summary>
    /// ������ �����͸� ����ִ� ����
    /// </summary>
    public UseItemData itemData;

    private void Start()
    {
        ImgAbleSetting();
    }

    /// <summary>
    /// �ش� �����Ͱ� ������ �ƴ϶�� �ش� �̹����� ��Ȱ��ȭ / �ƴ϶�� Ȱ��ȭ
    /// </summary>
    public void ImgAbleSetting()
    {
        itemImg ??= GetComponent<Image>();

        if (itemData.index >= 1001)
        {
            itemImg.enabled = true;
            itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        }
        if (itemData.index < 1001)
        {
            itemImg.enabled = false;
        }
    }

    /// <summary>
    /// ������ �����͸� �����Ѵ�
    /// </summary>
    public void ItemDataSetting(UseItemData itemData)
    {
        this.itemData = new UseItemData(itemData);
        ImgAbleSetting();
    }
}
