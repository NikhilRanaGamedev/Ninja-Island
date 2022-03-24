using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    bool paused = false;
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame()
    {
        if (!paused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            paused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            paused = false;
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
