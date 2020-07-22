using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBrainsWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    [SerializeField] int cost; // цена за 1 штуку
    [SerializeField] int maxNumbers = 1000; // максимальное количество для покупки
    int currentNumber; //сколько есть на старте у игрока
    int afterDealNum; //сколько будет после сделки
    int dealGold; //сколько в итоге заработал с продажи


    [SerializeField] Text afterDealNumTxt; //показывает, сколько в итоге останется товара
    [SerializeField] Text dealGoldTxt; // показывает, сколько заработает или потратит в результате сделки
    [SerializeField] Slider slider; //слайдер
    Player player;


    public void OpenWindow(int brainCost, int brainMaxNumber)
    {
        maxNumbers = brainMaxNumber;
        cost = brainCost;

        player = GameManager.Instance.WhoIsPlayer();

        slider.minValue = 0;
        slider.maxValue = maxNumbers;
        slider.value = 0;
        afterDealNum = 0;
        afterDealNumTxt.text = "" + afterDealNum;
        dealGoldTxt.text = "0 кг";
        dealGoldTxt.color = Color.black;


    }

    public void OnSliderChanged()
    {
        afterDealNum = Mathf.FloorToInt(slider.value);
        dealGold = - afterDealNum * cost;
        ChangeDealGoldTxt();
        afterDealNumTxt.text = "" + afterDealNum;
    }

    private void ChangeDealGoldTxt()
    {
        if (dealGold == 0)
        {

            dealGoldTxt.text = dealGold + " кг";
            dealGoldTxt.color = Color.black;
        }
        else if (dealGold < 0)
        {

            dealGoldTxt.text = dealGold + " кг";
            dealGoldTxt.color = Color.red;
        }
    }

    public void BuyButton()
    {

        print("dealGold: " + dealGold);
        if (dealGold + player.gold >= 0)
        {
            GameManager.Instance.ChangePlayerWallet(0, dealGold, 0, 0, 0, 0, 0, 0);
            player.brains[2] += afterDealNum;
            dealGold = 0;
            ChangeDealGoldTxt();
            GameManager.Instance.NextPlayerTurn();
            UIManager.Instance.HideWindow(window);
        }
    }
}
