using UnityEngine;
public class Disparador : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
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
}