using UnityEngine;
using System;
using System.Collections.Generic;

public class DialogueTree : MonoBehaviour
{
    public string headChoiceAnswer;
    public string option1Answer;
    public string option2Answer;
    public string option3Answer;
    public string reaction1Answer;
    public string reaction2Answer;
    public string reaction3Answer;
    public string reaction4Answer;
    public string reaction5Answer;
    public string reaction6Answer;

    //connection strings or what the UI buttons say
    public string option1ConnectionStr;
    public string option2ConnectionStr;
    public string option3ConnectionStr;
    public string reaction1ConnectionStr;
    public string reaction2ConnectionStr;
    public string reaction3ConnectionStr;
    public string reaction4ConnectionStr;
    public string reaction5ConnectionStr;
    public string reaction6ConnectionStr;

    void Start()
    {
        //INITIALIZE CHOICES :))
        Choice headChoice = new Choice(headChoiceAnswer);
        Choice option1 = new Choice(option1Answer);
        Choice option2 = new Choice(option2Answer);
        Choice option3 = new Choice(option3Answer);
        Choice reaction1 = new Choice(reaction1Answer);
        Choice reaction2 = new Choice(reaction2Answer);
        Choice reaction3 = new Choice(reaction3Answer);
        Choice reaction4 = new Choice(reaction4Answer);
        Choice reaction5 = new Choice(reaction5Answer);
        Choice reaction6 = new Choice(reaction6Answer);

        //CREATE CONNECTIONS
        Connect(headChoice, option1, option1ConnectionStr);
        Connect(headChoice, option2, option2ConnectionStr);
        Connect(headChoice, option3, option3ConnectionStr);
        Connect(option1, reaction1, reaction1ConnectionStr);
        Connect(option1, reaction2, reaction2ConnectionStr);
        Connect(option2, reaction3, reaction3ConnectionStr);
        Connect(option2, reaction4, reaction4ConnectionStr);
        Connect(option3, reaction5, reaction5ConnectionStr);
        Connect(option3, reaction6, reaction6ConnectionStr);

    }

    void Update()
    {
        
    }

    public static bool Connect(Choice c1, Choice c2, string connection) 
    {
        if(c1.choices.Contains(c2))
        {
            return false;
        }
        c1.choices.Add(c2);
        c1.connectionStrings.Add(connection);
        return true;
    }

    public class Choice : MonoBehaviour{
        public string answer;
        public List<Choice> choices;
        public List<string> connectionStrings;

        public Choice(string answer) {
            this.answer = answer;
            choices = new List<Choice>();
            connectionStrings = new List<string>();
        }
    }
}
