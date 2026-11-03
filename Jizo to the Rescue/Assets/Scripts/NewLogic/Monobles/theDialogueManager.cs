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
    [SerializeField] Button answerButton1; // top
    [SerializeField] Button answerButton2; // bottom
 
    [SerializeField] TMP_Text answerText1;
    [SerializeField] TMP_Text answerText2;
 
    [SerializeField] theInterviewScontroller interviewController;
 
    CharacterDetailsInspector person;
 
    List<Node> countEnd = new List<Node>(); // all nodes node marked CountsTowardsEnding
    List<Node> visited = new List<Node>();  // the marked nodes we've been has been through
 
    // The controller calls this when the player presses "Start Interview"
    public void StartDialogue(CharacterDetailsInspector newPerson)
    {
        person = newPerson;
 
        // start fresh for this character
        countEnd.Clear();
        visited.Clear();
 
        // collect the ending nodes
        foreach (Node n in newPerson.CharacterNodes.Values)
        {
            if (n.CountsTowardsEnding)
            {
                countEnd.Add(n);
            }
        }
 
        ShowNode("IntroductionText");
    }
 
    void ShowNode(string id)
    {
        Node node = person.CharacterNodes[id]; // finds the node with this id
 
        // remember marked nodes the player has been through once
        if (node.CountsTowardsEnding && !visited.Contains(node))
        {
            visited.Add(node);
        }
 
        // on the ending node, and been through every marked node -> the controller plays the final remarks
        if (id == "ending" && visited.Count == countEnd.Count)
        {
            interviewController.ReachedEnding();
            return;
        }
 
        // displaying stuff
        textBox.text = node.character_text;
        baseImage.sprite = node.baseSprite;
        mouthImage.sprite = node.mouthSprite;
 
        // show a button if the answer is actually there
        answerButton1.gameObject.SetActive(node.answers.Count > 0);
        answerButton2.gameObject.SetActive(node.answers.Count > 1);
 
        if (node.answers.Count > 0) //fun way to hide answer if we go over 2
        {
            answerText1.text = node.answers[0].answer_text;
            SetupButton(answerButton1, node.answers[0]);
        }
 
        if (node.answers.Count > 1)
        {
            answerText2.text = node.answers[1].answer_text;
            SetupButton(answerButton2, node.answers[1]);
        }
    }
 
    public void HideAnswerButtons()
    {
        answerButton1.gameObject.SetActive(false);
        answerButton2.gameObject.SetActive(false);
    }
 
    void SetupButton(Button button, Answer answer)
    {
        button.onClick.RemoveAllListeners(); // clear the previous node's click
 
        string nextID = answer.nextID;
        button.onClick.AddListener(() => ShowNode(nextID)); //I still don't really get how this works 
    }
}