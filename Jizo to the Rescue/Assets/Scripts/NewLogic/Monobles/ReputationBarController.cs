using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.XR.GoogleVr;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class ReputationBarController : MonoBehaviour
{
    public Slider slider;

    bool judgmentApplied = false;

    public enum Tier
    {
        High, Mid, Low
    }


     

    void Update()
    {
        float reputationValue = theGAMEMANAGER.Instance.reputationValue;

        if(theGAMEMANAGER.Instance.CurrentState == GameState.Judgment)
        {
            if (!judgmentApplied) //added this just so it doesn't constantly update (just updates during judgment)
            {
                judgmentApplied = true;

                float NPCrep = theDayCYCLE.dayCycleInstance.currentPerson.karmicMerit;

                Realm NPCrealm = theDayCYCLE.dayCycleInstance.currentPerson.Destination; 

                float negativeMultiplier = GetMultiplier(NPCrep,NPCrealm);

                theGAMEMANAGER.Instance.reputationValue = reputationValue + (NPCrep/2 * negativeMultiplier);

            }
        }
        else
        {
            judgmentApplied = false; 
        }
        float newrepvalue = theGAMEMANAGER.Instance.reputationValue;
        if (newrepvalue > 101) {
            newrepvalue = 100;
        }

        slider.value = newrepvalue/100;
        theGAMEMANAGER.Instance.reputationValue = newrepvalue;

    }

    public float GetMultiplier(float NPCrep, Realm NPCrealm)
    {

        bool TenguStatus = theDayCYCLE.dayCycleInstance.currentPerson.Tengu;
        
        if (TenguStatus){
            return 0;
        }

        Tier tier = NPCTier(NPCrep);

    
        if (NPCrealm == Realm.Human)
        {
            if(tier == Tier.Low){ //CHANGE LATER
                return -0.5f;
            }
            else if(tier == Tier.Mid)
                return 1;
            else if(tier == Tier.High)
                return -0.5f;
            else 
                return 0;
        }
        if (NPCrealm == Realm.Hell)
        {
            if(tier == Tier.Low)
                return 1;
            else if(tier == Tier.Mid)
                return -0.5f;
            else if(tier == Tier.High)
                return -1;
            else 
                return 0;
        }

        if (NPCrealm == Realm.Heaven)
        {
            if(tier == Tier.Low)
                return -1;
            else if(tier == Tier.Mid)
                return 0.5f;
            else if(tier == Tier.High)
                return 1;
            else 
                return 0;
        }
        return 0;    
    }
    

    public Tier NPCTier(float NPCrep)
    {
        if (NPCrep>0 && NPCrep < 33){
            return Tier.Low;      
        }
        if (NPCrep>=33 && NPCrep < 67){
            return Tier.Mid;      
        }
        if (NPCrep>=67 && NPCrep < 100){
            return Tier.High;      
        }
        return Tier.Low;
    }


}

    
