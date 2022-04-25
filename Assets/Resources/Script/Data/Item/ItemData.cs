using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 데이터
/// </summary>
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
    /// 현재 소지 개수
    /// </summary>
    public int currentHandCount;

    /// <summary>
    /// 드랍 확률
    /// </summary>
    public float dropPer;

    /// <summary>
    /// 아이템 가격
    /// </summary>
    public float salePrice;

    /// <summary>
    /// 아이템 설명
    /// </summary>
    public string description;

    /// <summary>
    /// 이미지(택스쳐) 경로
    /// </summary>
    public string resourcePath;
}
