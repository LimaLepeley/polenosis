using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private PlayerControls playerControls;

    void OnEnable()
    {
        playerControls.Enable();
        playerControls.Base.Pause.started += TogglePauseMenu;
    }

    void OnDisable()
    {
        playerControls.Disable();
        playerControls.Base.Pause.started -= TogglePauseMenu;
    }

    void Awake()
    {
        pauseMenu.SetActive(false);
        playerControls = new PlayerControls();
    }

    private void TogglePauseMenu(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }

    public void MainMenu()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        //Cargar la escena del juego
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void RetryLevel()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        //Cargar la escena del juego
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
