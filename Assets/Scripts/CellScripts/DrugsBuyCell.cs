using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsBuyCell : Cell
{
    [SerializeField] int cost;
    [SerializeField] int maxNumber;
    [Tooltip("Drugs, Trinkets, Colonial Bank, Casino")][SerializeField] string goodsType;
    [Tooltip("Colony or Other")][SerializeField] string wayType;
    public override void ActivateCell()
    {
        UIManager.Instance.ShowBuyDrugsWindow(cellIcon,
                                    cellTitle,
                                    cellDescription,
                                    cellWayTitle,
                                    cost,
                                    maxNumber,
                                    goodsType,
                                    wayType);
    }
}
