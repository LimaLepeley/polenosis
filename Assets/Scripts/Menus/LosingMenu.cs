using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerDetection playerDetection;
    [SerializeField]
    private GameObject losingMenu;

    void Update()
    {
        if(playerDetection == null)
        {
            Debug.LogError("PlayerDetection no está asignado en el inspector.");
            return;
        }

        if(playerDetection.IsDetected())
        {
            //Mostrar el menu de perder
            losingMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        //Cargar la escena del juego
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        //Cargar la escena del juego
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
