using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스텟 UI 클래스
/// </summary>
public class StatUI : MonoBehaviour
{
    [SerializeField]
    Image hp;                   // 체력 이미지
    [SerializeField]
    RectTransform hpBar;        // 체력 이미지의 부모 Rect

    [SerializeField]
    Image mp;                   // 마나 이미지
    [SerializeField]
    RectTransform mpBar;        // 마나 이미지의 부모 Rect

    [SerializeField]
    Image stamina;              // 스테미너 이미지
    [SerializeField]
    RectTransform staminaBar;   // 스테미너 이미지의 부모 Rect

    // 변경후 데이터
    public UsePlayerData playerData;
    // 변경전 데이터
    private UsePlayerData originData;

    private void Update()
    {
        // 플레이어 데이터가 존재한다면 실행
        if (InGameManager.Instance.Player != null)
        {
            StatUpdate();
        }
    }

    /// <summary>
    /// 스텟이 변경되었다면 업데이트
    /// </summary>
    private void StatUpdate()
    {
        // 플레이어 데이터의 인덱스가 정상값이 아니라면 값을 삽입
        if (playerData.index <= 999)
        {
            originData = playerData;
            playerData = InGameManager.Instance.Player.playerData;
        }

        // 플레이어의 데이터가 변경되었다면 실행
        if (playerData != originData | playerData.level != originData.level)
        {
            // 현재 스텟 변경
            CurrentStatChange();

            // 레벨이 상승하였다면 실행
            if (playerData.level != originData.level)
            {
                // 스텟 부모 크기 설정
                CurrentRectChange();
            }
            
            originData = new UsePlayerData(playerData, InGameManager.Instance.Player.playerData.growthStat);

            return;
        }
        
    }

    /// <summary>
    /// 현재 스텟 변경
    /// </summary>
    private void CurrentStatChange()
    {
        // 현재 스텟 설정
        hp.fillAmount = playerData.currentHp / playerData.maxHp;
        mp.fillAmount = playerData.currentMana / playerData.maxMana;
        stamina.fillAmount = playerData.currentStamina / playerData.maxStamina;
    }

    /// <summary>
    /// 현재 Bar의 크기 변경
    /// </summary>
    private void CurrentRectChange()
    {
        // 스텟 부모 크기 설정  (스텟만큼 Bar의 길이가 증가)
        hpBar.sizeDelta = new Vector2(playerData.maxHp, 20);
        mpBar.sizeDelta = new Vector2(playerData.maxMana, 20);
        staminaBar.sizeDelta = new Vector2(playerData.maxStamina * 2, 20);
    }
}
