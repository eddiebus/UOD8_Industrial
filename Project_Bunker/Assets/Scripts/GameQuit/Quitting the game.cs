using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quittingthegame : MonoBehaviour
{
   
    public float timeRemaining = 3;
    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && (timeRemaining > 0))
        {
            timeRemaining -= Time.deltaTime;

        }

        else
        {
            Application.Quit();
            Debug.Log("Thanks for playing");
        }
    }   

}
    

