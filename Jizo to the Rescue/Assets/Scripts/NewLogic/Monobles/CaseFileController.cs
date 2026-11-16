using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseFileController : MonoBehaviour
{
    //display all the information of person
    [SerializeField] CharacterDetailsInspector character;

    CaseFile caseFile;

    [SerializeField]Image portraitImage;
    [SerializeField] TMP_Text textBox;

    void Update()
    {
        if(character != null)
        {
            caseFile = character.caseFile;
            portraitImage.sprite = caseFile.portrait;

            string displayStr = caseFile.characterName + "\n"+ "Age: " + 
            caseFile.characterAge + "\n" +"Karmic Merit " + caseFile.karmicMerit
             + "\n" + caseFile.reasons;

            textBox.text = displayStr;
        }
    }

    public void SetCaseFileCharacter(CharacterDetailsInspector aCharacter)
    {
        character = aCharacter;
    }
}
