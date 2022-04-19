using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� + ��� â UI�� �����ϴ� Ŭ����
/// </summary>
public class StatEquipWindowUI : MonoBehaviour
{
    #region 2��° ĵ���� �׷� ���� ����
    /// <summary>
    /// ����+���â Ȱ��ȭ�� ������ ��� UI�� Alpha 0���� ����� ����+���â�� ��½�Ű������ ĵ���� �׷�
    /// </summary>
    [SerializeField]
    CanvasGroup Number2CanvasGroup;

    /// <summary>
    /// 2��° ĵ�����׷��� On/Off�� ����
    /// </summary>
    public static Define.UIDraw Num2CanvasState = Define.UIDraw.Activation;

    /// <summary>
    /// 2��° ĵ�����׷� ������ ����
    /// </summary>
    Define.UIDraw Num2OriginState = Define.UIDraw.Inactive;
    #endregion

    #region ����+���â ĵ���� �׷� ���� ����
    /// <summary>
    /// ���� + ���â�� On/Off�� ĵ���� �׷�
    /// </summary>
    CanvasGroup StatEquipCanvasGroup;

    /// <summary>
    /// ����+���â �׷� ������ ������ ����
    /// </summary>
    public static Define.UIDraw StatEquipState = Define.UIDraw.Inactive;

    /// <summary>
    /// StatEquipState �� ������ ����
    /// </summary>
    Define.UIDraw StatEquipOriginState = Define.UIDraw.SlowlyInactive;
    #endregion

    private void Start()
    {
        StatEquipCanvasGroup = GetComponent<CanvasGroup>();
        StatEquipCanvasGroup.blocksRaycasts = false;     // �ش� ĵ���� �ڽ��� ����ĳ��Ʈ ��ȣ����� ��ȯ
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref StatEquipState, ref StatEquipOriginState, StatEquipCanvasGroup,NumberTwoCanvasSetting);
    }

    /// <summary>
    /// 2��° ĵ���� �׷�� ���+����â�� ĵ�����׷��� ��ȣ �����ϴ� �޼��� (����+���â�� ���°� ����ɽ� ����)
    /// </summary>
    private void NumberTwoCanvasSetting()
    {
        if(StatEquipState != StatEquipOriginState)
        {
            // ����â�� ������ On ���¶��
            if (StatEquipState == Define.UIDraw.SlowlyActivation)
            {
                // ���� + ���â�� ������ ���â�� ������ Off
                Num2CanvasState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref Num2CanvasState, ref Num2OriginState, Number2CanvasGroup);
                StatEquipCanvasGroup.blocksRaycasts = false;     // �ش� ĵ���� �ڽ��� ����ĳ��Ʈ ��ȣ����� ��ȯ
            }
            // ����â�� ������ Off ���¶��
            if (StatEquipState == Define.UIDraw.SlowlyInactive)
            {
                // ���� + ���â�� ������ ���â�� ������ On
                Num2CanvasState = Define.UIDraw.SlowlyActivation;
                UIManager.Instance.SwitchWindowOption(ref Num2CanvasState, ref Num2OriginState, Number2CanvasGroup);
                StatEquipCanvasGroup.blocksRaycasts = true;     // �ش� ĵ���� �ڽ��� ����ĳ��Ʈ ȣ����� ��ȯ

            }
        }
    }
}
