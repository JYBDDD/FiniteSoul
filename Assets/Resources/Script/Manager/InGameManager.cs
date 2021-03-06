using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임에 관련된 데이터를 들고있을 매니저 클래스
/// </summary>
public class InGameManager : Singleton<InGameManager>
{
    // 플레이어
    public PlayerController Player = null;
    // 몬스터 리스트
    public List<MonsterController> Monsters = new List<MonsterController>();



    /// <summary>
    /// 리스트에 해당 몬스터 객체 추가
    /// </summary>
    /// <param name="addObject"></param>
    public void MonsterRegist(MonsterController addObject)
    {
        Monsters.Add(addObject);
    }

    /// <summary>
    /// 리스트에 해당 몬스터 객체 삭제
    /// </summary>
    /// <param name="removeObject"></param>
    public void MonsterRegistRemove(MonsterController removeObject)
    {
        Monsters.Remove(removeObject);
    }

    /// <summary>
    /// 플레이어 인게임매니저 등록
    /// </summary>
    public void PlayerRegist(PlayerController player)
    {
        this.Player = player;
    }

    /// <summary>
    /// 인게임에 등록된, 플레이어, 몬스터 리스트를 클리어
    /// </summary>
    public void ClearInGame()
    {
        for(int i = 0; i < Monsters.Count; ++i)
        {
            ObjectPoolManager.Instance.GetPush(Monsters[i].gameObject);
        }

        if(Player != null)
        {
            ObjectPoolManager.Instance.GetPush(Player.gameObject);
        }

        Player = null;
        Monsters?.Clear();
    }

}
