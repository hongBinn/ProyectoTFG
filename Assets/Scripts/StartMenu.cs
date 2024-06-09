using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // M�todo para cargar la escena de selecci�n de nivel.
    public void OnButtonStartGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    // M�todo para salir de la aplicaci�n.
    public void OnButtonQuit()
    {
        Application.Quit();
    }

    // M�todo para cargar la escena del men� principal.
    public void OnButtonMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    // M�todo para cargar la escena del primer nivel.
    public void OnButtonStage1()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
