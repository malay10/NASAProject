using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu;

    public AudioMixer audioMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))// Escape Down
        {
            if (isPaused) //when game is paused
            {
                Resume();
            }
            else // when not paused
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0; //freezing all the activity
            }
        }

        if (Time.timeScale == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }

    
    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1; //resume time in game
    }

    public void VolumeChange(float setVolume)
    {
        audioMixer.SetFloat("Volume", setVolume);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CursorInMenu()
    {
       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
