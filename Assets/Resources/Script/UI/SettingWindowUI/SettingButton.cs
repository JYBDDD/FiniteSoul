using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 셋팅 윈도우 온/오프를 설정하는 버튼 클래스
/// </summary>
public class SettingButton : MonoBehaviour
{
    [SerializeField, Tooltip("셋팅 윈도우")]
    SettingWindowUI settingWindow;

    Button myButton;


    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SettingWindowSet);
    }

    private void SettingWindowSet()
    {
        // 셋팅 윈도우 출력
        if (!settingWindow.gameObject.activeSelf)
        {
            settingWindow.gameObject.SetActive(true);
            Time.timeScale = 0;
            InGameManager.Instance.Player.NotToMove = false;
        }
        // 셋팅 윈도우 삭제
        else
        {
            settingWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
            InGameManager.Instance.Player.NotToMove = true;
        }
    }
}
