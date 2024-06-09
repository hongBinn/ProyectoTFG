using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPosition : MonoBehaviour
{
    // Objeto que indica si hay una torre construida en esta posici�n.
    [HideInInspector]
    public GameObject isTower;

    // M�todo para construir una torre en esta posici�n.
    public void BuildTower(GameObject towerPrefab)
    {
        // Instanciar una torre en la posici�n actual con la rotaci�n por defecto.
        isTower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }
}
