using System;
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
    /// 생성할 몬스터, 생성할 위치, 스테이지인덱스 값을 가지는 ScriptableObject 
    /// </summary>
    [SerializeField]
    public ScriptablePickUp MonsterSpawnsDoc;

    /// <summary>
    /// 몬스터 생성
    /// </summary>
    public void MonsterSpawn()
    {
        // 만약 현재 BuildScene 과 stageIndex값이 다르다면 리턴
        if (MonsterSpawnsDoc.stageItem.stageIndex != stageData.index)
        {
            return;
        }

        var monsterDatas = GameManager.Instance.FullData.monstersData;
        for(int i = 0; i < monsterDatas.Count; ++i)
        {
            // monsterDatas 의 몬스터 인덱스와 스크립터블 오브젝트의 몬스터 인덱스 값이 동일하다면 생성
            for(int j = 0; j < MonsterSpawnsDoc.stageItem.monsterIndex.Length; ++j )
            {
                if (monsterDatas[i].index == MonsterSpawnsDoc.stageItem.monsterIndex[j])
                {
                    // 몬스터 생성
                    GameObject monsterObj = ObjectPoolManager.Instance.GetPool<MoveableObject>(monsterDatas[i].ResourcePath,monsterDatas[i].name,Define.CharacterType.Monster);
                    // 몬스터 위치값 지정
                    monsterObj.transform.position = MonsterSpawnsDoc.stageItem.locations[j];
                    MonsterController monsterC = monsterObj.GetComponent<MonsterController>();
                    // 몬스터 데이터 삽입
                    monsterC.monsterData = monsterDatas[i];
                    // 몬스터 초기화
                    monsterC.Initialize(monsterDatas[i]);
                    monsterC.monsterStartPos = MonsterSpawnsDoc.stageItem.locations[j];
                    monsterC.AttackColliderSet();
                    // 몬스터 스텟 설정
                    monsterC.SetStat();
                    // 몬스터 레이어,태그 설정
                    monsterC.gameObject.layer = LayerMask.NameToLayer("Monster");
                    monsterC.tag = "Monster";
                    // InGameManager Monsters 리스트에 몬스터 등록
                    InGameManager.Instance.MonsterRegist(monsterC);
                }
            }

        }
    }

    /// <summary>
    /// 플레이어 생성
    /// </summary>
    public void PlayerSpawn()       
    {
        // SaveData의 index (캐릭터 인덱스) 에 따라 캐릭터 생성 (일단은 팔라딘으로 설정함 -> SaveData 인덱스가 팔라딘값으로 되어있음)
        // 시작씬에서 캐릭터를 선택하였다면 선택한 캐릭터 인덱스를 SaveData로 저장한다음 불러오는것이 좋을듯 TODO
        var loadFile = ResoureUtil.LoadSaveFile();
        GameObject player = ResoureUtil.InsertPrefabs(loadFile.resourcePath);
        var volData = loadFile.playerVolatility;

        // 해당 플레이어를 0,0,0 위치 or 저장된 위치에서 생성
        player.transform.position = new Vector3(volData.posX, volData.posY, volData.posZ);

        PlayerController playerC = player.GetComponent<PlayerController>();
        // 플레이어 데이터 삽입
        playerC.playerData = loadFile;
        // 플레이어 초기화
        playerC.Initialize(playerC.playerData);
        playerC.AttackColliderSet();
        // 플레이어 스텟 설정
        playerC.SetStat();
        // 플레이어 레이어,태그 설정
        playerC.gameObject.layer = LayerMask.NameToLayer("Player");
        playerC.tag = "Player";
        // InGameManager  Player 에 플레이어 등록
        InGameManager.Instance.PlayerRegist(playerC);

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

/// <summary>
/// ScriptableObject를 임시로 들고있을 클래스
/// </summary>
[Serializable]
public class ScriptablePickUp
{
    public NewScriptableObject stageItem;
}

