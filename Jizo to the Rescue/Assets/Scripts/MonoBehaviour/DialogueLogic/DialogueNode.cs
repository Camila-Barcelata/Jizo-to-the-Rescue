using UnityEngine;
using System.Collections.Generic;


//I made this its own script for organization but this is basically the choice
//nodes of the diagram :)

public class DialogueNode{

    public string NPCtext; //text displayed
    public List <DialogueNode> nextNodes; //next nodes
    public List <string> answerChoices; //answers
    public bool traveledTo; //bool that keeps track of if the node has been traveled to

    public int baseSpriteIndex; //index correlated to baseSprite object in baseSprites
    public int mouthSpriteIndex; //index correlated to mouthSprite object in mouthSprites

    public Sprite baseSprite; //sprite objects that will be displayed with a node
    public Sprite mouthSprite;

    public DialogueNode(string NPCanswer) {
        this.NPCtext = NPCanswer;
        answerChoices = new List<string>();
        nextNodes = new List<DialogueNode>(); 
        traveledTo = false;

        // baseSpriteIndex = 0;
        // mouthSpriteIndex = 0;

        baseSprite = null;
        mouthSprite = null;

    }
}
