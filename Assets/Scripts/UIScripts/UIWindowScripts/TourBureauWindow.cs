using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourBureauWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Cell portCell;
    [SerializeField] Shares portShare;
    [SerializeField] int travelCost;



    public void TravelButton()
    {
        Player player = GameManager.Instance.WhoIsPlayer();
        player.WalletChange(travelCost, 0, 0, 0, 0, 0, 0, 0);
        Player portOwner = GameManager.Instance.GetShareOwner(portShare);
        if (portOwner != null)
        {
            portOwner.WalletChange(-travelCost, 0, 0, 0, 0, 0, 0, 0);
        }

        PlayerMovement plMvmnt = GameManager.Instance.WhoIsPlayerMVMNT();
        plMvmnt.JumpToCellAndActivateIt(portCell);
        UIManager.Instance.HideWindow(window);

    }

    public void CommonWayButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);

    }
}
