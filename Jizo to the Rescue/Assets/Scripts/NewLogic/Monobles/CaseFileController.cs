using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaseFileController : MonoBehaviour
{
    //display all the information of person
    CharacterDetailsInspector character;

    CaseFile caseFile;

    [SerializeField] TMP_Text textBox;

    void Update()
    {
        if(character != null)
        {
            caseFile = character.caseFile;

            string displayStr = caseFile.characterName + "\n\n"+ "Age: " + 
            caseFile.characterAge + "\n" +"Karmic Merit: " + caseFile.karmicMerit
             + "\n" + caseFile.reasons;

            textBox.text = displayStr;
        }
    }

    public void SetCaseFileCharacter(CharacterDetailsInspector aCharacter)
    {
        character = aCharacter;
    }
}
