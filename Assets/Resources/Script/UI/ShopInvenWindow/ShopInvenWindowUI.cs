using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 상점과 인벤토리를 담당하는 클래스
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{
    #region UI 상태 관련 변수
    /// <summary>
    /// 상점 + 인벤토리 UI 상태
    /// </summary>
    public static Define.UIDraw ShopInvenUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// 상점 + 인벤토리 UI 변경전 상태
    /// </summary>
    Define.UIDraw ShopInvenOriginState = Define.UIDraw.SlowlyActivation;

    /// <summary>
    /// 상점 + 인벤토리 담당 캔버스 그룹
    /// </summary>
    CanvasGroup shopInvenCanvas;
    #endregion

    #region 데이터 관련 변수
    /// <summary>
    /// 인벤토리 슬롯을 들고있을 리스트
    /// </summary>
    public static List<InvenSlot> Inventory = new List<InvenSlot>();

    /// <summary>
    /// 상점 슬롯을 들고있을 리스트
    /// </summary>
    public static List<ShopSlot> Shop = new List<ShopSlot>();

    /// <summary>
    /// 임시로 아이템 데이터를 가지고있을 변수
    /// </summary>
    UseItemData temporaryItemData = new UseItemData();

    /// <summary>
    /// 판매시 사용되는 Inventory의 해당 아이템 슬롯
    /// </summary>
    public static InvenSlot subtractSlot;

    [SerializeField,Tooltip("설명 박스")]
    Description descriptionBox;

    [SerializeField,Tooltip("구매,판매 창")]
    SellPurchaseWindow sellPurchaseWindow;
    #endregion

    private void Start()
    {
        shopInvenCanvas = GetComponent<CanvasGroup>();
        descriptionBox.DescriptionDataNull();

        // 구매 판매창 첫설정 비활성화로 조정
        sellPurchaseWindow.gameObject.SetActive(false);
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref ShopInvenUIState, ref ShopInvenOriginState, shopInvenCanvas,NumberTwoCanvasSetting);
    }

    /// <summary>
    /// 2번째 캔버스 그룹과 상점+인벤토리창의 캔버스그룹을 상호 조정하는 메서드 (상점+인벤토리창의 상태가 변경될시 실행)
    /// </summary>
    private void NumberTwoCanvasSetting()
    {
        if (ShopInvenUIState != ShopInvenOriginState)
        {
            CanvasGroup Num2Canvas = UIManager.Number2CanvasGroup;

            // 상점창이 서서히 On 상태라면
            if (ShopInvenUIState == Define.UIDraw.SlowlyActivation)
            {
                // 상점+인벤토리창을 제외한 모든창을 서서히 Off
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);
            }
            // 상점창이 서서히 Off 상태라면
            if (ShopInvenUIState == Define.UIDraw.SlowlyInactive)
            {
                // 상점+인벤토리창을 제외한 모든창을 서서히 On
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyActivation;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);

            }
        }
    }

    /// <summary>
    /// 마우스의 위치가 데이터를 읽을 슬롯안으로 진입했을시 실행
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        var result = eventData.pointerCurrentRaycast;

        // 인벤토리
        // 마우스 위치값이 인벤 슬롯에 들어왔을시 실행
        if(result.gameObject.GetComponent<InvenSlot>())
        {
            var resultSlot = result.gameObject.GetComponent<InvenSlot>();

            // 해당 아이템 값이 존재한다면 실행
            if(resultSlot.itemData != null && resultSlot.itemData.index > 1000)
            {
                temporaryItemData = resultSlot.itemData;

                // 설명박스 값 설정
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }

        // 상점
        // 마우스 위치값이 상점 슬롯에 들어왔을시 실행
        if(result.gameObject.GetComponent<ShopSlot>())
        {
            var resultSlot = result.gameObject.GetComponent<ShopSlot>();

            // 해당 아이템 값이 존재한다면 실행
            if (resultSlot.itemData != null && resultSlot.itemData.index > 1000)
            {;
                temporaryItemData = resultSlot.itemData;

                // 설명박스 값 설정
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }
    }

    /// <summary>
    /// 클릭한 슬롯에 아이템데이터가 정상적으로 들어가져 있다면 실행하도록 설정
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        RaycastResult result = eventData.pointerCurrentRaycast;

        // 클릭한 위치가 상점슬롯이며, 아이템값이 정상적으로 존재하는 상태라면 실행
        if(result.gameObject.GetComponent<ShopSlot>()?.itemData?.index > 1000)
        {
            // 구매 창 출력
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.PurchaseWindowPrint(result.gameObject.GetComponent<ShopSlot>().itemData);
        }

        // 클릭한 위치가 인벤토리슬롯이며, 아이템값이 정상적을 존재하는 상태라면 실행
        if(result.gameObject.GetComponent<InvenSlot>()?.itemData?.index > 1000)
        {
            // 판매 창 출력
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.SellWindowPrint(result.gameObject.GetComponent<InvenSlot>().itemData);

            // 판매 시 사용되는 인벤토리 임시 변수 값 할당
            subtractSlot = result.gameObject.GetComponent<InvenSlot>();
        }
    }

    /// <summary>
    /// 매개변수에 넣은 아이템이 인벤토리에 존재하는지 찾고 존재한다면 값을 삽입, 
    /// 아니라면 가장 앞의 빈 슬롯을 반환 -> (값을 모두 적용된 상태로 반환)
    /// </summary>
    /// <param name="useItemData"></param>
    /// <returns></returns>
    public static void SearchAddtionData(UseItemData useItemData)
    {
        // 최대 삽입후 남은 갯수
        int remainCount = 0;
        // 최대 개수를 초과했을시 사용되는 임시 박스
        var changeItemData = new UseItemData();

        InvenSlot blankSlot = new InvenSlot();
        bool OneCheck = false;

        for (int i = 0; i < Inventory.Count; ++i)
        {
            // 인벤토리에 아이템 인덱스가 같은 값이 있고 장비아이템이 아니라면, 
            if(useItemData.index == Inventory[i].itemData.index && useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment)
            {
                //보유개수가 99개를 넘었다면 실행
                if (useItemData.currentHandCount + Inventory[i].itemData.currentHandCount > 99)
                {
                    // 최대 개수를 뺀 남은 개수
                    remainCount = (useItemData.currentHandCount + Inventory[i].itemData.currentHandCount) - 99;

                    changeItemData = new UseItemData(useItemData);
                    changeItemData.currentHandCount = 99;
                    Inventory[i].ItemCountSetting(changeItemData);
                }
                //보유개수가 99개를 넘지않았다면
                else
                {
                    // 해당 인벤토리 슬롯에 소지 개수 상승
                    Inventory[i].ItemCountSetting(useItemData);
                    return;
                }
            }

            // 남은 갯수가 존재한다면 인벤토리슬롯이 비어있는 가장 앞의 슬롯값 삽입
            if (remainCount > 0 && Inventory[i].itemData?.index <= 1000)
            {
                // 남은 값까지 모두 삽입후 remainCount = 0 으로 지정
                changeItemData.currentHandCount = remainCount;
                Inventory[i].ImageDataSetting(changeItemData);
                Inventory[i].ItemCountSetting(changeItemData);
                remainCount = 0;
            }

            // 인벤토리슬롯이 비어있는 가장 앞의 슬롯값 삽입
            if (remainCount <= 0 && Inventory[i].itemData?.index <= 1000 && !OneCheck)
            {
                OneCheck = true;
                blankSlot = Inventory[i];
            }
        }

        // 일치하는 값이 없다면 인벤토리 비어있는 가장 앞 슬롯으로 삽입
        blankSlot.ImageDataSetting(useItemData);
        blankSlot.TextCountAlpha();
    }

    /// <summary>
    /// 매개변수로 받은 값의 아이템 개수 차감, 0이 되었을시 해당 인벤토리 아이템의 인덱스를 1000(사용 불가능값)으로 변경하며
    /// 해당 인벤토리 슬롯의 이미지 값을 재설정
    /// </summary>
    public static void SearchSubtractData(int sellCount)
    {
        subtractSlot.itemData.currentHandCount -= sellCount;

        // 만약 갯수가 0이 되었을 경우 해당 이미지, 데이터값을 재조정
        if(subtractSlot.itemData.currentHandCount <= 0)
        {
            subtractSlot.ImageDataSetting();
            subtractSlot.TextCountAlpha();
        }
    }

    /// <summary>
    /// 인벤토리에 있는 해당 아이템타입을 가진 인벤토리 슬롯 리스트를 반환
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public static List<InvenSlot> InventoryDataFind(Define.ItemMixEnum.ItemType itemType)
    {
        List<InvenSlot> ivenList = new List<InvenSlot>();

        for(int i =0; i < Inventory.Count; ++i)
        {
            if(itemType == Inventory[i].itemData.itemType)
            {
                ivenList.Add(Inventory[i]);
            }
        }


        return ivenList;
    }
}
