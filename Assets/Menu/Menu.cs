using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        Application.LoadLevel(1);
    }
    public void LoadGame()
    {

    }
    public void Settings()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
