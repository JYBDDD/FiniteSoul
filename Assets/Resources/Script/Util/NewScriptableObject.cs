using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSpawn", menuName = "MonsterSpawnsPos")]
public class NewScriptableObject : ScriptableObject
{
    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    public int stageIndex;
    /// <summary>
    /// 스폰할 몬스터 인덱스 , Locations 위치와 같은 인덱스로 해당 몬스터를 스폰할것
    /// </summary>
    public int[] monsterIndex;
    /// <summary>
    /// 스폰 위치
    /// </summary>
    public Vector3[] locations;
}
