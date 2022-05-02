using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상점 구매,판매 창 클래스
/// </summary>
public class SellPurchaseWindow : MonoBehaviour
{
    [SerializeField,Tooltip("표시할 메인 텍스트")]
    TextMeshProUGUI mainText;

    [SerializeField, Tooltip("필요룬 / 판매룬")]
    TextMeshProUGUI changeBackText;

    [SerializeField, Tooltip("구매,판매할 아이템 이미지")]
    Image itemImg;

    [SerializeField,Tooltip("구매,판매할 아이템 이름")]
    TextMeshProUGUI itemName;

    [SerializeField, Tooltip("구매,판매갯수를 올리는 버튼")]
    Button upButton;

    [SerializeField, Tooltip("구매,판매갯수를 내리는 버튼")]
    Button downButton;

    [SerializeField, Tooltip("현재 구매,판매 개수 / 최대 구매,판매 가능 개수")]
    TextMeshProUGUI allCount;

    [SerializeField, Tooltip("총 가격")]
    TextMeshProUGUI allPrice;

    [SerializeField, Tooltip("구매 버튼")]
    Button okButton;

    [SerializeField, Tooltip("취소 버튼")]
    Button cancelButton;

    /// <summary>
    /// 값을 넘겨받은후 최대 구매,판매 가능 갯수를 들고있을 변수
    /// </summary>
    int maxCount;

    /// <summary>
    /// 현재 구매,판매 갯수
    /// </summary>
    int currentCount;

    /// <summary>
    /// 구매,판매창에 사용되는 아이템 데이터
    /// </summary>
    UseItemData useItemData;

    /// <summary>
    /// 구매,판매 구별 Bool타입 (구매 - true / 판매 - false)
    /// </summary>
    bool purchaseTrue;

    private void Start()
    {
        // 이벤트 바인딩
        upButton.onClick.AddListener(UpCount);
        downButton.onClick.AddListener(DownCount);
        okButton.onClick.AddListener(OkButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
    }

    #region 구매,판매 윈도우 호출 출력 설정
    /// <summary>
    /// 구매 윈도우 출력 + 데이터 셋팅
    /// </summary>
    public void PurchaseWindowPrint(UseItemData itemData)
    {
        mainText.text = "구매할 수량을 선택해주십시오";
        changeBackText.text = "필요 룬";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        itemName.text = itemData.name;
        // 플레이어가 가지고 있는 최대 구매개수만큼 설정
        maxCount = 0;
        var playerRune = InGameManager.Instance.Player.playerData.currentRune;
        while(true)
        {
            // 플레이어의 보유룬으로 해당 아이템 구매 비용을 넘어선다면 실행
            if (playerRune < itemData.salePrice * maxCount + 1)
                break;
            ++maxCount;
        }

        // 플레이어의 소지룬으로 구매 가능한 갯수라면 1부터 설정
        if (maxCount > 0)
        {
            currentCount = 1;
        }
        // 아니라면 0부터 설정
        else
        {
            currentCount = 0;
        }

        allCount.text = $"0 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * currentCount}";

        // 장비아이템이라면 한개로만 적용
        if (itemData.itemType == Define.ItemMixEnum.ItemType.Equipment)
        {
            allCount.text = $"{currentCount} / {currentCount}";
        }

        useItemData = new UseItemData(itemData);
        purchaseTrue = true;

        

    }

    /// <summary>
    /// 판매 윈도우 출력 + 데이터 셋팅
    /// </summary>
    public void SellWindowPrint(UseItemData itemData)
    {
        mainText.text = "판매할 수량을 선택해주십시오";
        changeBackText.text = "판매 룬";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        itemName.text = itemData.name;
        // 플레이어가 가지고 있는 최대 판매개수만큼 설정
        maxCount = 0;
        var inventory = ShopInvenWindowUI.Inventory;
        for(int i = 0; i < inventory.Count; ++i)
        {
            // 인벤토리에서 데이터가 같은 값을 찾았다면 값을 더함
            if(inventory[i].itemData.index == itemData.index)
            {
                maxCount += inventory[i].itemData.currentHandCount;
            }
        }
        currentCount = 1;

        allCount.text = $"1 / {maxCount}";
        allPrice.text = $"{itemData.salePrice * currentCount}";

        // 장비아이템이라면 한개로만 적용
        if(itemData.itemType == Define.ItemMixEnum.ItemType.Equipment)
        {
            allCount.text = $"1 / {currentCount}";
        }

        useItemData = new UseItemData(itemData);
        purchaseTrue = false;
    }
    #endregion

    #region 구매,판매 갯수 설정
    /// <summary>
    /// 구매, 판매 개수증가
    /// </summary>
    private void UpCount()
    {
        // 구매라면 실행 (+ 장비가 아니라면)
        if(useItemData.salePrice * currentCount < InGameManager.Instance.Player.playerData.currentRune &&
            useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment && purchaseTrue)
        {
            ++currentCount;

            // 총가격, 구매/판매 갯수 설정
            CountAndPriceSet();

            // UI 사운드 출력
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
        }
        // 판매라면 실행 (+ 장비가 아니라면) (+ 최대 판매갯수가 넘어가지 않았다면)
        if(useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment && !purchaseTrue &&
            useItemData.currentHandCount > currentCount)
        {
            ++currentCount;

            // 총가격, 구매/판매 갯수 설정
            CountAndPriceSet();

            // UI 사운드 출력
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
        }
    }
    
    /// <summary>
    /// 구매, 판매 개수 감소
    /// </summary>
    private void DownCount()
    {
        // 구매, 판매 개수가 1이하가 아니라면 실행 (+ 장비가 아니라면)
        if (currentCount > 1 && useItemData.itemType != Define.ItemMixEnum.ItemType.Equipment)
        {
            --currentCount;

            // 총가격, 구매/판매 갯수 설정
            CountAndPriceSet();

            // UI 사운드 출력
            SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
        }
    }

    /// <summary>
    /// 구매,판매 갯수 및 가격 설정 메소드
    /// </summary>
    private void CountAndPriceSet()
    {
        // 카운트 재설정
        allCount.text = $"{currentCount} / " + $"{maxCount}";
        // 총금액 재설정
        allPrice.text = $"{currentCount * useItemData.salePrice}";
    }
    #endregion

    #region Ok,Cancel 버튼
    
    private void OkButtonClick()
    {
        // 판매 및 구매를 할수있는 상태라면 실행
        if(maxCount > 0)
        {
            var inventory = ShopInvenWindowUI.Inventory;

            // 상점에서 구매중이라면
            if(purchaseTrue == true)
            {
                // 플레이어 소지룬 감소
                InGameManager.Instance.Player.playerData.currentRune -= (currentCount * useItemData.salePrice);
                // 인벤토리에 아이템 추가, 해당 아이템이 장비라면 다음 비어있는 칸으로 추가 / 기타,소비 아이템은 99개가 넘을경우 다음 칸으로 추가
                useItemData.currentHandCount = currentCount;
                ShopInvenWindowUI.SearchAddtionData(useItemData);

                // 적용후 종료
                CancelButtonClick();

                // 사운드 출력
                SoundManager.Instance.PlayAudio("UIComplete", SoundPlayType.Single);
            }
            // 아이템을 판매중이라면
            if(purchaseTrue == false)
            {
                // 플레이어 소지룬 증가
                InGameManager.Instance.Player.playerData.currentRune += (currentCount * useItemData.salePrice);
                // 소지중인 아이템 갯수 감소 갯수가 0이라면 인벤토리에서 아이템 인덱스값 1000(미사용값)으로 변경
                useItemData.currentHandCount = currentCount;
                ShopInvenWindowUI.SearchSubtractData(currentCount);

                // 적용후 종료
                CancelButtonClick();

                // 사운드 출력
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
