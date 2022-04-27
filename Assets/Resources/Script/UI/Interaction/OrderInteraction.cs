using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ��������� Interaction�� ����ִ� ������Ʈ�� ��ȣ�ۿ��� ������ �Ǵ� Ŭ����
/// </summary>
public class OrderInteraction : MonoBehaviour
{
    /// <summary>
    /// �÷��̾ ��ȣ�ۿ��� �õ��Ϸ��ϴ��� Ȯ���ϴ� Bool Ÿ�� ����
    /// </summary>
    public static bool playerInteractionTrue;

    private void Start()
    {
        InputManager.Instance.KeyAction += OnInteraction;
    }

    /// <summary>
    /// �÷��̾ ��ȣ�ۿ� On/Off�� �Ϸ��Ұ�� ����Ǵ� �޼���
    /// </summary>
    private void OnInteraction()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            playerInteractionTrue = true;
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            playerInteractionTrue = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Interaction")
        {
            // ��ںҰ� ��Ұ� ��ȣ�ۿ��Ϸ� �Ѵٸ�, ������ ���� �� �������ͽ�â Ȱ��ȭ
            if (other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Save && playerInteractionTrue)
            {
                // Esc Ű�� ������ �������ͽ�â Ż��
                if (Input.GetKey(KeyCode.Escape))
                {
                    // �̵� ���� ����
                    InGameManager.Instance.Player.NotToMove = true;

                    // �÷��̾� ��ȣ�ۿ� ����
                    playerInteractionTrue = false;

                    // �������ͽ�â õõ�� ��Ȱ��ȭ
                    StatEquipWindowUI.StatEquipState = Define.UIDraw.SlowlyInactive;

                    // ������ ����
                    ResourceUtil.SaveData(new PlayerVolatilityData(InGameManager.Instance.Player.playerData, transform.position, StageManager.stageData));

                    return;
                }

                // �̵� �Ұ� ����
                InGameManager.Instance.Player.NotToMove = false;

                // �������ͽ�â Ȱ��ȭ
                StatEquipWindowUI.StatEquipState = Define.UIDraw.SlowlyActivation;
            }

            // ��� ��ȣ�ۿ��Ϸ� �Ѵٸ� �ش� �� ȸ��
            if(other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Rune && playerInteractionTrue)
            {
                // �÷��̾�� ���� �ѱ�
                Rune.DropRuneHandOver();

                // ���� Ǯ���Ŵ����� �ѱ�
                ObjectPoolManager.Instance.GetPush(other.gameObject);

                // �� ȹ�� ����Ʈ Ȱ��ȭ
                var runeEffect = ObjectPoolManager.Instance.GetPool<ParticleChild>(Define.ParticleEffectPath.PlayerParticle.runeEffect, Define.CharacterType.Particle);
                runeEffect.transform.position = other.gameObject.transform.position + Vector3.up;
            }

            // ������ ��ȣ�ۿ��Ϸ� �Ѵٸ� ����
            if(other.GetComponent<Interaction>().interactionTarget == Define.InteractionTarget.Shop && playerInteractionTrue)
            {
                // Esc Ű�� ������ ����â Ż��
                if (Input.GetKey(KeyCode.Escape))
                {
                    // �̵� ���� ����
                    InGameManager.Instance.Player.NotToMove = true;

                    // �÷��̾� ��ȣ�ۿ� ����
                    playerInteractionTrue = false;

                    // ����â õõ�� ��Ȱ��ȭ
                    ShopInvenWindowUI.ShopInvenUIState = Define.UIDraw.SlowlyInactive;

                    return;
                }

                // �̵� �Ұ� ����
                InGameManager.Instance.Player.NotToMove = false;

                // ���� + �κ��丮 â Ȱ��ȭ
                ShopInvenWindowUI.ShopInvenUIState = Define.UIDraw.SlowlyActivation;
            }

        }

    }
}
