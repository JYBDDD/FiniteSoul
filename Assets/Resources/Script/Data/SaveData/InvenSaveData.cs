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
}
