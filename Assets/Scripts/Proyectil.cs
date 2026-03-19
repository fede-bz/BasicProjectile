using UnityEngine;

public class Proyectil : MonoBehaviour
{
    // EJERCICIO 2: Variable que disminuye
    public float tiempoDeVida = 3f;

    // EJERCICIO 3: Velocidad de movimiento
    public float velocidad = 10f;

    void Start()
    {
        Debug.Log("Proyectil disparado!");
    }

    void Update()
    {
        // EJERCICIO 3: Movimiento con Translate()
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        // EJERCICIO 2: Tiempo de vida disminuye
        tiempoDeVida -= Time.deltaTime;

        if (tiempoDeVida <= 0)
        {
            Debug.Log("Proyectil destruido por tiempo");
            Destroy(gameObject);
        }
    }

    // EJERCICIO 4: Colisión que destruye
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("¡Proyectil golpeó: " + collision.gameObject.name + "!");

        // Solo destruir si tiene el Tag "Objetivo"
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            Debug.Log("¡Objetivo destruido!");

            // Obtener el PADRE directo (un nivel arriba)
            if (collision.transform.parent != null)
            {
                // Destruir el padre (Objetivo 1, 2, 3, etc.)
                Destroy(collision.transform.parent.gameObject);
            }
            else
            {
                // Si no tiene padre, destruir el objeto mismo
                Destroy(collision.gameObject);
            }
        }

        // El proyectil siempre se destruye
        Destroy(gameObject);
    }
}