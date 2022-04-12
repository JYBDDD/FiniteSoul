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
    private Define.UIDraw UIOriginState = Define.UIDraw.Activation;

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
        // �Ѱ� UI ���°��� ����Ǿ��ٸ� ����
        if (UIDrawState != UIOriginState)
        {
            SwitchWindowOption();
        }
    }

    /// <summary>
    /// ������ ��/Ȱ�� ������ȯ�� ������ �޼ҵ�
    /// </summary>
    private void SwitchWindowOption()
    {
        switch (UIDrawState)
        {
            case Define.UIDraw.Activation:
                UIWindowActive();
                break;
            case Define.UIDraw.SlowlyActivation:
                UIWindowSlowlyActive();
                break;
            case Define.UIDraw.Inactive:
                UIWindowInActive();
                break;
            case Define.UIDraw.SlowlyInactive:
                UIWindowSlowlyInActive();
                break;
        }

        UIOriginState = UIDrawState;
    }

    /// <summary>
    /// �Ѱ� UI Window ��� Ȱ��ȭ
    /// </summary>
    private void UIWindowActive()
    {
        canvasGroup.alpha = 1;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyActive()
    {
        StartCoroutine(SlowlyActive());

        IEnumerator SlowlyActive()
        {
            float duraction = 1f;   // ���� �ð�
            float time = 0;

            while(time < duraction)
            {
                time += Time.deltaTime;

                canvasGroup.alpha = Mathf.Lerp(0, 1, time / duraction);
                yield return null;
            }

            if(time >= duraction)
            {
                canvasGroup.alpha = 1;
                yield break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// �Ѱ� UI Window ��� ��Ȱ��ȭ
    /// </summary>
    private void UIWindowInActive()
    {
        canvasGroup.alpha = 0;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� ��Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyInActive()
    {
        StartCoroutine(SlowlyInActive());

        IEnumerator SlowlyInActive()
        {
            float duraction = 1f;   // ���� �ð�
            float time = 0;

            while (time < duraction)
            {
                time += Time.deltaTime;

                canvasGroup.alpha = Mathf.Lerp(1, 0, time / duraction);
                yield return null;
            }

            if (time >= duraction)
            {
                canvasGroup.alpha = 0;
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