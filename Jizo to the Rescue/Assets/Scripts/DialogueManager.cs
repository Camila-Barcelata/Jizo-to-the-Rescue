using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


//Called this the dialogue manager, this will build the tree and instantiate the first


public class DialogueManager : MonoBehaviour
{
    //JUST IN CASEEEE
    // //NPCtext
    // public string firstNodeText;

    // //NPCanswers
    // public string NPCanswer1aText;
    // public string NPCanswer1bText;
    // public string NPCanswer1cText;

    //NPC judgement texts
    [SerializeField] public string ascensionDialogue; //what person says in response to being ascended
    [SerializeField] public string reincarnationDialogue; 
    [SerializeField] public string condemnationDialogue; 
    
    //OTHER JUST IN CASE
    // //choices
    // public string PlayerConnection1; 
    // public string PlayerConnection2; 
    // public string PlayerConnection3;

    PersonController character;

    public TMP_Text speechBubbleText;
    public TMP_Text optionButton1Text;
    public TMP_Text optionButton2Text;

    private DialogueNode firstNode;
    private DialogueNode currentNode;

    //[SerializeField] public Sprite characterSprite;

    void Start(){
        character = GetComponent<PersonController>();
        //this is the dictionary that will be made into a heap
        Dictionary<string, string[]> thingsCharacterSays = character.thingsPersonSays;

        // initialize all DialogueNodes! <3 
        // dialogueNodeDict holds thingsChrSays's keys as keys & its DialogueNode object as values
        Dictionary<string, DialogueNode> dialogueNodeDict =InitializeDialogueNodes(thingsCharacterSays);
        //Initialize the heap! <3
        InitializeHeap(thingsCharacterSays, dialogueNodeDict);
        //--------------------------------------------------------
        //ERRRRRRRR WRONG BUTTON THIS IS WRONG!!!! 
        //should reference the first dialogue node object in dialogueNodeDict, since we only initialize our dialogue nodes here
        //  (i'll leave that to u <3)
        List<string> thingsChrSaysKeys = new List<string>(thingsCharacterSays.Keys);
        currentNode = dialogueNodeDict[thingsChrSaysKeys[0]];

        //also i think we should do the traversal/displaying in update? mayhaps?
        DisplayCurrentNode(); //call display function

        //comment hell.... where u go when u don't use 6.6. tabbed for clarity, untab if we use this
            //first chocies will intialize, 

            // firstNode = new DialogueNode(firstNodeText);

            // DialogueNode Node1a = new DialogueNode(NPCanswer1aText);
            // DialogueNode Node1b = new DialogueNode(NPCanswer1bText);
            // DialogueNode Node1c = new DialogueNode(NPCanswer1cText);

            // Connect(firstNode,Node1a, PlayerConnection1);
            // Connect(firstNode,Node1b, PlayerConnection2);
            // Connect(firstNode,Node1c, PlayerConnection3);

            // //-------------------------------------------------------
            // //we'll figure out a way to make the text for the next nodes, either serializes field
            // //or putting it in directly (might be easier to just do it directly)

            // //this is branch 1
            // DialogueNode Node1a2a = new DialogueNode("NPCanswer2aText");
            // DialogueNode Node1a2b = new DialogueNode("NPCanswer1bText");
            // DialogueNode Node1a2c = new DialogueNode("NPCanswer1cText");

            // Connect(Node1a, Node1a2a, "yooooo"); //change text
            // Connect(Node1a, Node1a2b, "wewefwef"); 
            // Connect(Node1a, Node1a2c, "ladadadadada");

            // //-------------------------------------------------------
            
            // //this is branch 2 (test end)
            // DialogueNode endNode = new DialogueNode("Good bye!!!!");
            // DialogueNode endNode1 = new DialogueNode("Good bye!!!!");
            // DialogueNode endNode2 = new DialogueNode("Good bye!!!!");

            // Connect(Node1a2a, endNode, "bye bye!!!"); //change text
            // Connect(Node1a2a, endNode1, "bye byeee!!!"); //change text
            // Connect(Node1a2a, endNode2, "bye byeeeeeee!!!"); //change text
        
    }
    

    public static bool Connect(DialogueNode headnode, DialogueNode followingnode)
    {
        if (headnode.nextNodes.Contains(followingnode)){
            return false;
        }

        headnode.nextNodes.Add(followingnode);
        // headnode.answerChoices.Add(playerAnswer);
        return true;    
        
    }

    Dictionary<string, DialogueNode> InitializeDialogueNodes(Dictionary<string, string[]> thingsCharacterSays)
    {

        //keys = strings, vals = dialogue node object
        Dictionary<string, DialogueNode> dialogueNodeDict = new Dictionary<string, DialogueNode>();
        for(int i = 0; i < thingsCharacterSays.Count; i++)
        {
            List<string> keyList = new List<string>(thingsCharacterSays.Keys);

            dialogueNodeDict[keyList[i]]= new DialogueNode(keyList[i]);
            string[] tempQuestionsLst = thingsCharacterSays[keyList[i]];
        
            dialogueNodeDict[keyList[i]].answerChoices.Add(tempQuestionsLst[0]);
            dialogueNodeDict[keyList[i]].answerChoices.Add(tempQuestionsLst[1]);
        }
        return dialogueNodeDict;
    }

    void InitializeHeap(Dictionary<string, string[]> thingsCharacterSays, Dictionary<string, DialogueNode> dialogueNodeDict)
    {
        for (int i = 0; i < thingsCharacterSays.Count; i ++)
        {
            List<string> characterKeyList = new List<string>(thingsCharacterSays.Keys);
            List<string> dialogueNodeKeys = new List<string>(dialogueNodeDict.Keys);
            
            //ensure no null pointers
            if (characterKeyList[i] != null && dialogueNodeKeys[i] != null) 
            {
                //get left/right child indices, & the head node object
                int leftChild = FindLeftChild(i);
                int rightChild = FindRightChild(i);
                Debug.Log("InitializeHeap(): leftChild: " + leftChild);
                Debug.Log("InitializeHeap(): rightChild: " + rightChild);
                DialogueNode tempHeadDialogueNode = dialogueNodeDict[dialogueNodeKeys[i]];
                Debug.Log("DialogueNode tempHeadDialogueNode: " + tempHeadDialogueNode + "dialogueNodeKeys[i]" + dialogueNodeKeys[i]);
                Debug.Log("tempHeadDialogueNode.answerChoices: " + tempHeadDialogueNode.answerChoices[0]);

                //connect 2 children to head node
                // for (int j; j< dialogueNodeDict.Count; j++)
                // {
                //     Debug.Log("dialogueNodeDict[dialogueNodeKeys[j]]" + dialogueNodeDict[dialogueNodeKeys[j]]);    
                // }

                Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[leftChild]]);
                Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]]);

                //Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]], tempHeadDialogueNode.answerChoices[1]);

            } else {
                break;
            }
        }
    }


    int FindLeftChild(int index) {
        int leftChildIndex = (index* 2 )+ 1;
        return leftChildIndex;

    }
    int FindRightChild(int index) {
        int rightChildIndex = (index* 2 )+ 2;
        return rightChildIndex;
    }
    
    void DisplayCurrentNode() //then this displays everything

    { 
        speechBubbleText.text = currentNode.NPCtext;
        optionButton1Text.text = currentNode.answerChoices[0];
        optionButton2Text.text = currentNode.answerChoices[1];
        //characterSprite = characterSprite;
    }

    public void TraverseLeftChild()
    {
        currentNode = currentNode.nextNodes[0];
        DisplayCurrentNode();
    }
    public void TraverseRightChild()
    {
        currentNode = currentNode.nextNodes[1];
        DisplayCurrentNode();
    }
 
    // public void SelectOption(int index) //this first
    // {
    //     currentNode = currentNode.nextNodes[index];
    //     DisplayCurrentNode();   
    // }

    
    public void AscensionClicked(int index)
    {
        if (index == 0)
        {
            speechBubbleText.text = ascensionDialogue;
            SceneManager.LoadScene("Ascension");
        }
        else if (index == 1)
        {   
            speechBubbleText.text = reincarnationDialogue;
            SceneManager.LoadScene("Ascension");
        }
        else if (index == 2)
        {
            speechBubbleText.text = condemnationDialogue;
            SceneManager.LoadScene("Ascension");
        }
    }
            

}
    



