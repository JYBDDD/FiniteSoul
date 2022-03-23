using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : Singleton<LoadingSceneManager>
{
    // ���� �� ���� �Ŀ� ����, �����ϸ� �� TODO

    public static string nextSceneName;

    // �̹��� -> fillAmount ��

    // �ؽ�Ʈ ->  �ۼ�Ʈ��

    private void Start()
    {
        StartCoroutine(LoadScene());        // �ش� �κ��� ���� LoadScene ȣ��� ����ǵ��� ������ �� TODO
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
                // �̹��� fillAmount Ÿ�̸ӿ� ���� Lerp�� ���

                // fillAmount �� op.progress ���� ũ�ٸ� 
                // Ÿ�̸� 0
            }
            else
            {
                // �̹��� fillAmount �ִ�� ���

                // ���� �̹��� fillAmount�� 1�� ���ٸ�
                // op.allowSceneActivation = true;
                yield break;
            }
        }


        yield return null;
    }


}
