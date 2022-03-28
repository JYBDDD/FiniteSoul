using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // �ش� �������� ������
    public static StageData stageData;


    // �ش� PlayerSpawn() �� LoadingSceneAdjust �� ���� �ڿ� ȣ���� ������ϴµ�,
    // op.allowSceneActivation = true; ������ �ϸ� �ε����� �÷��̾ ������ �ǹ���
    // �� ��ȯ�� �Ϸ�ǰ�, ���������� �ٲ������ PlayerSpawn()�� ȣ���������      TODO


    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void PlayerSpawn()       
    {
        // ���۾����� ĳ���͸� �����Ͽ��ٸ� ������ ĳ���� �ε����� SaveData�� �����Ѵ��� �ҷ����°��� ������ TODO
        // SaveData�� index (ĳ���� �ε���) �� ���� ĳ���� ���� TODO  (�ϴ��� �ȶ������ ������)
        Debug.Log(SceneManager.GetActiveScene().name);

        GameObject player = ResoureUtil.InsertPrefabs("Player/Paladin");
        var volData = ResoureUtil.LoadSaveFile().playerVolatility;

        // �ش� �÷��̾ 0,0,0 ��ġ���� ����
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

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
