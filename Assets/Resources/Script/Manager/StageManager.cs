using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // 해당 스테이지 데이터
    public static StageData stageData;



    /// <summary>
    /// 플레이어 생성
    /// </summary>
    public void PlayerSpawn()       
    {
        // 시작씬에서 캐릭터를 선택하였다면 선택한 캐릭터 인덱스를 SaveData로 저장한다음 불러오는것이 좋을듯 TODO
        // SaveData의 index (캐릭터 인덱스) 에 따라 캐릭터 생성 TODO  (일단은 팔라딘으로 설정함)

        GameObject player = ResoureUtil.InsertPrefabs("Player/Paladin");
        var volData = ResoureUtil.LoadSaveFile().playerVolatility;

        // 해당 플레이어를 0,0,0 위치에서 생성
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        // 위에 TODO 수정후 Archer 일경우도 수정 ㄱㄱ TODO
        Paladin paladin = player.GetComponent<Paladin>();
        paladin.playerData = ResoureUtil.LoadSaveFile();
        paladin.SetStat();

        // 만약 워프를 탔다면 이동후 저장 TODO
    }

    /// <summary>
    /// 스테이지 데이터를 넣어주는 메서드 (LoadingSceneAdjust에서 호출)
    /// </summary>
    public static void StageDataInsert(string sceneName)
    {
        int sceneIndex = int.Parse(sceneName);
        stageData = GameManager.Instance.FullData.stagesData.Where(_ => _.index == sceneIndex).SingleOrDefault();
    }
}
