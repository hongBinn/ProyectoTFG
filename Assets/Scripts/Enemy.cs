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
    // �ndice actual de la posici�n a la que se mueve el enemigo.
    private int index = 0;
    // Slider para mostrar la barra de vida del enemigo.
    public Slider lpSlider;
    // Animator para controlar la animaci�n del enemigo.
    private Animator animator;
    // Identificador del camino que sigue el enemigo.
    public int wayID;
    // AudioSource para reproducir sonidos del enemigo.
    private AudioSource enemyAudioSource;

    // M�todo que se llama antes del primer frame.
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
        // Ajustar la velocidad de la animaci�n.
        animator.speed = GameManagerScript.timeScale * 100f;
        // Obtener el AudioSource del objeto.
        enemyAudioSource = GetComponent<AudioSource>();
    }

    // M�todo que se llama una vez por frame.
    void Update()
    {
        // Ajustar la velocidad de la animaci�n.
        animator.speed = GameManagerScript.timeScale * 100f;
        // Si el enemigo est� muerto o no tiene posiciones, no se mueve.
        if (lifePoint <= 0 || positions == null) transform.position = transform.position;
        else Move();
    }

    // M�todo para mover al enemigo.
    void Move()
    {
        // Si el �ndice es mayor que la cantidad de posiciones, salir.
        if (index > positions.Length - 1) return;
        // Mirar hacia la siguiente posici�n.
        transform.LookAt(positions[index]);
        // Mover al enemigo hacia la siguiente posici�n.
        transform.position = Vector3.MoveTowards(transform.position, positions[index].position, speed * GameManagerScript.timeScale);
        // Si el enemigo llega a la siguiente posici�n, avanzar al siguiente �ndice.
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f) index++;
        // Si el enemigo llega al final del camino, llamar a ReachDestination().
        if (index > positions.Length - 1) ReachDestination();
    }

    // M�todo llamado cuando el enemigo alcanza su destino.
    void ReachDestination()
    {
        // Actualizar el contador de enemigos muertos.
        GameManagerScript.Instance.UpdateEnemyDieNumText(1);
        // Destruir el objeto enemigo.
        Destroy(gameObject);
    }

    // M�todo llamado cuando el enemigo es destruido.
    void OnDestroy()
    {
        // Decrementar el contador de enemigos vivos.
        EnemySpawner.countEnemyAlive--;
    }

    // M�todo para obtener los puntos de vida del enemigo.
    public int GetLifePoint()
    {
        return lifePoint;
    }

    // M�todo para aplicar da�o al enemigo.
    public void TakeDamage(int dmg)
    {
        // Si el enemigo ya est� muerto, salir del m�todo.
        if (lifePoint == 0) return;
        // Aplicar da�o al enemigo.
        lifePoint -= dmg;
        // Si el enemigo queda sin puntos de vida...
        if (lifePoint <= 0)
        {
            // Activar la animaci�n de muerte.
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
            // Destruir el objeto enemigo despu�s de un tiempo para que la animaci�n se complete.
            Destroy(gameObject, 3f);
        }
        // Actualizar el valor de la barra de vida.
        lpSlider.value = (float)lifePoint / totalLife;
    }
}
