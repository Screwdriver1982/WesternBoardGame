using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionWindow : MonoBehaviour
{
    [SerializeField] CanvasGroup window;
    [SerializeField] Color businessColor;
    [SerializeField] Color criminalColor;
    [SerializeField] Color colonialColor;
    [SerializeField] Color klondikeColor;
    [SerializeField] Text title;
    [SerializeField] Text description;
    [SerializeField] Text wayTitle;
    [SerializeField] Image icon;

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

    public void OkButtonDescr()
    {
        UIManager.Instance.HideWindow(window);
    }

}
