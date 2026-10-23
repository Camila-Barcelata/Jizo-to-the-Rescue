using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //setting up singleton
    public static GameManager Instance;

    //holds all Person objects
    List<Person> People;
    //global values
    float reputationValue;
    int yokaiSentIncorrectly;
    int soulsSentToHell;
    int soulsSentToHuman;
    int soulsSentToHeaven;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject); //ensures no duplicates :)
        }
    }
    
}
