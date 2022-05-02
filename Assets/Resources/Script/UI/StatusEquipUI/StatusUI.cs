using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스테이터스 능력치창의 능력치를 설정하는 클래스
/// </summary>
public class StatusUI : MonoBehaviour
{
    #region TextMeshPro 변수
    [SerializeField,Tooltip("레벨")]
    TextMeshProUGUI lvText;

    [SerializeField, Tooltip("소지룬")]
    TextMeshProUGUI handRuneText;

    [SerializeField, Tooltip("필요룬")]
    TextMeshProUGUI maxRuneText;

    [SerializeField, Tooltip("(현재체력 / 최대체력)")]
    TextMeshProUGUI hpText;

    [SerializeField, Tooltip("공격력")]
    TextMeshProUGUI atkText;

    [SerializeField, Tooltip("방어력")]
    TextMeshProUGUI defText;
    #endregion

    #region 버튼 필드 직렬화 변수
    [SerializeField, Tooltip("체력 상승 버튼")]
    Button hpUpButton;

    [SerializeField, Tooltip("체력 빼기 버튼")]
    Button hpDownButton;

    [SerializeField, Tooltip("공격력 상승 버튼")]
    Button atkUpButton;

    [SerializeField, Tooltip("공격력 빼기 버튼")]
    Button atkDownButton;

    [SerializeField, Tooltip("방어력 상승 버튼")]
    Button defUpButton;

    [SerializeField, Tooltip("방어력 빼기 버튼")]
    Button defDownButton;

    [SerializeField, Tooltip("변경사항 적용 버튼")]
    Button applySetButton;
    #endregion

    /// <summary>
    /// 변경후 플레이어 데이터  (1)
    /// </summary>
    public static UsePlayerData playerData;

    /// <summary>
    /// 변경전 플레이어 데이터  (2)
    /// </summary>
    UsePlayerData originPlayerData;

    /// <summary>
    /// 스테이터스를 셋팅중일때 사용되는 Bool타입 변수
    /// </summary>
    bool statusSetting = false;

    private void Start()
    {
        // 이벤트 바인딩
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
    /// 플레이어 스테이터스 셋팅
    /// </summary>
    private void PlayerStatusSetting()
    {
        TextUpdate();

        originPlayerData = new UsePlayerData(playerData, InGameManager.Instance.Player.playerData.growthStat);
    }

    /// <summary>
    /// 텍스트값 업데이트
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
    /// 가진 소지룬만큼 레벨을 올릴수 있게 하는 메서드
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
            // 소지룬이 최대룬보다 많다면 실행
            if (playerData.currentRune >= playerData.maxRune)
            {
                data += pData;
                playerData.level += 1;
                playerData.currentRune -= playerData.maxRune;
                playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;
                UpText(tmp);
                UpText(lvText);
                TextUpdate();

                // UI 사운드 출력
                SoundManager.Instance.PlayAudio("UIClick",SoundPlayType.Multi);
            }
        }
    }

    /// <summary>
    /// 본래 능력치까지는 값을 내릴수 있도록 해주는 메서드
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
            // 본래 값보다 크다면 실행
            if (data > originData)
            {
                data -= pData;
                playerData.level -= 1;
                playerData.maxRune = playerData.growthStat.maxRune * playerData.level * playerData.growthStat.growthRune;
                playerData.currentRune += playerData.maxRune;
                UpText(tmp);
                UpText(lvText);
                TextUpdate();

                // UI 사운드 출력
                SoundManager.Instance.PlayAudio("UIClick", SoundPlayType.Multi);
            }
            // 값이 같다면 색상 리턴
            if(data == originData)
            {
                OneReturnText(tmp);
            }
            // 레벨이 본래값이라면 리턴
            if(playerData.level == originPlayerData.level)
            {
                OneReturnText(lvText);
            }
        }

    }

    /// <summary>
    /// 변경사항을 저장해주는 메서드
    /// </summary>
    public void LevelApplySetting()
    {
        if(originPlayerData != playerData)
        {
            statusSetting = false;
            originPlayerData = new UsePlayerData(playerData,InGameManager.Instance.Player.playerData.growthStat);
            ReturnText();

            // 사운드 출력
            SoundManager.Instance.PlayAudio("UIComplete",SoundPlayType.Single);
        }
    }

    /// <summary>
    /// 레벨 셋팅을 하지 않을시 값을 리턴
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
    /// 스테이터스를 상승시켰을때 텍스트 색상을 변경
    /// </summary>
    private void UpText(TextMeshProUGUI text)
    {
        text.color = new Color(165f / 255f, 165f / 255f, 255f / 255f);
    }

    /// <summary>
    /// 지정한 하나의 텍스트만 색상 리턴
    /// </summary>
    /// <param name="text"></param>
    private void OneReturnText(TextMeshProUGUI text)
    {
        text.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
    }

    /// <summary>
    /// 스테이터스가 본래값이라면 색상을 원상복귀
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

