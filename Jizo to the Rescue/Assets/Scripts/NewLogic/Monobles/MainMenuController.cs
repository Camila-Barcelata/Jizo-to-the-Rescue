using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public RectTransform arrowSprite;
    // [SerializeField] public RectTransform button;
    public static MainMenuController menuController;
    public float xOffset;

    void Awake()
    {
        if (menuController == null)
        {
            menuController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void DisplayArrow(RectTransform button)
    {
        arrowSprite.anchoredPosition = 
            new Vector3(button.anchoredPosition.x + xOffset, 
            button.anchoredPosition.y);
        arrowSprite.gameObject.SetActive(true);
    }
}
