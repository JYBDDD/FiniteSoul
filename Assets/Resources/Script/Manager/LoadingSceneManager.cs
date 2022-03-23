using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : Singleton<LoadingSceneManager>
{
    // 시작 씬 만든 후에 설정, 수정하면 됨 TODO

    public static string nextSceneName;

    // 이미지 -> fillAmount 용

    // 텍스트 ->  퍼센트용

    private void Start()
    {
        StartCoroutine(LoadScene());        // 해당 부분은 밑의 LoadScene 호출시 실행되도록 수정할 것 TODO
    }

    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while(!op.isDone)
        {
            timer += Time.deltaTime;
            if(op.progress < 0.9f)
            {
                // 이미지 fillAmount 타이머에 걸쳐 Lerp로 상승

                // fillAmount 가 op.progress 보다 크다면 
                // 타이머 0
            }
            else
            {
                // 이미지 fillAmount 최대로 상승

                // 만약 이미지 fillAmount가 1과 같다면
                // op.allowSceneActivation = true;
                yield break;
            }
        }


        yield return null;
    }


}
