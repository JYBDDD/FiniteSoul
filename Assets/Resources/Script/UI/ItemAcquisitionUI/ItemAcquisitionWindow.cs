using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템을 획득했을경우 출력되는 윈도우 클래스
/// </summary>
public class ItemAcquisitionWindow : MonoBehaviour
{
    /// <summary>
    /// 값을 넘겨주는 용도로 임시 사용될 정적 변수
    /// </summary>
    public static ItemAcquisitionWindow TempInstance;

    [SerializeField, Tooltip("순간적인 텍스트 번짐에 사용한 SubText")]
    TextMeshProUGUI subText;

    [SerializeField, Tooltip("아이템 이름")]
    TextMeshProUGUI itemNameText;

    [SerializeField, Tooltip("출력할 아이템 이미지")]
    Image itemImg;

    /// <summary>
    /// Item UI 상태
    /// </summary>
    public Define.UIDraw ItemUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// 바뀌기 전 Item UI의 상태
    /// </summary>
    Define.UIDraw ItemUIOriginState = Define.UIDraw.Activation;

    /// <summary>
    /// Item UI 캔버스 그룹
    /// </summary>
    private CanvasGroup itemUICanvasGroup;

    private void Awake()
    {
        TempInstance = this;
        itemUICanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // UI 비활성화
        UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);
    }

    private void Update()
    {
        AcquisitionWindowExit();
    }

    /// <summary>
    /// 아이템 획득 윈도우 출력
    /// </summary>
    public void AcquisitionWindowPrint(UseItemData useItemData)
    {
        // 아이템 획득 사운드 출력
        SoundManager.Instance.PlayAudio("ItemAcheive", SoundPlayType.Single);

        ItemDataSetting(useItemData);

        // 즉시 활성화
        ItemUIState = Define.UIDraw.Activation;
        UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);

        // SubText 번짐 활성화
        StartCoroutine(SubTextSpread());
    }

    /// <summary>
    /// 아이템 획득 윈도우 닫기
    /// </summary>
    public void AcquisitionWindowExit()
    {
        // 윈도우가 활성화 되어있는 상태에서 F키 입력시 실행
        if(ItemUIState == Define.UIDraw.Activation && Input.GetKeyDown(KeyCode.F))
        {
            // 서서히 비활성화
            ItemUIState = Define.UIDraw.SlowlyInactive;
            UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);
        }
    }

    /// <summary>
    /// 아이템 데이터 설정
    /// </summary>
    private void ItemDataSetting(UseItemData useItemData)
    {
        itemNameText.text = $"{useItemData.name}";
        itemImg.sprite = Resources.Load<Sprite>(useItemData.resourcePath);

        SubTextAlpha(subText.color, 0f / 255f);
    }

    /// <summary>
    /// SubText 번짐을 활성화 할 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SubTextSpread()
    {
        float duraction = 0.5f;
        float time = 0;

        while(time < duraction)
        {
            time += Time.deltaTime;

            subText.color = Color.Lerp(subText.color, SubTextAlpha(subText.color, 150f / 255f), time / duraction);

            yield return null;
        }

        if(time > duraction)
        {
            duraction = 1f;
            time = 0;
            while(true)
            {
                if(time > duraction)
                {
                    yield break;
                }

                time += Time.deltaTime;

                subText.color = Color.Lerp(subText.color, SubTextAlpha(subText.color, 0f / 255f), time / duraction);

                yield return null;
            }
        }


        yield return null;
    }

    /// <summary>
    /// SubText 알파값 재조정 메서드
    /// </summary>
    /// <param name="subColor"></param>
    /// <param name="alpha"></param>
    private Color SubTextAlpha(Color subColor,float alpha)
    {
        // SubText 알파값 재조정
        return new Color(subColor.r, subColor.g, subColor.b, alpha);
    }

}
