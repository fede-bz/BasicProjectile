using UnityEngine;
public class Proyectil : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Velocidad de avance del proyectil hacia adelante")]
    [Range(1f, 30f)]
    public float velocidad = 10f;

    [Header("Tiempo de Vida")]
    [Tooltip("Segundos antes de autodestruirse si no impacta nada")]
    [Range(0.5f, 10f)]
    public float tiempoDeVida = 1f;

    [Header("Efectos")]
    [Tooltip("Partícula que se instancia al impactar contra algo")]
    [SerializeField] GameObject impactoPS;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        tiempoDeVida -= Time.deltaTime;
        if (tiempoDeVida <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (impactoPS != null)
            Instantiate(impactoPS, transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            if (!ShootingGalleryManager.instance.juegoActivo) return;
            ObjetivoAnimado obj = collision.gameObject.GetComponentInParent<ObjetivoAnimado>();
            if (obj != null)
                obj.Golpear();
            else
                Destroy(collision.transform.parent != null ? collision.transform.parent.gameObject : collision.gameObject);
        }
        Destroy(gameObject);
    }
}