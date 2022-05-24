using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool isGamePaused = false;
    
    {SerializedField } GameObject pauseMenu;
    void Update()
    {
        if (input.GetKeyDown(keyCode.Escape))
        {
            isGamePausef (isGamePaused)
                {
                ResumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }
    public void ResumeGame()
    {
    pauseMenu.SetActive(false);
    Time.timescale = 1f;
    isGamePaused = true;
}

    void PauseGame()
    {
    pauseMenu.SetActive(true);
    Time.timescale = 0f;
    isGamePaused = true;
    }
    public void LoadMenu()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timescale = 1f;
}

    public void QuitGame()
{
    Application.Quit()


    Debug.log("Quit");
}
}
