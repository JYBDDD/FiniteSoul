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
    public static Define.UIDraw TargetUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// TargetUIState �� ������ ����
    /// </summary>
    Define.UIDraw TargetOriginUIState = Define.UIDraw.SlowlyInactive;

    /// <summary>
    /// Ÿ�� ���� (AttackController ���� �÷��̾�� �������� ���� ����� ������ ����)
    /// </summary>
    public static MonsterController targetMonsterC;

    private float monsterOriginHp;

    [SerializeField,Tooltip("Ÿ�� ���� �̸�")]
    TextMeshProUGUI monsterName;

    [SerializeField, Tooltip("Ÿ�� ���� ü��")]
    Image monsterHp;  

    CanvasGroup targetCanvasGroup;


    private void Update()
    {
        if(targetCanvasGroup == null)
        {
            targetCanvasGroup = GetComponent<CanvasGroup>();
        }

        UIManager.Instance.SwitchWindowOption(ref TargetUIState,ref TargetOriginUIState,targetCanvasGroup);

        // Ÿ�� ���Ͱ� Null�� �ƴϰų� / TargetMonsterC �� currentHp�� ���� ������ ���� ��� ����
        if (targetMonsterC != null && targetMonsterC.monsterData.currentHp != monsterOriginHp)
        {
            TargetDataSetting();
            monsterOriginHp = targetMonsterC.monsterData.currentHp;
        }

        // ���Ͱ� ������� ��� ����
        if (targetMonsterC?.monsterData.currentHp <= 0)
        {
            TargetUIState = Define.UIDraw.SlowlyInactive;
            targetMonsterC = null;
        }
    }


    private void TargetDataSetting()
    {
        // ������ ü���� 0���� ������� ����
        if(targetMonsterC.monsterData.currentHp <= 0)
        {
            monsterHp.fillAmount = 0;
            return;
        }

        // ���� ���� ü�� ����
        monsterHp.fillAmount = targetMonsterC.monsterData.currentHp / targetMonsterC.monsterData.maxHp;

        // ���� �̸� ����
        monsterName.text = targetMonsterC.monsterData.name;
    }

}
