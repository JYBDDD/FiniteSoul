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
        Debug.Log($" ���� ���� Inactive �̿��� ��{UIOriginState}");
        SwitchWindowOption();
    }

    /// <summary>
    /// ������ ��/Ȱ�� ������ȯ�� ������ �޼ҵ�
    /// </summary>
    /// <param name="cGroup">cGroup �� Null �̶�� UIManager�� canvasGroup�� �����ǵ��� ��</param>
    protected virtual void SwitchWindowOption(CanvasGroup cGroup = null)
    {
        // �Ѱ� UI ���°��� ����Ǿ��ٸ� ����
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
    /// �Ѱ� UI Window ��� Ȱ��ȭ
    /// </summary>
    /// <param name="cGroup">cGroup �� Null �̶�� UIManager�� canvasGroup�� �����ǵ��� ��</param>
    protected void UIWindowActive(CanvasGroup cGroup = null)
    {
        if(cGroup == null)
        {
            cGroup = canvasGroup;
        }
        canvasGroup.alpha = 1;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� Ȱ��ȭ
    /// </summary>
    /// <param name="cGroup">cGroup �� Null �̶�� UIManager�� canvasGroup�� �����ǵ��� ��</param>
    protected void UIWindowSlowlyActive(CanvasGroup cGroup = null)
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
    /// <param name="cGroup">cGroup �� Null �̶�� UIManager�� canvasGroup�� �����ǵ��� ��</param>
    protected void UIWindowInActive(CanvasGroup cGroup = null)
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
    /// <param name="cGroup">cGroup �� Null �̶�� UIManager�� canvasGroup�� �����ǵ��� ��</param>
    protected void UIWindowSlowlyInActive(CanvasGroup cGroup = null)
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

    /// <summary>
    /// �ʱ� ���� ����
    /// </summary>
    public virtual void InitStatSetting()
    {

    }

    
}