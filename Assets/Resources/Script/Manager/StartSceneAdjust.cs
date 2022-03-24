using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StartScene 을 총괄하는 클래스
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    [SerializeField]
    Image worldImg;          // 배경

    [SerializeField]
    Text mainTitle;          // 메인 타이틀

    [SerializeField]
    Button newButton;        // 새로하기 버튼

    [SerializeField]
    Button continueButton;   // 이어하기 버튼

    [SerializeField]
    Button exitButton;       // 종료하기 버튼

    private void Start()
    {
        // 메인 타이틀 셋팅
        StartCoroutine(MainTitleSetting(mainTitle));

        // 배경 색상 변경
        StartCoroutine(ChangeBackGroundColor());

        // 버튼 셋팅
        StartCoroutine(ButtonSetting(newButton,0));
        StartCoroutine(ButtonSetting(continueButton,1));
        StartCoroutine(ButtonSetting(exitButton,2));

        // 이벤트 바인딩
        newButton.onClick.AddListener(NewButtonSet);
        continueButton.onClick.AddListener(ContinueButtonSet);
        exitButton.onClick.AddListener(ExitButtonSet);


    }

    /// <summary>
    /// 새로 시작하기 메서드
    /// </summary>
    public void NewButtonSet()
    {
        // 첫시작은 무조건 1001 스테이지에서 생성  (현재는 SampleScene) TODO
        LoadingSceneAdjust.LoadScene("SampleScene");
    }

    /// <summary>
    /// 이어하기 메서드
    /// </summary>
    public void ContinueButtonSet()
    {
        // TODO
        // 이어서 할 경우 별도의 저장파일을 불러오는 위치를 지정해줄 것 (ResourcesUtil 에 만들면 될듯)

        // 이어서 할 파일이 없을 경우 인덱스를 없는 값인 (1000)으로 설정해줄 것
        // 만약 이어서 할 파일이 있다면, "게임을 이어서 진행합니다" 라는 텍스트를 갖는 윈도우 출력, 후 2초뒤 로딩씬으로 넘기기
        // 아니라면, "로드할 파일이 없습니다. 게임을 새로 진행합니다" 라는 텍스트를 갖는 윈도우 출력, 후 2초뒤 로딩씬으로 넘기기
    }

    /// <summary>
    /// 종료하기 메서드
    /// </summary>
    public void ExitButtonSet()
    {
        // TODO
        // "게임을 종료하시겠습니까?" 라는 텍스트를 갖는 윈도우(자식으로 "Yes" 버튼 , "NO" 버튼)를 출력
        // 위의 해당 윈도우는 ExitButton 이 자식으로 SetActive 상태로 들고 있으며, ExitButton 클릭시 윈도우 SetActive(true) 로 변경
        // "Yes" 버튼 클릭시 게임 종료, "No" 버튼 클릭시 해당 윈도우 SetActive(false)
    }


    /// <summary>
    /// 바탕화면 색상을 변경시켜줄 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeBackGroundColor()
    {
        float duration = 2f;    // 변경 간격
        float time = 0;

        float R = Random.Range(150, 255);
        float G = Random.Range(150, 255);
        float B = Random.Range(150, 255);

        while (time < duration)
        {
            time += Time.deltaTime;

            worldImg.color = Color.Lerp(worldImg.color, new Color(R / 255f, G / 255f, B / 255f), Time.deltaTime / 2f);

            yield return null;
        }

        StartCoroutine(ChangeBackGroundColor());

        yield return null;
    }

    /// <summary>
    /// 시작시 버튼 셋팅 코루틴
    /// </summary>
    /// <param name="button"></param>
    private IEnumerator ButtonSetting(Button button, int idleCount)
    {
        RectTransform buttonTrans = button.GetComponent<RectTransform>();

        StartCoroutine(FlyButton());

        // 화면 밖에서 날아오도록 하는 코루틴
        IEnumerator FlyButton()
        {
            buttonTrans.anchoredPosition = new Vector2(1000, buttonTrans.anchoredPosition.y);       // 출발 위치값 세팅

            yield return new WaitForSeconds(0.5f * idleCount);

            float duration = 10f;    // 진행 시간
            float time = 0;

            while(time < duration && buttonTrans.anchoredPosition.x > -100)
            {
                time += Time.deltaTime;
                buttonTrans.anchoredPosition = Vector2.Lerp(buttonTrans.anchoredPosition, new Vector2(-200, buttonTrans.anchoredPosition.y), Time.deltaTime * 2f);

                yield return null;
            }

            StartCoroutine(MoveInPeaceButton());

            yield return null;
        }

        // FlyButton 이후 위치를 잡아줄 코루틴
        IEnumerator MoveInPeaceButton()
        {
            float duration = 1f;
            float time = 0;

            while(time < duration && buttonTrans.anchoredPosition.x < 0)
            {
                time += Time.deltaTime;
                buttonTrans.anchoredPosition = Vector2.Lerp(buttonTrans.anchoredPosition, new Vector2(100, buttonTrans.anchoredPosition.y), Time.deltaTime * 2f);

                yield return null;
            }


            buttonTrans.anchoredPosition = new Vector2(0, buttonTrans.anchoredPosition.y);
            yield return null;
        }

        yield return null;
    }

    /// <summary>
    /// 시작시 타이틀 텍스트 셋팅 코루틴
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator MainTitleSetting(Text text)
    {
        text.fontSize = 300;   // 초기 폰트 크기

        while(text.fontSize > 200)
        {
            text.fontSize -= 3;
            yield return new WaitForEndOfFrame();
        }

        text.fontSize = 200;
        yield return null;
    }

}
