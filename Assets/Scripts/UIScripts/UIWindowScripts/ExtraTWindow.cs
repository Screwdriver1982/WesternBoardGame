using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTWindow : MonoBehaviour
{
    
    [SerializeField] CanvasGroup window;
    [SerializeField] int extraTurnTemp;
    PlayerMovement playerMvmnt;
    public void OpenWindow(int extraTurn)
    {
        extraTurnTemp = extraTurn;
    }

    public void OkButton()
    {
        playerMvmnt = GameManager.Instance.WhoIsPlayerMVMNT();
        playerMvmnt.extraTurnNumber += extraTurnTemp;
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

}
