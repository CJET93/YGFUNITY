using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaDL : MonoBehaviour
{
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosDuelo datosDuelo;
    private DatosJuego datosJuego;
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    public GameObject logoTienda;
    public transicion transicion;
    public List<int> listaMusica;
    public bool desactivarControles;
    // Start is called before the first frame update
    void Start()
    {
        desactivarControles = false;
        sonido.numero = datosDuelo.GetMusicaDueloLibre();
        sonido.ReproducirMusica();
        datosDuelo.SetMusicaDueloLibre(listaMusica.IndexOf(datosDuelo.GetMusicaDueloLibre()));
    }
    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!desactivarControles) { 
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (datosDuelo.GetMusicaDueloLibre() != 9)
            {

                datosDuelo.SetMusicaDueloLibre(datosDuelo.GetMusicaDueloLibre() + 1);
            }
            else
            {
                datosDuelo.SetMusicaDueloLibre(0);
            }

            sonido.numero = listaMusica[datosDuelo.GetMusicaDueloLibre()];
            sonido.ReproducirMusica();

        }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (datosDuelo.GetMusicaDueloLibre() != 0)
                {

                    datosDuelo.SetMusicaDueloLibre(datosDuelo.GetMusicaDueloLibre() -1 );
                }
                else
                {
                    datosDuelo.SetMusicaDueloLibre(9);
                }

                sonido.numero = listaMusica[datosDuelo.GetMusicaDueloLibre()];
                sonido.ReproducirMusica();

            }
           else  if (Input.GetKeyDown(KeyCode.C))
        {
            desactivarControles = true;
            datosDuelo.SetMusicaDueloLibre(listaMusica[datosDuelo.GetMusicaDueloLibre()]);
            efectosSonido.CancelarAccion();
            transicion.CargarEscena("MenuContinuar");
        }
    }
    }

    public void BotonC()
    {
        if (!desactivarControles)
        {
            desactivarControles = true;
            datosDuelo.SetMusicaDueloLibre(listaMusica[datosDuelo.GetMusicaDueloLibre()]);
            efectosSonido.CancelarAccion();
            transicion.CargarEscena("MenuContinuar");
        }
    }

    public void BotonDerecha()
    {
        if (!desactivarControles)
        {
            if (datosDuelo.GetMusicaDueloLibre() != 9)
            {

                datosDuelo.SetMusicaDueloLibre(datosDuelo.GetMusicaDueloLibre() + 1);
            }
            else
            {
                datosDuelo.SetMusicaDueloLibre(0);
            }

            sonido.numero = listaMusica[datosDuelo.GetMusicaDueloLibre()];
            sonido.ReproducirMusica();
        }
    }

    public void BotonIzquierda()
    {
        if (!desactivarControles)
        {
            if (datosDuelo.GetMusicaDueloLibre() != 0)
            {

                datosDuelo.SetMusicaDueloLibre(datosDuelo.GetMusicaDueloLibre() - 1);
            }
            else
            {
                datosDuelo.SetMusicaDueloLibre(9);
            }

            sonido.numero = listaMusica[datosDuelo.GetMusicaDueloLibre()];
            sonido.ReproducirMusica();
        }
    }
}
