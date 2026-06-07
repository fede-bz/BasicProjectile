using UnityEngine;
using System.Collections;

public class ObjetivoAnimado : MonoBehaviour
{
    [SerializeField] private DianaData datos;
    private Animator anim;
    private bool yaGolpeada = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (datos != null)
        {
            // Aplicar velocidad al MoverAdelante
            MoverAdelante mover = GetComponent<MoverAdelante>();
            if (mover != null) mover.velocidad = datos.velocidad;

            // Aplicar color al hijo "Target" con instancia propia de material
            Transform targetChild = transform.Find("Target");
            if (targetChild != null)
            {
                Renderer rend = targetChild.GetComponent<Renderer>();
                if (rend != null)
                {
                    Material matInstancia = new Material(rend.material);
                    matInstancia.color = datos.colorDiana;
                    rend.material = matInstancia;
                }
            }
        }
    }

    public void Golpear()
    {
        if (yaGolpeada) return;
        yaGolpeada = true;
        AudioManager.Instance.PlayImpacto();
        if (anim != null)
        {
            anim.SetTrigger("Golpeada");
            StartCoroutine(DestruirDespues(0.5f));
        }
        else
        {
            ShootingGalleryManager.instance.DianaDestruida(ObtenerPuntaje());
            Destroy(gameObject);
        }
    }

    IEnumerator DestruirDespues(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootingGalleryManager.instance.DianaDestruida(ObtenerPuntaje());
        Destroy(gameObject);
    }

    private int ObtenerPuntaje()
    {
        return datos != null ? datos.puntaje : ShootingGalleryManager.instance.puntajePorDiana;
    }
}