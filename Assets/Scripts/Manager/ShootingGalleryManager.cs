using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ShootingGalleryManager : MonoBehaviour
{
    public static ShootingGalleryManager instance;

    [Header("Configuración Nivel 1")]
    public int balasNivel1 = 8;
    public int dianasNivel1 = 6;

    [Header("Configuración Nivel 2")]
    public int balasNivel2 = 10;
    public int dianasNivel2 = 8;

    [Header("Configuración General")]
    public int puntajePorDiana = 100;

    [Header("Estado")]
    public int balasRestantes;
    public int score;
    public int dianasActivas;
    public bool juegoActivo = false;
    public int nivelActual = 1;

    private Coroutine coroutineGameOver;
    private bool victoriaEjecutada = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Nivel1" || scene.name == "Nivel2")
        {
            if (SaveManager.Instancia != null && SaveManager.Instancia.hayPartidaCargada)
            {
                DatosPartida datos = SaveManager.Instancia.datosARestaurar;
                nivelActual = datos.nivel;
                score = datos.score;
                balasRestantes = datos.balas;
                dianasActivas = datos.dianasRestantes;
                juegoActivo = true;
                victoriaEjecutada = false;
                coroutineGameOver = null;

                SaveManager.Instancia.hayPartidaCargada = false;

                // Omitir cinemática y reposicionar soldado
                GameObject jugador = GameObject.Find("JugadorSoldado");
                if (jugador != null)
                {
                    UnityEngine.Playables.PlayableDirector director =
                        jugador.GetComponent<UnityEngine.Playables.PlayableDirector>();
                    if (director != null) director.enabled = false;

                    // Reposicionar en su posición de juego
                    jugador.transform.position = new Vector3(
                        jugador.transform.position.x,
                        jugador.transform.position.y,
                        -12.1f
                    );
                }

                // Destruir dianas sobrantes esperando un frame
                StartCoroutine(AjustarDianas(dianasActivas));

                if (HUDManager.instance != null)
                {
                    HUDManager.instance.ActualizarBalas(balasRestantes);
                    HUDManager.instance.ActualizarScore(score);
                }
            }
            else
            {
                if (scene.name == "Nivel1") { nivelActual = 1; IniciarJuego(dianasNivel1, balasNivel1); }
                else if (scene.name == "Nivel2") { nivelActual = 2; IniciarJuego(dianasNivel2, balasNivel2); }
            }
        }
    }

    IEnumerator AjustarDianas(int dianasAConservar)
    {
        yield return null;
        yield return null;

        // Buscar solo objetos raíz con tag Objetivo (no hijos)
        GameObject[] todas = GameObject.FindGameObjectsWithTag("Objetivo");
        System.Collections.Generic.List<GameObject> raices = new System.Collections.Generic.List<GameObject>();

        foreach (GameObject d in todas)
        {
            if (d.transform.parent == null || !d.transform.parent.CompareTag("Objetivo"))
                raices.Add(d);
        }

        Debug.Log("Dianas raíz encontradas: " + raices.Count + " | A conservar: " + dianasAConservar);

        int sobrantes = raices.Count - dianasAConservar;
        for (int i = 0; i < sobrantes; i++)
        {
            Destroy(raices[i]);
        }
    }

    public void IniciarJuego(int cantidadDianas, int cantidadBalas)
    {
        balasRestantes = cantidadBalas;
        if (nivelActual == 1) score = 0;
        dianasActivas = cantidadDianas;
        juegoActivo = true;
        coroutineGameOver = null;
        victoriaEjecutada = false;
        if (HUDManager.instance != null)
        {
            HUDManager.instance.ActualizarBalas(balasRestantes);
            HUDManager.instance.ActualizarScore(score);
        }
    }

    public bool PuedeDisparar() => juegoActivo && balasRestantes > 0;

    public void GastarBala()
    {
        balasRestantes--;
        if (HUDManager.instance != null)
            HUDManager.instance.ActualizarBalas(balasRestantes);
        if (balasRestantes <= 0 && dianasActivas > 0)
            coroutineGameOver = StartCoroutine(EsperarProyectiles());
    }

    IEnumerator EsperarProyectiles()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Proyectil").Length == 0);
        yield return new WaitForSeconds(0.55f);
        if (dianasActivas > 0 && !victoriaEjecutada)
            GameOver();
    }

    public void DianaDestruida(int puntaje = 0)
    {
        score += puntaje;
        dianasActivas--;
        if (HUDManager.instance != null)
            HUDManager.instance.ActualizarScore(score);
        if (dianasActivas <= 0)
            Victoria();
    }

    void Victoria()
    {
        victoriaEjecutada = true;
        if (coroutineGameOver != null) { StopCoroutine(coroutineGameOver); coroutineGameOver = null; }
        juegoActivo = false;
        if (nivelActual == 1)
        {
            GameObject directorObj = GameObject.Find("DirectorTransicion");
            if (directorObj != null)
            {
                UnityEngine.Playables.PlayableDirector pd = directorObj.GetComponent<UnityEngine.Playables.PlayableDirector>();
                if (pd != null) { pd.Play(); StartCoroutine(EsperarTimeline(pd)); }
            }
        }
        else
        {
            AudioManager.Instance.PlayVictoria();
            if (HUDManager.instance != null)
                HUDManager.instance.MostrarVictoria(score);
        }
    }

    IEnumerator EsperarTimeline(UnityEngine.Playables.PlayableDirector pd)
    {
        yield return new WaitUntil(() => pd.state != UnityEngine.Playables.PlayState.Playing);
        CargarSiguienteNivel();
    }

    void CargarSiguienteNivel()
    {
        StartCoroutine(FadeManager.instance.FadeOut(() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)));
    }

    void GameOver()
    {
        if (victoriaEjecutada) return;
        juegoActivo = false;
        AudioManager.Instance.PlayGameOver();
        foreach (GameObject proyectil in GameObject.FindGameObjectsWithTag("Proyectil"))
            Destroy(proyectil);
        if (HUDManager.instance != null)
            HUDManager.instance.MostrarGameOver(score);
    }
}