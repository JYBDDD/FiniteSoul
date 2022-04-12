using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� ������ UI(������)�� ��½�ų Ŭ���� 
/// </summary>
public class TargetMonsterUI : MonoBehaviour
{
    /// <summary>
    /// Ÿ�� UI ����
    /// </summary>
    Define.UIDraw TargetUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// TargetUIState �� ������ ����
    /// </summary>
    Define.UIDraw TargetOriginUIState = Define.UIDraw.SlowlyInactive;

    /// <summary>
    /// Ÿ�� ���� (AttackController ���� �÷��̾�� �������� ���� ����� ������ ����)
    /// </summary>
    public static MonsterController targetMonsterC;

    private float monsterOriginHp;

    [SerializeField]
    TextMeshProUGUI monsterName;    // Ÿ�� ���� �̸�

    [SerializeField]
    Image monsterHp;                // Ÿ�� ���� ü��

    CanvasGroup targetCanvasGroup;


    private void Update()
    {
        if(targetCanvasGroup == null)
        {
            targetCanvasGroup = GetComponent<CanvasGroup>();
        }

        UIManager.Instance.SwitchWindowOption(TargetUIState,TargetOriginUIState,targetCanvasGroup);

        // TargetMonsterC �� currentHp�� ���� ������ ���� ��� ����
        if(targetMonsterC != null && targetMonsterC.monsterData.currentHp != monsterOriginHp)
        {
            TargetDataSetting();
            monsterOriginHp = targetMonsterC.monsterData.currentHp;
        }
    }


    // targetMonsterC �� Ÿ���� ������ ���, TargetMonsterUI.alpha �� ��� 1�� ����  TODO
    // targetMonsterC �� Ÿ���� ���� ü���� 0�� ���, TargetMonsterUI.alpha ���� ������ 0�� �ǵ��� ���� (TargetUIState = Define.UIDraw.SlowlyInactive ���·� �����ϸ� ��) TODO


    private void TargetDataSetting()
    {
        // ���� ���� ü�� ����
        monsterHp.fillAmount = targetMonsterC.monsterData.currentHp / targetMonsterC.monsterData.maxHp;

        // ���� �̸� ����
        monsterName.text = targetMonsterC.monsterData.name;
    }

}
