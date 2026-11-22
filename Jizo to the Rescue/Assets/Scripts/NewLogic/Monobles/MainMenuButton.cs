using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : Button
{
    RectTransform rectTransform;
    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    void Update()
    {
        if(IsHighlighted())
        {
            MainMenuController.menuController.DisplayArrow(rectTransform);
        }
    }
}
