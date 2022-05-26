using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public bool Active = true;
    public float timeRemaining = 3; //How long until game quits
    public float maxTime = 3; //How long esc is to be held
    public TextToScreen screenText; //UI system from Ed


    public void SetActive(bool newValue)
    {
        Active = newValue;
    }

    private void Start()
    {
        timeRemaining = maxTime; //Set time to full 
        screenText = GetComponent<TextToScreen>(); //Get UI System
        screenText.TextSize = 20; //Set Text size
        screenText.SetSize(new Vector2(900, 100)); 
        //Set Text position
        screenText.SetPosition(new Vector2(
            Screen.width * 0.5f,
            Screen.height * 0.5f));
    }

    //Set text contents and colour
    private void SetText()
    {
        if (screenText)
        {
            float percental = timeRemaining / maxTime;
            screenText.SetColour(new Color(1, 0, 0, 1 - percental));
            screenText.TextString = "Hold [Esc] to Quit";
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Make text quickly visible. skip
            float startPoint = 0.4f;
            if (timeRemaining > maxTime * startPoint )
            {
                timeRemaining = maxTime * startPoint;
            }
            //Reduce hold time
            timeRemaining -= Time.deltaTime;

            //Quit if held for full time
            if (timeRemaining <= 0)
            {
                Application.Quit();
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Quit!");
                }
            }
            
        }
        //Escape is not being held. Refill held time
        else
        {
            timeRemaining += Time.deltaTime * 10;
            if (timeRemaining > maxTime)
            {
                timeRemaining = maxTime;
            }
        }
        SetText();
       
    }   

}
    

