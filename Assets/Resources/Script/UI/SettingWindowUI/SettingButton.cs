using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ������ ��/������ �����ϴ� ��ư Ŭ����
/// </summary>
public class SettingButton : MonoBehaviour
{
    [SerializeField, Tooltip("���� ������")]
    SettingWindowUI settingWindow;

    Button myButton;


    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SettingWindowSet);
    }

    private void SettingWindowSet()
    {
        // ���� ������ ���
        if (!settingWindow.gameObject.activeSelf)
        {
            settingWindow.gameObject.SetActive(true);
            Time.timeScale = 0;
            InGameManager.Instance.Player.NotToMove = false;
        }
        // ���� ������ ����
        else
        {
            settingWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
            InGameManager.Instance.Player.NotToMove = true;
        }
    }
}
