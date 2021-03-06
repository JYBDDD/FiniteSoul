using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 Moveable이 가지고 있는 데이터
/// </summary>
[Serializable]
public class StaticData
{
    /// <summary>
    /// 모든 데이터가 가지고 있는 인덱스
    /// </summary>
    public int index;

    /// <summary>
    /// 플레이어인지 몬스터인지 구분해주는 타입
    /// </summary>
    public Define.CharacterType characterType = Define.CharacterType.None;
}
