using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스탯 + 장비 창 UI를 관리하는 클래스
/// </summary>
public class StatEquipWindowUI : MonoBehaviour
{
    #region 2번째 캔버스 그룹 조정 변수
    /// <summary>
    /// 스텟+장비창 활성화시 기존의 모든 UI를 Alpha 0으로 만들고 스텟+장비창만 출력시키기위한 캔버스 그룹
    /// </summary>
    [SerializeField]
    CanvasGroup Number2CanvasGroup;

    /// <summary>
    /// 2번째 캔버스그룹을 On/Off할 상태
    /// </summary>
    public static Define.UIDraw Num2CanvasState = Define.UIDraw.Activation;

    /// <summary>
    /// 2번째 캔버스그룹 변경전 상태
    /// </summary>
    Define.UIDraw Num2OriginState = Define.UIDraw.Inactive;
    #endregion

    #region 스탯+장비창 캔버스 그룹 조정 변수
    /// <summary>
    /// 스텟 + 장비창을 On/Off할 캔버스 그룹
    /// </summary>
    CanvasGroup StatEquipCanvasGroup;

    /// <summary>
    /// 스텟+장비창 그룹 설정을 지정할 상태
    /// </summary>
    public static Define.UIDraw StatEquipState = Define.UIDraw.Inactive;

    /// <summary>
    /// StatEquipState 의 변경전 상태
    /// </summary>
    Define.UIDraw StatEquipOriginState = Define.UIDraw.SlowlyInactive;
    #endregion

    private void Start()
    {
        StatEquipCanvasGroup = GetComponent<CanvasGroup>();
        StatEquipCanvasGroup.blocksRaycasts = false;     // 해당 캔버스 자식의 레이캐스트 미호출상태 전환
    }

    private void Update()
    {
        UIManager.Instance.SwitchWindowOption(ref StatEquipState, ref StatEquipOriginState, StatEquipCanvasGroup,NumberTwoCanvasSetting);
    }

    /// <summary>
    /// 2번째 캔버스 그룹과 장비+스탯창의 캔버스그룹을 상호 조정하는 메서드 (스탯+장비창의 상태가 변경될시 실행)
    /// </summary>
    private void NumberTwoCanvasSetting()
    {
        if(StatEquipState != StatEquipOriginState)
        {
            // 스탯창이 서서히 On 상태라면
            if (StatEquipState == Define.UIDraw.SlowlyActivation)
            {
                // 스탯 + 장비창을 제외한 모든창을 서서히 Off
                Num2CanvasState = Define.UIDraw.SlowlyInactive;
                UIManager.Instance.SwitchWindowOption(ref Num2CanvasState, ref Num2OriginState, Number2CanvasGroup);
                StatEquipCanvasGroup.blocksRaycasts = false;     // 해당 캔버스 자식의 레이캐스트 미호출상태 전환
            }
            // 스탯창이 서서히 Off 상태라면
            if (StatEquipState == Define.UIDraw.SlowlyInactive)
            {
                // 스탯 + 장비창을 제외한 모든창을 서서히 On
                Num2CanvasState = Define.UIDraw.SlowlyActivation;
                UIManager.Instance.SwitchWindowOption(ref Num2CanvasState, ref Num2OriginState, Number2CanvasGroup);
                StatEquipCanvasGroup.blocksRaycasts = true;     // 해당 캔버스 자식의 레이캐스트 호출상태 전환

            }
        }
    }
}
