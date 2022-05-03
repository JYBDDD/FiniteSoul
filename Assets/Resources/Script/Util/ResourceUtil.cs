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
    /// �̾��ϱ� �Ҽ��ִ� �������� Ȯ���ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");
        string jsonData = File.ReadAllText(path);
        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(jsonData);

        // �������� �ε����� 1000 ���� Ŭ ���
        if (playerVolatilityData.stageIndex > 1000)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// �̾��ϱ� ������ �������� �޼��� , �����ϱ� ��� playerVol(�ֹ߼�)������ ������� ������ ��
    /// </summary>
    public static UsePlayerData LoadSaveFile()
    {
        var textAsset = Path.Combine(Application.dataPath +"/Resources/Document/SaveData/SaveData.json");
        string path = File.ReadAllText(textAsset);

        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(path);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == playerVolatilityData.index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        // �κ��丮 ���� ������ ����
        LoadInvenSaveFile();

        return new UsePlayerData(growthStatData, playerData, playerVolatilityData);
    }

    /// <summary>
    /// �κ��丮�� ����� �����͸� �������� �޼���
    /// </summary>
    public static void LoadInvenSaveFile()
    {
        var textAsset = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");
        string path = File.ReadAllText(textAsset);

        List<InvenSaveData> invenList = JsonConvert.DeserializeObject<InvenSaveData[]>(path).ToList();

        var fullItem = GameManager.Instance.FullData.itemsData;

        // ����� �����Ͱ� �����Ѵٸ� ����
        if (invenList.Count > 0)
        {
            for (int i = 0; i < invenList.Count; ++i)
            {
                // ���� ���� �̶�� �κ��丮�� ���� ����
                if (invenList[i].index > 1000)
                {
                    // ����� �������� �������ε����� ��ü������ ������ �ε����� ���ٸ� ������
                    UseItemData IData = fullItem.Where(_ => _.index == invenList[i].index).FirstOrDefault();

                    // ������ �̹��� �� ������ ����
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].ImageDataSetting(IData);
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].ItemCountSetting(invenList[i]);

                }
                // ���� ������ �ƴ϶�� ����
                if(invenList[i].index <= 1000)
                {
                    ShopInvenWindowUI.Inventory[i].ImageDataSetting();
                    ShopInvenWindowUI.Inventory[i].TextCountAlpha();
                }

            }
        }
    }

    /// <summary>
    /// FullDataCollection �� �����͸� �����ϴ� �޼��� (GameManager.Awake() ���� ȣ��)
    /// </summary>
    public static void InsertDataSetting()
    {
        var fullData = GameManager.Instance.FullData;

        // �÷��̾� ������
        fullData.playersData = ParsingJsonData<UsePlayerData>("Player").ToList(); 

        // ���� ������
        fullData.growthsData = ParsingJsonData<GrowthStatData>("GrowthStat").ToList();

        // ���� ������
        fullData.monstersData = ParsingJsonData<UseMonsterData>("Monster").ToList();

        // �������� ������
        fullData.stagesData = ParsingJsonData<StageData>("Stage").ToList();

        // ������ ������
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

            return JsonConvert.DeserializeObject<T[]>(Resources.Load<TextAsset>($"Document/Json/{name}").ToString());           /// -> �ٸ� �κе鵵 �̷��� �ٲ�� TODO
        }
    }

    /// <summary>
    /// Resources �������� ������ ������(��� ����)   / Ǯ������ �����ϴ°��� �ƴ� (���ǡ�)
    /// </summary>
    /// <param name="path"></param>
    public static GameObject InsertPrefabs(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
    }

    /// <summary>
    /// ������Ʈ�� ��ġ,���Ⱚ ����
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
    /// ��ƼŬ ����Ʈ ���� �� ����
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
    /// ������ ����
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
        // ���� ���� ��ġ
        var path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");

        File.WriteAllText(path, JsonUtility.ToJson(playerVolatilityData, true));

        // �κ��丮 ������ ����
        InvenSaveData();

        // ������ ���̽� ���ΰ�ħ
        //AssetDatabase.Refresh();
    }

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public static void InvenSaveData()
    {
        // �κ� ������ ���� ��ġ
        var path = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");

        var inventory = ShopInvenWindowUI.Inventory;
        List<InvenSaveData> invenSaves = new List<InvenSaveData>();
        for(int i = 0; i < inventory.Count;++i)
        {
            // i = �κ��丮 �ε���, �ش� �κ��丮 �ε����� ������ ������ ����
            var data = new InvenSaveData(i, inventory[i]);
            invenSaves.Add(data);
        }

        File.WriteAllText(path, JsonConvert.SerializeObject(invenSaves));

        // ������ ���̽� ���ΰ�ħ
        //AssetDatabase.Refresh();
    }

    /// <summary>
    /// �����ϱ� �����ͷ� �ʱ�ȭ �ϴ� �޼���
    /// </summary>
    public static void NewDataReturn(int characterIndex)
    {
        // �÷��̾� �⺻ ���� ���� ��ġ
        var path1 = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/SaveData.json");
        // �÷��̾� �κ��丮 ���� ���� ��ġ
        var path2 = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");

        File.WriteAllText(path1, JsonUtility.ToJson(new PlayerVolatilityData(characterIndex), true));

        File.WriteAllText(path2, JsonConvert.SerializeObject(InventoryRefreash()));

        // ������ ���̽� ���ΰ�ħ
        //AssetDatabase.Refresh();
        
        // �κ��丮 ��ü�� �ʱ�ȭ
        List<InvenSaveData> InventoryRefreash()
        {
            var textAsset = Path.Combine(Application.dataPath + "/Resources/Document/SaveData/InvenSaveData.json");
            string path = File.ReadAllText(textAsset);

            List<InvenSaveData> invenList = JsonConvert.DeserializeObject<InvenSaveData[]>(path).ToList();

            for (int i = 0; i < invenList.Count; ++i)
            {
                // ��� �ʱ�ȭ
                invenList[i] = new InvenSaveData(1000);
            }

            return invenList;
        }
    }
}
