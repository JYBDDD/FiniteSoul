using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 슬롯 생성기 클래스
/// </summary>
public class SlotGenerator : MonoBehaviour
{

    private void Start()
    {
        // 인벤토리 슬롯 42개를 만들어 리스트에 삽입
        for(int i = 0; i < 42; ++i)
        {
            // 인벤토리 슬롯 생성
            GameObject invenObj = ResourceUtil.InsertPrefabs(Define.SlotUIPath.invenSlotPath);
            // 인벤토리 슬롯 위치 설정
            invenObj.transform.SetParent(transform, false);
            // 인벤토리 슬롯 데이터 및 이미지 셋팅
            invenObj.GetComponent<InvenSlot>().ImageDataSetting();
        }
    }
}
