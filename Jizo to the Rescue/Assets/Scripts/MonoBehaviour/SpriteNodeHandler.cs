/*
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
        
    }

    // Update is called once per frame
    void Update()
    {
        currentNode = dialogueManager.currentNode; //WRITE A CONSTRUCTOR FOR DIALOGUEMANAGER???

        //display currentNode's sprites
        //Debug.Log("TEXT: " + currentNode.NPCtext + "current base: " + currentNode.baseSpriteIndex + "current mouth: " + currentNode.mouthSpriteIndex);
        baseSpriteRenderer.sprite = currentNode.baseSprite;
        mouthSpriteRenderer.sprite = currentNode.mouthSprite;
    }
}
*/