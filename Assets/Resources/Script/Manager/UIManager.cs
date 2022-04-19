using System;
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
        SwitchWindowOption(ref UIDrawState,ref UIOriginState, canvasGroup);
    }

    /// <summary>
    /// 윈도우 비/활성 상태전환을 실행할 메소드
    /// </summary>
    /// <param name="uiDraw">변경시 상태도 같이 전환되는 UIDraw 상태 (외부의 넣은 변수값도 영향을 받도록 값을 참조함 Ref)</param>
    /// <param name="originDraw">uiDraw와 값이 다를시 변경되도록 사용되는 상태 (외부의 넣은 변수값도 영향을 받도록 값을 참조함 Ref)</param>
    /// <param name="cGroup">On/Off를 실행할 캔버스 그룹</param>
    /// <param name="addAction">추가할 값</param>
    public void SwitchWindowOption(ref Define.UIDraw uiDraw,ref Define.UIDraw originDraw,CanvasGroup cGroup, Action addAction = null)
    {
        // 총괄 UI 상태값이 변경되었다면 실행
        if (uiDraw != originDraw)
        {
            switch (uiDraw)
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
            addAction?.Invoke();

            originDraw = uiDraw;
        }
        
    }

    /// <summary>
    /// 총괄 UI Window 즉시 활성화
    /// </summary>
    private void UIWindowActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;
        cGroup.alpha = 1;
        cGroup.blocksRaycasts = true;
        cGroup.interactable = true;
    }

    /// <summary>
    /// 총괄 UI Window 천천히 활성화
    /// </summary>
    private void UIWindowSlowlyActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;

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
                cGroup.blocksRaycasts = true;
                cGroup.interactable = true;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// 총괄 UI Window 즉시 비활성화
    /// </summary>
    private void UIWindowInActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;
        cGroup.alpha = 0;
        cGroup.blocksRaycasts = false;
        cGroup.interactable = false;
    }

    /// <summary>
    /// 총괄 UI Window 천천히 비활성화
    /// </summary>
    private void UIWindowSlowlyInActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;

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
                cGroup.blocksRaycasts = false;
                cGroup.interactable = false;
                yield break;
            }
            yield return null;
        }
    }
}