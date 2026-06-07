using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instancia { get; private set; }

    private string ruta;
    public bool hayPartidaCargada = false;
    public DatosPartida datosARestaurar;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);
        ruta = Application.persistentDataPath + "/guardado.json";
    }

    public void Guardar()
    {
        ShootingGalleryManager sgm = ShootingGalleryManager.instance;

        DatosPartida datos = new DatosPartida();
        datos.score = sgm.score;
        datos.balas = sgm.balasRestantes;
        datos.dianasRestantes = sgm.dianasActivas;
        datos.nivel = sgm.nivelActual;

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(ruta, json);
        Debug.Log("Partida guardada: " + json);
    }

    public void Cargar()
    {
        if (!File.Exists(ruta))
        {
            Debug.Log("No hay guardado disponible.");
            return;
        }

        string json = File.ReadAllText(ruta);
        datosARestaurar = JsonUtility.FromJson<DatosPartida>(json);
        hayPartidaCargada = true;

        string nombreEscena = datosARestaurar.nivel == 2 ? "Nivel2" : "Nivel1";
        SceneManager.LoadScene(nombreEscena);
    }

    public bool HayGuardado()
    {
        return File.Exists(ruta);
    }
}

[System.Serializable]
public class DatosPartida
{
    public int score;
    public int balas;
    public int dianasRestantes;
    public int nivel;
}