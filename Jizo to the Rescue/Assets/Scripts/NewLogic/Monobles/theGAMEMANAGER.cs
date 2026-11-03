using UnityEngine;
using System.Collections.Generic;
 
public class theGAMEMANAGER : MonoBehaviour
{
    public static theGAMEMANAGER Instance;
    public GameState CurrentState;
 
    [SerializeField] theInterviewScontroller interviewController;
 
    public List<CharacterDetailsInspector> characters;
    public List<CharacterDetailsInspector> peopleVisited;
 
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
                break;
            case GameState.Interview:
                interviewController.BeginInterview(theDayCYCLE.dayCycleInstance.currentPerson);
                break;
            case GameState.Judgment:
                break;
            case GameState.StatReview:
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
 
        for (int i = 0; i < 3; i++)
        {
            picks.Add(characters[i]);
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
        if (choice == Realm.Human)
            soulsSentToHuman++;
        if (choice == Realm.Heaven) 
            soulsSentToHeaven++;
 
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