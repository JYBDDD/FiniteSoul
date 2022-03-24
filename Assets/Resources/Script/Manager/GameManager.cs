using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        ResoureUtil.InsertDataSetting();
    }

    /// <summary>
    /// รัฐý ตฅภฬลอ
    /// </summary>
    public FullDataCollection FullData { get => Instance.fullData;}
    private FullDataCollection fullData = new FullDataCollection();

}
