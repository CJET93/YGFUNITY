using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadroUsuario : MonoBehaviour
{
    public Interfaz interfaz;
    public Apuntador apuntador;
    public ApuntadorAtaque apuntadorAtaque;
    public ClonCarta clonCarta;
    public Material[] campoMaterial = new Material[5];
    public Material[] cuadrosMaterial = new Material[10];
    public GameObject[] materiales = new GameObject[10];
    public GameObject[] materialesCpu = new GameObject[10];
    public GameObject batallaUsuario;
    public GameObject batallaCpu;
    public bool terminarAnimacionInfinita;
    void Start()
    {
        terminarAnimacionInfinita = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivarAnimacion(int indice)
    {
        //materiales[indice].GetComponent<Renderer>().material.color = Color.gray;
        GetComponent<Animator>().Play("ClipCampo1");

    }
    public void EsperarAnimacion()
    {
       
        interfaz.ActivarComponentes();
        interfaz.DesactivarDatosUI();
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
        interfaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        interfaz.datosCartaCpu.SetActive(true);
        interfaz.datosCarta.SetActive(true);
        interfaz.Comprobante();
    }
    //para activar los componentes del usuario cuando la cpu pasa el turno

    public void EsperarAnimacionUsuario()
    {
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
        interfaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        interfaz.datosCartaCpu.SetActive(true);
        interfaz.datosCarta.SetActive(true);
        interfaz.ActivarComponentesUsuario();
        interfaz.ActivarGuardianStar(false);
        interfaz.Comprobante();
    }
    public void CampoUsuario(int indice)
    {
        //materiales[indice].GetComponent<Renderer>().material.color = Color.gray;

        GetComponent<Animator>().Play("ClipCampo2");
    }
    public void InicioJuego()
    {
   
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(1f, 1f, 1f, 0.1f);
        //materiales[indice].GetComponent<Renderer>().material.color = Color.gray;
        StartCoroutine(animacionColocarTablero());
    }
   public IEnumerator animacionColocarTablero()
    {
        while (gameObject.transform.position.y > 0)
        {
            gameObject.transform.Translate(0f * Time.deltaTime, -5f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        gameObject.transform.position = new Vector3(0.4f, 0f, -0.5f);
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("ClipCampo3");
    }

    //campo cpu
    public void ActivarAnimacionCpu(int indice)
    {
        //materiales[indice].GetComponent<Renderer>().material.color = Color.gray;
        GetComponent<Animator>().enabled = true;

    }
    public void CuadrosPosBatalla()
    {
        apuntador.gameObject.SetActive(false);
        apuntadorAtaque.gameObject.SetActive(false);
    }
    public void CambiarMaterial(int campo,int c1, int c2)
    {
        GetComponent<Renderer>().sharedMaterial = campoMaterial[campo];
        int pares = 0;
        int impares = 1;
        for(int i = 0; i < 5; i++)
        {
            materiales[pares].GetComponent<Renderer>().material = cuadrosMaterial[c1];
            materiales[impares].GetComponent<Renderer>().material = cuadrosMaterial[c2];
            materialesCpu[pares].GetComponent<Renderer>().material = cuadrosMaterial[c2];
            materialesCpu[impares].GetComponent<Renderer>().material = cuadrosMaterial[c1];
            pares += 2;
            impares += 2;
        }
    }
    public void EmpezarAnimacionInfinita()
    {
        GetComponent<Animator>().enabled = false;
        StartCoroutine(AnimacionInfinita());
        
      
    }
    IEnumerator AnimacionInfinita()
    {
       
        while (terminarAnimacionInfinita == false)
        {
           gameObject.transform.Rotate(0 * Time.deltaTime, 20f * Time.deltaTime, 0 * Time.deltaTime);
            yield return null;
        }
    }
    public void TableroBatallaUsuario(bool ataque)
    {
        if (ataque)
        {
            batallaUsuario.transform.localPosition = new Vector3(3.25f, 0.046f, -4.35f);
            clonCarta.contenedorCampoCpu.transform.localPosition = new Vector3(0f, 0f, -1.2f);
            //interfaz.espadasLuz.transform.localPosition = new Vector3(interfaz.espadasLuz.transform.localPosition.x, interfaz.espadasLuz.transform.localPosition.y, -2.52f);
        }
        else
        {
            batallaUsuario.transform.localPosition = new Vector3(3.25f, 0.046f, -3.1f);
            clonCarta.contenedorCampoCpu.transform.localPosition = new Vector3(0f, 0f, -0f);
            //interfaz.espadasLuz.transform.localPosition = new Vector3(interfaz.espadasLuz.transform.localPosition.x, interfaz.espadasLuz.transform.localPosition.y, -1.4f);
        }
    }
    public void TableroBatallaCpu(bool ataque)
    {
        if (ataque)
        {
            batallaCpu.transform.localPosition = new Vector3(3.25f, 0.046f, 2.71f);
            clonCarta.contenedorCampoUsuario.transform.localPosition = new Vector3(0f, 0f, 0.9f);
        }
        else
        {
            batallaCpu.transform.localPosition = new Vector3(3.25f, 0.046f, 1.76f);
            clonCarta.contenedorCampoUsuario.transform.localPosition = new Vector3(0f, 0f, -0f);
        }
    }



}
