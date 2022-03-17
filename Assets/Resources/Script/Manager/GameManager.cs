using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
    public static FullDataCollection FullData { get => Instance.fullData;}
    private FullDataCollection fullData = new FullDataCollection();

    #region ������ ���� �� ��ȯ��
    /// <summary>
    /// FullDataCollection �� ������ ����
    /// </summary>
    private void InsertDataSetting()
    {
        Dictionary<int, UsePlayerData> usePlayerDic;
        UsePlayerData[] arrPlayerData;

        // �÷��̾� ������
        ParsingJsonData("Player");
        Debug.Log(usePlayerDic.Count);      // �� ���� ������??? ���� ���⸸ �ϸ� �ɵ�  TODO
        

        // jsonUtility�� �迭�� �� ��������,
        // Litjson�� ���� ���ε��� ã�� ���̱⶧���� ���� ���� ���� ������, �Ѳ����� �ֱⰡ ���ŷο�,



        // ���� ������



        // ���� ������

        void ParsingJsonData(string name)
        {
            string path = Path.Combine(Application.dataPath , $"Resources/Document/Json");

            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", path, name), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);

            arrPlayerData = JsonConvert.DeserializeObject<UsePlayerData[]>(jsonData);

            usePlayerDic = new Dictionary<int, UsePlayerData>();

            int i = 0;
            foreach(UsePlayerData pData in arrPlayerData)
            {
                usePlayerDic.Add(i, pData);
                ++i;
            }

            // �÷��̾��Ͻ� �ش� ��ųʸ� ���� Ǯ������ ����Ʈ ���� �־��ֱ� -> ���彺�ݵ� �Ȱ��� TODO




            //return JsonConvert.DeserializeObject<T>(jsonData);      // ���� json ���� �迭�̶� ������ȭ �Ҽ� ���ٰ� ����, �迭 �ƴϸ� �ߵ�
        }
    }

    
    

    /*/// <summary>
    /// Json ������ ��ȯ
    /// </summary>
    private T JsonConvertData<T>(string path)
    {
        Dictionary<int, Dictionary<string, string>> dataDic = new Dictionary<int, Dictionary<string, string>>();    // ������ ���
        Dictionary<string, string> varNameDic = new Dictionary<string, string>(); // ����(int,float ��..), ��ȯ�� ���� ��

        string textAssetData = Resources.Load<TextAsset>($"Document/Json/{path}").ToString();
        string[] lines = textAssetData.Split('[', ']', '{', '}'); // ��� �ڸ���

        int startVarNameIndex = 0;
        int lastVarNameIndex = 0;
        string varName = "";

        int startValueIndex = 0;
        int lastValueIndex = 0;
        string valueName = "";

        for (int i = 0; i < lines.Length; ++i)
        {
            lines[i] = lines[i].Replace(" ", "");  // ��� ���� ����
            lines[i] = lines[i].Replace("\"", "");  // ��� �ֵ���ǥ ����

            if (lines[i].Contains(":"))
            {
                startVarNameIndex = lines[i].IndexOf('(');
                lastVarNameIndex = lines[i].IndexOf(')');
                varName = lines[i].Substring(startVarNameIndex, lastVarNameIndex);  // ���� ã��

                startValueIndex = lines[i].IndexOf(':');
                lastValueIndex = lines[i].IndexOf(',');
                valueName = lines[i].Substring(startValueIndex, lastValueIndex);    // ���� �� ã��

                varNameDic.Add(varName, valueName);

                dataDic.Add(i, varNameDic);
                Debug.Log(dataDic[i]);
                Debug.Log(varNameDic[varName]);
            }
        }
        // ���� JsonConvertData ���� dataDic ��ųʸ����� -> �迭�ε���(int), ����(string), ������(string) ���� �������
        // ���ϰ��� �ƹ��ų� �־����
        return JsonUtility.FromJson<T>(textAssetData);
    }

    *//*// ���� �̻���� TODO (�����ϸ� �����)
    private void ParseVariableName(string variableName, string value)
    {
        if (variableName == "int")
        {
            int.Parse(value);
        }
        else if (variableName == "float")
        {
            float.Parse(value);
        }
        else if (variableName == "string")
        {
            value = value as string;
        }

    }*/


    #endregion
}
