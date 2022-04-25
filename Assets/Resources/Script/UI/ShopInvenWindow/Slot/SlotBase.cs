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
    public UseItemData itemData = null;

    [SerializeField,Tooltip("아이템 이미지를 출력시켜줄 자식 이미지")]
    protected Image itemImg;

    /// <summary>
    /// 정상 아이템 데이터가 들어온다면 이미지를 셋팅할 메소드 (호출용)
    /// </summary>
    public void ImageDataSetting(UseItemData useItemData = null)
    {
        // 들어온 데이터값이 Null 일경우, Null로 데이터 셋팅
        if (useItemData == null)
        {
            itemData = null;
        }

        // 정상적인 데이터가 들어왔을경우 데이터 삽입 및 이미지 삽입
        if (useItemData != null)
        {
            itemData = new UseItemData(useItemData);

            itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        }

        ItemAlphaSet(itemImg);
    }

    /// <summary>
    /// 아이템 이미지 알파값 셋팅
    /// </summary>
    protected void ItemAlphaSet(Image setImage)
    {
        var itemColor = setImage.color;
        if (itemData == null)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 0f / 0f);
        }
        if (itemData != null)
        {
            setImage.color = new Color(itemColor.r, itemColor.g, itemColor.b, 255f / 255f);
        }

    }
}
