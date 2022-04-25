using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ������ �κ��丮�� ����ϴ� Ŭ����
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour,IPointerEnterHandler
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
    UseItemData temporaryItemData = null;

    /// <summary>
    /// ���� �ڽ�
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
            if(resultSlot.itemData != null)
            {
                temporaryItemData = new UseItemData();
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
            if (resultSlot.itemData != null)
            {
                temporaryItemData = new UseItemData();
                temporaryItemData = resultSlot.itemData;

                // ����ڽ� �� ����
                descriptionBox.DescriptionDataSetting(resultSlot.itemData);
            }
        }
    }



    // �ش� â�� ������    Num2ĵ������ ����+���â ĵ������ ����  TODO


}
