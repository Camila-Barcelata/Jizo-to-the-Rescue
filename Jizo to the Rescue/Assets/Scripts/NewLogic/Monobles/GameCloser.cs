using UnityEngine;

public class GameCloser : MonoBehaviour
{

    public void ExitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
