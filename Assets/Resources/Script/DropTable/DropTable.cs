using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 처치시 드랍할 아이템을 결정할 클래스
/// </summary>
public class DropTable
{
    /// <summary>
    /// 아이템 누적확률값 계산에 사용할 큐
    /// </summary>
    static Queue<UseItemData> queItem = new Queue<UseItemData>();

    /// <summary>
    /// 드랍된 아이템 (값 삽입용)
    /// </summary>
    static UseItemData dropItem = new UseItemData();

    /// <summary>
    /// 몬스터 사망시 드랍확률 계산
    /// </summary>
    public static void DropPerCalculation(UseMonsterData monsterData)
    {
        // 몬스터 드랍확률에 진입시 실행
        if (Random.Range(0, 100) < monsterData.dropItemPer)
        {
            dropItem = ItemDropPerCalculation();
            DropQueueReset();

            // 인벤토리에 해당 값 삽입
            ShopInvenWindowUI.SearchAddtionData(dropItem);

            // 아이템 획득 윈도우 출력
            ItemAcquisitionWindow.TempInstance.AcquisitionWindowPrint(dropItem);
        }

        // 룬은 드랍확률을 벗어나더라도 반드시 드랍
        DropMonsterRune(monsterData);
    }

    /// <summary>
    /// 몬스터 드랍확률에 진입했을시 드랍할 아이템 확률을 계산하는 메서드
    /// </summary>
    private static UseItemData ItemDropPerCalculation()
    {
        float r = Random.Range(0f, 1f);
        float dropRand = 0f;  // 0 ~ 100 드랍확률
        float dropCal = 0;    // 총 아이템 드랍 퍼센트 (모든 아이템의 드랍확률을 더한값)
        float startCumulative = 0;  // 누적확률 계산에 사용할 값

        // 큐에 데이터 삽입
        var itemData = GameManager.Instance.FullData.itemsData;
        for (int i = 0; i < itemData.Count; ++i)
        {
            dropCal += itemData[i].dropPer;
            queItem.Enqueue(itemData[i]);    // 순차적으로 드랍확률 삽입
        }

        dropRand = r * dropCal;

        // 데이터가 존재한다면 데이터를 삽입하지 않고 즉시 실행
        if (queItem.Count > 0)
        {
            while(true)
            {
                startCumulative += queItem.Peek().dropPer;

                // 드랍확률 범위에 들어왔다면 실행
                if(dropRand <= startCumulative)
                {
                    return queItem.Peek();
                }
                // 범위에 들어오지 않았다면 해당되지 않는 데이터 삭제
                else
                {
                    queItem.Dequeue();
                }
            }
        }

        return null;

    }

    /// <summary>
    /// 몬스터 사망시 반드시 드랍할 룬
    /// </summary>
    private static void DropMonsterRune(UseMonsterData monsterData)
    {
        InGameManager.Instance.Player.playerData.currentRune += monsterData.dropRune;
    }

    /// <summary>
    /// 모든 큐 데이터 리셋
    /// </summary>
    private static void DropQueueReset()
    {
        queItem.Clear();
    }






}
