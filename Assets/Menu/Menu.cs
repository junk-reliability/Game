using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public void StartGame()
    {
#pragma warning disable CS0618 // Тип или член устарел
        Application.LoadLevel(1);
#pragma warning restore CS0618 // Тип или член устарел
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
