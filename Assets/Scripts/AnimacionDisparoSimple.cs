using UnityEngine;

public class AnimacionDisparoSimple : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Cuando presiona Espacio, trigger animación
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
}