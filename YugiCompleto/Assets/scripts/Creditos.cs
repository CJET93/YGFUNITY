using UnityEngine;

public class Creditos : MonoBehaviour
{
    // Start is called before the first frame update
    public transicion transicion;
    void Start()
    {
        Invoke("MenuPrincipal", 30);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuPrincipal()
    {
        transicion.CargarEscena("MenuInicio");
    }
}
