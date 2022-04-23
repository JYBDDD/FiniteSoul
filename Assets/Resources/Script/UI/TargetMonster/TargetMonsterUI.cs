using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 공격을 당한 몬스터의 UI(데이터)를 출력시킬 클래스 
/// </summary>
public class TargetMonsterUI : MonoBehaviour
{
    /// <summary>
    /// 타겟 UI 상태
    /// </summary>
    public static Define.UIDraw TargetUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// TargetUIState 의 변경전 상태
    /// </summary>
    Define.UIDraw TargetOriginUIState = Define.UIDraw.SlowlyInactive;

    /// <summary>
    /// 타겟 몬스터 (AttackController 에서 플레이어에게 데미지를 입은 대상이 있을시 삽입)
    /// </summary>
    public static MonsterController targetMonsterC;

    private float monsterOriginHp;

    [SerializeField,Tooltip("타깃 몬스터 이름")]
    TextMeshProUGUI monsterName;

    [SerializeField, Tooltip("타깃 몬스터 체력")]
    Image monsterHp;  

    CanvasGroup targetCanvasGroup;


    private void Update()
    {
        if(targetCanvasGroup == null)
        {
            targetCanvasGroup = GetComponent<CanvasGroup>();
        }

        UIManager.Instance.SwitchWindowOption(ref TargetUIState,ref TargetOriginUIState,targetCanvasGroup);

        // 타겟 몬스터가 Null이 아니거나 / TargetMonsterC 의 currentHp의 값이 변동이 있을 경우 실행
        if (targetMonsterC != null && targetMonsterC.monsterData.currentHp != monsterOriginHp)
        {
            TargetDataSetting();
            monsterOriginHp = targetMonsterC.monsterData.currentHp;
        }

        // 몬스터가 사망했을 경우 실행
        if (targetMonsterC?.monsterData.currentHp <= 0)
        {
            TargetUIState = Define.UIDraw.SlowlyInactive;
            targetMonsterC = null;
        }
    }


    private void TargetDataSetting()
    {
        // 몬스터의 체력이 0보다 작을경우 실행
        if(targetMonsterC.monsterData.currentHp <= 0)
        {
            monsterHp.fillAmount = 0;
            return;
        }

        // 몬스터 현재 체력 설정
        monsterHp.fillAmount = targetMonsterC.monsterData.currentHp / targetMonsterC.monsterData.maxHp;

        // 몬스터 이름 설정
        monsterName.text = targetMonsterC.monsterData.name;
    }

}
