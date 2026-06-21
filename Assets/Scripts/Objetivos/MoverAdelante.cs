using UnityEngine;
public class MoverAdelante : MonoBehaviour
{
    [HideInInspector] public float velocidad = 0f;
    void Update()
    {
        if (ShootingGalleryManager.instance != null && !ShootingGalleryManager.instance.juegoActivo) return;
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    // --- GIZMOS ---
    private void OnDrawGizmosSelected()
    {
        // Flecha que muestra hacia dónde se mueve la diana y a qué velocidad relativa
        Gizmos.color = Color.cyan;
        Vector3 destino = transform.position + transform.forward * 2f;
        Gizmos.DrawLine(transform.position, destino);
        Gizmos.DrawWireSphere(destino, 0.1f);
    }
}