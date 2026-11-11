using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
 
public class theGAMEMANAGER : MonoBehaviour
{
    public static theGAMEMANAGER Instance;
    public GameState CurrentState;
 
    [SerializeField] theInterviewScontroller interviewController;
 
    public List<CharacterDetailsInspector> characters;
    // [SerializeField] currentPerson;
    public List<CharacterDetailsInspector> peopleVisited;

    //different objs for each day. elem 0 = day1:
    public List<EnmaDialogueObject> enmaDialogueObjects;
 
    public int day;
 
    [Header("Stats")]
    public float reputationValue = 100;
    public int lives = 3;
    public int yokaiSentIncorrectly;
    public int soulsSentToHell;
    public int soulsSentToHuman;
    public int soulsSentToHeaven;
 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
 
    void Start()
    {
        StartDay(); 
    }
 
    void StartDay()
    {
        day++;
        theDayCYCLE.dayCycleInstance.SetPeople(SelectInterviewees()); //this sends in ppl
        SetGameState(GameState.Interview); // the first person's intro cutscene starts here
    }
 
    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Entered state: " + newState); //testingstates
 
        switch (newState)
        {
            case GameState.Introduction:
                // tell the comic to play
                break;
            case GameState.Cutscene:
                // tell Enma's dialogue to play
                SceneManager.LoadScene("DayOverCutscene");
                break;
            case GameState.Interview:
                interviewController.BeginInterview(theDayCYCLE.dayCycleInstance.currentPerson);
                break;
            case GameState.Judgment:
                break;
            case GameState.StatReview:
                theDayCYCLE.dayCycleInstance.FinishedResults();
                break;
            case GameState.GameEnding:
                break;
            case GameState.Gameover:
                break;
        }
    }
 
    public List<CharacterDetailsInspector> SelectInterviewees() //this just adds characters from a list
    {
        List<CharacterDetailsInspector> picks = new List<CharacterDetailsInspector>();
        
        if(day ==1)
        {
            for(int startElement = 0; startElement < 3; startElement ++)
            {
                picks.Add(characters[startElement]);
            }
        } else if (day == 2)
        {
            for(int startElement = 3; startElement < 3; startElement ++)
            {
                picks.Add(characters[startElement]);
            }

        } else if (day ==3)
        {
            for(int startElement = 6; startElement < 3; startElement ++)
            {
                picks.Add(characters[startElement]);
            }
        } else if(day ==4)
        {
            for(int startElement = 9; startElement < 2; startElement ++)
            {
                picks.Add(characters[startElement]);
            }
        }

        return picks;
    }

    //stats <3 <3 <3
 
    //interview controller calls this when a realm button gets clicked
    public void RealmChosen(Realm choice)
    {
        SetGameState(GameState.Judgment);
 
        CharacterDetailsInspector person = theDayCYCLE.dayCycleInstance.currentPerson;
 
        // count where the soul went
        if (choice == Realm.Hell) 
            soulsSentToHell++;
            person.Destination = Realm.Hell;
        if (choice == Realm.Human)
            soulsSentToHuman++;
            person.Destination = Realm.Human;
        if (choice == Realm.Heaven) 
            soulsSentToHeaven++;
            person.Destination = Realm.Heaven;

            //tell tara that we should make characters a queue. or that day cycle shouldn't be handling the current person
    }


 
    //THIS STUFF IS AFTER THINGS ARE OVER <3
    public void AfterJudgment()
    {   if (CurrentState != GameState.Judgment) //yeets it out
            return;
        if (theDayCYCLE.dayCycleInstance.NextPerson())
            SetGameState(GameState.Interview);  //deicdes if its supposed to be another person
        else
            SetGameState(GameState.StatReview); // day over

    }
 
    //
    public void AfterStatReview()
    {
        if (CurrentState != GameState.StatReview) 
            return;
 
        if (lives <= 0)
        {
            SetGameState(GameState.Gameover);
        }
        //this should increment days and do a new cycle select interviews
    }

    public void SwitchToCutscene()
    {
        //called in the button in the stat review menu
        SetGameState(GameState.Cutscene);
    }
}
 
public enum GameState
{
    Introduction, //comic cutscene <3
    Cutscene, //any enma dialogue/intro or after
    Interview,//obvs Judgment, //decision made so animation can play
    StatReview, //after the stats and stuff
    GameEnding, //Overall game ending

    Judgment,//obvs Judgment, //decision made so animation can play
    Gameover //like if you lose all your lives, tengu live lost after stats
}
 
public enum Realm
{
    Hell,
    Human,
    Heaven,
    Unjudged
}

public enum TempleState
{
    GoodTemple,
    MidTemple,
    BadTemple,
}