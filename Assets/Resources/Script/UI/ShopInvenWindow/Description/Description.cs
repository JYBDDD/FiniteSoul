using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���� â�� ����ϴ� Ŭ����
/// </summary>
public class Description : MonoBehaviour
{
    [SerializeField,Tooltip("������ ����")]
    TextMeshProUGUI description;

    [SerializeField,Tooltip("������ �̹���")]
    Image itemImg;

    [SerializeField,Tooltip("������ ���� ���� / �κ��丮 ������ RayCast�������� ����")]
    TextMeshProUGUI itemBelongingsCount;

    [SerializeField,Tooltip("������ �� ��� �̹���")]
    Image backImg;
    [SerializeField, Tooltip("������ ���� Ÿ��Ʋ")]
    TextMeshProUGUI backTitle;

    /// <summary>
    /// �ӽ÷� ������ �����͸� ���������� ����
    /// </summary>
    public static UseItemData temporaryItemData = new UseItemData();



    // �ٸ� ������ ���� ������ �κ����Կ� RayCast �������� �����ش�   TODO     ////////////////////////////////////////////

    /// <summary>
    /// ����ڽ� ������ ����
    /// </summary>
    public void DescriptionDataSetting(UseItemData itemData)
    {
        description.text = $"{itemData.description}";
        itemImg.sprite = Resources.Load<Sprite>(itemData.resourcePath);

        // �κ��丮���� ����ִ� �ش� �������� ����
        itemBelongingsCount.text = $"{InventoryItemFind()}";
    }

    /// <summary>
    /// ������ ������������ �����Ѵٸ� ����
    /// </summary>
    public void DescriptionAlphaOne()
    {
        AlphaSet(description.color, 1);
        AlphaSet(itemImg.color, 1);
        AlphaSet(itemBelongingsCount.color, 1);
        AlphaSet(backImg.color, 1);
        AlphaSet(backTitle.color, 1);
    }

    /// <summary>
    /// ������ ������������ �������� �ʴ´ٸ� ����
    /// </summary>
    public void DescriptionAlphaZero()
    {
        StartCoroutine(AlhpaZero());

        IEnumerator AlhpaZero()
        {
            float duraction = 2f;
            float time = 0;

            while(time < duraction)
            {
                time += Time.deltaTime;

                description.color = Color.Lerp(description.color, AlphaSet(description.color,0), time / 2);
                itemImg.color = Color.Lerp(itemImg.color, AlphaSet(itemImg.color, 0), time / 2);
                itemBelongingsCount.color = Color.Lerp(itemBelongingsCount.color, AlphaSet(itemBelongingsCount.color, 0), time / 2);
                backImg.color = Color.Lerp(backImg.color, AlphaSet(backImg.color, 0), time / 2);
                backTitle.color = Color.Lerp(backTitle.color, AlphaSet(backTitle.color, 0), time / 2);

                yield return null;
            }
        }
    }

    /// <summary>
    /// ���İ��� �������ִ� �޼ҵ�
    /// </summary>
    /// <param name="setColor"></param>
    /// <param name="alpha"></param>
    private Color AlphaSet(Color setColor,float alpha)
    {
        return new Color(setColor.r, setColor.g, setColor.b, alpha);
    }

    /// <summary>
    /// �κ��丮�ȿ� �ִ� �ش� �������� ���� ������ ������ �޼ҵ�
    /// </summary>
    /// <returns></returns>
    private int InventoryItemFind()
    {
        // �κ��丮 ����Ʈ
        var inven = ShopInvenWindowUI.Inventory;

        // �ش� �������� ����
        int thisCount = 0;

        for(int i =0; i < inven.Count; ++i)
        {
            // �κ��丮�� �ִ� ������ �ε����� ���콺 ��ġ�� �ִ� ������ �ε����� ���ٸ� ���� ���Ѵ�
            if(inven[i].itemData.index == temporaryItemData.index)
            {
                thisCount += inven[i].itemData.currentHandCount;
            }
        }

        return thisCount;
    }
}
