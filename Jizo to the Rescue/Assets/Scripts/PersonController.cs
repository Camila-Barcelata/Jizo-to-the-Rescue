using UnityEngine;

public class PersonController : MonoBehaviour
{

    //--------<3333 SPRITE DECLARATION <3333-----
    //base and mouth sprites should be assigned in the same elements. 
    //e.g. element 0 of baseSprites should be neutralBase(or neutralSprite)
    //       and element 0 of mouthSprites should be neutralMouth
    [SerializeField] private Sprite[] baseSprites;
    [SerializeField] private Sprite[] mouthSprites;
    public Sprite neutralBase;
    public Sprite sadBase;
    public Sprite angryBase;
    public Sprite happyBase;
    public Sprite neutralMouth;
    public Sprite sadMouth;
    public Sprite angryMouth;
    public Sprite happyMouth;

    //idk exactly how this will b implemented, just know that this will
    //      eventually affect the player's reputation depending on where
    //      they send the person.
    public float moralPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //might have to add "new" 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
