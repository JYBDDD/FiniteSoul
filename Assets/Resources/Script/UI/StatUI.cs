using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� UI Ŭ����
/// </summary>
public class StatUI : MonoBehaviour
{
    [SerializeField,Tooltip("ü�� �̹���")]
    Image hp;   
    [SerializeField, Tooltip("ü�� �̹����� �θ� Rect")]
    RectTransform hpBar;

    [SerializeField, Tooltip("���� �̹���")]
    Image mp;   
    [SerializeField, Tooltip("���� �̹����� �θ� Rect")]
    RectTransform mpBar;  

    [SerializeField, Tooltip("���׹̳� �̹���")]
    Image stamina;   
    [SerializeField, Tooltip("���׹̳� �̹����� �θ� Rect")]
    RectTransform staminaBar; 

    // ������ ������
    public static UsePlayerData playerData;
    // ������ ������
    private UsePlayerData originData = new UsePlayerData();

    private void Update()
    {
        // �÷��̾� �����Ͱ� �����Ѵٸ� ����
        if (InGameManager.Instance.Player != null)
        {
            StatUpdate();
        }
    }

    /// <summary>
    /// ������ ����Ǿ��ٸ� ������Ʈ
    /// </summary>
    private void StatUpdate()
    {
        // �÷��̾��� �����Ͱ� ����Ǿ��ٸ� ����
        if (playerData.level != originData.level | playerData.currentStamina != originData.currentStamina |
            playerData.currentHp != originData.currentHp | playerData.currentMana != originData.currentMana)
        {
            // ���� ���� ����
            CurrentStatChange();

            // ���� �θ� ũ�� ����
            CurrentRectChange();

            originData = new UsePlayerData(playerData,playerData.growthStat);

            return;
        }
        
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    private void CurrentStatChange()
    {
        // ���� ���� ����
        hp.fillAmount = playerData.currentHp / playerData.maxHp;
        mp.fillAmount = playerData.currentMana / playerData.maxMana;
        stamina.fillAmount = playerData.currentStamina / playerData.maxStamina;
    }

    /// <summary>
    /// ���� Bar�� ũ�� ����
    /// </summary>
    private void CurrentRectChange()
    {
        // ���� �θ� ũ�� ����  (���ݸ�ŭ Bar�� ���̰� ����)
        hpBar.sizeDelta = new Vector2(playerData.maxHp, 20);
        mpBar.sizeDelta = new Vector2(playerData.maxMana, 20);
        staminaBar.sizeDelta = new Vector2(playerData.maxStamina * 2, 20);
    }
}
