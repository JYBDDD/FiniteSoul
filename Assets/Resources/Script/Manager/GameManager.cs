using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

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
    public FullDataCollection FullData { get => Instance.fullData;}
    private FullDataCollection fullData = new FullDataCollection();

    #region 데이터 삽입 및 변환부
    /// <summary>
    /// FullDataCollection 에 데이터 삽입
    /// </summary>
    private void InsertDataSetting()
    {
        // 플레이어 데이터
        UsePlayerData[] arrPlayerData;
        ParsingJsonData("Player");

        // 성장 데이터
        GrowthStatData[] arrGrowthData;
        ParsingJsonData("GrowthStat");

        // 몬스터 데이터 삽입 추가 TODO

        // 스테이지 데이터
        StageData[] arrStageData;
        ParsingJsonData("Stage");

        void ParsingJsonData(string name)
        {
            string path = Path.Combine(Application.dataPath , $"Resources/Document/Json");

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, name), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            arrPlayerData = JsonConvert.DeserializeObject<UsePlayerData[]>(jsonData);

            if (name == "Player" && FullData.playersData.Count <= 0)        // 한번만 호출되도록 설정 (Player 데이터 로드)
            {
                for (int i = 0; i < arrPlayerData.Length; ++i)
                {
                    FullData.playersData.Add(arrPlayerData[i]);
                }
            }
            else if (name == "GrowthStat" && FullData.growthsData.Count <= 0)       // 한번만 호출되도록 설정 (GrowthStat 데이터 로드)
            {
                arrGrowthData = JsonConvert.DeserializeObject<GrowthStatData[]>(jsonData);

                for (int i = 0; i < arrGrowthData.Length; ++i)
                {
                    FullData.growthsData.Add(arrGrowthData[i]);
                }
            }
            else if(name == "Stage" && FullData.stagesData.Count <= 0)
            {
                arrStageData = JsonConvert.DeserializeObject<StageData[]>(jsonData);

                for (int i = 0; i < arrStageData.Length; ++i)
                {
                    FullData.stagesData.Add(arrStageData[i]);
                }
            }
            

            

        }
    }
    #endregion
}
