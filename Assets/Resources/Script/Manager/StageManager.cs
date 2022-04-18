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
    public ScriptablePickUp MonsterSpawnsDoc;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void MonsterSpawn()
    {
        // ���� ���� BuildScene �� stageIndex���� �ٸ��ٸ� ����
        if (MonsterSpawnsDoc.stageItem.stageIndex != stageData.index)
        {
            return;
        }

        var monsterDatas = GameManager.Instance.FullData.monstersData;
        // ������ ���� �ε��� ����(�� ������ ���� ��) ��ŭ ����
        for (int j = 0; j < MonsterSpawnsDoc.stageItem.monsterIndex.Length; ++j)
        {
            var monsterData = monsterDatas.FirstOrDefault(_ => _.index == MonsterSpawnsDoc.stageItem.monsterIndex[j]);

            // monsterData ���� Null �� �ƴ϶�� ����
            if (monsterData != null)
            {
                // ���� ����
                GameObject monsterObj = ObjectPoolManager.Instance.GetPool<MoveableObject>(monsterData.resourcePath, monsterData.name, Define.CharacterType.Monster);
                // ���� ��ġ�� ����
                monsterObj.transform.position = MonsterSpawnsDoc.stageItem.locations[j];
                MonsterController monsterC = monsterObj.GetComponent<MonsterController>();
                // InGameManager Monsters ����Ʈ�� ���� ���
                InGameManager.Instance.MonsterRegist(monsterC);
                // ���� ������ ����
                monsterC.monsterData = new UseMonsterData(monsterData);     // -> �����Ͱ� FullData�� �ִ� �������� ������� �ʵ��� ���� �缳��
                // ���� �ʱ�ȭ
                monsterC.Initialize(new UseMonsterData(monsterData));
                monsterC.monsterStartPos = MonsterSpawnsDoc.stageItem.locations[j];
                monsterC.AttackColliderSet();
                // ���� ���� ����
                monsterC.SetStat();
                // ���� ���̾�,�±� ����
                monsterC.gameObject.layer = LayerMask.NameToLayer("Monster");
                monsterC.tag = "Monster";

            }
        }
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void PlayerSpawn()       
    {
        // SaveData�� index (ĳ���� �ε���) �� ���� ĳ���� ���� (�ϴ��� �ȶ������ ������ -> SaveData �ε����� �ȶ������ �Ǿ�����)
        // ���۾����� ĳ���͸� �����Ͽ��ٸ� ������ ĳ���� �ε����� SaveData�� �����Ѵ��� �ҷ����°��� ������ TODO
        var loadFile = ResourceUtil.LoadSaveFile();
        GameObject player = ResourceUtil.InsertPrefabs(loadFile.resourcePath);
        var volData = loadFile.playerVolatility;

        // �ش� �÷��̾ 0,0,0 ��ġ or ����� ��ġ���� ����
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        PlayerController playerC = player.GetComponent<PlayerController>();
        // �÷��̾� ������ ����
        playerC.playerData = loadFile;
        // �÷��̾� �ʱ�ȭ
        playerC.Initialize(playerC.playerData);
        playerC.AttackColliderSet();
        // �÷��̾� ���� ����
        playerC.SetStat(volData);
        // �÷��̾� ���̾�,�±� ����
        playerC.gameObject.layer = LayerMask.NameToLayer("Player");
        playerC.tag = "Player";
        // �÷��̾� ��ȣ�ۿ��� �߰�
        playerC.gameObject.AddComponent<OrderInteraction>();
        // InGameManager  Player �� �÷��̾� ���
        InGameManager.Instance.PlayerRegist(playerC);
        // �������ͽ� ����
        StatusUI.playerData = InGameManager.Instance.Player.playerData;

        // ����ī�޶� ����
        ResourceUtil.InsertPrefabs(Define.CameraPath.mainCamPath);
        // �÷��̾ �ٶ󺸴� VirtualCam ����
        ResourceUtil.InsertPrefabs(Define.CameraPath.playerVirtualCamPath);

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

