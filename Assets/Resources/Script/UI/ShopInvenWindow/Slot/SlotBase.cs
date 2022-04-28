using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 모든 슬롯의 베이스
/// </summary>
public class SlotBase : MonoBehaviour
{
    /// <summary>
    /// 아이템 데이터를 담아둘 변수
    /// </summary>
    public UseItemData itemData = new UseItemData();

    [SerializeField,Tooltip("아이템 이미지를 출력시켜줄 자식 이미지")]
    protected Image itemImg;

    /// <summary>
    /// 정상 아이템 데이터가 들어온다면 이미지를 셋팅할 메소드 (호출용)
    /// </summary>
    public void ImageDataSetting(UseItemData useItemData = null)
    {
        // 정상적인 데이터가 들어왔을경우 데이터 삽입 및 이미지 삽입
        if (useItemData != null)
        {
            itemData = new UseItemData(useItemData);

            itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        }
        // Null 값이 들어왔다면 사용불가능한 값으로 변경
        if(useItemData == null)
        {
            itemData.index = 1000;
        }

        ItemAlphaSet(itemImg, useItemData);
    }

    /// <summary>
    /// 아이템 이미지 알파값 셋팅
    /// </summary>
    protected void ItemAlphaSet(Image setImage, UseItemData useItemData = null)
    {
        var itemColor = setImage.color;
        if (useItemData?.index < 1000 | useItemData == null)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 0f / 0f);
        }
        if (useItemData?.index > 1000)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 255f / 255f);
        }

    }
}
