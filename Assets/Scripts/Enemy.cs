using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Velocidad de movimiento del enemigo.
    public float speed = 7;
    // Puntos de vida del enemigo.
    public int lifePoint = 250;
    // Puntos de vida totales del enemigo (para la barra de vida).
    private int totalLife;
    // Array de posiciones a las que se mueve el enemigo.
    private Transform[] positions;
    // Índice actual de la posición a la que se mueve el enemigo.
    private int index = 0;
    // Slider para mostrar la barra de vida del enemigo.
    public Slider lpSlider;
    // Animator para controlar la animación del enemigo.
    private Animator animator;
    // Identificador del camino que sigue el enemigo.
    public int wayID;
    // AudioSource para reproducir sonidos del enemigo.
    private AudioSource enemyAudioSource;

    // Método que se llama antes del primer frame.
    void Start()
    {
        // Obtener el Animator del objeto.
        animator = GetComponent<Animator>();
        // Obtener las posiciones del camino del diccionario de caminos.
        if (Way.ways.ContainsKey(wayID))
        {
            positions = Way.ways[wayID];
        }
        // Guardar los puntos de vida totales.
        totalLife = lifePoint;
        // Ajustar la velocidad de la animación.
        animator.speed = GameManagerScript.timeScale * 100f;
        // Obtener el AudioSource del objeto.
        enemyAudioSource = GetComponent<AudioSource>();
    }

    // Método que se llama una vez por frame.
    void Update()
    {
        // Ajustar la velocidad de la animación.
        animator.speed = GameManagerScript.timeScale * 100f;
        // Si el enemigo está muerto o no tiene posiciones, no se mueve.
        if (lifePoint <= 0 || positions == null) transform.position = transform.position;
        else Move();
    }

    // Método para mover al enemigo.
    void Move()
    {
        // Si el índice es mayor que la cantidad de posiciones, salir.
        if (index > positions.Length - 1) return;
        // Mirar hacia la siguiente posición.
        transform.LookAt(positions[index]);
        // Mover al enemigo hacia la siguiente posición.
        transform.position = Vector3.MoveTowards(transform.position, positions[index].position, speed * GameManagerScript.timeScale);
        // Si el enemigo llega a la siguiente posición, avanzar al siguiente índice.
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f) index++;
        // Si el enemigo llega al final del camino, llamar a ReachDestination().
        if (index > positions.Length - 1) ReachDestination();
    }

    // Método llamado cuando el enemigo alcanza su destino.
    void ReachDestination()
    {
        // Actualizar el contador de enemigos muertos.
        GameManagerScript.Instance.UpdateEnemyDieNumText(1);
        // Destruir el objeto enemigo.
        Destroy(gameObject);
    }

    // Método llamado cuando el enemigo es destruido.
    void OnDestroy()
    {
        // Decrementar el contador de enemigos vivos.
        EnemySpawner.countEnemyAlive--;
    }

    // Método para obtener los puntos de vida del enemigo.
    public int GetLifePoint()
    {
        return lifePoint;
    }

    // Método para aplicar daño al enemigo.
    public void TakeDamage(int dmg)
    {
        // Si el enemigo ya está muerto, salir del método.
        if (lifePoint == 0) return;
        // Aplicar daño al enemigo.
        lifePoint -= dmg;
        // Si el enemigo queda sin puntos de vida...
        if (lifePoint <= 0)
        {
            // Activar la animación de muerte.
            animator.SetTrigger("Die");
            // Actualizar el costo del BuildManager (por ejemplo, cuando el enemigo es destruido, se puede dar una recompensa).
            BuildManager.Instance.UpdateCost(3);
            // Actualizar el contador de enemigos muertos.
            GameManagerScript.Instance.UpdateEnemyDieNumText(1);
            // Si hay un AudioSource, activarlo y reproducir el sonido.
            if (enemyAudioSource != null)
            {
                enemyAudioSource.mute = false;
                enemyAudioSource.Play();
            }
            // Destruir el objeto enemigo después de un tiempo para que la animación se complete.
            Destroy(gameObject, 3f);
        }
        // Actualizar el valor de la barra de vida.
        lpSlider.value = (float)lifePoint / totalLife;
    }
}
