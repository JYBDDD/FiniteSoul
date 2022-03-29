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
    ScriptableObject MonsterSpawnsDoc;

    /// <summary>
    /// ���� ����
    /// </summary>
    public void MonsterSpawn()
    {
        // ���� ������ġ�� ScriptableObject �� ����� �־��ֵ��� ���� (¥�� ������ �� �ȹٲܰŴϱ�)  TODO
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void PlayerSpawn()       
    {
        // ���۾����� ĳ���͸� �����Ͽ��ٸ� ������ ĳ���� �ε����� SaveData�� �����Ѵ��� �ҷ����°��� ������ TODO
        // SaveData�� index (ĳ���� �ε���) �� ���� ĳ���� ���� TODO  (�ϴ��� �ȶ������ ������)

        GameObject player = ResoureUtil.InsertPrefabs("Player/Paladin");
        var volData = ResoureUtil.LoadSaveFile().playerVolatility;

        // �ش� �÷��̾ 0,0,0 ��ġ���� ����
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        // ���� TODO ������ Archer �ϰ�쵵 ���� ���� TODO
        Paladin paladin = player.GetComponent<Paladin>();
        paladin.playerData = ResoureUtil.LoadSaveFile();
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
