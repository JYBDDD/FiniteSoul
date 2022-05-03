using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 스테이지 전환시 스테이지 이름을 출력시키는 클래스
/// </summary>
public class DrawStageWindow : MonoBehaviour
{
    /// <summary>
    /// 임시 호출시 사용될 정적 변수
    /// </summary>
    public static DrawStageWindow TempInstance;

    [SerializeField,Tooltip("스테이지 이름")]
    TextMeshProUGUI stageName;

    /// <summary>
    /// 해당 윈도우의 RectTransform
    /// </summary>
    RectTransform stageRectTrans;

    private void Start()
    {
        if (TempInstance != null)   // -> 시작씬으로 돌아갈경우 재호출이 되어 해당 인스턴스가 두번 생기는 오류 방지
        {
            Destroy(this.gameObject);
            return;
        }

        TempInstance = this;
        stageRectTrans = gameObject.GetComponent<RectTransform>();
    }

    /// <summary>
    /// 씬 전환 완료시 스테이지 윈도우를 출력시킬 메서드
    /// </summary>
    public void StageUIPrint()
    {
        StartCoroutine(DownUI());

        IEnumerator DownUI()
        {
            while(true)
            {
                if(stageRectTrans.anchoredPosition.y < 0)
                {
                    StartCoroutine(DrawText());
                    yield break;
                }

                stageRectTrans.anchoredPosition = Vector2.Lerp(stageRectTrans.anchoredPosition, new Vector2(0, -100), Time.deltaTime);

                yield return null;
            }
        }

        IEnumerator UpUI()
        {
            while (true)
            {
                if (stageRectTrans.anchoredPosition.y > 400)
                {
                    yield break;
                }

                stageRectTrans.anchoredPosition = Vector2.Lerp(stageRectTrans.anchoredPosition, new Vector2(0, 500), Time.deltaTime);

                yield return null;
            }
        }

        // 텍스트를 현 스테이지 이름 만큼 시간에 따라 출력
        IEnumerator DrawText()
        {
            List<string> textList = new List<string>();

            stageName.text = StageManager.stageData.name;

            int maxCount = stageName.text.Length;
            int startCount = -1;

            for(int i = 0; i < stageName.text.Length; ++i)
            {
                textList.Add(stageName.text.Substring(i,1));
            }

            stageName.text = "";

            while(true)
            {
                ++startCount;
                stageName.text += textList[startCount];

                yield return new WaitForSeconds(0.1f);

                // 스테이지 이름이 모두 출력되었다면 윈도우를 올린다
                if (stageName.text.Length == maxCount)
                {
                    StartCoroutine(UpUI());
                    yield break;
                }
            }
        }
    }


    /// <summary>
    /// 씬 전환이나 플레이어 사망시 설정 리셋
    /// </summary>
    public void StageUIReset()
    {
        // 스테이지 이름 초기화
        stageName.text = "";

        // 위치 초기값 재설정
        stageRectTrans.anchoredPosition = new Vector2(0, 400);
    }
}
