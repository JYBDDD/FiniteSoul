using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �κ��丮�� ����ϴ� Ŭ����
/// </summary>
public class ShopInvenWindowUI : MonoBehaviour
{
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

    /// <summary>
    /// �κ��丮 ������ ������� ����Ʈ
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



    // �ش� â�� ������    Num2ĵ������ ����+���â ĵ������ ����  TODO


}
