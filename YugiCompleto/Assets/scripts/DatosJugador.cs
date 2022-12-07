using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DatosJugador
{
    public int historia;
    public int estrellas;
    public int eventosHistoria;
    public List<int> deckUsuario;
    public string nombreJugador;
    public List<int> cofre;
    public List<int> tienda;
    public List<string> desbloqueables;
    public List<int> cantidadCofre;
    public List<int> nuevasCartas;
    public int[] victorias;
    public int[] derrotas;
    public int[] magos;
    public bool desbloqueoSeto2;
    public int idNueva;
    public string slot;
    public bool completoModoHistoria;
    public DatosJugador(DatosJuego datosjuego)
    {
        historia = datosjuego.GetHistoria();
        estrellas = datosjuego.GetEstrellas();
        deckUsuario = datosjuego.GetDeckUsuario();
        nombreJugador = datosjuego.GetNombreJugador();
        slot = datosjuego.GetSlot();
        eventosHistoria = datosjuego.GetEventosHistoria();
        cofre = datosjuego.GetCofre();
        tienda = datosjuego.GetCartasTienda();
        desbloqueables = datosjuego.GetDesbloqueables();
        cantidadCofre = datosjuego.GetCantidadCofre();
        nuevasCartas = datosjuego.GetNueva();
        victorias = datosjuego.GetVictorias();
        derrotas = datosjuego.GetDerrotas();
        magos = datosjuego.GetMagos();
        desbloqueoSeto2 = datosjuego.desbloqueoSeto2;
        idNueva = datosjuego.GetIdNueva();
        completoModoHistoria = datosjuego.GetModoHistoriaCompleto();
    }
}
