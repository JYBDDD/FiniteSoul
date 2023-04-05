using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ü���� UI�� �Ѱ��ϴ� Ŭ����
/// </summary>
public class UIManager : Singleton<UIManager>
{
    #region 2��° ĵ���� �׷� ���� ����
    [SerializeField, Tooltip("����+���â Ȱ��ȭ�� ������ ��� UI�� Alpha 0���� ����� ����+���â�� ��½�Ű������ ĵ���� �׷�")]
    public static CanvasGroup Number2CanvasGroup;

    /// <summary>
    /// 2��° ĵ�����׷��� On/Off�� ����
    /// </summary>
    public static Define.UIDraw Num2CanvasState = Define.UIDraw.Activation;

    /// <summary>
    /// 2��° ĵ�����׷� ������ ����
    /// </summary>
    public static Define.UIDraw Num2OriginState = Define.UIDraw.Inactive;
    #endregion

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


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Number2CanvasGroup = gameObject.transform.GetChild(0).GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        SwitchWindowOption(ref UIDrawState,ref UIOriginState, canvasGroup);
    }

    /// <summary>
    /// ������ ��/Ȱ�� ������ȯ�� ������ �޼ҵ�
    /// </summary>
    /// <param name="uiDraw">����� ���µ� ���� ��ȯ�Ǵ� UIDraw ���� (�ܺ��� ���� �������� ������ �޵��� ���� ������ Ref)</param>
    /// <param name="originDraw">uiDraw�� ���� �ٸ��� ����ǵ��� ���Ǵ� ���� (�ܺ��� ���� �������� ������ �޵��� ���� ������ Ref)</param>
    /// <param name="cGroup">On/Off�� ������ ĵ���� �׷�</param>
    /// <param name="addAction">�߰��� ��</param>
    public void SwitchWindowOption(ref Define.UIDraw uiDraw,ref Define.UIDraw originDraw,CanvasGroup cGroup, Action addAction = null)
    {
        // �Ѱ� UI ���°��� ����Ǿ��ٸ� ����
        if (uiDraw != originDraw)
        {
            switch (uiDraw)
            {
                // ��� Ȱ��ȭ
                case Define.UIDraw.Activation:
                    UIWindowActive(cGroup);
                    break;
                // õõ�� Ȱ��ȭ
                case Define.UIDraw.SlowlyActivation:
                    UIWindowSlowlyActive(cGroup);
                    break;
                // ��� ��Ȱ��
                case Define.UIDraw.Inactive:
                    UIWindowInActive(cGroup);
                    break;
                // õõ�� ��Ȱ��
                case Define.UIDraw.SlowlyInactive:
                    UIWindowSlowlyInActive(cGroup);
                    break;
            }
            addAction?.Invoke();

            originDraw = uiDraw;
        }
        
    }

    /// <summary>
    /// �Ѱ� UI Window ��� Ȱ��ȭ
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
    /// �Ѱ� UI Window õõ�� Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;

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
                cGroup.blocksRaycasts = true;
                cGroup.interactable = true;
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
            return;
        cGroup.alpha = 0;
        cGroup.blocksRaycasts = false;
        cGroup.interactable = false;
    }

    /// <summary>
    /// �Ѱ� UI Window õõ�� ��Ȱ��ȭ
    /// </summary>
    private void UIWindowSlowlyInActive(CanvasGroup cGroup)
    {
        if (cGroup == null)
            return;

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
                cGroup.blocksRaycasts = false;
                cGroup.interactable = false;
                yield break;
            }
            yield return null;
        }
    }
}