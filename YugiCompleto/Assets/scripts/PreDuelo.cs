using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PreDuelo : MonoBehaviour
{
    public float yOffset = 0.0015f;
    //public float yScroll = 0.00147f;
    public float yScroll = 0.0147f;
    public float yScrollDeck = 0.0147f;
    private string fase;
    public float indiceScroll = 1;
    public float indiceScrollDeck = 1;
    public ScrollRect scroll;
    public ScrollRect scrollDeck;
    public Image apuntador;
    public Image apuntadorDeck;
    public GameObject original;
    public GameObject originalDeck;
    private GameObject[] clonCarta;
    private GameObject[] clonDeck;
    public GameObject contenedorDeck;
    public GameObject contenedor;
    public GameObject panelDeck;
    public GameObject panelCofre;
    public ImportadorTextos txt;
    public Sonido sonido;
    public GameObject panel1;
    public GameObject panel2;
    public EfectosSonido efectosSonido;
    public transicion transicion;
    public bool deslizadoRapido;
    //cofre
    public TextMeshProUGUI totalDeck;
    public TextMeshProUGUI nombreCarta;

    public TextMeshProUGUI cantidadCofreTexto;
    //imagnes orden
    public Image numero;
    public Image nombre;
    public Image poder;
    public Image ataque;
    public Image defensa;
    public Image orTipo;
    public Image nueva;
    public GameObject panelCarta;
    public GameObject panelGuardian;
    public int indice;
    private int indiceDeck;
    private int cantidadCofre;
    private int idOrden;
    private int idOrdenDeck;
    private int totalCartasDeck;
    private int numeroEnDeck;
    public int indiceApCofre;
    private bool estaEnDeck;
    public ImportadorHistoria importadorHistoria;
    //deck
    public TextMeshProUGUI totalDeckEnDeck;

    public TextMeshProUGUI cantidadCofreTextoDeck;

    //imagnes orden
    public Image numeroDeck;
    public Image nombreDeck;
    public Image poderDeck;
    public Image ataqueDeck;
    public Image defensaDeck;
    public Image orTipoDeck;
    private bool iniciar;
    public TextMeshProUGUI descripcionCarta;



    //listas de prueba
    public List<int> deck = new List<int>();
    public List<int> cartasCofre = new List<int>();
    public List<int> cantidadCartasCofre = new List<int>();
    public List<int> cantidadCartasDeck = new List<int>();
    public List<int> pruebacofreda = new List<int>();
    public List<int> idNuevasCartas = new List<int>();
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    private DatosDuelo datosDuelo;
    // Start is called before the first frame update
    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
    }
    void Start()
    {
        deslizadoRapido = false;
        clonCarta = new GameObject[723];
        clonDeck = new GameObject[40];
        Cursor.visible = false;
        sonido.MusicaCrearDuelo();

        indiceDeck = 0;
        totalCartasDeck = 1;
        idOrden = 1;
        idOrdenDeck = 1;
        indice = 1;
        cantidadCofre = 0;
        fase = "cofre";
        //deck de prueba
        deck = datosJuego.GetDeckUsuario();

        //DESCOMENTARIEEEAR

        cartasCofre = datosJuego.GetCofre();




        cantidadCartasCofre = datosJuego.GetCantidadCofre();
        idNuevasCartas = datosJuego.GetNueva();
        //for(int i = 0; i < datosJuego.GetCofre().Count; i++)
        //{

        //int random = Random.Range(1, 680);
        //pruebacofreda.Add(random);

        //if (!cartasCofre.Contains(datosJuego.GetCofre()[i]))
        //{
        //cartasCofre.Add(datosJuego.GetCofre()[i]);
        //cantidadCartasCofre.Add(0);
        //idNuevasCartas.Add(i + 1);

        //}
        //else
        //{

        //int conteo = cartasCofre.IndexOf(datosJuego.GetCofre()[i]);
        //cantidadCartasCofre[conteo] = cantidadCartasCofre[conteo]+1;
        // }


        //}
        for (int i = 0; i < cantidadCartasCofre.Count; i++)
        {
            cantidadCofre = cantidadCofre + cantidadCartasCofre[i];
        }
        cantidadCofreTexto.text = "" + cantidadCofre;
        cantidadCofreTextoDeck.text = "" + cantidadCofre;
        totalDeck.text = "" + deck.Count;
        totalDeckEnDeck.text = "" + deck.Count;
        CrearInstancias();
        CrearInstanciasDeck();
        OrdenarCofre();
        OrdenarDeck();
        iniciar = false;
        StartCoroutine(Inicio());


    }
    IEnumerator Inicio()
    {
        yield return new WaitForSeconds(1f);
        iniciar = true;
    }
    public void CrearInstancias()
    {
        int posY = 0;
        for (int i = 0; i < 723; i++)
        {

            clonCarta[i] = Instantiate(original, new Vector3(original.transform.localPosition.x, original.transform.localPosition.y - posY, original.transform.localPosition.z), original.transform.rotation);
            posY += 50;
            clonCarta[i].transform.SetParent(contenedor.transform, false);
        }


    }
    public void CrearInstanciasDeck()
    {
        int posY = 0;
        for (int i = 0; i < 40; i++)
        {

            clonDeck[i] = Instantiate(originalDeck, new Vector3(originalDeck.transform.localPosition.x, originalDeck.transform.localPosition.y - posY, originalDeck.transform.localPosition.z), originalDeck.transform.rotation);
            posY += 50;
            if (i.ToString().Length == 1)
            {
                if (i != 9)
                {
                    clonDeck[i].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "0" + (i + 1);
                }
                else
                {
                    clonDeck[i].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "" + (i + 1);
                }

            }
            else
            {
                clonDeck[i].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "" + (i + 1);
            }

            clonDeck[i].transform.SetParent(contenedorDeck.transform, false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (fase.Equals("cofre"))
            {

                estaEnDeck = false;
                panelGuardian.SetActive(false);
                int cartaReemplazo = indice - 1;
                int cantidadReemplazo = indice - 1;
                if (cartaReemplazo >= cartasCofre.Count)
                {
                    cartaReemplazo = -1;
                }
                if (idOrden == 1)
                {
                    cartaReemplazo = cartasCofre.IndexOf(indice);
                    cantidadReemplazo = cartaReemplazo;

                }

                if (cartaReemplazo != -1)
                {
                    fase = "cerrar";
                    panelCarta.transform.Find("imgcarta").GetComponent<Image>().sprite = (Sprite)txt.cartasBatalla.GetValue(cartasCofre[cartaReemplazo]);
                    Debug.LogError(txt.GetDescripcionCarta().GetValue(cartasCofre[cartaReemplazo]).ToString());
                    panelCarta.transform.Find("panelDescripcion").Find("descripcionCarta").GetComponent<TextMeshProUGUI>().text = txt.GetDescripcionCarta().GetValue(cartasCofre[cartaReemplazo]).ToString();

                    if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Monstruo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    }

                    else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Equipo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);

                    }
                    else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Campo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "CAMPO";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                    }
                    else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Magica"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "MAGICA";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                    }
                    else
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
                    }


                    if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Monstruo"))
                    {
                        panelGuardian.SetActive(true);
                        panelGuardian.transform.Find("imgua1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                        panelGuardian.transform.Find("imgua2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                        panelGuardian.transform.Find("textogua1").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos1().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                        panelGuardian.transform.Find("textogua2").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos2().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    }
                    panelCarta.SetActive(true);

                }
            }
            else if (fase.Equals("deck"))
            {

                estaEnDeck = true;
                panelGuardian.SetActive(false);
                int cartaReemplazo = indice - 1;
                int cantidadReemplazo = indice - 1;
                if (deck.Count > 0 && indiceDeck < deck.Count)
                {
                    fase = "cerrar";
                    panelCarta.transform.Find("imgcarta").GetComponent<Image>().sprite = (Sprite)txt.cartasBatalla.GetValue(deck[indiceDeck]);

                    if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Monstruo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(deck[indiceDeck]).ToString()));
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(deck[indiceDeck]).ToString()));
                    }

                    else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Equipo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);

                    }
                    else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Campo"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "CAMPO";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                    }
                    else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Magica"))
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "MAGICA";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                    }
                    else
                    {
                        panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
                        panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
                    }


                    if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Monstruo"))
                    {
                        panelGuardian.SetActive(true);
                        panelGuardian.transform.Find("imgua1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(deck[indiceDeck]).ToString()));
                        panelGuardian.transform.Find("imgua2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(deck[indiceDeck]).ToString()));
                        panelGuardian.transform.Find("textogua1").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos1().GetValue(deck[indiceDeck]).ToString()));
                        panelGuardian.transform.Find("textogua2").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos2().GetValue(deck[indiceDeck]).ToString()));
                    }
                    panelCarta.SetActive(true);

                }
            }
            else if (fase.Equals("cerrar"))
            {
                panelCarta.SetActive(false);
                if (estaEnDeck)
                {
                    fase = "deck";
                }
                else
                {
                    fase = "cofre";
                }

            }

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (fase.Equals("cofre"))
            {
                //validar que hay menos de 40 cartas en deck
                numeroEnDeck = 0;
                int cartaReemplazo = indice - 1;
                int cantidadReemplazo = indice - 1;
                if (idOrden == 1)
                {
                    cartaReemplazo = cartasCofre.IndexOf(indice);
                    cantidadReemplazo = cartaReemplazo;

                }

                if (cartaReemplazo != -1)
                {
                    for (int i = 0; i < deck.Count; i++)
                    {
                        if (cartasCofre[cartaReemplazo].Equals(deck[i]))
                        {
                            numeroEnDeck++;
                        }
                    }



                    if (deck.Count < 40 && cantidadCartasCofre[cantidadReemplazo] > 0 && numeroEnDeck < 3)
                    {

                        cantidadCartasCofre[cantidadReemplazo] = cantidadCartasCofre[cantidadReemplazo] - 1;
                        cantidadCofre--;
                        deck.Add(cartasCofre[cartaReemplazo]);


                        totalDeck.text = "" + deck.Count;
                        totalDeckEnDeck.text = "" + deck.Count;
                        cantidadCofreTexto.text = "" + cantidadCofre;
                        cantidadCofreTextoDeck.text = "" + cantidadCofre;
                        LimpiarLista();
                        for (int i = 0; i < cartasCofre.Count; i++)
                        {
                            ObtenerDatosCarta(cartasCofre[i], i);

                        }
                        OrdenarDeck();

                        if (deck.Count == 40)
                        {
                            totalDeck.color = Color.white;
                            totalDeckEnDeck.color = Color.white;
                        }
                        efectosSonido.SeleccionarCarta();

                    }
                }
                else
                {
                    efectosSonido.NoFusion();
                }


            }
            else if (fase.Equals("deck"))
            {
                if (deck.Count > 0 && indiceDeck < deck.Count)
                {
                    int conteo = cartasCofre.IndexOf(deck[indiceDeck]);
                    cantidadCartasCofre[conteo] = cantidadCartasCofre[conteo] + 1;
                    cantidadCofre++;
                    deck.Remove(deck[indiceDeck]);
                    LimpiarListaDeck();

                    for (int i = 0; i < deck.Count; i++)
                    {
                        ObtenerDatosCartaDeck(deck[i], i);

                    }

                    OrdenarCofre();
                    if (indiceDeck != 0)
                    {

                        totalCartasDeck--;
                    }
                    totalDeck.text = "" + deck.Count;
                    totalDeck.color = Color.red;
                    totalDeckEnDeck.color = Color.red;
                    cantidadCofreTextoDeck.text = "" + cantidadCofre;
                    totalDeckEnDeck.text = "" + deck.Count;
                    cantidadCofreTexto.text = "" + cantidadCofre;

                    efectosSonido.SeleccionarCarta();
                }
                else
                {
                    efectosSonido.NoFusion();
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (fase.Equals("cofre"))
            {
                efectosSonido.CambiarPosicionCarta();
                panelCofre.SetActive(false);
                panelDeck.SetActive(true);
                fase = "deck";



            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            deslizadoRapido = false;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StopAllCoroutines();
            deslizadoRapido = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (fase.Equals("deck"))
            {
                efectosSonido.CambiarPosicionCarta();
                panelCofre.SetActive(true);
                panelDeck.SetActive(false);
                fase = "cofre";


            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !deslizadoRapido)
        {
            StartCoroutine(FlechaAbajoArriba());
            if (fase.Equals("cofre"))
            {
                if (indice < 722)
                {
                    efectosSonido.moverCarta();
                    indiceApCofre++;
                    if (indiceApCofre <= 7)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y -= yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 7;
                    }

                    indiceScroll++;
                    if (indiceScroll > 7)
                    {

                        indiceScroll = 8;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition - yScroll;

                    }
                    indice++;
                }

            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck < 39)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck++;
                    if (indiceScrollDeck <= 7)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y -= yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }


                    if (indiceScrollDeck > 7)
                    {
                        indiceScrollDeck = 8;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition - yScrollDeck;

                    }
                    indiceDeck++;
                }
            }



        }
        if (Input.GetKey(KeyCode.DownArrow) && deslizadoRapido)
        {
            if (fase.Equals("cofre"))
            {
                if (indice < 722)
                {
                    efectosSonido.moverCarta();
                    indiceApCofre++;
                    if (indiceApCofre <= 7)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y -= yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 7;
                    }

                    indiceScroll++;
                    if (indiceScroll > 7)
                    {

                        indiceScroll = 8;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition - yScroll;

                    }
                    indice++;
                }

            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck < 39)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck++;
                    if (indiceScrollDeck <= 7)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y -= yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }


                    if (indiceScrollDeck > 7)
                    {
                        indiceScrollDeck = 8;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition - yScrollDeck;

                    }
                    indiceDeck++;
                }
            }



        }
        if (Input.GetKey(KeyCode.UpArrow) && deslizadoRapido)
        {
            if (fase.Equals("cofre"))
            {
                if (indice != 1)
                {
                    efectosSonido.moverCarta();
                    indiceScroll--;
                    indiceApCofre--;
                    if (indiceApCofre > 0)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y += yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 1;
                    }

                    if (indiceScroll == 7)
                    {
                        indiceScroll = 6;
                    }
                    if (indiceScroll < 1)
                    {
                        indiceScroll = 1;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition + yScroll;

                    }
                    indice--;

                }
            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck != 0)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck--;
                    if (indiceScrollDeck > 0)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y += yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }

                    if (indiceScrollDeck == 7)
                    {
                        indiceScrollDeck = 6;
                    }
                    if (indiceScrollDeck < 1)
                    {
                        indiceScrollDeck = 1;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition + yScrollDeck;

                    }
                    indiceDeck--;

                }
            }



        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !deslizadoRapido)
        {
            StartCoroutine(FlechaAbajoArriba());
            if (fase.Equals("cofre"))
            {
                if (indice != 1)
                {
                    efectosSonido.moverCarta();
                    indiceScroll--;
                    indiceApCofre--;
                    if (indiceApCofre > 0)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y += yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 1;
                    }

                    if (indiceScroll == 7)
                    {
                        indiceScroll = 6;
                    }
                    if (indiceScroll < 1)
                    {
                        indiceScroll = 1;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition + yScroll;

                    }
                    indice--;

                }
            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck != 0)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck--;
                    if (indiceScrollDeck > 0)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y += yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }

                    if (indiceScrollDeck == 7)
                    {
                        indiceScrollDeck = 6;
                    }
                    if (indiceScrollDeck < 1)
                    {
                        indiceScrollDeck = 1;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition + yScrollDeck;

                    }
                    indiceDeck--;

                }
            }



        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            if (fase.Equals("cofre"))
            {
                efectosSonido.Descarte();
                idOrden++;
                if (idOrden == 8)
                {
                    idOrden = 1;
                }

                //totalCartasTexto.text = "1/685";
                OrdenarCofre();

            }
            else if (fase.Equals("deck"))
            {
                efectosSonido.Descarte();
                idOrdenDeck++;
                if (idOrdenDeck == 7)
                {
                    idOrdenDeck = 1;
                }
                OrdenarDeck();
            }

        }
        if (Input.GetKeyDown(KeyCode.C) && iniciar)
        {
            if (!fase.Equals("cerrar"))
            {
                if (deck.Count == 40)
                {
                    iniciar = false;
                    datosJuego.SetDeckUsuario(deck);
                    datosJuego.SetCofre(cartasCofre);
                    datosJuego.SetCantidadCofre(cantidadCartasCofre);
                    datosJuego.SetNuevo(idNuevasCartas);
                    efectosSonido.CancelarAccion();
                    if (datosJuego.GetFase().Equals("duelo"))
                    {
                        if (datosDuelo.GetModoHistoria() == false)
                        {
                            ActualizarDeckCpu();
                        }

                        transicion.CargarEscena("Juego");
                    }
                    else if (datosJuego.GetFase().Equals("dueloLibre"))
                    {
                        transicion.CargarEscena("DueloLibre");
                    }
                    else if (datosJuego.GetFase().Equals("menuContinuar"))
                    {
                        transicion.CargarEscena("MenuContinuar");
                    }
                }
                else
                {
                    efectosSonido.NoFusion();
                }
            }

        }
    }
    public void ObtenerDatosCarta(int id, int idCantCofre)
    {

        int idCantidad = id - 1;
        numeroEnDeck = 0;
        if (idOrden != 1)
        {
            idCantidad = idCantCofre;
        }
        for (int i = 0; i < deck.Count; i++)
        {
            if (cartasCofre[idCantCofre].Equals(deck[i]))
            {
                numeroEnDeck++;
            }
        }
        clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().enabled = true;
        clonCarta[idCantidad].transform.Find("nombre").GetComponent<TextMeshProUGUI>().text = txt.getnom().GetValue(id).ToString();

        clonCarta[idCantidad].transform.Find("enCofre").GetComponent<TextMeshProUGUI>().text = "" + cantidadCartasCofre[idCantCofre];

        clonCarta[idCantidad].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().text = "" + numeroEnDeck;
        if (numeroEnDeck == 3)
        {
            clonCarta[idCantidad].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (id.ToString().Length == 1)
        {
            clonCarta[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "00" + id;
        }
        else if (id.ToString().Length == 2)
        {

            clonCarta[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "0" + id;
        }
        else
        {
            clonCarta[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "" + id;

        }
        if (cantidadCartasCofre[idCantCofre] == 0)
        {
            clonCarta[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("enCofre").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("nombre").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("ataque").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("defensa").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("espada").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            clonCarta[idCantidad].transform.Find("escudo").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Monstruo"))
        {
            clonCarta[idCantidad].transform.Find("espada").GetComponent<Image>().enabled = true;
            clonCarta[idCantidad].transform.Find("escudo").GetComponent<Image>().enabled = true;
            clonCarta[idCantidad].transform.Find("ataque").GetComponent<TextMeshProUGUI>().text = txt.getatk().GetValue(id).ToString();
            clonCarta[idCantidad].transform.Find("defensa").GetComponent<TextMeshProUGUI>().text = txt.getdef().GetValue(id).ToString();
            clonCarta[idCantidad].transform.Find("guar1").GetComponent<RawImage>().enabled = true;
            clonCarta[idCantidad].transform.Find("guar1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(id).ToString()));
            clonCarta[idCantidad].transform.Find("guar2").GetComponent<RawImage>().enabled = true;
            clonCarta[idCantidad].transform.Find("guar2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(id).ToString()));
            clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(id).ToString()));
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Equipo"))
        {
            clonCarta[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
            clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Campo"))
        {
            clonCarta[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "CAMPO";
            clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Magica"))
        {
            clonCarta[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "MAGICA";
            clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
        }
        else
        {
            clonCarta[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
            clonCarta[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
        }



    }
    public void ObtenerDatosCartaDeck(int id, int idCantCofre)
    {


        int idCantidad = idCantCofre;
        if (idCantidad.ToString().Length == 1)
        {
            if (idCantCofre != 9)
            {
                clonDeck[idCantidad].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "0" + (idCantCofre + 1);
            }
            else
            {
                clonDeck[idCantidad].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "" + (idCantCofre + 1);
            }
        }
        else
        {
            clonDeck[idCantidad].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "" + (idCantCofre + 1);
        }




        clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().enabled = true;
        clonDeck[idCantidad].transform.Find("nombre").GetComponent<TextMeshProUGUI>().text = txt.getnom().GetValue(id).ToString();
        if (id.ToString().Length == 1)
        {
            clonDeck[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "00" + id;
        }
        else if (id.ToString().Length == 2)
        {

            clonDeck[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "0" + id;
        }
        else
        {
            clonDeck[idCantidad].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "" + id;

        }

        if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Monstruo"))
        {
            clonDeck[idCantidad].transform.Find("espada").GetComponent<Image>().enabled = true;
            clonDeck[idCantidad].transform.Find("escudo").GetComponent<Image>().enabled = true;
            clonDeck[idCantidad].transform.Find("ataque").GetComponent<TextMeshProUGUI>().text = txt.getatk().GetValue(id).ToString();
            clonDeck[idCantidad].transform.Find("defensa").GetComponent<TextMeshProUGUI>().text = txt.getdef().GetValue(id).ToString();
            clonDeck[idCantidad].transform.Find("guar1").GetComponent<RawImage>().enabled = true;
            clonDeck[idCantidad].transform.Find("guar1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(id).ToString()));
            clonDeck[idCantidad].transform.Find("guar2").GetComponent<RawImage>().enabled = true;
            clonDeck[idCantidad].transform.Find("guar2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(id).ToString()));
            clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(id).ToString()));
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Equipo"))
        {
            clonDeck[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
            clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Campo"))
        {
            clonDeck[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "CAMPO";
            clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
        }
        else if (txt.GetTipoCarta().GetValue(id).ToString().Trim().Equals("Magica"))
        {
            clonDeck[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "MAGICA";
            clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
        }
        else
        {
            clonDeck[idCantidad].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
            clonDeck[idCantidad].transform.Find("atributo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
        }



    }
    public void LimpiarLista()
    {
        for (int i = 0; i < clonCarta.Length; i++)
        {
            clonCarta[i].transform.Find("enCofre").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("numero").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("nombre").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("ataque").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("defensa").GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("espada").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("escudo").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            clonCarta[i].transform.Find("atributo").GetComponent<RawImage>().enabled = false;
            clonCarta[i].transform.Find("nombre").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("enCofre").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("espada").GetComponent<Image>().enabled = false;
            clonCarta[i].transform.Find("escudo").GetComponent<Image>().enabled = false;
            clonCarta[i].transform.Find("ataque").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("defensa").GetComponent<TextMeshProUGUI>().text = "";
            clonCarta[i].transform.Find("guar1").GetComponent<RawImage>().enabled = false;
            clonCarta[i].transform.Find("guar2").GetComponent<RawImage>().enabled = false;
            clonCarta[i].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void LimpiarListaDeck()
    {
        for (int i = 0; i < clonDeck.Length; i++)
        {
            clonDeck[i].transform.Find("numeroEnDeck").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("atributo").GetComponent<RawImage>().enabled = false;
            clonDeck[i].transform.Find("nombre").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("enCofre").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("enDeck").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("numero").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("espada").GetComponent<Image>().enabled = false;
            clonDeck[i].transform.Find("escudo").GetComponent<Image>().enabled = false;
            clonDeck[i].transform.Find("ataque").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("defensa").GetComponent<TextMeshProUGUI>().text = "";
            clonDeck[i].transform.Find("guar1").GetComponent<RawImage>().enabled = false;
            clonDeck[i].transform.Find("guar2").GetComponent<RawImage>().enabled = false;
            clonDeck[i].transform.Find("MT").GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void OrdenarCofre()
    {
        numero.color = new Color(1f, 1f, 1f, 0f);
        nombre.color = new Color(1f, 1f, 1f, 0f);
        poder.color = new Color(1f, 1f, 1f, 0f);
        ataque.color = new Color(1f, 1f, 1f, 0f);
        defensa.color = new Color(1f, 1f, 1f, 0f);
        orTipo.color = new Color(1f, 1f, 1f, 0f);
        nueva.color = new Color(1f, 1f, 1f, 0f);
        if (idOrden == 1)
        {
            numero.color = new Color(1f, 1f, 1f, 1f);
            List<int> poderTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                poderTemp.Add((cartasCofre[i]));
            }
            for (int i = 0; i < cartasCofre.Count - 1; i++)
            {
                for (int j = 0; j < cartasCofre.Count - 1 - i; j++)
                {
                    if (poderTemp[j] > poderTemp[j + 1])
                    {
                        int actualPoder = poderTemp[j];
                        int actualPos = cartasCofre[j];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        cartasCofre[j] = cartasCofre[j + 1];
                        poderTemp[j] = poderTemp[j + 1];

                        poderTemp[j + 1] = actualPoder;
                        cartasCofre[j + 1] = actualPos;

                    }
                }

            }
        }

        else if (idOrden == 2)
        {
            nombre.color = new Color(1f, 1f, 1f, 1f);
            List<string> nombreTemp = new List<string>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                nombreTemp.Add(txt.getnom().GetValue(cartasCofre[i]).ToString());
            }
            //ataqueTemp.Sort(Compare);
            for (int i = 0; i < nombreTemp.Count - 1; i++)
            {
                for (int j = 0; j < nombreTemp.Count - 1 - i; j++)
                {
                    //int c=string.Compare()
                    if (string.Compare(nombreTemp[j], nombreTemp[j + 1]) == 1)
                    {
                        string actualAtaque = nombreTemp[j];

                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        nombreTemp[j] = nombreTemp[j + 1];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];

                        nombreTemp[j + 1] = actualAtaque;
                        cartasCofre[j + 1] = actualPos;
                        int actualIdnueva = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdnueva;


                    }
                }

            }



        }
        else if (idOrden == 3)
        {
            poder.color = new Color(1f, 1f, 1f, 1f);
            List<int> poderTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(cartasCofre[i]).ToString().Trim().Equals("Monstruo"))
                {
                    poderTemp.Add(int.Parse(txt.getatk().GetValue(cartasCofre[i]).ToString()) + int.Parse(txt.getdef().GetValue(cartasCofre[i]).ToString()));
                }
                else
                {
                    poderTemp.Add(0);
                }


            }
            for (int i = 0; i < cartasCofre.Count - 1; i++)
            {
                for (int j = 0; j < cartasCofre.Count - 1 - i; j++)
                {
                    if (poderTemp[j] < poderTemp[j + 1])
                    {
                        int actualPoder = poderTemp[j];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];
                        poderTemp[j] = poderTemp[j + 1];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        poderTemp[j + 1] = actualPoder;
                        cartasCofre[j + 1] = actualPos;

                    }
                }

            }
        }
        else if (idOrden == 4)
        {
            ataque.color = new Color(1f, 1f, 1f, 1f);
            List<int> ataqueTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(cartasCofre[i]).ToString().Trim().Equals("Monstruo"))
                {
                    ataqueTemp.Add(int.Parse(txt.getatk().GetValue(cartasCofre[i]).ToString()));
                }
                else
                {
                    ataqueTemp.Add(0);
                }


            }
            for (int i = 0; i < cartasCofre.Count - 1; i++)
            {
                for (int j = 0; j < cartasCofre.Count - 1 - i; j++)
                {
                    if (ataqueTemp[j] < ataqueTemp[j + 1])
                    {
                        int actualPoder = ataqueTemp[j];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];
                        ataqueTemp[j] = ataqueTemp[j + 1];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        ataqueTemp[j + 1] = actualPoder;
                        cartasCofre[j + 1] = actualPos;

                    }
                }

            }
        }
        else if (idOrden == 5)
        {
            defensa.color = new Color(1f, 1f, 1f, 1f);
            List<int> defensaTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(cartasCofre[i]).ToString().Trim().Equals("Monstruo"))
                {
                    defensaTemp.Add(int.Parse(txt.getdef().GetValue(cartasCofre[i]).ToString()));
                }
                else
                {
                    defensaTemp.Add(0);
                }


            }
            for (int i = 0; i < cartasCofre.Count - 1; i++)
            {
                for (int j = 0; j < cartasCofre.Count - 1 - i; j++)
                {
                    if (defensaTemp[j] < defensaTemp[j + 1])
                    {
                        int actualPoder = defensaTemp[j];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];
                        defensaTemp[j] = defensaTemp[j + 1];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        defensaTemp[j + 1] = actualPoder;
                        cartasCofre[j + 1] = actualPos;

                    }
                }

            }
        }
        else if (idOrden == 6)
        {
            orTipo.color = new Color(1f, 1f, 1f, 1f);
            List<string> nombreTemp = new List<string>();
            //cartasCofre;
            for (int i = 0; i < cartasCofre.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(cartasCofre[i]).ToString().Trim().Equals("Monstruo"))
                {
                    nombreTemp.Add(txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(cartasCofre[i]).ToString())).ToString());
                }
                else
                {
                    nombreTemp.Add("z");
                }
            }
            //ataqueTemp.Sort(Compare);
            for (int i = 0; i < nombreTemp.Count - 1; i++)
            {
                for (int j = 0; j < nombreTemp.Count - 1 - i; j++)
                {
                    //int c=string.Compare()
                    if (string.Compare(nombreTemp[j], nombreTemp[j + 1]) == 1)
                    {
                        string actualAtaque = nombreTemp[j];
                        nombreTemp[j] = nombreTemp[j + 1];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        nombreTemp[j + 1] = actualAtaque;
                        cartasCofre[j + 1] = actualPos;



                    }
                }

            }



        }
        else
        {
            nueva.color = new Color(1f, 1f, 1f, 1f);
            List<int> nuevaTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < idNuevasCartas.Count; i++)
            {
                nuevaTemp.Add((idNuevasCartas[i]));
            }

            for (int i = 0; i < nuevaTemp.Count - 1; i++)
            {
                for (int j = 0; j < nuevaTemp.Count - 1 - i; j++)
                {
                    //int c=string.Compare()
                    if (nuevaTemp[j] < nuevaTemp[j + 1])
                    {
                        int actualNueva = nuevaTemp[j];
                        nuevaTemp[j] = nuevaTemp[j + 1];
                        int actualPos = cartasCofre[j];
                        cartasCofre[j] = cartasCofre[j + 1];
                        int actualCantCarta = cantidadCartasCofre[j];
                        cantidadCartasCofre[j] = cantidadCartasCofre[j + 1];
                        cantidadCartasCofre[j + 1] = actualCantCarta;
                        int actualIdCarta = idNuevasCartas[j];
                        idNuevasCartas[j] = idNuevasCartas[j + 1];
                        idNuevasCartas[j + 1] = actualIdCarta;
                        nuevaTemp[j + 1] = actualNueva;
                        cartasCofre[j + 1] = actualPos;



                    }
                }

            }


        }
        LimpiarLista();
        for (int i = 0; i < cartasCofre.Count; i++)
        {
            ObtenerDatosCarta(cartasCofre[i], i);
        }


    }

    public void OrdenarDeck()
    {
        numeroDeck.color = new Color(1f, 1f, 1f, 0f);
        nombreDeck.color = new Color(1f, 1f, 1f, 0f);
        poderDeck.color = new Color(1f, 1f, 1f, 0f);
        ataqueDeck.color = new Color(1f, 1f, 1f, 0f);
        defensaDeck.color = new Color(1f, 1f, 1f, 0f);
        orTipoDeck.color = new Color(1f, 1f, 1f, 0f);
        if (idOrdenDeck == 1)
        {
            numeroDeck.color = new Color(1f, 1f, 1f, 1f);
            List<int> poderTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                poderTemp.Add((deck[i]));
            }
            for (int i = 0; i < deck.Count - 1; i++)
            {
                for (int j = 0; j < deck.Count - 1 - i; j++)
                {
                    if (poderTemp[j] > poderTemp[j + 1])
                    {
                        int actualPoder = poderTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        poderTemp[j] = poderTemp[j + 1];
                        poderTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;

                    }
                }

            }
        }

        else if (idOrdenDeck == 2)
        {
            nombreDeck.color = new Color(1f, 1f, 1f, 1f);
            List<string> nombreTemp = new List<string>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                nombreTemp.Add(txt.getnom().GetValue(deck[i]).ToString());
            }
            //ataqueTemp.Sort(Compare);
            for (int i = 0; i < nombreTemp.Count - 1; i++)
            {
                for (int j = 0; j < nombreTemp.Count - 1 - i; j++)
                {
                    //int c=string.Compare()
                    if (string.Compare(nombreTemp[j], nombreTemp[j + 1]) == 1)
                    {
                        string actualPoder = nombreTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        nombreTemp[j] = nombreTemp[j + 1];
                        nombreTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;

                    }
                }

            }



        }
        else if (idOrdenDeck == 3)
        {
            poderDeck.color = new Color(1f, 1f, 1f, 1f);
            List<int> poderTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(deck[i]).ToString().Trim().Equals("Monstruo"))
                {
                    poderTemp.Add(int.Parse(txt.getatk().GetValue(deck[i]).ToString()) + int.Parse(txt.getdef().GetValue(deck[i]).ToString()));
                }
                else
                {
                    poderTemp.Add(0);
                }


            }
            for (int i = 0; i < deck.Count - 1; i++)
            {
                for (int j = 0; j < deck.Count - 1 - i; j++)
                {
                    if (poderTemp[j] < poderTemp[j + 1])
                    {
                        int actualPoder = poderTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        poderTemp[j] = poderTemp[j + 1];
                        poderTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;

                    }
                }

            }
        }
        else if (idOrdenDeck == 4)
        {
            ataqueDeck.color = new Color(1f, 1f, 1f, 1f);
            List<int> ataqueTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(deck[i]).ToString().Trim().Equals("Monstruo"))
                {
                    ataqueTemp.Add(int.Parse(txt.getatk().GetValue(deck[i]).ToString()));
                }
                else
                {
                    ataqueTemp.Add(0);
                }


            }
            for (int i = 0; i < deck.Count - 1; i++)
            {
                for (int j = 0; j < deck.Count - 1 - i; j++)
                {
                    if (ataqueTemp[j] < ataqueTemp[j + 1])
                    {
                        int actualPoder = ataqueTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        ataqueTemp[j] = ataqueTemp[j + 1];
                        ataqueTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;

                    }
                }

            }
        }
        else if (idOrdenDeck == 5)
        {
            defensaDeck.color = new Color(1f, 1f, 1f, 1f);
            List<int> defensaTemp = new List<int>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(deck[i]).ToString().Trim().Equals("Monstruo"))
                {
                    defensaTemp.Add(int.Parse(txt.getdef().GetValue(deck[i]).ToString()));
                }
                else
                {
                    defensaTemp.Add(0);
                }


            }
            for (int i = 0; i < deck.Count - 1; i++)
            {
                for (int j = 0; j < deck.Count - 1 - i; j++)
                {
                    if (defensaTemp[j] < defensaTemp[j + 1])
                    {

                        int actualPoder = defensaTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        defensaTemp[j] = defensaTemp[j + 1];
                        defensaTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;
                    }
                }

            }
        }
        else if (idOrdenDeck == 6)
        {
            orTipoDeck.color = new Color(1f, 1f, 1f, 1f);
            List<string> nombreTemp = new List<string>();
            //cartasCofre;
            for (int i = 0; i < deck.Count; i++)
            {
                if (txt.GetTipoCarta().GetValue(deck[i]).ToString().Trim().Equals("Monstruo"))
                {
                    nombreTemp.Add(txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(deck[i]).ToString())).ToString());
                }
                else
                {
                    nombreTemp.Add("z");
                }
            }
            //ataqueTemp.Sort(Compare);
            for (int i = 0; i < nombreTemp.Count - 1; i++)
            {
                for (int j = 0; j < nombreTemp.Count - 1 - i; j++)
                {
                    //int c=string.Compare()
                    if (string.Compare(nombreTemp[j], nombreTemp[j + 1]) == 1)
                    {
                        string actualPoder = nombreTemp[j];
                        int actualPos = deck[j];
                        deck[j] = deck[j + 1];
                        nombreTemp[j] = nombreTemp[j + 1];
                        nombreTemp[j + 1] = actualPoder;
                        deck[j + 1] = actualPos;



                    }
                }

            }



        }

        LimpiarListaDeck();
        for (int i = 0; i < deck.Count; i++)
        {

            ObtenerDatosCartaDeck(deck[i], i);
        }





    }
    public void ActualizarDeckCpu()
    {
        List<int> deckTemporal = new List<int>();
        int[] probabilidadDrop = new int[2048];
        string[] idDuelistas = importadorHistoria.GetSistema();
        string[] destinoDeck = importadorHistoria.GetDestinoDeck();
        string[] dropDeck = importadorHistoria.GetDropDeck();
        string[] duelistas = importadorHistoria.GetDuelistas();

        for (int i = 0; i < 40; i++)
        {
            deckTemporal.Add(0);
        }


        int duelista = 38;
        //obtener el id duelista 
        duelista = datosDuelo.GetIdDuelista();

        //obtener el deck
        int actual = 0;
        int guardarValor = 0;
        int indiceDeck = 0;
        int cartaAdquirir = 0;
        if (duelista > 1 && duelista < 39)
        {
            while (actual != duelista - 1)
            {
                if (destinoDeck[cartaAdquirir].Contains("s"))
                {
                    actual++;
                }
                cartaAdquirir++;
                guardarValor = cartaAdquirir;
            }
        }
        if (duelista < 39)
        {
            actual = 0;
            int actualCarta = 1;

            while (!destinoDeck[cartaAdquirir].Contains("s"))
            {

                int numeroDrop = int.Parse(dropDeck[cartaAdquirir]);
                for (int i = 0; i < numeroDrop; i++)
                {

                    probabilidadDrop[actual] = int.Parse(destinoDeck[cartaAdquirir]);
                    actual++;

                }
                cartaAdquirir++;
            }
            while (deckTemporal.Contains(0))
            {
                actual = 0;
                actualCarta = Random.Range(0, 2048);
                int contador = 0;
                for (int i = 0; i < 40; i++)
                {
                    if (probabilidadDrop[actualCarta] == deckTemporal[i])
                    {
                        contador++;
                    }
                }
                if (contador < 3)
                {
                    deckTemporal[indiceDeck] = probabilidadDrop[actualCarta];
                    indiceDeck++;
                }
            }
        }
        else
        {
            for (int i = 0; i < 40; i++)
            {
                deckTemporal[i] = (datosJuego.GetDeckUsuario()[i]);
            }


        }
        datosDuelo.SetDuelistaCpu(duelistas[duelista]);
        datosDuelo.SetDeckCpu(deckTemporal);
    }
    IEnumerator FlechaAbajoArriba()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        deslizadoRapido = true;
    }

    //Acciones de botones para android
    public void BotonC()
    {
        if (!fase.Equals("cerrar"))
        {
            if (deck.Count == 40)
            {
                datosJuego.SetDeckUsuario(deck);
                datosJuego.SetCofre(cartasCofre);
                datosJuego.SetCantidadCofre(cantidadCartasCofre);
                datosJuego.SetNuevo(idNuevasCartas);
                efectosSonido.CancelarAccion();
                if (datosJuego.GetFase().Equals("duelo"))
                {
                    if (datosDuelo.GetModoHistoria() == false)
                    {
                        ActualizarDeckCpu();
                    }

                    transicion.CargarEscena("Juego");
                }
                else if (datosJuego.GetFase().Equals("dueloLibre"))
                {
                    transicion.CargarEscena("DueloLibre");
                }
                else if (datosJuego.GetFase().Equals("menuContinuar"))
                {
                    transicion.CargarEscena("MenuContinuar");
                }
            }
            else
            {
                efectosSonido.NoFusion();
            }
        }
    }

    // acciones botones android
    public void BotonAbajoLiberado()
    {
        StopAllCoroutines();
        deslizadoRapido = false;
        Debug.LogError("entre acá");
    }
    public void BotonAbajo()
    {
        Debug.LogError("entre acá eva");
        if (!deslizadoRapido)
        {
            StartCoroutine(FlechaAbajoArriba());
            if (fase.Equals("cofre"))
            {
                if (indice < 722)
                {
                    efectosSonido.moverCarta();
                    indiceApCofre++;
                    if (indiceApCofre <= 7)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y -= yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 7;
                    }

                    indiceScroll++;
                    if (indiceScroll > 7)
                    {

                        indiceScroll = 8;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition - yScroll;

                    }
                    indice++;
                }

            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck < 39)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck++;
                    if (indiceScrollDeck <= 7)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y -= yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }


                    if (indiceScrollDeck > 7)
                    {
                        indiceScrollDeck = 8;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition - yScrollDeck;

                    }
                    indiceDeck++;
                }
            }
        }
        else
        {
            if (fase.Equals("cofre"))
            {
                if (indice < 722)
                {
                    efectosSonido.moverCarta();
                    indiceApCofre++;
                    if (indiceApCofre <= 7)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y -= yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 7;
                    }

                    indiceScroll++;
                    if (indiceScroll > 7)
                    {

                        indiceScroll = 8;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition - yScroll;

                    }
                    indice++;
                }

            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck < 39)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck++;
                    if (indiceScrollDeck <= 7)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y -= yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }


                    if (indiceScrollDeck > 7)
                    {
                        indiceScrollDeck = 8;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition - yScrollDeck;

                    }
                    indiceDeck++;
                }
            }
        }
    }
    public void BotonArriba()
    {
        if (deslizadoRapido)
        {
            if (fase.Equals("cofre"))
            {
                if (indice != 1)
                {
                    efectosSonido.moverCarta();
                    indiceScroll--;
                    indiceApCofre--;
                    if (indiceApCofre > 0)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y += yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 1;
                    }

                    if (indiceScroll == 7)
                    {
                        indiceScroll = 6;
                    }
                    if (indiceScroll < 1)
                    {
                        indiceScroll = 1;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition + yScroll;

                    }
                    indice--;

                }
            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck != 0)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck--;
                    if (indiceScrollDeck > 0)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y += yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }

                    if (indiceScrollDeck == 7)
                    {
                        indiceScrollDeck = 6;
                    }
                    if (indiceScrollDeck < 1)
                    {
                        indiceScrollDeck = 1;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition + yScrollDeck;

                    }
                    indiceDeck--;

                }
            }



        }
        if (!deslizadoRapido)
        {
            StartCoroutine(FlechaAbajoArriba());
            if (fase.Equals("cofre"))
            {
                if (indice != 1)
                {
                    efectosSonido.moverCarta();
                    indiceScroll--;
                    indiceApCofre--;
                    if (indiceApCofre > 0)
                    {

                        Vector3 pos = apuntador.transform.localPosition;
                        pos.y += yOffset;
                        apuntador.transform.localPosition = pos;
                    }
                    else
                    {
                        indiceApCofre = 1;
                    }

                    if (indiceScroll == 7)
                    {
                        indiceScroll = 6;
                    }
                    if (indiceScroll < 1)
                    {
                        indiceScroll = 1;
                        scroll.verticalNormalizedPosition = scroll.verticalNormalizedPosition + yScroll;

                    }
                    indice--;

                }
            }
            else if (fase.Equals("deck"))
            {
                if (indiceDeck != 0)
                {
                    efectosSonido.moverCarta();
                    indiceScrollDeck--;
                    if (indiceScrollDeck > 0)
                    {
                        Vector3 pos = apuntadorDeck.transform.localPosition;
                        pos.y += yOffset;
                        apuntadorDeck.transform.localPosition = pos;
                    }

                    if (indiceScrollDeck == 7)
                    {
                        indiceScrollDeck = 6;
                    }
                    if (indiceScrollDeck < 1)
                    {
                        indiceScrollDeck = 1;
                        scrollDeck.verticalNormalizedPosition = scrollDeck.verticalNormalizedPosition + yScrollDeck;

                    }
                    indiceDeck--;

                }
            }



        }

    }
    public void BotonArribaLiberado()
    {
        StopAllCoroutines();
        deslizadoRapido = false;

    }
    public void BotonIzquierda()
    {
        if (fase.Equals("deck"))
        {
            efectosSonido.CambiarPosicionCarta();
            panelCofre.SetActive(true);
            panelDeck.SetActive(false);
            fase = "cofre";


        }
    }
    public void BotonDerecha()
    {
        if (fase.Equals("cofre"))
        {
            efectosSonido.CambiarPosicionCarta();
            panelCofre.SetActive(false);
            panelDeck.SetActive(true);
            fase = "deck";
        }
    }
    public void BotonD()
    {
        if (fase.Equals("cofre"))
        {

            estaEnDeck = false;
            panelGuardian.SetActive(false);
            int cartaReemplazo = indice - 1;
            int cantidadReemplazo = indice - 1;
            if (cartaReemplazo >= cartasCofre.Count)
            {
                cartaReemplazo = -1;
            }
            if (idOrden == 1)
            {
                cartaReemplazo = cartasCofre.IndexOf(indice);
                cantidadReemplazo = cartaReemplazo;

            }

            if (cartaReemplazo != -1)
            {
                fase = "cerrar";
                panelCarta.transform.Find("imgcarta").GetComponent<Image>().sprite = (Sprite)txt.cartasBatalla.GetValue(cartasCofre[cartaReemplazo]);

                if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Monstruo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                }

                else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Equipo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);

                }
                else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Campo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "CAMPO";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                }
                else if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Magica"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "MAGICA";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                }
                else
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
                }


                if (txt.GetTipoCarta().GetValue(cartasCofre[cartaReemplazo]).ToString().Trim().Equals("Monstruo"))
                {
                    panelGuardian.SetActive(true);
                    panelGuardian.transform.Find("imgua1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    panelGuardian.transform.Find("imgua2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    panelGuardian.transform.Find("textogua1").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos1().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                    panelGuardian.transform.Find("textogua2").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos2().GetValue(cartasCofre[cartaReemplazo]).ToString()));
                }
                panelCarta.SetActive(true);

            }
        }
        else if (fase.Equals("deck"))
        {

            estaEnDeck = true;
            panelGuardian.SetActive(false);
            int cartaReemplazo = indice - 1;
            int cantidadReemplazo = indice - 1;
            if (deck.Count > 0 && indiceDeck < deck.Count)
            {
                fase = "cerrar";
                panelCarta.transform.Find("imgcarta").GetComponent<Image>().sprite = (Sprite)txt.cartasBatalla.GetValue(deck[indiceDeck]);

                if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Monstruo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNombreTipoCarta().GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(deck[indiceDeck]).ToString()));
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(int.Parse(txt.GetNumeroTipoCarta().GetValue(deck[indiceDeck]).ToString()));
                }

                else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Equipo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "EQUIPO";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(22);

                }
                else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Campo"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "CAMPO";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                }
                else if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Magica"))
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "MAGICA";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(24);
                }
                else
                {
                    panel1.transform.Find("textotipo").GetComponent<TextMeshProUGUI>().text = "TRAMPA";
                    panel1.transform.Find("imgtipo").GetComponent<RawImage>().texture = (Texture2D)txt.atirbutos.GetValue(23);
                }


                if (txt.GetTipoCarta().GetValue(deck[indiceDeck]).ToString().Trim().Equals("Monstruo"))
                {
                    panelGuardian.SetActive(true);
                    panelGuardian.transform.Find("imgua1").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos1().GetValue(deck[indiceDeck]).ToString()));
                    panelGuardian.transform.Find("imgua2").GetComponent<RawImage>().texture = (Texture2D)txt.guardianes.GetValue(int.Parse(txt.GetAtributos2().GetValue(deck[indiceDeck]).ToString()));
                    panelGuardian.transform.Find("textogua1").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos1().GetValue(deck[indiceDeck]).ToString()));
                    panelGuardian.transform.Find("textogua2").GetComponent<TextMeshProUGUI>().text = (string)txt.GetNomAtributo().GetValue(int.Parse(txt.GetAtributos2().GetValue(deck[indiceDeck]).ToString()));
                }
                panelCarta.SetActive(true);

            }
        }
        else if (fase.Equals("cerrar"))
        {
            panelCarta.SetActive(false);
            if (estaEnDeck)
            {
                fase = "deck";
            }
            else
            {
                fase = "cofre";
            }

        }
    }
    public void BotonZ()
    {
        if (fase.Equals("cofre"))
        {
            //validar que hay menos de 40 cartas en deck
            numeroEnDeck = 0;
            int cartaReemplazo = indice - 1;
            int cantidadReemplazo = indice - 1;
            if (idOrden == 1)
            {
                cartaReemplazo = cartasCofre.IndexOf(indice);
                cantidadReemplazo = cartaReemplazo;

            }

            if (cartaReemplazo != -1)
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    if (cartasCofre[cartaReemplazo].Equals(deck[i]))
                    {
                        numeroEnDeck++;
                    }
                }



                if (deck.Count < 40 && cantidadCartasCofre[cantidadReemplazo] > 0 && numeroEnDeck < 3)
                {

                    cantidadCartasCofre[cantidadReemplazo] = cantidadCartasCofre[cantidadReemplazo] - 1;
                    cantidadCofre--;
                    deck.Add(cartasCofre[cartaReemplazo]);


                    totalDeck.text = "" + deck.Count;
                    totalDeckEnDeck.text = "" + deck.Count;
                    cantidadCofreTexto.text = "" + cantidadCofre;
                    cantidadCofreTextoDeck.text = "" + cantidadCofre;
                    LimpiarLista();
                    for (int i = 0; i < cartasCofre.Count; i++)
                    {
                        ObtenerDatosCarta(cartasCofre[i], i);

                    }
                    OrdenarDeck();

                    if (deck.Count == 40)
                    {
                        totalDeck.color = Color.white;
                        totalDeckEnDeck.color = Color.white;
                    }
                    efectosSonido.SeleccionarCarta();

                }
            }
            else
            {
                efectosSonido.NoFusion();
            }


        }
        else if (fase.Equals("deck"))
        {
            if (deck.Count > 0 && indiceDeck < deck.Count)
            {
                int conteo = cartasCofre.IndexOf(deck[indiceDeck]);
                cantidadCartasCofre[conteo] = cantidadCartasCofre[conteo] + 1;
                cantidadCofre++;
                deck.Remove(deck[indiceDeck]);
                LimpiarListaDeck();

                for (int i = 0; i < deck.Count; i++)
                {
                    ObtenerDatosCartaDeck(deck[i], i);

                }

                OrdenarCofre();
                if (indiceDeck != 0)
                {

                    totalCartasDeck--;
                }
                totalDeck.text = "" + deck.Count;
                totalDeck.color = Color.red;
                totalDeckEnDeck.color = Color.red;
                cantidadCofreTextoDeck.text = "" + cantidadCofre;
                totalDeckEnDeck.text = "" + deck.Count;
                cantidadCofreTexto.text = "" + cantidadCofre;

                efectosSonido.SeleccionarCarta();
            }
            else
            {
                efectosSonido.NoFusion();
            }

        }
    }
    public void Boton1()
    {

        if (fase.Equals("cofre"))
        {
            efectosSonido.Descarte();
            idOrden++;
            if (idOrden == 8)
            {
                idOrden = 1;
            }

            //totalCartasTexto.text = "1/685";
            OrdenarCofre();

        }
        else if (fase.Equals("deck"))
        {
            efectosSonido.Descarte();
            idOrdenDeck++;
            if (idOrdenDeck == 7)
            {
                idOrdenDeck = 1;
            }
            OrdenarDeck();
        }
    }





}
