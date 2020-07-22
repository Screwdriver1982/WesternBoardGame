using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSharesWindow : MonoBehaviour
{
    [SerializeField] GameObject shareUIArea;
    [SerializeField] RectTransform content;
    [SerializeField] CanvasGroup window;
    ShareUI shareUI;

    // Start is called before the first frame update
    public void OpenWindow(Player playerInit)
    {
        //чистим акции, которые были
        foreach (Transform child in content)
        {
            print("Destroy " +child);
            Destroy(child.gameObject);
        }

        //рисуем акции игрока
        foreach (Shares share in playerInit.playerShares)
        {
            GameObject newShareUI = Instantiate(shareUIArea);
            print("Create " + newShareUI);
            newShareUI.transform.SetParent(content, false);
            shareUI = shareUIArea.GetComponent<ShareUI>();
            shareUI.Initialize(share, 1f);
        }
    }

    public void OkButton()
    {
        UIManager.Instance.HideWindow(window);
    }

}
