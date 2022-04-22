using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾� ����� ���Ǵ� Ŭ����
/// </summary>
public class DyingWindowUI : MonoBehaviour
{
    /// <summary>
    /// "You Died" �޽����� ������� TMPro
    /// </summary>
    TextMeshProUGUI youDiedText;

    /// <summary>
    /// "You Died" �ؽ�Ʈ�� BackImg
    /// </summary>
    Image youDiedBackImg;

    /// <summary>
    /// Diying Window �� ĵ���� �׷�
    /// </summary>
    public static CanvasGroup dyingCanvas;

    /// <summary>
    /// Diying Window�� ����
    /// </summary>
    public static Define.UIDraw DyingUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// Diying Window�� ������ ����
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
    /// Dying Message�� ��½�Ű�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DyingMessage()      // ���� �ƹ����� �ȳ־��� TODO /////////////////////////////////////////////
    {
        float duraction = 2f;
        float time = 0;

        while(true)
        {

            yield return null;
        }
    }

    /// <summary>
    /// ��� UI�� ������ ��� UI�� �������� Dying �޼��� ��� �޼ҵ�
    /// </summary>
    public static void AllUIQuit()
    {
        StatEquipWindowUI.Num2CanvasState = Define.UIDraw.SlowlyInactive;

        
    }
}
