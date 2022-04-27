using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� + ��� â UI�� �����ϴ� Ŭ����
/// </summary>
public class StatEquipWindowUI : MonoBehaviour
{
    

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
        if (StatEquipState != StatEquipOriginState)
        {
            CanvasGroup Num2Canvas = UIManager.Number2CanvasGroup;

            // ����â�� ������ On ���¶��
            if (StatEquipState == Define.UIDraw.SlowlyActivation)
            {
                // ���� + ���â�� ������ ���â�� ������ Off
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);
            }
            // ����â�� ������ Off ���¶��
            if (StatEquipState == Define.UIDraw.SlowlyInactive)
            {
                // ���� + ���â�� ������ ���â�� ������ On
                UIManager.Num2CanvasState = Define.UIDraw.SlowlyActivation;
                UIManager.Instance.SwitchWindowOption(ref UIManager.Num2CanvasState, ref UIManager.Num2OriginState, Num2Canvas);

            }
        }
    }
}
