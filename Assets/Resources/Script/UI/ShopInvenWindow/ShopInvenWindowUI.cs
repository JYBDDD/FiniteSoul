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

    /// <summary>
    /// �ǸŽ� ���Ǵ� Inventory�� �ش� ������ ����
    /// </summary>
    public static InvenSlot subtractSlot;

    [SerializeField,Tooltip("���� �ڽ�")]
    Description descriptionBox;

    [SerializeField,Tooltip("����,�Ǹ� â")]
    SellPurchaseWindow sellPurchaseWindow;
    #endregion

    private void Start()
    {
        shopInvenCanvas = GetComponent<CanvasGroup>();
        descriptionBox.DescriptionDataNull();

        // ���� �Ǹ�â ù���� ��Ȱ��ȭ�� ����
        sellPurchaseWindow.gameObject.SetActive(false);
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
        if(result.gameObject.GetComponent<ShopSlot>()?.itemData?.index > 1000)
        {
            // ���� â ���
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.PurchaseWindowPrint(result.gameObject.GetComponent<ShopSlot>().itemData);
        }

        // Ŭ���� ��ġ�� �κ��丮�����̸�, �����۰��� �������� �����ϴ� ���¶�� ����
        if(result.gameObject.GetComponent<InvenSlot>()?.itemData?.index > 1000)
        {
            // �Ǹ� â ���
            sellPurchaseWindow.gameObject.SetActive(true);
            sellPurchaseWindow.SellWindowPrint(result.gameObject.GetComponent<InvenSlot>().itemData);

            // �Ǹ� �� ���Ǵ� �κ��丮 �ӽ� ���� �� �Ҵ�
            subtractSlot = result.gameObject.GetComponent<InvenSlot>();
        }
    }

    /// <summary>
    /// �Ű������� ���� �������� �κ��丮�� �����ϴ��� ã�� �����Ѵٸ� ���� ����, 
    /// �ƴ϶�� ���� ���� �� ������ ��ȯ -> (���� ��� ����� ���·� ��ȯ)
    /// </summary>
    /// <param name="useItemData"></param>
    /// <returns></returns>
    public static void SearchAddtionData(UseItemData useItemData)
    {
        // �ִ� ������ ���� ����
        int remainCount = 0;
        // �ִ� ������ �ʰ������� ���Ǵ� �ӽ� �ڽ�
        var changeItemData = new UseItemData();

        InvenSlot blankSlot = new InvenSlot();
        bool OneCheck = false;

        for (int i = 0; i < Inventory.Count; ++i)
        {
            // �κ��丮�� ������ �ε����� ���� ���� �ְ� ���������� �ƴ϶��, 
            if(useItemData.index == Inventory[i].itemData.index && useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment)
            {
                //���������� 99���� �Ѿ��ٸ� ����
                if (useItemData.currentHandCount + Inventory[i].itemData.currentHandCount > 99)
                {
                    // �ִ� ������ �� ���� ����
                    remainCount = (useItemData.currentHandCount + Inventory[i].itemData.currentHandCount) - 99;

                    changeItemData = new UseItemData(useItemData);
                    changeItemData.currentHandCount = 99;
                    Inventory[i].ItemCountSetting(changeItemData);
                }
                //���������� 99���� �����ʾҴٸ�
                else
                {
                    // �ش� �κ��丮 ���Կ� ���� ���� ���
                    Inventory[i].ItemCountSetting(useItemData);
                    return;
                }
            }

            // ���� ������ �����Ѵٸ� �κ��丮������ ����ִ� ���� ���� ���԰� ����
            if (remainCount > 0 && Inventory[i].itemData?.index <= 1000)
            {
                // ���� ������ ��� ������ remainCount = 0 ���� ����
                changeItemData.currentHandCount = remainCount;
                Inventory[i].ImageDataSetting(changeItemData);
                Inventory[i].ItemCountSetting(changeItemData);
                remainCount = 0;
            }

            // �κ��丮������ ����ִ� ���� ���� ���԰� ����
            if (remainCount <= 0 && Inventory[i].itemData?.index <= 1000 && !OneCheck)
            {
                OneCheck = true;
                blankSlot = Inventory[i];
            }
        }

        // ��ġ�ϴ� ���� ���ٸ� �κ��丮 ����ִ� ���� �� �������� ����
        blankSlot.ImageDataSetting(useItemData);
        blankSlot.TextCountAlpha();
    }

    /// <summary>
    /// �Ű������� ���� ���� ������ ���� ����, 0�� �Ǿ����� �ش� �κ��丮 �������� �ε����� 1000(��� �Ұ��ɰ�)���� �����ϸ�
    /// �ش� �κ��丮 ������ �̹��� ���� �缳��
    /// </summary>
    public static void SearchSubtractData(int sellCount)
    {
        subtractSlot.itemData.currentHandCount -= sellCount;

        // ���� ������ 0�� �Ǿ��� ��� �ش� �̹���, �����Ͱ��� ������
        if(subtractSlot.itemData.currentHandCount <= 0)
        {
            subtractSlot.ImageDataSetting();
            subtractSlot.TextCountAlpha();
        }
    }

    /// <summary>
    /// �κ��丮�� �ִ� �ش� ������Ÿ���� ���� �κ��丮 ���� ����Ʈ�� ��ȯ
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
