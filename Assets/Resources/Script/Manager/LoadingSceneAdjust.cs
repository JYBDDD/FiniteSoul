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
    Image loadingBackProgress;  // 로딩 배경 이미지

    [SerializeField]
    Image worldImg;             // 배경 이미지

    [SerializeField]
    Text loadingText;           // 로딩 진행 텍스트

    private void Start()
    {
        // UIManager 드로잉 비활성화
        UIManager.UIDrawState = Define.UIDraw.Inactive;
        // 인게임 리스트 클리어
        InGameManager.Instance.ClearInGame();

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScene");     // 먼저 로딩씬 전환
        nextSceneName = sceneName;
        StageManager.StageDataInsert(nextSceneName);
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

            // 비동기 작업이 완료된 후 실행
            op.completed += (AsyncOperation p) => 
            {
                // 플레이어 스폰
                StageManager.Instance.PlayerSpawn();
                // 몬스터 스폰
                StageManager.Instance.MonsterSpawn();
                // UIManager 드로잉 서서히 활성화
                UIManager.UIDrawState = Define.UIDraw.SlowlyActivation;
            };

            yield break;
        }
    }

    
}
