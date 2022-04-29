using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentHandRune : MonoBehaviour
{
    /// <summary>
    /// ���� �������� �� �ؽ�Ʈ
    /// </summary>
    TextMeshProUGUI currentRuneText;

    /// <summary>
    /// �ٲ���� ���� ���� ����
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
    /// �������� ���� ������ �޶����ų�, �ΰ��� �÷��̾ Null �� �ƴҶ� ȣ��
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
