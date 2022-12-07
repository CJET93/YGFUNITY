using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class JuegoOnline : MonoBehaviourPun
{
    // Start is called before the first frame update
    public Campo campo;
    private Fade fade;
    private ClonOnline clon;
    private ImportadorTextos txt;
    private List<int> defensasAltas = new List<int>();
    private List<int> cartasCampo = new List<int>();
    public InterfazOnline interfaz;
    private List<int> deckUsuario = new List<int>();
    public List<int> deckCpu = new List<int>();
    private bool turnoUsuario = true;
    private int cantDecksUario = 40;
    private int cantTurnos = 0;
    private int cantDeckCpu = 40;
    private int vidaUsuario = 0;
    private int vidaCpu = 0;
    public int rankPoints;
    private Color color;
    private bool primerAtaque = true;
    private int indiceCarta;
    public Camara camara;
    public ControlesOnline controles;
    public CuadroUsuarioOnline cuadroUsuario;
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    public int cualCampo;
    public string espadasLusReveladora;
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosDuelo datosDuelo;
    private DatosJuego datosJuego;
    private string perderPorCartas;
    private int ataqueFinalCpu;
    private int defensaFinalCpu;
    public transicion transicion;
    public int tamañoMano;
    private bool finJuego;
    //pantalla final de duelo
    public bool nuevaCarta = false;
    private int estrellasGanadas;
    private string rangoObtenido;
    private string duelistaDuelo;
    private int idCarta;
    public int ataquePromedioCpu;
    public int defensaPromedioCpu;
    public int ataquePromedio;
    public int defensaPromedio;
    private string nombreCarta;
    public int contadorCartasDestruidas = 0;
    private string condicionVictoria;
    //al destruir un monstruo en ataque y hacer daño
    private int ataquesEfectivos;
    // al ganar un ataque de la cpu con carta en defensa y hacerle ddaño
    private int defensasEfectivas;
    // colocar una carta boca abajo, tambien cuentan monstruos
    private int cartasBocaAbajo;
    //fusion correcta
    private int fusionCorrecta;
    // equipos correctos
    private int equiposCorrectos;
    //magicas activadas
    private int magicasUsadas;
    //trampas activadas
    private int trampasActiadas;
    private int ataquePromedioDeck;
    private int defensaPromedioDeck;
    private static string VICTORIA_EXODIA = "Victoria por Exodia";
    private static string VICTORIA_NORMAL = "Aniquilación Total";
    private static string VICTORIA_TECNICA = "Victora por Desgaste";

    public int AtaquesEfectivos { get => ataquesEfectivos; set => ataquesEfectivos = value; }
    public int DefensasEfectivas { get => defensasEfectivas; set => defensasEfectivas = value; }
    public int CartasBocaAbajo { get => cartasBocaAbajo; set => cartasBocaAbajo = value; }
    public int FusionCorrecta { get => fusionCorrecta; set => fusionCorrecta = value; }
    public int EquiposCorrectos { get => equiposCorrectos; set => equiposCorrectos = value; }
    public int MagicasUsadas { get => magicasUsadas; set => magicasUsadas = value; }
    public int TrampasActiadas { get => trampasActiadas; set => trampasActiadas = value; }
    public int AtaquePromedioDeck { get => ataquePromedioDeck; set => ataquePromedioDeck = value; }
    public int DefensaPromedioDeck { get => defensaPromedioDeck; set => defensaPromedioDeck = value; }
    public string CondicionVictoria { get => condicionVictoria; set => condicionVictoria = value; }

    public bool esJugadorUno;

    private void Awake()
    {
        finJuego = false;
        cualCampo = -1;
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
        if (datosDuelo.GetModoHistoria() == true)
        {
            if (datosDuelo.GetIdDuelista() - 1 == 4 || datosDuelo.GetIdDuelista() - 1 == 35)
            {
                interfaz.ActualizarMaterialCampo(1);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 37)
            {
                interfaz.ActualizarMaterialCampo(6);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 23 || datosDuelo.GetIdDuelista() - 1 == 24)
            {
                interfaz.ActualizarMaterialCampo(0);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 29 || datosDuelo.GetIdDuelista() - 1 == 30)
            {
                interfaz.ActualizarMaterialCampo(2);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 25 || datosDuelo.GetIdDuelista() - 1 == 26)
            {
                interfaz.ActualizarMaterialCampo(5);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 27 || datosDuelo.GetIdDuelista() - 1 == 28)
            {
                interfaz.ActualizarMaterialCampo(4);
            }
            else if (datosDuelo.GetIdDuelista() - 1 == 32 || datosDuelo.GetIdDuelista() - 1 == 33)
            {
                interfaz.ActualizarMaterialCampo(3);
            }
        }

        txt = GameObject.Find("Listas").GetComponent<ImportadorTextos>();
        fade = GameObject.Find("fade").GetComponent<Fade>();
        clon = GameObject.Find("Clon").GetComponent<ClonOnline>();


    }
    void Start()
    {
        esJugadorUno = datosJuego.esJugadorUno;
        condicionVictoria = VICTORIA_NORMAL;
        tamañoMano = int.Parse(txt.GetSuerteCpu()[datosDuelo.GetIdDuelista()]);
        ataquePromedio = 0;
        ataquePromedioCpu = 0;
        defensaPromedio = 0;
        defensaPromedioCpu = 0;
        rankPoints = 50;
        Cursor.visible = false;
        int [] temporal= new int[20];
        temporal[0] = 45;
        temporal[1] = 150;
        temporal[2] = 300;
        temporal[3] = 650;
        for (int i = 0; i < 10; i++)
        {
            deckUsuario.Add(temporal[0]);
            deckCpu.Add(temporal[0]);
            //AtaquePromedioDeck += int.Parse((string)txt.getatk().GetValue(deckUsuario[i]));
            //defensaPromedioDeck += int.Parse((string)txt.getdef().GetValue(deckUsuario[i]));


        }
        for (int i = 0; i < 10; i++)
        {
            deckUsuario.Add(temporal[1]);
            deckCpu.Add(temporal[1]);
            //AtaquePromedioDeck += int.Parse((string)txt.getatk().GetValue(deckUsuario[i]));
            //defensaPromedioDeck += int.Parse((string)txt.getdef().GetValue(deckUsuario[i]));


        }
        for (int i = 0; i < 10; i++)
        {
            deckUsuario.Add(temporal[2]);
            deckCpu.Add(temporal[2]);
            //AtaquePromedioDeck += int.Parse((string)txt.getatk().GetValue(deckUsuario[i]));
            //defensaPromedioDeck += int.Parse((string)txt.getdef().GetValue(deckUsuario[i]));


        }
        for (int i = 0; i < 10; i++)
        {
            deckUsuario.Add(temporal[3]);
            deckCpu.Add(temporal[3]);
            //AtaquePromedioDeck += int.Parse((string)txt.getatk().GetValue(deckUsuario[i]));
            //defensaPromedioDeck += int.Parse((string)txt.getdef().GetValue(deckUsuario[i]));


        }
        AtaquePromedioDeck /= 40;
        defensaPromedioDeck /= 40;
       // deckCpu = datosJuego.GetDeckUsuario();
        // ordenar cartas en ataque del campo usuario
        //organizar los ataques del usuario y ponerlos en un nuevo array temporal
        //dependiente de la suerte

        perderPorCartas = "";
        defensasAltas.Add(45);
        defensasAltas.Add(47);
        defensasAltas.Add(101);
        defensasAltas.Add(105);
        defensasAltas.Add(292);
        defensasAltas.Add(142);
        defensasAltas.Add(105);
        defensasAltas.Add(301);
        StartCoroutine(AnimacionVida());

        //CreasInstancais();
        //carta.obtenerCarta();
    }
    IEnumerator AnimacionVida()
    {
        //si el ataque del usuario es mayor al de la cpu
        int constante = 50;
        bool inicio = false;

        while (vidaUsuario < 8000)
        {
            if (vidaUsuario >= 5000 && inicio == false)
            {
                cuadroUsuario.InicioJuego();
                inicio = true;
                constante = 20;
            }
            if (vidaUsuario >= 7000)
            {
                constante = 10;
            }

            vidaCpu += constante;
            vidaUsuario += constante;
            interfaz.vidaUsuarioT.text = "" + GetVidaUsuario();
            interfaz.vidaCpuT.text = "" + GetVidaCpu();
            yield return null;
        }
        vidaUsuario = 8000;
        vidaCpu = 8000;
        Time.timeScale = 1.2f;
        interfaz.datosCarta.SetActive(true);
        interfaz.datosCartaCpu.SetActive(true);

        NuevaCartaUsuario();
    }
    public string GetNomDuelista()
    {
        return datosDuelo.GetDuelistaCpu();
    }
    // estos cuatro metodos de obtener ataque es el ataque final y la defensa finalnque se tiene al equipar en
    //la mano de la cpu por fusion o por carta magica
    public void OrdenarPorAtaqueCpu()
    {
        int[] ataqueTemp = new int[deckCpu.Count];
        int cantidad = deckCpu.Count;
        //carta c = GetComponent<carta>();
        for (int i = 0; i < cantidad; i++)
        {
            ataqueTemp[i] = int.Parse((string)txt.getatk().GetValue(deckCpu[i]));
        }
        for (int i = 0; i < cantidad - 1; i++)
        {
            for (int j = 0; j < (cantidad - 1) - i; j++)
            {
                if (ataqueTemp[j] < ataqueTemp[j + 1])
                {
                    int actualAtaque = ataqueTemp[j];
                    int actualPos = deckCpu[j];
                    deckCpu[j] = deckCpu[j + 1];
                    ataqueTemp[j] = ataqueTemp[j + 1];

                    ataqueTemp[j + 1] = actualAtaque;
                    deckCpu[j + 1] = actualPos;
                }
            }

        }
    }
    public int ObtenerAtaqueMasAlto()
    {
        return int.Parse((string)txt.getatk().GetValue(deckCpu[0]));
    }
    public int ObtenerDefensaMasAlta()
    {
        return int.Parse((string)txt.getdef().GetValue(deckCpu[0]));
    }
    public bool GetFinJuego()
    {
        return finJuego;
    }
    public void OrdenarPorDefCpu()
    {
        int[] defTemp = new int[deckCpu.Count];
        int cantidad = deckCpu.Count;
        //carta c = GetComponent<carta>();
        for (int i = 0; i < cantidad; i++)
        {
            defTemp[i] = int.Parse((string)txt.getdef().GetValue(deckCpu[i]));
        }
        for (int i = 0; i < cantidad - 1; i++)
        {
            for (int j = 0; j < (cantidad - 1) - i; j++)
            {
                if (defTemp[j] < defTemp[j + 1])
                {
                    int actualAtaque = defTemp[j];
                    int actualPos = deckCpu[j];
                    deckCpu[j] = deckCpu[j + 1];
                    defTemp[j] = defTemp[j + 1];

                    defTemp[j + 1] = actualAtaque;
                    deckCpu[j + 1] = actualPos;
                }
            }

        }
    }
    public void OrdenarPorMT()
    {
        int[] ataqueTemp = new int[40];
        int cantidad = deckCpu.Count;
        //carta c = GetComponent<carta>();
        for (int i = 0; i < cantidad; i++)
        {
            ataqueTemp[i] = int.Parse((string)txt.getatk().GetValue(deckCpu[i]));
        }
        for (int i = 0; i < cantidad - 1; i++)
        {
            for (int j = 0; j < (cantidad - 1) - i; j++)
            {
                if (ataqueTemp[j] > ataqueTemp[j + 1])
                {
                    int actualAtaque = ataqueTemp[j];
                    int actualPos = deckCpu[j];
                    deckCpu[j] = deckCpu[j + 1];
                    ataqueTemp[j] = ataqueTemp[j + 1];

                    ataqueTemp[j + 1] = actualAtaque;
                    deckCpu[j + 1] = actualPos;
                }
            }

        }
    }
    public void ReemplazarManoCpu()
    {
        deckCpu.Add(campo.GetManoCpu(0));
        campo.SetManoCpu(0, deckCpu[0]);
        deckCpu.RemoveAt(0);
    }
    public int GetTamañoMano()
    {
        return tamañoMano;
    }
    public void SetTamañoMano()
    {
        tamañoMano--;
    }
    public int GetAtaqueFinalCpu()
    {
        return ataqueFinalCpu;
    }
    public void SetAtaqueFinalCpu(int ataque)
    {
        ataqueFinalCpu = ataque;
    }
    public int GetDefensaFinalCpu()
    {
        return defensaFinalCpu;
    }
    public void SetDefensaFinalCpu(int defensa)
    {
        defensaFinalCpu = defensa;
    }
    public int GetIdCarta()
    {
        return idCarta;
    }
    public string GetnombreCarta()
    {
        return nombreCarta;
    }
    public string GetDuelistaDuelo()
    {
        return duelistaDuelo;
    }
    public int GetEstrellas()
    {
        return estrellasGanadas;
    }
    public string GetRango()
    {
        return rangoObtenido;
    }
    public string GetPerderPorCartas()
    {
        return perderPorCartas;
    }
    public void SetPerderPorCartas(string duelista)
    {
        perderPorCartas = duelista;
    }
    public string GetEspadasLuzReveladora()
    {
        return espadasLusReveladora;
    }
    public void SetEspadasLuzReveladora(string cambio)
    {
        espadasLusReveladora = cambio;
    }
    public int GetCampoModificado()
    {
        return cualCampo;
    }
    public void SetCampoModificado(int modificador)
    {
        cualCampo = modificador;
    }
    [PunRPC]
    public void pruebaCaros()
    {
        int contador = 0;
        if (espadasLusReveladora.Contains("usuario"))
        {
            if (espadasLusReveladora.Contains("3"))
            {
                espadasLusReveladora = "usuario2";
            }
            else if (espadasLusReveladora.Contains("2"))
            {
                espadasLusReveladora = "usuario1";
            }
            else if ((espadasLusReveladora.Contains("1")))
            {
                espadasLusReveladora = "usuario0";
            }
            else
            {
                espadasLusReveladora = "";
            }
        }

        bool salir = false;
        for (int i = 0; i < 5 && salir == false; i++)
        {
            if (campo.GetManoUsuario(i) == 0)
            {
                if (deckUsuario.Count > 0)
                {
                    int rand = Random.Range(0, deckUsuario.Count);
                    int carta = deckUsuario[rand];
                    deckUsuario.RemoveAt(rand);
                    campo.SetManoUsuario(i, carta);
                    clon.CreasInstancias(i, true);
                    contador++;

                }

                else
                {
                    contador++;
                    salir = true;
                }



            }
        }
        clon.AnimacionInstancias(contador);
    }
    public void NuevaCartaUsuario()
    {
        if(esJugadorUno)
            base.photonView.RPC("pruebaCaros", RpcTarget.All);
    }
    public void NuevaCartaCpu()
    {//
        int contador = 0;
        if (espadasLusReveladora.Contains("cpu"))
        {
            if (espadasLusReveladora.Contains("3"))
            {
                espadasLusReveladora = "cpu2";
            }
            else if (espadasLusReveladora.Contains("2"))
            {
                espadasLusReveladora = "cpu1";
            }
            else if (espadasLusReveladora.Contains("1"))
            {
                espadasLusReveladora = "cpu0";
            }
            else
            {
                espadasLusReveladora = "";
            }
        }
        bool salir = false;
        if (cantTurnos == 0)
        {
            if (datosDuelo.GetDuelistaCpu().Contains("Mago"))
            {
                ValidarPrimerTurnoCpu();
            }

        }
        for (int i = 0; i < 5 && salir == false; i++)
        {
            if (cantTurnos == 0)
            {
                if (campo.GetManoCpu(i) == 0)
                {
                    int rand = Random.Range(0, deckCpu.Count);
                    int carta = deckCpu[rand];
                    deckCpu.RemoveAt(rand);
                    campo.SetManoCpu(i, carta);



                }
                clon.CreasInstancias(i,false);
                contador++;
            }
            else
            {
                if (campo.GetManoCpu(i) == 0)
                {
                    if (deckCpu.Count > 0)
                    {
                        int rand = Random.Range(0, deckCpu.Count);
                        int carta = deckCpu[rand];
                        deckCpu.RemoveAt(rand);
                        campo.SetManoCpu(i, carta);
                        clon.CreasInstancias(i,false);
                        contador++;
                    }
                    else
                    {
                        contador++;
                        salir = true;
                    }
                }
            }

        }
        clon.AnimacionInstanciasCpu(contador);

    }
    //NIVEL 10 DE LOGICA CPU
    public void InicioLogicaCpu()
    {



        int posCartaCpu = controles.GetIndiceAtaqueCpu();
        if (cantTurnos == 0)
        {
            posCartaCpu = 0;
        }

        bool hayAtaque = false;
        for (int i = 0; i < 5 && hayAtaque == false; i++)
        {
            if (campo.GetAtaquesCpu(i) == 1)
            {
                hayAtaque = true;
                posCartaCpu = i;
            }
        }
        if (primerAtaque == true)
        {
            posCartaCpu = indiceCarta;
            controles.SetIndiceAtaqueCpu(posCartaCpu);
            primerAtaque = false;
        }
        StopAllCoroutines();
        StartCoroutine(AnimacionEmpezarAtaqueCpu(posCartaCpu, hayAtaque));


    }
    IEnumerator AnimacionEmpezarAtaqueCpu(int posCartaCpu, bool hayAtaque)
    {
        if (hayAtaque == true)
        {

            int indice = controles.GetIndiceAtaqueCpu();
            int cuadro = indice;
            yield return new WaitForSeconds(0.09f);
            while (cuadro != posCartaCpu)
            {
                ReproducirEfectoMover();
                if (posCartaCpu > indice)
                {
                    indice++;
                    cuadro++;

                    interfaz.ActualizarUICpuCampo(indice);

                    interfaz.MoverApuntadorDerecha();
                }
                else
                {
                    indice--;
                    cuadro--;

                    interfaz.ActualizarUICpuCampo(indice);

                    interfaz.MoverApuntadorIzquierda();
                }

                yield return new WaitForSeconds(0.09f);
            }
        }

        LogicaCpu(posCartaCpu, hayAtaque);
    }
    public void LogicaCpu(int posCartaCpu, bool hayAtaque)
    {
        for (int i = 0; i < 5 && hayAtaque == false; i++)
        {
            if (campo.GetAtaquesCpu(i) == 1)
            {
                hayAtaque = true;
                posCartaCpu = i;
            }
        }
        if (hayAtaque == true)
        {
            if (primerAtaque == true)
            {
                posCartaCpu = indiceCarta;
                primerAtaque = false;
            }
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) == null)
                {
                    contador++;
                }
            }
            if (contador < 5)
            {


                //organizar los ataques del usuario y ponerlos en un nuevo array temporal
                int[] ataqueTemp = new int[5];
                int[] posTemp = new int[5];
                int[] defensaTemp = new int[5];
                int[] atributoTemp = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                        atributoTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().GetGuardianStarA();
                    }
                    else
                    {
                        ataqueTemp[i] = 0;
                        defensaTemp[i] = 0;
                        atributoTemp[i] = 0;
                    }

                    posTemp[i] = i;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4 - i; j++)
                    {
                        if (ataqueTemp[j] < ataqueTemp[j + 1])
                        {
                            int actualAtaque = ataqueTemp[j];
                            int actualPos = posTemp[j];
                            int actualDefenssa = defensaTemp[j];
                            int actualAtributo = atributoTemp[j];
                            ataqueTemp[j] = ataqueTemp[j + 1];
                            posTemp[j] = posTemp[j + 1];
                            defensaTemp[j] = defensaTemp[j + 1];
                            ataqueTemp[j + 1] = actualAtaque;
                            posTemp[j + 1] = actualPos;
                            defensaTemp[j + 1] = actualDefenssa;
                            atributoTemp[j] = atributoTemp[j + 1];
                            atributoTemp[j + 1] = actualAtributo;

                        }
                    }



                }
                //inicio de logica de ataque
                bool detener = false;
                int ataqueCpu = clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque();
                int defensaCpu = clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa();
                int posCartaU = -1;
                for (int i = 0; i < 5 && detener == false; i++)
                {
                    ataqueCpu = clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque();
                    defensaCpu = clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa();
                    if (clon.getCartaCampoU(posTemp[i]) != null)
                    {
                        if (clon.GetCartaCpu(posCartaCpu) != null && campo.GetAtaquesCpu(posCartaCpu) != 0 && !espadasLusReveladora.Contains("usuario"))
                        {
                            if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                            {
                                if (clon.getCartaCampoU(posTemp[i]).GetComponent<carta>().getPos() == 1)
                                {
                                    string favorable = LogicaAtributo(posTemp[i], posCartaCpu);
                                    if (favorable.Equals("atributoCpu"))
                                    {
                                        ataqueCpu += 500;

                                    }
                                    else if (favorable.Equals("atributoUsuario"))
                                    {
                                        ataqueCpu -= 500;

                                    }
                                    if (ataqueCpu > ataqueTemp[i])
                                    {
                                        if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa() - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 500)
                                        {
                                            detener = true;
                                            campo.SetAtaquesCpu(posCartaCpu, 0);
                                            int indice = -1;
                                            bool salir = false;
                                            for (int k = 0; k < 5 && salir == false; k++)
                                            {
                                                if (posTemp[i] == campo.GetPosCampo(k))
                                                {

                                                    salir = true;
                                                }
                                                indice++;
                                            }

                                            posCartaU = posTemp[i];
                                            for (int x = 0; x < 5; x++)
                                            {
                                                if (clon.getCartaCampoU(x) != null)
                                                {
                                                    if (clon.getCartaCampoU(x).GetComponent<carta>().getPos() == 1)
                                                    {
                                                        clon.getCartaCampoU(x).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
                                                    }
                                                    else
                                                    {
                                                        clon.getCartaCampoU(x).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 90, 0));
                                                    }

                                                }
                                            }
                                            interfaz.SetEstadoMano(false);
                                            if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getPos() == 0)
                                            {
                                                ReproducirCambiarPos();
                                                clon.CambiarPosCartaCpu(posCartaCpu);
                                            }
                                            ReproducirEfectoSeleccionar();
                                            cuadroUsuario.TableroBatallaCpu(true);
                                            camara.FijarAtaque();
                                            if (ParametrosActivacionTrampas(0, posCartaCpu) == -1)
                                            {
                                                controles.AtaqueCpu(posCartaU, posCartaCpu, posCartaU, "ataqueCpu");
                                            }
                                            else
                                            {
                                                controles.AtaqueCpu(ParametrosActivacionTrampas(0, posCartaCpu), posCartaCpu, posCartaU, "trampaUsuario");
                                            }
                                            campo.SetAtaquesCpu(posCartaCpu, 0);
                                            clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().SetDatosCarta(1);
                                        }


                                    }

                                }
                                else
                                {
                                    string favorable = LogicaAtributo(posTemp[i], posCartaCpu);
                                    if (favorable.Equals("atributoCpu"))
                                    {
                                        ataqueCpu += 500;

                                    }
                                    else if (favorable.Equals("atributoUsuario"))
                                    {
                                        ataqueCpu -= 500;

                                    }
                                    if (ataqueCpu > defensaTemp[i])
                                    {
                                        if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa() - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 500)
                                        {
                                            detener = true;
                                            campo.SetAtaquesCpu(posCartaCpu, 0);
                                            int indice = -1;
                                            bool salir = false;
                                            for (int k = 0; k < 5 && salir == false; k++)
                                            {
                                                if (posTemp[i] == campo.GetPosCampo(k))
                                                {

                                                    salir = true;
                                                }
                                                indice++;
                                            }
                                            posCartaU = posTemp[i];
                                            interfaz.SetEstadoMano(false);
                                            if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getPos() == 0)
                                            {
                                                ReproducirCambiarPos();
                                                clon.CambiarPosCartaCpu(posCartaCpu);
                                            }
                                            ReproducirEfectoSeleccionar();
                                            cuadroUsuario.TableroBatallaCpu(true);
                                            camara.FijarAtaque();
                                            if (ParametrosActivacionTrampas(0, posCartaCpu) == -1)
                                            {
                                                controles.AtaqueCpu(posCartaU, posCartaCpu, posCartaU, "ataqueCpu");
                                            }
                                            else
                                            {
                                                controles.AtaqueCpu(ParametrosActivacionTrampas(0, posCartaCpu), posCartaCpu, posCartaU, "trampaUsuario");
                                            }
                                            campo.SetAtaquesCpu(posCartaCpu, 0);
                                            clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().SetDatosCarta(1);
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (clon.getCartaCampoU(posTemp[i]).GetComponent<carta>().GetDatosCarta() == 1)
                                {
                                    if (clon.getCartaCampoU(posTemp[i]).GetComponent<carta>().getPos() == 1)
                                    {
                                        string favorable = LogicaAtributo(posTemp[i], posCartaCpu);
                                        if (favorable.Equals("atributoCpu"))
                                        {
                                            ataqueCpu += 500;

                                        }
                                        else if (favorable.Equals("atributoUsuario"))
                                        {
                                            ataqueCpu -= 500;

                                        }
                                        if (ataqueCpu > ataqueTemp[i])
                                        {
                                            if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa() - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 500)
                                            {
                                                detener = true;
                                                campo.SetAtaquesCpu(posCartaCpu, 0);
                                                int indice = -1;
                                                bool salir = false;
                                                for (int k = 0; k < 5 && salir == false; k++)
                                                {
                                                    if (posTemp[i] == campo.GetPosCampo(k))
                                                    {

                                                        salir = true;
                                                    }
                                                    indice++;
                                                }

                                                posCartaU = posTemp[i];

                                                interfaz.SetEstadoMano(false);
                                                for (int x = 0; x < 5; x++)
                                                {
                                                    if (clon.getCartaCampoU(x) != null)
                                                    {
                                                        if (clon.getCartaCampoU(x).GetComponent<carta>().getPos() == 1)
                                                        {
                                                            clon.getCartaCampoU(x).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
                                                        }
                                                        else
                                                        {
                                                            clon.getCartaCampoU(x).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 90, 0));
                                                        }

                                                    }
                                                }
                                                if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getPos() == 0)
                                                {
                                                    ReproducirCambiarPos();
                                                    clon.CambiarPosCartaCpu(posCartaCpu);
                                                }
                                                ReproducirEfectoSeleccionar();
                                                cuadroUsuario.TableroBatallaCpu(true);
                                                camara.FijarAtaque();
                                                if (ParametrosActivacionTrampas(0, posCartaCpu) == -1)
                                                {
                                                    controles.AtaqueCpu(posCartaU, posCartaCpu, posCartaU, "ataqueCpu");
                                                }
                                                else
                                                {
                                                    controles.AtaqueCpu(ParametrosActivacionTrampas(0, posCartaCpu), posCartaCpu, posCartaU, "trampaUsuario");
                                                }
                                                campo.SetAtaquesCpu(posCartaCpu, 0);
                                                clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().SetDatosCarta(1);
                                            }


                                        }

                                    }
                                    else
                                    {
                                        string favorable = LogicaAtributo(posTemp[i], posCartaCpu);
                                        if (favorable.Equals("atributoCpu"))
                                        {
                                            ataqueCpu += 500;

                                        }
                                        else if (favorable.Equals("atributoUsuario"))
                                        {
                                            ataqueCpu -= 500;

                                        }
                                        if (ataqueCpu > defensaTemp[i])
                                        {
                                            if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa() - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 500)
                                            {
                                                detener = true;
                                                campo.SetAtaquesCpu(posCartaCpu, 0);
                                                int indice = -1;
                                                bool salir = false;
                                                for (int k = 0; k < 5 && salir == false; k++)
                                                {
                                                    if (posTemp[i] == campo.GetPosCampo(k))
                                                    {

                                                        salir = true;
                                                    }
                                                    indice++;
                                                }
                                                posCartaU = posTemp[i];
                                                interfaz.SetEstadoMano(false);
                                                if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getPos() == 0)
                                                {
                                                    ReproducirCambiarPos();
                                                    clon.CambiarPosCartaCpu(posCartaCpu);
                                                }
                                                ReproducirEfectoSeleccionar();
                                                cuadroUsuario.TableroBatallaCpu(true);
                                                camara.FijarAtaque();
                                                if (ParametrosActivacionTrampas(0, posCartaCpu) == -1)
                                                {
                                                    controles.AtaqueCpu(posCartaU, posCartaCpu, posCartaU, "ataqueCpu");
                                                }
                                                else
                                                {
                                                    controles.AtaqueCpu(ParametrosActivacionTrampas(0, posCartaCpu), posCartaCpu, posCartaU, "trampaUsuario");
                                                }
                                                campo.SetAtaquesCpu(posCartaCpu, 0);
                                                clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().SetDatosCarta(1);
                                            }

                                        }
                                    }
                                }

                                else
                                {
                                    posCartaU = posTemp[i];
                                }
                            }


                        }
                    }



                }
                if (detener == false)
                {

                    if (posCartaU != -1 && !espadasLusReveladora.Contains("usuario"))
                    {
                        if (LogicaDeAtaque(posCartaCpu) == true)
                        {
                            campo.SetAtaquesCpu(posCartaCpu, 0);
                            int indice = -1;
                            bool salir = false;
                            for (int k = 0; k < 5 && salir == false; k++)
                            {
                                if (posTemp[posCartaU] == campo.GetPosCampo(k))
                                {

                                    salir = true;
                                }
                                indice++;
                            }
                            interfaz.SetEstadoMano(false);
                            if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getPos() == 0)
                            {
                                ReproducirCambiarPos();
                                clon.CambiarPosCartaCpu(posCartaCpu);
                            }
                            camara.FijarAtaque();
                            cuadroUsuario.TableroBatallaCpu(true);
                            ReproducirEfectoSeleccionar();
                            if (ParametrosActivacionTrampas(0, posCartaCpu) == -1)
                            {
                                controles.AtaqueCpu(posCartaU, posCartaCpu, posCartaU, "ataqueCpu");
                            }
                            else
                            {
                                controles.AtaqueCpu(ParametrosActivacionTrampas(0, posCartaCpu), posCartaCpu, posCartaU, "trampaUsuario");
                            }

                            campo.SetAtaquesCpu(posCartaCpu, 0);
                            clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().SetDatosCarta(1);
                        }
                        else
                        {

                            clon.TiempoCambiarPos(posCartaCpu);



                        }
                    }
                    else
                    {
                        clon.TiempoCambiarPos(posCartaCpu);
                    }



                    //Invoke("AcabarTurno", 0.5f);
                }

            }
            else
            {
                if (!espadasLusReveladora.Contains("usuario"))
                {
                    if (vidaUsuario - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 0)
                    {
                        AtaqueDirectoCpu(posCartaCpu);
                    }
                    else if (clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getDefensa() - clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() <= 500 && clon.GetCartaCpu(posCartaCpu).GetComponent<carta>().getAtaque() > 0)
                    {
                        AtaqueDirectoCpu(posCartaCpu);
                    }
                    else
                    {
                        clon.TiempoCambiarPos(posCartaCpu);
                    }
                }
                else
                {
                    clon.TiempoCambiarPos(posCartaCpu);
                }


            }
        }
        else
        {
            interfaz.datosCartaCpu.SetActive(false);
            AcabarTurno();
        }
    }
    public void AtaqueDirectoCpu(int cartaPos)
    {
        StopAllCoroutines();
        campo.SetAtaquesCpu(cartaPos, 0);
        //clon.GetCartaCpu(cartaPos).GetComponent<carta>().SetPos(1);
        clon.GetCartaCpu(cartaPos).GetComponent<carta>().SetDatosCarta(1);
        clon.DesactivarComponentesCpu();
        if (clon.GetCartaCpu(cartaPos).GetComponent<carta>().getPos() == 0)
        {
            ReproducirCambiarPos();
            clon.CambiarPosCartaCpu(cartaPos);
        }
        camara.FijarAtaque();
        cuadroUsuario.TableroBatallaCpu(true);
        ReproducirEfectoSeleccionar();
        if (ParametrosActivacionTrampas(0, cartaPos) == -1)
        {
            controles.AtaqueDirectoCpu(0, cartaPos, "ataqueDirectoCpu");
        }
        else
        {
            controles.AtaqueDirectoCpu(ParametrosActivacionTrampas(0, cartaPos), cartaPos, "trampaUsuario");
        }

    }
    public void InicarBatalla(int posUsuario, int posCpu, string ataque)
    {
        StartCoroutine(EsperarAnimacionCamara(posUsuario, posCpu, ataque));
    }
    IEnumerator EsperarAnimacionCamara(int posUsuario, int posCpu, string ataque)
    {

        yield return new WaitForSeconds(1f);
        EntraBatalla(posUsuario, posCpu, ataque);



    }
    public void TerminarTurnoCpu()
    {
        turnoUsuario = true;
        cantTurnos++;
    }
    public int GetVidaCpu()
    {
        return vidaCpu;
    }
    public void SetVidaCpu(int vida)
    {
        vidaCpu = vidaCpu - vida;
        if (vidaCpu <= 0)
        {
            vidaCpu = 0;
        }
    }
    public void SetVidaCpuAumento(int vida)
    {
        vidaCpu = vida;
    }
    public int GetVidaUsuario()
    {
        return vidaUsuario;
    }
    public void SetVidaUsuario(int vida)
    {
        vidaUsuario = vidaUsuario - vida;
        if (vidaUsuario <= 0)
        {
            vidaUsuario = 0;
        }
    }
    public void SetVidaUsuarioAumento(int vida)
    {
        vidaUsuario = vida;
    }
    public int GetCantTurnos()
    {
        return cantTurnos;
    }
    public void SetCantTurnos()
    {
        cantTurnos++;
    }
    public void SetTurnoUsuario(bool turno)
    {
        turnoUsuario = turno;
    }
    public bool GetTurnoUsuario()
    {
        return turnoUsuario;
    }
    public void EntraBatalla(int posUsuario, int posCpu, string ataque)
    {
        if (ataque.Equals("ataqueDirectoCpu"))
        {
            string nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoCpu(posCpu)).ToString();
            if (nombreCarta.Length > 17)
            {
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
            }
            else if (nombreCarta.Length > 12)
            {
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.24f;
            }
            else
            {
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
            }
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoCpu(posCpu));
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
            clon.GetCartaCpu(posCpu).transform.Translate(0, 0, -2);
            clon.GetCartaCpu(posCpu).transform.rotation = (Quaternion.Euler(180, 0, 0));
            clon.GetCartaCpu(posCpu).GetComponent<Transform>().localPosition = new Vector3(-2.1f, 2.3f, -7.3f);
            clon.GetCartaCpu(posCpu).transform.localScale = new Vector3(1f, 1f, 0.1f);
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
            clon.GetCartaCpu(posCpu).GetComponent<carta>().SetDatosCarta(1);

        }
        else if (ataque.Equals("ataqueDirecto"))
        {
            nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoUsuario(posUsuario)).ToString();
            if (nombreCarta.Length > 17)
            {
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
            }
            else if (nombreCarta.Length > 12)
            {
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
            }
            else
            {
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
            }
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoUsuario(posUsuario));
            clon.getCartaCampoU(posUsuario).transform.Translate(0, 0, -2);
            clon.getCartaCampoU(posUsuario).transform.rotation = (Quaternion.Euler(180, 0, 0));
            clon.getCartaCampoU(posUsuario).GetComponent<Transform>().localPosition = new Vector3(2.1f, 2.3f, 7.3f);
            clon.getCartaCampoU(posUsuario).transform.localScale = new Vector3(1f, 1f, 0.1f);
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);

            clon.getCartaCampoU(posUsuario).GetComponent<carta>().SetDatosCarta(1);
        }
        else
        {
            if (ataque.Equals("ataqueCpu"))
            {
                //carta cpu
                string nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoCpu(posCpu)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.24f;
                }
                else
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoCpu(posCpu));
                clon.GetCartaCpu(posCpu).transform.Translate(0, 0, -2);
                clon.GetCartaCpu(posCpu).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.GetCartaCpu(posCpu).GetComponent<Transform>().localPosition = new Vector3(-2.1f, 2.3f, -7.3f);
                clon.GetCartaCpu(posCpu).transform.localScale = new Vector3(1f, 1f, 0.1f);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                clon.GetCartaCpu(posCpu).GetComponent<carta>().SetDatosCarta(1);
                //carta usuario
                nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoUsuario(posUsuario)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoUsuario(posUsuario));
                clon.getCartaCampoU(posUsuario).transform.Translate(0, 0, -2);
                clon.getCartaCampoU(posUsuario).transform.rotation = (Quaternion.Euler(-180, 0, 0));
                clon.getCartaCampoU(posUsuario).GetComponent<Transform>().localPosition = new Vector3(2.1f, 2.3f, -7.3f);
                clon.getCartaCampoU(posUsuario).transform.localScale = new Vector3(1f, 1f, 0.1f);
                if (clon.getCartaCampoU(posUsuario).GetComponent<carta>().getPos() == 1)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);
                }

                clon.getCartaCampoU(posUsuario).GetComponent<carta>().SetDatosCarta(1);
            }
            else if (ataque.Equals("ataqueUsuario"))
            {
                //carta cpu
                string nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoCpu(posCpu)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.24f;
                }
                else
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoCpu(posCpu));
                clon.GetCartaCpu(posCpu).transform.Translate(0, 0, -2);
                clon.GetCartaCpu(posCpu).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.GetCartaCpu(posCpu).GetComponent<Transform>().localPosition = new Vector3(-2.1f, 2.3f, 7.3f);

                clon.GetCartaCpu(posCpu).transform.localScale = new Vector3(1f, 1f, 0.1f);
                if (clon.GetCartaCpu(posCpu).GetComponent<carta>().getPos() == 1)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);
                }

                //carta usuario
                nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoUsuario(posUsuario)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(true);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoUsuario(posUsuario));
                clon.getCartaCampoU(posUsuario).transform.Translate(0, 0, -2);
                clon.getCartaCampoU(posUsuario).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.getCartaCampoU(posUsuario).GetComponent<Transform>().localPosition = new Vector3(2.1f, 2.3f, 7.3f);
                clon.getCartaCampoU(posUsuario).transform.localScale = new Vector3(1f, 1f, 0.1f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                clon.getCartaCampoU(posUsuario).GetComponent<carta>().SetDatosCarta(1);
            }
            else if (ataque.Equals("trampaUsuario"))
            {
                //carta cpu
                string nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoCpu(posCpu)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.24f;
                }
                else
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.text = "" + clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoCpu(posCpu));
                clon.GetCartaCpu(posCpu).transform.Translate(0, 0, -2);
                clon.GetCartaCpu(posCpu).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.GetCartaCpu(posCpu).GetComponent<Transform>().localPosition = new Vector3(-2.1f, 2.3f, -7.3f);
                clon.GetCartaCpu(posCpu).transform.localScale = new Vector3(4f, 3f, 0.1f);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                clon.GetCartaCpu(posCpu).GetComponent<carta>().SetDatosCarta(1);
                //carta usuario
                nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoUsuario(posUsuario)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().contenedorNombre.texture = clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().color[2];
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoUsuario(posUsuario));
                clon.getCartaCampoU(posUsuario).transform.Translate(0, 0, -2);
                clon.getCartaCampoU(posUsuario).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.getCartaCampoU(posUsuario).GetComponent<Transform>().localPosition = new Vector3(2.1f, 2.3f, -7.3f);
                clon.getCartaCampoU(posUsuario).transform.localScale = new Vector3(4f, 3f, 0.1f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                clon.getCartaCampoU(posUsuario).GetComponent<carta>().SetDatosCarta(1);
            }
            else if (ataque.Equals("trampaCpu"))
            {
                //carta cpu
                string nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoCpu(posCpu)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.24f;
                }
                else
                {
                    clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().contenedorNombre.texture = clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().color[2];
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                clon.GetCartaCpu(posCpu).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoCpu(posCpu));
                clon.GetCartaCpu(posCpu).transform.Translate(0, 0, -2);
                clon.GetCartaCpu(posCpu).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.GetCartaCpu(posCpu).GetComponent<Transform>().localPosition = new Vector3(-2.1f, 2.3f, 7.3f);
                clon.GetCartaCpu(posCpu).transform.localScale = new Vector3(4f, 3f, 0.1f);


                //carta usuario
                nombreCarta = txt.nombresCartas.GetValue(campo.GetCampoUsuario(posUsuario)).ToString();
                if (nombreCarta.Length > 17)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else if (nombreCarta.Length > 12)
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.18f;
                }
                else
                {
                    clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.fontSize = 0.30f;
                }
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().nombreCarta.text = nombreCarta;
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.text = "" + clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartasBatalla.GetValue(campo.GetCampoUsuario(posUsuario));
                clon.getCartaCampoU(posUsuario).transform.Translate(0, 0, -2);
                clon.getCartaCampoU(posUsuario).transform.rotation = (Quaternion.Euler(180, 0, 0));
                clon.getCartaCampoU(posUsuario).GetComponent<Transform>().localPosition = new Vector3(2.1f, 2.3f, 7.3f);
                clon.getCartaCampoU(posUsuario).transform.localScale = new Vector3(4f, 3f, 0.1f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().defensaB.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                clon.getCartaCampoU(posUsuario).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                clon.getCartaCampoU(posUsuario).GetComponent<carta>().SetDatosCarta(1);
            }
        }
        //carta.transform.Translate(0, 0, -2);
        //carta.transform.localScale = new Vector3(8f, 8f, 0);
        StartCoroutine(Batalla(posUsuario, posCpu, ataque));
    }
    IEnumerator Batalla(int i, int j, string ataque)
    {
        int daño = 0;
        yield return new WaitForSeconds(0.2f);
        if (ataque.Equals("ataqueDirectoCpu"))
        {
            int tiempo = 0;
            for (int x = 0; x < 180; x += 10)
            {
                yield return new WaitForSeconds(0.01f);
                clon.GetCartaCpu(j).transform.Rotate(0f, -10f, 0f);
                tiempo += 10;
                if (tiempo == 90 || tiempo == -90)
                {

                    clon.GetCartaCpu(j).transform.localScale = new Vector3(4f, 3f, 0.1f);
                    clon.GetCartaCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(180, -90, 0);
                    clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                    clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);


                }
            }
            tiempo = 0;
            daño = clon.GetCartaCpu(j).GetComponent<carta>().getAtaque();
            clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(163, 0, 0));
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(animacionBatalla(0, j, daño, "usuarioDirecto"));
        }
        else if (ataque.Equals("ataqueDirecto"))
        {
            campo.SetAtaquesUsuario(i, 0);
            int tiempo = 0;
            for (int x = 0; x < 180; x += 10)
            {
                yield return new WaitForSeconds(0.01f);
                clon.getCartaCampoU(i).transform.Rotate(0f, 10f, 0f);
                tiempo += 10;
                if (tiempo == 90 || tiempo == -90)
                {

                    clon.getCartaCampoU(i).transform.localScale = new Vector3(4f, 3f, 0.1f);
                    clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(180, 90, 0));
                    clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                    clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);

                }
            }
            tiempo = 0;
            clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(163, 0, 0));
            daño = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(animacionBatalla(i, 0, daño, "cpuDirecto"));
        }
        else
        {
            if (ataque.Equals("ataqueCpu"))
            {
                int tiempo = 0;
                for (int x = 0; x < 180; x += 10)
                {
                    yield return new WaitForSeconds(0.01f);
                    clon.getCartaCampoU(i).transform.Rotate(0f, 10f, 0f);
                    clon.GetCartaCpu(j).transform.Rotate(0f, -10f, 0f);
                    tiempo += 10;
                    if (tiempo == 90 || tiempo == -90)
                    {

                        clon.getCartaCampoU(i).transform.localScale = new Vector3(4f, 3f, 0.1f);
                        clon.GetCartaCpu(j).transform.localScale = new Vector3(4f, 3f, 0.1f);
                        clon.GetCartaCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(180, -90, 0);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(180, 90, 0));

                    }
                }
                tiempo = 0;
                clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(163, 0, 0));
                clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(163, 0, 0));
                yield return new WaitForSeconds(0.05f);
                LogicaBatallaCpu(i, j, ataque);

            }
            else if (ataque.Equals("trampaUsuario"))
            {
                interfaz.SetTiempoFlash(1f);
                interfaz.SetFlash(true);
                yield return new WaitForSeconds(1f);
                int tiempo = 0;
                for (int x = 0; x < 180; x += 10)
                {
                    yield return new WaitForSeconds(0.01f);
                    clon.getCartaCampoU(i).transform.Rotate(0f, 10f, 0f);
                    clon.GetCartaCpu(j).transform.Rotate(0f, -10f, 0f);
                    tiempo += 10;
                    if (tiempo == 90 || tiempo == -90)
                    {
                        clon.GetCartaCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(180, -90, 0);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        ///clon.GetClonCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(-200, 360, 180);
                        clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(180, 90, 0));


                    }
                }
                tiempo = 0;
                clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(163, 0, 0));
                clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(163, 0, 0));
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(animacionBatalla(i, j, 0, "trampaACpu"));


            }
            else if ((ataque.Equals("trampaCpu")))
            {
                interfaz.SetTiempoFlash(1f);
                interfaz.SetFlash(true);
                yield return new WaitForSeconds(1f);
                int tiempo = 0;
                for (int x = 0; x < 180; x += 10)
                {
                    yield return new WaitForSeconds(0.01f);
                    clon.getCartaCampoU(i).transform.Rotate(0f, 10f, 0f);
                    clon.GetCartaCpu(j).transform.Rotate(0f, -10f, 0f);
                    tiempo += 10;
                    if (tiempo == 90 || tiempo == -90)
                    {

                        clon.getCartaCampoU(i).transform.localScale = new Vector3(4f, 3f, 0.1f);
                        clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(180, 90, 0));
                        clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(180, -90, 0));
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);

                    }
                }
                tiempo = 0;
                clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(163, 0, 0));
                clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(163, 0, 0));

                yield return new WaitForSeconds(0.05f);
                StartCoroutine(animacionBatalla(i, j, 0, "trampaAUsuario"));
            }
            else
            {

                int tiempo = 0;
                for (int x = 0; x < 180; x += 10)
                {
                    yield return new WaitForSeconds(0.01f);
                    clon.getCartaCampoU(i).transform.Rotate(0f, 10f, 0f);
                    clon.GetCartaCpu(j).transform.Rotate(0f, -10f, 0f);
                    tiempo += 10;
                    if (tiempo == 90 || tiempo == -90)
                    {

                        clon.getCartaCampoU(i).transform.localScale = new Vector3(4f, 3f, 0.1f);
                        clon.GetCartaCpu(j).transform.localScale = new Vector3(4f, 3f, 0.1f);
                        clon.GetCartaCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(180, -90, 0);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(false);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.GetCartaCpu(j).GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                        clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(180, 90, 0));

                    }
                }
                tiempo = 0;
                clon.GetCartaCpu(j).transform.rotation = (Quaternion.Euler(163, 0, 0));
                clon.getCartaCampoU(i).transform.rotation = (Quaternion.Euler(163, 0, 0));
                yield return new WaitForSeconds(0.05f);
                LogicaBatallaUsuario(i, j, ataque);
            }



        }



    }
    public int GetCantDeckUsuario()
    {
        return cantDecksUario;
    }
    public int GetCantDeckCpu()
    {
        return cantDeckCpu;
    }
    public void SetCantDeckUsuario()
    {
        cantDecksUario--;
        interfaz.ActualizarDeckUsuario();
    }
    public void SetCantDeckCpu()
    {
        cantDeckCpu--;
        interfaz.ActualizarDeckCpu();
    }
    public void LogicaManoCpu()
    {
        clon.SetTipoCartaCpu("Monstruo");
        if (cantTurnos == 0)
        {
            bool salir = false;
            int ponerCartaCampo = -1;

            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Campo"))
                {
                    ponerCartaCampo = clon.ValidarEfectosManoCpu("Campo");
                    salir = true;
                }
            }
            if (ponerCartaCampo != -1)
            {
                if (!clon.GetClonCpu(ponerCartaCampo).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    clon.GetClonCpu(ponerCartaCampo).GetComponent<carta>().SetDatosCarta(1);
                }
                clon.SetTipoCartaCpu("campoArriba");
                clon.GetClonCpu(ponerCartaCampo);
                clon.SeleccionarCartaCpu(ponerCartaCampo);
            }
            else
            {
                clon.GetClonCpu(0);
                clon.SeleccionarCartaCpu(0);
            }

        }
        else
        {


            bool salir = false;
            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clon.GetCartaCpu(i) != null || clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                {
                    salir = true;
                }
            }
            if (salir == true)
            {
                salir = false;
                int ponerCartaCampo = -1;

                for (int i = 0; i < 5 && salir == false; i++)
                {
                    if (clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Campo"))
                    {
                        ponerCartaCampo = clon.ValidarEfectosManoCpu("Campo");
                        salir = true;
                    }
                    else if (clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Magica"))
                    {
                        ponerCartaCampo = clon.ValidarEfectosManoCpu("Magica");
                        salir = true;
                    }
                    else if (clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                    {
                        ponerCartaCampo = clon.ValidarEfectosManoCpu("Equipo");
                        salir = true;
                    }
                    else if (clon.GetClonCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        ponerCartaCampo = clon.ValidarEfectosManoCpu("Trampa");
                        salir = true;
                    }
                }
                if (ponerCartaCampo != -1)
                {
                    if (!clon.GetClonCpu(ponerCartaCampo).GetComponent<carta>().GetTipoCarta().Equals("Equipo") && !clon.GetClonCpu(ponerCartaCampo).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        clon.GetClonCpu(ponerCartaCampo).GetComponent<carta>().SetDatosCarta(1);
                    }
                    clon.SetTipoCartaCpu("campoArriba");
                    clon.GetClonCpu(ponerCartaCampo);
                    clon.SeleccionarCartaCpu(ponerCartaCampo);
                }
                else
                {
                    // ordenar cartas en ataque del campo usuario
                    //organizar los ataques del usuario y ponerlos en un nuevo array temporal
                    int[] ataqueTemp = new int[5];
                    int[] posTemp = new int[5];
                    int[] defensaTemp = new int[5];
                    //carta c = GetComponent<carta>();
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null)
                        {
                            ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                            defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                        }
                        else
                        {
                            ataqueTemp[i] = 0;
                            defensaTemp[i] = 0;
                        }

                        posTemp[i] = i;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4 - i; j++)
                        {
                            if (ataqueTemp[j] < ataqueTemp[j + 1])
                            {
                                int actualAtaque = ataqueTemp[j];
                                int actualPos = posTemp[j];
                                int actualDefenssa = defensaTemp[j];
                                ataqueTemp[j] = ataqueTemp[j + 1];
                                posTemp[j] = posTemp[j + 1];
                                defensaTemp[j] = defensaTemp[j + 1];
                                ataqueTemp[j + 1] = actualAtaque;
                                posTemp[j + 1] = actualPos;
                                defensaTemp[j + 1] = actualDefenssa;
                            }
                        }



                    }
                    //logica poner carta
                    bool detener = false;
                    for (int i = 0; i < 5 && detener == false; i++)
                    {
                        if (clon.getCartaCampoU(posTemp[0]) != null)
                        {
                            if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                            {
                                string favorable = LogicaAtributoMano(posTemp[0], i);
                                int ataqueCpu = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                                if (favorable.Equals("atributoCpu"))
                                {
                                    ataqueCpu += 500;

                                }
                                else if (favorable.Equals("atributoUsuario"))
                                {
                                    ataqueCpu -= 500;

                                }
                                if (ataqueCpu > ataqueTemp[0])
                                {
                                    clon.GetClonCpu(i);
                                    clon.SeleccionarCartaCpu(i);
                                    detener = true;

                                }
                            }
                            else
                            {
                                if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                                {
                                    string favorable = LogicaAtributoMano(posTemp[0], i);
                                    int ataqueCpu = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                                    if (favorable.Equals("atributoCpu"))
                                    {
                                        ataqueCpu += 500;

                                    }
                                    else if (favorable.Equals("atributoUsuario"))
                                    {
                                        ataqueCpu -= 500;

                                    }
                                    if (ataqueCpu > ataqueTemp[0])
                                    {
                                        clon.GetClonCpu(i);
                                        clon.SeleccionarCartaCpu(i);
                                        detener = true;

                                    }
                                }
                            }

                        }


                    }
                    if (detener == false)
                    {
                        //comprobar fusiones
                        int contador = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            if (clon.GetCartaCpu(i) != null)
                            {
                                contador++;
                            }
                        }
                        ataqueFinalCpu = 0;
                        bool fusionCampo = clon.ValidarSiFusionCampoCpu();
                        // ahora probabilidad de fusion
                        int aleatorio = Random.Range(1, 42);
                        int probFus = aleatorio * int.Parse(txt.GetPFCpu()[datosDuelo.GetIdDuelista()]);
                        if (contador < 5 && fusionCampo == false && probFus >= 81)
                        {
                            clon.ValidarSiFusion();
                        }
                        if (ataqueFinalCpu < ataqueTemp[0])
                        {
                            if (defensaFinalCpu < ataqueTemp[0])
                            {
                                if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                                {
                                    controles.GetListaDCartas().Clear();
                                }
                                else
                                {
                                    if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                                    {
                                        controles.GetListaDCartas().Clear();
                                    }
                                }
                            }
                        }
                        //buscar carta mas fuerte en defensa
                        ataqueTemp = new int[5];
                        posTemp = new int[5];
                        defensaTemp = new int[5];
                        //carta c = GetComponent<carta>();
                        for (int i = 0; i < 5; i++)
                        {
                            ataqueTemp[i] = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                            defensaTemp[i] = clon.GetClonCpu(i).GetComponent<carta>().getDefensa();
                            posTemp[i] = i;
                        }
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4 - i; j++)
                            {
                                if (defensaTemp[j] < defensaTemp[j + 1])
                                {
                                    int actualAtaque = ataqueTemp[j];
                                    int actualPos = posTemp[j];
                                    int actualDefenssa = defensaTemp[j];
                                    ataqueTemp[j] = ataqueTemp[j + 1];
                                    posTemp[j] = posTemp[j + 1];
                                    defensaTemp[j] = defensaTemp[j + 1];
                                    ataqueTemp[j + 1] = actualAtaque;
                                    posTemp[j + 1] = actualPos;
                                    defensaTemp[j + 1] = actualDefenssa;
                                }
                            }
                        }

                        clon.GetClonCpu(posTemp[0]);
                        clon.SeleccionarCartaCpu(posTemp[0]);


                    }
                }
            }
            else
            {
                // ordenar cartas en ataque del campo usuario
                //organizar los ataques del usuario y ponerlos en un nuevo array temporal
                int[] ataqueTemp = new int[5];
                int[] posTemp = new int[5];
                int[] defensaTemp = new int[5];
                //carta c = GetComponent<carta>();
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                    }
                    else
                    {
                        ataqueTemp[i] = 0;
                        defensaTemp[i] = 0;
                    }

                    posTemp[i] = i;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4 - i; j++)
                    {
                        if (ataqueTemp[j] < ataqueTemp[j + 1])
                        {
                            int actualAtaque = ataqueTemp[j];
                            int actualPos = posTemp[j];
                            int actualDefenssa = defensaTemp[j];
                            ataqueTemp[j] = ataqueTemp[j + 1];
                            posTemp[j] = posTemp[j + 1];
                            defensaTemp[j] = defensaTemp[j + 1];
                            ataqueTemp[j + 1] = actualAtaque;
                            posTemp[j + 1] = actualPos;
                            defensaTemp[j + 1] = actualDefenssa;
                        }
                    }



                }
                //logica poner carta
                bool detener = false;
                for (int i = 0; i < 5 && detener == false; i++)
                {
                    if (clon.getCartaCampoU(posTemp[0]) != null)
                    {
                        if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                        {
                            string favorable = LogicaAtributoMano(posTemp[0], i);
                            int ataqueCpu = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                            if (favorable.Equals("atributoCpu"))
                            {
                                ataqueCpu += 500;

                            }
                            else if (favorable.Equals("atributoUsuario"))
                            {
                                ataqueCpu -= 500;

                            }
                            if (ataqueCpu > ataqueTemp[0])
                            {
                                clon.GetClonCpu(i);
                                clon.SeleccionarCartaCpu(i);
                                detener = true;

                            }
                        }
                        else
                        {
                            if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                            {
                                string favorable = LogicaAtributoMano(posTemp[0], i);
                                int ataqueCpu = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                                if (favorable.Equals("atributoCpu"))
                                {
                                    ataqueCpu += 500;

                                }
                                else if (favorable.Equals("atributoUsuario"))
                                {
                                    ataqueCpu -= 500;

                                }
                                if (ataqueCpu > ataqueTemp[0])
                                {
                                    clon.GetClonCpu(i);
                                    clon.SeleccionarCartaCpu(i);
                                    detener = true;

                                }
                            }
                        }

                    }


                }
                if (detener == false)
                {
                    //comprobar fusiones
                    int contador = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {
                            contador++;
                        }
                    }
                    ataqueFinalCpu = 0;
                    bool fusionCampo = clon.ValidarSiFusionCampoCpu();
                    // ahora probabilidad de fusion
                    int aleatorio = Random.Range(1, 42);
                    int probFus = aleatorio * int.Parse(txt.GetPFCpu()[datosDuelo.GetIdDuelista()]);
                    if (contador < 5 && fusionCampo == false && probFus >= 81)
                    {
                        clon.ValidarSiFusion();
                    }
                    if (ataqueFinalCpu < ataqueTemp[0])
                    {
                        if (defensaFinalCpu < ataqueTemp[0])
                        {
                            if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                            {
                                controles.GetListaDCartas().Clear();
                            }
                            else
                            {
                                if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                                {
                                    controles.GetListaDCartas().Clear();
                                }
                            }
                        }
                    }

                    //buscar carta mas fuerte en defensa
                    ataqueTemp = new int[5];
                    posTemp = new int[5];
                    defensaTemp = new int[5];
                    //carta c = GetComponent<carta>();
                    for (int i = 0; i < 5; i++)
                    {
                        ataqueTemp[i] = clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                        defensaTemp[i] = clon.GetClonCpu(i).GetComponent<carta>().getDefensa();
                        posTemp[i] = i;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4 - i; j++)
                        {
                            if (defensaTemp[j] < defensaTemp[j + 1])
                            {
                                int actualAtaque = ataqueTemp[j];
                                int actualPos = posTemp[j];
                                int actualDefenssa = defensaTemp[j];
                                ataqueTemp[j] = ataqueTemp[j + 1];
                                posTemp[j] = posTemp[j + 1];
                                defensaTemp[j] = defensaTemp[j + 1];
                                ataqueTemp[j + 1] = actualAtaque;
                                posTemp[j + 1] = actualPos;
                                defensaTemp[j + 1] = actualDefenssa;
                            }
                        }
                    }

                    clon.GetClonCpu(posTemp[0]);
                    clon.SeleccionarCartaCpu(posTemp[0]);
                }
            }

        }
    }
    public bool ValidarPrimerTurnoCpu()
    {
        List<int> cartasCampo = new List<int>();
        bool entro = false;
        for (int i = 632; i < 638; i++)
        {
            cartasCampo.Add(i);
        }

        int actualCarta = Random.Range(1, 10);
        bool salir = false;
        //carta de campo
        actualCarta = Random.Range(1, 10);
        salir = false;
        if (actualCarta > 2)
        {
            for (int i = 0; i < deckCpu.Count && salir == false; i++)
            {
                if (cartasCampo.Contains(deckCpu[i]))
                {
                    campo.SetManoCpu(Random.Range(1, 4), deckCpu[i]);
                    deckCpu.RemoveAt(i);
                    entro = true;
                    salir = true;
                }
            }
        }
        if (entro == false)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public void LogicaBatallaCpu(int posUsuario, int posCpu, string ataque)
    {
        int cartaUsuarioPos = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getPos();
        int cartaCpuPos = clon.GetCartaCpu(posCpu).GetComponent<carta>().getPos();
        int ataqueUsuario = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
        int defensaUsuario = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
        int ataqueCpu = clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
        int defensaCpu = clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
        string destinoDaño = null;
        string favorable = LogicaAtributo(posUsuario, posCpu);
        int daño = 0;
        if (favorable.Equals("atributoCpu"))
        {
            ataqueCpu += 500;
            defensaCpu += 500;
        }
        else if (favorable.Equals("atributoUsuario"))
        {
            ataqueCpu -= 500;
            defensaCpu -= 500;
        }
        if (cartaUsuarioPos == 1)
        {
            if (ataqueCpu > ataqueUsuario)
            {
                destinoDaño = "usuario";
                daño = ataqueCpu - ataqueUsuario;
            }
            else if (ataqueCpu == ataqueUsuario)
            {
                destinoDaño = "dos";
            }
            else
            {
                destinoDaño = "cpu";
                daño = ataqueUsuario - ataqueCpu;
            }
        }
        else
        {
            if (ataqueCpu > defensaUsuario)
            {
                destinoDaño = "usuario";
            }
            else
            {
                destinoDaño = "cpuNoDestruir";
                daño = defensaUsuario - ataqueCpu;
                if (ataqueCpu < defensaUsuario)
                {
                    defensasEfectivas++;
                }
            }
        }
        StopAllCoroutines();
        StartCoroutine(animacionBatalla(posUsuario, posCpu, daño, destinoDaño));

    }
    public void LogicaBatallaUsuario(int posUsuario, int posCpu, string ataque)
    {

        int cartaUsuarioPos = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getPos();
        int cartaCpuPos = clon.GetCartaCpu(posCpu).GetComponent<carta>().getPos();
        int ataqueUsuario = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getAtaque();
        int defensaUsuario = clon.getCartaCampoU(posUsuario).GetComponent<carta>().getDefensa();
        int ataqueCpu = clon.GetCartaCpu(posCpu).GetComponent<carta>().getAtaque();
        int defensaCpu = clon.GetCartaCpu(posCpu).GetComponent<carta>().getDefensa();
        string destinoDaño = null;
        string favorable = LogicaAtributo(posUsuario, posCpu);
        int daño = 0;
        if (favorable.Equals("atributoCpu"))
        {
            ataqueCpu += 500;
            defensaCpu += 500;
        }
        else if (favorable.Equals("atributoUsuario"))
        {
            ataqueCpu -= 500;
            defensaCpu -= 500;
        }
        if (cartaCpuPos == 1)
        {
            if (ataqueUsuario > ataqueCpu)
            {
                ataquesEfectivos++;
                destinoDaño = "cpu";
                daño = ataqueUsuario - ataqueCpu;
            }
            else if (ataqueCpu == ataqueUsuario)
            {
                destinoDaño = "dos";
            }
            else
            {
                destinoDaño = "usuario";
                daño = ataqueCpu - ataqueUsuario;
            }
        }
        else
        {

            if (ataqueUsuario > defensaCpu)
            {
                destinoDaño = "cpu";
            }
            else
            {
                destinoDaño = "usuarioNoDestruir";
                daño = defensaCpu - ataqueUsuario;
            }
        }
        StopAllCoroutines();
        StartCoroutine(animacionBatalla(posUsuario, posCpu, daño, destinoDaño));


    }
    IEnumerator animacionBatalla(int usuarioPos, int cpuPos, int daño, string destinoDaño)
    {
        yield return new WaitForSeconds(0.03f);
        fade.InicioFade();
        yield return new WaitForSeconds(0.3f);
        if (destinoDaño.Equals("usuarioDirecto") || destinoDaño.Equals("cpuDirecto"))
        {
            interfaz.mostrarModificadorDirecto(usuarioPos, cpuPos, destinoDaño);
        }
        else if (destinoDaño.Equals("trampaACpu") || destinoDaño.Equals("trampaAUsuario"))
        {
            //no va nada aca pero es para que entre aca y no abajo
            interfaz.mostrarModificadorDirecto(usuarioPos, cpuPos, destinoDaño);

        }
        else
        {
            string favorable = LogicaAtributo(usuarioPos, cpuPos);
            if (!favorable.Equals(""))
            {
                interfaz.MostrarModificadores(usuarioPos, cpuPos);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }


        }
        interfaz.InicioAtaqueCarta(usuarioPos, cpuPos, destinoDaño);
        interfaz.InicioDestruirCarta(usuarioPos, cpuPos, destinoDaño);
        interfaz.MostrarDaño(daño, destinoDaño, usuarioPos, cpuPos);
        yield return new WaitForSeconds(0.03f);
        interfaz.ColorFlash(daño);
        if (daño < 1000)
        {
            interfaz.SetTiempoFlash(5f);
        }
        else if (daño < 2000)
        {
            interfaz.SetTiempoFlash(3f);
        }
        else
        {
            interfaz.SetTiempoFlash(1f);
        }

        interfaz.SetFlash(true);
        interfaz.desvanecerDaño();
        yield return new WaitForSeconds(1f);
        interfaz.FinBatalla(daño, destinoDaño, usuarioPos, cpuPos);
    }
    public void AcabarTurno()
    {
        controles.AcabarTurnoCpu();
    }
    public bool LogicaDeAtaque(int cartaCpu)
    {
        int ataqueCpu = clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque();
        int defensaCpu = clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getDefensa();
        int aleatorio = 0;
        if (ataqueCpu >= defensaCpu)
        {
            if (ataqueCpu == 0)
            {
                aleatorio = 0;
            }
            else if (ataqueCpu <= 400)
            {
                aleatorio = Random.Range(1, 5);

            }
            else if (ataqueCpu < 800)
            {
                aleatorio = Random.Range(1, 5);
            }
            else if (ataqueCpu < 2200)
            {
                aleatorio = Random.Range(1, 4);
                if (aleatorio != 2)
                {
                    aleatorio = Random.Range(1, 3);
                }
            }
            else if (ataqueCpu >= 2200)
            {

                aleatorio = Random.Range(1, 3);

                if (aleatorio != 2)
                {

                    aleatorio = Random.Range(1, 3);

                    if (aleatorio != 2)
                    {

                        aleatorio = Random.Range(1, 3);

                    }


                }

            }

        }
        else
        {
            if (defensaCpu - ataqueCpu > 499)
            {
                aleatorio = 0;
            }
            else if (ataqueCpu <= 400)
            {
                aleatorio = Random.Range(1, 8);

            }
            else if (ataqueCpu < 800)
            {
                aleatorio = Random.Range(1, 7);
            }
            else if (ataqueCpu < 2200)
            {
                aleatorio = Random.Range(1, 6);

            }
            else
            {
                aleatorio = Random.Range(1, 3);
            }
        }
        if (aleatorio == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetPrimerAtaque(int carta)
    {
        indiceCarta = carta;
    }
    public void SetPrimerAtaque(bool estado)
    {
        primerAtaque = estado;
    }
    public bool GetPrimerAtaque()
    {
        return primerAtaque;
    }
    public int GetIndiePrimerAtaque()
    {
        return indiceCarta;
    }
    public void ColorAtributo(int carta, int cartaCpu)
    {
        int[] temp1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] temp2 = { 2, 3, 4, 1, 9, 5, 6, 7, 10, 8 };
        color = Color.gray;
        bool salir = false;
        int atributoCartaUsuario = clon.getCartaCampoU(carta).GetComponent<carta>().GetGuardianStarA();
        int atributoCartaCpu = clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetGuardianStarA();
        for (int i = 0; i < 10 && salir == false; i++)
        {
            if (atributoCartaUsuario == temp1[i] && atributoCartaCpu == temp2[i])
            {
                color = Color.yellow;
                if (turnoUsuario)
                {
                    color = Color.red;
                }

                salir = true;
            }
            else if (atributoCartaUsuario == temp2[i] && atributoCartaCpu == temp1[i])
            {
                color = Color.red;
                if (turnoUsuario)
                {
                    color = Color.yellow;
                }

                salir = true;
            }
        }



    }
    public string LogicaAtributo(int carta, int cartaCpu)
    {
        int[] temp1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] temp2 = { 2, 3, 4, 1, 9, 5, 6, 7, 10, 8 };
        string favorable = "";
        bool salir = false;
        int atributoCartaUsuario = clon.getCartaCampoU(carta).GetComponent<carta>().GetGuardianStarA();
        int atributoCartaCpu = clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetGuardianStarA();
        for (int i = 0; i < 10 && salir == false; i++)
        {
            if (atributoCartaUsuario == temp1[i] && atributoCartaCpu == temp2[i])
            {
                favorable = "atributoCpu";
                salir = true;
            }
            else if (atributoCartaUsuario == temp2[i] && atributoCartaCpu == temp1[i])
            {
                favorable = "atributoUsuario";
                salir = true;
            }
        }
        return favorable;



    }
    public string LogicaAtributoMano(int carta, int cartaCpu)
    {
        int[] temp1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] temp2 = { 2, 3, 4, 1, 9, 5, 6, 7, 10, 8 };
        string favorable = "";
        bool salir = false;
        int atributoCartaUsuario = clon.getCartaCampoU(carta).GetComponent<carta>().GetGuardianStarA();
        int atributoCartaCpu = clon.GetClonCpu(cartaCpu).GetComponent<carta>().GetGuardianStarA();
        for (int i = 0; i < 10 && salir == false; i++)
        {
            if (atributoCartaUsuario == temp1[i] && atributoCartaCpu == temp2[i])
            {
                favorable = "atributoCpu";
                salir = true;
            }
            else if (atributoCartaUsuario == temp2[i] && atributoCartaCpu == temp1[i])
            {
                favorable = "atributoUsuario";
                salir = true;
            }
        }
        return favorable;



    }
    public Color ColorAtributo()
    {

        return color;
    }
    //inicio logica efectos carta magicas y trampa
    //cartas de campo
    public void EfectosCartasCampo(int campoModificado)
    {



        List<string> atributos = new List<string>();
        List<string> atributos2 = new List<string>();
        if (campoModificado == 0)
        {
            atributos.Add("Aqua");
            atributos.Add("Sea Serpent");
            atributos.Add("Thunder");
            atributos.Add("Fish");
            atributos2.Add("Machine");
            atributos2.Add("Pyro");
        }
        if (campoModificado == 1)
        {
            atributos.Add("Fiend");
            atributos.Add("Spellcaster");
            atributos2.Add("Fairy");

        }
        if (campoModificado == 2)
        {
            atributos.Add("Dragon");
            atributos.Add("Winged Beast");
            atributos.Add("Thunder");

        }
        if (campoModificado == 3)
        {
            atributos.Add("Warrior");
            atributos.Add("Beast-Warrior");


        }
        if (campoModificado == 4)
        {
            atributos.Add("Beast");
            atributos.Add("Beast-Warrior");
            atributos.Add("Plant");
            atributos.Add("Insect");


        }
        if (campoModificado == 5)
        {
            atributos.Add("Zombie");
            atributos.Add("Dinosaur");
            atributos.Add("Rock");

        }
        if (campoModificado == 6)
        {
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetClonCpu(i) != null && clon.GetClonCpu(i).GetComponent<carta>().GetTieneBono() == false && clon.GetClonCpu(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {

                    clon.GetClonCpu(i).GetComponent<carta>().SetAtaque(clon.GetClonCpu(i).GetComponent<carta>().getAtaque() + 500);
                    clon.GetClonCpu(i).GetComponent<carta>().SetDefensa(clon.GetClonCpu(i).GetComponent<carta>().getDefensa() + 500);
                    clon.GetClonCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                    clon.GetClonCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetClonCpu(i).GetComponent<carta>().getDefensa());
                    clon.GetClonCpu(i).GetComponent<carta>().SetTieneBono(true);
                }
                if (clon.GetCartaCpu(i) != null && clon.GetCartaCpu(i).GetComponent<carta>().GetTieneBono() == false && clon.GetCartaCpu(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {
                    clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() + 500);
                    clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() + 500);
                    clon.GetCartaCpu(i).GetComponent<carta>().SetTieneBono(true);
                    clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                    clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());

                }

            }

        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (clon.getClon(i) != null && clon.getClon(i).GetComponent<carta>().GetTieneBono() == false && clon.getClon(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {
                    string tipo = txt.GetNombreTipoCarta().GetValue(clon.getClon(i).GetComponent<carta>().GetTipoAtributo()).ToString().Trim();
                    if (atributos.Contains(tipo))
                    {
                        clon.getClon(i).GetComponent<carta>().SetAtaque(clon.getClon(i).GetComponent<carta>().getAtaque() + 500);
                        clon.getClon(i).GetComponent<carta>().SetDefensa(clon.getClon(i).GetComponent<carta>().getDefensa() + 500);
                        clon.getClon(i).GetComponent<carta>().SetTieneBono(true);
                    }
                    else if (atributos2.Contains(tipo))
                    {
                        if (clon.getClon(i).GetComponent<carta>().getAtaque() - 500 <= 0)
                        {
                            clon.getClon(i).GetComponent<carta>().SetAtaque(0);
                        }
                        else
                        {
                            clon.getClon(i).GetComponent<carta>().SetAtaque(clon.getClon(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        if (clon.getClon(i).GetComponent<carta>().getDefensa() - 500 <= 0)
                        {
                            clon.getClon(i).GetComponent<carta>().SetDefensa(0);
                        }
                        else
                        {
                            clon.getClon(i).GetComponent<carta>().SetDefensa(clon.getClon(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        clon.getClon(i).GetComponent<carta>().SetTieneBonoDesfavorable(true);
                    }

                }
                if (clon.GetClonCpu(i) != null && clon.GetClonCpu(i).GetComponent<carta>().GetTieneBono() == false && clon.GetClonCpu(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {
                    string tipo = txt.GetNombreTipoCarta().GetValue(clon.GetClonCpu(i).GetComponent<carta>().GetTipoAtributo()).ToString().Trim();
                    if (atributos.Contains(tipo))
                    {
                        clon.GetClonCpu(i).GetComponent<carta>().SetAtaque(clon.GetClonCpu(i).GetComponent<carta>().getAtaque() + 500);
                        clon.GetClonCpu(i).GetComponent<carta>().SetDefensa(clon.GetClonCpu(i).GetComponent<carta>().getDefensa() + 500);
                        clon.GetClonCpu(i).GetComponent<carta>().SetTieneBono(true);
                    }
                    else if (atributos2.Contains(tipo))
                    {
                        if (clon.GetClonCpu(i).GetComponent<carta>().getAtaque() - 500 <= 0)
                        {
                            clon.GetClonCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        else
                        {
                            clon.GetClonCpu(i).GetComponent<carta>().SetAtaque(clon.GetClonCpu(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        if (clon.GetClonCpu(i).GetComponent<carta>().getDefensa() - 500 <= 0)
                        {
                            clon.GetClonCpu(i).GetComponent<carta>().SetDefensa(0);
                        }
                        else
                        {
                            clon.GetClonCpu(i).GetComponent<carta>().SetDefensa(clon.GetClonCpu(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        clon.GetClonCpu(i).GetComponent<carta>().SetTieneBonoDesfavorable(true);
                    }

                }
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetTieneBono() == false && clon.getCartaCampoU(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {

                    string tipo = txt.GetNombreTipoCarta().GetValue(clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo()).ToString().Trim();
                    if (atributos.Contains(tipo))
                    {
                        clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() + 500);
                        clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() + 500);
                        clon.getCartaCampoU(i).GetComponent<carta>().SetTieneBono(true);
                    }
                    else if (atributos2.Contains(tipo))
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 500 <= 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 500 <= 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        clon.getCartaCampoU(i).GetComponent<carta>().SetTieneBonoDesfavorable(true);
                    }

                }
                if (clon.GetCartaCpu(i) != null && clon.GetCartaCpu(i).GetComponent<carta>().GetTieneBono() == false && clon.GetCartaCpu(i).GetComponent<carta>().GetTieneBonoDesfavorable() == false)
                {
                    string tipo = txt.GetNombreTipoCarta().GetValue(clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo()).ToString().Trim();
                    if (atributos.Contains(tipo))
                    {
                        clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() + 500);
                        clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() + 500);
                        clon.GetCartaCpu(i).GetComponent<carta>().SetTieneBono(true);
                    }
                    else if (atributos2.Contains(tipo))
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 500 <= 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 500 <= 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        clon.GetCartaCpu(i).GetComponent<carta>().SetTieneBonoDesfavorable(true);
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (clon.GetCartaCpu(i) != null)
            {
                clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());
            }
            if (clon.getCartaCampoU(i) != null)
            {
                clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());
            }

        }




    }
    public void EfectosCartasMagicas(int efecto, string efectoDe)
    {
        if (efectoDe.Equals("usuario"))
        {
            //raigeki
            if (efecto == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        GameObject.Destroy(clon.GetCartaCpu(i));
                        clon.SetCartaCpuCampo(i);
                        campo.SetCampoCpu(i, 0);
                    }
                }
            }
            else if (efecto == 2)
            {
                //espadas de luz reveladora
                if (!espadasLusReveladora.Contains("usuario") && !(espadasLusReveladora.Contains("cpu")))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDatosCarta(1);
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(i));
                        }
                    }
                    interfaz.espadasLuz.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                    interfaz.espadasLuz.transform.localPosition = new Vector3(interfaz.espadasLuz.transform.localPosition.x, interfaz.espadasLuz.transform.localPosition.y, -1.4f);
                    espadasLusReveladora = "usuario3";
                    interfaz.SetEstadoEspadas(true, "3");
                }

            }
            else if (efecto == 3)
            {
                //dark piercing light
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        clon.GetCartaCpu(i).GetComponent<carta>().SetDatosCarta(1);
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(i));

                    }
                }
            }
            else if (efecto == 4)
            {
                //spellbinding circle
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 500 > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 500 > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                        }

                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());
                    }
                }
            }
            else if (efecto == 5)
            {
                //temendous fire 
                interfaz.EmpezarAnimacionVida(1000, "cpu");
                if (vidaCpu - 1000 <= 0)
                {
                    finJuego = true;
                    Invoke("FinJuego", 2f);
                }
            }
            else if (efecto == 6)
            {
                //dianketto
                interfaz.EmpezarAnimacionVidaAumento(5000, efectoDe);
            }
            else if (efecto == 7)
            {
                //dark hole
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        GameObject.Destroy(clon.GetCartaCpu(i));
                        clon.SetCartaCpuCampo(i);
                        campo.SetCampoCpu(i, 0);
                    }
                    if (clon.getCartaCampoU(i) != null)
                    {
                        GameObject.Destroy(clon.getCartaCampoU(i));
                        clon.SetCartaCampo(i);
                        campo.SetCampoUsuario(i, 0);
                    }
                }
            }
            else if (efecto == 8)
            {
                //acid rain
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 19)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 9)
            {
                //aerosol 
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 14)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 10)
            {
                //breath of light
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 17)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 11)
            {
                //crush card
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 1500)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 12)
            {
                //dragon capture jar
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 1)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 13)
            {
                //duster implementar ahora
                for (int i = 5; i < 10; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        GameObject.Destroy(clon.GetCartaCpu(i));
                        clon.SetCartaCpuCampo(i);
                        campo.SetCampoCpu(i, 0);
                    }
                }

            }
            else if (efecto == 14)
            {
                //red medicine implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(500, efectoDe);
            }
            else if (efecto == 15)
            {
                // shadow spell
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 1000 > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 1000);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 1000 > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 1000);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());

                    }
                }
            }
            else if (efecto == 16)
            {
                //stop defense

                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 180f, 0f);
                        clon.GetCartaCpu(i).GetComponent<carta>().SetPos(1);

                    }
                }
            }
            else if (efecto == 17)
            {
                //soul poure implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(2000, efectoDe);
            }
            else if (efecto == 18)
            {
                //warrior eliminarion
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 8)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 19)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoAtributo() == 6)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 20)
            {
                //bloqueo de ataque
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 90f, 0f);
                        clon.GetCartaCpu(i).GetComponent<carta>().SetPos(0);

                    }
                }

            }
            else if (efecto == 21)
            {
                //dado gracioso
                int[] valoresReferencia = { 100, 200, 300, 400, 500, 600 };
                int valor = Random.Range(0, 6);
                int aumento = valoresReferencia[valor];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {

                        clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() + aumento);
                        clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() + aumento);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());
                    }
                }

            }
            else if (efecto == 22)
            {
                //dado calavera
                int[] valoresReferencia = { 100, 200, 300, 400, 500, 600 };
                int valor = Random.Range(0, 6);
                int reduccion = valoresReferencia[valor];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - reduccion > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - reduccion);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - reduccion > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - reduccion);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());

                    }
                }

            }
            else if (efecto == 23)
            {
                //goblin secret remedy implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(1000, efectoDe);

            }
            else if (efecto == 24)
            {
                //gift mystical elf
                interfaz.EmpezarAnimacionVidaAumento(2500, efectoDe);

            }
            else if (efecto == 25)
            {
                //magical explosion
                int daño = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        daño += 400;
                    }
                }
                interfaz.EmpezarAnimacionVida(daño, "cpu");
                if (vidaCpu - daño <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 26)
            {
                //shield crush
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 0)
                        {
                            GameObject.Destroy(clon.GetCartaCpu(i));
                            clon.SetCartaCpuCampo(i);
                            campo.SetCampoCpu(i, 0);
                        }

                    }
                }

            }
            else if (efecto == 27)
            {
                //sparks
                interfaz.EmpezarAnimacionVida(50, "cpu");
                if (vidaCpu - 50 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 28)
            {
                //hinotama

                interfaz.EmpezarAnimacionVida(100, "cpu");
                if (vidaCpu - 100 <= 0)
                {
                    finJuego = true;
                    Invoke("FinJuego", 2f);
                }



            }
            else if (efecto == 29)
            {
                //final flame
                interfaz.EmpezarAnimacionVida(200, "cpu");
                if (vidaCpu - 200 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 30)
            {
                //ookazi
                interfaz.EmpezarAnimacionVida(500, "cpu");
                if (vidaCpu - 500 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 31)
            {
                //moyan curry
                interfaz.EmpezarAnimacionVidaAumento(200, efectoDe);

            }
            else if (efecto == 32)
            {
                //seven completed
                int valor = Random.Range(1, 4);
                if (valor == 1)
                {


                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null)
                        {

                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() + 700);
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() + 700);
                            clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                            clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());
                        }
                    }
                }
                else if (valor == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null)
                        {
                            if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 700 > 0)
                            {
                                clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 700);
                            }
                            else
                            {
                                clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                            }
                            if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 700 > 0)
                            {
                                clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 700);
                            }
                            else
                            {
                                clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                            }
                            clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                            clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());

                        }
                    }
                }



            }
            else if (efecto == 33)
            {
                //just desserts
                int daño = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        daño += 500;
                    }
                }
                interfaz.EmpezarAnimacionVida(daño, "cpu");
                if (vidaCpu - daño <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else
            {
                //carga del poderoso

                int reduccion;

                for (int i = 0; i < 5; i++)
                {
                    reduccion = 200;
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 5000)
                        {
                            reduccion *= 10;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 3500)
                        {
                            reduccion *= 9;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 3000)
                        {
                            reduccion *= 8;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 2500)
                        {
                            reduccion *= 7;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 2000)
                        {
                            reduccion *= 6;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 1500)
                        {
                            reduccion *= 5;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 1000)
                        {
                            reduccion *= 4;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() >= 500)
                        {
                            reduccion *= 3;
                        }
                        else if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() > 0)
                        {
                            reduccion *= 2;
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - reduccion > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - reduccion);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - reduccion > 0)
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - reduccion);
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());

                    }
                }
            }

        }
        else
        {
            //raigeki
            if (efecto == 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        GameObject.Destroy(clon.getCartaCampoU(i));
                        clon.SetCartaCampo(i);
                        campo.SetCampoUsuario(i, 0);
                    }
                }
            }
            else if (efecto == 2)
            {
                //espadas de luz reveladora
                if (!espadasLusReveladora.Contains("usuario") && !(espadasLusReveladora.Contains("cpu")))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDatosCarta(1);
                            clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                        }
                    }
                    espadasLusReveladora = "cpu3";
                    interfaz.espadasLuz.transform.eulerAngles = new Vector3(90f, 0f, 0f);
                    interfaz.espadasLuz.transform.localPosition = new Vector3(interfaz.espadasLuz.transform.localPosition.x, interfaz.espadasLuz.transform.localPosition.y, 1.748f);
                    interfaz.SetEstadoEspadas(true, "3");
                }

            }
            else if (efecto == 3)
            {
                //dark piercing light
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        clon.getCartaCampoU(i).GetComponent<carta>().SetDatosCarta(1);
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                    }
                }
            }
            else if (efecto == 4)
            {
                //spellbinding circle
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 500 > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 500);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 500 > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 500);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());

                    }
                }
            }
            else if (efecto == 5)
            {
                //temendous fire 
                interfaz.EmpezarAnimacionVida(1000, "usuario");
                if (vidaUsuario - 1000 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }
            }
            else if (efecto == 6)
            {
                //dianketto
                interfaz.EmpezarAnimacionVidaAumento(5000, efectoDe);
            }
            else if (efecto == 7)
            {
                //dark hole
                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        GameObject.Destroy(clon.GetCartaCpu(i));
                        clon.SetCartaCpuCampo(i);
                        campo.SetCampoCpu(i, 0);
                    }
                    if (clon.getCartaCampoU(i) != null)
                    {
                        GameObject.Destroy(clon.getCartaCampoU(i));
                        clon.SetCartaCampo(i);
                        campo.SetCampoUsuario(i, 0);
                    }
                }
            }
            else if (efecto == 8)
            {
                //acid rain
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 19)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 9)
            {
                //aerosol 
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 14)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 10)
            {
                //breath of light
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 17)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 11)
            {
                //crush card
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 1500)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 12)
            {
                //dragon capture jar
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 1)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 13)
            {
                //duster implementar ahora
                for (int i = 5; i < 10; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        GameObject.Destroy(clon.getCartaCampoU(i));
                        clon.SetCartaCampo(i);
                        campo.SetCampoUsuario(i, 0);
                    }
                }

            }
            else if (efecto == 14)
            {
                //red medicine implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(500, efectoDe);
            }
            else if (efecto == 15)
            {
                // shadow spell
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 1000 > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - 1000);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 1000 > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - 1000);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());

                    }
                }
            }
            else if (efecto == 16)
            {
                //stop defense

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        clon.getCartaCampoU(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 180f);
                        clon.getCartaCampoU(i).GetComponent<carta>().SetPos(1);

                    }
                }
            }
            else if (efecto == 17)
            {
                //soul poure implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(2000, efectoDe);
            }
            else if (efecto == 18)
            {
                //warrior eliminarion
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 8)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 19)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 6)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }

                }
            }
            else if (efecto == 20)
            {
                //bloqueo de ataque
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        clon.getCartaCampoU(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 90f, 0f);
                        clon.getCartaCampoU(i).GetComponent<carta>().SetPos(0);

                    }
                }

            }
            else if (efecto == 21)
            {
                //dado gracioso
                int[] valoresReferencia = { 100, 200, 300, 400, 500, 600 };
                int valor = Random.Range(0, 6);
                int aumento = valoresReferencia[valor];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {

                        clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() + aumento);
                        clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() + aumento);
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());
                    }
                }

            }
            else if (efecto == 22)
            {
                //dado calavera
                int[] valoresReferencia = { 100, 200, 300, 400, 500, 600 };
                int valor = Random.Range(0, 6);
                int reduccion = valoresReferencia[valor];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - reduccion > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - reduccion);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - reduccion > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - reduccion);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());

                    }
                }

            }
            else if (efecto == 23)
            {
                //goblin secret remedy implementar ahora
                interfaz.EmpezarAnimacionVidaAumento(1000, efectoDe);

            }
            else if (efecto == 24)
            {
                //gift mystical elf
                interfaz.EmpezarAnimacionVidaAumento(2500, efectoDe);

            }
            else if (efecto == 25)
            {
                //magical explosion
                int daño = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        daño += 400;
                    }
                }
                interfaz.EmpezarAnimacionVida(daño, "usuario");
                if (vidaUsuario - daño <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 26)
            {
                //shield crush
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getPos() == 0)
                        {
                            GameObject.Destroy(clon.getCartaCampoU(i));
                            clon.SetCartaCampo(i);
                            campo.SetCampoUsuario(i, 0);
                        }

                    }
                }

            }
            else if (efecto == 27)
            {
                //sparks
                interfaz.EmpezarAnimacionVida(50, "usuario");
                if (vidaUsuario - 50 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 28)
            {
                //hinotama
                interfaz.EmpezarAnimacionVida(100, "usuario");
                if (vidaUsuario - 100 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 29)
            {
                //final flame
                interfaz.EmpezarAnimacionVida(200, "usuario");
                if (vidaUsuario - 200 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 30)
            {
                //ookazi
                interfaz.EmpezarAnimacionVida(500, "usuario");
                if (vidaUsuario - 500 <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else if (efecto == 31)
            {
                //moyan curry
                interfaz.EmpezarAnimacionVidaAumento(200, efectoDe);

            }
            else if (efecto == 32)
            {
                //seven completed
                int valor = Random.Range(1, 4);
                if (valor == 1)
                {


                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {

                            clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() + 700);
                            clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() + 700);
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());
                        }
                    }
                }
                else if (valor == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {
                            if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 700 > 0)
                            {
                                clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() - 700);
                            }
                            else
                            {
                                clon.GetCartaCpu(i).GetComponent<carta>().SetAtaque(0);
                            }
                            if (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 700 > 0)
                            {
                                clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(clon.GetCartaCpu(i).GetComponent<carta>().getDefensa() - 700);
                            }
                            else
                            {
                                clon.GetCartaCpu(i).GetComponent<carta>().SetDefensa(0);
                            }
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                            clon.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.GetCartaCpu(i).GetComponent<carta>().getDefensa());

                        }
                    }
                }



            }
            else if (efecto == 33)
            {
                //just desserts
                int daño = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        daño += 500;
                    }
                }
                interfaz.EmpezarAnimacionVida(daño, "usuario");
                if (vidaUsuario - daño <= 0)
                {

                    Invoke("FinJuego", 2f);
                }

            }
            else
            {
                //carga del poderoso

                int reduccion;

                for (int i = 0; i < 5; i++)
                {
                    reduccion = 200;
                    if (clon.getCartaCampoU(i) != null)
                    {
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 5000)
                        {
                            reduccion *= 10;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 3500)
                        {
                            reduccion *= 9;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 3000)
                        {
                            reduccion *= 8;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 2500)
                        {
                            reduccion *= 7;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 2000)
                        {
                            reduccion *= 6;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 1500)
                        {
                            reduccion *= 5;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 1000)
                        {
                            reduccion *= 4;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 500)
                        {
                            reduccion *= 3;
                        }
                        else if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() > 0)
                        {
                            reduccion *= 2;
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - reduccion > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() - reduccion);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetAtaque(0);
                        }
                        if (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - reduccion > 0)
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(clon.getCartaCampoU(i).GetComponent<carta>().getDefensa() - reduccion);
                        }
                        else
                        {
                            clon.getCartaCampoU(i).GetComponent<carta>().SetDefensa(0);
                        }
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.text = "" + clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        clon.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.text = "" + (clon.getCartaCampoU(i).GetComponent<carta>().getDefensa());

                    }
                }
            }
        }
    }
    public void FinJuego()
    {
        if (turnoUsuario)
        {
            FinJuego(false);
        }
        else
        {
            FinJuego(true);
        }

    }
    public void FinJuegoReverso()
    {
        if (!turnoUsuario)
        {
            FinJuego(false);
        }
        else
        {
            FinJuego(true);
        }

    }
    public void FinJuego(bool usuarioPierde)
    {

        int contador = 0;
        int contadorCpu = 0;
        for (int i = 0; i < 5; i++)
        {
            if (clon.getCartaCampoU(i) != null)
            {
                ataquePromedio += clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                defensaPromedio += clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                contador++;
            }
            if (clon.GetCartaCpu(i) != null)
            {
                ataquePromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                defensaPromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getDefensa();
                contadorCpu++;
            }

        }
        if (contador > 0)
        {
            ataquePromedio = ataquePromedio / contador;
            defensaPromedio = defensaPromedio / contador;
        }
        if (contadorCpu > 0)
        {
            ataquePromedioCpu = ataquePromedioCpu / contadorCpu;
            defensaPromedioCpu = defensaPromedioCpu / contadorCpu;
        }
        controles.SetFase("letrasFin");
        DetenerMusica();

        if (usuarioPierde)
        {
            LogicaPantallaResultados(0);
            interfaz.MostrarTextoGanaPierde("cpu");

        }
        else
        {
            LogicaPantallaResultados(1);
            interfaz.MostrarTextoGanaPierde("usuario");


        }
    }
    public bool ValidarParametrosActivacionMagicas(int carta)
    {

        //raigeki spellbinding circle y shadow spell
        if (carta == 638 || carta == 643 || carta == 675)
        {
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    contador++;
                }
            }
            if (contador > 0)
            {
                int[] ataqueTemp = new int[5];
                int[] posTemp = new int[5];
                int[] defensaTemp = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                    }
                    else
                    {
                        ataqueTemp[i] = 0;
                        defensaTemp[i] = 0;
                    }

                    posTemp[i] = i;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4 - i; j++)
                    {
                        if (ataqueTemp[j] < ataqueTemp[j + 1])
                        {
                            int actualAtaque = ataqueTemp[j];
                            int actualPos = posTemp[j];
                            int actualDefenssa = defensaTemp[j];
                            ataqueTemp[j] = ataqueTemp[j + 1];
                            posTemp[j] = posTemp[j + 1];
                            defensaTemp[j] = defensaTemp[j + 1];
                            ataqueTemp[j + 1] = actualAtaque;
                            posTemp[j + 1] = actualPos;
                            defensaTemp[j + 1] = actualDefenssa;
                        }
                    }
                }
                bool detener = false;
                for (int i = 0; i < 5 && detener == false; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                        {

                            if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() > ataqueTemp[0])
                            {
                                detener = true;

                            }
                        }
                        else
                        {
                            if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                            {
                                if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() > ataqueTemp[0])
                                {
                                    detener = true;

                                }
                            }
                        }

                    }
                }
                if (detener == false)
                {
                    contador = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {

                            contador++;
                        }
                    }
                    if (contador > 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (vidaCpu >= 4000)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                else
                {
                    int suma = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {
                            suma = suma + clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                        }
                    }
                    if (suma >= vidaUsuario)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else
            {
                return false;
            }
        }


        else if (carta == 641)
        {
            //espadas de luz reveladora
            if (!espadasLusReveladora.Contains("usuario") && !(espadasLusReveladora.Contains("cpu")))
            {

                int contador = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        contador++;
                    }
                }
                if (contador > 0)
                {
                    int[] ataqueTemp = new int[5];
                    int[] posTemp = new int[5];
                    int[] defensaTemp = new int[5];

                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null)
                        {
                            ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                            defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                        }
                        else
                        {
                            ataqueTemp[i] = 0;
                            defensaTemp[i] = 0;
                        }

                        posTemp[i] = i;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4 - i; j++)
                        {
                            if (ataqueTemp[j] < ataqueTemp[j + 1])
                            {
                                int actualAtaque = ataqueTemp[j];
                                int actualPos = posTemp[j];
                                int actualDefenssa = defensaTemp[j];
                                ataqueTemp[j] = ataqueTemp[j + 1];
                                posTemp[j] = posTemp[j + 1];
                                defensaTemp[j] = defensaTemp[j + 1];
                                ataqueTemp[j + 1] = actualAtaque;
                                posTemp[j + 1] = actualPos;
                                defensaTemp[j + 1] = actualDefenssa;
                            }
                        }
                    }
                    bool detener = false;
                    for (int i = 0; i < 5 && detener == false; i++)
                    {
                        if (clon.GetCartaCpu(i) != null)
                        {
                            if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                            {
                                if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() > ataqueTemp[0])
                                {
                                    detener = true;
                                    return false;

                                }
                            }
                        }
                    }
                    if (detener == false)
                    {
                        return true;
                    }

                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        else if (carta == 642)
        {
            //dark piercing light
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 0)
                {
                    contador++;
                }

            }
            if (contador > 0)
            {
                return true;
            }
            return false;

        }
        else if (carta == 644)
        {
            //temendous fire 
            if (vidaUsuario - 1000 <= 0)
            {
                return true;
            }
            else
            {
                int daño = 0;
                for (int i = 5; i < 10; i++)
                {
                    if (clon.GetCartaCpu(i) != null && campo.GetCampoCpu(i) == 644)
                    {
                        daño += 1000;

                    }
                }
                if (vidaUsuario - daño <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (carta == 645 || carta == 674 || carta == 677 || carta == 704 || carta == 705 || carta == 713)
        {
            //cartas de curacion
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    return true;
                }
            }
            return false;
        }
        else if (carta == 646)
        {
            //dark hole
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    contador++;
                }
            }
            if (contador == 0)
            {
                for (int i = 0; i < 5; i++)
                {

                    if (clon.getCartaCampoU(i) != null)
                    {
                        contador++;
                    }
                }
                if (contador > 0)
                {
                    return true;
                }
            }
            return false;

        }
        else if (carta == 668)
        {
            //acid rain
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 19)
                {
                    return true;
                }


            }
            return false;
        }
        else if (carta == 669)
        {
            //aerosol 
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 14)
                {
                    return true;
                }


            }
            return false;
        }
        else if (carta == 670)
        {
            //breath of light
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 17)
                {
                    return true;
                }


            }
            return false;
        }
        else if (carta == 671)
        {
            //crush card
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    contador++;
                }
            }
            if (contador > 0)
            {
                int[] ataqueTemp = new int[5];
                int[] posTemp = new int[5];
                int[] defensaTemp = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null)
                    {
                        ataqueTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                        defensaTemp[i] = clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                    }
                    else
                    {
                        ataqueTemp[i] = 0;
                        defensaTemp[i] = 0;
                    }

                    posTemp[i] = i;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4 - i; j++)
                    {
                        if (ataqueTemp[j] < ataqueTemp[j + 1])
                        {
                            int actualAtaque = ataqueTemp[j];
                            int actualPos = posTemp[j];
                            int actualDefenssa = defensaTemp[j];
                            ataqueTemp[j] = ataqueTemp[j + 1];
                            posTemp[j] = posTemp[j + 1];
                            defensaTemp[j] = defensaTemp[j + 1];
                            ataqueTemp[j + 1] = actualAtaque;
                            posTemp[j + 1] = actualPos;
                            defensaTemp[j + 1] = actualDefenssa;
                        }
                    }
                }
                bool detener = false;
                for (int i = 0; i < 5 && detener == false; i++)
                {
                    if (clon.GetCartaCpu(i) != null)
                    {
                        if (clon.getCartaCampoU(posTemp[0]).GetComponent<carta>().GetDatosCarta() == 1)
                        {
                            if (clon.GetCartaCpu(i).GetComponent<carta>().getAtaque() > ataqueTemp[0])
                            {
                                return false;

                            }
                        }
                    }
                }
                if (detener == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().getAtaque() >= 1500)
                        {
                            return true;
                        }
                    }

                }

            }
            else
            {
                return false;
            }
            return false;
        }
        else if (carta == 672)
        {
            //dragon capture jar
            for (int i = 0; i < 5; i++)
            {
                if (datosDuelo.GetDuelistaCpu().Contains("Pegasus"))
                {
                    if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 1)
                    {
                        return true;
                    }
                }
                else
                {
                    if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 1)
                    {
                        return true;
                    }
                }



            }
            return false;
        }
        else if (carta == 673)
        {
            //duster implementar ahora
            for (int i = 5; i < 10; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    return true;
                }

            }
            return false;

        }

        else if (carta == 676)
        {
            //stop defense
            bool encontro = false;
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    encontro = true;
                    break;
                }
            }
            if (encontro)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().getPos() == 0)
                    {
                        return true;

                    }
                }
            }

            return false;
        }
        else if (carta == 678)
        {
            //warrior elimination
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 8)
                {
                    return true;
                }


            }
            return false;
        }
        else if (carta == 679)
        {
            //eternal rest
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().GetDatosCarta() == 1 && clon.getCartaCampoU(i).GetComponent<carta>().GetTipoAtributo() == 6)
                {
                    return true;
                }


            }
            return false;
        }
        else if (carta == 701)
        {
            //block attack
            bool encontro = false;
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    encontro = true;
                    break;
                }
            }
            if (encontro)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().getPos() == 1)
                    {
                        return true;

                    }
                }
            }

            return false;
        }
        else if (carta == 702)
        {
            //dado gracioso
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    contador++;
                    if (contador >= 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else if (carta == 703)
        {
            //dado calavera
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    contador++;
                    if (contador >= 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else if (carta == 706)
        {
            //magical explosion
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    contador++;
                    if (contador >= 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else if (carta == 707)
        {
            //shield crush
            bool encontro = false;
            for (int i = 0; i < 5 && !encontro; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    encontro = true;
                }

            }
            if (encontro)
            {
                int contador = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (clon.getCartaCampoU(i) != null && clon.getCartaCampoU(i).GetComponent<carta>().getPos() == 0)
                    {
                        contador++;
                    }
                }
                if (contador >= 2)
                {
                    return true;
                }
            }

            return false;
        }
        else if (carta == 717)
        {
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    return true;
                }
            }
            return false;
        }
        else if (carta == 719)
        {
            //just desserts
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    contador++;
                    if (contador >= 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else if (carta == 720)
        {
            //carga del poderoso
            for (int i = 0; i < 5; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    return true;
                }
            }
            return false;
        }
        else if (carta == 721)
        {
            //united we stand
            int contador = 0;
            for (int i = 0; i < 5; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    contador++;
                    if (contador >= 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else
        {

            return true;
        }
    }
    //cartas 
    public int IniciarParametrosTrampaReverso(int cartaUsuario, int cartaCpu)
    {
        int activador = ParametrosActivacionTrampasReverso(cartaUsuario, cartaCpu);
        return activador;
    }
    public void EfectosTrampaReverso(int carta)
    {
        if (carta == 644 || carta == 709 || carta == 710 || carta == 711 || carta == 712)
        {
            //efecto de goblin fan
            int daño = 1000;
            if (carta == 709)
            {
                daño = 50;
            }
            else if (carta == 710)
            {
                daño = 100;
            }
            else if (carta == 711)
            {
                daño = 200;
            }
            else if (carta == 712)
            {
                daño = 500;
            }
            if (turnoUsuario)
            {
                interfaz.EmpezarAnimacionVida(daño, "usuario");
                if (vidaUsuario - daño <= 0)
                {

                    Invoke("FinJuegoReverso", 2f);
                }
            }
            else
            {
                interfaz.EmpezarAnimacionVida(daño, "cpu");
                if (vidaCpu - daño <= 0)
                {

                    Invoke("FinJuegoReverso", 2f);
                }
            }

        }
        else if (carta == 645 || carta == 674 || carta == 677 || carta == 704 || carta == 705 || carta == 713)
        {
            // efecto bad reaction to simochi
            int daño = 5000;
            if (carta == 674)
            {
                daño = 500;
            }
            else if (carta == 677)
            {
                daño = 2000;
            }
            else if (carta == 704)
            {
                daño = 1000;
            }
            else if (carta == 705)
            {
                daño = 2500;
            }
            else if (carta == 713)
            {
                daño = 200;
            }
            if (turnoUsuario)
            {
                interfaz.EmpezarAnimacionVida(daño, "usuario");
                if (vidaUsuario - daño <= 0)
                {

                    Invoke("FinJuegoReverso", 2f);
                }
            }
            else
            {
                interfaz.EmpezarAnimacionVida(daño, "cpu");
                if (vidaCpu - daño <= 0)
                {

                    Invoke("FinJuegoReverso", 2f);
                }
            }
        }
        else
        {
            // efecto reverse trap
        }
    }
    public int ParametrosActivacionTrampasReverso(int cartausuario, int cartaCpu)
    {
        if (turnoUsuario)
        {
            for (int i = 5; i < 10; i++)
            {

                if (clon.GetCartaCpu(i) != null)
                {
                    if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        if (campo.GetCampoCpu(i) == 688)
                        {
                            if (cartausuario == 644 || cartausuario == 709 || cartausuario == 710 || cartausuario == 711 || cartausuario == 712)
                            {

                                return i;
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 696)
                        {
                            if (cartausuario == 645 || cartausuario == 674 || cartausuario == 677 || cartausuario == 704 || cartausuario == 705 || cartausuario == 713)
                            {

                                return i;

                            }
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 5; i < 10; i++)
            {

                if (clon.getCartaCampoU(i) != null)
                {
                    if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        if (campo.GetCampoUsuario(i) == 688)
                        {
                            if (cartaCpu == 644 || cartaCpu == 709 || cartaCpu == 710 || cartaCpu == 711 || cartaCpu == 712)
                            {
                                trampasActiadas++;
                                return i;
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 696)
                        {
                            if (cartaCpu == 645 || cartaCpu == 674 || cartaCpu == 677 || cartaCpu == 704 || cartaCpu == 705 || cartaCpu == 713)
                            {
                                trampasActiadas++;
                                return i;
                            }
                        }
                    }
                }
            }
        }
        return 0;
    }
    public int ParametrosActivacionTrampas(int cartaUsuario, int cartaCpu)
    {
        int cartaTrampa = -1;
        if (turnoUsuario == true)
        {
            for (int i = 5; i < 10; i++)
            {
                if (clon.GetCartaCpu(i) != null)
                {
                    if (clon.GetCartaCpu(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        //validar que carta es
                        if (campo.GetCampoCpu(i) == 680)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 3000)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 697)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 2500)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 715)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 2000)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 714)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 1500)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 716)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 21)
                            {
                                cartaTrampa = i;
                                return cartaTrampa;
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 718)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getDefensa() >= 2000)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 681)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 1000)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 690)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 9)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 691)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 14
                                 || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 5
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 19)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }

                        else if (campo.GetCampoCpu(i) == 692)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 3
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 12
                            )
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 693)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 2
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 6
                            )
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 694)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 15
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 16
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 20
                            )
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoCpu(i) == 699)
                        {
                            if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 4
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 8
                                || clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoAtributo() == 10
                            )
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }

                            }
                        }
                        else if (campo.GetCampoCpu(i) == 682 || campo.GetCampoCpu(i) == 684 || campo.GetCampoCpu(i) == 685
                            || campo.GetCampoCpu(i) == 686 || campo.GetCampoCpu(i) == 687 || campo.GetCampoCpu(i) == 689
                             || campo.GetCampoCpu(i) == 695 || campo.GetCampoCpu(i) == 700)
                        {
                            if (campo.GetCampoCpu(i) == 685 || campo.GetCampoCpu(i) == 689)
                            {
                                if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                            else
                            {
                                cartaTrampa = i;
                                return cartaTrampa;
                            }


                        }
                        else
                        {
                            if (campo.GetCampoCpu(i) != 688 && campo.GetCampoCpu(i) != 696 && campo.GetCampoCpu(i) != 698)
                            {
                                if (clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().getAtaque() <= 500)
                                {
                                    if (!clon.getCartaCampoU(cartaUsuario).GetComponent<carta>().esInmortal)
                                    {
                                        cartaTrampa = i;
                                        return cartaTrampa;
                                    }
                                }
                            }

                        }

                    }
                }

            }
        }
        else
        {
            for (int i = 5; i < 10; i++)
            {
                if (clon.getCartaCampoU(i) != null)
                {
                    if (clon.getCartaCampoU(i).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                    {
                        //validar que carta es
                        if (campo.GetCampoUsuario(i) == 680)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 3000)
                            {
                                cartaTrampa = i;
                                return cartaTrampa;
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 697)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 2500)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 715)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 2000)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 714)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 1500)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 716)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 21)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 718)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getDefensa() >= 2000)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 681)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 1000)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 690)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 9)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 691)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 14
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 5
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 19)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 692)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 3
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 12)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 693)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 2
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 6)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 694)
                        {
                            if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 5
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 16
                                || clon.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoAtributo() == 20)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                        }
                        else if (campo.GetCampoUsuario(i) == 682 || campo.GetCampoUsuario(i) == 684 || campo.GetCampoUsuario(i) == 685
                          || campo.GetCampoUsuario(i) == 686 || campo.GetCampoUsuario(i) == 687 || campo.GetCampoUsuario(i) == 689
                           || campo.GetCampoUsuario(i) == 695 || campo.GetCampoUsuario(i) == 700)
                        {
                            if (campo.GetCampoUsuario(i) == 685 || campo.GetCampoUsuario(i) == 689)
                            {
                                if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                {
                                    cartaTrampa = i;
                                    return cartaTrampa;
                                }
                            }
                            else
                            {
                                cartaTrampa = i;
                                return cartaTrampa;
                            }


                        }

                        else
                        {
                            if (campo.GetCampoUsuario(i) != 688 && campo.GetCampoUsuario(i) != 696 && campo.GetCampoUsuario(i) != 698)
                                if (clon.GetCartaCpu(cartaCpu).GetComponent<carta>().getAtaque() <= 500)
                                {
                                    if (!clon.GetCartaCpu(cartaCpu).GetComponent<carta>().esInmortal)
                                    {
                                        cartaTrampa = i;
                                        return cartaTrampa;
                                    }
                                }
                        }

                    }
                }

            }
        }
        return cartaTrampa;
    }
    public void FinJuegoPorCartas()
    {
        condicionVictoria = VICTORIA_TECNICA;
        StartCoroutine(AnimacionFinJuego());
    }
    public void FinJuegoPorExodia()
    {
        StartCoroutine(AnimacionExodia());
    }
    IEnumerator AnimacionExodia()
    {
        DetenerMusica();
        controles.SetFase("letrasFin");
        while (interfaz.datosCartaCpu.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        while (interfaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            interfaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            yield return null;
        }
        Vector3[] destino = new Vector3[5];
        destino[0] = new Vector3(15.8f, 397.6f, 0f);
        destino[1] = new Vector3(251.48f, 273.4f, 0f);
        destino[2] = new Vector3(-216.5f, 273.4f, 0f);
        destino[3] = new Vector3(150.1f, 53.9f, 0f);
        destino[4] = new Vector3(-117.1f, 53.9f, 0f);
        int[] temp = new int[5];
        int[] carta = { 93, 96, 97, 94, 95 };
        if (turnoUsuario)
        {


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (campo.GetManoUsuario(j) == carta[i])
                    {
                        temp[i] = j;
                        break;
                    }
                }
            }

            {

                for (int i = 0; i < 5; i++)
                {
                    while (Vector3.Distance(clon.getClon(temp[i]).transform.localPosition, destino[i]) > Time.deltaTime * 900)
                    {
                        clon.getClon(temp[i]).transform.localPosition = Vector3.MoveTowards(clon.getClon(temp[i]).transform.localPosition, destino[i], Time.deltaTime * 900);

                        yield return null;
                    }

                    clon.getClon(temp[i]).transform.localPosition = destino[i];
                    yield return new WaitForSeconds(0.5f);
                }
                yield return null;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (campo.GetManoCpu(j) == carta[i])
                    {
                        temp[i] = j;
                        break;
                    }
                }
            }

            {

                for (int i = 0; i < 5; i++)
                {
                    while (Vector3.Distance(clon.GetClonCpu(temp[i]).transform.localPosition, destino[i]) > Time.deltaTime * 900)
                    {
                        clon.GetClonCpu(temp[i]).transform.localPosition = Vector3.MoveTowards(clon.GetClonCpu(temp[i]).transform.localPosition, destino[i], Time.deltaTime * 900);

                        yield return null;
                    }

                    clon.GetClonCpu(temp[i]).transform.localPosition = destino[i];
                    yield return new WaitForSeconds(0.5f);
                }
                yield return null;
            }
        }
        int contador = 0;
        int contadorCpu = 0;
        for (int i = 0; i < 5; i++)
        {
            if (clon.getCartaCampoU(i) != null)
            {
                ataquePromedio += clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                defensaPromedio += clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                contador++;
            }
            if (clon.GetCartaCpu(i) != null)
            {
                ataquePromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                defensaPromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getDefensa();
                contadorCpu++;
            }

        }
        if (contador > 0)
        {
            ataquePromedio = ataquePromedio / contador;
            defensaPromedio = defensaPromedio / contador;
        }
        if (contadorCpu > 0)
        {
            ataquePromedioCpu = ataquePromedioCpu / contadorCpu;
            defensaPromedioCpu = defensaPromedioCpu / contadorCpu;
        }

        if (turnoUsuario == true)
        {

            interfaz.SetTiempoFlash(2f);
            interfaz.SetFlash(true);
            yield return new WaitForSeconds(2f);
            condicionVictoria = VICTORIA_EXODIA;
            LogicaPantallaResultados(1);
            interfaz.MostrarTextoGanaPierde("usuario");

        }
        else
        {
            interfaz.SetTiempoFlash(2f);
            interfaz.SetFlash(true);
            for (int i = 0; i < 5; i++)
            {
                clon.GetClonCpu(i).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu((i)));
                clon.GetClonCpu(i).GetComponent<muestraCarta>().ataque.text = "" + clon.GetClonCpu(i).GetComponent<carta>().getAtaque();
                clon.GetClonCpu(i).GetComponent<muestraCarta>().defensa.text = "" + clon.GetClonCpu(i).GetComponent<carta>().getDefensa();
            }
            yield return new WaitForSeconds(2f);
            LogicaPantallaResultados(0);
            interfaz.MostrarTextoGanaPierde("cpu");


        }
    }
    IEnumerator AnimacionFinJuego()
    {
        controles.SetFase("letrasFin");
        DetenerMusica();
        while (interfaz.datosCartaCpu.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        while (interfaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            interfaz.datosCarta.transform.Translate(0f, posicionar, 0f);

            for (int i = 0; i < 5; i++)
            {
                if (turnoUsuario)
                {
                    if (clon.getClon(i) != null)
                    {
                        clon.getClon(i).transform.Translate(0f, posicionar, 0f);
                    }

                }
                else
                {
                    if (clon.GetClonCpu(i) != null)
                    {
                        clon.GetClonCpu(i).transform.Translate(0f, posicionar, 0f);
                    }

                }

            }

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        //yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene("FinDuelo");
        int contador = 0;
        int contadorCpu = 0;
        for (int i = 0; i < 5; i++)
        {
            if (clon.getCartaCampoU(i) != null)
            {
                ataquePromedio += clon.getCartaCampoU(i).GetComponent<carta>().getAtaque();
                defensaPromedio += clon.getCartaCampoU(i).GetComponent<carta>().getDefensa();
                contador++;
            }
            if (clon.GetCartaCpu(i) != null)
            {
                ataquePromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getAtaque();
                defensaPromedioCpu += clon.GetCartaCpu(i).GetComponent<carta>().getDefensa();
                contadorCpu++;
            }

        }
        if (contador > 0)
        {
            ataquePromedio = ataquePromedio / contador;
            defensaPromedio = defensaPromedio / contador;
        }
        if (contadorCpu > 0)
        {
            ataquePromedioCpu = ataquePromedioCpu / contadorCpu;
            defensaPromedioCpu = defensaPromedioCpu / contadorCpu;
        }

        if (turnoUsuario == true)
        {


            LogicaPantallaResultados(0);
            interfaz.MostrarTextoGanaPierde("cpu");
            //IMPLEMENTAR LOGICA DE PANTALLA DE RESULTADOS
        }
        else
        {
            LogicaPantallaResultados(1);
            interfaz.MostrarTextoGanaPierde("usuario");
            //IMPLEMENTAR LOGICA DE PANTALLA DE RESULTADOS
        }


    }
    public List<int> DropBarajado(List<int> dropOrdenado)
    {
        List<int> desodenado = new List<int>();
        while (dropOrdenado.Count > 0)
        {
            int val = Random.Range(0, dropOrdenado.Count - 1);
            desodenado.Add(dropOrdenado[val]);
            dropOrdenado.RemoveAt(val);
        }
        return desodenado;
    }
    //logica pantalla de resultados nivel 6/10
    //IMPLEMENTAR LOS MAGOS Y REVISAR MAS COSAS
    public void LogicaPantallaResultados(int ganar)
    {
        Time.timeScale = 1f;
        duelistaDuelo = datosDuelo.GetDuelistaCpu();
        //perder el duelo
        if (ganar == 0)
        {
            if (datosDuelo.GetModoHistoria() == true)
            {
                if (datosJuego.GetHistoria() < 34)
                {
                    datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 2);
                    if (datosJuego.GetHistoria() == 4)
                    {
                        if (!datosJuego.GetDesbloqueables().Contains(duelistaDuelo))
                        {
                            datosJuego.AñadirDuelista(duelistaDuelo);
                        }
                    }
                }
                else
                {
                    datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 1);
                }

            }
            else
            {
                datosJuego.SetElementoDuelosJugados(datosDuelo.GetIdDuelista());
                datosJuego.SetElementoDerrota(datosDuelo.GetIdDuelista());
            }
            rangoObtenido = "F";
            estrellasGanadas = 0;
        }
        //ganar el duelo
        else
        {
            if (datosDuelo.GetModoHistoria() == true)
            {
                if (datosJuego.GetHistoria() < 34)
                {
                    datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 1);
                }
                else
                {
                    datosJuego.SetHistoria(datosJuego.GetHistoria() + 1);
                    datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 2);
                }

                if (datosJuego.GetDesbloqueables() == null)
                {
                    datosJuego.AñadirDuelista(duelistaDuelo);
                }
                else
                {
                    if (!datosJuego.GetDesbloqueables().Contains(duelistaDuelo))
                    {
                        datosJuego.AñadirDuelista(duelistaDuelo);
                    }
                }

            }
            else
            {
                datosJuego.SetElementoDuelosJugados(datosDuelo.GetIdDuelista());
                datosJuego.SetElementoVictoria(datosDuelo.GetIdDuelista());
            }
            Rangos();
            int carta = Random.Range(0, 2048);
            //obtener la carta del drop cpu
            //RANGO SA 
            List<int> probabilidadDrop = new List<int>();

            int actual = 0;
            int cartaAdquirir = 1;
            int actualizar = 0;
            if (rangoObtenido.Equals("S") || rangoObtenido.Equals("A"))
            {
                if (rankPoints < 20)
                {
                    if (datosDuelo.GetIdDuelista() != 1)
                    {
                        while (actual != datosDuelo.GetIdDuelista() - 1)
                        {
                            if (txt.GetDestionoSATEC()[cartaAdquirir].Contains("s"))
                            {
                                actual++;
                            }
                            cartaAdquirir++;
                            actualizar = cartaAdquirir;

                        }
                    }
                    actual = 0;
                    int actualCarta = 0;
                    while (!txt.GetDestionoSATEC()[cartaAdquirir].Contains("s"))
                    {
                        int numeroDrop = int.Parse(txt.GetDropSATEC()[cartaAdquirir]);
                        for (int i = 0; i < numeroDrop; i++)
                        {
                            probabilidadDrop.Add(int.Parse(txt.GetDestionoSATEC()[cartaAdquirir]));
                            actualCarta++;

                        }
                        cartaAdquirir++;
                    }

                }
                else
                {
                    if (datosDuelo.GetIdDuelista() != 1)
                    {
                        while (actual != datosDuelo.GetIdDuelista() - 1)
                        {
                            if (txt.GetDestinoSA()[cartaAdquirir].Contains("s"))
                            {
                                actual++;
                            }
                            cartaAdquirir++;
                            actualizar = cartaAdquirir;

                        }
                    }
                    actual = 0;
                    int actualCarta = 0;
                    while (!txt.GetDestinoSA()[cartaAdquirir].Contains("s"))
                    {
                        int numeroDrop = int.Parse(txt.GetDropSA()[cartaAdquirir]);
                        for (int i = 0; i < numeroDrop; i++)
                        {
                            probabilidadDrop.Add(int.Parse(txt.GetDestinoSA()[cartaAdquirir]));
                            actualCarta++;

                        }
                        cartaAdquirir++;
                    }
                }
            }
            else
            {
                if (datosDuelo.GetIdDuelista() != 1)
                {
                    while (actual != datosDuelo.GetIdDuelista() - 1)
                    {
                        if (txt.GetDestinoBCD()[cartaAdquirir].Contains("s"))
                        {
                            actual++;
                        }
                        cartaAdquirir++;
                        actualizar = cartaAdquirir;

                    }
                }
                actual = 0;
                int actualCarta = 0;
                while (!txt.GetDestinoBCD()[cartaAdquirir].Contains("s"))
                {
                    int numeroDrop = int.Parse(txt.GetDropBCD()[cartaAdquirir]);
                    for (int i = 0; i < numeroDrop; i++)
                    {
                        probabilidadDrop.Add(int.Parse(txt.GetDestinoBCD()[cartaAdquirir]));
                        actualCarta++;

                    }
                    cartaAdquirir++;
                }

            }
            probabilidadDrop = DropBarajado(probabilidadDrop);
            actual = carta;
            if (rankPoints > 19)
            {
                bool ruletaPots = false;
                int suerte = Random.Range(1, 3);
                if (rankPoints < 86 || suerte == 2 || ataquePromedio + defensaPromedio <= 6000 || contadorCartasDestruidas > 3)
                {
                    ruletaPots = true;
                }
                if (ruletaPots)
                {

                    int aleatorio = AletorioPots();
                    if (int.Parse(txt.GetPots()[probabilidadDrop[actual]]) == 1)
                    {

                        if (aleatorio != 2)
                        {
                            actual = Random.Range(0, 2048);
                            while (int.Parse(txt.GetPots()[probabilidadDrop[actual]]) == 1)
                            {
                                actual = Random.Range(0, 2048);
                            }
                        }
                        else
                        {
                            if (int.Parse(txt.getatk()[probabilidadDrop[actual]]) >= 2500)
                            {
                                datosJuego.LimpiarElementoDuelosJugados(datosDuelo.GetIdDuelista());
                            }
                        }
                    }
                }
            }
            cartaAdquirir = probabilidadDrop[actual];
            if (!datosJuego.GetCofre().Contains(cartaAdquirir))
            {
                datosJuego.AñadirElementoCofre(cartaAdquirir);
                datosJuego.AñadirCantidadCofre(1);
                datosJuego.IncrementarIdNueva();
                datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());
                nuevaCarta = true;
            }
            else
            {

                int conteo = datosJuego.GetCofre().IndexOf(cartaAdquirir);
                int cantidadActualCofre = datosJuego.GetCantidadCofre()[conteo];
                datosJuego.RemoverCantidadCofre(conteo);
                datosJuego.RemoverElementoCofre(conteo);
                datosJuego.RemoverIdNueva(conteo);
                datosJuego.AñadirElementoCofre(cartaAdquirir);
                datosJuego.AñadirCantidadCofre(cantidadActualCofre + 1);
                datosJuego.IncrementarIdNueva();
                datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());

            }
            datosJuego.SetEstrellas(datosJuego.GetEstrellas() + estrellasGanadas);
            idCarta = probabilidadDrop[actual];
            nombreCarta = txt.getnom()[cartaAdquirir];
        }
    }
    public void Rangos()
    {
        //stec 0-9 atec 10-19
        if (condicionVictoria.Equals(VICTORIA_NORMAL))
        {
            rankPoints += 2;
        }
        else if (condicionVictoria.Equals(VICTORIA_EXODIA))
        {
            rankPoints += 40;
        }
        else
        {
            rankPoints -= 40;
        }
        //turnos
        if (cantTurnos >= 0 && cantTurnos < 5)
        {
            rankPoints += 12;
        }
        else if (cantTurnos >= 5 && cantTurnos < 9)
        {
            rankPoints += 8;
        }
        else if (cantTurnos >= 9 && cantTurnos < 29)
        {
            rankPoints += 0;
        }
        else if (cantTurnos >= 29 && cantTurnos < 33)
        {
            rankPoints -= 8;
        }
        else
        {
            rankPoints -= 12;
        }
        //cartas usadas
        int cartasUsadas = 40 - GetCantDeckUsuario();
        if (cartasUsadas >= 0 && cartasUsadas < 9)
        {
            rankPoints += 15;
        }
        else if (cartasUsadas >= 9 && cartasUsadas < 13)
        {
            rankPoints += 12;
        }
        else if (cartasUsadas >= 13 && cartasUsadas < 33)
        {
            rankPoints += 0;
        }
        else if (cartasUsadas >= 33 && cartasUsadas < 37)
        {
            rankPoints -= 5;
        }
        else
        {
            rankPoints -= 7;
        }
        //ataques efectivos
        if (ataquesEfectivos == 0 || ataquesEfectivos == 1)
        {
            rankPoints += 4;
        }
        else if (ataquesEfectivos == 2 || ataquesEfectivos == 3)
        {
            rankPoints += 2;
        }
        else if (ataquesEfectivos >= 4 && ataquesEfectivos < 10)
        {
            rankPoints += 0;
        }
        else if (ataquesEfectivos >= 10 && ataquesEfectivos < 20)
        {
            rankPoints -= 2;
        }
        else
        {
            rankPoints -= 4;
        }
        //defensas efectivas
        if (defensasEfectivas == 0 || defensasEfectivas == 1)
        {
            rankPoints += 0;
        }
        else if (defensasEfectivas >= 2 && defensasEfectivas < 6)
        {
            rankPoints -= 10;
        }
        else if (defensasEfectivas >= 6 && defensasEfectivas < 10)
        {
            rankPoints -= 20;
        }
        else if (defensasEfectivas >= 10 && defensasEfectivas < 15)
        {
            rankPoints -= 30;
        }
        else
        {
            rankPoints -= 40;
        }

        //cartas bocaabajo
        if (cartasBocaAbajo == 0)
        {
            rankPoints += 0;
        }
        else if (cartasBocaAbajo >= 1 && cartasBocaAbajo < 11)
        {
            rankPoints -= 2;
        }
        else if (cartasBocaAbajo >= 11 && cartasBocaAbajo < 21)
        {
            rankPoints -= 4;
        }
        else if (cartasBocaAbajo >= 21 && cartasBocaAbajo < 31)
        {
            rankPoints -= 6;
        }
        else
        {
            rankPoints -= 8;
        }

        //fusiones correctas
        if (fusionCorrecta == 0)
        {
            rankPoints += 4;
        }
        else if (fusionCorrecta >= 1 && fusionCorrecta < 5)
        {
            rankPoints += 0;
        }
        else if (fusionCorrecta >= 5 && fusionCorrecta < 10)
        {
            rankPoints -= 4;
        }
        else if (fusionCorrecta >= 10 && fusionCorrecta < 15)
        {
            rankPoints -= 8;
        }
        else
        {
            rankPoints -= 12;
        }

        //equipos correctos
        if (equiposCorrectos == 0)
        {
            rankPoints += 4;
        }
        else if (equiposCorrectos >= 1 && equiposCorrectos < 5)
        {
            rankPoints += 0;
        }
        else if (equiposCorrectos >= 5 && equiposCorrectos < 10)
        {
            rankPoints -= 4;
        }
        else if (equiposCorrectos >= 10 && equiposCorrectos < 15)
        {
            rankPoints -= 8;
        }
        else
        {
            rankPoints -= 12;
        }

        //magicas usadas
        if (magicasUsadas == 0)
        {
            rankPoints += 2;
        }
        else if (magicasUsadas >= 1 && magicasUsadas < 4)
        {
            rankPoints -= 4;
        }
        else if (magicasUsadas >= 4 && magicasUsadas < 7)
        {
            rankPoints -= 8;
        }
        else if (magicasUsadas >= 7 && magicasUsadas < 10)
        {
            rankPoints -= 12;
        }
        else
        {
            rankPoints -= 16;
        }

        //trampas usadas
        if (trampasActiadas == 0)
        {
            rankPoints += 2;
        }
        else if (trampasActiadas >= 1 && trampasActiadas < 3)
        {
            rankPoints -= 8;
        }
        else if (trampasActiadas >= 3 && trampasActiadas < 5)
        {
            rankPoints -= 16;
        }
        else if (trampasActiadas >= 5 && trampasActiadas < 7)
        {
            rankPoints -= 24;
        }
        else
        {
            rankPoints -= 32;
        }
        //puntos de vida
        if (vidaUsuario >= 0 && vidaUsuario < 100)
        {
            rankPoints -= 7;
        }
        else if (vidaUsuario >= 100 && vidaUsuario < 1000)
        {
            rankPoints -= 5;
        }
        else if (vidaUsuario >= 1000 && vidaUsuario < 7000)
        {
            rankPoints += 0;
        }
        else if (vidaUsuario >= 7000 && vidaUsuario < 8000)
        {
            rankPoints += 4;
        }
        else
        {
            rankPoints += 6;
        }

        if (rankPoints >= 90 && rankPoints < 100)
        {
            rangoObtenido = "S";
            estrellasGanadas = 5;
        }
        else if (rankPoints >= 80 && rankPoints < 90)
        {
            rangoObtenido = "A";
            estrellasGanadas = 4;
        }
        else if (rankPoints >= 70 && rankPoints < 80)
        {
            rangoObtenido = "B";
            estrellasGanadas = 3;
        }
        else if (rankPoints >= 60 && rankPoints < 70)
        {
            rangoObtenido = "C";
            estrellasGanadas = 2;
        }
        else if (rankPoints >= 50 && rankPoints < 60)
        {
            rangoObtenido = "D";
            estrellasGanadas = 1;
        }
        else if (rankPoints >= 40 && rankPoints < 50)
        {
            rangoObtenido = "D";
            estrellasGanadas = 1;
        }
        else if (rankPoints >= 30 && rankPoints < 40)
        {
            rangoObtenido = "C";
            estrellasGanadas = 2;
        }
        else if (rankPoints >= 20 && rankPoints < 30)
        {
            rangoObtenido = "B";
            estrellasGanadas = 3;
        }
        else if (rankPoints >= 10 && rankPoints < 20)
        {
            rangoObtenido = "A";
            estrellasGanadas = 4;
        }
        else
        {
            rangoObtenido = "S";
            estrellasGanadas = 5;
        }
    }
    public int AletorioPots()
    {
        if ((ataquePromedioDeck + defensaPromedioDeck) >= 2500)
        {
            return 2;
        }
        else if ((ataquePromedioDeck + defensaPromedioDeck) >= 2000)
        {
            return Random.Range(1, 3);
        }
        return Random.Range(1, 4);


        //if (datosJuego.GetDuelosJugados()[datosDuelo.GetIdDuelista()] < 10)
        //{
        //return Random.Range(1, 6*(hardDrop));
        //}
        //else if(datosJuego.GetDuelosJugados()[datosDuelo.GetIdDuelista()] < 20)
        //{
        //return Random.Range(1, 5*(hardDrop));
        //}
        //else if (datosJuego.GetDuelosJugados()[datosDuelo.GetIdDuelista()] < 30)
        //{

        //return Random.Range(1, 4*(hardDrop));
        //}
        //else if (datosJuego.GetDuelosJugados()[datosDuelo.GetIdDuelista()] < 50)
        //{
        //return Random.Range(1, 3*(hardDrop));

    }
    public void FinDuelo()
    {
        DetenerMusica();
        if (datosDuelo.GetModoHistoria() == true)
        {
            transicion.CargarEscena("Historia");
        }
        else
        {
            transicion.CargarEscena("DueloLibre");
        }
    }
    //musica
    public void IniciarMusica()
    {
        if (datosDuelo.GetModoHistoria() == true)
        {
            if (datosJuego.GetHistoria() < 3)
            {
                sonido.ReproducirIntro(-1);
            }
            else if (datosJuego.GetHistoria() == 21 || datosJuego.GetHistoria() == 22)
            {
                sonido.ReproducirIntro(-2);
            }
            else if (datosJuego.GetHistoria() == 3 || datosJuego.GetHistoria() == 31 || datosJuego.GetHistoria() == 34)
            {
                sonido.ReproducirIntro(-3);
            }
            else if (datosJuego.GetHistoria() == 4 || datosJuego.GetHistoria() == 35)
            {
                sonido.ReproducirIntro(1);
            }
            else if (datosJuego.GetHistoria() >= 3 && datosJuego.GetHistoria() < 15)
            {
                sonido.ReproducirIntro(3);
            }
            else if (datosJuego.GetHistoria() >= 15 && datosJuego.GetHistoria() < 21)
            {
                sonido.ReproducirIntro(6);
            }
            else if (datosJuego.GetHistoria() == 21 || datosJuego.GetHistoria() == 22)
            {
                sonido.ReproducirIntro(1);
            }
            else if (datosJuego.GetHistoria() == 23)
            {
                sonido.ReproducirIntro(4);
            }
            else if (datosJuego.GetHistoria() == 24)
            {
                sonido.ReproducirIntro(2);
            }
            else if (datosJuego.GetHistoria() > 35)
            {
                sonido.ReproducirIntro(5);
            }
        }
        else
        {
            sonido.ReproducirIntro(datosDuelo.GetMusicaDueloLibre());
        }






    }
    public void ReproducirEfectoMover()
    {
        efectosSonido.moverCarta();
    }

    public void ReproducirEfectoSeleccionar()
    {
        efectosSonido.SeleccionarCarta();
    }
    public void ReproducirCancelarAccion()
    {
        efectosSonido.CancelarAccion();
    }
    public void ReproducirCambiarPos()
    {
        efectosSonido.CambiarPosicionCarta();
    }
    public void ReproducirRobar()
    {
        efectosSonido.Robar();
    }
    public void ReproducirAtaque()
    {
        efectosSonido.Atacar();
    }
    public void ReproducirAtaqueDirecto()
    {
        efectosSonido.AtacarDIrecto();
    }
    public void ReproducirCambiarTurno()
    {
        efectosSonido.CambiarTurno();
    }
    public void DetenerMusica()
    {
        sonido.DetenerSonidos();
        sonido.activar = false;

    }
    public void ReproducirDescarte()
    {
        efectosSonido.Descarte();
    }
    public void ReproducirFusion()
    {
        efectosSonido.Fusion();
    }
    public void ReproducirNoFusion()
    {
        efectosSonido.NoFusion();
    }
    public void ReproducirAumento()
    {
        efectosSonido.Aumento();
    }
    public void ReproducirGanar()
    {
        sonido.ReproducirMusicaVictoria();
    }
    public void ReproducirPerder()
    {
        sonido.MusicaDerrota();
    }
    public void ReproducirFinDUelo()
    {
        sonido.MusicaResultados();
    }
    public void ReproducirActivacion()
    {
        efectosSonido.ActivarEfecto();
    }
    public void ReproducirQuemar()
    {
        efectosSonido.Quemar();
    }
    public void ReproducirGuardianFavorable()
    {
        efectosSonido.GuardianFavorable();
    }

}
