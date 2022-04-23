using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UseItemData : ItemData
{
    /// <summary>
    /// 현재 소지 개수
    /// </summary>
    public int currentHandCount;





    public UseItemData() { }

    public UseItemData(UseItemData useItem)
    {
        index = useItem.index;
        name = useItem.name;
        jobIndex = useItem.jobIndex;
        itemType = useItem.itemType;
        handed = useItem.handed;
        maxHandCount = useItem.maxHandCount;
        dropPer = useItem.dropPer;
        salePrice = useItem.salePrice;
        description = useItem.description;
        resourcePath = useItem.resourcePath;
    }
}
