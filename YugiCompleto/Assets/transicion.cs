using UnityEngine;
using UnityEngine.SceneManagement;

public class transicion : MonoBehaviour
{
    private Animator animator;
    private string escenaACargar;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CargarEscena(string escena)
    {
        escenaACargar = escena;
        animator.SetTrigger("fadeSale");
    }
    public void FinCargarEscena()
    {
        SceneManager.LoadScene(escenaACargar);
    }
}
