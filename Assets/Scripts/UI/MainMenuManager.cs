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
}