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
    
    //objects
    PersonController character;
    public GameObject backButton;
    public GameObject leftButton;
    public GameObject rightButton;

    //text
    public TMP_Text speechBubbleText;
    public TMP_Text optionButton1Text;
    public TMP_Text optionButton2Text;
    
    //private important global dialogue nodes
    private DialogueNode firstNode;
    private DialogueNode currentNode;

    //animation...???
    public SpriteRenderer fire;
    public SpriteRenderer pureLand;
    public SpriteRenderer humanLand;

    void Start(){
        character = GetComponent<PersonController>();
        //this is the dictionary that will be made into a heap
        Dictionary<string, string[]> thingsCharacterSays = character.thingsPersonSays;

        // initialize all DialogueNodes! <3 
        // dialogueNodeDict holds thingsChrSays's keys as keys & its DialogueNode object as values
        Dictionary<string, DialogueNode> dialogueNodeDict =InitializeDialogueNodes(thingsCharacterSays);
        //Initialize the heap! <3
        InitializeHeap(thingsCharacterSays, dialogueNodeDict);

        List<string> thingsChrSaysKeys = new List<string>(thingsCharacterSays.Keys);
        firstNode = dialogueNodeDict[thingsChrSaysKeys[0]]; //DON'T DELETE THIS, THIS IS NOT REDUNDANT!
        currentNode = dialogueNodeDict[thingsChrSaysKeys[0]];

        //also i think we should do the traversal/displaying in update? mayhaps?
        DisplayCurrentNode(); //call display function 
    }
    
    void Update()
    {
        try
        {
            DisplayCurrentNode();
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

    Dictionary<string, DialogueNode> InitializeDialogueNodes(Dictionary<string, string[]> thingsCharacterSays)
    {
        //keys = strings, vals = dialogue node object
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
                //Debug.Log("DialogueNode tempHeadDialogueNode: " + tempHeadDialogueNode + "dialogueNodeKeys[i]" + dialogueNodeKeys[i]);
                //get left/right child indices, & the head node object
                int leftChild = FindLeftChild(i);
                int rightChild = FindRightChild(i);
                // Debug.Log("InitializeHeap(): leftChild: " + leftChild);
                // Debug.Log("InitializeHeap(): rightChild: " + rightChild);
                
                
                //if not at end, connect 2 children to head node
                try
                {
                    if(dialogueNodeKeys[leftChild] != null && dialogueNodeKeys[rightChild] != null)
                    {
                        // Debug.Log("tempHeadDialogueNode.answerChoices[0]: " + tempHeadDialogueNode.answerChoices[0]);
                        // Debug.Log("tempHeadDialogueNode.answerChoices[1]: " + tempHeadDialogueNode.answerChoices[1]);
                        
                        bool connect1Success = Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[leftChild]]);
                        bool connect2Success =Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]]);
                        // Debug.Log("Connection success:" + connect1Success + ", "+connect2Success);
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


    int FindLeftChild(int index) {
        int leftChildIndex = (index * 2 )+ 1;
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
        //Debug.Log("DispCurN: " + "speech: " + speechBubbleText.text + " optionButton1Text: " + optionButton1Text.text + " optionButton1Text: " + optionButton2Text.text);
    }

    public void TraverseLeftChild()
    {
        try
        {
            Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);
            if (currentNode.nextNodes[0].NPCtext.Contains("None"))
            {
                Debug.Log("left child is None");
            }
            currentNode = currentNode.nextNodes[0];
            Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);

        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }
    public void TraverseRightChild()
    {
        try
        {
            Debug.Log("currentNode.NPCtext: " + currentNode.NPCtext);
            if (currentNode.nextNodes[1].NPCtext.Contains("None"))
            {
                Debug.Log("right child is None");
            }
            currentNode = currentNode.nextNodes[1];
            Debug.Log("nextNode.NPCtext: " + currentNode.NPCtext);

        }
        catch(ArgumentOutOfRangeException argumentOutOfRangeException)
        {
            //reset to beginning of tree
            Debug.Log("Maybe reached end of tree");
            ResetTree();
        }  
    }
 
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

    public void ResetTree() 
    {
        currentNode = firstNode;
        currentNode.nextNodes = firstNode.nextNodes;
    }
}