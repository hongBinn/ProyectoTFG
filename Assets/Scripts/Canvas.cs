using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    // Almacena la rotaci�n inicial del objeto.
    private Quaternion initialRotation;

    void Start()
    {
        // Guarda la rotaci�n inicial del objeto.
        initialRotation = transform.rotation;
    }

    // M�todo que se llama despu�s de que todos los m�todos Update han sido llamados.
    void LateUpdate()
    {
        // Restablece la rotaci�n del objeto a su rotaci�n inicial.
        transform.rotation = initialRotation;
    }
}
