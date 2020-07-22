using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBrainsCell : Cell
{
    [SerializeField] int cost;
    [SerializeField] int maxNumber = 1000;

    public override void ActivateCell()
    {
        UIManager.Instance.ShowBuyBrainsWindow(cellIcon,
                                    cellTitle,
                                    cellDescription,
                                    cellWayTitle,
                                    cost,
                                    maxNumber);
    }
}
