using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UseItemData : ItemData
{
    public UseItemData() { }

    public UseItemData(UseItemData useItem)
    {
        index = useItem.index;
        name = useItem.name;
        jobIndex = useItem.jobIndex;
        itemType = useItem.itemType;
        handed = useItem.handed;
        currentHandCount = useItem.currentHandCount;
        dropPer = useItem.dropPer;
        salePrice = useItem.salePrice;
        description = useItem.description;
        resourcePath = useItem.resourcePath;
    }
}
