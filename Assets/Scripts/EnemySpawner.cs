using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Contador est�tico para el n�mero de enemigos vivos.
    public static int countEnemyAlive = 0;
    // Array de oleadas (waves) que contienen informaci�n sobre los enemigos que se generar�n.
    public Wave[] waves;
    // Transformaci�n del punto de inicio donde los enemigos se generar�n.
    public Transform START;
    // Tiempo de espera entre oleadas.
    public float waveRate = 0.2f;
    // Referencia a la corrutina que se ejecuta para generar los enemigos.
    public Coroutine coroutine;

    void Start()
    {
        // Inicia la corrutina para generar enemigos y guarda la referencia a la corrutina.
        coroutine = StartCoroutine(SpawnEnemy());
    }

    // M�todo para detener la corrutina que genera enemigos.
    public void Stop()
    {
        // Detiene la corrutina utilizando la referencia almacenada.
        StopCoroutine(coroutine);
    }

    // Corrutina para generar enemigos.
    IEnumerator SpawnEnemy()
    {
        // Itera sobre cada oleada en el array de oleadas.
        foreach (Wave wave in waves)
        {
            // Para cada oleada, genera el n�mero especificado de enemigos.
            for (int i = 0; i < wave.count; i++)
            {
                // Instancia el prefab del enemigo en la posici�n de inicio con la rotaci�n por defecto.
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                // Incrementa el contador de enemigos vivos.
                countEnemyAlive++;
                // Si no es el �ltimo enemigo de la oleada, espera el tiempo especificado antes de generar el pr�ximo.
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate / GameManagerScript.timeScale / 75);
                }
            }
            // Espera hasta que todos los enemigos de la oleada sean eliminados.
            while (countEnemyAlive > 0)
            {
                yield return 0;
            }

            // Espera un tiempo antes de empezar la siguiente oleada.
            yield return new WaitForSeconds(waveRate / GameManagerScript.timeScale / 100);
        }
        // Espera hasta que todos los enemigos sean eliminados al final de todas las oleadas.
        while (countEnemyAlive >= 1)
        {
            yield return 0;
        }
        // Llama al m�todo EnemyDie del GameManagerScript para indicar que todos los enemigos han sido derrotados.
        GameManagerScript.Instance.EnemyDie();
    }
}