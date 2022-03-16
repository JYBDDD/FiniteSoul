using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Text;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        InsertDataSetting();
    }

    /// <summary>
    /// 총괄 데이터
    /// </summary>
    public static FullDataCollection FullData { get => Instance.fullData;}
    private FullDataCollection fullData = new FullDataCollection();


    #region 데이터 삽입 및 변환부
    /// <summary>
    /// FullDataCollection 에 데이터 삽입
    /// </summary>
    private void InsertDataSetting()
    {
        // 플레이어 데이터
        TextAsset textAsset = Resources.Load<TextAsset>("Document/Json/Player");

        // TODO jsonUtility는 배열일시 못가져옴,
        // Litjson은 값을 따로따로 찾는 것이기때문에 값을 넣을 수는 있지만, 한꺼번에 넣기가 번거로움,



        // 성장 데이터



        // 몬스터 데이터
    }

    //  그냥 엑셀 데이터를 json으로 바꾸는부분부터 다시 해야될듯 싶다
    // 엑셀 데이터를 json 배열로 바꿀수 있는것으로 작성해야할듯 TODO

    /*private T LoadJsonFile<T>(string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.dataPath + "/Resources/Document/Json/", fileName),FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }*/


    

    /*/// <summary>
    /// Json 데이터 변환
    /// </summary>
    private T JsonConvertData<T>(string path)
    {
        Dictionary<int, Dictionary<string, string>> dataDic = new Dictionary<int, Dictionary<string, string>>();    // 데이터 목록
        Dictionary<string, string> varNameDic = new Dictionary<string, string>(); // 변수(int,float 등..), 변환된 변수 값

        string textAssetData = Resources.Load<TextAsset>($"Document/Json/{path}").ToString();
        string[] lines = textAssetData.Split('[', ']', '{', '}'); // 모두 자르기

        int startVarNameIndex = 0;
        int lastVarNameIndex = 0;
        string varName = "";

        int startValueIndex = 0;
        int lastValueIndex = 0;
        string valueName = "";

        for (int i = 0; i < lines.Length; ++i)
        {
            lines[i] = lines[i].Replace(" ", "");  // 모든 공백 제거
            lines[i] = lines[i].Replace("\"", "");  // 모든 쌍따음표 제거

            if (lines[i].Contains(":"))
            {
                startVarNameIndex = lines[i].IndexOf('(');
                lastVarNameIndex = lines[i].IndexOf(')');
                varName = lines[i].Substring(startVarNameIndex, lastVarNameIndex);  // 변수 찾기

                startValueIndex = lines[i].IndexOf(':');
                lastValueIndex = lines[i].IndexOf(',');
                valueName = lines[i].Substring(startValueIndex, lastValueIndex);    // 변수 값 찾기

                varNameDic.Add(varName, valueName);

                dataDic.Add(i, varNameDic);
                Debug.Log(dataDic[i]);
                Debug.Log(varNameDic[varName]);
            }
        }
        // 현재 JsonConvertData 안의 dataDic 딕셔너리에서 -> 배열인덱스(int), 변수(string), 변수값(string) 으로 들고있음
        // 리턴값은 아무거나 넣어놓음
        return JsonUtility.FromJson<T>(textAssetData);
    }

    *//*// 아직 미사용중 TODO (사용안하면 지우기)
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
