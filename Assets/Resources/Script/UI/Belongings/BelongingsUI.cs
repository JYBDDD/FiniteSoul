using System;
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
    /// <summary>
    /// 소지품 값이 변경되었을시 사용되는 정적 변수
    /// </summary>
    public static BelongingsUI TempInstance;

    [SerializeField,Tooltip("소비 아이템 이름")]
    private TextMeshProUGUI consumptionText;

    [SerializeField, Tooltip("소지품창의 데이터를 가지고있는 리스트")]
    public List<EquipSlot> belongingsList = new List<EquipSlot>();

    /// <summary>
    /// 변경전 소지품 데이터 리스트
    /// </summary>
    private List<EquipSlot> originList = new List<EquipSlot>();

    /// <summary>
    /// 소비 아이템 변경시 호출용으로 사용될 변수
    /// </summary>
    private int consumCount = -1;

    /// <summary>
    /// 현재 소지품 창에 출력중인 인벤토리 슬롯와 연결된 소비품 데이터
    /// </summary>
    private InvenSlot useConsumSlot;

    private void Awake()
    {
        TempInstance = this;
    }

    private void Update()
    {
        // 데이터가 변동되었다면 실행
        if(originList != belongingsList)    // -> 장비창에 플레이어의 데이터가 입력된 이후 실행된다
        {
            BelongingsListInsert();
        }
    }

    /// <summary>
    /// 소지품슬롯을 장비창데이터와 연동 (초기 시작시에만 호출되는 메서드)
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
        // 인벤토리에 소비아이템 존재시 값 삽입
        var consumData = ConsumDataChange();

        // 값이 Null값이 아니라면 리턴
        if(consumData != null)
            return;

        // 인벤토리에 소비 아이템이 존재하지 않을 경우 실행
        var consumptionData = GameManager.Instance.FullData.itemsData.Where(_ => _.itemType == Define.ItemMixEnum.ItemType.Consumption).FirstOrDefault();
        ImgAndTextChange(consumptionData,1);



    }

    /// <summary>
    /// 이미지 및 갯수 텍스트, 데이터 변경
    /// </summary>
    /// <param name="useItemData"></param>
    private void ImgAndTextChange(UseItemData useItemData, int basicCount = 0)
    {
        // 소비아이템 데이터 삽입
        belongingsList[2].ItemDataSetting(useItemData);
        consumptionText.text = belongingsList[2].itemData.name;

        // 현재 소지 개수 설정
        TextMeshProUGUI consumptionCount = belongingsList[2].GetComponentInChildren<TextMeshProUGUI>();
        consumptionCount.text = $"{belongingsList[2].itemData.currentHandCount - basicCount}";

        new EquipSlotDataList(belongingsList[2]);
    }

    /// <summary>
    /// 소비품 데이터 값이 변경되었을 경우 호출되는 메서드
    /// </summary>
    public UseItemData ConsumDataChange()  
    {
        var invenConsumData = ShopInvenWindowUI.InventoryDataFind(Define.ItemMixEnum.ItemType.Consumption);
        if (invenConsumData.Count > 0)
        {
            // 호출되는 카운트가 소비아이템의 리스트 갯수를 넘어섰다면 0으로 초기화후, 0번째 아이템으로 되돌린다 
            if (invenConsumData.Count <= consumCount + 1)
            {
                consumCount = 0;

                // 데이터값 인벤토리와 연동
                belongingsList[2].itemData = invenConsumData[consumCount].itemData;
                ImgAndTextChange(invenConsumData[consumCount].itemData);
                useConsumSlot = invenConsumData[consumCount];
                return invenConsumData[consumCount].itemData;
            }
            else
            {
                ++consumCount;

                // 데이터값 인벤토리와 연동
                belongingsList[2].itemData = invenConsumData[consumCount].itemData;
                ImgAndTextChange(invenConsumData[consumCount].itemData);
                useConsumSlot = invenConsumData[consumCount];
                return invenConsumData[consumCount].itemData;
            }
        }

        return null;
    }

    /// <summary>
    /// 인벤토리에서 소비아이템 변동시 사용되는 메서드
    /// </summary>
    public void InvenConsumDataChange()
    {
        var invenConsumData = ShopInvenWindowUI.InventoryDataFind(Define.ItemMixEnum.ItemType.Consumption);
        if (invenConsumData.Count > 0)
        {
            // 데이터값 인벤토리와 연동
            belongingsList[2].itemData = invenConsumData[consumCount].itemData;
            ImgAndTextChange(invenConsumData[consumCount].itemData);
            useConsumSlot = invenConsumData[consumCount];
        }
    }

    /// <summary>
    /// 소비 아이템 사용 메서드
    /// </summary>
    /// <returns></returns>
    public UseItemData ConsumUse()
    {
        // 해당 소비창에 아이템 갯수가 1개 이상일 경우 사용 가능
        if(belongingsList[2].itemData.currentHandCount >= 1)
        {
            // 인벤토리 소지품데이터를 소비품창의 아이템과 연결
            belongingsList[2].itemData = useConsumSlot.itemData;

            // 갯수 차감
            --belongingsList[2].itemData.currentHandCount;

            // 인벤토리 아이템 이미지, 텍스트 재설정
            useConsumSlot.ImageDataSetting(belongingsList[2].itemData);
            useConsumSlot.TextCountAlpha();

            // 소지품 데이터, 텍스트 재설정
            ImgAndTextChange(belongingsList[2].itemData);
            return belongingsList[2].itemData;
        }

        return null;
    }

    /// <summary>
    /// 소비아이템을 사용가능한지 체크하는 메서드
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
