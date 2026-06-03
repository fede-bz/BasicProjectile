using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public void JugarNivel1()
    {
        StartCoroutine(FadeManager.instance.FadeOut(() =>
        {
            SceneManager.LoadScene("Nivel1");
        }));
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