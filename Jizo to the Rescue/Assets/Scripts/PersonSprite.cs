using UnityEngine;

public class PersonSprite : MonoBehaviour
{
    public Sprite currentSprite;
    SpriteRenderer spriteRenderer;
    // Update is called once per frame
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
