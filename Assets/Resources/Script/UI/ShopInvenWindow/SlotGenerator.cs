using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 슬롯 생성기 클래스
/// </summary>
public class SlotGenerator : MonoBehaviour
{
    // 슬롯 타입을 지정해주는 String타입 변수
    [SerializeField]
    private string SlotType;


    private void Start()
    {
        // 슬롯 타입이 Inven일경우 실행 (인벤토리)
        if(SlotType == "Inven")
        {
            // 인벤토리 슬롯 42개를 만들어 리스트에 삽입
            for (int i = 0; i < 42; ++i)
            {
                // 인벤토리 슬롯 생성
                GameObject invenObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.invenSlotPath);
                // 인벤토리 슬롯 위치 설정
                invenObj.transform.SetParent(transform, false);
                // 인벤토리 슬롯 데이터 및 이미지 셋팅
                InvenSlot invenSlot = invenObj.GetComponent<InvenSlot>();
                invenSlot.ImageDataSetting();
                // 인벤 슬롯 리스트 삽입
                ShopInvenWindowUI.Inventory.Add(invenSlot);

            }
        }
        // 슬롯 타입이 Shop일경우 실행 (상점)
        if(SlotType == "Shop")
        {
            // 상점 아이템 데이터(Etc가 아닌 아이템)을 미리 가져 온다
            var allData = GameManager.Instance.FullData.itemsData;

            // 적용시킬 아이템 데이터
            List<UseItemData> ItemData = new List<UseItemData>();
            for (int i = 0; i < allData.Count; ++i)
            {
                // 아이템 타입이 기타 타입이라면 값 미삽입
                if (allData[i].itemType == Define.ItemMixEnum.ItemType.Etc) { }
                // 기타 타입이 아닌 아이템 데이터 삽입
                else
                {
                    ItemData.Add(new UseItemData(allData[i]));
                }
            }

            // 상점 슬롯 15개를 만들어 리스트에 삽입
            for (int i = 0; i < 15; ++i)
            {
                // 상점 슬롯 생성
                GameObject shopObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.shopSlotPath);
                // 상점 슬롯 위치 설정
                shopObj.transform.SetParent(transform, false);
                // 상점 슬롯 데이터 및 이미지 셋팅
                ShopSlot shopSlot = shopObj.GetComponent<ShopSlot>();
                if (ItemData.Count > i) // -> 해당 아이템데이터가 존재한다면 삽입
                    shopSlot.ImageDataSetting(ItemData[i]);
                else                    // -> 아니라면 Null
                    shopSlot.ImageDataSetting();

                shopSlot.ItemPriceSetting();
                // 상점 슬롯 리스트 삽입
                ShopInvenWindowUI.Shop.Add(shopSlot);
            }
        }

    }
}
