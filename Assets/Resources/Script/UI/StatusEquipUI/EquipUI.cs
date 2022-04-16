using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��� UI �� ����ϴ� Ŭ����
/// </summary>
public class EquipUI : MonoBehaviour
{
    /// <summary>
    /// ������ ���Ե��� ����ִ� ����Ʈ
    /// </summary>
    [SerializeField]
    public List<EquipSlot> equipSlots = new List<EquipSlot>();

    /// <summary>
    /// equipSlots ����Ʈ�� ������ ������
    /// </summary>
    private List<EquipSlot> originSlots = new List<EquipSlot>();

    private void Update()
    {
        if (InGameManager.Instance.Player != null && originSlots != equipSlots)
        {
            ItemDataFind(InGameManager.Instance.Player.playerData.index);
            new EquipSlotDataList(equipSlots);
            originSlots = equipSlots;
        }

        ConsumptionDataChange();
    }


    /// <summary>
    /// �ش� ���� �������� ���� / ������ �Ұ����ϵ��� ����
    /// </summary>
    private void ItemDataFind(int playerIndex)
    {
        var itemDataList = GameManager.Instance.FullData.itemsData.Where(_ => _.jobIndex == playerIndex).ToList();

        for (int i = 0; i < itemDataList.Count; ++i)
        {
            // ������ �ִ� Ÿ���� �����ϴٸ� �ִ´�
            if(equipSlots[i].AbleToItemtype == itemDataList[i].itemType.ToString())
            {
                equipSlots[i].ItemDataSetting(itemDataList[i]);
            }

        }
    }

    /// <summary>
    /// �Һ������ �����Ͱ� ����Ǿ����� ����
    /// </summary>
    private void ConsumptionDataChange()
    {
        if(EquipSlotDataList.ChangeConsum == true)
        {
            equipSlots[2].ItemDataSetting(EquipSlotDataList.consumptionSlot.itemData);
            EquipSlotDataList.ChangeConsum = false;
        }
    }

}

/// <summary>
/// ���â�� ����ǰâ�� �����Ͱ��� ������ Ŭ����
/// </summary>
public class EquipSlotDataList
{
    /// <summary>
    /// ������͸� �ѱ�� ���Ǵ� ����Ʈ
    /// </summary>
    public static List<EquipSlot> equipSlotList;

    /// <summary>
    /// ��� �����۰��� �ѱ�� ���Ǵ� BoolŸ�� ����
    /// </summary>
    public static bool ChangeEquip;

    /// <summary>
    /// �Һ����͸� �ѱ�� ���Ǵ� ����
    /// </summary>
    public static EquipSlot consumptionSlot;

    /// <summary>
    /// �Һ� �����۰��� �ѱ�� ���Ǵ� BoolŸ�� ����
    /// </summary>
    public static bool ChangeConsum;


    public EquipSlotDataList(List<EquipSlot> equipSlots)
    {
        equipSlotList = equipSlots;
        ChangeEquip = true;
    }

    public EquipSlotDataList(EquipSlot consumSlot)
    {
        consumptionSlot = consumSlot;
        ChangeConsum = true;
    }
}
