using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Libreria : MonoBehaviour
{
    public GameObject original;
    public GameObject contenedor;
    public ImportadorTextos txt;
    public int posX;
    public int posY;
    public int contador;
    public bool editar = true;
    public transicion transicion;
    public TextMeshProUGUI totalCartas;
    public TextMeshProUGUI porcentajeCartas;
    public Sonido sonido;
    public EfectosSonido EfectosSonido;
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
        sonido.MusicaLibreria();
        posX = 0;
        posY = 0;
        contador = 1;
        CrearInstanciasCartas();
        TotalYPorcentaje();
        //clon[i] = Instantiate(original, new Vector3(original.transform.position.x - 6, original.transform.position.y, original.transform.position.z), original.transform.rotation);
        //clon[i].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.GetManoUsuario(i));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && editar == true)
        {
            editar = false;
            EfectosSonido.CancelarAccion();
            transicion.CargarEscena("MenuContinuar");
        }
    }
    public void CrearInstanciasCartas()
    {
        for(int i = 0; i < 81; i++)
        {
           
            for (int j = 1; j < 10; j++)
            {
                GameObject clonCarta = Instantiate(original, new Vector3(original.transform.position.x + posX, original.transform.position.y+posY, original.transform.position.z), original.transform.rotation);
                if (datosJuego.GetCofre().Contains(contador))
                {
                    clonCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(contador);
                }
                else
                {
                    clonCarta.GetComponent<RawImage>().texture = (Texture2D)txt.cartas.GetValue(723);
                }
               
                clonCarta.transform.SetParent(contenedor.transform, false);
                posX += 75;
                contador++;
                if(contador == 723)
                {
                    break;
                }
            }
            posX = 0;
            posY -= 100;
        }
     

    }
    public void TotalYPorcentaje()
    {
        float cartas = 0;
        for(int i = 1; i < 723; i++)
        {
            if (datosJuego.GetCofre().Contains(i))
            {
                cartas++;
            }
        }
        float porcentajeDecim = 100 * (cartas / 722f);
        int porcentaje = (int)porcentajeDecim;
        if(porcentaje < 30)
        {
            porcentajeCartas.color = Color.red;
            totalCartas.color = Color.red;
        }
        else if(porcentaje < 70)
        {
            porcentajeCartas.color = Color.yellow;
            totalCartas.color = Color.yellow;
        }
        else if (porcentaje < 100)
        {
            porcentajeCartas.color = Color.green;
            totalCartas.color = Color.green;
        }
        else
        {
            porcentajeCartas.color = Color.blue;
            totalCartas.color = Color.blue;
        }
        porcentajeCartas.text = porcentaje + "%";
        totalCartas.text = cartas + "/722";
    }

    public void BotonC()
    {
        if (editar)
        {
            editar = false;
            EfectosSonido.CancelarAccion();
            transicion.CargarEscena("MenuContinuar");
        }
    }
}
