using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsBuyCell : Cell
{
    [SerializeField] int cost;
    [SerializeField] int maxNumber;
    [Tooltip("Drugs, Trinkets, Colonial Bank, Casino")][SerializeField] string goodsType;
    [Tooltip("Colony or Other")][SerializeField] string wayType;
    [SerializeField] int cashBonus; //сколько игрок получит, попадя на клетку
    public override void ActivateCell()
    {
        GameManager.Instance.WhoIsPlayer().WalletChange(cashBonus, 0, 0, 0, 0, 0, cashBonus, 0);
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
