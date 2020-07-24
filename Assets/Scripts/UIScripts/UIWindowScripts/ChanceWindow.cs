using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] int goldCost;



    public void NothingButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

    public void ChanceButton()
    {
        Player player = GameManager.Instance.WhoIsPlayer();
        PlayerMovement playerMvmnt = GameManager.Instance.WhoIsPlayerMVMNT();

        if (player.gold + goldCost >= 0)
        {
            player.WalletChange(0, goldCost, 0, 0, 0, 0, 0, 0);
            playerMvmnt.extraTurnNumber += 1;
            playerMvmnt.reversMovement = true;
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        }

    }

}
