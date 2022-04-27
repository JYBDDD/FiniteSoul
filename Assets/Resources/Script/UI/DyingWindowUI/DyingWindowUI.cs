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

    [SerializeField,Tooltip("You Died �ؽ�Ʈ�� BackImg")]
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

    /// <summary>
    /// UI ������ ������ BoolŸ�� ����
    /// </summary>
    public static bool startWindow = false;

    private void Awake()
    {
        dyingCanvas = GetComponent<CanvasGroup>();
        youDiedText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // ���� ����
        SettingRestore();

        UIManager.Instance.SwitchWindowOption(ref DyingUIState, ref dyingUIOriginState, dyingCanvas);
    }

    private void Update()
    {
        if(startWindow == true)
        {
            StartCoroutine(DyingMessage());
            startWindow = false;
        }
    }

    /// <summary>
    /// Dying Message�� ��½�Ű�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DyingMessage()
    {

        float imgHeight = 300f;     // ��½�ų �̹��� ����
        float time = 0;
        Color colorText = youDiedText.color;


        while(youDiedBackImg.rectTransform.rect.height < imgHeight)
        {
            // backImg ���� ���
            youDiedBackImg.rectTransform.sizeDelta = Vector2.Lerp(youDiedBackImg.rectTransform.sizeDelta, new Vector2(2000, 350), Time.deltaTime * 2f);

            yield return null;
        }

        // ���� ���� �������� �����Ͽ��ٸ� ����
        if (youDiedBackImg.rectTransform.rect.height > imgHeight)
        {
            youDiedBackImg.rectTransform.sizeDelta = new Vector2(2000, 300);

            // youDiedText �ؽ�Ʈ ������ ���
            while(time < 2f)
            {
                time += Time.deltaTime;
                youDiedText.color = Color.Lerp(youDiedText.color, new Color(colorText.r,colorText.g,colorText.b,255 / 255), Time.deltaTime);

                yield return null;
            }
            if(time > 2f)
            {
                // DyingUIWindow ������ ����
                DyingWindowUI.DyingUIState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref DyingUIState, ref dyingUIOriginState, dyingCanvas);

                yield return new WaitForSeconds(1f);
                // 1�� ���� ���� ��ε� �Ѵ�
                SceneReload();
            }
        }

        yield return null;
    }

    /// <summary>
    /// ��� UI�� ������ ��� UI�� �������� Dying �޼��� ��� �޼ҵ�
    /// </summary>
    public static void DyingWindowStart()
    {
        startWindow = true;
        UIManager.Num2CanvasState = Define.UIDraw.SlowlyInactive;
    }

    /// <summary>
    /// �÷��̾� ����� ���� ��ε�, ���� �����ϴ� �޼���
    /// </summary>
    private void SceneReload()
    {
        SettingRestore();

        // �� ��ȯ
        LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// ���� ���� �޼ҵ� (�ʱ�ȭ)
    /// </summary>
    private void SettingRestore()
    {
        // ���� ����
        youDiedBackImg.rectTransform.sizeDelta = new Vector2(2000, 0);
        Color color = youDiedText.color;
        color = new Color(color.r, color.g, color.b, 0);
        youDiedText.color = color;

        startWindow = false;
    }
}
