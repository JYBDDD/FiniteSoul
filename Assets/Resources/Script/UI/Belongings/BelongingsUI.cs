using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ǰ UI Ŭ����
/// </summary>
public class BelongingsUI : MonoBehaviour
{
    // ����� 4.6.4 �� �����ؼ� �ۼ��Ұ� ������� TODO

    /// <summary>
    /// �Һ� ������ �̸�
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI consumptionText;

    /// <summary>
    /// ����ǰâ�� �����͸� �������ִ� ����Ʈ
    /// </summary>
    [SerializeField]
    public List<EquipSlot> belongingsList = new List<EquipSlot>();

    /// <summary>
    /// ������ ����ǰ ������ ����Ʈ
    /// </summary>
    private List<EquipSlot> originList = new List<EquipSlot>();


    private void Update()
    {
        // �����Ͱ� �����Ǿ��ٸ� ����
        if(originList != belongingsList)
        {
            BelongingsListInsert();
        }
    }

    /// <summary>
    /// ����ǰ������ ���â�����Ϳ� ����
    /// </summary>
    /// <param name="count"></param>
    private void BelongingsListInsert()
    {
        if (EquipSlotDataList.ChangeEquip == true)
        {
            for (int i = 0; i < EquipSlotDataList.equipSlotList.Count; ++i)
            {
                belongingsList[i].ItemDataSetting(EquipSlotDataList.equipSlotList[i].itemData);
            }

            ConsumptionDataInsert();
            originList = belongingsList;
            EquipSlotDataList.ChangeEquip = false;
        }

    }

    /// <summary>
    /// �Һ������ �ʱⵥ���� ����
    /// </summary>
    private void ConsumptionDataInsert()
    {
        var consumptionData = GameManager.Instance.FullData.itemsData.Where(_ => _.itemType == Define.ItemMixEnum.ItemType.Consumption).FirstOrDefault();

        // �Һ������ ������ ����
        belongingsList[2].ItemDataSetting(consumptionData);
        consumptionText.text = belongingsList[2].itemData.name;

        // ���� ���� ���� ����
        TextMeshProUGUI consumptionCount = belongingsList[2].GetComponentInChildren<TextMeshProUGUI>();
        consumptionCount.text = $"{belongingsList[2].itemData.currentHandCount}";


        // ������ �κ��丮�� ������ �������� ������ ���� ������ ����, �ƴ϶�� 0���� ����  TODO


        // ���ǰ��� �����Ǿ��ٸ� ���â�� ������ ���� �����Ų��
        new EquipSlotDataList(belongingsList[2]);
    }
}
