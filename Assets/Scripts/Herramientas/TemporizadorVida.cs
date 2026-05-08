using UnityEngine;
public class TemporizadorVida : MonoBehaviour
{
    public float tiempoDeVida = 5f;
    void Update()
    {
        tiempoDeVida -= Time.deltaTime;
        if (tiempoDeVida <= 0)
            Destroy(gameObject);
    }
}