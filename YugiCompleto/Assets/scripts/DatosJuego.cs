using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosJuego : MonoBehaviour
{
    //Patron singleton solo una instancia en todo el juego
    static DatosJuego datosJuego;
    private int estrellas=0;
    public string nombreJugador = "";
    // historia
    private int historia = 0;
    // deck del usuario
    private List<int> deckUsuario=new List<int>();
    // vaul del usuario
    private List<int> cofre=new List<int>();
    // ganadas free duel
    private int[] victorias=new int[40];
    // derrotas en el free fuel
    private int[] derrotas = new int[40];
    //listas de cartas obtenidas en tienda y cartas obtenidas general
    private List<int> cartasTienda=new List<int>();
    // eventoshistoria
    //desbloqueables
    private List<string> desbloqueables = new List<string>();
    public int[] magos = new int[5];
    private int eventosHistoria = 0;
    public bool desbloqueoSeto2 = false;
    //nuevas cartas
    private List<int> nuevasCartas=new List<int>();
    //Cantidad en cofre
    private List<int> cantidadCofre = new List<int>();
    // numero incremntable de cartas nuevas
    public int Idndueva;
    // para saber en donde estoy en el juego
    private string obtenerFase="";
    // que archivo
    private string slot="datos1";
    //lista temporal de duelos jugados en freeDuel
    public int[] duelosJugados = new int[40];
    //si se completo el modo historia
    public bool modoHistoriaCompleto = false;
    //la suerte que cambia cuando se cambia de deck o se consigue carta de pot1(tratando de simular el drop original)
    private int numSuerte ;

    public bool esJugadorUno = true;
    private void Awake()
    {
        if(datosJuego != null)
        {
            Destroy(gameObject);
            return;
        }
        datosJuego = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
        Debug.LogWarning("me auto destuyo");
    }
    public void SetNumSuerte(int num)
    {
        numSuerte = num;
    }
    public int GetNumSuerte()
    {
        return numSuerte;
    }
    public void SetDeckUsuario(List<int>deck)
    {
        deckUsuario = deck;
    }
    public List<int> GetDeckUsuario()
    {
        return deckUsuario;
    }
    public void SetHistoria(int numero)
    {
        historia = numero;
    }
    public int  GetHistoria()
    {
        return historia;
    }
    public void SetEventosHistoria(int numero)
    {
        eventosHistoria = numero;
    }
    public int GetIdNueva()
    {
        return Idndueva;
    }
    public void IncrementarIdNueva()
    {
        Idndueva++;
    }
    public void SetCofre(List<int> nuevo)
    {
        cofre = nuevo;
    }
    public void SetTienda(List<int> nuevo)
    {
        cartasTienda= nuevo;
    }
    public void SetCantidadCofre(List<int> nuevo)
    {
        cantidadCofre = nuevo;
    }
    public void SetNuevo(List<int> nuevo)
    {
        nuevasCartas = nuevo;
    }
    public void SetDesbloqueables(List<string> nuevo)
    {
        desbloqueables = nuevo;
    }
    public int GetEventosHistoria()
    {
        return eventosHistoria;
    }
    public int GetEstrellas()
    {
        return estrellas;
    }
    public void SetEstrellas(int cambiar)
    {
        estrellas = cambiar;
    }
    public string GetNombreJugador()
    {
        return nombreJugador;
    }
    public void SetNombreJugador(string nombre)
    {
        nombreJugador = nombre;
    }
    public void SetSlot(string nuevoSlot)
    {
        slot = nuevoSlot;
    }
    public string GetSlot()
    {
        return slot;
    }
    public List<int> GetCofre()
    {
        return cofre;
    }
    public List<int> GetNueva()
    {
        return nuevasCartas;
    }
    public List<int> GetCantidadCofre()
    {
        return cantidadCofre;
    }
    public void AñadirCantidadCofre(int elemento)
    {
        cantidadCofre.Add(elemento);
    }
    public void AñadirElementoCofre(int elemento)
    {
        cofre.Add(elemento);
    }
    public void RemoverElementoCofre(int elemento)
    {
        cofre.RemoveAt(elemento);
    }
    public void RemoverCantidadCofre(int elemento)
    {
       cantidadCofre.RemoveAt(elemento);
    }
    public void RemoverIdNueva(int elemento)
    {
        nuevasCartas.RemoveAt(elemento);
    }
    public void AñadirElementoNuevo(int elemento)
    {
        nuevasCartas.Add(elemento);
    }
    public List <int> GetCartasTienda()
    {
        return cartasTienda;
    }
    public void AñadirElementoTienda(int elemento)
    {
        cartasTienda.Add(elemento);
    }
    public List<string> GetDesbloqueables()
    {
        return desbloqueables;
    }
    public void AñadirDuelista(string duelista)
    {
        desbloqueables.Add(duelista);
    }
    public int[] GetVictorias()
    {
        return victorias;
    }
    public void SetElementoVictoria(int pos)
    {
        victorias[pos] += 1;
    }
    public void SetElementoDuelosJugados(int pos)
    {
        duelosJugados[pos] += 1;
    }
    public void LimpiarElementoDuelosJugados(int pos)
    {
        duelosJugados[pos] =0;
    }
    public int[] GetDuelosJugados()
    {
        return duelosJugados;
    }
    public int[] GetDerrotas()
    {
        return derrotas;
    }
    public void SetElementoDerrota(int pos)
    {
        derrotas[pos] += 1;
    }
    public int[] GetMagos()
    {
        return magos;
    }
    public string GetFase()
    {
        return obtenerFase;
    }
    public void SetFase(string nueva)
    {
        obtenerFase = nueva;
    }
    public void SetMagos(int indice, int valor)
    {
        magos[indice] = valor;
    }
    public void SetVictoiras(int[] cambiar)
    {
        victorias = cambiar;
    }
    public void SetDerrotas(int[] cambiar)
    {
        derrotas = cambiar;
    }
    public void SetEsModoHistoriaCompleto(bool estado)
    {
        modoHistoriaCompleto = estado;
    }
    public bool GetModoHistoriaCompleto()
    {
        return modoHistoriaCompleto;
    }
    public void ReiniciarDatos()
    {

        cofre.Clear();
        cantidadCofre.Clear();
        nuevasCartas.Clear();
        deckUsuario.Clear();
        estrellas = 0;
        desbloqueoSeto2 = false;
        Idndueva = 0;
        magos = new int[5];
        victorias = new int[40];
        derrotas = new int[40];
        historia = 0;
        eventosHistoria = 0;
        cartasTienda.Clear();
        desbloqueables.Clear();
        modoHistoriaCompleto = false;
    }
    public void Cargar()
    {
        DatosJugador datos=SistemaGuardado.Cargar1();
        historia = datos.historia;
        estrellas = datos.estrellas;
        deckUsuario = datos.deckUsuario;
        nombreJugador = datos.nombreJugador;
        slot = datos.slot;
    }
    public void Cargar2()
    {
        DatosJugador datos = SistemaGuardado.Cargar2();
        historia = datos.historia;
        estrellas = datos.estrellas;
        deckUsuario = datos.deckUsuario;
        nombreJugador = datos.nombreJugador;
        slot = datos.slot;
    }
    public void Cargar3()
    {
        DatosJugador datos = SistemaGuardado.Cargar3();
        historia = datos.historia;
        estrellas = datos.estrellas;
        deckUsuario = datos.deckUsuario;
        nombreJugador = datos.nombreJugador;
        slot = datos.slot;
    }


  
}
