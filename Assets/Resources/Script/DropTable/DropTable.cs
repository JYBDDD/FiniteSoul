using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� óġ�� ����� �������� ������ Ŭ����
/// </summary>
public class DropTable
{
    /// <summary>
    /// ������ ����Ȯ���� ��꿡 ����� ť
    /// </summary>
    static Queue<UseItemData> queItem = new Queue<UseItemData>();

    /// <summary>
    /// ����� ������ (�� ���Կ�)
    /// </summary>
    static UseItemData dropItem = new UseItemData();

    /// <summary>
    /// ���� ����� ���Ȯ�� ���
    /// </summary>
    public static void DropPerCalculation(UseMonsterData monsterData)
    {
        // ���� ���Ȯ���� ���Խ� ����
        if (Random.Range(0, 100) < monsterData.dropItemPer)
        {
            dropItem = ItemDropPerCalculation();
            DropQueueReset();

            // �κ��丮�� �ش� �� ����
            ShopInvenWindowUI.SearchAddtionData(dropItem);

            // ������ ȹ�� ������ ���
            ItemAcquisitionWindow.TempInstance.AcquisitionWindowPrint(dropItem);
        }

        // ���� ���Ȯ���� ������� �ݵ�� ���
        DropMonsterRune(monsterData);
    }

    /// <summary>
    /// ���� ���Ȯ���� ���������� ����� ������ Ȯ���� ����ϴ� �޼���
    /// </summary>
    private static UseItemData ItemDropPerCalculation()
    {
        float r = Random.Range(0f, 1f);
        float dropRand = 0f;  // 0 ~ 100 ���Ȯ��
        float dropCal = 0;    // �� ������ ��� �ۼ�Ʈ (��� �������� ���Ȯ���� ���Ѱ�)
        float startCumulative = 0;  // ����Ȯ�� ��꿡 ����� ��

        // ť�� ������ ����
        var itemData = GameManager.Instance.FullData.itemsData;
        for (int i = 0; i < itemData.Count; ++i)
        {
            dropCal += itemData[i].dropPer;
            queItem.Enqueue(itemData[i]);    // ���������� ���Ȯ�� ����
        }

        dropRand = r * dropCal;

        // �����Ͱ� �����Ѵٸ� �����͸� �������� �ʰ� ��� ����
        if (queItem.Count > 0)
        {
            while(true)
            {
                startCumulative += queItem.Peek().dropPer;

                // ���Ȯ�� ������ ���Դٸ� ����
                if(dropRand <= startCumulative)
                {
                    return queItem.Peek();
                }
                // ������ ������ �ʾҴٸ� �ش���� �ʴ� ������ ����
                else
                {
                    queItem.Dequeue();
                }
            }
        }

        return null;

    }

    /// <summary>
    /// ���� ����� �ݵ�� ����� ��
    /// </summary>
    private static void DropMonsterRune(UseMonsterData monsterData)
    {
        InGameManager.Instance.Player.playerData.currentRune += monsterData.dropRune;
    }

    /// <summary>
    /// ��� ť ������ ����
    /// </summary>
    private static void DropQueueReset()
    {
        queItem.Clear();
    }






}
