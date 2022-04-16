using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 상호작용 UI를 출력시키는 클래스
/// </summary>
public class InteractionUI : MonoBehaviour
{
    /// <summary>
    /// 타겟 UI 상태
    /// </summary>
    public static Define.UIDraw InteractionUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// InteractionUIState 의 변경전 상태
    /// </summary>
    Define.UIDraw interactionOriginUIState = Define.UIDraw.SlowlyInactive;

    /// <summary>
    /// 상호작용하는 UI 출력을 On/Off할 캔버스 그룹
    /// </summary>
    [SerializeField]
    private CanvasGroup interactionCanvasGroup;

    /// <summary>
    /// 텍스트를 출력시킬 TexthMeshProGUI
    /// </summary>
    [SerializeField]
    public TextMeshProUGUI interactionTMPG;

    /// <summary>
    /// 텍스트값을 전달받아올 string 변수
    /// </summary>
    public static string interactionText;

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref InteractionUIState, ref interactionOriginUIState, interactionCanvasGroup,TextChange);
    }

    /// <summary>
    /// 출력할 TextMeshPro 값 변경
    /// </summary>
    private void TextChange()
    {
        interactionTMPG.text = interactionText;
    }
}
