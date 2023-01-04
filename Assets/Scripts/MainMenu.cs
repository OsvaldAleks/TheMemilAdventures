using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //click Play to load game screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        //Quit game
        Application.Quit();
        Debug.Log("Player quit the game");
        EventSystem.current.SetSelectedGameObject(null); //so quit button isn't in selected mode
    }
}