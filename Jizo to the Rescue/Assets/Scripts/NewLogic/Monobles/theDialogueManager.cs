using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
 
// Shows the nodes and answer buttons. When the ending is reached it tells the controller.
public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] Image baseImage;
    [SerializeField] Image mouthImage;

    //buttons
    [SerializeField] Button answerButton1; // top
    [SerializeField] Button answerButton2; // bottom

    //button images
    [SerializeField] Image answerButton1Image;
    [SerializeField] Image answerButton2Image;
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite greyedButtonSprite;
 
    [SerializeField] TMP_Text answerText1;
    [SerializeField] TMP_Text answerText2;
 
    [SerializeField] theInterviewScontroller interviewController;
 
    [SerializeField] CharacterDetailsInspector person;
 
    List<Node> countEnd = new List<Node>(); // all nodes node marked CountsTowardsEnding
    List<Node> visited = new List<Node>();  // the marked nodes we've been has been through

    //nodes for marking grey 

    //stack of node ids representing which path the player took which gets reset after each "reset" of the tree
    // starts from the first decision of the player from introduction text
    Stack<string> playerPath = new Stack<string>(); //used to know which way to go backwards

 
    // The controller calls this when the player presses "Start Interview"
    public void StartDialogue(CharacterDetailsInspector newPerson)
    {
        person = newPerson;
 
        // start fresh for this character
        countEnd.Clear();
        visited.Clear();
        ResetGreyNodes();
 
        // collect the ending nodes
        foreach (Node n in newPerson.CharacterNodes.Values)
        {
            if (n.CountsTowardsEnding)
            {
                countEnd.Add(n);
            }
        }
        TraverseToNode("IntroductionText");
    }
 
    void TraverseToNode(string id)
    {
        Node node = person.CharacterNodes[id]; // finds the node with this id

        DisplayNode(node);
        CheckForEndingDialogue(node);
        PushToPlayerPath(id);
        MarkNodesGrey(node);
        EvaluateResetPlayerPath(node);
    }

    void DisplayNode(Node node)
    {
        // displaying stuff
        textBox.text = node.character_text;
        //the character sprites as list implementation
        baseImage.sprite = node.baseSprite;
        mouthImage.sprite = node.mouthSprite;
 
        // show a button if the answer is actually there
        answerButton1.gameObject.SetActive(node.answers.Count > 0);
        answerButton2.gameObject.SetActive(node.answers.Count > 1);

        if (node.answers.Count > 0) //fun way to hide answer if we go over 2
        {
            //handle left button
            answerText1.text = node.answers[0].answer_text;
            SetupButton(answerButton1, node.answers[0], 0);

            //change left button colors to grey
            if(person.CharacterNodes[node.answers[0].nextID].markedGrey)
            {
                //Debug.Log("left answer is grey");
                answerButton1Image.sprite = greyedButtonSprite;
            }
            else
            {
                answerButton1Image.sprite = defaultButtonSprite;
            }
        }
 
        if (node.answers.Count > 1)
        {
            //handle right button
            answerText2.text = node.answers[1].answer_text;
            SetupButton(answerButton2, node.answers[1], 1);
            if(person.CharacterNodes[node.answers[1].nextID].markedGrey)
            {
                //Debug.Log("right answer is grey");
                answerButton2Image.sprite = greyedButtonSprite;
            }
            else
            {
                answerButton2Image.sprite = defaultButtonSprite;
            }
        }
    }
 
    public void HideAnswerButtons()
    {
        answerButton1.gameObject.SetActive(false);
        answerButton2.gameObject.SetActive(false);
    }
 
    void EvaluateResetPlayerPath(Node currentNode)
    {
        if(currentNode.isALastNode)
        {
            ResetPlayerPath();
        }
    }

    void PushToPlayerPath(string pathID)
    {
        playerPath.Push(pathID);
    }

    void ResetPlayerPath()
    {
        playerPath.Clear();
    }

    void SetupButton(Button button, Answer answer, int pathInt)
    {
        button.onClick.RemoveAllListeners(); // clear the previous node's button's click
 
        string nextID = answer.nextID;
        button.onClick.AddListener(() => TraverseToNode(nextID));  // show the node
    }

    void CheckForEndingDialogue(Node node)
    {
        if (node.CountsTowardsEnding && !visited.Contains(node))
        {
            visited.Add(node);
        }
 
        // on the ending node, and been through every marked node -> the controller plays the final remarks
        //if (node.nodeID == "ending" && visited.Count == countEnd.Count)
        if (visited.Count == countEnd.Count)
        {
            
            Debug.Log("visited and count end comparison: " + visited.Count + " " + countEnd.Count);
            answerButton1.onClick.AddListener(interviewController.ReachedEnding);
            answerButton2.onClick.AddListener(interviewController.ReachedEnding);
            return;
        }
    }

    void MarkNodesGrey(Node currentNode)
    {
        //algorithm only triggers when currentNode is a last node
        if(currentNode.isALastNode)
        {
            //automatically mark the last node as grey
            currentNode.markedGrey = true;
            
            Node dummyNode = currentNode;

            //while the dummy node isn't the intro text
            while(dummyNode != person.CharacterNodes["IntroductionText"])
            {
                //if dummy node isn't a "branch"
                if(dummyNode.answers.Count <2)
                {
                    dummyNode.markedGrey = true;

                    //go "backwards"
                    dummyNode = person.CharacterNodes[playerPath.Pop()];
                }
                else if (dummyNode.answers.Count >= 2) //if dummy node IS a branch
                {
                    int greyIncrementor = 0;
                    int loopIncrementor = 0;
                    //if all the answers of the branch are marked grey, mark the branch as grey
                    foreach(Answer answer in dummyNode.answers)
                    {
                        if(person.CharacterNodes[answer.nextID].markedGrey)
                        {
                            greyIncrementor ++;
                        }
                        loopIncrementor ++;
                    }

                    if(greyIncrementor == loopIncrementor)
                    {
                        dummyNode.markedGrey = true;
                        //keep going backwards
                        dummyNode = person.CharacterNodes[playerPath.Pop()];
                    } else
                    {
                        return;
                    }
                }
            }
        }
    }

    public void ResetGreyNodes()
    {
        foreach(Node node in person.CharacterNodes.Values)
        {
            node.markedGrey = false;
        }
    }
}