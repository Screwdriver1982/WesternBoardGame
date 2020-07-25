using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuctionWindow : MonoBehaviour
{
    [Header("Логика")]
    [SerializeField] CanvasGroup window;
    [SerializeField] int currentBestPrice;
    [SerializeField] int currentWinner;
    [SerializeField] List<Player> playersMerchant;
    [SerializeField] int[] playerBet;
    [SerializeField] int activeMerchant;
    [SerializeField] Shares soldShare; //продаваемая акция


    [Header("Оформление")]
    [SerializeField] Image shareImage;
    [SerializeField] Text shareTitle;
    [SerializeField] Text shareDescr;
    [Header("Ввод ставки")]
    [SerializeField] GameObject betPanel; //панель, если включена, то отображается поле ввода
    [SerializeField] Image plColor;
    [SerializeField] Slider betSlider;
    [SerializeField] Text currentBetTxt;
    [SerializeField] GameObject noMoneyTxt;

    [Header("Ставки игроков")]
    [SerializeField] GameObject winnerPanel;//панель со всеми игроками, если включена, то отображаются ставки игроков и победитель
    [SerializeField] GameObject[] participants; //строчка для каждого участника
    [SerializeField] Text[] betsTxt;
    [SerializeField] GameObject[] winnerLabels; 
    [SerializeField] Image[] playerColors;


    public void OpenWindow(Shares share, int secondPlayerNum, int thirdPlayerNum, int fourthPlayerNum)
    {
        betPanel.SetActive(true);
        //занулили все
        currentBestPrice = 0;
        currentWinner = -1;
        activeMerchant = 0;
        betSlider.minValue = 1;
        betSlider.value = 1;

        
        for (int j = 0; j < winnerLabels.Length; j++)
        {
            winnerLabels[j].SetActive(false);
            playerBet[0] = 0;
        }


        while (playersMerchant.Count > 0)
        {
            playersMerchant.RemoveAt(0);
            

        }

        for (int i = 0; i < winnerLabels.Length; i++)
        {
            winnerLabels[i].SetActive(false);
            participants[i].SetActive(false);
        }

        //добавили текущего игрока в лист плееров
        playersMerchant.Add(GameManager.Instance.WhoIsPlayer());
        playerColors[0].color = playersMerchant[0].playerColor;
        participants[0].SetActive(true);
        
        //присвоили фишке цвет игрока из нулевой ячейки
        plColor.color = playerColors[0].color;

        if (playersMerchant[0].cash >= 1)
        {
            betSlider.gameObject.SetActive(true);
            noMoneyTxt.SetActive(false);
            betSlider.maxValue = playersMerchant[0].cash;
        }
        else
        {
            betSlider.gameObject.SetActive(false);
            noMoneyTxt.SetActive(true);
        }

        currentBetTxt.text = betSlider.value + "$";


        //добавили других игроков в лист и их цвета
        if (secondPlayerNum != -1)
        {
            Player secondPlayer = GameManager.Instance.GetPlayerByPlayerNum(secondPlayerNum);
            playersMerchant.Add(secondPlayer);
            playerColors[playersMerchant.Count-1].color = secondPlayer.playerColor;
            participants[playersMerchant.Count-1].SetActive(true);
        }


        if (thirdPlayerNum != -1)
        {
            Player thirdPlayer = GameManager.Instance.GetPlayerByPlayerNum(thirdPlayerNum);
            playersMerchant.Add(thirdPlayer);
            playerColors[playersMerchant.Count-1].color = thirdPlayer.playerColor;
            participants[playersMerchant.Count-1].SetActive(true);
        }

        if (fourthPlayerNum != -1)
        {
            Player fourthPlayer = GameManager.Instance.GetPlayerByPlayerNum(fourthPlayerNum);
            playersMerchant.Add(fourthPlayer);
            playerColors[playersMerchant.Count-1].color = fourthPlayer.playerColor;
            participants[playersMerchant.Count-1].SetActive(true);
        }

        winnerPanel.SetActive(false);

        //оформляем акцию
        soldShare = share;
        shareTitle.text = soldShare.shareTitle;
        shareDescr.text = soldShare.shareDescription;
        shareImage.sprite = soldShare.icon;
    }

    public void MakeBet()
    {
        int bet = Mathf.RoundToInt(betSlider.value);
        if (bet > 0 && bet <= playersMerchant[activeMerchant].cash)
        {
            playerBet[activeMerchant] = bet;
            participants[activeMerchant].SetActive(true);
            betsTxt[activeMerchant].text = playerBet[activeMerchant] + "$";

            if (bet > currentBestPrice)
            {
                currentBestPrice = bet;
                if (currentWinner != -1)
                { 
                    winnerLabels[currentWinner].SetActive(false);
                }
                currentWinner = activeMerchant;
                winnerLabels[currentWinner].SetActive(true);
            }

            NextPlayerBet();
        }
    }
    public void NothingButton()

    {
        participants[activeMerchant].SetActive(false);
        NextPlayerBet();
    }

    public void OkButton()
    {
        if (currentWinner != -1)
        {
            playersMerchant[currentWinner].WalletChange(-playerBet[currentWinner], 0, 0, 0, 0, 0, 0, 0);
            GameManager.Instance.GiveShareToPlayer(playersMerchant[currentWinner], soldShare);
        }

        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);

    }


    void NextPlayerBet()
    {
        if (activeMerchant != playersMerchant.Count - 1)
        {
            activeMerchant += 1;
            plColor.color = playerColors[activeMerchant].color;

            if (playersMerchant[activeMerchant].cash >= 1)
            {
                betSlider.gameObject.SetActive(true);
                noMoneyTxt.SetActive(false);
                betSlider.value = 1;
                betSlider.maxValue = playersMerchant[activeMerchant].cash;
                currentBetTxt.text = betSlider.value + "$";
            }
            else
            {
                betSlider.gameObject.SetActive(false);
                noMoneyTxt.SetActive(true);
            }
            
            

        }
        else
        {
            betPanel.SetActive(false);
            winnerPanel.SetActive(true);
        }
    }

    public void OnSliderChanged()
    {
        currentBetTxt.text = betSlider.value + "$";
    }
}
