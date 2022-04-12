using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� UI Ŭ����
/// </summary>
public class StatUI : UIManager
{
    [SerializeField]
    Image hp;                   // ü�� �̹���
    [SerializeField]
    RectTransform hpBar;        // ü�� �̹����� �θ� Rect

    [SerializeField]
    Image mp;                   // ���� �̹���
    [SerializeField]
    RectTransform mpBar;        // ���� �̹����� �θ� Rect

    [SerializeField]
    Image stamina;              // ���׹̳� �̹���
    [SerializeField]
    RectTransform staminaBar;   // ���׹̳� �̹����� �θ� Rect

    // ������ ������
    public UsePlayerData playerData;
    // ������ ������
    public UsePlayerData originData;

    private void Update()
    {
        if(playerData != originData)
        {
            StatUpdate();
        }
    }

    /// <summary>
    /// ������ ����Ǿ��ٸ� ������Ʈ
    /// </summary>
    private void StatUpdate()
    {
        // ���� ���� ����
        CurrentStatChange();

        // ������ ����Ͽ��ٸ� ����
        if(playerData.level != originData.level)
        {
            // ���� �θ� ũ�� ����
            CurrentRectChange();
        }

        originData = playerData;
    }

    public override void InitStatSetting()
    {
        base.InitStatSetting();

        playerData = InGameManager.Instance.Player.playerData;
        originData = playerData;

        // ���� ���� ����
        CurrentStatChange();

        // ���� �θ� ũ�� ����
        CurrentRectChange();
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