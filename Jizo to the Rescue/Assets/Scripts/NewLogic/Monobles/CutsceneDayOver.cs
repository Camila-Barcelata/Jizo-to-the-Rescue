using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

    
// Plays a list of lines, one click at a time. It doesn't know what the lines are for.
public class CutsceneDayOver : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] SpriteRenderer baseSpriteRenderer;
    [SerializeField] SpriteRenderer mouthSpriteRenderer;
    [SerializeField] Button continueButton; // click to go to the next line
    
    //this is only serialized so you can view it in the inspector for debugging:
    [SerializeField] EnmaDialogueObject currEnmaDialogueObject; //scriptable objs that represent enma's
    // dialogue and gets passed in on a day by day basis from game manager

    List<CutsceneLine> lines;
    int index;
    bool gotToFinalLine;

    void Awake()
    {
        Debug.Log("awoke cutscene day over script");
        currEnmaDialogueObject = theGAMEMANAGER.Instance.enmaDialogueObjects[(theGAMEMANAGER.Instance.day)-1];
        lines = currEnmaDialogueObject.introLines;
        continueButton.onClick.AddListener(NextLine);
        gotToFinalLine = false; //reset for each day/scene reload
        //continueButton.gameObject.SetActive(false);
    }

    void Start()
    {
        //if there are intro lines
        if(lines != null)
        {
            Play(lines); //play intro lines
        } 
    }

    void Play(List<CutsceneLine> newLines)
    {
        lines = newLines;
        index = 0;

        // nothing to play, so tell the controller right away
        if (lines == null || lines.Count == 0)
        {
            continueButton.gameObject.SetActive(false); //stops the liesn
            return;
        }

        continueButton.gameObject.SetActive(true); //keeps going with the lienes
        ShowLine();
    }

    void ShowLine()
    {
        CutsceneLine line = lines[index];
        textBox.text = line.character_text;
        baseSpriteRenderer.sprite = line.baseSprite;
        mouthSpriteRenderer.sprite = line.mouthSprite;
    }

    public void NextLine()
    {
        index++;

        if (index >= lines.Count)
            {
                if(gotToFinalLine)
                {
                    GoToInterviewScene();
                    return;
                }
                else
                {
                    //change the lines to be the next lines
                    lines = GetNextCutsceneLines();
                    index = 0;
                }
            }

        ShowLine();
    }

    List<CutsceneLine> GetNextCutsceneLines()
    {
        if(lines == currEnmaDialogueObject.introLines)
        {
            //evaulate 1st person's destination
            if(theDayCYCLE.dayCycleInstance.dayPeople[0].Destination == Realm.Hell)
            {
                return currEnmaDialogueObject.person1BadLines;
            } else
            {
                return currEnmaDialogueObject.person1GoodLines;
            }
        }
        else if(lines == currEnmaDialogueObject.person1GoodLines || lines == currEnmaDialogueObject.person1BadLines) 
        {
            //evaluate 2nd person's destination
            if(theDayCYCLE.dayCycleInstance.dayPeople[1].Destination == Realm.Hell)
            {
                return currEnmaDialogueObject.person2BadLines;
            } else
            {
                return currEnmaDialogueObject.person2GoodLines;
            }
        }
        else if (lines == currEnmaDialogueObject.person2GoodLines || lines == currEnmaDialogueObject.person2BadLines)
        {
            //evaluate 3rd person's destination
            if(theDayCYCLE.dayCycleInstance.dayPeople[2].Destination == Realm.Hell)
            {
                return currEnmaDialogueObject.person3BadLines;
            } else
            {
                return currEnmaDialogueObject.person3GoodLines;
            }
        }
        else if(lines == currEnmaDialogueObject.person3GoodLines || lines == currEnmaDialogueObject.person3BadLines)
        {
            gotToFinalLine = true;
            return currEnmaDialogueObject.finalLines;
        }
        Debug.Log("returned null cutscene lines");
        return null;
    }

    void GoToInterviewScene()
    {
        continueButton.gameObject.SetActive(false);
        theGAMEMANAGER.Instance.day++;
        SceneManager.LoadScene("ShrineInterview");
    }
}
