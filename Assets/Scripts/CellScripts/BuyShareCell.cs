using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShareCell : Cell
{
    [SerializeField] Shares share;
    


    public override void ActivateCell()
    {
        int cost;
        Player owner = null;

        if (share.typeOfShares != "Corporation")
        {
            cost = share.cost;
        }
        else
        {
            cost = GameManager.Instance.GiveCorporationPrice(share);
        }

        owner = GameManager.Instance.GetShareOwner(share);

        UIManager.Instance.ShowBuyShareWindow(cellIcon,
                                              cellTitle,
                                              cellDescription,
                                              cellWayTitle, 
                                              owner, 
                                              share, 
                                              cost);
    }
}
