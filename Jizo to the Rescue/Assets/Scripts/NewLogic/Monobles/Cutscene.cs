using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
[System.Serializable]
public class CutsceneLine
{
    public string character_text;
    public Sprite baseSprite;
    public Sprite mouthSprite;
}
 
// Plays a list of lines, one click at a time. It doesn't know what the lines are for.
public class Cutscene : MonoBehaviour
{
    [SerializeField] TMP_Text textBox;
    [SerializeField] Image baseImage;
    [SerializeField] Image mouthImage;
    [SerializeField] Button continueButton; // click to go to the next line
    [SerializeField] theInterviewScontroller interviewController;
 
    List<CutsceneLine> lines;
    int index;
 
    void Awake()
    {
        continueButton.onClick.AddListener(NextLine);
        continueButton.gameObject.SetActive(false);
    }
 
    public void Play(List<CutsceneLine> newLines)
    {
        lines = newLines;
        index = 0;
 
        // nothing to play, so tell the controller right away
        if (lines == null || lines.Count == 0)
        {
            continueButton.gameObject.SetActive(false); //stops the liesn
            interviewController.CutsceneFinished();
            return;
        }
 
        continueButton.gameObject.SetActive(true); //keeps going with the lienes
        ShowLine();
    }
 
    void ShowLine()
    {
        CutsceneLine line = lines[index];
        textBox.text = line.character_text;
        baseImage.sprite = line.baseSprite;
        mouthImage.sprite = line.mouthSprite;
    }
 
    void NextLine()
    {
        index++;
 
        if (index >= lines.Count)
        {
            continueButton.gameObject.SetActive(false);
            interviewController.CutsceneFinished(); // controller decides what happens next
            return;
        }
 
        ShowLine();
    }
}