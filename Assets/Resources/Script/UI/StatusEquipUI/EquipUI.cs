using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비 UI 만 담당하는 클래스
/// </summary>
public class EquipUI : MonoBehaviour
{
    /// <summary>
    /// 아이템 슬롯들을 들고있는 리스트
    /// </summary>
    [SerializeField]
    public List<EquipSlot> equipSlots = new List<EquipSlot>();

    /// <summary>
    /// equipSlots 리스트의 변경전 데이터
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
    /// 해당 시작 아이템은 장착 / 해제가 불가능하도록 설정
    /// </summary>
    private void ItemDataFind(int playerIndex)
    {
        var itemDataList = GameManager.Instance.FullData.itemsData.Where(_ => _.jobIndex == playerIndex).ToList();

        for (int i = 0; i < itemDataList.Count; ++i)
        {
            // 넣을수 있는 타입이 동일하다면 넣는다
            if(equipSlots[i].AbleToItemtype == itemDataList[i].itemType.ToString())
            {
                equipSlots[i].ItemDataSetting(itemDataList[i]);
            }

        }
    }

    /// <summary>
    /// 소비아이템 데이터가 변경되었을시 변경
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
/// 장비창과 소지품창의 데이터값을 전달할 클래스
/// </summary>
public class EquipSlotDataList
{
    /// <summary>
    /// 장비데이터를 넘길시 사용되는 리스트
    /// </summary>
    public static List<EquipSlot> equipSlotList;

    /// <summary>
    /// 장비 아이템값을 넘길시 사용되는 Bool타입 변수
    /// </summary>
    public static bool ChangeEquip;

    /// <summary>
    /// 소비데이터를 넘길시 사용되는 변수
    /// </summary>
    public static EquipSlot consumptionSlot;

    /// <summary>
    /// 소비 아이템값을 넘길시 사용되는 Bool타입 변수
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
