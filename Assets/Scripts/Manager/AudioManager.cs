using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música")]
    [SerializeField] private AudioSource musicaFondo;

    [Header("Efectos")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip sonidoDisparo1;
    [SerializeField] private AudioClip sonidoDisparo2;
    [SerializeField] private AudioClip sonidoImpacto;
    [SerializeField] private AudioClip sonidoVictoria;
    [SerializeField] private AudioClip sonidoGameOver;

    private bool ultimoDisparoFue1 = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu0")
        {
            sfxSource.Stop();
            musicaFondo.Stop();
            musicaFondo.Play();
        }
        else if (scene.name == "Nivel1" && sfxSource.isPlaying)
        {
            sfxSource.Stop();
            musicaFondo.Stop();
            musicaFondo.Play();
        }
    }

    public void PlayDisparo()
    {
        if (ultimoDisparoFue1)
            sfxSource.PlayOneShot(sonidoDisparo2);
        else
            sfxSource.PlayOneShot(sonidoDisparo1);
        ultimoDisparoFue1 = !ultimoDisparoFue1;
    }

    public void PlayImpacto()
    {
        sfxSource.PlayOneShot(sonidoImpacto);
    }

    public void PlayVictoria()
    {
        musicaFondo.Stop();
        sfxSource.clip = sonidoVictoria;
        sfxSource.Play();
    }

    public void PlayGameOver()
    {
        musicaFondo.Stop();
        sfxSource.clip = sonidoGameOver;
        sfxSource.Play();
    }
}