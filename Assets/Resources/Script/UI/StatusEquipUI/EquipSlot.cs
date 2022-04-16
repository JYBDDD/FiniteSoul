using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비슬롯 클래스
/// </summary>
public class EquipSlot : MonoBehaviour
{
    /// <summary>
    /// 해당 슬롯에 담을수 있는 아이템 타입
    /// </summary>
    [SerializeField]
    public string AbleToItemtype;

    /// <summary>
    /// 아이템 데이터의 이미지
    /// </summary>
    Image itemImg;

    /// <summary>
    /// 아이템 데이터를 들고있는 변수
    /// </summary>
    public UseItemData itemData;

    private void Start()
    {
        ImgAbleSetting();
    }

    /// <summary>
    /// 해당 데이터가 정상값이 아니라면 해당 이미지를 비활성화 / 아니라면 활성화
    /// </summary>
    public void ImgAbleSetting()
    {
        itemImg ??= GetComponent<Image>();

        if (itemData.index >= 1001)
        {
            itemImg.enabled = true;
            itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);
        }
        if (itemData.index < 1001)
        {
            itemImg.enabled = false;
        }
    }

    /// <summary>
    /// 아이템 데이터를 셋팅한다
    /// </summary>
    public void ItemDataSetting(UseItemData itemData)
    {
        this.itemData = new UseItemData(itemData);
        ImgAbleSetting();
    }
}
