using UnityEngine;

public class PersonSprite : MonoBehaviour
{
    public Sprite currentSprite;
    SpriteRenderer spriteRenderer;
    
    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        DisplayCurrentSprite();
    }

    void DisplayCurrentSprite()
    {
        spriteRenderer.sprite = currentSprite;
    }
}
