using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;

    public void StartGameButton()
    {
        UIManager.Instance.HideWindow(window);
    }

}
