
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Properties;
using UnityEngine.SceneManagement;

// Runs the order of one interview:
// intro cutscene -> Start Interview button -> nodes
// all marked nodes visited + ending node -> final remarks-> Reset button -> back to the intro
// realm button -> judgment dialogue -> realm animation + Next button
public class theInterviewScontroller : MonoBehaviour
{
    //singleton instance
    theInterviewScontroller interviewControllerInst;

      [SerializeField] private Image JudgementDisplay;

    NPCfile npcfile;

    [Header("Other scripts")]
    [SerializeField] Cutscene cutscenePlayer;
    [SerializeField] DialogueManager dialogueManager;
 
    [Header("Start Interview button")]
    [SerializeField] Button startInterviewButton;
 
    [Header("Reset button after last words")]
    [SerializeField] Button resetButton;
 
    [Header("Judgment Stuff")]
    [SerializeField] Image Realmimage; //placeohlder for the sprites
    [SerializeField] GameObject nextButton;
    [SerializeField] Sprite hellSprite;
    [SerializeField] Sprite humanSprite;
    [SerializeField] Sprite heavenSprite;

    [Header("CaseFile")]
    [SerializeField] CaseFileController caseFile;

    [SerializeField] helpButton helpButton2;  

    public TMP_Text Daytext;
 
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


    void Start()  // <-- NEW METHOD
{
    theGAMEMANAGER.Instance.interviewController = this;
    theGAMEMANAGER.Instance.StartDay();
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
        displayDay();
    }

    public void displayDay()
    {
        Daytext.text = "Day " + theGAMEMANAGER.Instance.day;
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
        switch(stage)
        {
            case Stage.Intro:
                startInterviewButton.gameObject.SetActive(true); // intro is over
                GiveFile();
                break;
            case Stage.FinalRemarks:
                resetButton.gameObject.SetActive(true);  // final remarks are over
                break;

            case Stage.Judgment:
                ShowJudgmentImage(); // judgment dialogue is over
                break;
        }
    }
 
    void StartInterviewClicked()
    {
        startInterviewButton.gameObject.SetActive(false);
        stage = Stage.Nodes;
        dialogueManager.StartDialogue(person);

        Debug.Log("The check");
        if (theDayCYCLE.dayCycleInstance.currentPerson == theDayCYCLE.dayCycleInstance.dayPeople[0])
        {
            helpButton2.StartTutorial();
        }
        

    }
 
    void ResetClicked()
    {
       resetButton.gameObject.SetActive(false);
        stage = Stage.Nodes;
        dialogueManager.StartDialogue(person);
    }

  
    public void JudgementToggleClicked()
    {
        if (JudgementDisplay.gameObject.activeSelf) //checks if its on or not
            JudgementDisplay.gameObject.SetActive(false);
            
        else
            JudgementDisplay.gameObject.SetActive(true);
        
    }

    //called after intro lines are over. i think we should give this a cool sliding animation
    public void GiveFile()
    {
        caseFile.SetCaseFileCharacter(person);
        caseFile.gameObject.SetActive(true);
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
            // Debug.Log("persons Realm" + person.Destination);
            person.Destination = Realm.Hell;
            // Debug.Log("persons Realm" + person.Destination);

            cutscenePlayer.Play(person.hellLines);
            
        }
        else if (choice == Realm.Human)
        {
            person.Destination = Realm.Human;
            cutscenePlayer.Play(person.humanLines);
        }
        else
        {
            person.Destination = Realm.Heaven;
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

    public void settingInInterviewClicked()
    {
        theGAMEMANAGER.Instance.resetGameManager();
        SceneManager.LoadScene("MainMenu");
    }
}


