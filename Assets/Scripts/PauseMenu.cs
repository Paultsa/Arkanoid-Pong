using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject backUI;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused && !(gameManager.isGameOver()))
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        GameStatus.status.Save();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioSource.pitch = 1f;
    }

    void Pause()
    {
        if(gameManager.isGameOver())
        {
            backUI.SetActive(false);
        }
        else
        {
            backUI.SetActive(true);
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioSource.pitch = 0.5f;    }

    public void LoadMenu()
    {
        GameStatus.status.Save();
        SceneManager.LoadScene("MainMenu");
        Resume();
    }
}
