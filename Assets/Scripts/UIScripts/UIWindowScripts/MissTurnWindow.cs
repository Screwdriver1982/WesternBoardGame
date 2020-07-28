using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissTurnWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    
    [SerializeField] Text costText;
    [SerializeField] GameObject goldImage;
    [SerializeField] GameObject payButton;
    
    
    
    [SerializeField] Text missText;
    [SerializeField] GameObject missButton;

    [SerializeField] Text goodButtonText;
    [SerializeField] GameObject goodButton;
    
    
    PlayerMovement activePlayerMvmnt;
    Player activePlayer;


    int goldCost;
    int cashCost;
    int missTurn;
    int police = 0;
    int army = 0;
    Cell prison;
    Cell rescue;



    public void OpenWindow(int cost, int miss, string missType, Cell prisonCell, Cell rescueCell)
    {
        activePlayerMvmnt = GameManager.Instance.WhoIsPlayerMVMNT();
        activePlayer = GameManager.Instance.WhoIsPlayer();
        rescue = rescueCell;

        if (missType == "gold") //т.е. откупаться золотом нужно
        {

            goldCost = cost;
            cashCost = 0;
            missTurn = miss;
            police = 0;
            army = 0;
            prison = prisonCell;

            costText.text = "" + cost;
            costText.color = Color.red;
            goldImage.SetActive(true);
            missButton.SetActive(true);
            missText.gameObject.SetActive(true);
            missText.text = "Ходов: " + miss;
            goodButton.SetActive(false);
        }
        else if (missType == "save") //полиция спасла
        {

            goldCost = 0;
            cashCost = 0;
            missTurn = 0;
            police = -1;
            army = 0;
            costText.gameObject.SetActive(false);
            goldImage.SetActive(false);
            payButton.SetActive(false);
            missText.gameObject.SetActive(false);
            missButton.SetActive(false);
            goodButton.SetActive(true);
            goodButtonText.text = "Копы свои!";

        }
        else if (missType == "army save") //армия  спасла
        {

            goldCost = 0;
            cashCost = 0;
            missTurn = 0;
            police = 0;
            army = -1;
            costText.gameObject.SetActive(false);
            goldImage.SetActive(false);
            payButton.SetActive(false);
            missText.gameObject.SetActive(false);
            missButton.SetActive(false);
            goodButton.SetActive(true);
            goodButtonText.text = "Армия рулит!";

        }
        else if (missType == "boss")//игрок босс
        {
            goldCost = 0;
            cashCost = 0;
            missTurn = 0;
            police = 0;
            army = 0;
            costText.gameObject.SetActive(false);
            goldImage.SetActive(false);
            payButton.SetActive(false);
            missText.gameObject.SetActive(false);
            missButton.SetActive(false);
            goodButton.SetActive(true);
            goodButtonText.text = "Вы Крестный Отец!";

        }
        else if (missType == "miss")
        {

            goldCost = 0;
            cashCost = 0;
            missTurn = miss;
            police = 0;
            army = 0;
            prison = prisonCell;



            costText.gameObject.SetActive(false);
            goldImage.SetActive(false);
            payButton.SetActive(false);
            missText.gameObject.SetActive(true);
            missButton.SetActive(true);
            goodButton.SetActive(false);

            missText.text = "Ходов: " + miss;

        }
        else if (missType == "miss or robbery")
        {

            goldCost = 0;
            cashCost = cost;
            missTurn = miss;
            police = 0;
            army = 0;
            prison = prisonCell;


            costText.gameObject.SetActive(true);
            goldImage.SetActive(false);
            payButton.SetActive(true);
            missText.gameObject.SetActive(true);
            missButton.SetActive(true);
            goodButton.SetActive(false);

            missText.text = "Ходов: " + miss;
            costText.text = "" + cost + "$";
        }
        else if (missType == "toBoss")
        {

            goldCost = 0;
            cashCost = cost;
            missTurn = miss;
            police = 0;
            army = 0;
            prison = prisonCell;


            costText.gameObject.SetActive(true);
            goldImage.SetActive(false);
            payButton.SetActive(true);
            missText.gameObject.SetActive(true);
            missButton.SetActive(true);
            goodButton.SetActive(false);

            missText.text = "Ходов: " + miss;
            costText.text = "" + cost + "$";
        }
    }

    public void MissButton()
    {
        
       
        activePlayerMvmnt.turnMiss = missTurn;
        if (missTurn != 0 && prison !=null)
        {
            activePlayerMvmnt.JumpToCellAndActivateIt(prison);
        }
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

    public void PayButton()
    {

        if ((activePlayer.cash + cashCost >= 0) && (activePlayer.gold + goldCost >= 0))
        {
            GameManager.Instance.ChangePlayerWallet(cashCost, goldCost, 0, 0, 0, 0, cashCost,0);

            if (rescue != null)
            { 
                activePlayerMvmnt.JumpToCellAndActivateIt(rescue);
            }
            
            UIManager.Instance.HideWindow(window);

        }
    }

    public void GoodButton()
    {

        GameManager.Instance.ChangePlayerCards(0,police,army,0,0,0,0,0,0);
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);

    }


}
