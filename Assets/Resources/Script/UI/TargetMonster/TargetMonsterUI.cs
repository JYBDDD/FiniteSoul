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
    /// <summary>
    /// Ÿ�� ���� (AttackController ���� �÷��̾�� �������� ���� ����� ������ ����)
    /// </summary>
    public static MonsterController targetMonsterC;

    [SerializeField]
    TextMeshProUGUI monsterName;    // Ÿ�� ���� �̸�

    [SerializeField]
    Image monsterHp;                // Ÿ�� ���� ü��


    private void Update()
    {
        // TargetMonsterC �� currentHp�� ���� ������ ���� ��� ����  TODO
        TargetDataSetting();
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
