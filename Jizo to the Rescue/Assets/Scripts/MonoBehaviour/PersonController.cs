using UnityEngine;
using System.Collections.Generic;

public class PersonController : MonoBehaviour
{

    //------SPRITE DECLARATION-----<3
        //base and mouth sprites should be assigned in the same elements. 
        //e.g. element 0 of baseSprites should be neutralBase(or neutralSprite)
        //       and element 0 of mouthSprites should be neutralMouth
        [SerializeField] public List<Sprite> baseSprites;
        [SerializeField] public List<Sprite> mouthSprites;

        //this will correlate to which sprites get displayed at which nodes!
        [SerializeField] public List<int> baseSpriteForEachNode;
        [SerializeField] public List<int> mouthSpriteForEachNode;

    //-----DIALOGUE------<3
        //realmDialogue--dialogue the person says in response to which realm they've been reincarnated to
        // element 0 = condemnation dialogue, (hell)
        // element 1 = reincarnation dialogue, (human)
        // element 2 = heaven dialogue) 
        [SerializeField] public List<string> realmDialogue;

        //main dialogue tree dictionary
        [SerializeField] public Dictionary<string, string[]> thingsPersonSays;

        //ENDING DIALOGUE--dialogue that displays after all valid nodes have been traveled to
        [SerializeField] public List<string> endingDialogue;

    //-----KARMIC MERIT/MECHANICS-----<3
        //idk exactly how this will b implemented, just know that this will
        //      eventually affect the player's reputation depending on where
        //      they send the person.
        public float karmicMerit;
    
    void Start()
    {
        //always display a default base sprite + 
        //  default mouth sprite combo so that character doesn't just "disappear"
    }
}
