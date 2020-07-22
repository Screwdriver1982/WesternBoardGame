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
            print("Destroy " + child);
            Destroy(child.gameObject);
        }

        //рисуем акции игрока
        foreach (Shares share in playerInit.playerShares)
        {
            GameObject newShareUI = Instantiate(shareUIArea);
            print("Create " + newShareUI);
            newShareUI.transform.SetParent(content, false);
            shareUI = shareUIArea.GetComponent<ShareUI>();
            shareUI.Initialize(share, coef);
        }

        coefTxt.text = "x" + coef;
    }

    public void OkButton()
    {
        GameManager.Instance.NextPlayerTurn();
        UIManager.Instance.HideWindow(window);
    }

}
