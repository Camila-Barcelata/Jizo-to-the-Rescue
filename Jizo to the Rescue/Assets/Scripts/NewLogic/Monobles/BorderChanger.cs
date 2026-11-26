using System.Globalization;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BorderChanger : MonoBehaviour

{


    [SerializeField] List<Sprite> borderSprites;

    [SerializeField] Image border;
    void Update()
    {

        if(theGAMEMANAGER.Instance.CurrentState == GameState.Judgment)
        {
            float rep = theGAMEMANAGER.Instance.reputationValue;

            if(rep >= 75)
            {
                border.sprite = borderSprites[0];
            }
            else if (rep >= 35 && rep <= 75)
            {
                border.sprite = borderSprites[1];
            }
            else 
            {
                border.sprite = borderSprites[2];
            }
            
        }
    }
        
    
}
