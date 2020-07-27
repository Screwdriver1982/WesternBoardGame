﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShareCell : Cell
{
    [SerializeField] Shares share;
    [SerializeField] Shares resourceShare;
    


    public override void ActivateCell()
    {
        int cost;
        Player owner = null;
        Player incomeBeneficiar = null;

        if (resourceShare != null)
        {
            incomeBeneficiar = GameManager.Instance.GetShareOwner(resourceShare);
        }


        if (share.typeOfShares != "Corporation")
        {
            cost = share.cost;
        }
        else
        {
            cost = GameManager.Instance.GiveCorporationPrice(share);
        }

        owner = GameManager.Instance.GetShareOwner(share);

        if (incomeBeneficiar != null)
        {
            int cashincome=GameManager.Instance.GetShareIncomeCash(resourceShare);
            print("cash = " + cashincome);
            incomeBeneficiar.WalletChange(cashincome, 0, 0, 0, 0, 0, 0, 0);
        }

        UIManager.Instance.ShowBuyShareWindow(cellIcon,
                                              cellTitle,
                                              cellDescription,
                                              cellWayTitle, 
                                              owner, 
                                              share, 
                                              cost);
    }
}
