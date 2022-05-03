using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ǰ UI Ŭ����
/// </summary>
public class BelongingsUI : MonoBehaviour
{
    /// <summary>
    /// ����ǰ ���� ����Ǿ����� ���Ǵ� ���� ����
    /// </summary>
    public static BelongingsUI TempInstance;

    [SerializeField,Tooltip("�Һ� ������ �̸�")]
    private TextMeshProUGUI consumptionText;

    [SerializeField, Tooltip("����ǰâ�� �����͸� �������ִ� ����Ʈ")]
    public List<EquipSlot> belongingsList = new List<EquipSlot>();

    /// <summary>
    /// ������ ����ǰ ������ ����Ʈ
    /// </summary>
    private List<EquipSlot> originList = new List<EquipSlot>();

    /// <summary>
    /// �Һ� ������ ����� ȣ������� ���� ����
    /// </summary>
    private int consumCount = -1;

    /// <summary>
    /// ���� ����ǰ â�� ������� �κ��丮 ���Կ� ����� �Һ�ǰ ������
    /// </summary>
    private InvenSlot useConsumSlot;

    private void Awake()
    {
        TempInstance = this;
    }

    private void Update()
    {
        // �����Ͱ� �����Ǿ��ٸ� ����
        if(originList != belongingsList)    // -> ���â�� �÷��̾��� �����Ͱ� �Էµ� ���� ����ȴ�
        {
            BelongingsListInsert();
        }
    }

    /// <summary>
    /// ����ǰ������ ���â�����Ϳ� ���� (�ʱ� ���۽ÿ��� ȣ��Ǵ� �޼���)
    /// </summary>
    /// <param name="count"></param>
    private void BelongingsListInsert()
    {
        if (EquipSlotDataList.ChangeEquip == true)
        {
            for (int i = 0; i < EquipSlotDataList.equipSlotList.Count; ++i)
            {
                belongingsList[i].ItemDataSetting(EquipSlotDataList.equipSlotList[i].itemData);
            }

            ConsumptionDataInsert();
            originList = belongingsList;
            EquipSlotDataList.ChangeEquip = false;
        }

    }

    /// <summary>
    /// �Һ������ �ʱⵥ���� ����
    /// </summary>
    private void ConsumptionDataInsert()
    {
        // �κ��丮�� �Һ������ ����� �� ����
        var consumData = ConsumDataChange();

        // ���� Null���� �ƴ϶�� ����
        if(consumData != null)
            return;

        // �κ��丮�� �Һ� �������� �������� ���� ��� ����
        var consumptionData = GameManager.Instance.FullData.itemsData.Where(_ => _.itemType == Define.ItemMixEnum.ItemType.Consumption).FirstOrDefault();
        ImgAndTextChange(consumptionData,1);



    }

    /// <summary>
    /// �̹��� �� ���� �ؽ�Ʈ, ������ ����
    /// </summary>
    /// <param name="useItemData"></param>
    private void ImgAndTextChange(UseItemData useItemData, int basicCount = 0)
    {
        // �Һ������ ������ ����
        belongingsList[2].ItemDataSetting(useItemData);
        consumptionText.text = belongingsList[2].itemData.name;

        // ���� ���� ���� ����
        TextMeshProUGUI consumptionCount = belongingsList[2].GetComponentInChildren<TextMeshProUGUI>();
        consumptionCount.text = $"{belongingsList[2].itemData.currentHandCount - basicCount}";

        new EquipSlotDataList(belongingsList[2]);
    }

    /// <summary>
    /// �Һ�ǰ ������ ���� ����Ǿ��� ��� ȣ��Ǵ� �޼���
    /// </summary>
    public UseItemData ConsumDataChange()  
    {
        var invenConsumData = ShopInvenWindowUI.InventoryDataFind(Define.ItemMixEnum.ItemType.Consumption);
        if (invenConsumData.Count > 0)
        {
            // ȣ��Ǵ� ī��Ʈ�� �Һ�������� ����Ʈ ������ �Ѿ�ٸ� 0���� �ʱ�ȭ��, 0��° ���������� �ǵ����� 
            if (invenConsumData.Count <= consumCount + 1)
            {
                consumCount = 0;

                // �����Ͱ� �κ��丮�� ����
                belongingsList[2].itemData = invenConsumData[consumCount].itemData;
                ImgAndTextChange(invenConsumData[consumCount].itemData);
                useConsumSlot = invenConsumData[consumCount];
                return invenConsumData[consumCount].itemData;
            }
            else
            {
                ++consumCount;

                // �����Ͱ� �κ��丮�� ����
                belongingsList[2].itemData = invenConsumData[consumCount].itemData;
                ImgAndTextChange(invenConsumData[consumCount].itemData);
                useConsumSlot = invenConsumData[consumCount];
                return invenConsumData[consumCount].itemData;
            }
        }

        return null;
    }

    /// <summary>
    /// �κ��丮���� �Һ������ ������ ���Ǵ� �޼���
    /// </summary>
    public void InvenConsumDataChange()
    {
        var invenConsumData = ShopInvenWindowUI.InventoryDataFind(Define.ItemMixEnum.ItemType.Consumption);
        if (invenConsumData.Count > 0)
        {
            // �����Ͱ� �κ��丮�� ����
            belongingsList[2].itemData = invenConsumData[consumCount].itemData;
            ImgAndTextChange(invenConsumData[consumCount].itemData);
            useConsumSlot = invenConsumData[consumCount];
        }
    }

    /// <summary>
    /// �Һ� ������ ��� �޼���
    /// </summary>
    /// <returns></returns>
    public UseItemData ConsumUse()
    {
        // �ش� �Һ�â�� ������ ������ 1�� �̻��� ��� ��� ����
        if(belongingsList[2].itemData.currentHandCount >= 1)
        {
            // �κ��丮 ����ǰ�����͸� �Һ�ǰâ�� �����۰� ����
            belongingsList[2].itemData = useConsumSlot.itemData;

            // ���� ����
            --belongingsList[2].itemData.currentHandCount;

            // �κ��丮 ������ �̹���, �ؽ�Ʈ �缳��
            useConsumSlot.ImageDataSetting(belongingsList[2].itemData);
            useConsumSlot.TextCountAlpha();

            // ����ǰ ������, �ؽ�Ʈ �缳��
            ImgAndTextChange(belongingsList[2].itemData);
            return belongingsList[2].itemData;
        }

        return null;
    }

    /// <summary>
    /// �Һ�������� ��밡������ üũ�ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public bool ConsumBool()
    {
        if(belongingsList[2].itemData.currentHandCount > 0)
        {
            return true;
        }

        return false;
    }
}
