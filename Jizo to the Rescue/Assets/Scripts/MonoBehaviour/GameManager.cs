using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //setting up singleton
    public static GameManager Instance;

    //holds all Person objects
    List<CharacterObjectScript> allPeople;
    //global values
    float reputationValue; //6
    int yokaiSentIncorrectly;

    int lives;
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

    public void ReputationChange(int number)
    {
        reputationValue += number;
    }
    
    public void lifeLost (int number)
    {
        lives -= number;
    }

    public void AddIncorrect()
    {
        yokaiSentIncorrectly += 1;
    }

    public void addHeaven(int number)
    {
        soulsSentToHeaven += number;
    }

    public void addHuman(int number)
    {
        soulsSentToHuman += number;
    }

    public void addHell(int number)
    {
        soulsSentToHell += number;
    }

}
