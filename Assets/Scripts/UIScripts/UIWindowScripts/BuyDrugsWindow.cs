using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDrugsWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    [SerializeField] int cost; // цена за 1 штуку, для колониального кредита должна быть отрицательной
    [SerializeField] int maxNumbers = 10; // максимальное количество для покупки
    int currentNumber; //сколько есть на старте у игрока
    int afterDealNum; //сколько будет после сделки
    int dealCash; //сколько в итоге заработал с продажи
    int robbery;
    int colony;
    int getLoan = 0;
    int bet = 0;

    [SerializeField] Text afterDealNumTxt; //показывает, сколько в итоге останется товара
    [SerializeField] Text dealCashTxt; // показывает, сколько заработает или потратит в результате сделки
    [SerializeField] Slider slider; //слайдеры
    Player player;

    string goodsTypeW;
    string wayTypeW;


    public void OpenWindow(int drugsCost, int drugsMaxNumber, string goodsType, string wayType)
    {
        dealCash = 0;
        maxNumbers = drugsMaxNumber;
        cost = drugsCost;
        goodsTypeW = goodsType; //Drugs or Trinkets
        wayTypeW = wayType; //Colony or Other
        colony = 0;
        robbery = 0;
        bet = 0;
        

        player = GameManager.Instance.WhoIsPlayer();

        if (goodsTypeW == "Drugs")
        {
            currentNumber = player.drugs;

        }
        else if (goodsTypeW == "Trinkets")
        {
            currentNumber = player.trinkets;
            if (wayTypeW == "Other")
            {
                slider.minValue = currentNumber;
            }
            else
            {
                slider.minValue = 0;
            }
        }
        else if (goodsTypeW == "Colonial Bank")
        {
            currentNumber = player.colonyLoan;
            slider.minValue = player.colonyLoan;
            slider.maxValue = currentNumber + maxNumbers;


        }
        else if (goodsTypeW == "Casino")
        {
            currentNumber = 0;
            slider.minValue = 0;
            slider.maxValue = maxNumbers;
        }


        slider.value = currentNumber;
        afterDealNum = currentNumber;
        afterDealNumTxt.text = "" + afterDealNum;
        dealCashTxt.text = "0$";
        dealCashTxt.color = Color.black;
    

    }

    public void OnSliderChanged()
    {
        afterDealNum = Mathf.FloorToInt(slider.value);
        dealCash = (currentNumber - afterDealNum) * cost;
        ChangeDealCashTxt();
        afterDealNumTxt.text = "" + afterDealNum;
    }

    private void ChangeDealCashTxt()
    {
        if (dealCash > 0)
        {

            dealCashTxt.text = "+" + dealCash + "$";
            dealCashTxt.color = Color.green;

            if (goodsTypeW == "Drugs")
            {
                robbery = dealCash;
            }
            else if (goodsTypeW == "Trinkets")
            {
                colony = dealCash;
            }
            else if (goodsTypeW == "Colonial Bank")
            {
                getLoan = dealCash;
            }

        }
        else if (dealCash == 0)
        {

            dealCashTxt.text = dealCash + "$";
            dealCashTxt.color = Color.black;
        }
        else if (dealCash < 0)
        {

            dealCashTxt.text = dealCash + "$";
            dealCashTxt.color = Color.red;
            if (goodsTypeW == "Casino")
            {
                bet = -Mathf.FloorToInt(dealCash * GameManager.Instance.casinoRewardCoef);
            }

        }
    }

    public void BuyButton()
    {
        if (dealCash + player.cash >= 0)
        {
            GameManager.Instance.ChangePlayerWallet(dealCash, 0, 0, 0, afterDealNum - currentNumber, 0, robbery, colony);
            if (getLoan > 0)
            {
                player.colonyLoan += getLoan;
            }

            if (bet <= 0)
            {
                GameManager.Instance.NextPlayerTurn();
                UIManager.Instance.HideWindow(window);

            }
            else
            {
                player.casinoPossibleReward = bet;
                GameManager.Instance.ThrowDice();
                UIManager.Instance.HideWindow(window);
            }

        }
    }




}
