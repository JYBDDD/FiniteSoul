using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���� �� ������ ó���� ����� Ŭ���� / �ش� ��ũ��Ʈ�� ���� Controller�� InsertComponent���� AddComponent�� �����ְ� ����
/// </summary>
public class AttackController : MonoBehaviour
{
    /// <summary>
    /// �ӽ÷� ���� ������� ������ (MoveableObject �ʱ�ȭ�� ���� �Ѱ��ش�)
    /// </summary>
    public StaticData staticData;
    public Define.AtkType atkType;

    /// <summary>
    /// �⺻�� True    -> �߻�ü�� ��� ���� �¾����� ���� üũ���� �ʴ� �뵵
    /// </summary>
    public bool checkBool = true;

    /// <summary>
    /// ���� ��Ʈ�ѷ� �����Ͱ� �ʱ�ȭ
    /// </summary>
    /// <param name="staticData"></param>
    /// <param name="atkType"></param>
    public void AttackControllerInit(StaticData staticData,Define.AtkType atkType)
    {
        this.staticData = staticData;
        this.atkType = atkType;
        checkBool = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� ����ִ� �����Ͱ� �÷��̾��� ���� 
        if (staticData.characterType == Define.CharacterType.Player)
        {
            // ���Ϳ� ��Ҵٸ�
            if(other.gameObject.CompareTag("Monster") && checkBool == true)
            {
                

                var monsterC = other.gameObject.GetComponent<MonsterController>();
                var playerAtk = InGameManager.Instance.Player.playerData.atk;
                var damage = playerAtk - monsterC.monsterData.def;

                // �ٰŸ���� ����
                if (atkType == Define.AtkType.Normal && monsterC.monsterData.currentHp > 0)
                {
                    // �浹 ��ġ��
                    var closetPos = other.bounds.ClosestPoint(transform.position);

                    // �÷��̾� SwordAttackEffect ���
                    ResourceUtil.ParticleInit(Define.ParticleEffectPath.PlayerParticle.swordAttack, Define.CharacterType.Particle, this,
                        closetPos, gameObject.transform.parent.rotation);

                }

                // �������� 0���� �۰ų� ���ٸ� �������� 1�� ����
                if (damage <= 0)
                {
                    damage = 1;
                }
                // ���� ���� ü�� - (�÷��̾� ���ݷ� - ���� ����)
                monsterC.monsterData.currentHp -= damage;

                // ���� ���� Hurt�� ����
                monsterC.FSM.ChangeState(Define.State.Hurt, monsterC.HurtState);

                // TargetMonsterUI �� Ÿ�� ��Ʈ�ѷ��� Ÿ�ٰ� ����
                TargetMonsterUI.targetMonsterC = monsterC;

                // Ÿ���� �� ���� ü��(0���� Ŭ��),�̸� UI ��� Ȱ��ȭ
                if (monsterC.monsterData.currentHp > 0)
                {
                    TargetMonsterUI.TargetUIState = Define.UIDraw.Activation;
                }
            }

            // ���Ÿ���� ���� / ���Ϳ� ���� �ʾҴ��� ������ó�� �Ұ��ϰ� ����
            if (atkType == Define.AtkType.Projectile)
            {
                checkBool = false;
            }
        }

        // ���� ����ִ� �����Ͱ� ���Ͷ�� ����
        if (staticData.characterType == Define.CharacterType.Monster)
        {
            if(other.gameObject.CompareTag("Player") && InGameManager.Instance.Player.playerData.currentHp > 0)
            {
                // �浹 ��ġ��
                var closetPos = other.bounds.ClosestPoint(transform.position);

                // ���� AttackEffect ���
                ResourceUtil.ParticleInit(Define.ParticleEffectPath.MonsterParticle.monsterAttack, Define.CharacterType.Particle, this,
                    closetPos, Quaternion.identity);

                // Where �� SingleOrDefault���� ���� �����ϸ� �ΰ��̻��� ���� ã�� ������ �߻��Ҽ� �ֱ⿡ 
                // FirstOrDefault �� ù��° ���� ���������� ����
                var monsterC = InGameManager.Instance.Monsters.FirstOrDefault(_ => _.monsterData.index == staticData.index);
                var monsterAtk = monsterC.monsterData.atk;
                var playerC = InGameManager.Instance.Player;

                // �÷��̾ ȸ�� ���¶�� �������� ���� ����
                if (playerC.FSM.State == Define.State.Evasion)
                    return;

                // �÷��̾� ���� ü�� - (���� ���ݷ� - �÷��̾� ����)
                var damage = monsterAtk - playerC.playerData.def;
                playerC.playerData.currentHp -= damage;
                // �÷��̾� ���� Hurt�� ����
                playerC.FSM.ChangeState(Define.State.Hurt, playerC.HurtState);

                // �÷��̾��� ü���� 0���� ���ٸ� Diying UI Window�� ��½�Ų��
                if(playerC.playerData.currentHp <= 0)
                {
                    DyingWindowUI.DyingUIState = Define.UIDraw.SlowlyActivation;
                    UIManager.Instance.SwitchWindowOption(ref DyingWindowUI.DyingUIState, ref DyingWindowUI.dyingUIOriginState,
                        DyingWindowUI.dyingCanvas,DyingWindowUI.DyingWindowStart);
                    return;
                }
            }
        }
    }
}
