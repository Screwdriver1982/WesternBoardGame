using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionWindow : MonoBehaviour
{
    #region Singleton
    public static DescriptionWindow Instance { get; private set; }

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

    [SerializeField] Color businessColor;
    [SerializeField] Color criminalColor;
    [SerializeField] Color colonialColor;
    [SerializeField] Color klondikeColor;
    Text title;
    Text description;
    Text wayTitle;
    Image icon;

    public void ChangeDescriptionWindow(Sprite cellIcon, string cellTitle, string cellDescription, string cellWayTitle)
    {
        title.text = cellTitle;
        description.text = cellDescription;
        wayTitle.text = cellWayTitle;

        wayTitle.color = businessColor;
        if (cellWayTitle == "CRIMINAL")
        {
            wayTitle.color = criminalColor;
        }
        else if (cellWayTitle == "COLONIAL")
        {
            wayTitle.color = colonialColor;
        }
        else if (cellWayTitle == "KLONDIKE")
        {
            wayTitle.color = klondikeColor;

        }

        icon.sprite = cellIcon;

    }


}
