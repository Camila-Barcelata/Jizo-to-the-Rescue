using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class DialogueManager : MonoBehaviour
{
    //-----OBJECTS-------<3
        //character & UI buttons
        PersonController character;
        public GameObject nextButton;
        public GameObject backButton;
        public GameObject leftButton;
        public GameObject rightButton;
        //text objects
        public TMP_Text speechBubbleText;
        public TMP_Text optionButton1Text;
        public TMP_Text optionButton2Text;

        public GameObject resetButton;

        //private HandwritingText obj, handles the
        //  cool typewriter/handwriting dialogue effect
        // (i think this script should be put in the Person GameObject)
        //private HandwritingText handwritingText;

    //-----REALMS ANIMATIONS----<3
        //animations that play when you send a person to hell. all consolidated into a list.
        //element 0 = fire/hell animation
        //element 1 = human land animation
        //element 2 = heaven land animation

        //btw---this will probably not be how we handle animation for the future!!
        public List<SpriteRenderer> realmAnimations;

    //----HANDLE DIALOGUE STATES----<3
        //way to tell displayCurrentNode to display either dialogue tree dialogue, realms dialogue, or ending dialogue!
        enum DialogueState{mainDialogueTree, Realms, Endings};//could probably be named better lol
        DialogueState currentDialogueState = DialogueState.mainDialogueTree;

        int currentIndex =0; //used to iterate through dialogue boxes, updated with the Next-> button

    //-----MAIN DIALOGUE TREE------<3
        //global Dictionary, keys = NPCText : values = corresponding DialogueNode
        Dictionary<string, DialogueNode> dialogueNodeDict;
        int validNodesCount;
        int incrementor = 0; //keeps track of valid nodes that have been traveled to
        
        //private important global dialogue nodes
        private DialogueNode firstNode;
        private DialogueNode currentNode;

    //-----REALMS DIALOGUE---------<3
        //aka what a character says in response to where they're reincarnated
        List<string> realmDialogue;

    //-----ENDING DIALOGUE--------<3
        List<string> endingDialogue;


    void Start(){
        //-----ACCESS PERSON SCRIPTS/DATA-----<3
        character = GetComponent<PersonController>();
        //this is the dictionary that will be made into a heap
        Dictionary<string, string[]> thingsCharacterSays = character.thingsPersonSays;
        realmDialogue = character.realmDialogue;
        endingDialogue = character.endingDialogue;
        
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
        HandleDisplayCurrentText(); //call display function 
    }
    
    void Update()
    {
        try
        {
            HandleDisplayCurrentText();
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
        foreach(DialogueNode node in dialogueNodeDict.Values)
        {
            //if node is valid
            if(!node.NPCtext.Contains("None"))
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

    //handles displaying current text, regardless of it's in the main tree, ending dialogue list, or realms list
    void HandleDisplayCurrentText()
    {
        if(currentDialogueState == DialogueState.mainDialogueTree)
        {
            DisplayCurrentNode();
        } else if(currentDialogueState == DialogueState.Endings)
        {
            DisplayCurrentEndingText(currentIndex);
            //called when "Next" button is clicked
        }

        //don't need to call realm text-- that's called when you click a realms button
        
    }
    void DisplayCurrentNode() 
    { 
        //implement handwriting effect later lol
        //ONLY USE HANDWRITE EFFECT ON THIS NPC TEXT!!
        if(currentNode != null)
        {
            speechBubbleText.text = currentNode.NPCtext;

            optionButton1Text.text = currentNode.answerChoices[0];
            optionButton2Text.text = currentNode.answerChoices[1];
        }
    }

    void DisplayCurrentEndingText(int currentIndex)
    {
        //only shows first string --should fix the button to iterate to next ending dialogue 
        
        speechBubbleText.text = endingDialogue[currentIndex];
        nextButton.SetActive(true);
 
        if (currentIndex < endingDialogue.Count-1){
            nextButton.SetActive(true);
        }

        else{

            Debug.Log("reached the ending of nexts");
            nextButton.SetActive(false);
            backButton.SetActive(false);
        }
    }

    void DisplayCurrentRealmText(int index)
    {
        speechBubbleText.text = realmDialogue[index];

        //display realm animation
        realmAnimations[index].gameObject.SetActive(true);
    }

    //called when Next button is pressed
    public void SetNextString()
    {
        currentIndex ++;
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
        currentDialogueState = DialogueState.mainDialogueTree;
        currentNode = firstNode;
        currentNode.nextNodes = firstNode.nextNodes;
        //update dialogueState to main dialogue tree
        //might not work cause we're updating it locally??? maybe??
        foreach(DialogueNode node in dialogueNodeDict.Values)
        {
            Debug.Log("Node state first is:" + node.traveledTo);
            node.traveledTo = false;
            Debug.Log("Node state now is:" + node.traveledTo);
        }
        nextButton.SetActive(false);
        incrementor=0;
    }

    //top button press
    public void TraverseLeftChild()
    {
        try
        {
            //Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);

            //logs dummy nodes; should never end up being displayed
            if (currentNode.nextNodes[0].NPCtext.Contains("None"))
            {
                //Debug.Log("left child is None");
            }

            
            currentNode = currentNode.nextNodes[0];
            currentNode.traveledTo = true;
            CheckForEndingDialogue();
            

            //Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);
        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            //Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }

    //bottom button press
    public void TraverseRightChild()
    {
        try
        {
            //Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);

            //logs dummy nodes; should never end up being displayed
            if (currentNode.nextNodes[1].NPCtext.Contains("None"))
            {
                Debug.Log("right child is None");
            }

            currentNode = currentNode.nextNodes[1];
            currentNode.traveledTo = true;
            CheckForEndingDialogue();

            //Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);
        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            //Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }

    //NOT DONE!!!
    void CheckForEndingDialogue()
    {
        incrementor = 0;
        //test every non-dummy node
        Debug.Log("START checkingEnd: validNodesCt: " + validNodesCount + " incrementor: " + incrementor);
        
        foreach(DialogueNode node in dialogueNodeDict.Values)
        {

            //if node is not a dummy node
            
            if(!node.NPCtext.Contains("None") && node.traveledTo == true)
            {incrementor++;} 
    
        }

        Debug.Log("END checkingEnd: validNodesCt: " + validNodesCount + " incrementor: " + incrementor);
        if(incrementor >= validNodesCount-1)
        {
            //changes to display endingDialogue!
            
            currentDialogueState = DialogueState.Endings; 
            Debug.Log("Reached ending dialogue successfully!!");
        }
    }

    //is called when realm buttons are clicked
    public void RealmsButtonsClicked(int index)
    {
        currentDialogueState = DialogueState.Realms;

        DisplayCurrentRealmText(index);
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
        //main tree
        if(currentDialogueState == DialogueState.mainDialogueTree)
        {
            //if we're at the start node, make sure back button isn't Active but dialogue options are
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
        //ending state
        else if(currentDialogueState == DialogueState.Endings)
        {
            backButton.SetActive(false);
            resetButton.SetActive(true);
            //hide normal dialogue option buttons
            SetLeftButtonActive(false);
            SetRightButtonActive(false);
        }
        else if(currentDialogueState == DialogueState.Realms)
        {
            //hide every dialogue option buttons
            SetLeftButtonActive(false);
            SetRightButtonActive(false);
            SetBackButtonActive(false);
            nextButton.SetActive(false);
        }

        
    }

    void SetBackButtonActive(bool isActive)
    {
        backButton.SetActive(isActive);

    }

    //Handles setting button correlating to left child active or not
    void SetLeftButtonActive(bool isActive)
    {
        leftButton.SetActive(isActive);
    }
    //Handles setting button correlating to right child active or not
    void SetRightButtonActive(bool isActive)
    {
        rightButton.SetActive(isActive);
    }
}