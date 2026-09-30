using UnityEngine;
using System.Collections.Generic;

public class PersonController : MonoBehaviour
{

    //--------<3333 SPRITE DECLARATION <3333-----
    //base and mouth sprites should be assigned in the same elements. 
    //e.g. element 0 of baseSprites should be neutralBase(or neutralSprite)
    //       and element 0 of mouthSprites should be neutralMouth
    [SerializeField] private Sprite[] baseSprites;
    [SerializeField] private Sprite[] mouthSprites;

    //dialogue the person says --top options
    // (Element 0 is their Greeting dialogue, 
    // element 1 is their condemnation dialogue, 
    // element2 is their pure land dialogue,
    // element3 is their reincarnation dialogue) 
    [SerializeField] public string[] topOptionDialogue;


    //make an empty dictionary
    [SerializeField] public Dictionary<string, string[]> thingsPersonSays;
    //[SerializeField] public List<KeyValuePairs<string, string[]>> thingsPersonSays;


    //idk exactly how this will b implemented, just know that this will
    //      eventually affect the player's reputation depending on where
    //      they send the person.
    public float moralPoints;


    void Start()
    {
        //always display a default base sprite + 
        //  default mouth sprite combo so that character doesn't just "disappear"
    }

    
    void Update()
    {
    }

}
