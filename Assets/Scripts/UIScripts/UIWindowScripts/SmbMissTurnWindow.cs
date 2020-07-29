using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SmbMissTurnWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] GameObject cap;
    [SerializeField] Image capColor;
    [SerializeField] Text noLucky;
    int missTurnW;
    Player luckyPlayerW;

    public void OpenWindow(Player luckyPlayer, int missTurn)
    {
        print("luckyPlayer = " + luckyPlayer);
        luckyPlayerW = luckyPlayer;
        missTurnW = missTurn;

        if (luckyPlayerW == null)
        {
            noLucky.gameObject.SetActive(true);
            cap.SetActive(false);
        }
        else
        {
            noLucky.gameObject.SetActive(false);
            cap.SetActive(true);
            capColor.color = luckyPlayerW.playerColor;
        }
        
        print("ok");
    }

    public void OkButton()
    {
        print("ok Button");
        if (luckyPlayerW != null)
        {
            print("enter ban area");
            luckyPlayerW.GetComponent<PlayerMovement>().turnMiss += missTurnW;
        }
        print("movement = " + GameManager.Instance.WhoIsPlayerMVMNT());
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
