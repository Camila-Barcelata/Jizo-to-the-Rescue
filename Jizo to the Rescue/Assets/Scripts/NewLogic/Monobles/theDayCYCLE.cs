using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
 
public class theDayCYCLE : MonoBehaviour
{
    public static theDayCYCLE dayCycleInstance;
 
    public CharacterDetailsInspector currentPerson; // the person that is currently being reincarnated
    int currentPersonIndex;
    public List<CharacterDetailsInspector> dayPeople; // people interviewed in a day



    //for the results page after a day's work
    [SerializeField] GameObject statsReviewPanel;
    [SerializeField] TMP_Text statsText;
    [SerializeField] TMP_Text reputationValText;
    [SerializeField] TMP_Text dayOverText;

    public Button purebutton;

    


    void Awake()
        {
            dayCycleInstance = this;
        }

    public void FinishedResults()
    {
        statsReviewPanel.SetActive(true);
        dayOverText.text = "Day " + theGAMEMANAGER.Instance.day + " over!";
        reputationValText.text = "Reputation left: " + theGAMEMANAGER.Instance.reputationValue;
        statsText.text = "People reincarnated to the Hell Realm: " + theGAMEMANAGER.Instance.soulsSentToHell +  
                        "\nPeople reincarnated back as a Human: " + theGAMEMANAGER.Instance.soulsSentToHuman + 
                        "\nPeople reincarnated into the Pure Land: " + theGAMEMANAGER.Instance.soulsSentToHeaven;          
    }

    // GameManager calls this at the start of a day
    public void SetPeople(List<CharacterDetailsInspector> people)
{
    if (purebutton != null)
        purebutton.gameObject.SetActive(theGAMEMANAGER.Instance.day >= 2);

    if (people.Count == 0)
        return;

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

