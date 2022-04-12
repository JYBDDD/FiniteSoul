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

        Quaternion quaternion = new Quaternion(qutX, qutY, qutZ,0);
        return quaternion;
    }


    /// <summary>
    /// �̾��ϱ� �Ҽ��ִ� �������� Ȯ���ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    public static bool LoadConfirmFile()
    {
        string path = Resources.Load<TextAsset>("Document/SaveData/SaveData").ToString();

        PlayerVolatilityData playerVolatilityData = JsonUtility.FromJson<PlayerVolatilityData>(path);

        // �������� �ε����� 1000 �� �ƴҰ�� �̾��ϱ� �� ���ִ� ����
        if(playerVolatilityData.stageIndex != 1000)
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

        PlayerVolatilityData[] playerVolatilityData = JsonConvert.DeserializeObject<PlayerVolatilityData[]>(path);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == playerVolatilityData[0].index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        return new UsePlayerData(growthStatData,playerData,playerVolatilityData[0]);
    }

    /// <summary>
    /// FullDataCollection �� �����͸� �����ϴ� �޼��� (GameManager.Awake() ���� ȣ��)
    /// </summary>
    public static void InsertDataSetting()
    {
        // �÷��̾� ������
        UsePlayerData[] arrPlayerData;
        ParsingJsonData("Player");

        // ���� ������
        GrowthStatData[] arrGrowthData;
        ParsingJsonData("GrowthStat");

        // ���� ������
        UseMonsterData[] arrMonsterData;
        ParsingJsonData("Monster");

        // �������� ������
        StageData[] arrStageData;
        ParsingJsonData("Stage");

        // ������ ������
        ItemData[] arrItemData;
        ParsingJsonData("Item");

        void ParsingJsonData(string name)
        {
            string path = Path.Combine(Application.dataPath, $"Resources/Document/Json");

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, name), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            arrPlayerData = JsonConvert.DeserializeObject<UsePlayerData[]>(jsonData);

            if (name == "Player" && GameManager.Instance.FullData.playersData.Count <= 0)        // �ѹ��� ȣ��ǵ��� ���� (Player ������ �ε�)
            {
                for (int i = 0; i < arrPlayerData.Length; ++i)
                {
                    GameManager.Instance.FullData.playersData.Add(arrPlayerData[i]);
                }
            }
            else if (name == "GrowthStat" && GameManager.Instance.FullData.growthsData.Count <= 0)       // �ѹ��� ȣ��ǵ��� ���� (GrowthStat ������ �ε�)
            {
                arrGrowthData = JsonConvert.DeserializeObject<GrowthStatData[]>(jsonData);

                for (int i = 0; i < arrGrowthData.Length; ++i)
                {
                    GameManager.Instance.FullData.growthsData.Add(arrGrowthData[i]);
                }
            }
            else if (name == "Stage" && GameManager.Instance.FullData.stagesData.Count <= 0)        // �ѹ��� ȣ��ǵ��� ���� (StageData ������ �ε�)
            {
                arrStageData = JsonConvert.DeserializeObject<StageData[]>(jsonData);

                for (int i = 0; i < arrStageData.Length; ++i)
                {
                    GameManager.Instance.FullData.stagesData.Add(arrStageData[i]);
                }
            }
            else if (name == "Monster" && GameManager.Instance.FullData.monstersData.Count <= 0)        // �ѹ��� ȣ��ǵ��� ���� (MonsterData ������ �ε�)
            {
                arrMonsterData = JsonConvert.DeserializeObject<UseMonsterData[]>(jsonData);

                for (int i = 0; i < arrMonsterData.Length; ++i)
                {
                    GameManager.Instance.FullData.monstersData.Add(arrMonsterData[i]);
                }
            }
            else if (name == "Item" && GameManager.Instance.FullData.itemsData.Count <= 0)        // �ѹ��� ȣ��ǵ��� ���� (ItemData ������ �ε�)
            {
                arrItemData = JsonConvert.DeserializeObject<ItemData[]>(jsonData);

                for (int i = 0; i < arrItemData.Length; ++i)
                {
                    GameManager.Instance.FullData.itemsData.Add(arrItemData[i]);
                }
            }



        }
    }

    /// <summary>
    /// Resources �������� ������ ������(��� ����)
    /// </summary>
    /// <param name="path"></param>
    public static GameObject InsertPrefabs(string path)
    {
        return Instantiate(Resources.Load<GameObject>(path));
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
    public static void SaveData(int index, int level,int stageIndex, Vector3 pos, float rune, float raiseHp, float currentHp, float raiseAtk, float raiseDef)
    {
        // ���� ���� ��ġ
        var path = Resources.Load("Document/SaveData/SaveData.json");
        // ������ �÷��̾� ������ -> ���� ���� ��ġ �����

        var json = JsonUtility.ToJson(new PlayerVolatilityData(index, level, stageIndex, pos, rune, raiseHp, currentHp, raiseAtk, raiseDef),true);
        File.WriteAllText("path", json);

    }

}
