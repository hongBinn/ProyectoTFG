using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    // Almacena la rotación inicial del objeto.
    private Quaternion initialRotation;

    void Start()
    {
        // Guarda la rotación inicial del objeto.
        initialRotation = transform.rotation;
    }

    // Método que se llama después de que todos los métodos Update han sido llamados.
    void LateUpdate()
    {
        // Restablece la rotación del objeto a su rotación inicial.
        transform.rotation = initialRotation;
    }
}
