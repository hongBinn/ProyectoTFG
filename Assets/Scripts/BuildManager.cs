using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    // Instancia est�tica del BuildManager para permitir el acceso global.
    public static BuildManager Instance;

    // Datos de la torreta arquera.
    public TurretData archerData;
    // Datos de la torreta caster.
    public TurretData casterData;
    // Datos de la torreta seleccionada.
    private TurretData selectedTurret;

    // Texto de la UI que muestra el costo.
    public Text costText;
    // Animador para mostrar una animaci�n cuando el costo es insuficiente.
    public Animator costAnimator;

    // Costo inicial.
    private int cost = 20;

    // M�todo para actualizar el costo y el texto correspondiente.
    public void UpdateCost(int num = 0)
    {
        cost += num;
        costText.text = "" + cost;
    }

    private void Awake()
    {
        // Asigna esta instancia a la instancia est�tica.
        Instance = this;
    }

    void Update()
    {
        // Si se detecta un clic izquierdo del rat�n.
        if (Input.GetMouseButtonDown(0))
        {
            // Verifica si el puntero del rat�n no est� sobre un objeto de la UI.
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                // Lanza un rayo desde la posici�n del rat�n en la pantalla.
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Comprueba si el rayo colisiona con un objeto en la capa "Position".
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Position"));
                if (isCollider)
                {
                    // Obtiene el componente TowerPosition del objeto colisionado.
                    TowerPosition towerObject = hit.collider.GetComponent<TowerPosition>();
                    try
                    {
                        // Si hay una torreta seleccionada y la posici�n no tiene una torre construida.
                        if (selectedTurret != null && towerObject.isTower == null)
                        {
                            // Si el costo de la torreta es menor o igual al costo disponible.
                            if (selectedTurret.cost <= cost)
                            {
                                // Construye la torreta en la posici�n.
                                towerObject.BuildTower(selectedTurret.turretPrefab);
                                // Actualiza el costo restante.
                                UpdateCost(-selectedTurret.cost);
                                // Restablece la escala de tiempo del juego.
                                GameManagerScript.timeScale = GameManagerScript.timeScaleData;
                            }
                            else
                            {
                                // Si no hay suficiente costo, muestra una animaci�n.
                                costAnimator.SetTrigger("FlickTrigger");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // Captura cualquier excepci�n y obtiene la excepci�n base.
                        e.GetBaseException();
                    }
                }
            }
        }
    }

    // M�todo para seleccionar la torreta arquera.
    public void OnArcherSelected(bool isOn)
    {
        SelectingTower(isOn, archerData);
    }

    // M�todo para seleccionar la torreta caster.
    public void OnCasterSelected(bool isOn)
    {
        SelectingTower(isOn, casterData);
    }

    // M�todo privado para seleccionar una torreta.
    private void SelectingTower(bool isOn, TurretData turretData)
    {
        if (isOn)
        {
            // Reduce la escala de tiempo para permitir la selecci�n de la torreta.
            GameManagerScript.timeScale = 0.001f;
            selectedTurret = turretData;
        }
        else
        {
            // Restablece la escala de tiempo.
            GameManagerScript.timeScale = GameManagerScript.timeScaleData;
        }
    }
}
