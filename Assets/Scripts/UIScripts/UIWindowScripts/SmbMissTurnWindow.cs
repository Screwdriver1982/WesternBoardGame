using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SmbMissTurnWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] GameObject cap;
    [SerializeField] Image capColor;
    [SerializeField] Text noLucky;
    int missTurnW;
    Player luckyPlayerW;

    public void OpenWindow(Player luckyPlayer, int missTurn)
    {
        luckyPlayerW = luckyPlayer;
        noLucky.gameObject.SetActive(luckyPlayerW == null);
        cap.SetActive(luckyPlayerW != null);
        capColor.color = luckyPlayerW.playerColor;
        missTurnW = missTurn;

    }

    public void OkButton()
    {
        luckyPlayerW.GetComponent<PlayerMovement>().turnMiss += missTurnW;
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
