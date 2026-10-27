using UnityEngine;
using System.Collections.Generic;

public class PersonController : MonoBehaviour
{
    //These are the bools that will be updated, not the characterScriptableObject itself
    public bool isAppealing;
    public bool hasBeenJudged;
    public bool sentToHeaven;
    public bool sentToHuman;
    public bool sentToHell;

    public void UpdateJudgement(Realm judgedRealm)
    {
        //judgementInt ranges from 0-2 (or up to 3 if we implement pure land feature)
        
        //update person's judgement
        //pass in person to 
    }
}
