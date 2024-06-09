using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Daño que el proyectil inflige.
    public int damage = 0;
    // Velocidad a la que se mueve el proyectil.
    public float speed = 0;
    // Transform del objetivo al que se dirige el proyectil.
    private Transform target;

    // Método para establecer el objetivo del proyectil.
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Método que se llama una vez por frame.
    private void Update()
    {
        // Si el proyectil tiene un objetivo.
        if (target != null)
        {
            // Obtiene la posición del objetivo.
            Vector3 targetPosition = target.position;
            // Mantiene la altura del proyectil constante.
            targetPosition.y = transform.position.y;
            // Apunta el proyectil hacia el objetivo.
            transform.LookAt(targetPosition);
            // Mueve el proyectil hacia adelante a la velocidad definida.
            transform.Translate(Vector3.forward * speed * GameManagerScript.timeScale);
        }
        else
        {
            // Si no hay un objetivo, destruye el proyectil.
            Destroy(this.gameObject);
        }
    }

    // Método que se llama cuando el proyectil colisiona con otro objeto.
    void OnTriggerEnter(Collider col)
    {
        // Si el objeto con el que colisiona tiene el componente Enemy.
        if (col.GetComponent<Enemy>() != null)
        {
            // Llama al método TakeDamage del enemigo para infligirle daño.
            col.GetComponent<Enemy>().TakeDamage(damage);
        }
        // Destruye el proyectil después de la colisión.
        Destroy(this.gameObject);
    }
}
