using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneAdjust : MonoBehaviour
{
    public static string nextSceneName;

    [SerializeField]
    Image loadingProgress;      // 로딩바

    [SerializeField]
    Text loadingText;           // 로딩 진행 텍스트

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScene");     // 먼저 로딩씬 전환
        nextSceneName = sceneName;
    }

  
    /// <summary>
    /// 다음 씬으로 전환시켜주는 코루틴
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

                if (loadingProgress.fillAmount > op.progress)    // 로딩진행 이미지가 비동기작업 진행상황보다 크다면
                {
                    timer = 0;
                }
            }
            else
            {
                loadingText.text = "Data Load.." + Mathf.RoundToInt(loadingProgress.fillAmount * 100f).ToString() + "%";
                loadingProgress.fillAmount = Mathf.Lerp(loadingProgress.fillAmount, 1, Time.deltaTime);

                if (loadingProgress.fillAmount > 0.99f)     // 로딩이 완료되었다면, 씬 전환
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
