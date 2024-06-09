using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPosition : MonoBehaviour
{
    // Objeto que indica si hay una torre construida en esta posición.
    [HideInInspector]
    public GameObject isTower;

    // Método para construir una torre en esta posición.
    public void BuildTower(GameObject towerPrefab)
    {
        // Instanciar una torre en la posición actual con la rotación por defecto.
        isTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }
}
