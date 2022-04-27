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

    [SerializeField, Tooltip("상호작용하는 UI 출력을 On/Off할 캔버스 그룹")]
    private CanvasGroup interactionCanvasGroup;

    [SerializeField,Tooltip("텍스트를 출력시킬 TexthMeshProGUI")]
    public TextMeshProUGUI interactionTMPG;

    /// <summary>
    /// 텍스트값을 전달받아올 string 변수
    /// </summary>
    public static string interactionText;

    /// <summary>
    /// 상호작용 UI의 RectTransform
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
    /// 출력할 TextMeshPro 값 변경
    /// </summary>
    private void TextChange()
    {
        interactionTMPG.text = interactionText;

        // 글자가 8글자를 넘어갈 경우 한글자당 gameObject.Rectransform = left -10 , right - 20
        int textLength = -10;
        if(interactionTMPG.text?.Length > 8)
        {
            textLength *= interactionTMPG.text.Length - 7;

            interactionRect.offsetMin = new Vector2(-200 + textLength, 0);
            interactionRect.offsetMax = new Vector2(200 + (textLength * 2), 0);
            interactionRect.sizeDelta = new Vector2(400 + (-textLength * 2) + (-textLength), 90);       // 여기 부분 수정중.. TODO
        }
        else
        {
            interactionRect.offsetMin = new Vector2(-200, 0);
            interactionRect.offsetMax = new Vector2(200, 0);
            interactionRect.sizeDelta = new Vector2(400, 90);
        }

    }
}
