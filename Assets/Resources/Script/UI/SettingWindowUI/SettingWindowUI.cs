using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���� ������ Ŭ����
/// </summary>
public class SettingWindowUI : MonoBehaviour
{
    [SerializeField, Tooltip("���۾����� ��ȯ�ϴ� ���� ��ư")]
    Button returnSceneButton;

    [SerializeField, Tooltip("BGM ���� ���� ��ũ�ѹ�")]
    Scrollbar bgmScrollbar;
    [SerializeField, Tooltip("Effect ���� ���� ��ũ�ѹ�")]
    Scrollbar effectScrollbar;

    private void Start()
    {
        // �ʱ� ������ ����
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
    /// ���� ��ũ�ѹٵ��� �ش� Volume �� ����
    /// </summary>
    private void SoundControll()
    {
        SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().volume = bgmScrollbar.value;
        SoundManager.Instance.transform.GetChild(1).GetComponent<AudioSource>().volume = effectScrollbar.value;
    }

    /// <summary>
    /// ���۾����� �����ϴ� ��ư
    /// </summary>
    private void StartSceneReturn()
    {
        // �ڽ��� ���� ���� ��ġ������ ���� + �׿� ��� �����ʹ� ���� �����Ͱ����� ����
        var playVolData = ResourceUtil.LoadSaveFile().playerVolatility;
        Vector3 pos = new Vector3(playVolData.posX, playVolData.posY, playVolData.posZ);
        ResourceUtil.SaveData(new PlayerVolatilityData(InGameManager.Instance.Player.playerData, pos, StageManager.stageData));

        LoadingSceneAdjust.LoadStartScene();
    }

    /// <summary>
    /// �ش� ��ư�� ����� ������ Ȯ��
    /// </summary>
    private void ButtonActive()
    {
        // ��ư ���
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
