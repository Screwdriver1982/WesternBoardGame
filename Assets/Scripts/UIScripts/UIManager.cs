using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }
    #endregion
    


    [Header("Появление окон")]
    [SerializeField] float fadeDuration;
    [SerializeField] GameObject descriptionWindow;
    [SerializeField] UIActivePlayer activePlayerUI;
    [SerializeField] UINotActivePlayer secondPlayerUI;
    [SerializeField] UINotActivePlayer thirdPlayerUI;
    [SerializeField] UINotActivePlayer fourthPlayerUI;

    bool isDialogOpen = false;

    public void ShowDescriptionWindow(Sprite cellIcon, string cellTitle, string cellDescription, string cellWayTitle)
    {
        if (!isDialogOpen)
        { 
            descriptionWindow.SetActive(true);
            DescriptionWindow.Instance.ChangeDescriptionWindow(cellIcon, cellTitle, cellDescription, cellWayTitle);
            CanvasGroup descrWindow = DescriptionWindow.Instance.GetComponent<CanvasGroup>();
            ShowWindow(descrWindow);
        }
    }

    public void ShowWindow(CanvasGroup window)
    {
        isDialogOpen = true;
        window.gameObject.SetActive(true);
        window.alpha = 0;
        window.DOFade(1, fadeDuration).SetUpdate(true);


        //TODO lock activity
    }

    public void HideWindow(CanvasGroup window)
    {
        window.DOFade(0, fadeDuration).OnComplete(
               () => {
                   window.gameObject.SetActive(false);
                   isDialogOpen = false;
               }
           ).SetUpdate(true);
    }

    public void SetPlayersUI(Player activePlayer, Player secondPlayer, Player thirdPlayer, Player fourthPlayer)
    {
        activePlayerUI.SetPlayer(activePlayer);
        secondPlayerUI.SetPlayer(secondPlayer);
        thirdPlayerUI.SetPlayer(thirdPlayer);
        fourthPlayerUI.SetPlayer(fourthPlayer);

    }

}
