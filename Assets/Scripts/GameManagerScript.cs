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

    // Instancia estática del GameManagerScript.
    public static GameManagerScript Instance;

    // Referencias estáticas a dos EnemySpawner.
    private static EnemySpawner enemy1;
    private static EnemySpawner enemy2;

    // Contador de enemigos eliminados y el número total de enemigos que deben ser eliminados.
    private int allEnemyDie = 0;
    private int enemyDieNum = 0;
    private int allEnemy = 16;

    // Texto que muestra el número de enemigos eliminados.
    public Text enemyDieNumText;

    // Escala de tiempo del juego y su valor de respaldo.
    public static float timeScale = 0.01f;
    public static float timeScaleData = 0.01f;

    // Texto que muestra la velocidad del juego.
    public Text speedText;

    // Booleano para verificar si el juego está pausado.
    private bool isPaused = false;

    void Update()
    {
        // Control de la velocidad del juego al presionar la barra espaciadora.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpeedButton();
        }

        // Control del menú de pausa al presionar la tecla 'P'.
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

    // Método para actualizar el número de enemigos eliminados.
    public void UpdateEnemyDieNumText(int num = 0)
    {
        enemyDieNum = enemyDieNum + num;
        enemyDieNumText.text = "" + enemyDieNum;
    }

    private void Awake()
    {
        Instance = this;
        // Inicialización de referencias a los EnemySpawner (esto es incorrecto, ver nota abajo).
        enemy1 = GetComponent<EnemySpawner>();
        enemy2 = GetComponent<EnemySpawner>();
    }

    // Método para finalizar el juego mostrando un mensaje.
    void EndGame(string message)
    {
        ResetTimeScale();
        endUi.SetActive(true);
        endMessage.text = message;
    }

    // Método para indicar que el jugador ha ganado.
    public void Win()
    {
        EndGame("Victory");
    }

    // Método para indicar que el jugador ha fallado.
    public void Failed()
    {
        enemy1.Stop();
        enemy2.Stop();
        EndGame("Failed");
    }


    // Método para contar cuando un enemigo muere. Si todos los enemigos han muerto, el jugador gana.
    public void EnemyDie()
    {
        allEnemyDie++;
        if (allEnemyDie >= 2 && allEnemy == allEnemyDie)
            Win();
    }

    // Método para reiniciar el nivel actual.
    public void OnButtonReplay()
    {
        ResetTimeScale();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método para mostrar o esconder el menú de pausa.
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

    // Método para alternar el estado del menú de ayuda.
    public void ToggleHelpMenu()
    {
        // Verificar el estado actual del menú de ayuda y alternar.
        if (helpUi.activeSelf)
        {
            helpUi.SetActive(false);
        }
        else
        {
            helpUi.SetActive(true);
        }
    }

    // Método para volver al menú principal.
    public void OnButtonBackMenu()
    {
        
        SceneManager.LoadScene("StartScene");
    }

    // Método para cambiar la velocidad del juego.
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
