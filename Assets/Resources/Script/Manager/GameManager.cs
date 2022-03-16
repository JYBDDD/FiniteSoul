using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        InsertDataSetting();
    }

    /// <summary>
    /// 총괄 데이터
    /// </summary>
    public static FullDataCollection FullData { get => Instance.fullData; set => value = Instance.fullData; }
    private FullDataCollection fullData = new FullDataCollection();


    #region 데이터 삽입 및 변환부
    /// <summary>
    /// FullDataCollection 에 데이터 삽입
    /// </summary>
    private void InsertDataSetting()
    {
        // 플레이어 데이터
        var PlayerData = JsonConvertData<UsePlayerData>("Player");
        // TODO 데이터가 배열이 아닌것같은데..? 확인해보고 하나만 들어가면 수정하기

        // 성장 데이터



        // 몬스터 데이터
    }

    /// <summary>
    /// Json 데이터 변환
    /// </summary>
    private T JsonConvertData<T>(string path)
    {
        var textAssetData = Resources.Load<TextAsset>($"Document/Json/{path}");

        return JsonUtility.FromJson<T>(textAssetData.ToString());
    }
    #endregion
}
