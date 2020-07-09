using System;
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

    public void SetPlayer(Player newPlayer)
    {
        if (player != null)
        {
            player.onWalletChange -= PlayerWalletUpdate; 
        }
         
        
        player = newPlayer;
        playerAvatar.color = player.playerColor;
        player.onWalletChange += PlayerWalletUpdate;
        PlayerWalletUpdate();
    }

    void PlayerWalletUpdate()
    {
        cashText.text = "" + player.cash + "$";

        bossCard.SetActive(player.bossCard);
    }
    
}
