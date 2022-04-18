using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ResourceUtil : MonoBehaviour
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

        Quaternion quaternion = new Quaternion(qutX, qutY, qutZ, 0);
        return quaternion;
    }


    /// <summary>
    /// 이어하기 할수있는 파일인지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Resources.Load<TextAsset>("Document/SaveData/SaveData").ToString();

        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(path);

        // 스테이지 인덱스가 1000 보다 클 경우
        if (playerVolatilityData.stageIndex > 1000)
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
        var textAsset = Resources.Load<TextAsset>("Document/SaveData/SaveData");
        string path = textAsset.ToString();

        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(path);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == playerVolatilityData.index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        return new UsePlayerData(growthStatData, playerData, playerVolatilityData);
    }

    /// <summary>
    /// FullDataCollection 에 데이터를 삽입하는 메서드 (GameManager.Awake() 에서 호출)
    /// </summary>
    public static void InsertDataSetting()
    {
        var fullData = GameManager.Instance.FullData;

        // 플레이어 데이터
        fullData.playersData = ParsingJsonData<UsePlayerData>("Player").ToList();

        // 성장 데이터
        fullData.growthsData = ParsingJsonData<GrowthStatData>("GrowthStat").ToList();

        // 몬스터 데이터
        fullData.monstersData = ParsingJsonData<UseMonsterData>("Monster").ToList();

        // 스테이지 데이터
        fullData.stagesData = ParsingJsonData<StageData>("Stage").ToList();

        // 아이템 데이터
        fullData.itemsData = ParsingJsonData<UseItemData>("Item").ToList();


        T[] ParsingJsonData<T>(string name)
        {
            string path = Path.Combine(Application.dataPath, $"Resources/Document/Json");

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, name), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<T[]>(jsonData);
        }
    }

    /// <summary>
    /// Resources 폴더에서 가져올 프리팹(즉시 생성)
    /// </summary>
    /// <param name="path"></param>
    public static GameObject InsertPrefabs(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    /// <param name="index"></param>
    /// <param name="stageIndex"></param>
    /// <param name="pos"></param>
    /// <param name="rune"></param>
    /// <param name="raiseHp"></param>
    /// <param name="currentHp"></param>
    /// <param name="raiseAtk"></param>
    /// <param name="raiseDef"></param>
    public static void SaveData(UsePlayerData playerData, Vector3 pos, StageData stageData)
    {
        // 저장 파일 위치
        var path = "Assets/Resources/Document/SaveData/SaveData.json";

        // 인벤토리 저장 데이터도 만들어야함 TODO

        File.WriteAllText(path, JsonUtility.ToJson(new PlayerVolatilityData(playerData, pos, stageData), true));

        // 데이터 베이스 새로고침
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 새로하기 데이터로 초기화 하는 메서드
    /// </summary>
    public static void NewDataReturn(int characterIndex)
    {
        // 저장 파일 위치
        var path = "Assets/Resources/Document/SaveData/SaveData.json";
        
        File.WriteAllText(path, JsonUtility.ToJson(new PlayerVolatilityData(characterIndex), true));

        // 데이터 베이스 새로고침
        AssetDatabase.Refresh();
        
    }

    
}
