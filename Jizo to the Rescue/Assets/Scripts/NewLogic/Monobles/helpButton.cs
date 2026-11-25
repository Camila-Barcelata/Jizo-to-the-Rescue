using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;
using Unity.Hierarchy;
using Unity.VisualScripting;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;



public class helpButton: MonoBehaviour


{   

    [SerializeField] List<TutorialSet> tutorialsByDay; 
    List<TutorialSegment> segments;   
    [SerializeField] RectTransform tutorialPanel;
    [SerializeField] RectTransform arrowTutorial;
    [SerializeField] TMP_Text textBox;

    [SerializeField] GameObject helpButtonObject; 

    int segmentIndex; 
    int lineIndex; 


    public void StartTutorial()
{
    int dayIndex = theGAMEMANAGER.Instance.day - 1;

    //no tutorial day 3 and day 4 i imagine
    if (dayIndex >= tutorialsByDay.Count ||
    tutorialsByDay[dayIndex].segments == null ||
    tutorialsByDay[dayIndex].segments.Count == 0)
    {
    helpButtonObject.SetActive(false);
    return;
    }

    segments = tutorialsByDay[dayIndex].segments;
    segmentIndex = 0;
    lineIndex = 0;
    helpButtonObject.SetActive(false);
    tutorialPanel.gameObject.SetActive(true);
    ShowCurrent();
}
    void ShowCurrent()
    {
        TutorialSegment currSeg = segments[segmentIndex]; //da current tutorialsegment

        tutorialPanel.anchoredPosition = currSeg.coords; //coords yay

        //arrowTutorial.anchoredPosition = currSeg.arrowcoords;
        //arrowTutorial.localRotation = Quaternion.Euler(0f, 0f, currSeg.arrowrotation);

        textBox.text = currSeg.lines[lineIndex];

        


        foreach (TutorialSegment s in segments)
        {
            if (s.highlight == null)
                continue;


            if (s == currSeg)
            {
                s.highlight.SetActive(true);
            }
            else
            {
                s.highlight.SetActive(false);  //should disable all the other highlights
            }
        }
    }

    public void NextButtonClicked()
    {

        
        if (segmentIndex >= segments.Count){
            Debug.Log("hit this, ignoring click");
            return;
        }
        lineIndex++;

        if (lineIndex >= segments[segmentIndex].lines.Count) 
        {
            
            segmentIndex++;
            lineIndex = 0;

            if (segmentIndex >= segments.Count)
            {
                EndTutorial();
                return;
            }
        }

        ShowCurrent();
    }

    void EndTutorial()
    {
        foreach (TutorialSegment s in segments)
        {
            if (s.highlight != null)
                s.highlight.SetActive(false);
        }
        tutorialPanel.gameObject.SetActive(false);

        helpButtonObject.SetActive(true); 
    }

}

[Serializable]

public class TutorialSegment
{
    public List<string> lines;
    public Vector2 coords; // where the panel moves to
    public GameObject highlight;

    //public Vector2 arrowcoords; //where arrow moves

    //public float arrowrotation; //where arrow points

}

[Serializable]
public class TutorialSet
{
    public List<TutorialSegment> segments;
}

 