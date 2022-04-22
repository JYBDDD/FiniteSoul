using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 사망시 사용되는 클래스
/// </summary>
public class DyingWindowUI : MonoBehaviour
{
    /// <summary>
    /// "You Died" 메시지를 들고있을 TMPro
    /// </summary>
    TextMeshProUGUI youDiedText;

    /// <summary>
    /// "You Died" 텍스트의 BackImg
    /// </summary>
    Image youDiedBackImg;

    /// <summary>
    /// Diying Window 의 캔버스 그룹
    /// </summary>
    public static CanvasGroup dyingCanvas;

    /// <summary>
    /// Diying Window의 상태
    /// </summary>
    public static Define.UIDraw DyingUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// Diying Window의 변경전 상태
    /// </summary>
    public static Define.UIDraw dyingUIOriginState = Define.UIDraw.Activation;

    private void Awake()
    {
        dyingCanvas = GetComponent<CanvasGroup>();
        youDiedText = GetComponentInChildren<TextMeshProUGUI>();
        youDiedBackImg = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        UIManager.Instance.SwitchWindowOption(ref DyingUIState, ref dyingUIOriginState, dyingCanvas);
    }

    /// <summary>
    /// Dying Message를 출력시키는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DyingMessage()      // 아직 아무데도 안넣었땅 TODO /////////////////////////////////////////////
    {
        float duraction = 2f;
        float time = 0;

        while(true)
        {

            yield return null;
        }
    }

    /// <summary>
    /// 사망 UI를 제외한 모든 UI를 종료이후 Dying 메세지 출력 메소드
    /// </summary>
    public static void AllUIQuit()
    {
        StatEquipWindowUI.Num2CanvasState = Define.UIDraw.SlowlyInactive;

        
    }
}
