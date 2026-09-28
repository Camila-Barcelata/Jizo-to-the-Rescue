using UnityEngine;
using System.Collections.Generic;


//I made this its own script for organization but this is basically the choice
//nodes of the diagram :)

public class DialogueNode{

    public string NPCtext; //text displayed
    public List <DialogueNode> nextnodes; //next nodes
    public List <string> answerschoices; //answers
    public Sprite characterSprite; //node that keeps character sprites(?)
 
    public DialogueNode(string NPCanswer) {
        this.NPCtext = NPCanswer;
        answerschoices = new List<string>();
        nextnodes = new List<DialogueNode>(); 
        this.characterSprite = characterSprite; //doesnt work yet but im thinking every node contains the sprite of the character?
    }
}
