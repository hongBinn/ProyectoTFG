using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLife : MonoBehaviour
{
    // Puntos de vida iniciales.
    public int lifePoint = 10;
    // Puntos de vida totales (valor inicial).
    private int totalLife;
    // Referencia al objeto que controla la vida del terminal (por ejemplo, la base o punto de control del juego).
    public GameObject terminal;
    // Referencia al slider de la UI que muestra los puntos de vida.
    public Slider lpSlider;
    // Referencia al objeto de la UI que muestra la vida.
    public GameObject lifeUi;

    void Start()
    {
        // Inicializa los puntos de vida totales.
        totalLife = lifePoint;
    }

    // Método que se llama cuando otro objeto entra en el trigger collider de este objeto.
    void OnTriggerEnter(Collider collision)
    {
        // Si el objeto que colisiona tiene la etiqueta "Enemy".
        if (collision.tag == "Enemy" )
        {
            // Llama al método DecreaseLife del componente EndLife del terminal.
            terminal.GetComponent<EndLife>().DecreaseLife();
            // Disminuye los puntos de vida de este objeto.
            DecreaseLife();
            // Actualiza el valor del slider de la UI según los puntos de vida restantes.
            lpSlider.value = (float)lifePoint / totalLife;
            // Si los puntos de vida llegan a 0, desactiva la UI de vida.
            if (lifePoint == 0)
            {
                lifeUi.SetActive(false);
            }
        }
    }

    // Método para disminuir los puntos de vida.
    public void DecreaseLife()
    {
        // Decrementa los puntos de vida.
        lifePoint -= 1;
        // Si los puntos de vida llegan a 0, llama al método Failed del GameManagerScript.
        if (lifePoint == 0)
        {
            GameManagerScript.Instance.Failed();
        }
    }
}
