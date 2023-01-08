using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false; //if game is paused or not
    public GameObject PauseMenuCanvas; //reference to Canvas
    public GameObject AboutCanvas;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused && (AboutCanvas.activeSelf == false) && (gameOverScreen.activeSelf == false) && (victoryScreen.activeSelf == false))
            {
                Play();
                Debug.Log("Resume");
                Debug.Log(AboutCanvas.activeSelf);
            }
            else if(Paused == false && (AboutCanvas.activeSelf == false) && (gameOverScreen.activeSelf == false) && (victoryScreen.activeSelf == false))
            {
                Debug.Log("Stop");
                Stop();
            }
        }
    }

    //click Esc to pause the game
    void Stop()
    {
        PauseMenuCanvas.SetActive(true); //menu appears
        Time.timeScale = 0f; //freeze time
        Paused = true;
    }

    //click Resume button to play the game
    public void Play()
    {
        PauseMenuCanvas.SetActive(false); //menu disappears
        Time.timeScale = 1f; //unfreeze time
        Paused = false;
        EventSystem.current.SetSelectedGameObject(null); //so resume button isn't in selected mode
    }

    //click MainMenu button to get to Main Menu
    public void MainMenu()
    {
        //load Main Menu Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}