using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 셋팅 윈도우 클래스
/// </summary>
public class SettingWindowUI : MonoBehaviour
{
    [SerializeField, Tooltip("시작씬으로 전환하는 리턴 버튼")]
    Button returnSceneButton;

    [SerializeField, Tooltip("BGM 사운드 조절 스크롤바")]
    Scrollbar bgmScrollbar;
    [SerializeField, Tooltip("Effect 사운드 조절 스크롤바")]
    Scrollbar effectScrollbar;

    private void Start()
    {
        // 초기 설정값 지정
        bgmScrollbar.value = SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().volume;
        effectScrollbar.value = SoundManager.Instance.transform.GetChild(1).GetComponent<AudioSource>().volume;

        returnSceneButton.onClick.AddListener(StartSceneReturn);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ButtonActive();
    }

    private void Update()
    {
        SoundControll();
    }

    /// <summary>
    /// 각각 스크롤바들을 해당 Volume 과 연결
    /// </summary>
    private void SoundControll()
    {
        SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().volume = bgmScrollbar.value;
        SoundManager.Instance.transform.GetChild(1).GetComponent<AudioSource>().volume = effectScrollbar.value;
    }

    /// <summary>
    /// 시작씬으로 리턴하는 버튼
    /// </summary>
    private void StartSceneReturn()
    {
        // 자신의 이전 저장 위치값으로 저장 + 그외 모든 데이터는 현재 데이터값으로 저장
        var playVolData = ResourceUtil.LoadSaveFile().playerVolatility;
        Vector3 pos = new Vector3(playVolData.posX, playVolData.posY, playVolData.posZ);
        ResourceUtil.SaveData(new PlayerVolatilityData(InGameManager.Instance.Player.playerData, pos, StageManager.stageData));

        LoadingSceneAdjust.LoadStartScene();
    }

    /// <summary>
    /// 해당 버튼을 출력할 씬인지 확인
    /// </summary>
    private void ButtonActive()
    {
        // 버튼 출력
        if(SceneManager.GetActiveScene().name != "StartScene")
        {
            returnSceneButton.gameObject.SetActive(true);
        }
        else
        {
            returnSceneButton.gameObject.SetActive(false);
        }
    }
}
