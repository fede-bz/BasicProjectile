using UnityEngine;

public class TemporizadorVida : MonoBehaviour
{
    public float tiempoDeVida = 5f;

    void Start()
    {
        Debug.Log("Tiempo de vida inicial: " + tiempoDeVida + " segundos");
    }

    void Update()
    {
        tiempoDeVida -= Time.deltaTime;

        if (tiempoDeVida <= 0)
        {
            Debug.Log("¡Tiempo agotado! Objeto destruido");
            Destroy(gameObject);
        }
    }
}