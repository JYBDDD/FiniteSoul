using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneAdjust : MonoBehaviour
{
    public static string nextSceneName;

    [SerializeField]
    Image loadingProgress;      // �ε���

    [SerializeField]
    Text loadingText;           // �ε� ���� �ؽ�Ʈ

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScene");     // ���� �ε��� ��ȯ
        nextSceneName = sceneName;
    }

  
    /// <summary>
    /// ���� ������ ��ȯ�����ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
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
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
        yield return null;
    }
}
