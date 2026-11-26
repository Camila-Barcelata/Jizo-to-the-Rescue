using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Temple : MonoBehaviour
{
    //when temple button is clicked, a temple image based on your reputation value stats shows up
    //maybe flavor text as well?
    
    TempleState templeState;

    // [SerializeField] List<Sprite> templeImages; //images that show up when clicked. element 0 is good, 1 is mid, etc.
    [SerializeField] List<Sprite> templeSprites;
    [SerializeField] Image templeImageObj;
    public void ShowTemple()
    {
        switch(calculateTempState())
        {
            case TempleState.GoodTemple:
                templeImageObj.gameObject.SetActive(true);
                templeImageObj.sprite = templeSprites[0];
                break;
            case TempleState.MidTemple:
                templeImageObj.gameObject.SetActive(true);
                templeImageObj.sprite = templeSprites[1];
                break;
            case TempleState.BadTemple:
                templeImageObj.gameObject.SetActive(true);
                templeImageObj.sprite = templeSprites[2];
                break;
        }
    }

    public TempleState calculateTempState()
    {
        float reputationVal = theGAMEMANAGER.Instance.reputationValue;
        if(reputationVal >= 75) //good standing
        {
            return TempleState.GoodTemple;
        }
        else if (reputationVal >= 35 && reputationVal <= 75)
        {
            return TempleState.MidTemple;
        }
        else
        {
            return TempleState.BadTemple;
        }

        
    }

    public void HideTemple()
    {
        templeImageObj.gameObject.SetActive(false);
    }
}
