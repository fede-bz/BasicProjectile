using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoDeVida = 1f;
    public float velocidad = 10f;
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