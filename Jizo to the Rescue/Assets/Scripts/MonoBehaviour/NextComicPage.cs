using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;


public class NextComicPage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] pages;
    int pagesIndex = 0;

    //j = 1 in inspector, only there to increment through 
    public void SetNextPageActive() 
    {
        //does NOT set page1 to active, yes this is the intended outcome
        try
        {
            pagesIndex ++;
            pages[pagesIndex].SetActive(true);
        }
        catch(IndexOutOfRangeException indexOutOfRangeException)
        {
            //if we've reached end of array, load next scene
            SceneManager.LoadScene("ShrineInterview");
        }
    }
}
