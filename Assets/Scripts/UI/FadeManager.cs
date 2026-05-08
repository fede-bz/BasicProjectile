using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public Image imagenFade;
    public float duracionFade = 1f;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }

    public IEnumerator FadeOut(System.Action alTerminar = null)
    {
        float tiempo = 0f;
        Color color = imagenFade.color;
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, tiempo / duracionFade);
            imagenFade.color = color;
            yield return null;
        }
        alTerminar?.Invoke();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float tiempo = 0f;
        Color color = imagenFade.color;
        color.a = 1f;
        imagenFade.color = color;
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, tiempo / duracionFade);
            imagenFade.color = color;
            yield return null;
        }
    }
}