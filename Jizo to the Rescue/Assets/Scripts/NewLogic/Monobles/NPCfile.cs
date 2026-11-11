
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;



public class NPCfile : MonoBehaviour
{

    [SerializeField] private Image scrollDisplay;

    [SerializeField] private TMP_Text textbox;
    CharacterDetailsInspector person = theDayCYCLE.dayCycleInstance.currentPerson;

    public void NPCfileDisplay()
    {
        if (scrollDisplay.gameObject.activeSelf) //checks if its on or not
            scrollDisplay.gameObject.SetActive(false);
        else
            scrollDisplay.gameObject.SetActive(true);
            NPCtextdisplay();
        

        
    }
    public void NPCtextdisplay()
    {
        textbox.text = "Lady Yanagiya";
    }



}
