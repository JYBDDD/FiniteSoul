using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� ������ UI(������)�� ��½�ų Ŭ���� 
/// </summary>
public class TargetMonsterUI : UIManager
{
    Define.UIDraw TargetUIState = Define.UIDraw.Inactive;

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

    private void Start()
    {
        targetCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        SwitchWindowOption(targetCanvasGroup);

        // TargetMonsterC �� currentHp�� ���� ������ ���� ��� ����
        if(targetMonsterC != null && targetMonsterC.monsterData.currentHp != monsterOriginHp)
        {
            TargetDataSetting();
            monsterOriginHp = targetMonsterC.monsterData.currentHp;
        }
    }

    protected override void SwitchWindowOption(CanvasGroup cGroup = null)
    {
        if (cGroup == null)
            return;

        if (TargetUIState != UIOriginState)
        {
            switch (TargetUIState)
            {
                case Define.UIDraw.Activation:
                    UIWindowActive(cGroup);
                    break;
                case Define.UIDraw.SlowlyActivation:
                    UIWindowSlowlyActive(cGroup);
                    break;
                case Define.UIDraw.Inactive:
                    UIWindowInActive(cGroup);
                    break;
                case Define.UIDraw.SlowlyInactive:
                    UIWindowSlowlyInActive(cGroup);
                    break;
            }

            UIOriginState = TargetUIState;
        }


    }

    // targetMonsterC �� Ÿ���� ������ ���, TargetMonsterUI.alpha �� ��� 1�� ����
    // targetMonsterC �� Ÿ���� ���� ü���� 0�� ���, TargetMonsterUI.alpha ���� ������ 0�� �ǵ��� ����  TODO


    private void TargetDataSetting()
    {
        // ���� ���� ü�� ����
        monsterHp.fillAmount = targetMonsterC.monsterData.currentHp / targetMonsterC.monsterData.maxHp;

        // ���� �̸� ����
        monsterName.text = targetMonsterC.monsterData.name;
    }

}
