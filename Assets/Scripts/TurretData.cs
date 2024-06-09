using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumerado que define los tipos de torretas disponibles.
public enum TurretType
{
    ArcherTurret,
    CasterTurret
}

// Clase serializable que almacena los datos de una torreta.
[System.Serializable]
public class TurretData
{
    // Prefab de la torreta básica.
    public GameObject turretPrefab;

    // Costo de construcción de la torreta básica.
    public int cost;

    // Prefab de la torreta mejorada.
    public GameObject turretUpgradePrefab;

    // Costo de mejora de la torreta.
    public int costUpgrade;

    // Tipo de torreta.
    public TurretType type;
}