using UnityEngine;
using System.Collections.Generic;

public class DayCycle : MonoBehaviour
{

    List<Person> people;
    void Start()
    {
        people = GameManager.instance.People;

    }

    void Update()
    {
        
    }
}
