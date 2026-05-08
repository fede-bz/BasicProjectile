using UnityEngine;
using System.Collections;

public class ObjetivoAnimado : MonoBehaviour
{
    private Animator anim;
    private bool yaGolpeada = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Golpear()
    {
        if (yaGolpeada) return;
        yaGolpeada = true;
        if (anim != null)
        {
            anim.SetTrigger("Golpeada");
            StartCoroutine(DestruirDespues(0.5f));
        }
        else
        {
            ShootingGalleryManager.instance.DianaDestruida();
            Destroy(gameObject);
        }
    }

    IEnumerator DestruirDespues(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootingGalleryManager.instance.DianaDestruida();
        Destroy(gameObject);
    }
}