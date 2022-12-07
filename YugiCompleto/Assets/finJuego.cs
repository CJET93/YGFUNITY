using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finJuego : MonoBehaviour
{
    public Sonido sonido;
    private bool presionado;
    public transicion transicion;
    void Start()
    {
        Cursor.visible = false;
        presionado = false;
        sonido.MusicaGameOver();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (presionado == false)
            {
                presionado = true;
                transicion.CargarEscena("MenuInicio");
            }
        }
    }

    public void BotonC()
    {
        if (presionado == false)
        {
            presionado = true;
            transicion.CargarEscena("MenuInicio");
        }
    }


}
