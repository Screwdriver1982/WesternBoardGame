using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActivePlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image playerAvatar;
    [SerializeField] Text cashText;
    [SerializeField] Text robberyText;
    [SerializeField] Text colonyText;
    [SerializeField] GameObject gold;
    [SerializeField] Text goldText;

    [SerializeField] GameObject oil;
    [SerializeField] Text oilText;

    [SerializeField] GameObject cars;
    [SerializeField] Text carsText;

    [SerializeField] GameObject cola;
    [SerializeField] Text colaText;

    [SerializeField] GameObject drugs;
    [SerializeField] Text drugsText;

    [SerializeField] GameObject bossCard;
    [SerializeField] GameObject policeCard;
    [SerializeField] GameObject armyCard;
    [SerializeField] GameObject woolfyCard;
    [SerializeField] GameObject rabbyCard;


    public void SetPlayer(Player newPlayer)
    {
        if (player != null)
        {
            player.onWalletChanges -= PlayerWalletUpdate;
            player.onCardsChanges -= PlayerCardsUpdate;
        }


        player = newPlayer;
        playerAvatar.color = player.playerColor;
        player.onWalletChanges += PlayerWalletUpdate;
        player.onCardsChanges += PlayerCardsUpdate;
        PlayerWalletUpdate();
        PlayerCardsUpdate();
    }

    void PlayerWalletUpdate()
    {
        int cash = player.cash;
        cashText.text = "" + cash + "$";
        if (player.cash < 0)
        {
            cashText.color = Color.red;
        }
        else
        {
            cashText.color = Color.black;
        }

        //gold.SetActive(player.gold > 0);
        goldText.text = "" + player.gold;

        //cars.SetActive(player.cars > 0);
        carsText.text = "" + player.cars;

        //oil.SetActive(player.oil > 0);
        oilText.text = "" + player.oil;

        //cola.SetActive(player.cola > 0);
        colaText.text = "" + player.cola;

        //drugs.SetActive(player.drugs > 0);
        drugsText.text = "" + player.drugs;

        robberyText.gameObject.SetActive(player.robberiedMoney > 0);
        robberyText.text = "Награблено: " + player.robberiedMoney;

        colonyText.gameObject.SetActive(player.colonyMoney > 0);
        colonyText.text = "Колонии & Клондайк: " + player.colonyMoney;
    }
     
    void PlayerCardsUpdate()
    {   bossCard.SetActive(player.bossCard);
        policeCard.SetActive(player.policeCard);
        armyCard.SetActive(player.armyCard);
        woolfyCard.SetActive(player.woolfyCard);
        rabbyCard.SetActive(player.rabbyCard);

    }
}
