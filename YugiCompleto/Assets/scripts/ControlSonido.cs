using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSonido : MonoBehaviour
{
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    private void Start()
    {
        sonido.MusicaMenu();
    }
    public void ReproducirMusica()
    {
       
    }
}
