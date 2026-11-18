using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnmaDialogueObject", menuName = "Scriptable Objects/EnmaDialogueObject")]
public class EnmaDialogueObject : ScriptableObject
{
    [Header("Day Over Cutscene lines")]
    public List<CutsceneLine> introLines = new(); //always plays at the beginning (e.g. a greeting)
    public List<CutsceneLine> finalLines = new(); //always plays at the end (e.g. "Good luck!")
 
    public List<CutsceneLine> person1BadLines = new(); //hell
    public List<CutsceneLine> person1GoodLines = new(); //human/heaven
    public List<CutsceneLine> person2BadLines = new(); 
    public List<CutsceneLine> person2GoodLines = new(); 
    public List<CutsceneLine> person3BadLines = new(); 
    public List<CutsceneLine> person3GoodLines = new();


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
