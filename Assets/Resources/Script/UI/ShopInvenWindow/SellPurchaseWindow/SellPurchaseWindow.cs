using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ����,�Ǹ� â Ŭ����
/// </summary>
public class SellPurchaseWindow : MonoBehaviour
{
    [SerializeField,Tooltip("ǥ���� ���� �ؽ�Ʈ")]
    TextMeshProUGUI mainText;

    [SerializeField, Tooltip("����,�Ǹ��� ������ �̹���")]
    Image itemImg;

    [SerializeField,Tooltip("����,�Ǹ��� ������ �̸�")]
    TextMeshProUGUI itemName;

    [SerializeField, Tooltip("����,�ǸŰ����� �ø��� ��ư")]
    Button upButton;

    [SerializeField, Tooltip("����,�ǸŰ����� ������ ��ư")]
    Button downButton;

    [SerializeField, Tooltip("���� ����,�Ǹ� ���� / �ִ� ����,�Ǹ� ���� ����")]
    TextMeshProUGUI allCount;

    [SerializeField, Tooltip("�� ����")]
    TextMeshProUGUI allPrice;

    [SerializeField, Tooltip("���� ��ư")]
    Button okButton;

    [SerializeField, Tooltip("��� ��ư")]
    Button cancelButton;

    /// <summary>
    /// ���� �Ѱܹ����� �ִ� ����,�Ǹ� ���� ������ ������� ����
    /// </summary>
    int maxCount;

    /// <summary>
    /// ���� ����,�Ǹ� ����
    /// </summary>
    int currentCount;

    /// <summary>
    /// ����,�Ǹ�â�� ���Ǵ� ������ ����
    /// </summary>
    float itemPrice;

    /// <summary>
    /// ����,�Ǹ� ���� BoolŸ�� (���� - true / �Ǹ� - false)
    /// </summary>
    bool purchaseTrue;

    private void Start()
    {
        // �̺�Ʈ ���ε�
        upButton.onClick.AddListener(UpCount);
        downButton.onClick.AddListener(DownCount);
        okButton.onClick.AddListener(OkButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
    }

    #region ����,�Ǹ� ������ ȣ�� ��� ����
    /// <summary>
    /// ���� ������ ��� + ������ ����
    /// </summary>
    public void PurchaseWindowPrint(UseItemData itemData)
    {
        mainText.text = "������ ������ �������ֽʽÿ�";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        itemName.text = itemData.name;
        // �÷��̾ ������ �ִ� �ִ� ���Ű�����ŭ ����
        maxCount = 0;
        var playerRune = InGameManager.Instance.Player.playerData.currentRune;
        while(true)
        {
            // �÷��̾��� ���������� �ش� ������ ���� ����� �Ѿ�ٸ� ����
            if (playerRune < itemData.salePrice * maxCount)
                break;
            ++maxCount;
        }
        allCount.text = $"1 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * maxCount}";

        itemPrice = itemData.salePrice;
        purchaseTrue = true;

        // �÷��̾��� ���������� ���� ������ ������� 1���� ����
        if(maxCount > 0)
        {
            currentCount = 1;
        }
        // �ƴ϶�� 0���� ����
        else
        {
            currentCount = 0;
        }

    }

    /// <summary>
    /// �Ǹ� ������ ��� + ������ ����
    /// </summary>
    public void SellWindowPrint(UseItemData itemData)
    {
        mainText.text = "�Ǹ��� ������ �������ֽʽÿ�";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        itemName.text = itemData.name;
        // �÷��̾ ������ �ִ� �ִ� �ǸŰ�����ŭ ����
        maxCount = 0;
        var inventory = ShopInvenWindowUI.Inventory;
        for(int i = 0; i < inventory.Count; ++i)
        {
            // �κ��丮���� �����Ͱ� ���� ���� ã�Ҵٸ� ���� ����
            if(inventory[i].itemData.index == itemData.index)
            {
                maxCount += inventory[i].itemData.currentHandCount;
            }
        }
        allCount.text = $"1 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * maxCount}";

        itemPrice = itemData.salePrice;
        currentCount = 1;
        purchaseTrue = false;
    }
    #endregion

    #region ����,�Ǹ� ���� ����
    /// <summary>
    /// ����, �Ǹ� ��������
    /// </summary>
    private void UpCount()
    {
        // ����, �ǸŰ� ������ ������� ����
        if(itemPrice * maxCount < InGameManager.Instance.Player.playerData.currentRune)
        {
            ++currentCount;

            // �Ѱ���, ����/�Ǹ� ���� ����
            CountAndPriceSet();
        }
    }
    
    /// <summary>
    /// ����, �Ǹ� ���� ����
    /// </summary>
    private void DownCount()
    {
        // ����, �Ǹ� ������ 1���ϰ� �ƴ϶�� ����
        if(currentCount > 1)
        {
            --currentCount;

            // �Ѱ���, ����/�Ǹ� ���� ����
            CountAndPriceSet();
        }
    }

    /// <summary>
    /// ����,�Ǹ� ���� �� ���� ���� �޼ҵ�
    /// </summary>
    private void CountAndPriceSet()
    {
        // ī��Ʈ �缳��
        allCount.text = $"{currentCount} / " + $"{maxCount}";
        // �ѱݾ� �缳��
        allPrice.text = $"{currentCount * itemPrice}";
    }
    #endregion

    #region Ok,Cancel ��ư
    
    private void OkButtonClick()
    {
        // �Ǹ� �� ���Ÿ� �Ҽ��ִ� ���¶�� ����
        if(maxCount > 0)
        {
            var inventory = ShopInvenWindowUI.Inventory;

            // �������� �������̶��
            if(purchaseTrue == true)
            {
                // �÷��̾� ������ ����
                InGameManager.Instance.Player.playerData.currentRune -= (currentCount * itemPrice);
                // �κ��丮�� ������ �߰�, �ش� �������� ����� ���� ����ִ� ĭ���� �߰� / ��Ÿ,�Һ� �������� 99���� ������� ���� ĭ���� �߰� TODO




            }
            // �������� �Ǹ����̶��
            if(purchaseTrue == false)
            {
                // �÷��̾� ������ ����
                InGameManager.Instance.Player.playerData.currentRune += (currentCount * itemPrice);
                // �������� ������ ���� ���� ������ 0�̶�� �κ��丮���� ������ �ε����� 1000(�̻�밪)���� ���� TODO




            }
        }
    }

    private void CancelButtonClick()
    {

    }

    #endregion
}
