using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// ��ȣ�ۿ� UI�� ��½�Ű�� Ŭ����
/// </summary>
public class InteractionUI : MonoBehaviour
{
    /// <summary>
    /// Ÿ�� UI ����
    /// </summary>
    public static Define.UIDraw InteractionUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// InteractionUIState �� ������ ����
    /// </summary>
    Define.UIDraw interactionOriginUIState = Define.UIDraw.SlowlyInactive;

    /// <summary>
    /// ��ȣ�ۿ��ϴ� UI ����� On/Off�� ĵ���� �׷�
    /// </summary>
    [SerializeField]
    private CanvasGroup interactionCanvasGroup;

    /// <summary>
    /// �ؽ�Ʈ�� ��½�ų TexthMeshProGUI
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI interactionTMPG;

    /// <summary>
    /// �ؽ�Ʈ���� ���޹޾ƿ� string ����
    /// </summary>
    public static string interactionText;

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref InteractionUIState, ref interactionOriginUIState, interactionCanvasGroup,TextChange);
    }

    /// <summary>
    /// ����� TextMeshPro �� ����
    /// </summary>
    private void TextChange()
    {
        interactionTMPG.text = interactionText;
    }
}
