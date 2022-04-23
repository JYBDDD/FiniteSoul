using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ȣ�ۿ��� �Ű�ü�� ���Ǵ� Ŭ���� (Main : OrderInteraction  (�÷��̾ ������ ����))
/// </summary>
public class Interaction : MonoBehaviour
{
    [SerializeField, Tooltip("��ȣ�ۿ��� ����")]
    public Define.InteractionTarget interactionTarget;

    [SerializeField,Tooltip("��ȣ�ۿ� �ؽ�Ʈ")]
    public string interactionText;

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ���������� ���Դٸ� ����
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionUI.interactionText = interactionText;
            InteractionUI.InteractionUIState = Define.UIDraw.Activation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �÷��̾ ���������� ����ٸ� ����
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionUI.InteractionUIState = Define.UIDraw.Inactive;
        }
    }


}
