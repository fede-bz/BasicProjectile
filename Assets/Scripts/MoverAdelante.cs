using UnityEngine;

public class MoverAdelante : MonoBehaviour
{
    public float velocidad = 3f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
}