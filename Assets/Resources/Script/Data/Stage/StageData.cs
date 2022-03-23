using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData : StaticData
{
    /// <summary>
    /// 스테이지 이름
    /// </summary>
    public string name;

    /// <summary>
    /// 스테이지에서 생성하는 몬스터 인덱스
    /// </summary>
    public int monsterIndex;
}
