using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 능력치 자연 회복을 담당할 클래스
/// </summary>
public class NatureRecovery
{
    /// <summary>
    /// 스테미너 자연회복 메서드
    /// </summary>
    /// <returns></returns>
    public static IEnumerator NatureRecoveryStamina()
    {
        float duraction = 5f;
        float time = 0;
        var playerData = InGameManager.Instance.Player.playerData;

        while(true)
        {
            // 스테미너가 모두 회복되었다면 실행
            if(playerData.currentStamina >= playerData.maxStamina)
            {
                playerData.currentStamina = playerData.maxStamina;
                yield break;
            }

            time += Time.deltaTime;
            playerData.currentStamina = Mathf.Lerp(playerData.currentStamina, playerData.currentStamina * 1.1f, time / duraction);

            yield return null;
        }
    }
}
