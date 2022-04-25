using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ Ŭ����
/// </summary>
public class SlotGenerator : MonoBehaviour
{
    // ���� Ÿ���� �������ִ� StringŸ�� ����
    [SerializeField]
    private string SlotType;


    private void Start()
    {
        // ���� Ÿ���� Inven�ϰ�� ���� (�κ��丮)
        if(SlotType == "Inven")
        {
            // �κ��丮 ���� 42���� ����� ����Ʈ�� ����
            for (int i = 0; i < 42; ++i)
            {
                // �κ��丮 ���� ����
                GameObject invenObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.invenSlotPath);
                // �κ��丮 ���� ��ġ ����
                invenObj.transform.SetParent(transform, false);
                // �κ��丮 ���� ������ �� �̹��� ����
                InvenSlot invenSlot = invenObj.GetComponent<InvenSlot>();
                invenSlot.ImageDataSetting();
                // �κ� ���� ����Ʈ ����
                ShopInvenWindowUI.Inventory.Add(invenSlot);

            }
        }
        // ���� Ÿ���� Shop�ϰ�� ���� (����)
        if(SlotType == "Shop")
        {
            // ���� ������ ������(Etc�� �ƴ� ������)�� �̸� ���� �´�
            var allData = GameManager.Instance.FullData.itemsData;

            // �����ų ������ ������
            List<UseItemData> ItemData = new List<UseItemData>();
            for (int i = 0; i < allData.Count; ++i)
            {
                // ������ Ÿ���� ��Ÿ Ÿ���̶�� �� �̻���
                if (allData[i].itemType == Define.ItemMixEnum.ItemType.Etc) { }
                // ��Ÿ Ÿ���� �ƴ� ������ ������ ����
                else
                {
                    ItemData.Add(new UseItemData(allData[i]));
                }
            }

            // ���� ���� 15���� ����� ����Ʈ�� ����
            for (int i = 0; i < 15; ++i)
            {
                // ���� ���� ����
                GameObject shopObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.shopSlotPath);
                // ���� ���� ��ġ ����
                shopObj.transform.SetParent(transform, false);
                // ���� ���� ������ �� �̹��� ����
                ShopSlot shopSlot = shopObj.GetComponent<ShopSlot>();
                if (ItemData.Count > i) // -> �ش� �����۵����Ͱ� �����Ѵٸ� ����
                    shopSlot.ImageDataSetting(ItemData[i]);
                else                    // -> �ƴ϶�� Null
                    shopSlot.ImageDataSetting();

                shopSlot.ItemPriceSetting();
                // ���� ���� ����Ʈ ����
                ShopInvenWindowUI.Shop.Add(shopSlot);
            }
        }

    }
}
