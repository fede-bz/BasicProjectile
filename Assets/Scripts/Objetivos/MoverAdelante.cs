using UnityEngine;
public class MoverAdelante : MonoBehaviour
{
    public float velocidad = 3f;
    void Update()
    {
        if (ShootingGalleryManager.instance != null && !ShootingGalleryManager.instance.juegoActivo) return;
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}