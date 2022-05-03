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
    /// 이어하기 할수있는 파일인지 확인하는 메서드
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");
        string jsonData = File.ReadAllText(path);
        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(jsonData);

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
        var textAsset = Path.Combine(Application.dataPath +"/Resources/Document/SaveData/SaveData.json");
        string path = File.ReadAllText(textAsset);

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
        var textAsset = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");
        string path = File.ReadAllText(textAsset);

        List<InvenSaveData> invenList = JsonConvert.DeserializeObject<InvenSaveData[]>(path).ToList();

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
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].ItemCountSetting(invenList[i]);

                }
                // 값이 정상값이 아니라면 실행
                if(invenList[i].index <= 1000)
                {
                    ShopInvenWindowUI.Inventory[i].ImageDataSetting();
                    ShopInvenWindowUI.Inventory[i].TextCountAlpha();
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
            //string path = Path.Combine(Application.dataPath + "/Resources/Document/Json");
            //string path = Path.Combine(Application.dataPath + Resources.Load<TextAsset>($"Document/Json/{name}").ToString());
            //FileStream fileStream = new FileStream(path, FileMode.Open);
            //byte[] data = new byte[fileStream.Length];
            //fileStream.Read(data, 0, data.Length);
            //fileStream.Close();
            //string jsonData = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<T[]>(Resources.Load<TextAsset>($"Document/Json/{name}").ToString());           /// -> 다른 부분들도 이렇게 바꿔라 TODO
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
            GameObject particleObj = ObjectPoolManager.Instance.GetPool<ParticleChild>(path,pos,type);
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
        var path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");

        File.WriteAllText(path, JsonUtility.ToJson(playerVolatilityData, true));

        // 인벤토리 데이터 저장
        InvenSaveData();

        // 데이터 베이스 새로고침
        //AssetDatabase.Refresh();
    }

    /// <summary>
    /// 인벤토리 데이터 저장
    /// </summary>
    public static void InvenSaveData()
    {
        // 인벤 데이터 저장 위치
        var path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");

        var inventory = ShopInvenWindowUI.Inventory;
        List<InvenSaveData> invenSaves = new List<InvenSaveData>();
        for(int i = 0; i < inventory.Count;++i)
        {
            // i = 인벤토리 인덱스, 해당 인벤토리 인덱스의 아이템 데이터 삽입
            var data = new InvenSaveData(i, inventory[i]);
            invenSaves.Add(data);
        }

        File.WriteAllText(path, JsonConvert.SerializeObject(invenSaves));

        // 데이터 베이스 새로고침
        //AssetDatabase.Refresh();
    }

    /// <summary>
    /// 새로하기 데이터로 초기화 하는 메서드
    /// </summary>
    public static void NewDataReturn(int characterIndex)
    {
        // 플레이어 기본 저장 파일 위치
        var path1 = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");
        // 플레이어 인벤토리 저장 파일 위치
        var path2 = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");

        File.WriteAllText(path1, JsonUtility.ToJson(new PlayerVolatilityData(characterIndex), true));

        File.WriteAllText(path2, JsonConvert.SerializeObject(InventoryRefreash()));

        // 데이터 베이스 새로고침
        //AssetDatabase.Refresh();
        
        // 인벤토리 전체값 초기화
        List<InvenSaveData> InventoryRefreash()
        {
            var textAsset = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");
            string path = File.ReadAllText(textAsset);

            List<InvenSaveData> invenList = JsonConvert.DeserializeObject<InvenSaveData[]>(path).ToList();

            for (int i = 0; i < invenList.Count; ++i)
            {
                // 모두 초기화
                invenList[i] = new InvenSaveData(1000);
            }

            return invenList;
        }
    }
}
