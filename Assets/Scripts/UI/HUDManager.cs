using TMPro;
using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    public TextMeshProUGUI textoBalas;
    public TextMeshProUGUI textoScore;
    public GameObject panelGameOver;
    public TextMeshProUGUI textoScoreFinal;
    public GameObject panelVictoria;
    public TextMeshProUGUI textoScoreVictoria;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (ShootingGalleryManager.instance != null)
        {
            ActualizarBalas(ShootingGalleryManager.instance.balasRestantes);
            ActualizarScore(ShootingGalleryManager.instance.score);
        }
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    public void ActualizarBalas(int balas) => textoBalas.text = "BALAS: " + balas;
    public void ActualizarScore(int score) => textoScore.text = "PUNTAJE: " + score;

    public void MostrarGameOver(int scoreFinal)
    {
        ActualizarScore(scoreFinal);
        panelGameOver.SetActive(true);
        textoScoreFinal.text = "Score: " + scoreFinal;
    }

    public void MostrarVictoria(int scoreFinal)
    {
        ActualizarScore(scoreFinal);
        panelVictoria.SetActive(true);
        textoScoreVictoria.text = "Score: " + scoreFinal;
    }

    public void Reiniciar()
    {
        StartCoroutine(FadeManager.instance.FadeOut(() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel1")));
    }

    public void IrAlMenu()
    {
        StartCoroutine(FadeManager.instance.FadeOut(() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu0")));
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}