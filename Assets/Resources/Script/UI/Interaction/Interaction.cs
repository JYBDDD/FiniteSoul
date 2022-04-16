using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 상호작용의 매개체로 사용되는 클래스 (Main : OrderInteraction  (플레이어가 가지고 있음))
/// </summary>
public class Interaction : MonoBehaviour
{
    /// <summary>
    /// 상호작용할 내용    / -> Save, Shop
    /// </summary>
    [SerializeField]
    public Define.InteractionTarget interactionTarget;

    /// <summary>
    /// 상호작용 텍스트
    /// </summary>
    [SerializeField]
    public string interactionText;

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 범위안으로 들어왔다면 실행
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionUI.interactionText = interactionText;
            InteractionUI.InteractionUIState = Define.UIDraw.Activation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 범위밖으로 벗어났다면 실행
        if (other.gameObject.CompareTag("Player"))
        {
            InteractionUI.InteractionUIState = Define.UIDraw.Inactive;
        }
    }


}
