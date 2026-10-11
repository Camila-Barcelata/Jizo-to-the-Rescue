using UnityEngine;
using System.Collections.Generic;


//I made this its own script for organization but this is basically the choice
//nodes of the diagram :)

public class DialogueNode{

    public string NPCtext; //text displayed
    public List <DialogueNode> nextNodes; //next nodes
    public List <string> answerChoices; //answers
    public Sprite characterSprite; //node that keeps character sprites(?)
    public bool traveledTo; //bool that keeps track of if the node has been traveled to
 
    public DialogueNode(string NPCanswer) {
        this.NPCtext = NPCanswer;
        answerChoices = new List<string>();
        nextNodes = new List<DialogueNode>(); 
        traveledTo = false;
        this.characterSprite = characterSprite; //doesnt work yet but im thinking every node contains the sprite of the character?
    }
}
