using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComprobarNombre : MonoBehaviour
{
    public TextMeshProUGUI textoNombre;
    private GameObject objetoDatosJuego;
    private DatosJuego datosJuego;
    public GameObject panelError;
    public TextMeshProUGUI textoError;
    public transicion transicion;
    public void Start()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
    }
    public void comprobanteTexto()
    {
        if (textoNombre.text.Length!=1 )
        {
            if (textoNombre.text.Length > 20)
            {
                panelError.SetActive(true);
                textoError.text = "El campo es muy largo";
             
            }
            else
            {
                List<int> nuevoDeck = new List<int>();
                datosJuego.nombreJugador = textoNombre.text;
                datosJuego.ReiniciarDatos();
                transicion.CargarEscena("Historia");
              
            }
        }
        else{
            panelError.SetActive(true);
            textoError.text = "El campo esta vacio";
        }
    }
}
