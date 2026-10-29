using UnityEngine;
/*
public class InterviewController : MonoBehaviour
{
    //reputationcalc(realmdecisionthat player made)
    public static InterviewController interviewControllerInstance;

    void Awake()
    {
        if(interviewControllerInstance == null)
        {
            interviewControllerInstance = this;
        }
        else {
            Destroy(gameObject); //ensures no duplicates :)
        }
    }

    //PSEUDOCODE!!!
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        //reputationcalc(realmdecisionthat player made)

        //currentCharacter from daycycle

        //dialogue manager to start character cutscene

        //judgment button is clicked //result
        //update stats
        //we're done, day cycle get the next guy

    //method
        //updatestats.updateinfo()

    public void UpdateStats(Realm chosenRealm) {

        GameManager gm = GameManager.Instance;
        DayCycle dayCycle = DayCycle.dayCycleInstance;

        CharacterObjectScript currentCharacter = dayCycle.currentPerson; 

        // if (currentCharacter.isTengu)
        // {
        //     if (chosenRealm == Realm.Hell)
        //     {
        //         gm.ReputationChange(2);
        //     }
        //     else // heaven or human
        //     {
        //         gm.lifeLost(1);
        //         gm.ReputationChange(-1);
        //         gm.AddIncorrect();
        //     }
        // }
        // else if (currentCharacter.karmicMerit < 33)// BAD
        // {
        //     if (chosenRealm == Realm.Hell)
        //     {
        //         gm.ReputationChange(2);
        //     }
        //     else if (chosenRealm == Realm.Heaven)
        //     {
        //         gm.ReputationChange(-2);

        //     }
        //     else // human
        //     {
        //         gm.ReputationChange(-1);
 
        //     }
        // }
        // else if (currentCharacter.karmicMerit < 67)// MID
        // {
        //     if (chosenRealm == Realm.Human)
        //     {
        //         gm.ReputationChange(2);
        //     }
        //     else // hell or heaven
        //     {
        //         gm.ReputationChange(-1);

        //     }
        // }
        // else // HIGH
        // {
        //     if (chosenRealm == Realm.Heaven)
        //     {
        //         gm.ReputationChange(2);
        //     }
        //     else if (chosenRealm == Realm.Human)
        //     {
        //         gm.ReputationChange(1);

        //     }
        //     else // hell
        //     {
        //         gm.ReputationChange(-3);

        //     }
        // }

        // switch (chosenRealm) //increments soul counter
        // {
        //     case Realm.Heaven:
        //         gm.addHeaven(1);
        //         break;

        //     case Realm.Hell:
        //         gm.addHell(1);
        //         break;

        //     case Realm.Human:
        //         gm.addHuman(1);
        //         break;
                
        //     default:
        //         break;
        // }

    

    }
}


*/