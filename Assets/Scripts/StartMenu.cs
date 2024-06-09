using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Método para cargar la escena de selección de nivel.
    public void OnButtonStartGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    // Método para salir de la aplicación.
    public void OnButtonQuit()
    {
        Application.Quit();
    }

    // Método para cargar la escena del menú principal.
    public void OnButtonMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Método para cargar la escena del primer nivel.
    public void OnButtonStage1()
    {
        SceneManager.LoadScene("Stage1Scene");
    }
}
