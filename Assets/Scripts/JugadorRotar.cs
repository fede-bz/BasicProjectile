using UnityEngine;

public class JugadorRotar : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    void Update()
    {
        // Rotar con A y D
        float rotacion = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            rotacion = -velocidadRotacion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotacion = velocidadRotacion * Time.deltaTime;
        }

        transform.Rotate(0, rotacion, 0);
    }
}