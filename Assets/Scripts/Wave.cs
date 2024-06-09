using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

// Atributo Serializable para que la clase pueda ser vista y editada desde el Inspector de Unity.
[System.Serializable]
public class Wave
{
    // Prefab del enemigo que se generará en esta oleada.
    public GameObject enemyPrefab;

    // Cantidad de enemigos en esta oleada.
    public int count;

    // Tasa de generación de enemigos en esta oleada (en segundos entre cada enemigo).
    public float rate;
}