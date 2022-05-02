using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� ȹ��������� ��µǴ� ������ Ŭ����
/// </summary>
public class ItemAcquisitionWindow : MonoBehaviour
{
    /// <summary>
    /// ���� �Ѱ��ִ� �뵵�� �ӽ� ���� ���� ����
    /// </summary>
    public static ItemAcquisitionWindow TempInstance;

    [SerializeField, Tooltip("�������� �ؽ�Ʈ ������ ����� SubText")]
    TextMeshProUGUI subText;

    [SerializeField, Tooltip("������ �̸�")]
    TextMeshProUGUI itemNameText;

    [SerializeField, Tooltip("����� ������ �̹���")]
    Image itemImg;

    /// <summary>
    /// Item UI ����
    /// </summary>
    public Define.UIDraw ItemUIState = Define.UIDraw.Inactive;

    /// <summary>
    /// �ٲ�� �� Item UI�� ����
    /// </summary>
    Define.UIDraw ItemUIOriginState = Define.UIDraw.Activation;

    /// <summary>
    /// Item UI ĵ���� �׷�
    /// </summary>
    private CanvasGroup itemUICanvasGroup;

    private void Awake()
    {
        TempInstance = this;
        itemUICanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        // UI ��Ȱ��ȭ
        UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);
    }

    private void Update()
    {
        AcquisitionWindowExit();
    }

    /// <summary>
    /// ������ ȹ�� ������ ���
    /// </summary>
    public void AcquisitionWindowPrint(UseItemData useItemData)
    {
        // ������ ȹ�� ���� ���
        SoundManager.Instance.PlayAudio("ItemAcheive", SoundPlayType.Single);

        ItemDataSetting(useItemData);

        // ��� Ȱ��ȭ
        ItemUIState = Define.UIDraw.Activation;
        UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);

        // SubText ���� Ȱ��ȭ
        StartCoroutine(SubTextSpread());
    }

    /// <summary>
    /// ������ ȹ�� ������ �ݱ�
    /// </summary>
    public void AcquisitionWindowExit()
    {
        // �����찡 Ȱ��ȭ �Ǿ��ִ� ���¿��� FŰ �Է½� ����
        if(ItemUIState == Define.UIDraw.Activation && Input.GetKeyDown(KeyCode.F))
        {
            // ������ ��Ȱ��ȭ
            ItemUIState = Define.UIDraw.SlowlyInactive;
            UIManager.Instance.SwitchWindowOption(ref ItemUIState, ref ItemUIOriginState, itemUICanvasGroup);
        }
    }

    /// <summary>
    /// ������ ������ ����
    /// </summary>
    private void ItemDataSetting(UseItemData useItemData)
    {
        itemNameText.text = $"{useItemData.name}";
        itemImg.sprite = Resources.Load<Sprite>(useItemData.resourcePath);

        SubTextAlpha(subText.color, 0f / 255f);
    }

    /// <summary>
    /// SubText ������ Ȱ��ȭ �� �ڷ�ƾ
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
    /// SubText ���İ� ������ �޼���
    /// </summary>
    /// <param name="subColor"></param>
    /// <param name="alpha"></param>
    private Color SubTextAlpha(Color subColor,float alpha)
    {
        // SubText ���İ� ������
        return new Color(subColor.r, subColor.g, subColor.b, alpha);
    }

}
