
using UnityEngine;
using UnityEngine.UI;
 
// Runs the order of one interview:
// intro cutscene -> Start Interview button -> nodes
// all marked nodes visited + ending node -> final remarks-> Reset button -> back to the intro
// realm button -> judgment dialogue -> realm animation + Next button
public class theInterviewScontroller : MonoBehaviour
{
    //singleton instance
    theInterviewScontroller interviewControllerInst;

    [Header("Other scripts")]
    [SerializeField] Cutscene cutscenePlayer;
    [SerializeField] DialogueManager dialogueManager;
 
    [Header("Start Interview button")]
    [SerializeField] Button startInterviewButton;
 
    [Header("Reset button after last words")]
    [SerializeField] Button resetButton;
 
    [Header("Judgmentstuff")]
    [SerializeField] Image Realmimage; //placeohlder for the sprites
    [SerializeField] GameObject nextButton;
    [SerializeField] Sprite hellSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] Sprite heavenSprite;
 
    //lowkey im taking advantage of learning enums
    //so i spilt the interview cycle into them!!
    enum Stage { 
        Intro,
        Nodes, 
        FinalRemarks, 
        Judgment 
    }
    Stage stage;
 
    CharacterDetailsInspector person; //curent person :p
    Realm chosenRealm;  // where dae go
 
    void Awake()
    {
        if (interviewControllerInst == null)
        {
            interviewControllerInst = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        startInterviewButton.onClick.AddListener(StartInterviewClicked); //turns on
        resetButton.onClick.AddListener(ResetClicked); //goes back
 
        //bunch of turning off stuff
        startInterviewButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        Realmimage.gameObject.SetActive(false);
        nextButton.SetActive(false);
    }
 
    // ---------- Interview ---------- <3
 
    // Called by GameManager when it enters GameState.Interview
    public void BeginInterview(CharacterDetailsInspector currentPerson)
    {
        person = currentPerson;
        //Realmimage.gameObject.SetActive(false);
        nextButton.SetActive(false);
        resetButton.gameObject.SetActive(false);
 
        stage = Stage.Intro;
        startInterviewButton.gameObject.SetActive(false);
        dialogueManager.HideAnswerButtons();
        cutscenePlayer.Play(person.introLines);
    }
 
    //Lil cutscene at the end
    public void ReachedEnding()
    {
        stage = Stage.FinalRemarks;
        dialogueManager.HideAnswerButtons(); 
        cutscenePlayer.Play(person.finalLines); 
    }
 
    // Cutscene calls this whenever it runs out of lines
    public void CutsceneFinished() //i lowkey looked up list dialogue systems and how they get displayed
    {
        if (stage == Stage.Intro)
        {
            startInterviewButton.gameObject.SetActive(true); // intro is over
        }
        else if (stage == Stage.FinalRemarks)
        {
            resetButton.gameObject.SetActive(true);  // final remarks are over
        }
        else if (stage == Stage.Judgment)
        {
            ShowJudgmentImage(); // judgment dialogue is over
        }
    }
 
    void StartInterviewClicked()
    {
        startInterviewButton.gameObject.SetActive(false);
        stage = Stage.Nodes;
        dialogueManager.StartDialogue(person);
    }
 
    void ResetClicked()
    {
       resetButton.gameObject.SetActive(false);
        stage = Stage.Nodes;
        dialogueManager.StartDialogue(person);
    }

    //what did the player click
    public void HellButtonClicked()  
     { RealmButtonClicked(Realm.Hell); }
    public void HumanButtonClicked()  
    { RealmButtonClicked(Realm.Human);
     }
    public void HeavenButtonClicked() 
    { RealmButtonClicked(Realm.Heaven);
     }
 
    //send in that choice
    void RealmButtonClicked(Realm choice)
    {
        if (theGAMEMANAGER.Instance.CurrentState != GameState.Interview) 
            return;
 
        chosenRealm = choice;
        stage = Stage.Judgment;
 
        // stop the interview
        startInterviewButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        dialogueManager.HideAnswerButtons();
 
        // set the state to Judgment and update the stats
        theGAMEMANAGER.Instance.RealmChosen(choice);
 
        // play the dialogue for where they were sent
        if (choice == Realm.Hell)
        {
            cutscenePlayer.Play(person.hellLines);
        }
        else if (choice == Realm.Human)
        {
            cutscenePlayer.Play(person.humanLines);
        }
        else
        {
            cutscenePlayer.Play(person.heavenLines);
        }
    }
 
 //show off our potential animations!!
    void ShowJudgmentImage()
    {
        if (chosenRealm == Realm.Hell)
        {
            Realmimage.sprite = hellSprite;
        }
        else if (chosenRealm == Realm.Human)
        {
            Realmimage.sprite = humanSprite;
        }
        else
        {
            Realmimage.sprite = heavenSprite;
        }
 
        Realmimage.gameObject.SetActive(true);
        nextButton.SetActive(true);
    }

    public void NextClicked()
    {
        Realmimage.gameObject.SetActive(false);
        nextButton.SetActive(false);
        theGAMEMANAGER.Instance.AfterJudgment(); // next person, or the stat review
    }
}