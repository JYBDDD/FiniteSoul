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

    [SerializeField, Tooltip("��ȣ�ۿ��ϴ� UI ����� On/Off�� ĵ���� �׷�")]
    private CanvasGroup interactionCanvasGroup;

    [SerializeField,Tooltip("�ؽ�Ʈ�� ��½�ų TexthMeshProGUI")]
    public TextMeshProUGUI interactionTMPG;

    /// <summary>
    /// �ؽ�Ʈ���� ���޹޾ƿ� string ����
    /// </summary>
    public static string interactionText;

    /// <summary>
    /// ��ȣ�ۿ� UI�� RectTransform
    /// </summary>
    RectTransform interactionRect;

    private void Start()
    {
        interactionRect = gameObject.GetComponent<RectTransform>();
    }

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

        // ���ڰ� 8���ڸ� �Ѿ ��� �ѱ��ڴ� ũ�� ����
        int textLength = 15;
        if(interactionTMPG.text?.Length > 8)
        {
            textLength *= interactionTMPG.text.Length - 7;

            interactionRect.sizeDelta = new Vector2(400 + textLength, 90);
        }
        else
        {
            interactionRect.sizeDelta = new Vector2(400, 90);
        }

    }
}
