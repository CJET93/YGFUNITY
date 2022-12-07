using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlADeck : MonoBehaviour
{
    
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
      
    }
    public void cambiarADeck(string fase)
    {
        datosJuego.SetFase(fase);
    }

    
}
