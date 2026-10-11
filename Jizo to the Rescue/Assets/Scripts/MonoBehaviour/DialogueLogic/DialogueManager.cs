using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class DialogueManager : MonoBehaviour
{

    //NPC judgement texts
    [SerializeField] public string ascensionDialogue; //what person says in response to being ascended
    [SerializeField] public string reincarnationDialogue; 
    [SerializeField] public string condemnationDialogue; 
    
    //objects: character &UI
    PersonController character;
    public GameObject backButton;
    public GameObject leftButton;
    public GameObject rightButton;
    //text objects
    public TMP_Text speechBubbleText;
    public TMP_Text optionButton1Text;
    public TMP_Text optionButton2Text;

    //global Dictionary, keys = NPCText : values = corresponding DialogueNode
    Dictionary<string, DialogueNode> dialogueNodeDict
    int validNodesCount;
    
    //private important global dialogue nodes
    private DialogueNode firstNode;
    private DialogueNode currentNode;

    //private HandwritingText obj, handles the
    //  cool typewriter/handwriting dialogue effect
    //private HandwritingText handwritingText;

    //animation...???
    public SpriteRenderer fire;
    public SpriteRenderer pureLand;
    public SpriteRenderer humanLand;

    void Start(){
        character = GetComponent<PersonController>();
        //this is the dictionary that will be made into a heap
        Dictionary<string, string[]> thingsCharacterSays = character.thingsPersonSays;
        
        //-----INITIALIZE NODES AND TREE------<3

        // initialize all DialogueNodes! <3 
        // dialogueNodeDict holds thingsChrSays's keys as keys & its DialogueNode object as values
        dialogueNodeDict =InitializeDialogueNodes(thingsCharacterSays);
        //count how many "valid" nodes there are in dialogueNodeDict (or non-dummy nodes)
        validNodesCount = CountValidNodes(dialogueNodeDict);
        //Initialize the heap! <3
        InitializeHeap(thingsCharacterSays, dialogueNodeDict);

        //-----INITIALIZE IMPORTANT NODES------<3
        List<string> thingsChrSaysKeys = new List<string>(thingsCharacterSays.Keys);
        firstNode = dialogueNodeDict[thingsChrSaysKeys[0]]; //DON'T DELETE THIS, THIS IS NOT REDUNDANT!
        currentNode = dialogueNodeDict[thingsChrSaysKeys[0]];
        currentNode.traveledTo = true; //set firstNode's traveledTo to true, since technically we never travel to it using the buttons

        //-----DISPLAY IT ALL!!!------<3
        DisplayCurrentNode(); //call display function 
    }
    
    void Update()
    {
        try
        {
            DisplayCurrentNode();
            HandleButtons();
        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //skip dummy text
            Debug.Log("ArgumentOutOfRangeException in Update()");
            ResetTree();
        }
        
    }

    public static bool Connect(DialogueNode headnode, DialogueNode followingnode)
    {
        if (headnode.nextNodes.Contains(followingnode)){
            return false;
        }

        headnode.nextNodes.Add(followingnode);
        return true;    
    }

    int CountValidNodes(Dictionary<string, DialogueNode> dialogueNodeDict)
    {
        int validNodesCount = 0;
        foreach(DialogueNode node in dialogueNodeDict.Values())
        {
            //if node is valid
            if(!node.NPCText.Contains("None"))
            {
                //increment validNodesCount
                validNodesCount++;
            }
        }
        return validNodesCount;
    }

    Dictionary<string, DialogueNode> InitializeDialogueNodes(Dictionary<string, string[]> thingsCharacterSays)
    {
        //keys = NPCText strings, vals = dialogue node object
        Dictionary<string, DialogueNode> dialogueNodeDict = new Dictionary<string, DialogueNode>();
        for(int i = 0; i < thingsCharacterSays.Count; i++)
        {
            List<string> keyList = new List<string>(thingsCharacterSays.Keys);

            dialogueNodeDict[keyList[i]]= new DialogueNode(keyList[i]);
            string[] tempQuestionsLst = thingsCharacterSays[keyList[i]];
            if(tempQuestionsLst.Length !=0)
            {
                dialogueNodeDict[keyList[i]].answerChoices.Add(tempQuestionsLst[0]);
                dialogueNodeDict[keyList[i]].answerChoices.Add(tempQuestionsLst[1]);
            } 
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
                DialogueNode tempHeadDialogueNode = dialogueNodeDict[dialogueNodeKeys[i]];
                //get left/right child indices, & the head node object
                int leftChild = FindLeftChild(i);
                int rightChild = FindRightChild(i);
                
                //if not at end, connect 2 children to head node
                try
                {
                    if(dialogueNodeKeys[leftChild] != null && dialogueNodeKeys[rightChild] != null)
                    {
                        bool connect1Success = Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[leftChild]]);
                        bool connect2Success =Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]]);
                    }
                }
                catch (ArgumentOutOfRangeException argumentOutOfRangeException)
                { 
                    Debug.Log("Probably reached the end of tree; "+ argumentOutOfRangeException.Message);
                }

            } else {
                break;
            }
        }
    }

    void DisplayCurrentNode() 
    { 
        //implement handwriting effect later lol
        //ONLY USE HANDWRITE EFFECT ON THIS NPC TEXT!!
        speechBubbleText.text = currentNode.NPCtext;

        optionButton1Text.text = currentNode.answerChoices[0];
        optionButton2Text.text = currentNode.answerChoices[1];
    }
    
    //ONLY RESETS CURR NODE TO FIRST NODE, DOES NOT HANDLE RESETING FULL INTERACTION
    //used for looping back to start when reached a dummy node
    public void ResetTree() 
    {
        currentNode = firstNode;
        currentNode.nextNodes = firstNode.nextNodes;
    }

    //RESETS FULL INTERACTION
    public void ResetInteraction()
    {
        currentNode = firstNode;
        currentNode.nextNodes = firstNode.nextNodes;

        //might not work cause we're updating it locally??? maybe??
        foreach(DialogueNode node in dialogueNodeDict.Values())
        {
            node.traveledTo = false;
        }
    }

    //top button press
    public void TraverseLeftChild()
    {
        try
        {
            Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);

            //logs dummy nodes; should never end up being displayed
            if (currentNode.nextNodes[0].NPCtext.Contains("None"))
            {
                Debug.Log("left child is None");
            }

            currentNode.traveledTo = true;
            currentNode = currentNode.nextNodes[0];
            checkForEndingDialogue()

            Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);
        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }

    //bottom button press
    public void TraverseRightChild()
    {
        try
        {
            Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);

            //logs dummy nodes; should never end up being displayed
            if (currentNode.nextNodes[1].NPCtext.Contains("None"))
            {
                Debug.Log("right child is None");
            }

            currentNode = currentNode.nextNodes[1];
            currentNode.traveledTo = true;
            checkForEndingDialogue()

            Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);
        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }

    void checkForEndingDialogue()
    {
        //test every non-dummy node
        int incrementor = 0; //keeps track of valid nodes that have been traveled to
        foreach(DialogueNode node in dialogueNodeDict)
        {
            //if node is not a dummy node
            if(!node.NPCtext.Contains("None") && node.traveledTo = true)
            {incrementor++;} 
            //for better runtime--breaks early if we catch a valid node that hasn't been traveled to
            elif(!node.NPCtext.Contains("None") && node.traveledTo = false) 
            {break;}
        }

        if(incrementor == validNodesCount)
        {
            //!!!change to ending dialogue lst (will figure this out in a bit)
            Debug.Log("Reached ending dialogue successfully!!")
        }
    }

    //handles the realm decision animations
    public void AscensionClicked(int index)
    {
        if (index == 0)
        {
            speechBubbleText.text = condemnationDialogue;
            fire.gameObject.SetActive(true);
        }
        else if (index == 1)
        {   
            speechBubbleText.text = reincarnationDialogue;
            humanLand.gameObject.SetActive(true);
        }
        else if (index == 2)
        {
            speechBubbleText.text = ascensionDialogue;
            pureLand.gameObject.SetActive(true);
        }
    }

    int FindLeftChild(int index) {
        int leftChildIndex = (index * 2 )+ 1;
        return leftChildIndex;
    }
    int FindRightChild(int index) {
        int rightChildIndex = (index* 2 )+ 2;
        return rightChildIndex;
    }

    //------BUTTON LOGIC--HIDING AND UNHIDING FOR CLEANER UI---<3

    void HandleButtons()
    {
        //if we're at the start, make sure back button isn't Active
        if(currentNode == firstNode)
        {
            SetBackButtonActive(false);
            SetLeftButtonActive(true);
            SetRightButtonActive(true);
        }
        //if node is a dead end, set back button active & hide left/right button
        if (currentNode.answerChoices[0] == "" || currentNode.answerChoices[1] =="")
        {
            if(currentNode.answerChoices[0] == "" && currentNode.answerChoices[1] =="")
            {
                SetBackButtonActive(true);
            }
            if(currentNode.answerChoices[0] =="")
            {
                SetLeftButtonActive(false);
            }
            if(currentNode.answerChoices[1] =="")
            {
                SetRightButtonActive(false);
            }
        }
    }

    public void SetBackButtonActive(bool isActive)
    {
        backButton.SetActive(isActive);
    }

    //Handles setting button correlating to left child active or not
    public void SetLeftButtonActive(bool isActive)
    {
        leftButton.SetActive(isActive);
    }
    //Handles setting button correlating to right child active or not
    public void SetRightButtonActive(bool isActive)
    {
        rightButton.SetActive(isActive);
    }

    
}