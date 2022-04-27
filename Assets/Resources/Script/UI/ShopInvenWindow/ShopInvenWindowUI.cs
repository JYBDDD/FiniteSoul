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

    [SerializeField,Tooltip("설명 박스")]
    Description descriptionBox;

    [SerializeField,Tooltip("구매,판매 창")]
    SellPurchaseWindow sellPurchaseWindow;
    #endregion

    private void Start()
    {
        shopInvenCanvas = GetComponent<CanvasGroup>();
        descriptionBox.DescriptionDataNull();
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
        if(result.gameObject.GetComponent<ShopSlot>().itemData?.index > 1000)
        {
            // 구매 창 출력
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.PurchaseWindowPrint(result.gameObject.GetComponent<ShopSlot>().itemData);



        }

        // 클릭한 위치가 인벤토리슬롯이며, 아이템값이 정상적을 ㅗ존재하는 상태라면 실행
        if(result.gameObject.GetComponent<InvenSlot>().itemData?.index > 1000)
        {
            // 판매 창 출력
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.SellWindowPrint(result.gameObject.GetComponent<InvenSlot>().itemData);
        }
    }


    // 더블클릭시 해당 아이템데이터의 정보를 구매,판매 윈도우로 값을 넘겨줄것   TODO
    // 인벤토리를 클릭했을 경우 판매윈도우로 전환
    // 상점을 클릭했을 경우 구매윈도우로 전환 

}
