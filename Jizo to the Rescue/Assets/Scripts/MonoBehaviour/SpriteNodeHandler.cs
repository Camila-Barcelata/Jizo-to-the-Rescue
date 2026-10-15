using UnityEngine;

public class SpriteNodeHandler : MonoBehaviour
{
    DialogueManager dialogueManager;
    DialogueNode currentNode;

    //these will be assigned in the inspector
    public SpriteRenderer baseSpriteRenderer;
    public SpriteRenderer mouthSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = GetComponent<DialogueManager>();
        currentNode = dialogueManager.currentNode; //WRITE A CONSTRUCTOR FOR DIALOGUEMANAGER???
    }

    // Update is called once per frame
    void Update()
    {
        //display currentNode's sprites
        baseSpriteRenderer.sprite = currentNode.baseSprite;
        mouthSpriteRenderer.sprite = currentNode.mouthSprite;
    }
}
