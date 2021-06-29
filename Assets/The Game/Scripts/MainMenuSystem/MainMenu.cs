using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // this is all very self-explanatory.
    public void NewGame()
    {
        SceneManager.LoadScene("Loading Screen");
    }
    
    public void LoadGame()
    {
        OptionMenu.loadData = true;
        SceneManager.LoadScene("The Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}