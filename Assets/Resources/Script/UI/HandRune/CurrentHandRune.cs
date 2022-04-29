using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentHandRune : MonoBehaviour
{
    /// <summary>
    /// 현재 보유중인 룬 텍스트
    /// </summary>
    TextMeshProUGUI currentRuneText;

    /// <summary>
    /// 바뀌기전 룬의 보유 갯수
    /// </summary>
    float originRune = 0f;

    private void Start()
    {
        currentRuneText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        RuneSetting();
    }

    /// <summary>
    /// 보유중인 룬의 갯수가 달라졌거나, 인게임 플레이어가 Null 이 아닐때 호출
    /// </summary>
    private void RuneSetting()
    {
        if(InGameManager.Instance.Player != null)
        {
            if (InGameManager.Instance.Player.playerData.currentRune != originRune)
            {
                originRune = InGameManager.Instance.Player.playerData.currentRune;
                currentRuneText.text = $"{originRune}";
            }
        }
    }
}
