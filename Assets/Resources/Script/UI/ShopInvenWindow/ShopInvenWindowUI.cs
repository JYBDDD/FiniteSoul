using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 상점과 인벤토리를 담당하는 클래스
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour,IPointerEnterHandler
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
    UseItemData temporaryItemData = null;

    /// <summary>
    /// 설명 박스
    /// </summary>
    [SerializeField]
    Description descriptionBox;
    #endregion

    private void Start()
    {
        shopInvenCanvas = GetComponent<CanvasGroup>();
        descriptionBox.DescriptionDataNull();
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref ShopInvenUIState, ref ShopInvenOriginState, shopInvenCanvas);
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
            if(resultSlot.itemData != null)
            {
                temporaryItemData = new UseItemData();
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
            if (resultSlot.itemData != null)
            {
                temporaryItemData = new UseItemData();
                temporaryItemData = resultSlot.itemData;

                // 설명박스 값 설정
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }
    }



    // 해당 창이 켜질때    Num2캔버스와 스탯+장비창 캔버스를 종료  TODO


}
