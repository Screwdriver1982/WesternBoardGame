using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeculationExchange : MonoBehaviour
{
    [SerializeField] GameObject shareUIArea;
    [SerializeField] RectTransform content;
    [SerializeField] CanvasGroup window;
    [SerializeField] Text coefTxt;
    ShareUI shareUI;

    // Start is called before the first frame update
    public void OpenWindow(Player playerInit, float coef)
    {
        //чистим акции, которые были
        foreach (Transform child in content)
        {

            Destroy(child.gameObject);
        }

        //рисуем акции игрока
        DrawShare(playerInit, coef);
        foreach (Transform child in content)
        {

            Destroy(child.gameObject);
        }
        DrawShare(playerInit, coef);

        coefTxt.text = "x" + coef;
    }

    private void DrawShare(Player playerInit, float coef)
    {
        foreach (Shares share in playerInit.playerShares)
        {
            GameObject newShareUI = Instantiate(shareUIArea);

            newShareUI.transform.SetParent(content, false);
            shareUI = shareUIArea.GetComponent<ShareUI>();
            shareUI.Initialize(share, coef);
        }
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

}
