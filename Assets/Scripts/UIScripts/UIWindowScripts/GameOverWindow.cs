using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] Image winnerColor;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject drawPanel;
    public void OpenWindow(Player winner, bool winOrDraw)
    {
        winPanel.SetActive(winOrDraw);
        drawPanel.SetActive(!winOrDraw);
        winnerColor.color = winner.playerColor;
        
        Time.timeScale = 0f;
    }

    public void NewGameButton()
    { 
    
    }

    public void ExitGame()
    { 
    
    }
}
