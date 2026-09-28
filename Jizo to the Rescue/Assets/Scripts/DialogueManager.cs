using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Called this the dialogue manager, this will build the tree and instantiate the first


public class DialogueManager : MonoBehaviour
{

    //NPCtext
    public string firstNodeText;

    //NPCanswers
    public string NPCanswer1aText;
    public string NPCanswer1bText;
    public string NPCanswer1cText;
    //NPC judgement texts
    [SerializeField] public string ascensionDialogue; //what person says in response to being ascended
    [SerializeField] public string reincarnationDialogue; 
    [SerializeField] public string condemnationDialogue; 

    //choices
    public string PlayerConnection1; 
    public string PlayerConnection2; 
    public string PlayerConnection3;

    public TMP_Text speechBubbleText;

    public TMP_Text optionButton1Text;
    public TMP_Text optionButton2Text;
    public TMP_Text optionButton3Text;

    private DialogueNode firstNode;
    private DialogueNode currentNode;

    //[SerializeField] public Sprite characterSprite;

    void Start(){
        
        //first chocies will intialize, 

        firstNode = new DialogueNode(firstNodeText);

        DialogueNode Node1a = new DialogueNode(NPCanswer1aText);
        DialogueNode Node1b = new DialogueNode(NPCanswer1bText);
        DialogueNode Node1c = new DialogueNode(NPCanswer1cText);

        Connect(firstNode,Node1a, PlayerConnection1);
        Connect(firstNode,Node1b, PlayerConnection2);
        Connect(firstNode,Node1c, PlayerConnection3);

        //-------------------------------------------------------
        //we'll figure out a way to make the text for the next nodes, either serializes field
        //or putting it in directly (might be easier to just do it directly)

        //this is branch 1
        DialogueNode Node1a2a = new DialogueNode("NPCanswer2aText");
        DialogueNode Node1a2b = new DialogueNode("NPCanswer1bText");
        DialogueNode Node1a2c = new DialogueNode("NPCanswer1cText");

        Connect(Node1a, Node1a2a, "yooooo"); //change text
        Connect(Node1a, Node1a2b, "wewefwef"); 
        Connect(Node1a, Node1a2c, "ladadadadada");

        //-------------------------------------------------------
        
        //this is branch 2 (test end)
        DialogueNode endNode = new DialogueNode("Good bye!!!!");
        DialogueNode endNode1 = new DialogueNode("Good bye!!!!");
        DialogueNode endNode2 = new DialogueNode("Good bye!!!!");

        Connect(Node1a2a, endNode, "bye bye!!!"); //change text
        Connect(Node1a2a, endNode1, "bye byeee!!!"); //change text
        Connect(Node1a2a, endNode2, "bye byeeeeeee!!!"); //change text

        //--------------------------------------------------------
        
        currentNode = firstNode;
        DisplayCurrentNode(); //call display function
        
    }
    

    public static bool Connect(DialogueNode headnode, DialogueNode followingnode, string playerAnswer)
    {
        if (headnode.nextnodes.Contains(followingnode)){
            return false;
        }

        headnode.nextnodes.Add(followingnode);
        headnode.answerschoices.Add(playerAnswer);
        return true;    
        
    }
    
    void DisplayCurrentNode() //then this displays everything

    {
    
        speechBubbleText.text = currentNode.NPCtext;
        optionButton1Text.text = currentNode.answerschoices[0];
        optionButton2Text.text = currentNode.answerschoices[1];
        optionButton3Text.text = currentNode.answerschoices[2];
        //characterSprite = characterSprite

    }
 
    public void SelectOption(int index) //this first


    {
        currentNode = currentNode.nextnodes[index];
        DisplayCurrentNode();   
    }

    
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
    



