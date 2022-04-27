using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인벤토리 저장데이터 클래스
/// </summary>
[Serializable]
public class InvenSaveData : StaticData
{
    /// <summary>
    /// 인벤토리 인덱스
    /// </summary>
    public int invenIndex;

    /// <summary>
    /// 들고있는 아이템의 갯수
    /// </summary>
    public int currentHandCount;

    /// <summary>
    /// 인벤토리 데이터 저장시 사용되는 생성자
    /// </summary>
    /// <param name="invenIndex"></param>
    /// <param name="invenSlotData"></param>
    public InvenSaveData(int invenIndex,InvenSlot invenSlotData)
    {
        index = invenSlotData.itemData.index;
        this.invenIndex = invenIndex;
        currentHandCount = invenSlotData.itemData.currentHandCount;
    }

    /// <summary>
    /// 인벤토리 데이터 초기화시 사용되는 생성자
    /// </summary>
    /// <param name="returnData"></param>
    public InvenSaveData(int returnData)
    {
        index = returnData;
        invenIndex = 0;
        currentHandCount = 0;
    }
}
