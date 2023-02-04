using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevisarDatos : MonoBehaviour
{
    public GameObject panel;
    public GameObject menuGuardar;
    public GameObject menuIntroducirNombre;
    private GameObject objetoDatosJuego;
    public GameObject campaña;
    public GameObject dueloLibre;
    public GameObject armarDeck;
    public GameObject libreria;
    public GameObject claves;
    public GameObject volver;
    public GameObject botonGuardar;
    private DatosJuego datosJuego;
    public EfectosSonido efectosSonido;
    public transicion transicion;
    public GameObject packs;
    public GameObject musica;
    TouchScreenKeyboard keyboard;
    private GameObject objetoDatosDuelo;
    private DatosDuelo datosDuelo;

    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        MenusOcultos();
    }
    private void Update()
    {
       

    }
    public void MenusOcultos()
    {
        if (datosJuego.GetModoHistoriaCompleto() == false)
        {
            if (packs != null && musica != null)
            {
                packs.SetActive(false);
                musica.SetActive(false);
            }
        }
        else
        {
            if (packs != null && musica != null)
            {
                packs.SetActive(true);
                musica.SetActive(true);
            }

        }
    }
    public void RevisarGuardado()
    {
        if (SistemaGuardado.prepararGuardado(datosJuego) == true)
        {
            efectosSonido.SeleccionarCarta();
            panel.SetActive(true);
        }
        else
        {
            efectosSonido.SeleccionarCarta();
            menuGuardar.SetActive(false);
            menuIntroducirNombre.SetActive(true);

        }
    }
    public void reinicarMusica()
    {
        if (objetoDatosDuelo != null)
        {
            datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
            datosDuelo.SetMusicaDueloLibre(0);
        }
    }
    public void RevisarCargado1()
    {
        datosJuego.Descuento = 0;
        reinicarMusica();
        if (objetoDatosDuelo != null)
        {
            datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
            datosDuelo.SetMusicaDueloLibre(0);
        }
        if (SistemaGuardado.PrepararCargado1() == true)
        {
            efectosSonido.SeleccionarCarta();
             DatosJugador datos= SistemaGuardado.Cargar1();
            datosJuego.SetEstrellas(datos.estrellas);
            datosJuego.SetHistoria(datos.historia);
            datosJuego.SetCofre(datos.cofre);
            datosJuego.SetDerrotas(datos.derrotas);
            datosJuego.SetVictoiras(datos.victorias);
            datosJuego.SetEventosHistoria(datos.eventosHistoria);
            datosJuego.SetNombreJugador(datos.nombreJugador);
            datosJuego.SetTienda(datos.tienda);
            datosJuego.SetSlot(datos.slot);
            datosJuego.Idndueva = datos.idNueva;
            datosJuego.SetNuevo(datos.nuevasCartas);
            datosJuego.SetDesbloqueables(datos.desbloqueables);
            datosJuego.desbloqueoSeto2 = datos.desbloqueoSeto2;
            datosJuego.magos = datos.magos;
            datosJuego.SetDeckUsuario(datos.deckUsuario);
            datosJuego.SetCantidadCofre(datos.cantidadCofre);
            datosJuego.SetEsModoHistoriaCompleto(datos.completoModoHistoria);
            transicion.CargarEscena("MenuContinuar");

        }
        else
        {
            efectosSonido.NoFusion();
        }
    }
    public void RevisarCargado2()
    {
        datosJuego.Descuento = 0;
        reinicarMusica();
        if (SistemaGuardado.PrepararCargado2() == true)
        {
            efectosSonido.SeleccionarCarta();
            DatosJugador datos = SistemaGuardado.Cargar2();
            datosJuego.SetEstrellas(datos.estrellas);
            datosJuego.SetHistoria(datos.historia);
            datosJuego.SetCofre(datos.cofre);
            datosJuego.SetDerrotas(datos.derrotas);
            datosJuego.SetVictoiras(datos.victorias);
            datosJuego.SetEventosHistoria(datos.eventosHistoria);
            datosJuego.SetNombreJugador(datos.nombreJugador);
            datosJuego.SetTienda(datos.tienda);
            datosJuego.SetSlot(datos.slot);
            datosJuego.Idndueva = datos.idNueva;
            datosJuego.SetNuevo(datos.nuevasCartas);
            datosJuego.SetDesbloqueables(datos.desbloqueables);
            datosJuego.desbloqueoSeto2 = datos.desbloqueoSeto2;
            datosJuego.magos = datos.magos;
            datosJuego.SetDeckUsuario(datos.deckUsuario);
            datosJuego.SetCantidadCofre(datos.cantidadCofre);
            datosJuego.SetEsModoHistoriaCompleto(datos.completoModoHistoria);
            transicion.CargarEscena("MenuContinuar");

        }
        else
        {
            efectosSonido.NoFusion();
        }
    }
    public void RevisarCargado3()
    {
        datosJuego.Descuento = 0;
        reinicarMusica();
        if (SistemaGuardado.PrepararCargado3() == true)
        {
            efectosSonido.SeleccionarCarta();
            DatosJugador datos = SistemaGuardado.Cargar3();
            datosJuego.SetEstrellas(datos.estrellas);
            datosJuego.SetHistoria(datos.historia);
            datosJuego.SetCofre(datos.cofre);
            datosJuego.SetDerrotas(datos.derrotas);
            datosJuego.SetVictoiras(datos.victorias);
            datosJuego.SetEventosHistoria(datos.eventosHistoria);
            datosJuego.SetNombreJugador(datos.nombreJugador);
            datosJuego.SetTienda(datos.tienda);
            datosJuego.SetSlot(datos.slot);
            datosJuego.Idndueva = datos.idNueva;
            datosJuego.SetNuevo(datos.nuevasCartas);
            datosJuego.SetDesbloqueables(datos.desbloqueables);
            datosJuego.desbloqueoSeto2 = datos.desbloqueoSeto2;
            datosJuego.magos = datos.magos;
            datosJuego.SetDeckUsuario(datos.deckUsuario);
            datosJuego.SetCantidadCofre(datos.cantidadCofre);
            datosJuego.SetEsModoHistoriaCompleto(datos.completoModoHistoria);
            transicion.CargarEscena("MenuContinuar");

        }
        else
        {
            efectosSonido.NoFusion();
        }
    }
    public void CancelarAccion()
    {
        panel.SetActive(false);
        if (campaña != null)
        {
            campaña.SetActive(true);
            dueloLibre.SetActive(true);
            armarDeck.SetActive(true);
            libreria.SetActive(true);
            claves.SetActive(true);
            volver.SetActive(true);
            botonGuardar.SetActive(true);
            MenusOcultos();
        }
      
    }
    public void guardarDatos()
    {
        if (campaña != null)
        {
            campaña.SetActive(false);
            dueloLibre.SetActive(false);
            armarDeck.SetActive(false);
            libreria.SetActive(false);
            claves.SetActive(false);
            volver.SetActive(false);
            botonGuardar.SetActive(false);
            packs.SetActive(false);
            musica.SetActive(false);
        }
      
        SistemaGuardado.Guardar(datosJuego);
        panel.SetActive(true);
    }
    public void SetSlotJuego(string valor)
    {
        datosJuego.SetSlot(valor);
        Debug.LogWarning(datosJuego.GetSlot());
    }

    public void AbrirTeclado()
    {
        Debug.LogWarning("si abro teclado");
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //keyboard.Open(string text, TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default, bool autocorrection = true, bool multiline = false, bool secure = false, bool alert = false, string textPlaceholder = "", int characterLimit = 0);
    }

   
}
