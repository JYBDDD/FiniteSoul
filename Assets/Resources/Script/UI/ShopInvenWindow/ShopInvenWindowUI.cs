using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상점과 인벤토리를 담당하는 클래스
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour
{
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

    /// <summary>
    /// 인벤토리 슬롯을 들고있을 리스트
    /// </summary>
    public static List<InvenSlot> Inventory = new List<InvenSlot>();

    private void Start()
    {
        shopInvenCanvas = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref ShopInvenUIState, ref ShopInvenOriginState, shopInvenCanvas);
    }



    // 해당 창이 켜질때    Num2캔버스와 스탯+장비창 캔버스를 종료  TODO


}
