using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    // Lista de enemigos dentro del alcance de la torre.
    public List<GameObject> enemys = new List<GameObject>();

    // Método que se llama cuando un objeto entra en el trigger collider de la torre.
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            // Añade el enemigo a la lista de enemigos.
            enemys.Add(col.gameObject);
        }
    }

    // Método que se llama cuando un objeto sale del trigger collider de la torre.
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            // Elimina el enemigo de la lista de enemigos.
            enemys.Remove(col.gameObject);
        }
    }

    // Tiempo entre ataques.
    public float attackTime = 1;
    // Temporizador para controlar el tiempo entre ataques.
    private float timer = 0;
    // Efecto visual del ataque.
    public GameObject attackEffect;
    // Posición donde se generará el efecto del ataque.
    public Transform attackEfectPosition;
    // Componente Animator para controlar las animaciones de la torre.
    public Animator animator;

    private void Start()
    {
        // Obtiene el componente Animator del GameObject.
        animator = gameObject.GetComponent<Animator>();
        // Inicializa el temporizador.
        timer = attackTime;
        // Ajusta la velocidad del animador según la escala de tiempo del juego.
        animator.speed = GameManagerScript.timeScale * 100f;
    }

    void Update()
    {
        // Ajusta la velocidad del animador según la escala de tiempo del juego.
        animator.speed = GameManagerScript.timeScale * 100f;
        // Incrementa el temporizador.
        timer += GameManagerScript.timeScale;
        // Si el primer enemigo en la lista es null, actualiza la lista de enemigos.
        if (enemys[0] == null)
        {
            // Cambia a la animación de idle.
            animator.SetTrigger("IdelTrigger");
            UpdateEnemys();
        }
        // Si hay enemigos en la lista y el temporizador ha alcanzado el tiempo de ataque, ataca.
        if (enemys.Count > 0 && timer >= attackTime)
        {
            timer = 0;
            Attack();
        }
    }

    // Método para atacar al enemigo.
    void Attack()
    {
        // Si hay al menos un enemigo en la lista.
        if (enemys.Count >= 1)
        {
            // Si el enemigo tiene 0 o menos puntos de vida, elimina al enemigo de la lista.
            if (enemys[0].GetComponent<Enemy>().GetLifePoint() <= 0)
            {
                enemys[0] = null;
                // Cambia a la animación de idle.
                animator.SetTrigger("IdelTrigger");
                return;
            }
            // La torre se orienta hacia el enemigo.
            transform.LookAt(enemys[0].transform.position);
            // Cambia a la animación de ataque.
            animator.SetTrigger("AttackTrigger");
            // Inicia una corrutina para realizar la acción de ataque después de un retraso.
            StartCoroutine(DelayedAction(() =>
            {
                // Instancia el efecto de ataque.
                GameObject attack = GameObject.Instantiate(attackEffect, attackEfectPosition.position, attackEfectPosition.rotation);
                // Establece el objetivo del ataque.
                attack.GetComponent<Attack>().SetTarget(enemys[0].transform);
            }, GameManagerScript.timeScale * 10));
        }
        else
        {
            // Reinicia el temporizador si no hay enemigos.
            timer = attackTime;
        }
    }

    // Corrutina para realizar una acción después de un retraso.
    IEnumerator DelayedAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    // Método para actualizar la lista de enemigos (eliminar los nulls).
    void UpdateEnemys()
    {
        // Lista de índices de enemigos null.
        List<int> index = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                index.Add(i);
            }
        }
        // Elimina los enemigos null de la lista.
        for (int i = 0; i < index.Count; i++)
        {
            enemys.RemoveAt(index[i] - i);
        }
    }
}
