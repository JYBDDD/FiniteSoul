using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StartScene �� �Ѱ��ϴ� Ŭ����
/// </summary>
public class StartSceneAdjust : MonoBehaviour
{
    [SerializeField]
    Image worldImg;          // ���

    [SerializeField]
    Text mainTitle;          // ���� Ÿ��Ʋ

    [SerializeField]
    Button newButton;        // �����ϱ� ��ư

    [SerializeField]
    Button continueButton;   // �̾��ϱ� ��ư

    [SerializeField]
    Button exitButton;       // �����ϱ� ��ư


    [SerializeField]
    Image descriptionWindow;    // ���� ������
    [SerializeField]
    TextMeshProUGUI dWText;     // ���� ������ �ؽ�Ʈ

    private void Start()
    {
        // ���� Ÿ��Ʋ ����
        StartCoroutine(MainTitleSetting(mainTitle));

        // ��� ���� ����
        StartCoroutine(ChangeBackGroundColor());

        // ��ư ����
        StartCoroutine(ButtonSetting(newButton,0));
        StartCoroutine(ButtonSetting(continueButton,1));
        StartCoroutine(ButtonSetting(exitButton,2));

        // �̺�Ʈ ���ε�
        newButton.onClick.AddListener(NewButtonSet);
        continueButton.onClick.AddListener(ContinueButtonSet);
        exitButton.onClick.AddListener(ExitButtonSet);


    }

    /// <summary>
    /// ���� �����ϱ� �޼���
    /// </summary>
    public void NewButtonSet()
    {
        // ĳ���� ����â ����� TODO
        LoadingSceneAdjust.LoadScene("1001");
    }

    /// <summary>
    /// �̾��ϱ� �޼���
    /// </summary>
    public void ContinueButtonSet()
    {
        // �̾��ϱ� �Ҽ� ���� �����̶�� ����
        if(!ResourceUtil.LoadConfirmFile())
        {
            // �̾��ϱ⸦ �Ҽ� ���� �����Դϴ� ���� TODO

            // ���� ������ ǥ��
            dWText.text = "�̾��ϱ� �Ҽ����� \n �����Դϴ�..";
            StartCoroutine(AppearWindow());

            return;
        }

        // �̾��ϱ� ����� ������ �ҷ�����
        var saveData = ResourceUtil.LoadSaveFile();

        // �����ð��� (����� ��������)���������� ��ȯ
        StartCoroutine(ContinueGame());


        IEnumerator ContinueGame()
        {
            // 3�ʵ� ����
            yield return new WaitForSeconds(3f);

            // ���� ������ �ֱ� TODO @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


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

            // ���İ� ������
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
        // TODO
        // "������ �����Ͻðڽ��ϱ�?" ��� �ؽ�Ʈ�� ���� ������(�ڽ����� "Yes" ��ư , "NO" ��ư)�� ���
        // ���� �ش� ������� ExitButton �� �ڽ����� SetActive ���·� ��� ������, ExitButton Ŭ���� ������ SetActive(true) �� ����
        // "Yes" ��ư Ŭ���� ���� ����, "No" ��ư Ŭ���� �ش� ������ SetActive(false)
    }


    /// <summary>
    /// ����ȭ�� ������ ��������� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeBackGroundColor()
    {
        float duration = 2f;    // ���� ����
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

}
