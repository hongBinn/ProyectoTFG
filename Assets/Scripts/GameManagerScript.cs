using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    // Referencias a las interfaces de usuario para el final del juego, pausa y ayuda.
    public GameObject endUi;
    public GameObject pauseUi;
    public GameObject helpUi;

    // Referencia al texto del mensaje final.
    public Text endMessage;

    // Instancia est�tica del GameManagerScript.
    public static GameManagerScript Instance;

    // Referencias est�ticas a dos EnemySpawner.
    private static EnemySpawner enemy1;
    private static EnemySpawner enemy2;

    // Contador de enemigos eliminados y el n�mero total de enemigos que deben ser eliminados.
    private int allEnemyDie = 0;
    private int enemyDieNum = 0;
    private int allEnemy = 16;

    // Texto que muestra el n�mero de enemigos eliminados.
    public Text enemyDieNumText;

    // Escala de tiempo del juego y su valor de respaldo.
    public static float timeScale = 0.01f;
    public static float timeScaleData = 0.01f;

    // Texto que muestra la velocidad del juego.
    public Text speedText;

    // Booleano para verificar si el juego est� pausado.
    private bool isPaused = false;

    void Update()
    {
        // Control de la velocidad del juego al presionar la barra espaciadora.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpeedButton();
        }

        // Control del men� de pausa al presionar la tecla 'P'.
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnButtonMenu();
        }

        // Pausar y reanudar el tiempo del juego al presionar la tecla 'T'.
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (timeScale != 0f)
                timeScale = 0f;
            else
                timeScale = timeScaleData;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleHelpMenu();
        }
        // Actualizar el texto de la velocidad del juego.
        if (timeScale == 0.02f)
            speedText.text = "x2";
        else
            speedText.text = "x1";
        if (enemyDieNum == allEnemy)
            Win();
    }

    // M�todo para actualizar el n�mero de enemigos eliminados.
    public void UpdateEnemyDieNumText(int num = 0)
    {
        enemyDieNum = enemyDieNum + num;
        enemyDieNumText.text = "" + enemyDieNum;
    }

    private void Awake()
    {
        Instance = this;
        // Inicializaci�n de referencias a los EnemySpawner (esto es incorrecto, ver nota abajo).
        enemy1 = GetComponent<EnemySpawner>();
        enemy2 = GetComponent<EnemySpawner>();
    }

    // M�todo para finalizar el juego mostrando un mensaje.
    void EndGame(string message)
    {
        ResetTimeScale();
        endUi.SetActive(true);
        endMessage.text = message;
    }

    // M�todo para indicar que el jugador ha ganado.
    public void Win()
    {
        EndGame("Victory");
    }

    // M�todo para indicar que el jugador ha fallado.
    public void Failed()
    {
        enemy1.Stop();
        enemy2.Stop();
        EndGame("Failed");
    }


    // M�todo para contar cuando un enemigo muere. Si todos los enemigos han muerto, el jugador gana.
    public void EnemyDie()
    {
        allEnemyDie++;
        if (allEnemyDie >= 2 && allEnemy == allEnemyDie)
            Win();
    }

    // M�todo para reiniciar el nivel actual.
    public void OnButtonReplay()
    {
        ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // M�todo para mostrar o esconder el men� de pausa.
    public void OnButtonMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseUi.SetActive(true);
            timeScale = 0f;
        }
        else
        {
            pauseUi.SetActive(false);
            timeScale = timeScaleData;
        }
    }

    // M�todo para alternar el estado del men� de ayuda.
    public void ToggleHelpMenu()
    {
        // Verificar el estado actual del men� de ayuda y alternar.
        if (helpUi.activeSelf)
        {
            helpUi.SetActive(false);
        }
        else
        {
            helpUi.SetActive(true);
        }
    }

    // M�todo para volver al men� principal.
    public void OnButtonBackMenu()
    {
        
        SceneManager.LoadScene("StartScene");
    }

    // M�todo para cambiar la velocidad del juego.
    public void OnSpeedButton()
    {
        if (timeScale == 0.01f)
        {
            timeScale += 0.01f;
            timeScaleData = timeScale;
        }
        else if(timeScale == 0.02f)
        {
            timeScale -= 0.01f;
            timeScaleData = timeScale;
        }
    }

    // Restaurar la escala del tiempo al valor por defecto.
    private void ResetTimeScale()
    {
        timeScale = 0.01f;
        timeScaleData = 0.01f;
    }
}
