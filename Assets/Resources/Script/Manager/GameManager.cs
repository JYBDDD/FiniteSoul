using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

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
        // �÷��̾� ������
        TextAsset textAsset = Resources.Load<TextAsset>("Document/Json/Player");

        // jsonUtility�� �迭�� �� ��������,
        // Litjson�� ���� ���ε��� ã�� ���̱⶧���� ���� ���� ���� ������, �Ѳ����� �ֱⰡ ���ŷο�,



        // ���� ������



        // ���� ������
    }

    //  �׳� ���� �����͸� json���� �ٲٴºκк��� �ٽ� �ؾߵɵ� �ʹ�
    // ���� �����͸� json �迭�� �ٲܼ� �ִ°����� �ۼ��ؾ��ҵ� TODO

    

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
