using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    // Diccionario estático para almacenar los caminos con su ID correspondiente.
    public static Dictionary<int, Transform[]> ways = new Dictionary<int, Transform[]>();

    // Arreglo de posiciones que forman el camino.
    public Transform[] positions;

    // ID del camino.
    public int wayID;

    // Método llamado antes del primer frame.
    void Start()
    {
        // Inicializa el arreglo de posiciones con el número de hijos (puntos) del objeto actual.
        positions = new Transform[transform.childCount];

        // Itera a través de los hijos del objeto actual para obtener las posiciones.
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.GetChild(i);
        }

        // Agrega el arreglo de posiciones al diccionario de caminos usando su ID como clave.
        ways[wayID] = positions;
    }
}
