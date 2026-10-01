using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;


//Called this the dialogue manager, this will build the tree and instantiate the first


public class DialogueManager : MonoBehaviour
{

    //NPC judgement texts
    [SerializeField] public string ascensionDialogue; //what person says in response to being ascended
    [SerializeField] public string reincarnationDialogue; 
    [SerializeField] public string condemnationDialogue; 
    
    PersonController character;

    public TMP_Text speechBubbleText;
    public TMP_Text optionButton1Text;
    public TMP_Text optionButton2Text;

    private DialogueNode firstNode;
    private DialogueNode currentNode;

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
        List<string> thingsChrSaysKeys = new List<string>(thingsCharacterSays.Keys);
        currentNode = dialogueNodeDict[thingsChrSaysKeys[0]];

        //also i think we should do the traversal/displaying in update? mayhaps?
        DisplayCurrentNode(); //call display function 
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
                Debug.Log("DialogueNode tempHeadDialogueNode: " + tempHeadDialogueNode + "dialogueNodeKeys[i]" + dialogueNodeKeys[i]);
                //get left/right child indices, & the head node object
                int leftChild = FindLeftChild(i);
                int rightChild = FindRightChild(i);
                Debug.Log("InitializeHeap(): leftChild: " + leftChild);
                Debug.Log("InitializeHeap(): rightChild: " + rightChild);
                
                

                //if not at end, connect 2 children to head node
                try
                {
                    if(dialogueNodeKeys[leftChild] != null && dialogueNodeKeys[rightChild] != null)
                    {
                        Debug.Log("tempHeadDialogueNode.answerChoices[0]: " + tempHeadDialogueNode.answerChoices[0]);
                        Debug.Log("tempHeadDialogueNode.answerChoices[1]: " + tempHeadDialogueNode.answerChoices[1]);
                        
                        bool connect1Success = Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[leftChild]]);
                        bool connect2Success =Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]]);
                        Debug.Log("Connection success:" + connect1Success + ", "+connect2Success);
                    }
                }
                catch (ArgumentOutOfRangeException argumentOutOfRangeException)
                { 
                    Debug.Log("Probably reached the end of tree; "+ argumentOutOfRangeException.Message);
                }
                // Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[leftChild]]);
                // Connect(tempHeadDialogueNode, dialogueNodeDict[dialogueNodeKeys[rightChild]]);

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
    



