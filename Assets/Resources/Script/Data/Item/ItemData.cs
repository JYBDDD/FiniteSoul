using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData : StaticData
{
    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string name;

    /// <summary>
    /// 사용가능한 직업 인덱스 (1000 일시 공용)
    /// </summary>
    public int jobIndex;

    /// <summary>
    /// 아이템 타입 (장비,소비,기타)
    /// </summary>
    public Define.ItemMixEnum.ItemType itemType;

    /// <summary>
    /// 아이템 착용타입 (한손, 두손)
    /// </summary>
    public Define.ItemMixEnum.ItemHandedType handed;

    /// <summary>
    /// 최대 소지가능한 개수 (인벤토리)
    /// </summary>
    public int maxHandCount;

    /// <summary>
    /// 드랍 확률
    /// </summary>
    public float dropPer;

    /// <summary>
    /// 아이템 가격
    /// </summary>
    public float salePrice;

    /// <summary>
    /// 이미지(택스쳐) 경로
    /// </summary>
    public string resourcePath;
}
