using UnityEngine;

public class Disparador : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float cooldownDisparo = 0.5f;

    private float tiempoUltimoDisparo = 0f;

    void Update()
    {
        // Disparar con Espacio
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= tiempoUltimoDisparo + cooldownDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        Debug.Log("¡Disparo!");

        if (prefabProyectil == null || puntoDisparo == null)
        {
            Debug.LogError("Falta asignar prefab o punto de disparo");
            return;
        }

        // Instanciar proyectil
        Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
    }
}