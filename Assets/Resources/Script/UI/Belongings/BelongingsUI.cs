using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 소지품 UI 클래스
/// </summary>
public class BelongingsUI : MonoBehaviour
{
    // 깃허브 4.6.4 에 연동해서 작성할것 적어놓음 TODO

    /// <summary>
    /// 소비 아이템 이름
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI consumptionText;

    /// <summary>
    /// 소지품창의 데이터를 가지고있는 리스트
    /// </summary>
    [SerializeField]
    public List<EquipSlot> belongingsList = new List<EquipSlot>();

    /// <summary>
    /// 변경전 소지품 데이터 리스트
    /// </summary>
    private List<EquipSlot> originList = new List<EquipSlot>();


    private void Update()
    {
        // 데이터가 변동되었다면 실행
        if(originList != belongingsList)
        {
            BelongingsListInsert();
        }
    }

    /// <summary>
    /// 소지품슬롯을 장비창데이터와 연동
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
    /// 소비아이템 초기데이터 삽입
    /// </summary>
    private void ConsumptionDataInsert()
    {
        var consumptionData = GameManager.Instance.FullData.itemsData.Where(_ => _.itemType == Define.ItemMixEnum.ItemType.Consumption).FirstOrDefault();

        // 소비아이템 데이터 삽입
        belongingsList[2].ItemDataSetting(consumptionData);
        consumptionText.text = belongingsList[2].itemData.name;

        // 현재 소지 개수 설정
        TextMeshProUGUI consumptionCount = belongingsList[2].GetComponentInChildren<TextMeshProUGUI>();
        consumptionCount.text = $"{belongingsList[2].itemData.currentHandCount}";


        // 개수는 인벤토리에 동일한 아이템이 있을시 같은 개수로 조정, 아니라면 0으로 조정  TODO


        // 포션값이 변동되었다면 장비창의 데이터 값도 변경시킨다
        new EquipSlotDataList(belongingsList[2]);
    }
}
