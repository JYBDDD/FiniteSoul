using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ü���� UI�� �Ѱ��ϴ� Ŭ����
/// </summary>
public class UIManager : Singleton<UIManager>
{
    /// <summary>
    /// UI �Ѱ� ����
    /// </summary>
    public static Define.UIDraw UIDrawState = Define.UIDraw.Inactive;

    /// <summary>
    /// �ٲ�� �� UI�� ����
    /// </summary>
    protected Define.UIDraw UIOriginState = Define.UIDraw.Activation;

    /// <summary>
    /// �ֻ����� ��ġ�� ĵ���� �׷�
    /// </summary>
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        //Debug.Log($" ���� ���� Inactive �̿��� ��{UIOriginState}");
        SwitchWindowOption(UIDrawState,UIOriginState,canvasGroup);
    }

    /// <summary>
    /// ������ ��/Ȱ�� ������ȯ�� ������ �޼ҵ�
    /// </summary>
    /// <param name="uiDraw">����� ���µ� ���� ��ȯ�Ǵ� UIDraw ����</param>
    /// <param name="originDraw">uiDraw�� ���� �ٸ��� ����ǵ��� ���Ǵ� ����</param>
    /// <param name="cGroup">On/Off�� ������ ĵ���� �׷�</param>
    public void SwitchWindowOption(Define.UIDraw uiDraw,Define.UIDraw originDraw,CanvasGroup cGroup)
    {
        // �Ѱ� UI ���°��� ����Ǿ��ٸ� ����
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

            originDraw = uiDraw;
        }
        
    }

    /// <summary>
    /// �Ѱ� UI Window ��� Ȱ��ȭ
    /// </summary>
    private void UIWindowActive(CanvasGroup cGroup)
    {
        cGroup.alpha = 1;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }

        StartCoroutine(SlowlyActive());

        IEnumerator SlowlyActive()
        {
            float duraction = 1f;   // ���� �ð�
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
    /// �Ѱ� UI Window ��� ��Ȱ��ȭ
    /// </summary>
    private void UIWindowInActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }
        cGroup.alpha = 0;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� ��Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyInActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
        {
            cGroup = canvasGroup;
        }

        StartCoroutine(SlowlyInActive());

        IEnumerator SlowlyInActive()
        {
            float duraction = 1f;   // ���� �ð�
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
}