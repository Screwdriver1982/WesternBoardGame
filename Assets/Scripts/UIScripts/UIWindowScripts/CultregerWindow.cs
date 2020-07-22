using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultregerWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    public void CommonWayButton()
    {
        GameManager.Instance.CurrentPlayerDiceTurn();
        UIManager.Instance.HideWindow(window);
    }

    public void ColonyWayButton()
    {
        GameManager.Instance.WhoIsPlayerMVMNT().toColony = true;
        GameManager.Instance.CurrentPlayerDiceTurn();
        UIManager.Instance.HideWindow(window);
    }
}
