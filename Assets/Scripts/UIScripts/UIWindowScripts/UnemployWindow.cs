using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnemployWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] TypeOfWindow typeOfWindow;
    int cashBonus;

    enum TypeOfWindow
    {
        UNEMPLOY,
        MOVIE
    }




    public void StayButton()
    {
        switch (typeOfWindow)
        {
            case TypeOfWindow.UNEMPLOY:
                cashBonus = GameManager.Instance.roundBaseMoney;
                break;

            case TypeOfWindow.MOVIE:
                cashBonus = GameManager.Instance.movieMoney;
                break;

        }


        GameManager.Instance.ChangePlayerWallet(cashBonus, 0, 0, 0, 0, 0, 0, 0);
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

    public void MoveButton()
    {
        GameManager.Instance.CurrentPlayerDiceTurn();
        UIManager.Instance.HideWindow(window);
    }
}
