using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
        switch(templeState)
        {
            case TempleState.GoodTemple:
                Debug.Log("gg");
                templeImageObj.gameObject.SetActive(true);
                templeImageObj.sprite = templeSprites[0];
                break;
            //     templeImageObj = templeImages[0];
            // }
            // case(BadTemple)
            // {
            //     templeImageObj = templeImages[2];
            // }
        }

        // if(reputationVal >= 75) //good standing
        // {
        //     templeImageObj = templeImages[0];
        // }
        // else if (reputationVal >= 35 && reputationVal <= 75)
        // {
        //     templeImageObj = templeImages[1];
        // }
        // else if(reputationVal <= 35)
        // {
        //     templeImageObj = templeImages[2];
        // }
    }

    public void HideTemple()
    {
        templeImageObj.gameObject.SetActive(false);
    }
}
