using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSharesWindow : MonoBehaviour
{
    [SerializeField] GameObject shareUIArea;
    [SerializeField] RectTransform content;
    [SerializeField] CanvasGroup window;
    [SerializeField] Player player;
    
    bool firstOpen = true;



    // Start is called before the first frame update
    public void OpenWindow(Player playerInit)
    {
        player = playerInit;
        //чистим акции, которые были
        foreach (Transform child in content)
        {
            print("Destroy " +child);
            Destroy(child.gameObject);
        }


        DrawShares();
        foreach (Transform child in content)
        {
            print("Destroy " + child);
            Destroy(child.gameObject);
        }
        DrawShares();

        //StartCoroutine(DrawSharesCoroutine(UIManager.Instance.timeToDrawShare));

    }


    public void OkButton()
    {
        foreach (Transform child in content)
        {
            print("Destroy " + child);
            Destroy(child.gameObject);
        }
        UIManager.Instance.HideWindow(window);
    }
    void DrawShares()
    {
        for (int i = 0; i < player.playerShares.Count; i++)
        {
            GameObject newShareUI = Instantiate(shareUIArea);
            print("Create " + newShareUI);
            newShareUI.transform.SetParent(content, false);
            ShareUI shareUI = shareUIArea.GetComponent<ShareUI>();
            shareUI.Initialize(player.playerShares[i], 1f);
        }

    }

}
