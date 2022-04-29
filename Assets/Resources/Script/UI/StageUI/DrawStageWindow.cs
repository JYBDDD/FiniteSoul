using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �������� ��ȯ�� �������� �̸��� ��½�Ű�� Ŭ����
/// </summary>
public class DrawStageWindow : MonoBehaviour
{
    /// <summary>
    /// �ӽ� ȣ��� ���� ���� ����
    /// </summary>
    public static DrawStageWindow TempInstance;

    [SerializeField,Tooltip("�������� �̸�")]
    TextMeshProUGUI stageName;

    /// <summary>
    /// �ش� �������� RectTransform
    /// </summary>
    RectTransform stageRectTrans;

    private void Awake()
    {
        TempInstance = this;
        stageRectTrans = gameObject.GetComponent<RectTransform>();
    }

    /// <summary>
    /// �� ��ȯ �Ϸ�� �������� �����츦 ��½�ų �޼���
    /// </summary>
    public void StageUIPrint()
    {
        StartCoroutine(DownUI());

        IEnumerator DownUI()
        {
            while(true)
            {
                if(stageRectTrans.anchoredPosition.y < 0)
                {
                    StartCoroutine(DrawText());
                    yield break;
                }

                stageRectTrans.anchoredPosition = Vector2.Lerp(stageRectTrans.anchoredPosition, new Vector2(0, -100), Time.deltaTime);

                yield return null;
            }
        }

        IEnumerator UpUI()
        {
            while (true)
            {
                if (stageRectTrans.anchoredPosition.y > 400)
                {
                    yield break;
                }

                stageRectTrans.anchoredPosition = Vector2.Lerp(stageRectTrans.anchoredPosition, new Vector2(0, 500), Time.deltaTime);

                yield return null;
            }
        }

        // �ؽ�Ʈ�� �� �������� �̸� ��ŭ �ð��� ���� ���
        IEnumerator DrawText()
        {
            List<string> textList = new List<string>();

            stageName.text = StageManager.stageData.name;

            int maxCount = stageName.text.Length;
            int startCount = -1;

            for(int i = 0; i < stageName.text.Length; ++i)
            {
                textList.Add(stageName.text.Substring(i,1));
            }

            stageName.text = "";

            while(true)
            {
                ++startCount;
                stageName.text += textList[startCount];

                yield return new WaitForSeconds(0.1f);

                // �������� �̸��� ��� ��µǾ��ٸ� �����츦 �ø���
                if (stageName.text.Length == maxCount)
                {
                    StartCoroutine(UpUI());
                    yield break;
                }
            }
        }
    }


    /// <summary>
    /// �� ��ȯ�̳� �÷��̾� ����� ���� ����
    /// </summary>
    public void StageUIReset()
    {
        // �������� �̸� �ʱ�ȭ
        stageName.text = "";

        // ��ġ �ʱⰪ �缳��
        stageRectTrans.anchoredPosition = new Vector2(0, 400);
    }
}
