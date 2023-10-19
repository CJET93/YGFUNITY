using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicaTienda : MonoBehaviour
{
    public GameObject imagenPack2;
    public GameObject imagenPack3;
    public GameObject imagenPack1;
    public GameObject imagenComprar2;
    public GameObject imagenComprar3;
    public GameObject imagenValor2;
    public GameObject imagenValor3;
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosDuelo datosDuelo;
    private DatosJuego datosJuego;
    public GameObject[] clon;
    public GameObject original;
    public ImportadorTextos txt;
    public GameObject imagenCarta;
    public GameObject contenedor;
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    int posX;
    int posY;
    private bool habilitarPack2;
    private bool habilitarPack3;
    public int[] cartas;
    public GameObject logoTienda;
    public TextMeshProUGUI estrellas;
    private bool desactivarControles;
    private string fase;
    public transicion transicion;
    public RevisarDatos revisarDatos;
    public int valorPack1 = 100;
    public int valorPack2 = 500;
    public int valorPack3 = 1000;
    public TextMeshProUGUI valorPack1Txt;
    public TextMeshProUGUI valorPack2Txt;
    public TextMeshProUGUI valorPack3Txt;
    public TextMeshProUGUI textoDescuento;
    // Start is called before the first frame update
    void Start()
    {
        habilitarPack2 = false;
        habilitarPack3 = false; 
        clon = new GameObject[6];
        cartas = new int[6];
        datosJuego.SetEstrellas(datosJuego.GetEstrellas());
        estrellas.text = "Estrellas X"+datosJuego.GetEstrellas();
        desactivarControles = false;
        fase = "packs";
        ValidarPacks();
        Validar();
    }
    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
    }
    public void ValidarPacks()
    {
        for (int i = 1; i < datosJuego.GetVictorias().Length; i++)
        {
            habilitarPack2 = false;
            habilitarPack3 = false;
            if (datosJuego.GetVictorias()[i] >= 100)
            {
                
                habilitarPack2 = true;
                habilitarPack3 = true;
            }
            else if (datosJuego.GetVictorias()[i] >= 25)
            {
                habilitarPack2 = true;
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        if(datosJuego.Descuento > 0)
        {
            valorPack1 = 100/2;
            valorPack1Txt.color = Color.yellow;
            valorPack2Txt.color = Color.yellow;
            valorPack3Txt.color = Color.yellow;
            valorPack2 = 500/2;
            valorPack3 = 1000/2;
            textoDescuento.text = "TIEMPO DESCUENTO EN PACKS "+datosJuego.Descuento.ToString("0");
        }
        else
        {
            valorPack1Txt.color = Color.blue;
            valorPack2Txt.color = Color.blue;
            valorPack3Txt.color = Color.blue;
            textoDescuento.text = "";
            valorPack1 = 100;
            valorPack2 =500;
            valorPack3 = 1000;
        }
        valorPack1Txt.text = "x"+valorPack1;
        valorPack2Txt.text = "x"+valorPack2;
        valorPack3Txt.text = "x"+valorPack3;
        if (!desactivarControles)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (fase.Equals("packs"))
                {
                    desactivarControles = true;
                    efectosSonido.CancelarAccion();
                    transicion.CargarEscena("MenuContinuar");
                }
                else
                {
                    fase = "packs";
                    efectosSonido.CancelarAccion();
                    HabilitarTodo();
                }
            }
        }
    }
    public void Validar()
    {
        imagenPack2.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        imagenComprar2.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        imagenPack3.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        imagenComprar3.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        imagenValor2.SetActive(false);
        imagenValor3.SetActive(false);
        if (habilitarPack2)
        {
            imagenPack2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            imagenComprar2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            imagenValor2.SetActive(true);
        }
        if (habilitarPack3)
        {
            imagenPack3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            imagenComprar3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            imagenValor3.SetActive(true);
        }
    }
    public void ComprarPack(int valor)
    {
        //pack3
     
        if (valor == 500)
        {
            if (habilitarPack2)
            {
                if (datosJuego.GetEstrellas() >= valorPack2)
                {
                    efectosSonido.SeleccionarCarta();
                    desactivarControles = true;
                    datosJuego.SetEstrellas(datosJuego.GetEstrellas() - valorPack2);
                    EsconderTodo();
                    llenarListaCartas(500);
                    guardarCartas();
                    CrearInstanciasCartas();
                }
                else
                {
                    efectosSonido.NoFusion();
                }
            }
            else
            {
                efectosSonido.NoFusion();
            }
        }
        if (valor == 1000)
        {
            if (habilitarPack3)
            {
                if (datosJuego.GetEstrellas() >= valorPack3)
                {
                    efectosSonido.SeleccionarCarta();
                    desactivarControles = true;
                    datosJuego.SetEstrellas(datosJuego.GetEstrellas() - valorPack3);
                    EsconderTodo();
                    llenarListaCartas(1000);
                    guardarCartas();
                    CrearInstanciasCartas();
                }
                else
                {
                    efectosSonido.NoFusion();
                }
            }
            else
            {
                efectosSonido.NoFusion();
            }
        }
        if (valor == 100)
        {          
                if (datosJuego.GetEstrellas() >= valorPack1)
                {
                    efectosSonido.SeleccionarCarta();
                    desactivarControles = true;
                    datosJuego.SetEstrellas(datosJuego.GetEstrellas() - valorPack1);
                    EsconderTodo();                    
                    llenarListaCartas(100);
                    guardarCartas();
                    CrearInstanciasCartas();
                }
                else
                {
                    efectosSonido.NoFusion();
                }
            
        }

    }
    public void guardarCartas()
    {
        for (int i = 0; i < cartas.Length; i++)
        {

            int idCarta = cartas[i];
            if (!datosJuego.GetCofre().Contains(idCarta))
            {
                datosJuego.AñadirElementoTienda(idCarta);
                datosJuego.AñadirElementoCofre(idCarta);
                datosJuego.AñadirCantidadCofre(1);
                datosJuego.IncrementarIdNueva();
                datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());
            }
            else
            {
                int conteo = datosJuego.GetCofre().IndexOf(idCarta);
                int cantidadActualCofre = datosJuego.GetCantidadCofre()[conteo];
                datosJuego.RemoverCantidadCofre(conteo);
                datosJuego.RemoverElementoCofre(conteo);
                datosJuego.RemoverIdNueva(conteo);
                datosJuego.AñadirElementoTienda(idCarta);
                datosJuego.AñadirElementoCofre(idCarta);
                datosJuego.AñadirCantidadCofre(cantidadActualCofre + 1);
                datosJuego.IncrementarIdNueva();
                datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());

            }
        }
        SistemaGuardado.Guardar(datosJuego);
    }
    public void EsconderTodo()
    {
        estrellas.text = "Estrellas X" + datosJuego.GetEstrellas();
        imagenPack1.SetActive(false);
        imagenPack2.SetActive(false);
        imagenPack3.SetActive(false);
        logoTienda.SetActive(false);
        textoDescuento.gameObject.SetActive(false);
        estrellas.gameObject.SetActive(false);
    }
    public void HabilitarTodo()
    {
        textoDescuento.gameObject.SetActive(true);
        imagenPack1.SetActive(true);
        imagenPack2.SetActive(true);
        imagenPack3.SetActive(true);
        logoTienda.SetActive(true);
        estrellas.gameObject.SetActive(true);
        for(int i = 0; i < clon.Length; i++)
        {
            GameObject.Destroy(clon[i]);
        }
      
    }
    public void CrearInstanciasCartas()
    {
        int pos = 0;
        posX = 0;
        posY = 0;
        for (int i = 0; i < 2; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                clon[pos] = Instantiate(original, new Vector3(original.transform.localPosition.x + posX, original.transform.localPosition.y + posY, original.transform.position.z), original.transform.rotation);
               

                clon[pos].transform.SetParent(contenedor.transform, false);
                clon[pos].SetActive(true);
                posX += 500;
                pos++;
            }
            posX = 0;
            posY -= 500;
        }
        AnimacionEntraCarta();


    }
    public void AnimacionEntraCarta()
    {
       
            StartCoroutine(EmpezarAnimacionEntraCarta());
        
        //imagenCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(idCarta);
    }
    IEnumerator EmpezarAnimacionEntraCarta()
    {
        for (int i = 0; i < clon.Length; i++)
        {
            bool realizada = false;
            bool animacion1 = false;

            while (!realizada)
            {
                clon[i].transform.transform.Rotate(0 * Time.fixedDeltaTime, 900 * Time.fixedDeltaTime, 0 * Time.fixedDeltaTime);
                if (clon[i].transform.rotation.eulerAngles.y > 90 && animacion1 == false)
                {
                    clon[i].transform.eulerAngles = new Vector3(0, -90, 0);
                    GameObject cardContainer = clon[i].transform.Find("noReverseContainer").gameObject;
                    cardContainer.SetActive(true);
                    cardContainer.transform.Find("cardImage").GetComponent<Image>().sprite = (Sprite)txt.cartas1.GetValue(cartas[i]);
                    cardContainer.transform.Find("cardName").GetComponent<TextMeshProUGUI>().text = txt.nombresCartas.GetValue(cartas[i]).ToString();
                    float fontSize = GetFontCardName(txt.nombresCartas.GetValue(cartas[i]).ToString());
                    cardContainer.transform.Find("cardName").GetComponent<TextMeshProUGUI>().fontSize = fontSize;
                    cardContainer.transform.Find("monsterContainer/cardAtk").GetComponent<TextMeshProUGUI>().text = "Atk "+txt.getatk().GetValue(cartas[i]).ToString();
                    cardContainer.transform.Find("monsterContainer/cardDef").GetComponent<TextMeshProUGUI>().text = "Def " + txt.getatk().GetValue(cartas[i]).ToString();
                      if (!txt.GetTipoCarta().GetValue(cartas[i]).ToString().Trim().Equals("Monstruo"))
                    {
                        cardContainer.transform.Find("specialContainer").gameObject.SetActive(true);
                        if (txt.GetTipoCarta().GetValue(cartas[i]).ToString().Trim().Equals("Trampa"))
                        {
                            cardContainer.transform.Find("specialContainer/trapContainer").gameObject.SetActive(true);
                        }
                    }
                    GetAttribute(cardContainer, cartas[i]);
                    getStars(cardContainer, cartas[i]);
                    animacion1 = true;


                }
                if (clon[i].transform.rotation.eulerAngles.y < 180 && animacion1 == true)
                {
                    clon[i].transform.rotation = (Quaternion.Euler(0, 0, 0));
                    realizada = true;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
        desactivarControles = false;
      
      
       
      
        //botonCambiar.SetActive(true);
        //botonCancelar.SetActive(true);

    }

    private void GetAttribute(GameObject cardContainer,int cardNumber)
    {
        string name = txt.attributeText[cardNumber];
        int indice = -1;

        for (int i = 0; i < txt.attributeImages.Length; i++)
        {
            if (txt.attributeImages[i].name == name)
            {
                indice = i;
                break;
            }
        }

        if (indice != -1)
        {
            cardContainer.transform.Find("cardAttribute").GetComponent<Image>().sprite = txt.attributeImages[indice];
        }
        else
        {
            Debug.LogError($"Sprite con nombre '{name}' no encontrado en txt.attributeImages.");
        }
    }

    private float GetFontCardName(string name)
    {
        float fontSize;
            if (name.Length > 29)
            {
                fontSize = 20f;
            }
            else if (name.Length > 20)
            {
                fontSize = 25f;
            }
            else if (name.Length > 16)
            {
                fontSize = 30f;
            }
            else
            {
                fontSize = 40f;
            }

        return fontSize;
    }


    private void getStars(GameObject container, int cardNumber)
    {
        int numberOfStarsToShow = int.Parse((string)txt.GetStars().GetValue(cardNumber));
        int stars = container.transform.Find("monsterContainer/starsContainer").childCount;
        for (int i = 0; i < stars; i++)
        {
            Transform star = container.transform.Find("monsterContainer/starsContainer").GetChild(i);
            star.gameObject.SetActive(i < numberOfStarsToShow);
        }
    }
    public void llenarListaCartas(int valor)
    {
        fase = "cartas";
        if (valor == 100)
        {
            int random = Random.Range(1, 631);
            int valorTotal = int.Parse(txt.getatk()[random]) ;
            int probabilidad = Random.Range(1, 100);
            for (int i = 0; i < cartas.Length - 1; i++)
            {
                    while (valorTotal > 2000 || int.Parse(txt.GetPots()[random]) == 1)
                    {
                        random = Random.Range(1, 631);
                        valorTotal = (int.Parse(txt.getatk()[random])) ;
                    }
                cartas[i] = random;
                random = Random.Range(1, 631);
                valorTotal = (int.Parse(txt.getatk()[random]));
            }
            random = Random.Range(1, 631);
            valorTotal = (int.Parse(txt.getatk()[random]));
            if (probabilidad == 99)
            {
                while (valorTotal < 3000)
                {
                    random = Random.Range(1, 631);
                    valorTotal = (int.Parse(txt.getatk()[random]));
                }
            }
            else
            {
                while (valorTotal < 1500 || valorTotal > 2500)
                {
                    random = Random.Range(1, 631);
                    valorTotal = (int.Parse(txt.getatk()[random]));
                }
            }
            cartas[5] = random;

        }
        else if (valor == 500)
        {
            int random = Random.Range(1, Constants.TOTAL_CARDS);
            int valorTotal = int.Parse(txt.getatk()[random]);
            int probabilidad = Random.Range(1, 100);
            for (int i = 0; i < cartas.Length - 1; i++)
            {
                if (probabilidad == 99)
                {
                    while (valorTotal < 3000)
                    {
                        random = Random.Range(1, Constants.TOTAL_CARDS);
                        valorTotal = (int.Parse(txt.getatk()[random]));
                    }
                }
                else
                {
                    while (valorTotal > 3000)
                    {
                        random = Random.Range(1, Constants.TOTAL_CARDS);
                        valorTotal = (int.Parse(txt.getatk()[random]));
                    }
                }
                Debug.LogWarning(probabilidad);
                cartas[i] = random;
                random = Random.Range(1, Constants.TOTAL_CARDS);
                valorTotal = (int.Parse(txt.getatk()[random]));
                probabilidad = Random.Range(1, 100);
            }
            random = Random.Range(1, Constants.TOTAL_CARDS);
            valorTotal = (int.Parse(txt.getatk()[random]));
            while (valorTotal <1800 || valorTotal > 3500)
            {
                random = Random.Range(1, Constants.TOTAL_CARDS);
                valorTotal = (int.Parse(txt.getatk()[random]));
            }
            cartas[5] = random;

        }
        else if (valor == 1000)
        {
            int random = Random.Range(1, Constants.TOTAL_CARDS);
            int valorTotal = int.Parse(txt.getatk()[random]);
            int probabilidad = Random.Range(1, 100);
            for (int i = 0; i < cartas.Length - 1; i++)
            {
                if (probabilidad == 99)
                {
                    Debug.LogWarning(probabilidad);
                    while (valorTotal < 3000 || int.Parse(txt.GetPots()[random]) > 3)
                    {
                        random = Random.Range(1, Constants.TOTAL_CARDS);
                        valorTotal = (int.Parse(txt.getatk()[random]));

                    }
                }
                else
                {
                    while (valorTotal > 4000 || int.Parse(txt.GetPots()[random]) > 3)
                    {
                        random = Random.Range(1, Constants.TOTAL_CARDS);
                        valorTotal = (int.Parse(txt.getatk()[random]));
                    }
                }
                cartas[i] = random;
                random = Random.Range(1, Constants.TOTAL_CARDS);
                valorTotal = (int.Parse(txt.getatk()[random]));
                probabilidad = Random.Range(1, 100);
            }
            random = Random.Range(1, Constants.TOTAL_CARDS);
            valorTotal = (int.Parse(txt.getatk()[random]));
     
            while (valorTotal < 2400 || valorTotal >4900)
            {
                random = Random.Range(1, Constants.TOTAL_CARDS);
                valorTotal = (int.Parse(txt.getatk()[random]));
            }
            cartas[5] = random;

        }
    }

    public void BotonC()
    {
        if (!desactivarControles)
        {
                if (fase.Equals("packs"))
                {
                    desactivarControles = true;
                    efectosSonido.CancelarAccion();
                    transicion.CargarEscena("MenuContinuar");
                }
                else
                {
                    fase = "packs";
                    efectosSonido.CancelarAccion();
                    HabilitarTodo();
                }
            }
        
    }

}
