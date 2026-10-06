using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterObjectScript", menuName = "Scriptable Objects/CharacterObjectScript")]
public class CharacterObjectScript : ScriptableObject
{
    //base and mouth sprites should be assigned in the same elements. 
    //e.g. element 0 of baseSprites should be neutralBase(or neutralSprite)
    //       and element 0 of mouthSprites should be neutralMouth
    [SerializeField] public List<Sprite> baseSprites;
    [SerializeField] public List<Sprite> mouthSprites;

    //dialogue the person says --top options
    // (Element 0 is their Greeting dialogue, 
    // element 1 is their condemnation dialogue, 
    // element2 is their pure land dialogue,
    // element3 is their reincarnation dialogue) 
    [SerializeField] public string[] topOptionDialogue;
    //dictionary that is made into a dialogue tree/heap
    [SerializeField] public Dictionary<string, string[]> thingsPersonSays;

    public float karmicMerit;
}
