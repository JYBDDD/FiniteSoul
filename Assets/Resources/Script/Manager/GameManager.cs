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
        Dictionary<int, UsePlayerData> usePlayerDic;
        UsePlayerData[] arrPlayerData;

        // 플레이어 데이터
        ParsingJsonData("Player");
        Debug.Log(usePlayerDic.Count);      // 값 들어간것 같은데??? 이제 뺴기만 하면 될듯  TODO
        

        // jsonUtility는 배열일 시 못가져옴,
        // Litjson은 값을 따로따로 찾는 것이기때문에 값을 넣을 수는 있지만, 한꺼번에 넣기가 번거로움,



        // 성장 데이터



        // 몬스터 데이터

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

            // 플레이어일시 해당 딕셔너리 값을 풀데이터 리스트 값에 넣어주기 -> 성장스텟도 똑같이 TODO




            //return JsonConvert.DeserializeObject<T>(jsonData);      // 지금 json 값이 배열이라 역직렬화 할수 없다고 나옴, 배열 아니면 잘됨
        }
    }

    
    

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
