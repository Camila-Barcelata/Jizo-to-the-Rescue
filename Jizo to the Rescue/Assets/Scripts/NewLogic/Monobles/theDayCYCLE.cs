using UnityEngine;
using System.Collections.Generic;
 
public class theDayCYCLE : MonoBehaviour
{
    public static theDayCYCLE dayCycleInstance;
 
    public CharacterDetailsInspector currentPerson; // the person that is currently being reincarnated
    int currentPersonIndex;
    public List<CharacterDetailsInspector> dayPeople; // people interviewed in a day
 
    void Awake()
    {
        if (dayCycleInstance == null)
        {
            dayCycleInstance = this;
        }
        else
        {
            Destroy(gameObject); // ensures no duplicates :)
            return;
        }
    }
 
    // GameManager calls this at the start of a day
    public void SetPeople(List<CharacterDetailsInspector> people)
    {
        if (people.Count == 0) 
            return; // nobody to interview
 
        dayPeople = people;
        currentPersonIndex = 0;
        currentPerson = dayPeople[0];
    }
 
    // GameManager calls this after each judgment
    public bool NextPerson()
    {
        currentPersonIndex++;
 
        if (currentPersonIndex >= dayPeople.Count)
        {
            return false; // day finished
        }
 
        currentPerson = dayPeople[currentPersonIndex];
        return true;
    }
}