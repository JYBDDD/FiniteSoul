using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���� �� ������ ó���� ����� Ŭ���� / �ش� ��ũ��Ʈ�� ���� Controller�� InsertComponent���� AddComponent�� �����ְ� ����
/// </summary>
public class AttackController : MonoBehaviour
{
    // �ӽ÷� ���� ������� ������ (MoveableObject �ʱ�ȭ�� ���� �Ѱ��ش�)
    public StaticData staticData;

    private void OnTriggerEnter(Collider other)
    {
        // ���� ����ִ� �����Ͱ� �÷��̾��� ���� 
        if (staticData.characterType == Define.CharacterType.Player)
        {
            // ���Ϳ� ��Ҵٸ�
            if(other.gameObject.CompareTag("Monster"))
            {
                var monsterC = other.gameObject.GetComponent<MonsterController>();
                var playerData = GameManager.Instance.FullData.playersData.Where(_ => _.index == staticData.index).SingleOrDefault();
                // ���� ���� ü�� - (�÷��̾� ���ݷ� - ���� ����)
                monsterC.monsterData.currentHp = monsterC.monsterData.currentHp - (playerData.atk - monsterC.monsterData.def);
                // ���� ���� Hurt�� ����
                monsterC.FSM.ChangeState(Define.State.Hurt, monsterC.HurtState);
            }
        }

        // ���� ����ִ� �����Ͱ� ���Ͷ�� ����
        if (staticData.characterType == Define.CharacterType.Monster)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                var monsterData = GameManager.Instance.FullData.monstersData.Where(_ => _.index == staticData.index).SingleOrDefault();
                var playerC = other.gameObject.GetComponent<PlayerController>();

                // �÷��̾ ȸ�� ���¶�� �������� ���� ����
                if (playerC.FSM.State == Define.State.Evasion)
                    return;

                // �÷��̾� ���� ü�� - (���� ���ݷ� - �÷��̾� ����)
                playerC.playerData.currentHp = playerC.playerData.currentHp - (monsterData.atk - playerC.playerData.def);
                // �÷��̾� ���� Hurt�� ����
                playerC.FSM.ChangeState(Define.State.Hurt, playerC.HurtState);
            }
        }
    }
}
