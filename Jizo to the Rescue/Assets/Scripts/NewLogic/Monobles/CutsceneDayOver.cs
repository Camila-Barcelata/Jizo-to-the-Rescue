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
    [SerializeField] EnmaDialogueObject enmaDialogueObject; //scriptable objs that represent enma's
    // dialogue and gets passed in on a day by day basis from game manager

    List<CutsceneLine> lines;
    int index;

    void Awake()
    {
        enmaDialogueObject = theGAMEMANAGER.Instance.enmaDialogueObjects[(theGAMEMANAGER.Instance.day)-1];
        continueButton.onClick.AddListener(NextLine);
        lines = enmaDialogueObject.introLines;
        //continueButton.gameObject.SetActive(false);
    }

    void Start()
    {
        Play(lines);
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
            continueButton.gameObject.SetActive(false);
            //load the shrine interview scene and increment the day count!
            SceneManager.LoadScene("ShrineInterview");
            theGAMEMANAGER.Instance.day += 1;
            return;
        }

        ShowLine();
    }
}
