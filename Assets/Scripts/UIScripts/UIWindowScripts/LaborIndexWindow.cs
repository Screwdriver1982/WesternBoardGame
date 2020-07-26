using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaborIndexWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    [Header("Индексы")]
    [SerializeField] RectTransform[] firstPlayerMatrix;
    [SerializeField] RectTransform[] secondPlayerMatrix;
    [SerializeField] RectTransform[] thirdPlayerMatrix;
    [SerializeField] RectTransform[] fourthPlayerMatrix;

    [Header("Фишки игроков")]
    [SerializeField] GameObject firstPlayerCap;
    [SerializeField] GameObject secondPlayerCap;
    [SerializeField] GameObject thirdPlayerCap;
    [SerializeField] GameObject fourthPlayerCap;

    [Header("Цвета игроков")]
    [SerializeField] Image firstPlColor;
    [SerializeField] Image secondPlColor;
    [SerializeField] Image thirdPlColor;
    [SerializeField] Image fourthPlColor;

    [Header("Игроки")]
    [SerializeField] Player firstPlayer;
    [SerializeField] Player secondPlayer;
    [SerializeField] Player thirdPlayer;
    [SerializeField] Player fourthPlayer;

    bool firstOpen = true;

    public void OpenWindow()
    {
        if (firstOpen)
        {
            firstPlayer = GameManager.Instance.GetPlayerByPlayerNum(0);
            secondPlayer = GameManager.Instance.GetPlayerByPlayerNum(1);
            thirdPlayer = GameManager.Instance.GetPlayerByPlayerNum(2);
            fourthPlayer = GameManager.Instance.GetPlayerByPlayerNum(3);

            firstPlColor.color = firstPlayer.playerColor;
            secondPlColor.color = secondPlayer.playerColor;
            thirdPlColor.color = thirdPlayer.playerColor;
            fourthPlColor.color = fourthPlayer.playerColor;

            firstOpen = false;
        }

        int firstInd = firstPlayer.laborIndex;
        int secondInd = secondPlayer.laborIndex;
        int thirdInd = thirdPlayer.laborIndex;
        int fourthInd = fourthPlayer.laborIndex;



        firstPlayerCap.transform.SetParent(firstPlayerMatrix[firstInd+5],false );
        secondPlayerCap.transform.SetParent(secondPlayerMatrix[secondInd+5], false);
        thirdPlayerCap.transform.SetParent(thirdPlayerMatrix[thirdInd+5], false);
        fourthPlayerCap.transform.SetParent(fourthPlayerMatrix[fourthInd+5], false);

    }

    public void OkButton()
    {
        UIManager.Instance.HideWindow(window);
    }
}
