using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

/// <summary>
/// StartScene �� �Ѱ��ϴ� Ŭ����
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    #region �⺻ ĵ���� ���� (Button, Image, Text)
    [SerializeField,Header("�⺻ ĵ���� ����"),Tooltip("���")]
    Image worldImg;

    [SerializeField, Tooltip("���� Ÿ��Ʋ")]
    Text mainTitle;

    [SerializeField, Tooltip("�����ϱ� ��ư")]
    Button newButton;

    [SerializeField, Tooltip("�̾��ϱ� ��ư")]
    Button continueButton;

    [SerializeField, Tooltip("�����ϱ� ��ư")]
    Button exitButton;

    [SerializeField,Tooltip("���� ��ư")]
    Button soundButton;

    [SerializeField, Tooltip("���� ������")]
    Image descriptionWindow;
    [SerializeField, Tooltip("���� ������ �ؽ�Ʈ")]
    TextMeshProUGUI dWText;
    [SerializeField, Tooltip("���� ������")]
    SettingWindowUI settingWindow;
    #endregion

    #region �ó׸ӽ� ���� ����
    [SerializeField,Header("�ó׸ӽ� ���� ����"),Tooltip("���ӿ�����Ʈ�� ������ Ÿ�Ӷ����� ����Ǿ��ִ� ����")]
    PlayableDirector playableDirectors;

    [SerializeField,Tooltip("�����ų Ÿ�Ӷ���")]
    TimelineAsset timelineAsset;
    #endregion

    #region ĵ���� �׷� ����
    [SerializeField,Header("ĵ���� �׷� ����"),Tooltip("���۴����� ĵ���� �׷�")]
    CanvasGroup canvasGroup;
    [Tooltip("���� ������ ĵ���� ����")]
    public static Define.UIDraw StartCanvasState = Define.UIDraw.Activation;
    [Tooltip("���� ������ �⺻ ĵ���� ����")]
    private Define.UIDraw StartCanvasOriginState = Define.UIDraw.Inactive;

    [SerializeField,Tooltip("ĳ���� ����â�� ĵ���� �׷�")]
    CanvasGroup choiseCanvasGroup;
    [Tooltip("ĳ���� ����â�� ĵ���� ����")]
    public static Define.UIDraw ChoiseCanvasState = Define.UIDraw.Inactive;
    [Tooltip("ĳ���� ����â�� �⺻ ĵ���� ����")]
    private Define.UIDraw ChoiseOriginState = Define.UIDraw.Activation;

    /// <summary>
    /// ĳ���� ����â���� �Ѿ�� �������ͽ� �������� �������� �Ѱ��� Bool Ÿ��
    /// </summary>
    public static bool choiseBool = false;
    #endregion


    private void Start()
    {
        // BGM ����
        SoundManager.Instance.SceneConversionBGM();

        // ���� Ÿ��Ʋ ����
        StartCoroutine(MainTitleSetting(mainTitle));

        // ��� ���� ����
        StartCoroutine(ChangeBackGroundColor());

        // ��ư ����
        StartCoroutine(ButtonSetting(newButton,0));
        StartCoroutine(ButtonSetting(continueButton,1));
        StartCoroutine(ButtonSetting(exitButton,2));
        StartCoroutine(ButtonAlphaSet(soundButton));

        // �̺�Ʈ ���ε�
        newButton.onClick.AddListener(NewButtonSet);
        continueButton.onClick.AddListener(ContinueButtonSet);
        exitButton.onClick.AddListener(ExitButtonSet);
        soundButton.onClick.AddListener(SoundButtonSet);
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref StartCanvasState, ref StartCanvasOriginState, canvasGroup);
        UIManager.Instance.SwitchWindowOption(ref ChoiseCanvasState, ref ChoiseOriginState, choiseCanvasGroup);
    }

    /// <summary>
    /// ���� �����ϱ� �޼���
    /// </summary>
    public void NewButtonSet()
    {
        // ĳ���� ����â���� �̵�
        CharacterChoiseMoving();

        // UI ���� ���
        SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
    }

    /// <summary>
    /// �̾��ϱ� �޼���
    /// </summary>
    public void ContinueButtonSet()
    {
        // �̾��ϱ� �Ҽ� ���� �����̶�� ����
        if(!ResourceUtil.LoadConfirmFile())
        {
            // ���� ������ ǥ��
            dWText.text = "�̾��ϱ� �Ҽ����� \n �����Դϴ�..";
            StartCoroutine(AppearWindow());

            return;
        }

        // �̾��ϱ� ����� �������� �������� �����Ͱ��� ã��
        var saveData = ResourceUtil.LoadSaveFile();

        // �����ð��� (����� ��������)���������� ��ȯ
        dWText.text = "�̾��ϱ� �����͸� \n �ҷ��ɴϴ�..";
        StartCoroutine(ContinueGame());

        // UI ���� ���
        SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);

        IEnumerator ContinueGame()
        {
            // ���İ� 1;
            ReturnAlpha(255f / 255f);

            // 3�ʵ� ����
            yield return new WaitForSeconds(2f);


            // ���� ��ȯ
            LoadingSceneAdjust.LoadScene($"{saveData.playerVolatility.stageIndex}");
            yield break;
        }

        
        IEnumerator AppearWindow()
        {
            // ���İ� ������
            ReturnAlpha(255f / 255f);

            // 2�� ���
            yield return new WaitForSeconds(2f);

            // ���İ� 0;
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
    /// �����ϱ� �޼���
    /// </summary>
    public void ExitButtonSet()
    {
        // ���� ����
        Application.Quit();
    }

    /// <summary>
    /// ���� ���� �޼���
    /// </summary>
    public void SoundButtonSet()
    {
        // ���� ������ ���
        if(!settingWindow.gameObject.activeSelf)
        {
            settingWindow.gameObject.SetActive(true);
        }
        // ���� ������ ����
        else
        {
            settingWindow.gameObject.SetActive(false);
        }


        // UI ���� ���
        SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);

        
    }

    #region ���۽� ȭ�� ���� �޼���
    /// <summary>
    /// ����ȭ�� ������ ��������� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeBackGroundColor()
    {
        float duration = 2f;    // ���� ����
        float time = 0;

        float R = UnityEngine.Random.Range(150, 255);
        float G = UnityEngine.Random.Range(150, 255);
        float B = UnityEngine.Random.Range(150, 255);

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
    /// ���۽� ��ư ���� �ڷ�ƾ
    /// </summary>
    /// <param name="button"></param>
    private IEnumerator ButtonSetting(Button button, int idleCount)
    {
        RectTransform buttonTrans = button.GetComponent<RectTransform>();

        StartCoroutine(FlyButton());

        // ȭ�� �ۿ��� ���ƿ����� �ϴ� �ڷ�ƾ
        IEnumerator FlyButton()
        {
            buttonTrans.anchoredPosition = new Vector2(1000, buttonTrans.anchoredPosition.y);       // ��� ��ġ�� ����

            yield return new WaitForSeconds(0.5f * idleCount);

            float duration = 10f;    // ���� �ð�
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

        // FlyButton ���� ��ġ�� ����� �ڷ�ƾ
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
    /// ���۽� Ÿ��Ʋ �ؽ�Ʈ ���� �ڷ�ƾ
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private IEnumerator MainTitleSetting(Text text)
    {
        text.fontSize = 300;   // �ʱ� ��Ʈ ũ��

        while(text.fontSize > 200)
        {
            text.fontSize -= 3;
            yield return new WaitForEndOfFrame();
        }

        text.fontSize = 200;
        yield return null;
    }

    /// <summary>
    /// ���۽� ��ư ���İ��� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ButtonAlphaSet(Button button)
    {
        var btnText = button.transform.GetChild(0).GetComponent<Text>();
        btnText.color = new Color(btnText.color.r, btnText.color.g, btnText.color.b, 0);

        float duraction = 3f;
        float time = 0;

        while(true)
        {
            if(time > duraction)
            {
                btnText.color = new Color(btnText.color.r, btnText.color.g, btnText.color.b, 255f / 255f);
                yield break;
            }

            time += Time.deltaTime;
            btnText.color = Color.Lerp(btnText.color, new Color(btnText.color.r, btnText.color.g, btnText.color.b, 255f / 255f),time / duraction);

            yield return null;
        }
    }
    #endregion

    #region ĳ���� ���ÿ� ���Ǵ� �޼��� (+�ó׸ӽ�)
    /// <summary>
    /// ĳ���� ����â���� �̵��Ѵ�
    /// </summary>
    private void CharacterChoiseMoving()
    {
        // �⺻ ĵ����â ��� ��Ȱ��ȭ
        StartCanvasState = Define.UIDraw.Inactive;

        // ĳ���� ����â���� �̵��ϴ� Ÿ�Ӷ��� ����
        ChoiseStart();

        StartCoroutine(WaitingTime());

        IEnumerator WaitingTime()
        {
            yield return new WaitForSeconds(2f);

            // ĳ���� ����â UI ������ Ȱ��ȭ
            ChoiseCanvasState = Define.UIDraw.SlowlyActivation;

            // ĳ������ �̸����� �Ѱ���
            CharacterChoiseStatus.charcterStaticName = "Paladin";

            // �������ͽ� �ʱⰪ�� �����Ҽ� ����
            choiseBool = true;

            yield break;
        }

        void ChoiseStart()
        {
            playableDirectors.Play(timelineAsset);
        }
    }
    #endregion
}
