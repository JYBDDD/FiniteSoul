using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 총괄 데이터
    /// </summary>
    public static FullDataCollection FullData { get => Instance.fullData; set => value = Instance.fullData; }
    private FullDataCollection fullData = new FullDataCollection();


    // 데이터 FullDataCollection 삽입부 여기에 만들 것임 TODO
}
