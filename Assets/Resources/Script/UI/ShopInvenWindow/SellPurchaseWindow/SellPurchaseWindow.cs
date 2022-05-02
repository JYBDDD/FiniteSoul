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

    [SerializeField, Tooltip("�ʿ�� / �Ǹŷ�")]
    TextMeshProUGUI changeBackText;

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
    /// ����,�Ǹ�â�� ���Ǵ� ������ ������
    /// </summary>
    UseItemData useItemData;

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
        changeBackText.text = "�ʿ� ��";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        itemName.text = itemData.name;
        // �÷��̾ ������ �ִ� �ִ� ���Ű�����ŭ ����
        maxCount = 0;
        var playerRune = InGameManager.Instance.Player.playerData.currentRune;
        while(true)
        {
            // �÷��̾��� ���������� �ش� ������ ���� ����� �Ѿ�ٸ� ����
            if (playerRune < itemData.salePrice * maxCount + 1)
                break;
            ++maxCount;
        }

        // �÷��̾��� ���������� ���� ������ ������� 1���� ����
        if (maxCount > 0)
        {
            currentCount = 1;
        }
        // �ƴ϶�� 0���� ����
        else
        {
            currentCount = 0;
        }

        allCount.text = $"0 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * currentCount}";

        // ���������̶�� �Ѱ��θ� ����
        if (itemData.itemType == Define.ItemMixEnum.ItemType.Equipment)
        {
            allCount.text = $"{currentCount} / {currentCount}";
        }

        useItemData = new UseItemData(itemData);
        purchaseTrue = true;

        

    }

    /// <summary>
    /// �Ǹ� ������ ��� + ������ ����
    /// </summary>
    public void SellWindowPrint(UseItemData itemData)
    {
        mainText.text = "�Ǹ��� ������ �������ֽʽÿ�";
        changeBackText.text = "�Ǹ� ��";
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
        currentCount = 1;

        allCount.text = $"1 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * currentCount}";

        // ���������̶�� �Ѱ��θ� ����
        if(itemData.itemType == Define.ItemMixEnum.ItemType.Equipment)
        {
            allCount.text = $"1 / {currentCount}";
        }

        useItemData = new UseItemData(itemData);
        purchaseTrue = false;
    }
    #endregion

    #region ����,�Ǹ� ���� ����
    /// <summary>
    /// ����, �Ǹ� ��������
    /// </summary>
    private void UpCount()
    {
        // ���Ŷ�� ���� (+ ��� �ƴ϶��)
        if(useItemData.salePrice * currentCount < InGameManager.Instance.Player.playerData.currentRune &&
            useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment && purchaseTrue)
        {
            ++currentCount;

            // �Ѱ���, ����/�Ǹ� ���� ����
            CountAndPriceSet();

            // UI ���� ���
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
        }
        // �ǸŶ�� ���� (+ ��� �ƴ϶��) (+ �ִ� �ǸŰ����� �Ѿ�� �ʾҴٸ�)
        if(useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment && !purchaseTrue &&
            useItemData.currentHandCount > currentCount)
        {
            ++currentCount;

            // �Ѱ���, ����/�Ǹ� ���� ����
            CountAndPriceSet();

            // UI ���� ���
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
        }
    }
    
    /// <summary>
    /// ����, �Ǹ� ���� ����
    /// </summary>
    private void DownCount()
    {
        // ����, �Ǹ� ������ 1���ϰ� �ƴ϶�� ���� (+ ��� �ƴ϶��)
        if (currentCount > 1 && useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment)
        {
            --currentCount;

            // �Ѱ���, ����/�Ǹ� ���� ����
            CountAndPriceSet();

            // UI ���� ���
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
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
        allPrice.text = $"{currentCount * useItemData.salePrice}";
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
                InGameManager.Instance.Player.playerData.currentRune -= (currentCount * useItemData.salePrice);
                // �κ��丮�� ������ �߰�, �ش� �������� ����� ���� ����ִ� ĭ���� �߰� / ��Ÿ,�Һ� �������� 99���� ������� ���� ĭ���� �߰�
                useItemData.currentHandCount = currentCount;
                ShopInvenWindowUI.SearchAddtionData(useItemData);

                // ������ ����
                CancelButtonClick();

                // ���� ���
                SoundManager.Instance.PlayAudio("UIComplete", SoundPlayType.Single);
            }
            // �������� �Ǹ����̶��
            if(purchaseTrue == false)
            {
                // �÷��̾� ������ ����
                InGameManager.Instance.Player.playerData.currentRune += (currentCount * useItemData.salePrice);
                // �������� ������ ���� ���� ������ 0�̶�� �κ��丮���� ������ �ε����� 1000(�̻�밪)���� ����
                useItemData.currentHandCount = currentCount;
                ShopInvenWindowUI.SearchSubtractData(currentCount);

                // ������ ����
                CancelButtonClick();

                // ���� ���
                SoundManager.Instance.PlayAudio("UIComplete", SoundPlayType.Single);
            }
        }
    }

    private void CancelButtonClick()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
