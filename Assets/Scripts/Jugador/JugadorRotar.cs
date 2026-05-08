using UnityEngine;
public class JugadorRotar : MonoBehaviour
{
    public float velocidadRotacion = 100f;
    void Update()
    {
        float rotacion = Input.GetKey(KeyCode.A) ? -velocidadRotacion :
                         Input.GetKey(KeyCode.D) ? velocidadRotacion : 0f;
        transform.Rotate(0, rotacion * Time.deltaTime, 0);
    }
}