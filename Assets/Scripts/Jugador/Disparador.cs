using UnityEngine;
public class Disparador : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Prefab del proyectil que se instancia al disparar")]
    public GameObject prefabProyectil;

    [Tooltip("Punto desde donde sale el disparo (define posición y dirección)")]
    public Transform puntoDisparo;

    [Header("Configuración de Disparo")]
    [Range(0.1f, 3f)]
    [Tooltip("Tiempo mínimo entre disparos, en segundos")]
    public float cooldownDisparo = 0.5f;

    private float tiempoUltimoDisparo = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= tiempoUltimoDisparo + cooldownDisparo)
        {
            if (ShootingGalleryManager.instance.PuedeDisparar())
            {
                Disparar();
                ShootingGalleryManager.instance.GastarBala();
                tiempoUltimoDisparo = Time.time;
            }
        }
    }

    void Disparar()
    {
        if (prefabProyectil == null || puntoDisparo == null)
        {
            Debug.LogError("Falta asignar prefab o punto de disparo");
            return;
        }
        Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
        AudioManager.Instance.PlayDisparo();
    }

    private void OnDrawGizmos()
    {
        if (puntoDisparo == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(puntoDisparo.position, 0.05f);
    }

    private void OnDrawGizmosSelected()
    {
        if (puntoDisparo == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(puntoDisparo.position, puntoDisparo.position + puntoDisparo.forward * 10f);
        Gizmos.DrawWireSphere(puntoDisparo.position + puntoDisparo.forward * 10f, 0.15f);
    }
}