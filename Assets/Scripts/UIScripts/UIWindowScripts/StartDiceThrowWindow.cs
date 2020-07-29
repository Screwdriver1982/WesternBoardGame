using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDiceThrowWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    public void OkButton()
    {
        GameManager.Instance.gameStarted = true;
        UIManager.Instance.HideWindow(window);
    }

}
