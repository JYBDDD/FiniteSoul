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

    [SerializeField,Tooltip("You Died 텍스트의 BackImg")]
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

    /// <summary>
    /// UI 실행을 결정할 Bool타입 변수
    /// </summary>
    public static bool startWindow = false;

    private void Awake()
    {
        dyingCanvas = GetComponent<CanvasGroup>();
        youDiedText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 설정 복구
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
    /// Dying Message를 출력시키는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DyingMessage()
    {

        float imgHeight = 300f;     // 상승시킬 이미지 높이
        float time = 0;
        Color colorText = youDiedText.color;


        while(youDiedBackImg.rectTransform.rect.height < imgHeight)
        {
            // backImg 높이 상승
            youDiedBackImg.rectTransform.sizeDelta = Vector2.Lerp(youDiedBackImg.rectTransform.sizeDelta, new Vector2(2000, 350), Time.deltaTime * 2f);

            yield return null;
        }

        // 높이 갚이 설정값에 도달하였다면 실행
        if (youDiedBackImg.rectTransform.rect.height > imgHeight)
        {
            youDiedBackImg.rectTransform.sizeDelta = new Vector2(2000, 300);

            // youDiedText 텍스트 서서히 출력
            while(time < 2f)
            {
                time += Time.deltaTime;
                youDiedText.color = Color.Lerp(youDiedText.color, new Color(colorText.r,colorText.g,colorText.b,255 / 255), Time.deltaTime);

                yield return null;
            }
            if(time > 2f)
            {
                // DyingUIWindow 서서히 종료
                DyingWindowUI.DyingUIState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref DyingUIState, ref dyingUIOriginState, dyingCanvas);

                yield return new WaitForSeconds(1f);
                // 1초 이후 씬를 재로드 한다
                SceneReload();
            }
        }

        yield return null;
    }

    /// <summary>
    /// 사망 UI를 제외한 모든 UI를 종료이후 Dying 메세지 출력 메소드
    /// </summary>
    public static void DyingWindowStart()
    {
        startWindow = true;
        UIManager.Num2CanvasState = Define.UIDraw.SlowlyInactive;
    }

    /// <summary>
    /// 플레이어 사망후 씬을 재로드, 설정 복구하는 메서드
    /// </summary>
    private void SceneReload()
    {
        SettingRestore();

        // 씬 전환
        LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// 설정 복구 메소드 (초기화)
    /// </summary>
    private void SettingRestore()
    {
        // 설정 복구
        youDiedBackImg.rectTransform.sizeDelta = new Vector2(2000, 0);
        Color color = youDiedText.color;
        color = new Color(color.r, color.g, color.b, 0);
        youDiedText.color = color;

        startWindow = false;
    }
}
