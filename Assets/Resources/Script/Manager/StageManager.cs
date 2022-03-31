using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // �ش� �������� ������
    public static StageData stageData;

    /// <summary>
    /// ������ ����, ������ ��ġ, ���������ε��� ���� ������ ScriptableObject 
    /// </summary>
    [SerializeField]
    ScriptablePickUp MonsterSpawnsDoc;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void MonsterSpawn()
    {
        // ���� ���� BuildScene �� stageIndex���� �ٸ��ٸ� ����
        if (MonsterSpawnsDoc.stageItem.stageIndex == int.Parse(SceneManager.GetActiveScene().ToString()))
        {
            return;
        }

        var monsterDatas = GameManager.Instance.FullData.monstersData;
        for(int i = 0; i < MonsterSpawnsDoc.stageItem.monsterIndex.Length; ++i)
        {
            // monsterDatas �� ���� �ε����� ��ũ���ͺ� ������Ʈ�� ���� �ε��� ���� �����ϴٸ� ����
            if(monsterDatas[i].index == MonsterSpawnsDoc.stageItem.monsterIndex[i])
            {
                // monsterDatas �� ���� ���� �ΰ��� ����ְ�,
                // ��ũ���ͺ� ������Ʈ ���� �ε������� ���� 6���� ��� ����,       (����ٶ�)  TODO
            }
        }
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void PlayerSpawn()       
    {
        // ���۾����� ĳ���͸� �����Ͽ��ٸ� ������ ĳ���� �ε����� SaveData�� �����Ѵ��� �ҷ����°��� ������ TODO
        // SaveData�� index (ĳ���� �ε���) �� ���� ĳ���� ���� TODO  (�ϴ��� �ȶ������ ������)
        var loadFile = ResoureUtil.LoadSaveFile();
        GameObject player = ResoureUtil.InsertPrefabs(loadFile.resourcePath);
        var volData = loadFile.playerVolatility;

        // �ش� �÷��̾ 0,0,0 ��ġ���� ����
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        // ���� TODO ������ Archer �ϰ�쵵 ���� ���� TODO
        Paladin paladin = player.GetComponent<Paladin>();
        paladin.playerData = loadFile;
        paladin.Initialize(paladin);
        paladin.SetStat();

        // InGameManager  Player �� �÷��̾� ���
        InGameManager.Instance.PlayerRegist(paladin);

        // ���� ������ ���ٸ� �̵��� ���� TODO
    }

    /// <summary>
    /// �������� �����͸� �־��ִ� �޼��� (LoadingSceneAdjust���� ȣ��)
    /// </summary>
    public static void StageDataInsert(string sceneName)
    {
        int sceneIndex = int.Parse(sceneName);
        stageData = GameManager.Instance.FullData.stagesData.Where(_ => _.index == sceneIndex).SingleOrDefault();
    }
}

/// <summary>
/// ScriptableObject�� �ӽ÷� ������� Ŭ����
/// </summary>
[Serializable]
public class ScriptablePickUp
{
    public NewScriptableObject stageItem;
}

