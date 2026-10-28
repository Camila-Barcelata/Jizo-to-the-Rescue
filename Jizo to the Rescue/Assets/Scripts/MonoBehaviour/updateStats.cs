using UnityEngine;

public class UpdateStats: MonoBehaviour
{

    public void updateInfo(Realm chosenRealm, CharacterObjectScript currentcharacter) {

        GameManager gm = GameManager.Instance;

        // if (currentcharacter.isTengu)
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
        // else if (currentcharacter.karmicMerit < 33)// BAD
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
        // else if (currentcharacter.karmicMerit < 67)// MID
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


//take in current character
    //realm character got sent to

        /*
    
        if (currentcharacter.isTengu){
            //if realm is hell
                //gain +2
            //if realm is heaven
                //life lost
                //lose -1
            //if realm is human
                //life lost
                //lose -1
        }
        else if currentcharacter.karmicMerit>1 and currentcharacter.karmicMerit <32 {
             // if karmic merit = 1-32 //BAD
            //if realm is hell
                //gain +2
            //if realm is heaven
                //lose -2 
            //if realm is human
                //lose -1
        }
        else if currentcharacter.karmicMerit>33 and currentcharacter.karmicMerit <66 {
            // if karmic merit = 33-66 //MID
            //if realm is hell
                //lose -5
            //if realm is heaven
                //lose -5 
            //if realm is human
                //gain +2   
        }
        else if currentcharacter.karmicMerit>67 and currentcharacter.karmicMerit < 100{
            // if karmic merit = 67-100 //HIGH
            //if realm is hell
                //lose -15
            //if realm is heaven
                //gain +10 
            //if realm is human
                //gain +5     
        }   

    
        */