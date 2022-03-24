using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class ResoureUtil : MonoBehaviour
{
    /// <summary>
    /// 소수점 세자릿수까지만 출력하는 메서드(Vector3 용)
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public static Vector3 RoundVecFloat(Vector3 vec)
    {
        string vecXstring = string.Format("{0:N3}", vec.x);
        string vecYstring = string.Format("{0:N3}", vec.y);
        string vecZstring = string.Format("{0:N3}", vec.z);

        float vecX = float.Parse(vecXstring);
        float vecY = float.Parse(vecYstring);
        float vecZ = float.Parse(vecZstring);

        Vector3 vector3 = new Vector3(vecX, vecY, vecZ);
        return vector3;
    }

    /// <summary>
    /// 소주점 세자릿수까지만 출력하는 메서드(Quaternion 용)
    /// </summary>
    /// <param name="qut"></param>
    /// <returns></returns>
    public static Quaternion RoundRotFloat(Quaternion qut)
    {
        string qutXstring = string.Format("{0:N3}", qut.x);
        string qutYstring = string.Format("{0:N3}", qut.y);
        string qutZstring = string.Format("{0:N3}", qut.z);

        float qutX = float.Parse(qutXstring);
        float qutY = float.Parse(qutYstring);
        float qutZ = float.Parse(qutZstring);

        Quaternion quaternion = new Quaternion(qutX, qutY, qutZ,0);
        return quaternion;
    }


    /// <summary>
    /// 이어하기 할수있는 파일인지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Resources.Load<TextAsset>("Document/SaveData/SaveData.json").ToString();

        PlayerVolatilityData playerVolatilityData = JsonUtility.FromJson<PlayerVolatilityData>(path);

        // 스테이지 인덱스가 1000 이 아닐경우 이어하기 할 수있는 파일
        if(playerVolatilityData.stageIndex != 1000)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 이어하기 파일을 가져오는 메서드 , 새로하기 라면 playerVol(휘발성)내용을 사용하지 않으면 됨
    /// </summary>
    public static UsePlayerData LoadSaveFile()
    {
        string path = Resources.Load<TextAsset>("Document/SaveData/SaveData.json").ToString();

        PlayerVolatilityData playerVolatilityData = JsonUtility.FromJson<PlayerVolatilityData>(path);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == playerVolatilityData.index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        return new UsePlayerData(growthStatData,playerData,playerVolatilityData);
    }

    /// <summary>
    /// FullDataCollection 에 데이터를 삽입하는 메서드 (GameManager.Awake() 에서 호출)
    /// </summary>
    public static void InsertDataSetting()
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
            string path = Path.Combine(Application.dataPath, $"Resources/Document/Json");

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, name), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            arrPlayerData = JsonConvert.DeserializeObject<UsePlayerData[]>(jsonData);

            if (name == "Player" && GameManager.Instance.FullData.playersData.Count <= 0)        // 한번만 호출되도록 설정 (Player 데이터 로드)
            {
                for (int i = 0; i < arrPlayerData.Length; ++i)
                {
                    GameManager.Instance.FullData.playersData.Add(arrPlayerData[i]);
                }
            }
            else if (name == "GrowthStat" && GameManager.Instance.FullData.growthsData.Count <= 0)       // 한번만 호출되도록 설정 (GrowthStat 데이터 로드)
            {
                arrGrowthData = JsonConvert.DeserializeObject<GrowthStatData[]>(jsonData);

                for (int i = 0; i < arrGrowthData.Length; ++i)
                {
                    GameManager.Instance.FullData.growthsData.Add(arrGrowthData[i]);
                }
            }
            else if (name == "Stage" && GameManager.Instance.FullData.stagesData.Count <= 0)
            {
                arrStageData = JsonConvert.DeserializeObject<StageData[]>(jsonData);

                for (int i = 0; i < arrStageData.Length; ++i)
                {
                    GameManager.Instance.FullData.stagesData.Add(arrStageData[i]);
                }
            }




        }
    }

}
