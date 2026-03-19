using UnityEngine;

public class TestMensajesUnity : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("AWAKE: Se ejecuta primero, antes de Start");
    }

    void Start()
    {
        Debug.Log("START: Se ejecuta una vez al inicio");
    }

    void Update()
    {
        // Comentado para no llenar la console
        // Debug.Log("UPDATE: Se ejecuta cada frame");
    }

    void FixedUpdate()
    {
        // Debug.Log("FIXED UPDATE: Cada intervalo fijo");
    }

    void LateUpdate()
    {
        // Debug.Log("LATE UPDATE: Después de Update");
    }

    void OnEnable()
    {
        Debug.Log("ON ENABLE: Cuando se activa");
    }

    void OnDisable()
    {
        Debug.Log("ON DISABLE: Cuando se desactiva");
    }
}