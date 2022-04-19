using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

/// <summary>
/// StartScene 을 총괄하는 클래스
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    #region 기본 캔버스 변수 (Button, Image, Text)
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

    [SerializeField]
    Image descriptionWindow;    // 설명 윈도우
    [SerializeField]
    TextMeshProUGUI dWText;     // 설명 윈도우 텍스트
    #endregion

    #region 캐릭터창 변수 (Button, Image)
    /// <summary>
    /// 캐릭터 선택버튼
    /// </summary>
    [SerializeField]
    Button ChoiseButton;

    /// <summary>
    /// 캐릭터 변경버튼
    /// </summary>
    [SerializeField]
    Button ChangeCharacter;

    #endregion

    #region 시네머신 관련 변수
    /// <summary>
    /// 게임오브젝트와 실행할 타임라인이 연결되어있는 변수
    /// </summary>
    [SerializeField]
    PlayableDirector playableDirectors;

    /// <summary>
    /// 실행시킬 타임라인
    /// </summary>
    [SerializeField]
    TimelineAsset timelineAsset;

    /// <summary>
    /// 사용되는 메인 VirtualCam
    /// </summary>
    [SerializeField]
    CinemachineVirtualCamera useVirtualCam;
    #endregion

    #region 캔버스 그룹 변수
    /// <summary>
    /// 시작대기씬의 캔버스 그룹
    /// </summary>
    [SerializeField]
    CanvasGroup canvasGroup;

    /// <summary>
    /// 시작대기씬 캔버스 상태
    /// </summary>
    public static Define.UIDraw StartCanvasState = Define.UIDraw.Activation;

    /// <summary>
    /// 시짝씬 캔버스의 바뀌기전 상태
    /// </summary>
    private Define.UIDraw StartCanvasOriginState = Define.UIDraw.Inactive;

    /// <summary>
    /// 캐릭터 선택창의 캔버스 그룹
    /// </summary>
    [SerializeField]
    CanvasGroup choiseCanvasGroup;

    /// <summary>
    /// 캐릭터 선택창의 캔버스 상태
    /// </summary>
    public static Define.UIDraw ChoiseCanvasState = Define.UIDraw.Inactive;

    /// <summary>
    /// 캐릭터 선택창의 변경전 캔버스 상태
    /// </summary>
    private Define.UIDraw ChoiseOriginState = Define.UIDraw.Activation;

    #endregion


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

        // 캔버스 그룹 설정
        canvasGroup = GetComponent<CanvasGroup>();

        UIManager.Instance.SwitchWindowOption(ref ChoiseCanvasState, ref ChoiseOriginState, choiseCanvasGroup);
    }

    /// <summary>
    /// 새로 시작하기 메서드
    /// </summary>
    public void NewButtonSet()
    {
        // 캐릭터 선택창으로 이동
        CharacterChoiseMoving();
        
        // 데이터 초기화
        //ResourceUtil.NewDataReturn(1001);   // ->  캐릭터 선택후 해당 캐릭터 인덱스 삽입하기 TODO
        //LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// 이어하기 메서드
    /// </summary>
    public void ContinueButtonSet()
    {
        // 이어하기 할수 없는 파일이라면 실행
        if(!ResourceUtil.LoadConfirmFile())
        {
            // 설명 윈도우 표시
            dWText.text = "이어하기 할수없는 \n 파일입니다..";
            StartCoroutine(AppearWindow());

            return;
        }

        // 이어하기 저장된 데이터의 스테이지 데이터값을 찾음
        var saveData = ResourceUtil.LoadSaveFile();

        // 일정시간후 (저장된 데이터의)다음씬으로 전환
        dWText.text = "이어하기 데이터를 \n 불러옵니다..";
        StartCoroutine(ContinueGame());


        IEnumerator ContinueGame()
        {
            // 알파값 1;
            ReturnAlpha(255f / 255f);

            // 3초뒤 실행
            yield return new WaitForSeconds(2f);


            // 씬을 전환
            LoadingSceneAdjust.LoadScene($"{saveData.playerVolatility.stageIndex}");
            yield break;
        }

        
        IEnumerator AppearWindow()
        {
            // 알파값 재조정
            ReturnAlpha(255f / 255f);

            // 2초 대기
            yield return new WaitForSeconds(2f);

            // 알파값 0;
            ReturnAlpha(0f / 0f);
            yield break;

        }

        void ReturnAlpha(float alpha)
        {
            descriptionWindow.color = new Color(descriptionWindow.color.r, descriptionWindow.color.g, descriptionWindow.color.b, alpha);
            dWText.color = new Color(dWText.color.r, dWText.color.g, dWText.color.b, alpha);
        }

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

    #region 시작시 화면 셋팅 메서드
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
    #endregion

    #region 캐릭터 선택에 사용되는 메서드 (+시네머신)
    /// <summary>
    /// 캐릭터 선택창으로 이동한다
    /// </summary>
    private void CharacterChoiseMoving()
    {
        // 기본 캔버스창 즉시 비활성화
        StartCanvasState = Define.UIDraw.Inactive;
        UIManager.Instance.SwitchWindowOption(ref StartCanvasState, ref StartCanvasOriginState, canvasGroup);

        // 캐릭터 선택창으로 이동하는 타임라인 실행
        ChoiseStart();

        StartCoroutine(WaitingTime());

        IEnumerator WaitingTime()
        {
            yield return new WaitForSeconds(2f);

            // 캐릭터 선택창 UI 서서히 활성화
            ChoiseCanvasState = Define.UIDraw.SlowlyActivation;
            UIManager.Instance.SwitchWindowOption(ref ChoiseCanvasState, ref ChoiseOriginState, choiseCanvasGroup);

            yield break;
        }

        void ChoiseStart()
        {
            playableDirectors.Play(timelineAsset);
        }
    }

    ///////////////////////////////////////////////////// 밑에 있는 메소드 들은 버튼클릭, 기본캔버스로 돌아갈때 사용할 메서드임 TODO

    /// <summary>
    /// 본래 캔버스가 있던 위치로 리턴
    /// </summary>
    private void ReturnCanvasMoving()
    {
        UIManager.Instance.SwitchWindowOption(ref StartCanvasState,ref StartCanvasOriginState,canvasGroup);


        // 본래 있던 카메라 위치로 즉시 이동 TODO
    }
    /// <summary>
    /// 바라볼 대상을 변경하는 메서드
    /// </summary>
    /// <param name="timelineAsset">실행시킬 타임라인</param>
    /// <param name="choiseCharacter">지정할 캐릭터</param>
    private void ChoiseCineMachine(Transform choiseCharacter)
    {
        useVirtualCam.Follow = choiseCharacter;
        useVirtualCam.LookAt = choiseCharacter;
    }
    #endregion
}
