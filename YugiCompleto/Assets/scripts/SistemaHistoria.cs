using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SistemaHistoria : MonoBehaviour
{
   
    public ImportadorHistoria importadorHistoria;
    public List<int> deckTemporal = new List<int>();
    public List<int> deckInicio = new List<int>();
    public int[] probabilidadDrop = new int[2048];
    public string fase = "";
    public int inicioDialogos = 0;
    public string[] dialogos;
    public string[] sistema;
    private string[] imagen;
    public string[] sonido;
    public string[] sistemaDialogos;
    public int idImagen;
    public int idImagenActual;
    public TextMeshProUGUI textoDialogos;
    public Button botonSi;
    public Button botonNo;
    public Button botonOceano;
    public Button botonDesierto;
    public Button botonBosque;
    public Button botonMontana;
    public Button botonPradera;
    public Button botonSagrado;
    public bool duelo;
    public Image imagenDialogo;
    public transicion transicion;
    public cambioFondo cambioFondo;
    private AudioSource fuente;
    public MusicaHistoria musicaHistoria;
    public transicionImagen transicionImagen;
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    private DatosDuelo datosDuelo;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
        fuente = GetComponent<AudioSource>();
        idImagenActual = -1;
        Dialogos();
        if (datosJuego.GetDeckUsuario().Count == 0)
        {
            DeckInicial();
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Z))
        {
            transicionImagen.Iniciar();
            if (fase.Equals("activarDialogos"))
            {
                if (inicioDialogos < dialogos.Length - 1)
                {
                    fase = "";
                    fuente.Play();
                    inicioDialogos++;
                    textoDialogos.text = "";
                    StartCoroutine(muestraLetras());
                   
                }
               
              
            }
          
        }
    }
    IEnumerator muestraLetras()
    {
       
       
        idImagen = int.Parse(imagen[inicioDialogos]);
        if (idImagen != idImagenActual)
        {
            idImagenActual = idImagen;
            float desaparecer = 0.1f;
            while (desaparecer <= 1.1f)
            {

                transicionImagen.GetComponent<Image>().color = new Color(0f, 0f, 0f, desaparecer);
                desaparecer += 0.1f;
                yield return new WaitForSeconds(0.05f);
               
            }
            cambioFondo.GetComponent<Image>().sprite = (Sprite)importadorHistoria.imagenes.GetValue(idImagen);
            while (desaparecer >= 0)
            {
                transicionImagen.GetComponent<Image>().color = new Color(0f, 0f, 0f, desaparecer);
                desaparecer -= 0.1f;
                yield return new WaitForSeconds(0.05f);
              
            }


        }
        if (int.Parse(sonido[inicioDialogos]) != -1)
        {
            musicaHistoria.reproducirMusica(int.Parse(sonido[inicioDialogos]));
        }
        cambioFondo.GetComponent<Image>().sprite = (Sprite)importadorHistoria.imagenes.GetValue(idImagen);
        foreach (char letra in dialogos[inicioDialogos].ToCharArray())
        {
            textoDialogos.text += letra;
            yield return new WaitForSeconds(0.01f);
        }
        if (sistemaDialogos[inicioDialogos].Contains("opc)"))
        {
          
            fase = "";
            if (datosJuego.GetHistoria() > 22)
            {
                botonSi.GetComponentInChildren<Text>().text = "Proceder";
                botonNo.GetComponentInChildren<Text>().text = "Regresar";
            }
            botonSi.gameObject.SetActive(true);
            botonNo.gameObject.SetActive(true);
        }
        else if (sistemaDialogos[inicioDialogos].Contains("duelo)"))
        {
            datosDuelo.SetModoHistoria(true);
            fase = "";
            QueDeck(true);
        }
        else if (sistemaDialogos[inicioDialogos].Contains("salir"))
        {
            datosJuego.SetHistoria(datosJuego.GetHistoria() + 1);
            datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 3);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("sumar"))
        {
            if (datosJuego.GetHistoria() == 4 && datosJuego.GetEventosHistoria() == 14)
            {
                datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 1);
            }
            else
            {
                datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 2);
            }
            datosJuego.SetHistoria(datosJuego.GetHistoria() + 1);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("secmenton"))
        {
            datosJuego.SetMagos(0,1);
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("martis"))
        {
            datosJuego.SetMagos(1, 1);
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("anubisius"))
        {
            datosJuego.SetMagos(2, 1);
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("atenza"))
        {
            datosJuego.SetMagos(3, 1);
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("kepura"))
        {
            datosJuego.SetMagos(4, 1);
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("seto2"))
        {
            datosJuego.desbloqueoSeto2 = true;
            datosJuego.SetEventosHistoria(69);
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if (sistemaDialogos[inicioDialogos].Contains("magos"))
        {
            fase = "";
            botonOceano.gameObject.SetActive(true);
            botonDesierto.gameObject.SetActive(true);
            botonBosque.gameObject.SetActive(true);
            botonMontana.gameObject.SetActive(true);
            botonPradera.gameObject.SetActive(true);
            botonSagrado.gameObject.SetActive(true);

        }
        else if ((sistemaDialogos[inicioDialogos].Contains("fin")))
        {
            fase = "";
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("FinJuego");
        }
        else if ((sistemaDialogos[inicioDialogos].Contains("continuar")))
        {
            fase = "";
            datosJuego.SetEventosHistoria(69);
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("MenuContinuar");
        }
        else if ((sistemaDialogos[inicioDialogos].Contains("creditos")))
        {
            fase = "";
            // ACA DEBE CAMBIAR ES A UNA NUEVA ESCENA DE CREDITOS Y GUARDAR AUTOMATICAMENTE EN EL SLOT QUE ESTA POR DEFECTO
            //POR AHORA SOLO IRA AL MENU COMNTINUAR     
            datosJuego.SetEventosHistoria(0);
            datosJuego.SetHistoria(0);
            datosJuego.SetEsModoHistoriaCompleto(true);
            datosJuego.magos = new int[5];
            SistemaGuardado.Guardar(datosJuego);
            musicaHistoria.detenerMusica();
            transicion.CargarEscena("Creditos");
        }
        else
        {
            fase = "activarDialogos";
        }
       
    }
    public void Dialogos()
    {
      sistema = importadorHistoria.GetSistema();
      dialogos = importadorHistoria.GetDialogos();
          string historia="(e"+datosJuego.GetEventosHistoria()+")";
        bool salir = false;
        bool inicio = false;
        //obtener el tamaño del arreglo
        int tamaño = 0;
        for(int i=0;i<sistema.Length&& salir == false;i++)
        {
            if (sistema[i].Contains(historia))
            {
                tamaño++;
                if (!sistema[i + 1].Contains(historia))
                {
                    salir = true;
                }
            }
           
           
        }
        
        salir = false;
        dialogos = new string[tamaño];
        sistemaDialogos = new string[tamaño];
        imagen = new string[tamaño];
        sonido = new string[tamaño];
        int indice = 0;
        for (int i = 0; i < sistema.Length && salir == false; i++)
        {
            if (sistema[i].Contains(historia))
            {
                sonido[indice] = importadorHistoria.GetSonidoss().GetValue(i).ToString();
                imagen[indice] = importadorHistoria.GetImagenes().GetValue(i).ToString();
                sistemaDialogos[indice] = importadorHistoria.GetSistema().GetValue(i).ToString();
                if (sistema[i].Contains("(jugador)"))
                {
                    dialogos[indice] = importadorHistoria.GetDialogos().GetValue(i).ToString().Trim()+" "+datosJuego.GetNombreJugador();
                }
                else if ((sistema[i].Contains("estrellas")))
                {
                    int est = DarEstrellas();
                    dialogos[indice] = importadorHistoria.GetDialogos().GetValue(i).ToString().Trim() + " " + est+" Estrellas"; 
                }
                else
                {
                    dialogos[indice] = importadorHistoria.GetDialogos().GetValue(i).ToString();
                }

                
               if (!sistema[i+1].Contains(historia))
                {
                    salir = true;
                }
                if (inicio == false)
                {
                    idImagen = i;
                    inicio = true;
                }
                indice++;
            }
        }

        idImagen = int.Parse(imagen[inicioDialogos]);
        if (idImagenActual == -1)
        {
            idImagenActual = idImagen;
        }
      
        // inicioDialogos++;
        imagenDialogo.gameObject.SetActive(true);
        //fase = "activarDialogos";
        StartCoroutine(muestraLetras());
        //QueDeck(true);
    }
    public void QueDeck(bool esHistoria)
    {
        musicaHistoria.detenerMusica();
       
        string[] idDuelistas = importadorHistoria.GetSistema();
        string []destinoDeck = importadorHistoria.GetDestinoDeck();
        string[] dropDeck = importadorHistoria.GetDropDeck();
        string[] duelistas = importadorHistoria.GetDuelistas();
       
        for(int i = 0; i < 40; i++)
        {
            deckTemporal.Add(0);
        }
       
       
        int duelista = 38;
        //obtener el id duelista 
        if (datosDuelo.GetModoHistoria() == true)
        {
            if (datosJuego.GetHistoria() < 23 || datosJuego.GetHistoria()>33)
            {
                duelista = datosJuego.GetHistoria() + 1;
                datosDuelo.SetIdDuelista(duelista);
            }
            else
            {
                duelista = datosDuelo.GetIdDuelista();
            }
           
        }
        //obtener el deck
        int actual = 0;
        int guardarValor = 0;
        int indiceDeck = 0;
        int cartaAdquirir = 0;
        if (duelista != 1)
        {
            while (actual != duelista - 1)
            {
                if (destinoDeck[cartaAdquirir].Contains("s")){
                    actual++;
                }
                cartaAdquirir++;
                guardarValor = cartaAdquirir;
            }
        }
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
            for(int i = 0; i<40; i++){
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
      
        datosDuelo.SetDuelistaCpu(duelistas[duelista]);
        datosDuelo.SetDeckCpu(deckTemporal);
        datosJuego.SetFase("duelo");
        transicion.CargarEscena("CrearDeck");

        
    }
    public void IrDuelo()
    {
       
        botonSi.gameObject.SetActive(false);
        botonNo.gameObject.SetActive(false);
        if (datosJuego.GetHistoria() > 22)
        {
            datosDuelo.SetIdDuelista(datosDuelo.GetIdDuelista()+1);
            datosJuego.SetHistoria(24);
            inicioDialogos = 0;
            textoDialogos.text = "";
            datosJuego.SetEventosHistoria(datosJuego.GetEventosHistoria() + 2);
            Dialogos();
        }
        else
        {
            inicioDialogos++;
            textoDialogos.text = "";
            StartCoroutine(muestraLetras());
        }
        

    }
    public void SaltarDuelo()
    {
      
        botonSi.gameObject.SetActive(false);
        botonNo.gameObject.SetActive(false);
        if (datosJuego.GetHistoria() > 22)
        {
            inicioDialogos++;
            textoDialogos.text = "";
            StartCoroutine(muestraLetras());
        }
        else
        {
            inicioDialogos += 3;
            textoDialogos.text = "";
            StartCoroutine(muestraLetras());
        }
       
    }
    public void DeckInicial()
    {
        int contador = 0;
        string[] deckInicial = importadorHistoria.GetDeckInicial();
        for(int i = 0; i < 40; i++)
        {
            deckInicio.Add(0);
        }
        int posDeck = 0;
        int aleatorio = 0;
        //set 1 16 cartas de total de 42
        for(int i = 0; i < 16; i++)
        {
            contador = 3;
            while (contador >= 3)
            {
                contador = 0;
                aleatorio = int.Parse(deckInicial[Random.Range(1, 43)]);
                for(int j=0;j<40; j++)
                {
                    if (deckInicio[j]==aleatorio){
                        contador++;
                    }
                }
            }
            deckInicio[posDeck] = aleatorio;
            posDeck++;
        }
        // set 2  16 cartas de total de 84
        for (int i = 0; i < 16; i++)
        {
            contador = 3;
            while (contador >= 3)
            {
                contador = 0;
                aleatorio = int.Parse(deckInicial[Random.Range(43, 126)]);
                for (int j = 0; j < 40; j++)
                {
                    if (deckInicio[j] == aleatorio)
                    {
                        contador++;
                    }
                }
            }
            deckInicio[posDeck] = aleatorio;
            posDeck++;
        }
        // set 3 4 cartas de total de 97
        for (int i = 0; i < 4; i++)
        {
            contador = 3;
            while (contador >= 3)
            {
                contador = 0;
                aleatorio = int.Parse(deckInicial[Random.Range(127, 223)]);
                for (int j = 0; j < 40; j++)
                {
                    if (deckInicio[j] == aleatorio)
                    {
                        contador++;
                    }
                }
            }
            deckInicio[posDeck] = aleatorio;
            posDeck++;
        }
        // set 4 1 carta de un total de 86
        contador = 3;
        while (contador >= 3)
        {
            contador = 0;
            aleatorio = int.Parse(deckInicial[Random.Range(223, 308)]);
            for (int j = 0; j < 40; j++)
            {
                if (deckInicio[j] == aleatorio)
                {
                    contador++;
                }
            }
        }
        deckInicio[posDeck] = aleatorio;
        posDeck++;
        // set 5 una sola carta de un total de 2
        contador = 3;
        while (contador >= 3)
        {
            contador = 0;
            aleatorio = int.Parse(deckInicial[Random.Range(308, 310)]);
            for (int j = 0; j < 40; j++)
            {
                if (deckInicio[j] == aleatorio)
                {
                    contador++;
                }
            }
        }
        deckInicio[posDeck] = aleatorio;
        posDeck++;
        // set 6 una sola carta de total de 6
        contador = 3;
        while (contador >= 3)
        {
            contador = 0;
            aleatorio = int.Parse(deckInicial[Random.Range(310, 316)]);
            for (int j = 0; j < 40; j++)
            {
                if (deckInicio[j] == aleatorio)
                {
                    contador++;
                }
            }
        }
        deckInicio[posDeck] = aleatorio;
        posDeck++;
        // set 7 una sola carta de total de 17
        contador = 3;
        while (contador >= 3)
        {
            contador = 0;
            aleatorio = int.Parse(deckInicial[Random.Range(316, 334)]);
            for (int j = 0; j < 40; j++)
            {
                if (deckInicio[j] == aleatorio)
                {
                    contador++;
                }
            }
        }
        deckInicio[posDeck] = aleatorio;
        posDeck++;
        datosJuego.SetDeckUsuario(deckInicio);
        for(int i = 0; i < datosJuego.GetDeckUsuario().Count; i++)
        {
            if (!datosJuego.GetCofre().Contains(datosJuego.GetDeckUsuario()[i]))
            {
                datosJuego.AñadirElementoCofre(datosJuego.GetDeckUsuario()[i]);
                datosJuego.AñadirCantidadCofre(0);
                datosJuego.IncrementarIdNueva();
                datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());
            }
           
        }
    }
    public void SetOceano()
    {
        datosDuelo.SetIdDuelista(24);
        datosJuego.SetHistoria(23);
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        textoDialogos.text = "";
        if (datosJuego.GetMagos()[0] == 1)
        {
            datosJuego.SetEventosHistoria(113);
        }
        else
        {
            datosJuego.SetEventosHistoria(70);
        }
        
        Dialogos();
    }
    public void SetDesierto()
    {
        datosDuelo.SetIdDuelista(26);
        datosJuego.SetHistoria(23);
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        textoDialogos.text = "";
        if (datosJuego.GetMagos()[1] == 1)
        {
            datosJuego.SetEventosHistoria(113);
        }
        else
        {
            datosJuego.SetEventosHistoria(76);
        }

        Dialogos();
    }
    public void SetBosque()
    {
        datosDuelo.SetIdDuelista(28);
        datosJuego.SetHistoria(23);
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        textoDialogos.text = "";
        if (datosJuego.GetMagos()[2] == 1)
        {
            datosJuego.SetEventosHistoria(113);
        }
        else
        {
            datosJuego.SetEventosHistoria(82);
        }

        Dialogos();
    }
    public void SetMontaña()
    {
        datosDuelo.SetIdDuelista(30);
        datosJuego.SetHistoria(23);
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        textoDialogos.text = "";
        if (datosJuego.GetMagos()[3] == 1)
        {
            datosJuego.SetEventosHistoria(113);
        }
        else
        {
            datosJuego.SetEventosHistoria(88);
        }

        Dialogos();
    }
    public void SetPradera()
    {
        datosDuelo.SetIdDuelista(33);
        datosJuego.SetHistoria(23);
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        textoDialogos.text = "";
        if (datosJuego.GetMagos()[4] == 1)
        {
            datosJuego.SetEventosHistoria(113);
        }
        else
        {
            datosJuego.SetEventosHistoria(94);
        }

        Dialogos();
    }
    public void SetSagrado()
    {
       
        botonOceano.gameObject.SetActive(false);
        botonDesierto.gameObject.SetActive(false);
        botonBosque.gameObject.SetActive(false);
        botonMontana.gameObject.SetActive(false);
        botonPradera.gameObject.SetActive(false);
        botonSagrado.gameObject.SetActive(false);
        int contador = 0;
        textoDialogos.text = "";
        for(int i = 0; i < 5; i++)
        {
            if (datosJuego.GetMagos()[i] == 1)
            {
                contador++;
            }
        }
        if(contador==2 && datosJuego.desbloqueoSeto2 == false)
        {
            datosJuego.SetHistoria(31);
            datosDuelo.SetIdDuelista(32);
            datosJuego.SetEventosHistoria(101);
        }
        else if (contador==5)
        {
            datosJuego.SetHistoria(34);
            datosDuelo.SetIdDuelista(35);
            datosJuego.SetEventosHistoria(104);
        }
        else
        {
            //abandonado
            datosJuego.SetEventosHistoria(100);
        }

        Dialogos();
    }
    public int DarEstrellas()
    {
       int estrellasDadas = Random.Range(40, 300);
        datosJuego.SetEstrellas(datosJuego.GetEstrellas() + estrellasDadas);
        return estrellasDadas;
    }

    public void BotonZ()
    {
        transicionImagen.Iniciar();
        if (fase.Equals("activarDialogos"))
        {
            if (inicioDialogos < dialogos.Length - 1)
            {
                fase = "";
                fuente.Play();
                inicioDialogos++;
                textoDialogos.text = "";
                StartCoroutine(muestraLetras());

            }


        }
    }

}
