using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    // �ش� �������� ������
    public StageData stageData;




    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void PlayerSpawn()       // �ش� ������ ����κп��� ȣ�� TODO?
    {
        // ���۾����� ĳ���͸� �����Ͽ��ٸ� ������ ĳ���� �ε����� SaveData�� �����Ѵ��� �ҷ����°��� ������ TODO
        // SaveData�� index (ĳ���� �ε���) �� ���� ĳ���� ���� TODO  (�ϴ��� �ȶ������ ������)
        GameObject player = ResoureUtil.InsertPrefabs("Player/Paladin");
        var volData = ResoureUtil.LoadSaveFile().playerVolatility;

        // �ش� �÷��̾ 0,0,0 ��ġ���� ����
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        // ���� ������ ���ٸ� �̵��� ���� TODO
    }

    /// <summary>
    /// �������� �����͸� �־��ִ� �޼��� (LoadingSceneAdjust���� ȣ��)
    /// </summary>
    public void StageDataInsert(string sceneName)
    {
        int sceneIndex = int.Parse(sceneName);
        stageData = GameManager.Instance.FullData.stagesData.Where(_ => _.index == sceneIndex).SingleOrDefault();
    }
}
