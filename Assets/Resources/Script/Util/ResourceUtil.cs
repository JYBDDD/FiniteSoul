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
    /// �Ҽ��� ���ڸ��������� ����ϴ� �޼���(Vector3 ��)
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
    /// ������ ���ڸ��������� ����ϴ� �޼���(Quaternion ��)
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
    /// �̾��ϱ� �Ҽ��ִ� �������� Ȯ���ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Resources.Load<TextAsset>("Document/SaveData/SaveData").ToString();

        PlayerVolatilityData playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData>(path);

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
        var textAsset = Resources.Load<TextAsset>("Document/SaveData/SaveData");
        string path = textAsset.ToString();

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
        var textAsset = Resources.Load<TextAsset>("Document/SaveData/InvenSaveData");
        string path = textAsset.ToString();

        List<InvenSaveData> invenList = JsonConvert.DeserializeObject<List<InvenSaveData>>(path);

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
                    // �ش� �κ��丮�� ������ ����, ���� ����
                    ShopInvenWindowUI.Inventory[invenList[i].invenIndex].itemData.currentHandCount = IData.currentHandCount;
                    
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
            GameObject particleObj = ObjectPoolManager.Instance.GetPool<ParticleChild>(path, type);
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
        var path = "Assets/Resources/Document/SaveData/SaveData.json";

        File.WriteAllText(path, JsonUtility.ToJson(playerVolatilityData, true));

        // �κ��丮 ������ ����
        InvenSaveData();

        // ������ ���̽� ���ΰ�ħ
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// �κ��丮 ������ ����
    /// </summary>
    public static void InvenSaveData()
    {
        // �κ� ������ ���� ��ġ
        var path = "Assets/Resources/Document/SaveData/InvenSaveData.json";

        var inventory = ShopInvenWindowUI.Inventory;
        List<InvenSaveData> invenSaves = new List<InvenSaveData>();
        for(int i = 0; i < inventory.Count;++i)
        {
            // �����̶�� ������ ����
            if(inventory[i].itemData.index > 1000)
            {
                var data = new InvenSaveData(i, inventory[i]);
                invenSaves.Add(data);
            }
        }

        File.WriteAllText(path, JsonUtility.ToJson(invenSaves, true));

        // ������ ���̽� ���ΰ�ħ
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// �����ϱ� �����ͷ� �ʱ�ȭ �ϴ� �޼���
    /// </summary>
    public static void NewDataReturn(int characterIndex)
    {
        // �÷��̾� �⺻ ���� ���� ��ġ
        var path1 = "Assets/Resources/Document/SaveData/SaveData.json";
        // �÷��̾� �κ��丮 ���� ���� ��ġ
        var path2 = "Assets/Resources/Document/SaveData/InvenSaveData.json";

        File.WriteAllText(path1, JsonUtility.ToJson(new PlayerVolatilityData(characterIndex), true));
        File.WriteAllText(path2, JsonUtility.ToJson(new InvenSaveData(1000), true));

        // ������ ���̽� ���ΰ�ħ
        AssetDatabase.Refresh();
        
    }

    
}
