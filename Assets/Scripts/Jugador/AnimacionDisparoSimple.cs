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
        if (Input.GetKeyDown(KeyCode.Space) && animator != null)
            animator.SetTrigger("Shoot");
    }
}