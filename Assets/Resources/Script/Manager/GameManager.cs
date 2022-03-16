using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static FullDataCollection FullData { get => Instance.fullData; set => value = Instance.fullData; }
    private FullDataCollection fullData = new FullDataCollection();


    #region ������ ���� �� ��ȯ��
    /// <summary>
    /// FullDataCollection �� ������ ����
    /// </summary>
    private void InsertDataSetting()
    {
        // �÷��̾� ������
        var PlayerData = JsonConvertData<UsePlayerData>("Player");
        // TODO �����Ͱ� �迭�� �ƴѰͰ�����..? Ȯ���غ��� �ϳ��� ���� �����ϱ�

        // ���� ������



        // ���� ������
    }

    /// <summary>
    /// Json ������ ��ȯ
    /// </summary>
    private T JsonConvertData<T>(string path)
    {
        var textAssetData = Resources.Load<TextAsset>($"Document/Json/{path}");

        return JsonUtility.FromJson<T>(textAssetData.ToString());
    }
    #endregion
}
