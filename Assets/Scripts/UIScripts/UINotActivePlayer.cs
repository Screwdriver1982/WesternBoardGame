﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotActivePlayer : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Text cashText;
    [SerializeField] GameObject bossCard;
    [SerializeField] Image playerAvatar;
    [SerializeField] GameObject bankrupt;

    public void SetPlayer(Player newPlayer)
    {
        if (player != null)
        {
            player.onCashChanges -= PlayerWalletUpdate; 
        }
         
        
        player = newPlayer;
        playerAvatar.color = player.playerColor;
        player.onCashChanges += PlayerWalletUpdate;
        PlayerWalletUpdate();
    }

    void PlayerWalletUpdate()
    {

        if (GameManager.Instance.IsPlayerInGame(player))
        {

            cashText.text = "" + player.cash + "$";
            bossCard.SetActive(player.bossCard);
            bankrupt.SetActive(false);
        }
        else
        {
            bankrupt.SetActive(true);
        }
    }
    
}
