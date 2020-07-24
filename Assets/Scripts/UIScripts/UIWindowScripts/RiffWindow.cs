using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    public void OkButton()
    {
        Player player = GameManager.Instance.WhoIsPlayer();
        player.trinkets = 0;
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }
}
