using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionCell : Cell
{
    int[] playersNum = { -1, -1, -1 };
    
    public override void ActivateCell()
    {
        print("enter ");

        Shares notSoldShare = null;
        print("create share" + notSoldShare);
        notSoldShare = GameManager.Instance.GetRandomUnsoldShare();
        print("notSoldShare " + notSoldShare);
        
        int activePlayerNum = GameManager.Instance.activePlayerNum;
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.IsPlayerInGameNum((activePlayerNum + 1 + i) % 4))
            {
                playersNum[i] = (activePlayerNum + 1 + i) % 4;
            }
            else
            {
                playersNum[i] = -1;
            }
        }

        if (notSoldShare != null)
        { 
            UIManager.Instance.ShowAuctionWindow(notSoldShare, playersNum[0], playersNum[1], playersNum[2]);
        }
    }
}
