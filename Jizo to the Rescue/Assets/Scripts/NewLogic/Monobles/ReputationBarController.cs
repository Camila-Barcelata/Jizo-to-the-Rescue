using UnityEngine;
using UnityEngine.UI;

public class ReputationBarController : MonoBehaviour
{
    public Slider slider;

    void Update()
    {
        float reputationValue = theGAMEMANAGER.Instance.reputationValue;

        //this is for testing
        if(theGAMEMANAGER.Instance.CurrentState == GameState.Judgment)
        {
            reputationValue = 40;
        }

        slider.value = (reputationValue)/100;
        
    }
}
