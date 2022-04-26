using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 설명 창을 출력하는 클래스
/// </summary>
public class Description : MonoBehaviour
{
    [SerializeField,Tooltip("아이템 설명")]
    TextMeshProUGUI description;

    [SerializeField,Tooltip("아이템 이미지")]
    Image itemImg;

    [SerializeField, Tooltip("아이템 이름")]
    TextMeshProUGUI itemName;

    [SerializeField,Tooltip("아이템 소지 개수 / 인벤토리 슬롯을 RayCast했을때만 실행")]
    TextMeshProUGUI itemBelongingsCount;

    [SerializeField,Tooltip("아이템 뒷 배경 이미지")]
    Image backImg;
    [SerializeField, Tooltip("아이템 개수 타이틀")]
    TextMeshProUGUI backTitle;

    /// <summary>
    /// 임시로 아이템 데이터를 가지고있을 변수
    /// </summary>
    UseItemData temporaryItemData = null;


    /// <summary>
    /// 설명박스 데이터 셋팅
    /// </summary>
    public void DescriptionDataSetting(UseItemData itemData)
    {
        DescriptionAlphaOne();

        temporaryItemData = new UseItemData(itemData);

        itemName.text = $"{itemData.name}";
        description.text = $"{itemData.description}";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);

        // 인벤토리에서 들고있는 해당 아이템의 개수
        itemBelongingsCount.text = $"{InventoryItemFind()}";
    }

    /// <summary>
    /// 넘겨줄 데이터값이 Null일시 실행시킬 메서드 (데이터 초기화)
    /// </summary>
    public void DescriptionDataNull()
    {
        // 인덱스값을 사용하지 않는 값으로 설정
        temporaryItemData.index = 1000;

        DescriptionAlphaZero();
    }

    /// <summary>
    /// 셋팅할 아이템정보가 존재한다면 실행
    /// </summary>
    private void DescriptionAlphaOne()
    {
        description.color = AlphaSet(description.color, 1);
        itemImg.color = AlphaSet(itemImg.color, 1);
        itemBelongingsCount.color = AlphaSet(itemBelongingsCount.color, 1);
        backImg.color = AlphaSet(backImg.color, 1);
        backTitle.color = AlphaSet(backTitle.color, 1);
        itemName.color =  AlphaSet(itemName.color, 1);
    }

    /// <summary>
    /// 셋팅할 아이템정보가 존재하지 않는다면 실행
    /// </summary>
    private void DescriptionAlphaZero()
    {
        StartCoroutine(AlhpaZero());

        IEnumerator AlhpaZero()
        {
            float duraction = 2f;
            float time = 0;

            while(time < duraction)
            {
                time += Time.deltaTime;

                description.color = Color.Lerp(description.color, AlphaSet(description.color,0), time / 2);
                itemImg.color = Color.Lerp(itemImg.color, AlphaSet(itemImg.color, 0), time / 2);
                itemBelongingsCount.color = Color.Lerp(itemBelongingsCount.color, AlphaSet(itemBelongingsCount.color, 0), time / 2);
                backImg.color = Color.Lerp(backImg.color, AlphaSet(backImg.color, 0), time / 2);
                backTitle.color = Color.Lerp(backTitle.color, AlphaSet(backTitle.color, 0), time / 2);
                itemName.color = Color.Lerp(itemName.color, AlphaSet(itemName.color, 0), time / 2);

                yield return null;
            }
        }
    }

    /// <summary>
    /// 알파값을 셋팅해주는 메소드
    /// </summary>
    /// <param name="setColor"></param>
    /// <param name="alpha"></param>
    private Color AlphaSet(Color setColor,float alpha)
    {
        return new Color(setColor.r, setColor.g, setColor.b, alpha);
    }

    /// <summary>
    /// 인벤토리안에 있는 해당 아이템의 갯수 정보를 가져올 메소드
    /// </summary>
    /// <returns></returns>
    private int InventoryItemFind()
    {
        // 인벤토리 리스트
        var inven = ShopInvenWindowUI.Inventory;

        // 해당 아이템의 개수
        int thisCount = 0;

        for(int i =0; i < inven.Count; ++i)
        {
            // 인벤토리에 있는 아이템 인덱스와 마우스 위치에 있는 아이템 인덱스가 같다면 값을 더한다
            if(inven[i].itemData != null)
            {
                if (inven[i].itemData.index == temporaryItemData?.index)
                {
                    thisCount += inven[i].itemData.currentHandCount;
                }
            }
        }

        return thisCount;
    }
}
