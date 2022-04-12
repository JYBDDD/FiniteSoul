using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전체적인 UI를 총괄하는 클래스
/// </summary>
public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// UI 총괄 상태
    /// </summary>
    public static Define.UIDraw UIDrawState = Define.UIDraw.Inactive;

    /// <summary>
    /// 바뀌기 전 UI의 상태
    /// </summary>
    protected Define.UIDraw UIOriginState = Define.UIDraw.Activation;

    /// <summary>
    /// 최상위에 위치한 캔버스 그룹
    /// </summary>
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        Debug.Log($" 본래 값은 Inactive 이여야 함{UIOriginState}");
        SwitchWindowOption();
    }

    /// <summary>
    /// 윈도우 비/활성 상태전환을 실행할 메소드
    /// </summary>
    /// <param name="cGroup">cGroup 이 Null 이라면 UIManager의 canvasGroup이 설정되도록 함</param>
    protected virtual void SwitchWindowOption(CanvasGroup cGroup = null)
    {
        // 총괄 UI 상태값이 변경되었다면 실행
        if (UIDrawState != UIOriginState)
        {
            switch (UIDrawState)
            {
                case Define.UIDraw.Activation:
                    UIWindowActive(cGroup);
                    break;
                case Define.UIDraw.SlowlyActivation:
                    UIWindowSlowlyActive(cGroup);
                    break;
                case Define.UIDraw.Inactive:
                    UIWindowInActive(cGroup);
                    break;
                case Define.UIDraw.SlowlyInactive:
                    UIWindowSlowlyInActive(cGroup);
                    break;
            }

            UIOriginState = UIDrawState;
        }
        
    }

    /// <summary>
    /// 총괄 UI Window 즉시 활성화
    /// </summary>
    /// <param name="cGroup">cGroup 이 Null 이라면 UIManager의 canvasGroup이 설정되도록 함</param>
    protected void UIWindowActive(CanvasGroup cGroup = null)
    {
        if(cGroup == null)
        {
            cGroup = canvasGroup;
        }
        canvasGroup.alpha = 1;
    }

    /// <summary>
    /// 총괄 UI Window 천천히 활성화
    /// </summary>
    /// <param name="cGroup">cGroup 이 Null 이라면 UIManager의 canvasGroup이 설정되도록 함</param>
    protected void UIWindowSlowlyActive(CanvasGroup cGroup = null)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }

        StartCoroutine(SlowlyActive());

        IEnumerator SlowlyActive()
        {
            float duraction = 1f;   // 지속 시간
            float time = 0;

            while(time < duraction)
            {
                time += Time.deltaTime;

                cGroup.alpha = Mathf.Lerp(0, 1, time / duraction);
                yield return null;
            }

            if(time >= duraction)
            {
                cGroup.alpha = 1;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 총괄 UI Window 즉시 비활성화
    /// </summary>
    /// <param name="cGroup">cGroup 이 Null 이라면 UIManager의 canvasGroup이 설정되도록 함</param>
    protected void UIWindowInActive(CanvasGroup cGroup = null)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }
        cGroup.alpha = 0;
    }

    /// <summary>
    /// 총괄 UI Window 천천히 비활성화
    /// </summary>
    /// <param name="cGroup">cGroup 이 Null 이라면 UIManager의 canvasGroup이 설정되도록 함</param>
    protected void UIWindowSlowlyInActive(CanvasGroup cGroup = null)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }

        StartCoroutine(SlowlyInActive());

        IEnumerator SlowlyInActive()
        {
            float duraction = 1f;   // 지속 시간
            float time = 0;

            while (time < duraction)
            {
                time += Time.deltaTime;

                cGroup.alpha = Mathf.Lerp(1, 0, time / duraction);
                yield return null;
            }

            if (time >= duraction)
            {
                cGroup.alpha = 0;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 초기 스텟 설정
    /// </summary>
    public virtual void InitStatSetting()
    {

    }

    
}