using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������ͽ� �ɷ�ġâ�� �ɷ�ġ�� �����ϴ� Ŭ����
/// </summary>
public class StatusUI : MonoBehaviour
{
    #region TextMeshPro ����
    [SerializeField,Tooltip("����")]
    TextMeshProUGUI lvText;

    [SerializeField, Tooltip("������")]
    TextMeshProUGUI handRuneText;

    [SerializeField, Tooltip("�ʿ��")]
    TextMeshProUGUI maxRuneText;

    [SerializeField, Tooltip("(����ü�� / �ִ�ü��)")]
    TextMeshProUGUI hpText;

    [SerializeField, Tooltip("���ݷ�")]
    TextMeshProUGUI atkText;

    [SerializeField, Tooltip("����")]
    TextMeshProUGUI defText;
    #endregion

    #region ��ư �ʵ� ����ȭ ����
    [SerializeField, Tooltip("ü�� ��� ��ư")]
    Button hpUpButton;

    [SerializeField, Tooltip("ü�� ���� ��ư")]
    Button hpDownButton;

    [SerializeField, Tooltip("���ݷ� ��� ��ư")]
    Button atkUpButton;

    [SerializeField, Tooltip("���ݷ� ���� ��ư")]
    Button atkDownButton;

    [SerializeField, Tooltip("���� ��� ��ư")]
    Button defUpButton;

    [SerializeField, Tooltip("���� ���� ��ư")]
    Button defDownButton;

    [SerializeField, Tooltip("������� ���� ��ư")]
    Button applySetButton;
    #endregion

    /// <summary>
    /// ������ �÷��̾� ������  (1)
    /// </summary>
    public static UsePlayerData playerData;

    /// <summary>
    /// ������ �÷��̾� ������  (2)
    /// </summary>
    UsePlayerData originPlayerData;

    /// <summary>
    /// �������ͽ��� �������϶� ���Ǵ� BoolŸ�� ����
    /// </summary>
    bool statusSetting = false;

    private void Start()
    {
        // �̺�Ʈ ���ε�
        StartCoroutine(EventBiding());
        applySetButton.onClick.AddListener(LevelApplySetting);
    }

    private void Update()
    {
        if (InGameManager.Instance.Player != null && playerData != originPlayerData && !statusSetting)
        {
            PlayerStatusSetting();
        }

        ReturnLevelSetting();
    }

    /// <summary>
    /// �÷��̾� �������ͽ� ����
    /// </summary>
    private void PlayerStatusSetting()
    {
        TextUpdate();

        originPlayerData = new UsePlayerData(playerData, InGameManager.Instance.Player.playerData.growthStat);
    }

    /// <summary>
    /// �ؽ�Ʈ�� ������Ʈ
    /// </summary>
    private void TextUpdate()
    {
        lvText.text = $"{playerData.level}";
        handRuneText.text = $"{playerData.currentRune}";
        maxRuneText.text = $"{playerData.maxRune}";
        hpText.text = $"{playerData.currentHp} / {playerData.maxHp}";
        atkText.text = $"{playerData.atk}";
        defText.text = $"{playerData.def}";
    }

    /// <summary>
    /// ���� �����鸸ŭ ������ �ø��� �ְ� �ϴ� �޼���
    /// </summary>
    public void LevelUpStatus(string name,float pData)
    {
        statusSetting = true;

        if (name == "Hp")
        {
            StatusManagment(ref playerData.maxHp,hpText);
        }
        if(name == "Atk")
        {
            StatusManagment(ref playerData.atk,atkText);
        }
        if(name == "Def")
        {
            StatusManagment(ref playerData.def,defText);
        }

        void StatusManagment(ref float data,TextMeshProUGUI tmp)
        {
            // �������� �ִ�麸�� ���ٸ� ����
            if (playerData.currentRune >= playerData.maxRune)
            {
                data += pData;
                playerData.level += 1;
                playerData.currentRune -= playerData.maxRune;
                playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;
                UpText(tmp);
                UpText(lvText);
                TextUpdate();

                // UI ���� ���
                SoundManager.Instance.PlayAudio("UIClick",SoundPlayType.Multi);
            }
        }
    }

    /// <summary>
    /// ���� �ɷ�ġ������ ���� ������ �ֵ��� ���ִ� �޼���
    /// </summary>
    public void LevelDownStatus(string name,float pData)
    {
        statusSetting = true;

        if (name == "Hp")
        {
            StatusManagment(originPlayerData.maxHp,ref playerData.maxHp, hpText);
        }
        if (name == "Atk")
        {
            StatusManagment(originPlayerData.atk,ref playerData.atk, atkText);
        }
        if (name == "Def")
        {
            StatusManagment(originPlayerData.def,ref playerData.def, defText);
        }

        void StatusManagment(float originData,ref float data, TextMeshProUGUI tmp)
        {
            // ���� ������ ũ�ٸ� ����
            if (data > originData)
            {
                data -= pData;
                playerData.level -= 1;
                playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;
                playerData.currentRune += playerData.maxRune;
                UpText(tmp);
                UpText(lvText);
                TextUpdate();

                // UI ���� ���
                SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
            }
            // ���� ���ٸ� ���� ����
            if(data == originData)
            {
                OneReturnText(tmp);
            }
            // ������ �������̶�� ����
            if(playerData.level == originPlayerData.level)
            {
                OneReturnText(lvText);
            }
        }

    }

    /// <summary>
    /// ��������� �������ִ� �޼���
    /// </summary>
    public void LevelApplySetting()
    {
        if(originPlayerData != playerData)
        {
            statusSetting = false;
            originPlayerData = new UsePlayerData(playerData,InGameManager.Instance.Player.playerData.growthStat);
            ReturnText();

            // ���� ���
            SoundManager.Instance.PlayAudio("UIComplete",SoundPlayType.Single);
        }
    }

    /// <summary>
    /// ���� ������ ���� ������ ���� ����
    /// </summary>
    public void ReturnLevelSetting()
    {
        if(statusSetting)
        {
            if (!OrderInteraction.playerInteractionTrue)
            {
                originPlayerData.growthStat ??= playerData.growthStat;
                statusSetting = false;
                playerData = new UsePlayerData(originPlayerData, InGameManager.Instance.Player.playerData.growthStat);
                ReturnText();
            }
        }

    }

    /// <summary>
    /// �������ͽ��� ��½������� �ؽ�Ʈ ������ ����
    /// </summary>
    private void UpText(TextMeshProUGUI text)
    {
        text.color = new Color(165f / 255f, 165f / 255f, 255f / 255f);
    }

    /// <summary>
    /// ������ �ϳ��� �ؽ�Ʈ�� ���� ����
    /// </summary>
    /// <param name="text"></param>
    private void OneReturnText(TextMeshProUGUI text)
    {
        text.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
    }

    /// <summary>
    /// �������ͽ��� �������̶�� ������ ���󺹱�
    /// </summary>
    private void ReturnText()
    {
        lvText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        handRuneText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        maxRuneText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        hpText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        atkText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        defText.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
    }

    IEnumerator EventBiding()
    {
        while(InGameManager.Instance.Player == null)
        {
            yield return null;
        }

        if (InGameManager.Instance.Player != null)
        {
            hpUpButton.onClick.AddListener(() => LevelUpStatus("Hp", playerData.growthStat.extraHp));
            atkUpButton.onClick.AddListener(() => LevelUpStatus("Atk", playerData.growthStat.extraAtk));
            defUpButton.onClick.AddListener(() => LevelUpStatus("Def", playerData.growthStat.extraDef));
            hpDownButton.onClick.AddListener(() => LevelDownStatus("Hp", playerData.growthStat.extraHp));
            atkDownButton.onClick.AddListener(() => LevelDownStatus("Atk", playerData.growthStat.extraAtk));
            defDownButton.onClick.AddListener(() => LevelDownStatus("Def", playerData.growthStat.extraDef));

            yield break;
        }
    }
}

