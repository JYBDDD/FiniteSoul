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

    [SerializeField, Tooltip("생성할 몬스터, 생성할 위치, 스테이지인덱스 값을 가지는 ScriptableObject ")]
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
        // 생성할 몬스터 인덱스 길이(총 생성할 몬스터 수) 만큼 실행
        for (int j = 0; j < MonsterSpawnsDoc.stageItem.monsterIndex.Length; ++j)
        {
            var monsterData = monsterDatas.FirstOrDefault(_ => _.index == MonsterSpawnsDoc.stageItem.monsterIndex[j]);

            // monsterData 값이 Null 이 아니라면 실행
            if (monsterData != null)
            {
                // 몬스터 생성
                GameObject monsterObj = ObjectPoolManager.Instance.GetPool<MoveableObject>(monsterData.resourcePath, 
                    MonsterSpawnsDoc.stageItem.locations[j], Define.CharacterType.Monster);
                MonsterController monsterC = monsterObj.GetComponent<MonsterController>();
                // InGameManager Monsters 리스트에 몬스터 등록
                InGameManager.Instance.MonsterRegist(monsterC);
                // 몬스터 데이터 삽입
                monsterC.monsterData = new UseMonsterData(monsterData);     // -> 데이터가 FullData에 있는 원본값을 출력하지 않도록 값을 재설정
                // 몬스터 초기화
                monsterC.Initialize(new UseMonsterData(monsterData));
                monsterC.monsterStartPos = MonsterSpawnsDoc.stageItem.locations[j];
                monsterC.AttackColliderSet();
                // 몬스터 스텟 설정
                monsterC.SetStat();
                // 몬스터 레이어,태그 설정
                monsterC.gameObject.layer = LayerMask.NameToLayer("Monster");
                monsterC.tag = "Monster";

            }
        }
    }

    /// <summary>
    /// 플레이어 생성
    /// </summary>
    public void PlayerSpawn()       
    {
        var loadFile = ResourceUtil.LoadSaveFile();
        var volData = loadFile.playerVolatility;

        // 해당 플레이어를 0,0,0 위치 or 저장된 위치에서 생성
        GameObject player = ObjectPoolManager.Instance.GetPool<PlayerController>(loadFile.resourcePath, 
            new Vector3(volData.posX, volData.posY, volData.posZ), Define.CharacterType.Player);

        PlayerController playerC = player.GetComponent<PlayerController>();
        // 플레이어 데이터 삽입
        playerC.playerData = loadFile;
        // 플레이어 초기화
        playerC.Initialize(playerC.playerData);
        playerC.AttackColliderSet();
        // 플레이어 스텟 설정
        playerC.SetStat(volData);
        // 플레이어 레이어,태그 설정
        playerC.gameObject.layer = LayerMask.NameToLayer("Player");
        playerC.tag = "Player";
        // 플레이어 상호작용기능 추가 (플레이어에게 상호작용 스크립트가 없는 경우만 적용)
        if(playerC.GetComponent<OrderInteraction>() == null)
        {
            playerC.gameObject.AddComponent<OrderInteraction>();
        }
        // InGameManager  Player 에 플레이어 등록
        InGameManager.Instance.PlayerRegist(playerC);
        // 스테이터스 설정
        StatusUI.playerData = InGameManager.Instance.Player.playerData;

        // UI Stat 설정
        StatUI.playerData = playerC.playerData;
        // UI Target 설정 초기화
        TargetMonsterUI.targetMonsterC = null;
        TargetMonsterUI.TargetUIState = Define.UIDraw.Inactive;

        // 메인카메라 생성
        ResourceUtil.InsertPrefabs(Define.CameraPath.mainCamPath);
        // 플레이어를 바라보는 VirtualCam 생성
        ResourceUtil.InsertPrefabs(Define.CameraPath.playerVirtualCamPath);

        // 룬 드랍
        Rune.RuneDrop();

        // 스테이지 UI 리셋후 실행
        DrawStageWindow.TempInstance.StageUIReset();
        DrawStageWindow.TempInstance.StageUIPrint();
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

