using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 공격을 당한 몬스터의 UI(데이터)를 출력시킬 클래스 
/// </summary>
public class TargetMonsterUI : UIManager
{
    /// <summary>
    /// 타겟 몬스터 (AttackController 에서 플레이어에게 데미지를 입은 대상이 있을시 삽입)
    /// </summary>
    public static MonsterController targetMonsterC;

    [SerializeField]
    TextMeshProUGUI monsterName;    // 타깃 몬스터 이름

    [SerializeField]
    Image monsterHp;                // 타깃 몬스터 체력


    private void Update()
    {
        // TargetMonsterC 의 currentHp의 값이 변동이 있을 경우 실행  TODO
        TargetDataSetting();
    }

    // targetMonsterC 의 타깃이 생겼을 경우, TargetMonsterUI.alpha 값 즉시 1로 증가
    // targetMonsterC 의 타깃의 현재 체력이 0일 경우, TargetMonsterUI.alpha 값이 서서히 0이 되도록 설정  TODO


    private void TargetDataSetting()
    {
        // 몬스터 현재 체력 설정
        monsterHp.fillAmount = targetMonsterC.monsterData.currentHp / targetMonsterC.monsterData.maxHp;

        // 몬스터 이름 설정
        monsterName.text = targetMonsterC.monsterData.name;
    }

}
