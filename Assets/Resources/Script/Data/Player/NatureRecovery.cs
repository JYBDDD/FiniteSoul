using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �ɷ�ġ �ڿ� ȸ���� ����� Ŭ����
/// </summary>
public class NatureRecovery
{
    /// <summary>
    /// ���׹̳� �ڿ�ȸ�� �޼���
    /// </summary>
    /// <returns></returns>
    public static IEnumerator NatureRecoveryStamina()
    {
        float duraction = 5f;
        float time = 0;
        var playerData = InGameManager.Instance.Player.playerData;

        while(true)
        {
            // ���׹̳ʰ� ��� ȸ���Ǿ��ٸ� ����
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
