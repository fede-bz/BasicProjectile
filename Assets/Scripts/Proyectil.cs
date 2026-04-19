using UnityEngine;
using System.Collections;

public class Proyectil : MonoBehaviour
{
    public float tiempoDeVida = 3f;
    public float velocidad = 10f;

    void Start()
    {
        Debug.Log("Proyectil disparado!");
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
        tiempoDeVida -= Time.deltaTime;
        if (tiempoDeVida <= 0)
        {
            Debug.Log("Proyectil destruido por tiempo");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("¡Proyectil golpeó: " + collision.gameObject.name + "!");

        if (collision.gameObject.CompareTag("Objetivo"))
        {
            ObjetivoAnimado obj = collision.gameObject.GetComponentInParent<ObjetivoAnimado>();
            if (obj != null)
            {
                obj.Golpear();
            }
            else
            {
                GameObject objetivo = collision.transform.parent != null
                    ? collision.transform.parent.gameObject
                    : collision.gameObject;
                Destroy(objetivo);
            }
        }

        Destroy(gameObject);
    }

    IEnumerator DestruirDespues(GameObject obj, float delay)
    {
        Debug.Log("Esperando " + delay + " segundos para destruir: " + obj.name);
        yield return new WaitForSeconds(delay);
        Debug.Log("Destruyendo: " + obj.name);
        Destroy(obj);
    }
}