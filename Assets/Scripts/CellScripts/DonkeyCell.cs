using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyCell : Cell
{
    Player activePlayer;
    List<Player> competitors;
    int cash;
    Player compet_1;
    Player compet_2;
    Player compet_3;

    public override void ActivateCell()
    {

        activePlayer = GameManager.Instance.WhoIsPlayer();
        print("palyer " + activePlayer);
        cash = - activePlayer.cash;
        print("cash " + cash);
        int playerNum = GameManager.Instance.activePlayerNum;
        print("playerNum " + playerNum);



        if (GameManager.Instance.IsPlayerInGameNum((playerNum+1)%4))
        {
            compet_1 = GameManager.Instance.GetPlayerByPlayerNum((playerNum + 1) % 4);
        }
        else
        {
            compet_1 = null;
        }

        if (GameManager.Instance.IsPlayerInGameNum((playerNum + 2) % 4))
        {
            compet_2 = GameManager.Instance.GetPlayerByPlayerNum((playerNum + 2) % 4);
        }
        else
        {
            compet_2 = null;
        }

        if (GameManager.Instance.IsPlayerInGameNum((playerNum + 3) % 4))
        {
            compet_3 = GameManager.Instance.GetPlayerByPlayerNum((playerNum + 3) % 4);
        }
        else
        {
            compet_3 = null;
        }

        print("Покажи окно");
        UIManager.Instance.ShowDonkeyWindow(cellIcon,
                                            cellTitle,
                                            cellDescription,
                                            cellWayTitle,
                                            compet_1,
                                            compet_2,
                                            compet_3,
                                            cash);

    }
}
