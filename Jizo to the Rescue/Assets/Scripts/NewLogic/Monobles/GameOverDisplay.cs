using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour
{

    
    [SerializeField] List<DisplayInfo> gameoverDisplays; 

    [SerializeField] TMP_Text textBox;

    [SerializeField] Image background;

    [SerializeField] Button resetButton;

    [SerializeField] Button exitButton;


    // Update is called once per frame
    void Start(){

        if(theGAMEMANAGER.Instance.ExitReason == "tengulives")
        {
            
            DisplayInfo displayOne = gameoverDisplays[0];
            StartCoroutine(DisplayGameOver(displayOne));
        }

        else if(theGAMEMANAGER.Instance.ExitReason == "reputationValue")
        {

            Debug.Log("hit the reputation gameover");
            DisplayInfo displayTwo = gameoverDisplays[1];
            StartCoroutine(DisplayGameOver(displayTwo));
        }
        
    }

    public IEnumerator DisplayGameOver(DisplayInfo gameover)
    {

        Debug.Log("hit the reputatio gameover couroutine");
        background.sprite  = gameover.background;
        foreach (string line in gameover.gameoverlines)
        {

            textBox.text = line;
            
            yield return new WaitForSeconds(3f);
        }

        textBox.gameObject.SetActive(false);
        DisplayButtons();
        
    }

    public void DisplayButtons()
    {
        resetButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        
    }


    [Serializable]
    public class DisplayInfo
    {
         
        public List<string> gameoverlines;
        public Sprite background; 
    }

    public void ResetButtonClicked()
    {
        theGAMEMANAGER.Instance.resetGameManager();
        SceneManager.LoadScene("ShrineInterview");
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
