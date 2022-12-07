using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Claves : MonoBehaviour
{
    public int idCarta;
    public int costo;
    public Sonido sonido;
    public EfectosSonido efectosSonido;
    public TextMeshProUGUI estrellasTexto;
    public TextMeshProUGUI numeroCarta;
    public TextMeshProUGUI costoCarta;
    public TextMeshProUGUI claveTexto;
    public ImportadorTextos txt;
    public string clave;
    public GameObject imagenCarta;
    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    private DatosDuelo datosDuelo;
    public GameObject botonAceptar;
    public GameObject botonSalir;
    public GameObject botonCancelar;
    public GameObject botonCambiar;
    public transicion transicion;
    private void Awake()
    {
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        objetoDatosDuelo = GameObject.Find("DatosDuelo");
        datosDuelo = objetoDatosDuelo.GetComponent<DatosDuelo>();
    }
    void Start()
    {
        sonido.MusicaClaves();
        estrellasTexto.text = "X" + datosJuego.GetEstrellas();
    }
    public void obtenerCarta()
    {
        clave = claveTexto.text.Trim((char)8203);
        bool salir = false;
        bool valida = true;
        int indice = 0;
        if (clave.Equals("0") || clave.Equals("null"))
        {
            valida = false;
        }
        for(int i = 0; i < txt.GetClaves().Length && salir==false; i++)
        {
            if (clave.Equals(txt.GetClaves().GetValue(i).ToString().Trim()) )
            {
                indice = i;
                salir = true;
            }
        }
        if (salir == true && valida==true)
        {
            efectosSonido.SeleccionarCarta();
            botonAceptar.SetActive(false);
            botonSalir.SetActive(false);
            botonCambiar.SetActive(false);
            botonCancelar.SetActive(false);
            idCarta = indice;
            AnimacionEntraCarta();
        }
        else
        {
            efectosSonido.NoFusion();
        }
       
    }
    public void AnimacionEntraCarta()
    {
        StartCoroutine(EmpezarAnimacionEntraCarta());
        //imagenCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(idCarta);
    }
    IEnumerator EmpezarAnimacionEntraCarta()
    {
        bool realizada = false;
        bool animacion1 = false;

        while (!realizada)
        {


            imagenCarta.GetComponent<RawImage>().transform.Rotate(0 * Time.fixedDeltaTime, 2400 * Time.fixedDeltaTime, 0 * Time.fixedDeltaTime);


            if (imagenCarta.GetComponent<RawImage>().transform.rotation.eulerAngles.y > 90 && animacion1 == false)
            {
                imagenCarta.GetComponent<RawImage>().transform.eulerAngles = new Vector3(0, -90, 0);

                ///clon.GetClonCpu(j).GetComponent<Transform>().eulerAngles = new Vector3(-200, 360, 180);
                imagenCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(idCarta);
                //cartaCpu.GetComponent<Transform>().eulerAngles = new Vector3(0, -240, 180);
                //cartaCpu.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)cartaCpu.txt.cartas.GetValue(campo.GetCpuPos());
                animacion1 = true;


            }
            if (imagenCarta.GetComponent<RawImage>().transform.rotation.eulerAngles.y < 180 && animacion1 == true)
            {
                imagenCarta.GetComponent<RawImage>().transform.rotation = (Quaternion.Euler(0, 0, 0));
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
            if (idCarta.ToString().Length == 1)
            {
                numeroCarta.text = "Número de carta " +"00"+ idCarta;
            }
            else if (idCarta.ToString().Length == 2)
            {
                numeroCarta.text = "Número de carta " + "0" + idCarta;
            }
            else
            {
                numeroCarta.text = "Número de carta " + idCarta;
            }
        costo = int.Parse(txt.GetCosto().GetValue(idCarta).ToString());
        if (datosJuego.GetEstrellas()>= costo)
        {
            costoCarta.color = Color.yellow;
        }
        else
        {
            costoCarta.color = Color.red;
        }
        if (datosJuego.GetCartasTienda().Contains(idCarta))
        {
            costoCarta.color = Color.red;
            costoCarta.text = "Carta canjeada.";
        }
        else
        {
            costoCarta.text = "Costo x" + txt.GetCosto().GetValue(idCarta).ToString();
        }
       
        botonCambiar.SetActive(true);
        botonCancelar.SetActive(true);

    }
    public void ComprarCarta()
    {
        if (datosJuego.GetEstrellas() >= costo)
        {
            if (!datosJuego.GetCartasTienda().Contains(idCarta))
            {
                efectosSonido.SeleccionarCarta();
                
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
                    datosJuego.AñadirCantidadCofre(cantidadActualCofre+1);
                    datosJuego.IncrementarIdNueva();
                    datosJuego.AñadirElementoNuevo(datosJuego.GetIdNueva());

                }
                botonAceptar.SetActive(false);
                botonSalir.SetActive(false);
                botonCambiar.SetActive(false);
                botonCancelar.SetActive(false);
                StartCoroutine(EmpezarDisminuirEstrellas());
                
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
    IEnumerator EmpezarDisminuirEstrellas()
    {
        int constante = 1;
        if (costo < 100)
        {
            constante = 2;

        }
        else if (costo >= 100 && costo < 500)
        {
            constante = 5;
        }
        else if(costo>=500 && costo <= 1000)
        {
            constante = 10;
        }
        else if(costo>1000 && costo<= 5000)
        {
            constante = 50;
        }
        else if (costo > 5000 && costo <=10000)
        {
            constante = 100;
        }
        else
        {
                constante = 600;

        }
        while (costo > 0 )
        {
            if (costo - constante < 0)
            {
                constante = costo;
            }
            datosJuego.SetEstrellas(datosJuego.GetEstrellas() - constante);
            estrellasTexto.text = "X" + datosJuego.GetEstrellas();
            costo = costo - constante;
            yield return null;
        }
        StartCoroutine(EmpezarAnimacionSaleCarta());
    }
    IEnumerator EmpezarAnimacionSaleCarta()
    {
        bool realizada = false;
        bool animacion1 = false;

        while (!realizada)
        {


            imagenCarta.GetComponent<RawImage>().transform.Rotate(0 * Time.fixedDeltaTime, 2400 * Time.fixedDeltaTime, 0 * Time.fixedDeltaTime);


            if (imagenCarta.GetComponent<RawImage>().transform.rotation.eulerAngles.y > 90 && animacion1 == false)
            {
                imagenCarta.GetComponent<RawImage>().transform.eulerAngles = new Vector3(0, -90, 0);
                imagenCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(723);
                animacion1 = true;


            }
            if (imagenCarta.GetComponent<RawImage>().transform.rotation.eulerAngles.y < 180 && animacion1 == true)
            {
                imagenCarta.GetComponent<RawImage>().transform.rotation = (Quaternion.Euler(0, 0, 0));
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
        claveTexto.text = "";
        numeroCarta.text = "";
        costoCarta.text = "";
        botonAceptar.SetActive(true);
        botonSalir.SetActive(true);


    }
    public void CancelarCompra()
    {
        efectosSonido.CancelarAccion();
        botonAceptar.SetActive(false);
        botonSalir.SetActive(false);
        botonCambiar.SetActive(false);
        botonCancelar.SetActive(false);
        StartCoroutine(EmpezarAnimacionSaleCarta());
        
    }
    public void Salir()
    {
        efectosSonido.CancelarAccion();
        transicion.CargarEscena("MenuContinuar");
    }


}
