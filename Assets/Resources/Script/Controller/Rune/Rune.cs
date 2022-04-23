using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 캐릭터 사망시 들고있을 룬 클래스
/// </summary>
public class Rune : MonoBehaviour,RecyclePooling
{
    /// <summary>
    /// 드랍 룬을 활성화할지 결정하는 클래스    -> DropRuneDataSetting 호출시 활성화됨 (플레이어 사망시)
    /// </summary>
    public static bool DropRuneTrue = false;

    /// <summary>
    /// 들고 있을 룬의 양
    /// </summary>
    public static float DropRuneAmount = 0;

    /// <summary>
    /// 드랍 룬의 위치값
    /// </summary>
    public static Vector3 DropRunePos = Vector3.zero;

    /// <summary>
    /// 룬을 필드위에 드랍
    /// </summary>
    public static void RuneDrop()
    {
        // 가지고 있는 룬이 0보다 많았었고,
        // 룬 드랍을 결정할 Bool타입 변수가 활성화 되었다면 실행 -> DropRuneDataSetting 호출시 활성화됨(플레이어 사망시)
        if(DropRuneTrue == true && DropRuneAmount > 0)
        {
            var runeObj = ObjectPoolManager.Instance.GetPool<Rune>(Define.DropRunePath.dropRunePath, Define.CharacterType.Rune);

            // 드랍 룬 위치값 지정
            runeObj.transform.position = DropRunePos;
        }

    }

    /// <summary>
    /// 현재 룬을 플레이어에게 넘기는 메서드
    /// </summary>
    public static void DropRuneHandOver()
    {
        InGameManager.Instance.Player.playerData.currentRune += DropRuneAmount;
    }

    /// <summary>
    /// 드랍할 룬의 데이터 셋팅   (플레이어 사망시 호출 , 데이터저장도 같이 병행)
    /// </summary>
    /// <param name="dropRune">드랍할 룬의 양</param>
    /// <param name="dropPos">드랍될 위치</param>
    public static void DropRuneDataSetting(float dropRune,Vector3 dropPos)
    {
        // 드랍룬을 활성화하도록 설정
        DropRuneTrue = true;

        // 룬과 드랍할 위치값 조정
        DropRuneAmount = dropRune;
        DropRunePos = dropPos;


        // 전에 저장되었던 휘발성 데이터     ->  (위치, 플레이어 인덱스 값만 사용)
        var loadFile = ResourceUtil.LoadSaveFile().playerVolatility;

        // 저장되는 플레이어위치는 이전 모닥불위치로 지정, 아니라면 Vector3.Zero 
        var playerVolData = new PlayerVolatilityData(InGameManager.Instance.Player.playerData, new Vector3(loadFile.posX, loadFile.posY, loadFile.posZ), StageManager.stageData);

        UsePlayerData playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == loadFile.index).SingleOrDefault();
        GrowthStatData growthStatData = GameManager.Instance.FullData.growthsData.Where(_ => _.index == playerData.growthRef).SingleOrDefault();

        // 값을 넣어줄 변경된 데이터
        UsePlayerData changeData = new UsePlayerData(growthStatData, playerData, playerVolData);

        // 플레이어의 CurrentRune = 0; 으로 설정
        changeData.currentRune = 0;

        // 현재 체력 설정부
        // 올린 데이터가 존재하지 않는다면
        if(playerVolData.raiseHp == 0)
        {
            // 기본값으로 설정
            changeData.playerVolatility.currentHp = playerData.maxHp;
        }
        else
        {
            // 올린 스텟값으로 설정
            changeData.playerVolatility.currentHp = playerVolData.raiseHp;
        }


        // 데이터 저장
        ResourceUtil.SaveData(new PlayerVolatilityData(changeData, new Vector3(loadFile.posX, loadFile.posY, loadFile.posZ), StageManager.stageData));
    }
}
