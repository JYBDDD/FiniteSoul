using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneAdjust : MonoBehaviour
{
    public static string nextSceneName;

    [SerializeField,Tooltip("�ε���")]
    Image loadingProgress;  

    [SerializeField, Tooltip("�ε� ��� �̹���")]
    Image loadingBackProgress;

    [SerializeField, Tooltip("��� �̹���")]
    Image worldImg; 

    [SerializeField, Tooltip("�ε� ���� �ؽ�Ʈ")]
    Text loadingText; 

    private void Start()
    {
        // UIManager ����� ��Ȱ��ȭ
        UIManager.UIDrawState = Define.UIDraw.Inactive;
        // �ΰ��� ����Ʈ Ŭ����
        InGameManager.Instance.ClearInGame();

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        // �� ��ȯ (�ε�)
        SceneManager.LoadScene("LoadingScene");     
        nextSceneName = sceneName;
        StageManager.StageDataInsert(nextSceneName);
    }

    public static void LoadStartScene()
    {
        SceneManager.LoadScene("LoadingScene");
        nextSceneName = "StartScene";

        // BGM ��ȯ
        SoundManager.Instance.SceneConversionBGM();

        // UIManager ĵ���� ����
        UIManager.UIDrawState = Define.UIDraw.Inactive;

        // ��Ÿ�� �缳��
        Time.timeScale = 1;
    }

  
    /// <summary>
    /// ���� ������ ��ȯ�����ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        // BGM ��ȯ
        SoundManager.Instance.SceneConversionBGM();

        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                loadingText.text = "Data Load.." + Mathf.RoundToInt(loadingProgress.fillAmount * 100f).ToString() + "%";
                loadingProgress.fillAmount = Mathf.Lerp(loadingProgress.fillAmount, op.progress, timer);

                if (loadingProgress.fillAmount > op.progress)    // �ε����� �̹����� �񵿱��۾� �����Ȳ���� ũ�ٸ�
                {
                    timer = 0;
                }
            }
            else
            {
                loadingText.text = "Data Load.." + Mathf.RoundToInt(loadingProgress.fillAmount * 100f).ToString() + "%";
                loadingProgress.fillAmount = Mathf.Lerp(loadingProgress.fillAmount, 1, Time.deltaTime);

                if (loadingProgress.fillAmount > 0.99f)     // �ε��� �Ϸ�Ǿ��ٸ�, �� ��ȯ
                {
                    StartCoroutine(FadeIn());

                    yield break;
                }
            }
            yield return null;
        }
        yield return null;


        IEnumerator FadeIn()
        {
            float duration = 1.5f;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;

                worldImg.color = Color.Lerp(worldImg.color, new Color(0 / 255f, 0 / 255f, 0 / 255f), Time.deltaTime * 3f);
                loadingBackProgress.color = Color.Lerp(loadingBackProgress.color, new Color(0 / 255f, 0 / 255f, 0 / 255f), Time.deltaTime * 3f);
                loadingProgress.color = Color.Lerp(loadingProgress.color, new Color(0 / 255f, 0 / 255f, 0 / 255f), Time.deltaTime * 3f);
                loadingText.color = Color.Lerp(loadingText.color, new Color(0 / 255f, 0 / 255f, 0 / 255f), Time.deltaTime * 3f);
                yield return null;
            }

            op.allowSceneActivation = true;

            // �̵����� ���۾��ϰ�� ����
            if (nextSceneName == "StartScene")
                yield break;

            // �񵿱� �۾��� �Ϸ�� �� ����
            op.completed += (AsyncOperation p) => 
            {
                // UIManager ����� ������ Ȱ��ȭ
                UIManager.UIDrawState = Define.UIDraw.SlowlyActivation;
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyActivation;
                // ��ȣ�ۿ� UI ��Ȱ��ȭ
                InteractionUI.InteractionUIState = Define.UIDraw.Inactive;
                // StartScene ĳ���� ���� ĵ���� ��Ȱ��ȭ / StartSceneCanvas Ȱ��ȭ
                StartSceneAdjust.ChoiseCanvasState = Define.UIDraw.Inactive;
                StartSceneAdjust.StartCanvasState = Define.UIDraw.Activation;
                // �÷��̾� ����
                StageManager.Instance.PlayerSpawn();
                // ���� ����
                StageManager.Instance.MonsterSpawn();
                // BGM ��ȯ
                SoundManager.Instance.SceneConversionBGM();
            };

            yield break;
        }
    }

    
}
