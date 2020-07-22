using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsExchangeWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    [SerializeField] int[] cost = { 0, 0, 0, 0 }; // цена за 1 штуку, последовательность: золото, нефть, авто, кола
    [SerializeField] int[] maxNumbers = { 0, 0, 0, 0 }; // максимальное количество для покупки, последовательность: золото, нефть, авто, кола
    int[] currentNumber = { 0, 0, 0, 0 }; //сколько есть на старте у игрока последовательность: золото, нефть, авто, кола
    int[] afterDealNum = { 0, 0, 0, 0 }; //сколько будет после сделки, последовательность: золото, нефть, авто, кола
    int[] dealCash = { 0, 0, 0, 0 }; //сколько в итоге заработал с продажи, последовательность: золото, нефть, авто, кола
    
    [SerializeField] Text[] afterDealNumTxt; //показывает, сколько в итоге останется товара
    [SerializeField] Text[] dealCashTxt; // показывает, сколько заработает или потратит в результате сделки
    [SerializeField] Slider[] slider; //слайдеры
    int playerLaborIndex; //индекс рабочей силы игрока
    Player player;

    public void OpenWindow(int maxGold, int maxOil, int maxCars, int maxCola)
    {
        maxNumbers[0] = maxGold;
        maxNumbers[1] = maxOil;
        maxNumbers[2] = maxCars;
        maxNumbers[3] = maxCola;
        
        
        player = GameManager.Instance.WhoIsPlayer();
        currentNumber[0] = player.gold;
        currentNumber[1] = player.oil;
        currentNumber[2] = player.cars;
        currentNumber[3] = player.cola;

        for (int i = 0; i < 4; i++)
        {
            cost[i] = GameManager.Instance.HowMuchDoesGoodCost(i);
            if (playerLaborIndex < 0)
            {
                slider[i].minValue = Mathf.FloorToInt(currentNumber[i] / 2);
            }
            else
            {
                slider[i].minValue = 0;
            }
            slider[i].maxValue = currentNumber[i] + maxNumbers[i];
            slider[i].value = currentNumber[i];
            afterDealNum[i] = currentNumber[i];
            afterDealNumTxt[i].text = "" + afterDealNum[i];
            dealCashTxt[i].text = "0$";
            dealCashTxt[i].color = Color.black;
        }

    }

    public void OnSliderChanged(int slNum)
    {
        afterDealNum[slNum] = Mathf.FloorToInt(slider[slNum].value);
        dealCash[slNum] = (currentNumber[slNum] - afterDealNum[slNum]) * cost[slNum];
        ChangeDealCashTxt(slNum);
        afterDealNumTxt[slNum].text = "" + afterDealNum[slNum];
    }

    private void ChangeDealCashTxt(int slNum)
    {
        if (dealCash[slNum] > 0)
        {

            dealCashTxt[slNum].text = "+" + dealCash[slNum] + "$";
            dealCashTxt[slNum].color = Color.green;

        }
        else if (dealCash[slNum] == 0)
        {

            dealCashTxt[slNum].text = dealCash[slNum] + "$";
            dealCashTxt[slNum].color = Color.black;
        }
        else if (dealCash[slNum] < 0)
        {

            dealCashTxt[slNum].text = dealCash[slNum] + "$";
            dealCashTxt[slNum].color = Color.red;
        }
    }

    public void BuyButtonGold()
    {
        if (dealCash[0] + player.cash >= 0)
        { 
            GameManager.Instance.ChangePlayerWallet(dealCash[0], afterDealNum[0]- currentNumber[0], 0, 0, 0, 0, 0, 0);
            dealCash[0] = 0;
            currentNumber[0] = afterDealNum[0];
            ChangeDealCashTxt(0);


        }
    }

    public void BuyButtonOil()
    {
        if (dealCash[1] + player.cash >= 0)
        {
            GameManager.Instance.ChangePlayerWallet(dealCash[1], 0, afterDealNum[1] - currentNumber[1], 0, 0, 0, 0, 0);
            dealCash[1] = 0;
            currentNumber[1] = afterDealNum[1];
            ChangeDealCashTxt(1);
        }
    }

    public void BuyButtonCars()
    {
        if (dealCash[2] + player.cash >= 0)
        {
            GameManager.Instance.ChangePlayerWallet(dealCash[2], 0, 0, afterDealNum[2] - currentNumber[2], 0, 0, 0, 0);
            dealCash[2] = 0;
            currentNumber[2] = afterDealNum[2];
            ChangeDealCashTxt(2);
        }
    }

    public void BuyButtonCola()
    {
        if (dealCash[3] + player.cash >= 0)
        {
            GameManager.Instance.ChangePlayerWallet(dealCash[3], 0, 0, 0, afterDealNum[3] - currentNumber[3], 0, 0, 0);
            dealCash[3] = 0;
            currentNumber[3] = afterDealNum[3];
            ChangeDealCashTxt(3);
        }
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
