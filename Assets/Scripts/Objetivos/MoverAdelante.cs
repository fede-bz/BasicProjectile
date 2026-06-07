using UnityEngine;

public class MoverAdelante : MonoBehaviour
{
    [HideInInspector] public float velocidad = 0f;

    void Update()
    {
        if (ShootingGalleryManager.instance != null && !ShootingGalleryManager.instance.juegoActivo) return;
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}