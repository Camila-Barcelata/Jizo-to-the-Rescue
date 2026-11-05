using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnmaDialogueObject", menuName = "Scriptable Objects/EnmaDialogueObject")]
public class EnmaDialogueObject : ScriptableObject
{
    [Header("Day Over Cutscene lines")]
    public List<CutsceneLine> introLines = new(); //always plays at the beginning (e.g. a greeting)
    public List<CutsceneLine> finalLines = new(); //always plays at the end (e.g. "Good luck!")
    public List<CutsceneLine> goodReputationLines = new(); //if you have good reputation
    public List<CutsceneLine> midReputationLines = new();
    public List<CutsceneLine> badReputationLines = new(); 
    public List<CutsceneLine> pureLandTipLines = new(); //only plays on day 1, is empty on other days

    [Header("Pop-Up Tutorial Dialogue")]
    //each "node" has an icon, the text, and the associated UI object
    public List<PopUpNode> tutorialLines = new();

    public class PopUpNode
    {
        public string tutorialText;
        public Sprite tutorialIcon;
        public GameObject UIpointedAt;
    }


}
