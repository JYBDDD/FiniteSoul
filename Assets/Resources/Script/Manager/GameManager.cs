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
    /// �Ѱ� ������
    /// </summary>
    public FullDataCollection FullData { get => Instance.fullData;}
    private FullDataCollection fullData = new FullDataCollection();

    #region ������ ���� �� ��ȯ��
    /// <summary>
    /// FullDataCollection �� ������ ����
    /// </summary>
    private void InsertDataSetting()
    {
        // �÷��̾� ������
        UsePlayerData[] arrPlayerData;
        ParsingJsonData("Player");

        // ���� ������
        GrowthStatData[] arrGrowthData;
        ParsingJsonData("GrowthStat");

        // ���� ������ ���� �߰� TODO

        // �������� ������
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

            if (name == "Player" && FullData.playersData.Count <= 0)        // �ѹ��� ȣ��ǵ��� ���� (Player ������ �ε�)
            {
                for (int i = 0; i < arrPlayerData.Length; ++i)
                {
                    FullData.playersData.Add(arrPlayerData[i]);
                }
            }
            else if (name == "GrowthStat" && FullData.growthsData.Count <= 0)       // �ѹ��� ȣ��ǵ��� ���� (GrowthStat ������ �ε�)
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
