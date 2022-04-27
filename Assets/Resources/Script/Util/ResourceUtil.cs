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

        // 인벤토리 저장 데이터 삽입
        LoadInvenSaveFile();

        return new UsePlayerData(growthStatData, playerData, playerVolatilityData);
    }

    /// <summary>
    /// 인벤토리의 저장된 데이터를 가져오는 메서드
    /// </summary>
    public static void LoadInvenSaveFile()
    {
        var textAsset = Resources.Load<TextAsset>("Document/SaveData/InvenSaveData");
        string path = textAsset.ToString();

        List<InvenSaveData> invenList = JsonConvert.DeserializeObject<List<InvenSaveData>>(path);

        var fullItem = GameManager.Instance.FullData.itemsData;

        // 저장된 데이터가 존재한다면 실행
        if (invenList.Count > 0)
        {
            for (int i = 0; i < invenList.Count; ++i)
            {
                // 값이 정상값 이라면 인벤토리에 값을 삽입
                if (invenList[i].index > 1000)
                {
                    // 저장된 데이터의 아이템인덱스와 전체데이터 아이템 인덱스가 같다면 가져옴
                    UseItemData IData = fullItem.Where(_ => _.index == invenList[i].index).FirstOrDefault();

                    // 아이템 이미지 및 데이터 셋팅
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].ImageDataSetting(IData);
                    // 해당 인벤토리에 데이터 삽입, 갯수 설정
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].itemData.currentHandCount = IData.currentHandCount;
                    
                }
            }
        }
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
    /// Resources 폴더에서 가져올 프리팹(즉시 생성)   / 풀링으로 생성하는것이 아님 (주의※)
    /// </summary>
    /// <param name="path"></param>
    public static GameObject InsertPrefabs(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
    }

    /// <summary>
    /// 오브젝트의 위치,방향값 지정
    /// </summary>
    /// <param name="thisObject"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public static void PosDirectionDesign(GameObject thisObject,Vector3 pos,Quaternion rot)
    {
        thisObject.transform.position = pos;
        thisObject.transform.rotation = rot;
    }

    /// <summary>
    /// 파티클 이펙트 생성 및 설정
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <param name="ac"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    public static void ParticleInit(string path, Define.CharacterType type, AttackController ac, Vector3 pos, Quaternion rot)
    {
        if (ac.checkBool == true)
        {
            GameObject particleObj = ObjectPoolManager.Instance.GetPool<ParticleChild>(path, type);
            PosDirectionDesign(particleObj, pos, rot);
        }
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
    public static void SaveData(PlayerVolatilityData playerVolatilityData)
    {
        // 저장 파일 위치
        var path = "Assets/Resources/Document/SaveData/SaveData.json";

        File.WriteAllText(path, JsonUtility.ToJson(playerVolatilityData, true));

        // 인벤토리 데이터 저장
        InvenSaveData();

        // 데이터 베이스 새로고침
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 인벤토리 데이터 저장
    /// </summary>
    public static void InvenSaveData()
    {
        // 인벤 데이터 저장 위치
        var path = "Assets/Resources/Document/SaveData/InvenSaveData.json";

        var inventory = ShopInvenWindowUI.Inventory;
        List<InvenSaveData> invenSaves = new List<InvenSaveData>();
        for(int i = 0; i < inventory.Count;++i)
        {
            // 정상값이라면 데이터 삽입
            if(inventory[i].itemData.index > 1000)
            {
                var data = new InvenSaveData(i, inventory[i]);
                invenSaves.Add(data);
            }
        }

        File.WriteAllText(path, JsonUtility.ToJson(invenSaves, true));

        // 데이터 베이스 새로고침
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 새로하기 데이터로 초기화 하는 메서드
    /// </summary>
    public static void NewDataReturn(int characterIndex)
    {
        // 플레이어 기본 저장 파일 위치
        var path1 = "Assets/Resources/Document/SaveData/SaveData.json";
        // 플레이어 인벤토리 저장 파일 위치
        var path2 = "Assets/Resources/Document/SaveData/InvenSaveData.json";

        File.WriteAllText(path1, JsonUtility.ToJson(new PlayerVolatilityData(characterIndex), true));
        File.WriteAllText(path2, JsonUtility.ToJson(new InvenSaveData(1000), true));

        // 데이터 베이스 새로고침
        AssetDatabase.Refresh();
        
    }

    
}
