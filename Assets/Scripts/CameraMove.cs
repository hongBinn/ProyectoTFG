using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Velocidad de movimiento del objeto.
    public float speed = 1;
    // Sensibilidad del ratón (no se utiliza en el código proporcionado).
    public float mouseSpeed = 60;
    // Límites mínimos y máximos para las coordenadas X y Z.
    public Vector2 minXZLimits = new Vector2(-50f, 75f);

    void Update()
    {
        // Obtener la entrada vertical (W/S o flechas hacia arriba/abajo).
        float v = Input.GetAxis("Vertical");
        // Obtener la entrada horizontal (A/D o flechas hacia la izquierda/derecha).
        float h = Input.GetAxis("Horizontal");

        // Calcular el vector de movimiento basado en la entrada del usuario.
        Vector3 movement = new Vector3(v * speed, 0, -h * speed) * Time.deltaTime;

        // Mover el objeto en el espacio del mundo.
        transform.Translate(movement, Space.World);

        // Limitar la posición de la cámara en el eje X y el eje Z.
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minXZLimits.x, -30);
        newPosition.z = Mathf.Clamp(newPosition.z, 15, minXZLimits.y);
        transform.position = newPosition;
    }
}
