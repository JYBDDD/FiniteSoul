using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ������ �κ��丮�� ����ϴ� Ŭ����
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{
    #region UI ���� ���� ����
    /// <summary>
    /// ���� + �κ��丮 UI ����
    /// </summary>
    public static Define.UIDraw ShopInvenUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// ���� + �κ��丮 UI ������ ����
    /// </summary>
    Define.UIDraw ShopInvenOriginState = Define.UIDraw.SlowlyActivation;

    /// <summary>
    /// ���� + �κ��丮 ��� ĵ���� �׷�
    /// </summary>
    CanvasGroup shopInvenCanvas;
    #endregion

    #region ������ ���� ����
    /// <summary>
    /// �κ��丮 ������ ������� ����Ʈ
    /// </summary>
    public static List<InvenSlot> Inventory = new List<InvenSlot>();

    /// <summary>
    /// ���� ������ ������� ����Ʈ
    /// </summary>
    public static List<ShopSlot> Shop = new List<ShopSlot>();

    /// <summary>
    /// �ӽ÷� ������ �����͸� ���������� ����
    /// </summary>
    UseItemData temporaryItemData = new UseItemData();

    [SerializeField,Tooltip("���� �ڽ�")]
    Description descriptionBox;

    [SerializeField,Tooltip("����,�Ǹ� â")]
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
    /// 2��° ĵ���� �׷�� ����+�κ��丮â�� ĵ�����׷��� ��ȣ �����ϴ� �޼��� (����+�κ��丮â�� ���°� ����ɽ� ����)
    /// </summary>
    private void NumberTwoCanvasSetting()
    {
        if (ShopInvenUIState != ShopInvenOriginState)
        {
            CanvasGroup Num2Canvas = UIManager.Number2CanvasGroup;

            // ����â�� ������ On ���¶��
            if (ShopInvenUIState == Define.UIDraw.SlowlyActivation)
            {
                // ����+�κ��丮â�� ������ ���â�� ������ Off
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);
            }
            // ����â�� ������ Off ���¶��
            if (ShopInvenUIState == Define.UIDraw.SlowlyInactive)
            {
                // ����+�κ��丮â�� ������ ���â�� ������ On
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyActivation;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);

            }
        }
    }

    /// <summary>
    /// ���콺�� ��ġ�� �����͸� ���� ���Ծ����� ���������� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        var result = eventData.pointerCurrentRaycast;

        // �κ��丮
        // ���콺 ��ġ���� �κ� ���Կ� �������� ����
        if(result.gameObject.GetComponent<InvenSlot>())
        {
            var resultSlot = result.gameObject.GetComponent<InvenSlot>();

            // �ش� ������ ���� �����Ѵٸ� ����
            if(resultSlot.itemData != null && resultSlot.itemData.index > 1000)
            {
                temporaryItemData = resultSlot.itemData;

                // ����ڽ� �� ����
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }

        // ����
        // ���콺 ��ġ���� ���� ���Կ� �������� ����
        if(result.gameObject.GetComponent<ShopSlot>())
        {
            var resultSlot = result.gameObject.GetComponent<ShopSlot>();

            // �ش� ������ ���� �����Ѵٸ� ����
            if (resultSlot.itemData != null && resultSlot.itemData.index > 1000)
            {;
                temporaryItemData = resultSlot.itemData;

                // ����ڽ� �� ����
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }
    }

    /// <summary>
    /// Ŭ���� ���Կ� �����۵����Ͱ� ���������� ���� �ִٸ� �����ϵ��� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        RaycastResult result = eventData.pointerCurrentRaycast;

        // Ŭ���� ��ġ�� ���������̸�, �����۰��� ���������� �����ϴ� ���¶�� ����
        if(result.gameObject.GetComponent<ShopSlot>().itemData?.index > 1000)
        {
            // ���� â ���
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.PurchaseWindowPrint(result.gameObject.GetComponent<ShopSlot>().itemData);



        }

        // Ŭ���� ��ġ�� �κ��丮�����̸�, �����۰��� �������� �������ϴ� ���¶�� ����
        if(result.gameObject.GetComponent<InvenSlot>().itemData?.index > 1000)
        {
            // �Ǹ� â ���
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.SellWindowPrint(result.gameObject.GetComponent<InvenSlot>().itemData);
        }
    }


    // ����Ŭ���� �ش� �����۵������� ������ ����,�Ǹ� ������� ���� �Ѱ��ٰ�   TODO
    // �κ��丮�� Ŭ������ ��� �Ǹ�������� ��ȯ
    // ������ Ŭ������ ��� ����������� ��ȯ 

}
