using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DueloLibre : MonoBehaviour
{

   // public float arribaScroll;
    //public float abajoScroll = 1;
    public int idCuadro = 0;
    public float posX = -270;
    public float posY = 100f;
    private bool mover = true;
    //private int 
    public ScrollRect scroll;
    public RawImage[] duelistas;
    public RawImage cuadro;
    private Data txt;
    public ImportadorHistoria importadorHistoria;
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    private DatosDuelo datosDuelo;
    public TextMeshProUGUI nombreDuelista;
    public TextMeshProUGUI ganadas;
    public TextMeshProUGUI perdidas;
    public TextMeshProUGUI ganadasTexto;
    public TextMeshProUGUI perdidasTexto;
    public transicion transicion;
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    public int indicesScroll = 0;
    public float[] valoresScroll = new float[7];
    public List<string> duelistaslista=new List<string>();
    public List<int> restriccionListaDerecha = new List<int>();
    public List<int> restriccionListaIzquierda = new List<int>();
    private int[] restriccionesIdCuadroDerecha = {  4, 9,14,19,24,29,34,39 };
    private int[] restriccionesIdCuadroIzquierda = { 0, 5, 10, 15, 20, 25, 30, 35};
    // Update is called once per frame
    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
    }
    private void Start()
    {
        txt = datosJuego.GetData();
        Cursor.visible = false;
        sonido.MenuDueloLibre();
        valoresScroll[0] = 1f;
        valoresScroll[1] = 0.82873f;
        valoresScroll[2] = 0.63692f;
        valoresScroll[3] = 0.45793f;
        valoresScroll[4] = 0.27704f;
        valoresScroll[5] = 0.08975f;
        valoresScroll[6] = 0f;
      
        for(int i = 0; i < restriccionesIdCuadroDerecha.Length; i++)
        {
            restriccionListaDerecha.Add(restriccionesIdCuadroDerecha[i]);
        }
        for (int i = 0; i < restriccionesIdCuadroIzquierda.Length; i++)
        {
            restriccionListaIzquierda.Add(restriccionesIdCuadroIzquierda[i]);
        }
        for (int i = 0; i < importadorHistoria.GetDuelistas().Length; i++)
        {
            duelistaslista.Add(importadorHistoria.GetDuelistas()[i]);
        }
        for (int i=0;i< importadorHistoria.GetDuelistas().Length; i++)
        {
            if (datosJuego.GetDesbloqueables().Contains(duelistaslista[i]))
            {
                int lol = duelistaslista.IndexOf(duelistaslista[i]);
                //int lal = importadorHistoria.GetDuelistas();
                int def = int.Parse(txt.GetOrdenDuelista()[lol]);
                duelistas[def].gameObject.SetActive(true);
                duelistas[def].GetComponentInChildren<Image>().gameObject.SetActive(true);
            }
        }
        DatosCuadro();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) &&mover==true)
        {
            if (indicesScroll != 6)
            {
                indicesScroll++;
                scroll.verticalNormalizedPosition = valoresScroll[indicesScroll];
            }



        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && mover == true)
        {
            if (indicesScroll != 0)
            {
                indicesScroll--;
                scroll.verticalNormalizedPosition = valoresScroll[indicesScroll];
            }




        }
        if (Input.GetKeyDown(KeyCode.Z) && mover == true)
        {
            if (idCuadro > 1)
            {
                int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                {
                    mover = false;
                    efectosSonido.CambiarTurno();
                    datosDuelo.SetIdDuelista(pos);
                    datosDuelo.posX = posX;
                    datosDuelo.posY = posY;
                    datosDuelo.idCuadro = idCuadro;
                    datosDuelo.idScroll = indicesScroll;
                    datosDuelo.SetDuelistaCpu(duelistaslista[pos]);
                    datosDuelo.SetModoHistoria(false);
                    datosJuego.SetFase("duelo");
                    transicion.CargarEscena("CrearDeck");

                }
            }
            else
            {
                if (idCuadro == 0)
                {
                    mover = false;
                    efectosSonido.CambiarTurno();
                    datosDuelo.posX = posX;
                    datosDuelo.posY = posY;
                    datosDuelo.idCuadro = idCuadro;
                    datosDuelo.idScroll = indicesScroll;
                    datosJuego.SetFase("dueloLibre");
                    transicion.CargarEscena("CrearDeck");

                }
                else
                {
                    mover = false;
                    efectosSonido.CambiarTurno();
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    datosDuelo.SetIdDuelista(pos);
                    datosDuelo.posX = posX;
                    datosDuelo.posY = posY;
                    datosDuelo.idCuadro = idCuadro;
                    datosDuelo.idScroll = indicesScroll;
                    datosDuelo.SetDuelistaCpu(importadorHistoria.GetDuelistas()[pos]);
                    datosJuego.SetFase("duelo");
                    datosDuelo.SetModoHistoria(false);
                    transicion.CargarEscena("CrearDeck");
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && mover == true)
        {
          
            ganadasTexto.text = "Ganadas:";
            perdidasTexto.text = "Perdidas:";
            if (!restriccionListaDerecha.Contains(idCuadro)) {
                efectosSonido.moverCarta();
                idCuadro++;
                posX += 135;
                cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                if (idCuadro == 1)
                {
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                    ganadas.text = "" + datosJuego.GetVictorias()[pos];
                    perdidas.text = "" + datosJuego.GetDerrotas()[pos];

                }
                else {
                 
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                    {

                        nombreDuelista.text = duelistaslista[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }
                    else
                    {
                        nombreDuelista.text = "";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                }
            }
        }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && mover == true)
            {



                if (!restriccionListaIzquierda.Contains(idCuadro))
                {
                efectosSonido.moverCarta();
                idCuadro--;
                    posX -= 135;
                    cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                    if (idCuadro < 2)
                    {
                        if (idCuadro == 0)
                        {
                            ganadasTexto.text = "";
                            perdidasTexto.text = "";
                            nombreDuelista.text = "Armar Deck";
                            ganadas.text = "";
                            perdidas.text = "";
                        }
                        else
                        {
                            int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                        nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                            ganadas.text = "" + datosJuego.GetVictorias()[pos];
                            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                        }

                    }
                    else
                    {
                        ganadasTexto.text = "Ganadas:";
                        perdidasTexto.text = "Perdidas:";
                        int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                        {

                            nombreDuelista.text = duelistaslista[pos];
                            ganadas.text = "" + datosJuego.GetVictorias()[pos];
                            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                        }
                        else
                        {
                            nombreDuelista.text = "";
                            ganadas.text = "";
                            perdidas.text = "";
                        }
                    }

                }




            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && mover == true)
            {
                if (idCuadro < 35)
                {
                efectosSonido.moverCarta();
                ganadasTexto.text = "Ganadas:";
                perdidasTexto.text = "Perdidas:";
                idCuadro += 5;
                    posY -= 125;
                    cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);

                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                    {

                        nombreDuelista.text = duelistaslista[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }
                    else
                    {
                        nombreDuelista.text = "";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                }


            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && mover == true)
            {
                if (idCuadro > 4)
                {
                efectosSonido.moverCarta();
                idCuadro -= 5;
                    posY += 125;
                    cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                    if (idCuadro < 2)
                    {
                        if (idCuadro == 0)
                        {
                            ganadasTexto.text = "";
                            perdidasTexto.text = "";
                            nombreDuelista.text = "Armar Deck";
                            ganadas.text = "";
                            perdidas.text = "";
                        }
                        else
                        {
                            int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                            nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                            ganadas.text = "" + datosJuego.GetVictorias()[pos];
                            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                        }

                    }
                    else
                    {
                    ganadasTexto.text = "Ganadas:";
                    perdidasTexto.text = "Perdidas:";
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                        if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                        {

                            nombreDuelista.text = duelistaslista[pos];
                            ganadas.text = "" + datosJuego.GetVictorias()[pos];
                            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                        }
                        else
                        {
                            nombreDuelista.text = "";
                            ganadas.text = "";
                            perdidas.text = "";
                        }
                    }
                }



            }
        if (Input.GetKeyDown(KeyCode.C) && mover == true)
        {
            mover = false;
            efectosSonido.CancelarAccion();
            datosDuelo.posX = -270;
            datosDuelo.posY = 100;
            datosDuelo.idCuadro = 0;
            datosDuelo.idScroll = 0;
            transicion.CargarEscena("MenuCOntinuar");
}
        }
    public void DatosCuadro()
    {
        posX = datosDuelo.posX;
        posY = datosDuelo.posY;
        idCuadro = datosDuelo.idCuadro;
        indicesScroll = datosDuelo.idScroll;
        scroll.verticalNormalizedPosition = valoresScroll[indicesScroll];
        cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
        if (idCuadro == 0)
        {
            ganadasTexto.text = "";
            perdidasTexto.text = "";
            nombreDuelista.text = "Armar Deck";
            ganadas.text = "";
            perdidas.text = "";
        }
        else if (idCuadro == 1)
        {
            int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
            nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
            ganadas.text = "" + datosJuego.GetVictorias()[pos];
            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
        }
        else
        {
            int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
            nombreDuelista.text = duelistaslista[pos];
            ganadas.text = "" + datosJuego.GetVictorias()[pos];
            perdidas.text = "" + datosJuego.GetDerrotas()[pos];
        }
    }
    public void BotonAbajo()
    {
        if (mover == true)
        {
            if (indicesScroll != 6)
            {
                indicesScroll++;
                scroll.verticalNormalizedPosition = valoresScroll[indicesScroll];
            }
            if (idCuadro < 35)
            {
                efectosSonido.moverCarta();
                ganadasTexto.text = "Ganadas:";
                perdidasTexto.text = "Perdidas:";
                idCuadro += 5;
                posY -= 125;
                cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);

                int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                {

                    nombreDuelista.text = duelistaslista[pos];
                    ganadas.text = "" + datosJuego.GetVictorias()[pos];
                    perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                }
                else
                {
                    nombreDuelista.text = "";
                    ganadas.text = "";
                    perdidas.text = "";
                }
            }

        }
    }

    public void BotonDerecha()
    {
        if (mover == true)
        {
            ganadasTexto.text = "Ganadas:";
            perdidasTexto.text = "Perdidas:";
            if (!restriccionListaDerecha.Contains(idCuadro))
            {
                efectosSonido.moverCarta();
                idCuadro++;
                posX += 135;
                cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                if (idCuadro == 1)
                {
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                    ganadas.text = "" + datosJuego.GetVictorias()[pos];
                    perdidas.text = "" + datosJuego.GetDerrotas()[pos];

                }
                else
                {

                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                    {

                        nombreDuelista.text = duelistaslista[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }
                    else
                    {
                        nombreDuelista.text = "";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                }
            }
        }
    }
    public void BotonIniciarDuelo()
    {
        if (idCuadro > 1)
        {
            int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
            if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
            {
                mover = false;
                efectosSonido.CambiarTurno();
                datosDuelo.SetIdDuelista(pos);
                datosDuelo.posX = posX;
                datosDuelo.posY = posY;
                datosDuelo.idCuadro = idCuadro;
                datosDuelo.idScroll = indicesScroll;
                datosDuelo.SetDuelistaCpu(duelistaslista[pos]);
                datosDuelo.SetModoHistoria(false);
                datosJuego.SetFase("duelo");
                transicion.CargarEscena("CrearDeck");

            }
        }
        else
        {
            if (idCuadro == 0)
            {
                mover = false;
                efectosSonido.CambiarTurno();
                datosDuelo.posX = posX;
                datosDuelo.posY = posY;
                datosDuelo.idCuadro = idCuadro;
                datosDuelo.idScroll = indicesScroll;
                datosJuego.SetFase("dueloLibre");
                transicion.CargarEscena("CrearDeck");

            }
            else
            {
                mover = false;
                efectosSonido.CambiarTurno();
                int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                datosDuelo.SetIdDuelista(pos);
                datosDuelo.posX = posX;
                datosDuelo.posY = posY;
                datosDuelo.idCuadro = idCuadro;
                datosDuelo.idScroll = indicesScroll;
                datosDuelo.SetDuelistaCpu(importadorHistoria.GetDuelistas()[pos]);
                datosJuego.SetFase("duelo");
                datosDuelo.SetModoHistoria(false);
                transicion.CargarEscena("CrearDeck");
            }
        }
    }

    public void BotonArriba()
    {
        if (mover)
        {
            if (indicesScroll != 0)
            {
                indicesScroll--;
                scroll.verticalNormalizedPosition = valoresScroll[indicesScroll];
            }

            if (idCuadro > 4)
            {
                efectosSonido.moverCarta();
                idCuadro -= 5;
                posY += 125;
                cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                if (idCuadro < 2)
                {
                    if (idCuadro == 0)
                    {
                        ganadasTexto.text = "";
                        perdidasTexto.text = "";
                        nombreDuelista.text = "Armar Deck";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                    else
                    {
                        int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                        nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }

                }
                else
                {
                    ganadasTexto.text = "Ganadas:";
                    perdidasTexto.text = "Perdidas:";
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                    {

                        nombreDuelista.text = duelistaslista[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }
                    else
                    {
                        nombreDuelista.text = "";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                }
            }
        }

    }

    public void BotonIzquierda()
    {
        if (mover)
        {
            if (!restriccionListaIzquierda.Contains(idCuadro))
            {
                efectosSonido.moverCarta();
                idCuadro--;
                posX -= 135;
                cuadro.GetComponent<RectTransform>().localPosition = new Vector3(posX, posY, 0f);
                if (idCuadro < 2)
                {
                    if (idCuadro == 0)
                    {
                        ganadasTexto.text = "";
                        perdidasTexto.text = "";
                        nombreDuelista.text = "Armar Deck";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                    else
                    {
                        int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                        nombreDuelista.text = importadorHistoria.GetDuelistas()[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }

                }
                else
                {
                    ganadasTexto.text = "Ganadas:";
                    perdidasTexto.text = "Perdidas:";
                    int pos = int.Parse(txt.GetIdDuelista()[idCuadro]);
                    if (datosJuego.GetDesbloqueables().Contains(duelistaslista[pos]))
                    {

                        nombreDuelista.text = duelistaslista[pos];
                        ganadas.text = "" + datosJuego.GetVictorias()[pos];
                        perdidas.text = "" + datosJuego.GetDerrotas()[pos];
                    }
                    else
                    {
                        nombreDuelista.text = "";
                        ganadas.text = "";
                        perdidas.text = "";
                    }
                }

            }


       }
    }

    public void BotonCancelar()
    {
        if (mover)
        {
            mover = false;
            efectosSonido.CancelarAccion();
            datosDuelo.posX = -270;
            datosDuelo.posY = 100;
            datosDuelo.idCuadro = 0;
            datosDuelo.idScroll = 0;
            transicion.CargarEscena("MenuCOntinuar");
        }
    }

}
