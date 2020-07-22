using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCell : Cell
{
    [SerializeField]int starCost = -5000;
    public override void ActivateCell()
    {
        Player activePlayer = GameManager.Instance.WhoIsPlayer();
        int starNum = 0;
        if (activePlayer.woolfyCard)
        {
            starNum = 1;
        }
        else if (activePlayer.rabbyCard)
        {
            starNum = 2;
        }

        print("starNum: " + starNum);


        UIManager.Instance.ShowStarWindow(cellIcon,
                                             cellTitle,
                                             cellDescription,
                                             cellWayTitle,
                                             starCost,
                                             starNum);
    }
}
