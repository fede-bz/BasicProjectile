using UnityEngine;
using System.Collections;

public class ObjetivoAnimado : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Golpear()
    {
        if (anim != null)
        {
            anim.SetTrigger("Golpeada");
            StartCoroutine(DestruirDespues(0.5f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestruirDespues(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}