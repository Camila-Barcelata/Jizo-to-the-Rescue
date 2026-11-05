using System;
using UnityEngine;
using System.Collections.Generic;
 
[CreateAssetMenu(fileName = "InspectorDictionary", menuName = "Scriptable Objects/CharacterDetailsInspector")]
public class CharacterDetailsInspector : ScriptableObject
{
    [Header("karmicMerit")]
    public float karmicMerit;
 
    [Header("Tengu Status")]
    public bool Tengu;
 
    [Header("Realm")]
    public Realm Destination;
 
    [Header("Cutscene lines")]
    public List<CutsceneLine> introLines = new();// plays before the interview
    public List<CutsceneLine> finalLines = new(); 
    public List<CutsceneLine> hellLines = new(); 
    public List<CutsceneLine> humanLines = new();  
    public List<CutsceneLine> heavenLines = new(); 
 
    [Header("FIRST NODE ID MUST BE IntroductionText also there should be one called ending")]
    [SerializeField] public Dictionary<string, Node> CharacterNodes = new();
}
 
[Serializable]
public class Node
{
    //public List<string> character_text;
    public string character_text;
    public string nodeID;
    public List<Answer> answers = new();
    public bool CountsTowardsEnding;
 
    [Header("Sprites")]
    public Sprite baseSprite;
    public Sprite mouthSprite;
}
 
[Serializable]
public class Answer
{
    public string answer_text;
    public string nextID;
}