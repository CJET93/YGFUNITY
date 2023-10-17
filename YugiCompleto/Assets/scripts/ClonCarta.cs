using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClonCarta : MonoBehaviour
{
    public GameObject original;
    public GameObject originalCampo;
    public GameObject contenedor;
    public GameObject contenedorCpu;
    public GameObject contenedorCampoUsuario;
    public GameObject contenedorCampoCpu;
    public CuadroUsuario cuadroUsuario;
    public ImportadorTextos txt;
    public Campo campo;
    public carta carta;
    public static int Constane = 9999;
    public Text texto;
    public Juego juego;
    public GameObject[] clon = new GameObject[5];
    public GameObject[] clonCpu = new GameObject[5];
    public GameObject[] campoU = new GameObject[10];
    public GameObject[] campoCpu = new GameObject[10];
    private Vector3 vec;
    private int pos;
    public Interfaz intefaz;
    public Controles controles;
    public Camara camara;
    private int descarta;
    private int posCampoCpuMT;
    private int indiceCartaMT;
    public List<int> validadorFusionCampoCpu = new List<int>();
    public Vector3 atkBatallPos;
    public Vector3 defBatallPos;
    private int imgCarta = 0;
    private int color = 0;
    private string txtMT = "";
    private int indiceMT;
    private bool primerMT;
    private int reduccion;
    private const string atkText = "";
    private const string defText = "";



    // Start is called before the first frame update

    void Start()
    {
        atkBatallPos = new Vector3(0f, 0f, 0f);
    }



    public void CreasInstancias(int i)
    {
        reduccion = 0;
        clon[i] = Instantiate(original, new Vector3(original.transform.position.x + 800, original.transform.position.y, original.transform.position.z), original.transform.rotation);
        clon[i].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoUsuario(i));
        clon[i].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetManoUsuario(i));
        clon[i].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetManoUsuario(i));
        string tipoCarta = txt.GetTipoCarta().GetValue(campo.GetManoUsuario(i)).ToString().Trim();
        int stars = int.Parse((string)txt.GetStars().GetValue(campo.GetManoUsuario(i)));
        string attribute = txt.GetAttributes().GetValue(campo.GetManoUsuario(i)).ToString();
        string nombre = (string)txt.getnom().GetValue(campo.GetManoUsuario(i));
        clon[i].GetComponent<carta>().SetName(nombre);
        clon[i].GetComponent<carta>().SetTipoCarta(tipoCarta);
        clon[i].GetComponent<carta>().SetStarsNumber(stars);
        clon[i].GetComponent<carta>().SetAttribute(attribute);
        clon[i].GetComponent<muestraCarta>().nombreCarta.text = nombre;

        if (tipoCarta.Equals("Monstruo"))
        {
            int ataqueconvertidor = int.Parse((string)txt.getatk().GetValue(campo.GetManoUsuario(i)));
            int defConvertidor = int.Parse((string)txt.getdef().GetValue(campo.GetManoUsuario(i)));
            int atributo1 = int.Parse((string)txt.GetAtributos1().GetValue(campo.GetManoUsuario(i)));
            int atributo2 = int.Parse((string)txt.GetAtributos2().GetValue(campo.GetManoUsuario(i)));
            int tipoAtributo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(campo.GetManoUsuario(i)));
            clon[i].GetComponent<carta>().SetAtaque(ataqueconvertidor);
            clon[i].GetComponent<carta>().SetDefensa(defConvertidor);
            clon[i].GetComponent<carta>().SetGuardianStar(atributo1);
            clon[i].GetComponent<carta>().SetGuardianStar2(atributo2);
            clon[i].GetComponent<carta>().SetTipoAtributo(tipoAtributo);
            clon[i].GetComponent<muestraCarta>().ataque.text = ataqueconvertidor.ToString();
            clon[i].GetComponent<muestraCarta>().defensa.text = defConvertidor.ToString();
        }
        else
        {

            clon[i].GetComponent<muestraCarta>().contenedorNormal.SetActive(false);
            clon[i].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
            clon[i].GetComponent<muestraCarta>().textoMT.text = tipoCarta.ToUpper();
            if (tipoCarta.Equals("Trampa"))
            {
                clon[i].GetComponent<muestraCarta>().panelDatos.texture = clon[i].GetComponent<muestraCarta>().color[2];
            }
            else
            {
                clon[i].GetComponent<muestraCarta>().panelDatos.texture = clon[i].GetComponent<muestraCarta>().color[1];
            }




            clon[i].GetComponent<carta>().SetGuardianStarA(0);

        }
        clon[i].transform.SetParent(contenedor.transform, false);
    }
    public void InactivateComponent(GameObject[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null)
            {
                cards[i].gameObject.SetActive(false);
            }
        }
    }

    public void InactivateComponent(GameObject[] cards, int id)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i != id && cards[i] != null)
            {
                cards[i].gameObject.SetActive(false);
            }
        }
    }

    public void AnimacionInstancias(int cont)
    {
        StartCoroutine(TiempoInstancias(cont));
    }
    IEnumerator TiempoInstancias(int i)

    {


        bool realizada = false;
        float pos = -2f;
        float espaciado = 160.5f;
        juego.EfectosCartasCampo(juego.GetCampoModificado());
        for (int c = 0; c < 5; c++)
        {
            if (clon[c] != null)
            {

                clon[c].GetComponent<muestraCarta>().ataque.text = clon[c].GetComponent<carta>().getAtaque().ToString();
                clon[c].GetComponent<muestraCarta>().defensa.text = clon[c].GetComponent<carta>().getDefensa().ToString();
            }
        }
        for (i = 5 - i; i < 5; i++)
        {
            if (clon[i] != null)
            {
                clon[i].GetComponent<muestraCarta>().ataque.text = clon[i].GetComponent<carta>().getAtaque().ToString();
                clon[i].GetComponent<muestraCarta>().defensa.text = clon[i].GetComponent<carta>().getDefensa().ToString();
            }


            realizada = false;
            while (!realizada)
            {
                float posicionar = pos * 3000 * Time.fixedDeltaTime;
                if (clon[i] != null)
                {
                    clon[i].transform.Translate(posicionar, 0f, 0f);
                    if (clon[i].transform.localPosition.x < -303 + (i * espaciado))
                    {

                        juego.SetCantDeckUsuario();
                        clon[i].transform.localPosition = new Vector3(-303 + (i * espaciado), 77.95f, 0f);
                        realizada = true;


                    }
                }
                else
                {
                    realizada = true;
                }


                yield return new WaitForSeconds(0.005f);

            }
            juego.ReproducirRobar();

        }
        if (campo.SinCartasUsuario() == true)
        {
            juego.FinJuegoPorCartas();
        }
        else if (campo.ExodiaUsuario() == true)
        {

            juego.FinJuegoPorExodia();
        }
        else
        {
            intefaz.SetEstado(true);
            intefaz.SetEstadoFlecha(true);
            controles.SetFase("mano");
            if (juego.GetEspadasLuzReveladora().Contains("cpu"))
            {
                if (juego.GetEspadasLuzReveladora().Equals(""))
                {
                    intefaz.SetEstadoEspadas(false, "");
                }
                else
                {
                    string numero = "";
                    if (juego.GetEspadasLuzReveladora().Contains("3"))
                    {
                        numero = "3";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else if (juego.GetEspadasLuzReveladora().Contains("2"))
                    {
                        numero = "2";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else if (juego.GetEspadasLuzReveladora().Contains("1"))
                    {
                        numero = "1";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else
                    {
                        juego.SetEspadasLuzReveladora("");
                        intefaz.SetEstadoEspadas(false, numero);
                    }

                }
            }


            intefaz.ActualizarUi(0);
            if (juego.GetCantTurnos() == 0)
            {
                juego.IniciarMusica();
            }

        }


    }

    public List<int> GetValidadorFusionCpu()
    {
        return validadorFusionCampoCpu;
    }
    public GameObject getClon(int indice)
    {
        return clon[indice];
    }
    public string guardian1Seleccionar()
    {
        string guardian = (string)txt.GetNomAtributo().GetValue(clon[pos].GetComponent<carta>().GetGuardianStar());
        return guardian;
    }
    public string guardian2Seleccionar()
    {
        string guardian = (string)txt.GetNomAtributo().GetValue(clon[pos].GetComponent<carta>().GetGuardianStar2());
        return guardian;
    }
    public int guardian1SeleccionarImagen()
    {
        return clon[pos].GetComponent<carta>().GetGuardianStar();
    }
    public int guardian2SeleccionarImagen()
    {
        return clon[pos].GetComponent<carta>().GetGuardianStar2();
    }
    public GameObject getCartaCampoU(int indice)
    {
        return campoU[indice];
    }
    public void SetTransformacion(int indice)
    {
        vec = clon[indice].transform.position;
        StartCoroutine(MoverArribaUsuario(indice));
    }
    IEnumerator MoverArribaUsuario(int indice)
    {
        Vector3 final = new Vector3(18f, 219f, 0f);
        while (Vector3.Distance(clon[indice].transform.localPosition, final) > Time.deltaTime * 1800)
        {
            clon[indice].transform.localPosition = Vector3.MoveTowards(clon[indice].transform.localPosition, final, Time.deltaTime * 1500);
            yield return null;
        }


        clon[indice].transform.localPosition = final;
        //clon[indice].transform.localPosition = new Vector3(0.7f, 2f, 5f);
        StartCoroutine(Animacion1(indice));

    }
    IEnumerator Animacion1(int indice)
    {
        if (clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo") || clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
        {
            int tiempo = 0;
            for (int i = 0; i < 180; i += 30)
            {
                yield return new WaitForSeconds(0.01f);
                clon[indice].transform.Rotate(0f, 30f, 0f);
                tiempo += 30;
                if (tiempo == 90 || tiempo == -90)
                {

                    clon[indice].GetComponent<muestraCarta>().contenedorReverso.gameObject.SetActive(true);


                }
            }
            tiempo = 0;
        }
        else
        {
            clon[indice].GetComponent<carta>().SetDatosCarta(1);
        }

        controles.SetFase("posicionMano");

    }
    public void SetPos(int posMano)
    {
        pos = posMano;
    }
    public void MostrarCarta(int indice)
    {
        StartCoroutine(AnimacionMostrarCarta(indice));


    }
    IEnumerator AnimacionMostrarCarta(int indice)
    {
        int tiempo = 0;
        for (int i = 0; i < 180; i += 30)
        {
            yield return new WaitForSeconds(0.01f);
            clon[indice].transform.Rotate(0f, 30f, 0f);
            tiempo += 30;
            if (tiempo == 90 || tiempo == -90)
            {

                if (clon[indice].GetComponent<carta>().GetDatosCarta() == 0)
                {
                    clon[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                    clon[indice].GetComponent<carta>().SetDatosCarta(1);
                }
                else
                {
                    //clon[indice].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(686);
                    clon[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
                    clon[indice].GetComponent<carta>().SetDatosCarta(0);
                }
            }
        }
        tiempo = 0;
        controles.SetFase("posicionMano");
    }
    public void Transformacion2(int indice)
    {
        //clon[indice].transform.localPosition = new Vector3(posX, posY, posZ);

        StopAllCoroutines();
        StartCoroutine(AnimacionTransformacion2(indice));

    }
    IEnumerator AnimacionTransformacion2(int indice)
    {
        Vector3 final = vec;
        while (Vector3.Distance(clon[indice].transform.position, final) > Time.deltaTime * 1500)
        {
            clon[indice].transform.position = Vector3.MoveTowards(clon[indice].transform.position, final, Time.deltaTime * 1500);
            yield return null;
        }
        clon[indice].transform.position = final;
        pos = indice;
        yield return new WaitForSeconds(0.1f);

        if (clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            camara.MoverCamara(false);
        }

        else
        {
            if (clon[indice].GetComponent<carta>().GetDatosCarta() == 1)
            {
                camara.MoverCamara(false);
            }
            else
            {
                camara.MoverCamara(true);
            }

        }

        yield return AnimacionMoverCamara(true, false);
        // para actualizar el UI
        int posCarta = indice;
        if (!clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            posCarta = indice + 5;
            for (int i = 5; i < 10; i++)
            {
                if (campoU[i] == null)
                {
                    posCarta = i;
                }
            }
            if (clon[indice].GetComponent<carta>().GetDatosCarta() == 1)
            {
                posCarta = 0;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (campoU[i] == null)
                {
                    posCarta = i;
                }
            }
        }

        intefaz.ActualizarUIUsuario(posCarta);
        controles.SetFase("ubicarCarta");
        intefaz.SetEstadoApuntador(true);
    }
    IEnumerator AnimacionDevolverCamara(bool turnoUsuario, bool fusion)
    {
        intefaz.SetEstadoApuntador(false);
        while (intefaz.datosCartaCpu.transform.position.y > -220)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        intefaz.DesactivarDatosUI();
        while (intefaz.datosCarta.transform.position.y > -220)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            if (turnoUsuario)
            {
                if (fusion)
                {
                    List<int> temp = controles.GetListaDCartas();
                    for (int i = 0; i < 5; i++)
                    {
                        if (!temp.Contains(i))
                            clon[i].transform.Translate(0f, posicionar, 0f);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i != pos)
                            clon[i].transform.Translate(0f, posicionar, 0f);
                    }
                }
            }
            else
            {
                if (fusion)
                {
                    List<int> temp = controles.GetListaDCartas();
                    for (int i = 0; i < 5; i++)
                    {
                        if (!temp.Contains(i))
                            clonCpu[i].transform.Translate(0f, posicionar, 0f);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i != pos)
                            clonCpu[i].transform.Translate(0f, posicionar, 0f);
                    }
                }
            }


            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        intefaz.datosCarta.SetActive(false);
        if (turnoUsuario)
        {
            if (!fusion)
            {
                InactivateComponent(clon,pos);
            }
        }
    }
    IEnumerator AnimacionMoverCamara(bool turnoUsuario, bool faseAtaque)
    {
        intefaz.DesactivarDatosUICampo();
        if (faseAtaque == true)
        {
            intefaz.datosCarta.SetActive(true);
            while (intefaz.datosCarta.transform.position.y < 44.1f)
            {
                float posicionar = 600 * Time.deltaTime;

                intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

                yield return null;
            }
            intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        }

        if (faseAtaque == false)
        {
            while (intefaz.datosCartaCpu.transform.localPosition.y < -60f)
            {
                float posicionar = 600 * Time.deltaTime;

                intefaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

                yield return null;
            }
            intefaz.datosCartaCpu.transform.localPosition = new Vector2(0, -47f);
        }
        yield return new WaitForSeconds(0.4f);
        intefaz.SetEstadoApuntador(true);
    }
    public void UbicarFusion(int indice)
    {
        StartCoroutine(AnimacionUbicarFusion(indice));
    }
    IEnumerator AnimacionUbicarFusion(int indice)
    {
        camara.MoverCamara(false);
        yield return AnimacionMoverCamara(true, false);
        intefaz.ActualizarUIUsuario(indice);
        controles.SetFase("ubicarCarta");
        intefaz.SetEstadoApuntador(true);

    }
    public void CancelarCarta(int indice)

    {
        StartCoroutine(AnimacionCancelarCarta(indice));
        //clon[indice].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.GetManoUsuario(indice));


    }
    IEnumerator AnimacionCancelarCarta(int indice)
    {

        if (clon[indice].GetComponent<carta>().GetDatosCarta() == 0)
        {
            int tiempo = 0;
            for (int i = 0; i < 180; i += 30)
            {
                yield return new WaitForSeconds(0.01f);
                clon[indice].transform.Rotate(0f, 30f, 0f);
                tiempo += 30;
                if (tiempo == 90 || tiempo == -90)
                {
                    clon[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                    clon[indice].GetComponent<carta>().SetDatosCarta(1);
                }
            }
            tiempo = 0;
        }
        StartCoroutine(MoverCancelacion(indice));


    }
    IEnumerator MoverCancelacion(int indice)
    {
        Vector3 final = vec;
        while (Vector3.Distance(clon[indice].transform.position, final) > Time.deltaTime * 1800)
        {
            clon[indice].transform.position = Vector3.MoveTowards(clon[indice].transform.position, final, Time.deltaTime * 1800);
            yield return null;
        }
        clon[indice].transform.position = final;
        clon[indice].GetComponent<carta>().SetDatosCarta(0);
        for (int i = 0; i < 5; i++)
        {
            if (i != indice)
            {
                clon[i].GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                clon[i].GetComponent<muestraCarta>().textoMT.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        clon[indice].transform.position = final;
        controles.SetFase("mano");
        intefaz.SetEstadoFlecha(true);
    }
    public void ColocarGuardian(int indice)
    {



        if (campoU[indice] == null && controles.GetListaDCartas().Count < 1)
        {

            if (!clon[pos].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
            {

                StartCoroutine(AnimacionColocarGuardian(indice));
            }
            else
            {
                intefaz.SetEstadoFlecha(false);
                StartCoroutine(AnimacionDestruirEquipoMano(indice));


            }


        }
        else
        {

            intefaz.SetEstadoApuntador(false);
            intefaz.SetEstadoFlecha(false);
            StartCoroutine(AnimacionColocarCartaDescarte(indice));
        }

    }
    IEnumerator AnimacionDestruirEquipoMano(int indice)
    {
        intefaz.DesactivarDatosUI();
        intefaz.SetEstadoApuntador(false);
        camara.DevolverCamara(false);
        yield return AnimacionDevolverCamara(true, false);
        intefaz.ReiniciarApuntador();
        intefaz.MoverApuntador(indice, false);
        juego.ReproducirActivacion();

        clon[pos].transform.localScale = new Vector3(2.5f, 2.5f, 0f);
        float reductor = 3f;
        while (reductor > 0)
        {
            clon[pos].transform.localScale = new Vector3(2.5f, reductor, 0f);
            reductor -= 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(clon[pos]);
        clon[pos] = null;
        camara.MoverCamara(false);
        yield return AnimacionMoverCamara(true, true);
        controles.SetFase("acabarTurno");
    }
    IEnumerator AnimacionDestruirEquipo(int indice)
    {
        camara.DevolverCamara(true);
        yield return AnimacionDevolverCamara(true, false);
        yield return new WaitForSeconds(0.2f);
        intefaz.ReiniciarApuntador();
        Vector3[] destino = new Vector3[2];
        destino[0] = new Vector3(3.2021f, 1.51f, 4.89f);
        destino[1] = new Vector3(173f, 208.9f, 0f);
        int realizada = 0;


        campoU[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        campoU[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);


        clon[pos].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);

        clon[pos].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        campoU[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoU[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        while (realizada < 2)
        {


            realizada = 0;
            campoU[indice].transform.localPosition = Vector3.MoveTowards(campoU[indice].transform.localPosition, destino[0], Time.deltaTime * 1900);
            clon[pos].transform.localPosition = Vector3.MoveTowards(clon[pos].transform.localPosition, destino[1], Time.deltaTime * 1900);


            if (Vector3.Distance(campoU[indice].transform.localPosition, destino[0]) < 1)
            {
                realizada++;
            }
            if (Vector3.Distance(clon[pos].transform.localPosition, destino[1]) < 1)
            {
                realizada++;
            }



            yield return null;
        }
        campoU[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        while (clon[pos].transform.localPosition.x >= -303f)
        {

            clon[pos].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        while (campoU[indice].transform.localPosition.x <= 6)
        {
            campoU[indice].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }

        Object.Destroy(campoU[indice]);
        campoU[indice] = null;

        clon[pos].transform.localScale = new Vector3(3f, 3f, 0.1f);
        float reductor = 3f;
        while (reductor > 0)
        {
            clon[pos].transform.localScale = new Vector3(3f, reductor, 0.1f);
            reductor -= 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(clon[pos]);
        clon[pos] = null;
        campo.SetManoUsuario(pos, 0);
        camara.MoverCamara(false);
        intefaz.MoverApuntador(indice, true);
        yield return AnimacionMoverCamara(true, true);
        controles.SetFase("acabarTurno");
        //intefaz.ActualizarUi(indice);

    }

    public void CambiarCartaEquipo(int indice, int cambio, bool conCampo)
    {
        // indice es la carta MT y cambio el monstruo
        txtMT = clon[indice].GetComponent<carta>().GetTipoCarta().ToUpper();
        int cambiar = -1;
        if (conCampo)
        {
            cambiar = campo.GetCampoUsuario(cambio);
        }
        else
        {
            cambiar = campo.GetManoUsuario(cambio);
        }
        clon[indice].GetComponent<muestraCarta>().contenedorNormal.SetActive(true);
        clon[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(false);
        clon[indice].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(cambiar);
        clon[indice].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(cambiar);
        clon[indice].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(cambiar);
        clon[indice].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(cambiar);
        string attribute = txt.GetAttributes().GetValue(cambiar).ToString();
        int stars = int.Parse((string)txt.GetStars().GetValue(cambiar));
        //string nombre = (string)txt.getnom().GetValue(campo.GetManoUsuario(i));
        clon[indice].GetComponent<carta>().SetStarsNumber(stars);
        clon[indice].GetComponent<carta>().SetAttribute(attribute);
        GetStars(clon[indice]);
        GetAttribute(clon[indice]);
        int ataqueconvertidor = 0;
        int defConvertidor = 0;
        if (conCampo)
        {
            ataqueconvertidor = campoU[cambio].GetComponent<carta>().getAtaque();
            defConvertidor = campoU[cambio].GetComponent<carta>().getDefensa();
        }
        else
        {
            ataqueconvertidor = clon[cambio].GetComponent<carta>().getAtaque();
            defConvertidor = clon[cambio].GetComponent<carta>().getDefensa();
        }
        string nombre = (string)txt.getnom().GetValue(cambiar);
        int atributo1 = int.Parse((string)txt.GetAtributos1().GetValue(cambiar));
        int atributo2 = int.Parse((string)txt.GetAtributos2().GetValue(cambiar));
        int tipoMonstruo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(cambiar));
        clon[indice].GetComponent<carta>().SetAtaque(ataqueconvertidor);
        clon[indice].GetComponent<carta>().SetDefensa(defConvertidor);
        clon[indice].GetComponent<carta>().SetName(nombre);
        clon[indice].GetComponent<carta>().SetGuardianStar(atributo1);
        clon[indice].GetComponent<carta>().SetGuardianStar2(atributo2);
        clon[indice].GetComponent<carta>().SetTipoAtributo(tipoMonstruo);
        if (conCampo)
        {
            clon[indice].GetComponent<carta>().esInmortal = (campoU[cambio].GetComponent<carta>().esInmortal);
            clon[indice].GetComponent<carta>().SetTieneBono(campoU[cambio].GetComponent<carta>().GetTieneBono());
            clon[indice].GetComponent<carta>().SetTieneBonoDesfavorable(campoU[cambio].GetComponent<carta>().GetTieneBonoDesfavorable());
        }
        else
        {

            clon[indice].GetComponent<carta>().esInmortal = (clon[cambio].GetComponent<carta>().esInmortal);
            clon[indice].GetComponent<carta>().SetTieneBono(clon[cambio].GetComponent<carta>().GetTieneBono());
            clon[indice].GetComponent<carta>().SetTieneBonoDesfavorable(clon[cambio].GetComponent<carta>().GetTieneBonoDesfavorable());
        }

        if (clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
        {
            color = 2;
        }
        else
        {
            color = 1;
        }
        clon[indice].GetComponent<carta>().SetTipoCarta("Monstruo");
        if (conCampo)
        {
            clon[indice].GetComponent<carta>().SetPos(campoU[cambio].GetComponent<carta>().getPos());
        }
        else
        {
            clon[indice].GetComponent<carta>().SetPos(clon[cambio].GetComponent<carta>().getPos());
        }

        clon[indice].GetComponent<muestraCarta>().ataque.text = "" + ataqueconvertidor;
        clon[indice].GetComponent<muestraCarta>().defensa.text = "" + defConvertidor;
        clon[indice].GetComponent<muestraCarta>().ataqueB.text = atkText + ataqueconvertidor;
        clon[indice].GetComponent<muestraCarta>().defensaB.text = defText + defConvertidor;
        clon[indice].GetComponent<muestraCarta>().nombreCarta.text = nombre;

        clon[indice].GetComponent<muestraCarta>().panelDatos.texture = clon[indice].GetComponent<muestraCarta>().color[0];
        imgCarta = campo.GetManoUsuario(indice);
        if (conCampo)
        {
            campo.SetManoUsuario(indice, campo.GetCampoUsuario(cambio));
        }
        else
        {
            campo.SetManoUsuario(indice, campo.GetManoUsuario(cambio));
        }
    }
    IEnumerator AnimacionColocarCartaDescarte(int indice)
    {
        camara.DevolverCamara(false);
        List<int> temp = controles.GetListaDCartas();
        if (controles.GetListaDCartas().Count < 1)
        {
            yield return AnimacionDevolverCamara(true, false);

        }
        else
        {
            yield return AnimacionDevolverCamara(true, true);

        }
        yield return new WaitForSeconds(0.2f);
        if (controles.GetListaDCartas().Count < 1)
        {
            Vector3[] destino = new Vector3[5];
            destino[0] = new Vector3(3.2021f, 1.51f, 4.89f);
            destino[1] = new Vector3(173f, 208.9f, 0f);
            int realizada = 0;
            if (campoU[indice].GetComponent<carta>().GetDatosCarta() == 0)
            {
                campoU[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            }
            if (clon[pos].GetComponent<carta>().GetDatosCarta() == 0)
            {

                clon[pos].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);

            }
            clon[pos].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            campoU[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            campoU[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
            while (realizada < 2)
            {


                realizada = 0;
                campoU[indice].transform.localPosition = Vector3.MoveTowards(campoU[indice].transform.localPosition, destino[0], Time.deltaTime * 1900);
                clon[pos].transform.localPosition = Vector3.MoveTowards(clon[pos].transform.localPosition, destino[1], Time.deltaTime * 1900);


                if (Vector3.Distance(campoU[indice].transform.localPosition, destino[0]) < 1)
                {
                    realizada++;
                }
                if (Vector3.Distance(clon[pos].transform.localPosition, destino[1]) < 1)
                {
                    realizada++;
                }



                yield return null;
            }
            campoU[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
            while (clon[pos].transform.localPosition.x >= -303f)
            {

                clon[pos].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                yield return null;
            }
            clon[pos].transform.localPosition = new Vector3(-303f, clon[pos].transform.localPosition.y, clon[pos].transform.localPosition.z);
            if (clon[pos].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
            {
                int aumento = EquipoConCampo(indice);
                CambiarCartaEquipo(pos, indice, true);

                if (aumento != 0)
                {

                    yield return GetUpgrade(clon[pos], aumento, campoU[indice],false);
                }
                else
                {
                    campoU[indice].GetComponent<muestraCarta>().contenedorNormal.SetActive(false);

                    campoU[indice].GetComponent<muestraCarta>().panelDatos.texture = campoU[indice].GetComponent<muestraCarta>().color[color];


                    campoU[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
                    campoU[indice].GetComponent<muestraCarta>().textoMT.text = txtMT;

                    campoU[indice].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(imgCarta);
                    campoU[indice].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(imgCarta);
                    while (campoU[indice].transform.localPosition.x <= 6)
                    {
                        campoU[indice].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                        yield return null;
                    }

                    Object.Destroy(campoU[indice]);
                    campoU[indice] = null;

                }
                clon[pos].GetComponent<carta>().SetDatosCarta(1);

                StartCoroutine(AnimacionColocarGuardian(indice));
            }
            else
            {
                int fusionar = FusionConCampo(indice);
                if (fusionar != 0)
                {

                    juego.FusionCorrecta++;
                    yield return GetFusion(clon[pos], pos, fusionar, campoU[indice], true);

                }
                else
                {
                    juego.ReproducirNoFusion();
                    while (campoU[indice].transform.localPosition.x <= 6)
                    {
                        campoU[indice].transform.Translate(5f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                        yield return null;
                    }

                    Object.Destroy(campoU[indice]);
                    campoU[indice] = null;
                    yield return new WaitForSeconds(0.3f);
                }
                clon[pos].GetComponent<carta>().SetDatosCarta(1);
                juego.EfectosCartasCampo(juego.GetCampoModificado());

                StartCoroutine(AnimacionColocarGuardian(indice));
            }




        }
        else
        {
            float posicionX = -303f;

            intefaz.datosCarta.SetActive(false);

            Vector3 final = new Vector3(3.2021f, 1.51f, 4.89f);
            if (campoU[indice] != null)
            {
                if (campoU[indice].GetComponent<carta>().GetDatosCarta() == 0)
                {
                    campoU[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                }
                campoU[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
                campoU[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
                while (Vector3.Distance(campoU[indice].transform.localPosition, final) > Time.deltaTime * 1)
                {
                    campoU[indice].transform.localPosition = Vector3.MoveTowards(campoU[indice].transform.localPosition, final, Time.deltaTime * 100);
                    yield return null;

                }
                campoU[indice].transform.localPosition = new Vector3(3.2021f, 1.51f, 4.89f);





            }
            Vector3[] destino = new Vector3[5];
            destino[0] = new Vector3(173f, 208.9f, 0f);
            destino[1] = new Vector3(203f, 208.9f, 0f);
            destino[2] = new Vector3(233f, 208.9f, 0f);
            destino[3] = new Vector3(263f, 208.9f, 0f);
            destino[4] = new Vector3(293f, 208.9f, 0f);
            if (campoU[indice] == null)
            {
                destino[0] = new Vector3(-303f, 208.9f, 0f);
                destino[1] = new Vector3(203f, 208.9f, 0f);
                destino[2] = new Vector3(233f, 208.9f, 0f);
                destino[3] = new Vector3(263f, 208.9f, 0f);
                destino[4] = new Vector3(293f, 208.9f, 0f);
            }

            int realizada = 0;


            float posicionar = 0;

            while (realizada < temp.Count)
            {

                for (int i = 0; i < temp.Count; i++)
                {
                    realizada = 0;
                    clon[temp[i]].transform.localPosition = Vector3.MoveTowards(clon[temp[i]].transform.localPosition, destino[i], Time.deltaTime * 1900);

                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (Vector3.Distance(clon[temp[j]].transform.localPosition, destino[j]) < 1)
                        {
                            realizada++;
                        }

                    }
                }
                yield return null;
            }

            if (campoU[indice] == null)
            {
                for (int i = 1; i < temp.Count; i++)
                {
                    clon[temp[i]].transform.localPosition = new Vector3(203f + posicionar, 208.9f, 0f);
                    posicionar += 30f;
                }
            }
            else
            {
                for (int i = 0; i < temp.Count; i++)
                {
                    clon[temp[i]].transform.localPosition = new Vector3(173f + posicionar, 208.9f, 0f);
                    posicionar += 30f;
                }
            }

            yield return null;



            if (campoU[indice] != null)
            {


                while (clon[temp[0]].transform.localPosition.x >= posicionX)
                {

                    clon[temp[0]].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                    yield return null;
                }
                clon[temp[0]].transform.localPosition = new Vector3(-303f, clon[temp[0]].transform.localPosition.y, clon[temp[0]].transform.localPosition.z);
                if (!clon[temp[0]].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                {
                    int aumento = EquipoDecarte(temp[0], indice, true);
                    CambiarCartaEquipo(temp[0], indice, true);
                    if (aumento != 0)
                    {
                        yield return GetUpgrade(clon[temp[0]], aumento, campoU[indice],false);
                    }
                    else
                    {
                        juego.ReproducirNoFusion();
                        campoU[indice].GetComponent<muestraCarta>().contenedorNormal.SetActive(false);

                        campoU[indice].GetComponent<muestraCarta>().panelDatos.texture = campoU[indice].GetComponent<muestraCarta>().color[color];


                        campoU[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
                        campoU[indice].GetComponent<muestraCarta>().textoMT.text = txtMT;


                        campoU[indice].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(imgCarta);
                        campoU[indice].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(imgCarta);
                        while (campoU[indice].transform.localPosition.x <= 6)
                        {
                            campoU[indice].transform.Translate(5f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;
                        }

                        Object.Destroy(campoU[indice]);
                        campoU[indice] = null;
                        yield return new WaitForSeconds(0.3f);

                    }

                }
                else
                {
                    int fusionar = fusionCartaDesecha(indice, temp[0], true);
                    if (fusionar != 0)
                    {
                        juego.FusionCorrecta++;
                        yield return GetFusion(clon[temp[0]], temp[0], fusionar, campoU[indice], true);

                    }
                    else
                    {
                        juego.ReproducirNoFusion();

                        while (campoU[indice].transform.localPosition.x <= 6)
                        {
                            campoU[indice].transform.Translate(6f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;
                        }
                        Object.Destroy(campoU[indice]);
                        campoU[indice] = null;
                        yield return new WaitForSeconds(0.3f);
                    }

                }


            }

            for (int i = 1; i < temp.Count; i++)
            {
                while (clon[temp[i]].transform.localPosition.x >= posicionX)
                {

                    clon[temp[i]].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                    yield return null;
                }
                if (clon[temp[i - 1]].GetComponent<carta>().GetTipoCarta().Equals("Monstruo") && !clon[temp[i]].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                {
                    int aumento = EquipoDecarte(temp[i], temp[i - 1], false);
                    CambiarCartaEquipo(temp[i], temp[i - 1], false);

                    if (aumento != 0)
                    {
                        Debug.LogError("cuando entrao en el miltor");
                        yield return GetUpgrade(clon[temp[i]], aumento, clon[temp[i - 1]],false);
                    }
                    else
                    {
                        juego.ReproducirNoFusion();
                        clon[temp[i - 1]].GetComponent<muestraCarta>().contenedorNormal.SetActive(false);

                        clon[temp[i - 1]].GetComponent<muestraCarta>().panelDatos.texture = clon[temp[i - 1]].GetComponent<muestraCarta>().color[color];


                        clon[temp[i - 1]].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
                        clon[temp[i - 1]].GetComponent<muestraCarta>().textoMT.text = txtMT;

                        clon[temp[i - 1]].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(imgCarta);
                        clon[temp[i - 1]].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(imgCarta);
                        while (clon[temp[i - 1]].transform.localPosition.x >= -500)
                        {
                            clon[temp[i - 1]].transform.Translate(-600f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;
                        }

                        Object.Destroy(clon[temp[i - 1]]);
                        clon[temp[i - 1]] = null;
                        yield return new WaitForSeconds(0.3f);

                    }

                }
                else if (clon[temp[i]].GetComponent<carta>().GetTipoCarta().Equals("Monstruo") && !clon[temp[i - 1]].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                {
                    int aumento = EquipoDecarte(temp[i - 1], temp[i], false);


                    if (aumento != 0)
                    {
                        yield return GetUpgrade(clon[temp[i]], aumento, clon[temp[i - 1]],false);
                    }
                    else
                    {

                        juego.ReproducirNoFusion();
                        //clon[temp[i]].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.GetManoUsuario(temp[i-1]));
                        while (clon[temp[i - 1]].transform.localPosition.x >= -500)
                        {
                            clon[temp[i - 1]].transform.Translate(-600f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;
                        }

                        Object.Destroy(clon[temp[i - 1]]);
                        clon[temp[i - 1]] = null;
                        yield return new WaitForSeconds(0.3f);

                    }

                }
                else
                {
                    int fusionar = fusionCartaDesecha(temp[i], temp[i - 1], false);
                    if (fusionar != 0)
                    {
                        juego.FusionCorrecta++;
                        yield return GetFusion(clon[temp[i]], temp[i], fusionar, clon[temp[i - 1]], true);

                    }
                    else
                    {

                        juego.ReproducirNoFusion();
                        while (clon[temp[i - 1]].transform.localPosition.x >= -500)
                        {
                            clon[temp[i - 1]].transform.Translate(-600f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;

                        }
                        Object.Destroy(clon[temp[i - 1]]);
                        clon[temp[i - 1]] = null;
                        yield return new WaitForSeconds(0.3f);
                    }

                }


            }





            pos = temp[temp.Count - 1];
            clon[pos].GetComponent<carta>().SetDatosCarta(1);


            StartCoroutine(AnimacionColocarGuardian(indice));

        }


    }
    IEnumerator AnimacionColocarGuardian(int indice)
    {

        //mover

        if (controles.GetListaDCartas().Count < 1)
        {

            intefaz.SetEstadoApuntador(false);
            camara.DevolverCamara(false);
            yield return AnimacionDevolverCamara(true, false);

        }
        Vector3 final = new Vector3(18f, 310f, 0f);
        while (Vector3.Distance(clon[pos].transform.localPosition, final) > Time.deltaTime * 2000)
        {
            clon[pos].transform.localPosition = Vector3.MoveTowards(clon[pos].transform.localPosition, final, Time.deltaTime * 2000);
            yield return null;
        }
        clon[pos].transform.localPosition = final;

        intefaz.EstadoGuardianes(true);
        controles.SetFase("ponerGuardian");


    }
    public void EfectoReverseTrap(int indice)
    {
        if (juego.GetTurnoUsuario())
        {
            if (getCartaCampoU(indice).GetComponent<carta>().getAtaque() - (reduccion * 2) < 0)
            {
                getCartaCampoU(indice).GetComponent<carta>().SetAtaque(0);
            }
            else
            {
                getCartaCampoU(indice).GetComponent<carta>().SetAtaque(getCartaCampoU(indice).GetComponent<carta>().getAtaque() - (reduccion * 2));
            }
            if (getCartaCampoU(indice).GetComponent<carta>().getDefensa() - (reduccion * 2) < 0)
            {
                getCartaCampoU(indice).GetComponent<carta>().SetDefensa(0);

            }
            else
            {
                getCartaCampoU(indice).GetComponent<carta>().SetDefensa(getCartaCampoU(indice).GetComponent<carta>().getDefensa() - (reduccion * 2));
            }


            getCartaCampoU(indice).GetComponent<muestraCarta>().ataque.text = "" + getCartaCampoU(indice).GetComponent<carta>().getAtaque();
            getCartaCampoU(indice).GetComponent<muestraCarta>().defensa.text = "" + getCartaCampoU(indice).GetComponent<carta>().getDefensa();
        }
        else
        {
            if (GetCartaCpu(indice).GetComponent<carta>().getAtaque() - (reduccion * 2) < 0)
            {
                GetCartaCpu(indice).GetComponent<carta>().SetAtaque(0);
            }
            else
            {
                GetCartaCpu(indice).GetComponent<carta>().SetAtaque(GetCartaCpu(indice).GetComponent<carta>().getAtaque() - (reduccion * 2));
            }
            if (GetCartaCpu(indice).GetComponent<carta>().getDefensa() - (reduccion * 2) < 0)
            {
                GetCartaCpu(indice).GetComponent<carta>().SetDefensa(0);

            }
            else
            {
                GetCartaCpu(indice).GetComponent<carta>().SetDefensa(GetCartaCpu(indice).GetComponent<carta>().getDefensa() - (reduccion * 2));
            }


            GetCartaCpu(indice).GetComponent<muestraCarta>().ataque.text = "" + GetCartaCpu(indice).GetComponent<carta>().getAtaque();
            GetCartaCpu(indice).GetComponent<muestraCarta>().defensa.text = "" + GetCartaCpu(indice).GetComponent<carta>().getDefensa();
        }
    }
    IEnumerator AnimacionFusion(GameObject card)
    {
        float rotar = -45f;
        bool realizada = false;
        bool animacion1 = false;

        while (!realizada)
        {

            float rotacion = rotar * 45 * Time.deltaTime;
            card.transform.Rotate(0f, rotacion, 0f);
            if (card.transform.eulerAngles.y > 90 && animacion1 == false)
            {
                card.GetComponent<Transform>().eulerAngles = new Vector3(0f, 90f, 0f);


                animacion1 = true;


            }

            if (card.transform.eulerAngles.y > 180 && animacion1 == true)
            {

                card.GetComponent<Transform>().eulerAngles = new Vector3(0f, 0f, 0f);
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }




    }
    IEnumerator AnimacionAumentoCampo(int indice)
    {
        float rotar = -45f;
        bool realizada = false;
        bool animacion1 = false;

        while (!realizada)
        {

            float rotacion = rotar * 45 * Time.deltaTime;
            campoU[indice].transform.Rotate(0f, rotacion, 0f);
            if (campoU[indice].transform.eulerAngles.y > 270 && animacion1 == false)
            {
                campoU[indice].GetComponent<Transform>().eulerAngles = new Vector3(180f, 270f, 0f);


                animacion1 = true;


            }

            if (campoU[indice].transform.eulerAngles.y > 180 && animacion1 == true)
            {

                campoU[indice].GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, 0f);
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
    public void ColocarCarta(int indice, bool guardian)
    {

        intefaz.SetEstadoApuntador(false);
        if (clon[pos].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            intefaz.ReiniciarApuntador();
            clon[pos].GetComponent<carta>().SetGuardianStarA(clon[pos].GetComponent<carta>().GetGuardianStar2());
            if (guardian == true)
            {
                clon[pos].GetComponent<carta>().SetGuardianStarA(clon[pos].GetComponent<carta>().GetGuardianStar());
            }
            StopAllCoroutines();
            StartCoroutine(AnimacionColocarCarta(indice));
        }
        else
        {
            if (campoU[indice] != null)
            {
                StartCoroutine(AnimacionDestruirEquipo(indice));
            }
            else
            {
                intefaz.ReiniciarApuntador();
                StopAllCoroutines();
                StartCoroutine(AnimacionColocarCarta(indice));
            }
        }

    }
    // LISTA DE POSICIONES CARTAS EN TABLERO USUARIO
    public float posicionesTableroUsuario(int indice)
    {
        Debug.LogError("queldo" + campoU[indice] +"y "+indice);
        float[] x = new float[5];
        x[0] = 3.24f;
        x[1] = 1.62f;
        x[2] = 0.03f;
        x[3] = -1.58f;
        x[4] = -3.2f;
        if (campoU[indice] != null)
        {
            if (!campoU[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                indice -= 5;

            }
        }

        return x[indice];
    }
    
    public void InstantiateCardsField(int idFieldCard, GameObject handCard)
    {
        GameObject fieldCard;
        if (juego.GetTurnoUsuario())
        {
            campoU[idFieldCard] = Instantiate(originalCampo, new Vector3(original.transform.position.x, original.transform.position.y, original.transform.position.z), original.transform.rotation);
            fieldCard = campoU[idFieldCard];
        }
        else
        {
            campoCpu[idFieldCard] = Instantiate(originalCampo, new Vector3(original.transform.position.x, original.transform.position.y, original.transform.position.z), original.transform.rotation);
            fieldCard = campoCpu[idFieldCard];
        }
    
       
        fieldCard.GetComponent<muestraCarta>().imagenCarta.texture = handCard.GetComponent<muestraCarta>().imagenCarta.texture;
        fieldCard.GetComponent<muestraCarta>().imagenMiniCarta.texture = handCard.GetComponent<muestraCarta>().imagenMiniCarta.texture;
        if (handCard.GetComponent<carta>().GetDatosCarta() == 0)
        {
            fieldCard.GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
        }
        string tipoCarta = handCard.GetComponent<carta>().GetTipoCarta();

        string nombre = handCard.GetComponent<carta>().GetName();
        fieldCard.GetComponent<carta>().SetName(nombre);
        fieldCard.GetComponent<carta>().SetTipoCarta(tipoCarta);
        fieldCard.GetComponent<carta>().SetAttribute(handCard.GetComponent<carta>().GetAttribute());
        fieldCard.GetComponent<muestraCarta>().nombreCarta.text = nombre;
        if (tipoCarta.Equals("Monstruo"))
        {
            int ataqueconvertidor = handCard.GetComponent<carta>().getAtaque();
            int defConvertidor = handCard.GetComponent<carta>().getDefensa();
            int atributo1 = handCard.GetComponent<carta>().GetGuardianStar();
            int atributo2 = handCard.GetComponent<carta>().GetGuardianStar2();
            int guardianActivo = handCard.GetComponent<carta>().GetGuardianStarA();
            int tipoAtributo = handCard.GetComponent<carta>().GetTipoAtributo();
            int datosCarta = handCard.GetComponent<carta>().GetDatosCarta();
            fieldCard.GetComponent<carta>().SetDatosCarta(datosCarta);
            fieldCard.GetComponent<carta>().SetAtaque(ataqueconvertidor);
            fieldCard.GetComponent<carta>().SetDefensa(defConvertidor);
            fieldCard.GetComponent<carta>().SetGuardianStar(atributo1);
            fieldCard.GetComponent<carta>().SetGuardianStar2(atributo2);
            fieldCard.GetComponent<carta>().SetTipoAtributo(tipoAtributo);
            fieldCard.GetComponent<muestraCarta>().ataque.text = ataqueconvertidor.ToString();
            fieldCard.GetComponent<muestraCarta>().defensa.text = defConvertidor.ToString();
            fieldCard.GetComponent<muestraCarta>().ataque.text = handCard.GetComponent<carta>().getAtaque().ToString();
            fieldCard.GetComponent<muestraCarta>().defensa.text = handCard.GetComponent<carta>().getDefensa().ToString();
            fieldCard.GetComponent<carta>().SetTieneBono(handCard.GetComponent<carta>().GetTieneBono());
            fieldCard.GetComponent<carta>().SetTieneBonoDesfavorable(handCard.GetComponent<carta>().GetTieneBonoDesfavorable());
            fieldCard.GetComponent<carta>().esInmortal = (handCard.GetComponent<carta>().esInmortal);
            fieldCard.GetComponent<carta>().SetGuardianStarA(guardianActivo);
            fieldCard.GetComponent<carta>().SetStarsNumber(handCard.GetComponent<carta>().GetStarsNumber());
            if (fieldCard.GetComponent<carta>().esInmortal)
            {
                fieldCard.GetComponent<muestraCarta>().panelDatos.texture = fieldCard.GetComponent<muestraCarta>().color[3];
            }


        }
        else
        {
            fieldCard.GetComponent<muestraCarta>().contenedorNormal.SetActive(false);
            fieldCard.GetComponent<muestraCarta>().textoMT.text = tipoCarta.ToUpper();
            if (tipoCarta.Equals("Trampa"))
            {
                fieldCard.GetComponent<muestraCarta>().panelDatos.texture = fieldCard.GetComponent<muestraCarta>().color[2];
            }
            else
            {
                fieldCard.GetComponent<muestraCarta>().panelDatos.texture = fieldCard.GetComponent<muestraCarta>().color[1];
            }
            fieldCard.GetComponent<carta>().SetGuardianStarA(0);

        }
        if (juego.GetTurnoUsuario())
        {
            fieldCard.transform.parent = contenedorCampoUsuario.transform;
        }
        else
        {
            fieldCard.transform.parent = contenedorCampoCpu.transform;
        }
        
    }

    IEnumerator AnimacionColocarCarta(int indice)
    {
        if (!clon[pos].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {

            camara.DevolverCamara(true);
            yield return AnimacionDevolverCamara(true, false);
            Vector3 final1 = new Vector3(18f, 310f, 0f);
            while (Vector3.Distance(clon[pos].transform.localPosition, final1) > Time.deltaTime * 2000)
            {
                clon[pos].transform.localPosition = Vector3.MoveTowards(clon[pos].transform.localPosition, final1, Time.deltaTime * 2000);
                yield return null;
            }
            clon[pos].transform.localPosition = final1;

        }
        yield return null;
        while (clon[pos].transform.localPosition.y < 570)
        {

            clon[pos].transform.Translate(0f * Time.deltaTime, 1200f * Time.deltaTime, 0f * Time.deltaTime);

            yield return null;
        }
        if (clon[pos].GetComponent<carta>().GetDatosCarta() == 0)
        {
            juego.CartasBocaAbajo++;
        }
        InstantiateCardsField(indice,clon[pos]);
        Object.Destroy(clon[pos]);
        clon[pos] = null;
        if (reduccion != 0)
        {
            int activador = 0;
            for (int i = 5; i < 10; i++)
            {
                if (GetCartaCpu(i) != null)
                {
                    if (campo.GetCampoCpu(i) == 698)
                    {
                        activador = i;
                    }
                }
            }
            if (activador != 0)
            {
                yield return StartCoroutine(ShowCardEffects(GetCartaCpu(activador), activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));
                EfectoReverseTrap(indice);
            }
        }

        reduccion = 0;
        //mover en x selectivo respecto al indice del campo(cuadro verde)
        float posicionarX = posicionesTableroUsuario(indice);


        float posicionarZ = 1.77f;
        if (!campoU[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            posicionarZ = 3.42f;
        }

        campoU[indice].transform.localPosition = new Vector3(posicionarX, 5f, posicionarZ);
        if (campoU[indice].GetComponent<carta>().getPos() == 1)
        {
            campoU[indice].transform.localScale = new Vector3(1.7f, 1.7f, 0.01f);
            campoU[indice].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);

        }
        else
        {
            campoU[indice].transform.localScale = new Vector3(1.4f, 1.8f, 0.01f);
            campoU[indice].GetComponent<Transform>().eulerAngles = new Vector3(90f, 90f, 0f);
        }

        //bajarCarta hasta 0,071
        Vector3 final = new Vector3(campoU[indice].transform.localPosition.x, 0.071f, campoU[indice].transform.localPosition.z);
        while (Vector3.Distance(campoU[indice].transform.localPosition, final) > Time.deltaTime * 2)
        {
            campoU[indice].transform.localPosition = Vector3.MoveTowards(campoU[indice].transform.localPosition, final, Time.deltaTime * 7);
            yield return null;
        }
        /* while (campoU[indice].transform.localPosition.y >= 0.071)
         {
             campoU[indice].transform.Translate(0f * Time.deltaTime, 0f * Time.deltaTime, 7f * Time.deltaTime);
             yield return null;
         }*/
        campoU[indice].transform.localPosition = new Vector3(posicionarX, 0.071f, posicionarZ);
        camara.MoverCamara(false);
        yield return AnimacionMoverCamara(true, true);
        campo.SetCampoUsuario(indice, campo.GetManoUsuario(pos));
        if (controles.GetListaDCartas().Count > 0)
        {
            List<int> temp = controles.GetListaDCartas();
            for (int i = 0; i < temp.Count; i++)
            {
                campo.SetManoUsuario(temp[i], 0);
                Object.Destroy(clon[temp[i]]);
                clon[temp[i]] = null;
            }
        }
        else
        {
            campo.SetManoUsuario(pos, 0);
        }

        if (campoU[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            campo.SetDefensaUsuario(indice, campoU[indice].GetComponent<carta>().getDefensa());
            campo.SetPosCampo(indice, pos);
            campo.SetAtaquesUsuario(indice, 1);
            intefaz.ActivarGuardianStar(true);
            intefaz.MoverApuntador(indice, false);

        }
        else
        {
            intefaz.MoverApuntador(indice, true);
        }
        controles.SetFase("acabarTurno");
        intefaz.ActivarDatosUI(indice);


    }

    private IEnumerator ShowCardEffects(GameObject card ,int activador,Vector3 localPosition,Quaternion quaternion)
    {
        intefaz.SetTiempoFlash(2f);
        intefaz.SetFlash(true);
        card.transform.localScale = new Vector3(1.5f, 1.5f, 0.1f);
        card.transform.localPosition = localPosition;
        card.transform.rotation = quaternion;
        juego.ReproducirActivacion();
        yield return new WaitForSeconds(1f);
        int field = !juego.GetTurnoUsuario() ? campo.GetCampoUsuario(activador) : campo.GetCampoCpu(activador);
        card.transform.localScale = new Vector3(4f, 3f, 0.1f);
        yield return StartCoroutine (SetCardRotation(card, field));
        yield return new WaitForSeconds(1f);
        float reductor = 3f;
        while (reductor > 0)
        {
            card.transform.localScale = new Vector3(4f, reductor, 0.1f);
            reductor -= 0.1f;
            yield return null;
        }
        intefaz.ColorFlash();
        intefaz.SetFlash(true);
        yield return new WaitForSeconds(0.5f);
        if (!juego.GetTurnoUsuario())
        {
            DestroyFieldCard(activador, campoU);
        }
        else
        {
            DestroyFieldCard(activador, campoCpu);
        }
      

    }

   

    private IEnumerator SetCardRotation(GameObject card, int field)
    {
        int tiempo = 0;
        var cardComponent = card.GetComponent<muestraCarta>();
        string cardName= txt.nombresCartas.GetValue(field).ToString();
        cardComponent.nombreCarta.text = cardName;
        cardComponent.nombreCarta.fontSize = GetFontCardName(cardName,true);
        cardComponent.imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(field);
        cardComponent.specialContainer.SetActive(true);
        if (card.GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
        {
            cardComponent.trapContainer.SetActive(true);
        }
        GetAttribute(card);
        for (int i = 0; i < 180; i += 10)
        {
            yield return new WaitForSeconds(0.01f);
            card.transform.Rotate(0f, -10, 0f);
            tiempo += 10;
            if (tiempo == 90 || tiempo == -90)
            {
                card.transform.eulerAngles = new Vector3(180, -90, 0);
                cardComponent.contenedorBatalla.SetActive(true);
            }
        }
    }

    private void DestroyFieldCard(int activador, GameObject[] campoArray)
    {
        Object.Destroy(campoArray[activador]);
        campoArray[activador] = null;
        if (!juego.GetTurnoUsuario())
        {
            campo.SetCampoUsuario(activador, 0);
        }
        else
        {
            campo.SetCampoCpu(activador, 0);
        }
    }

    public void CambiarPosCarta(int indice)
    {

        if (campoU[indice] != null)
        {
            juego.ReproducirCambiarPos();
            StartCoroutine(CambiarPos(indice));
        }


    }
    IEnumerator CambiarPos(int indice)
    {
        //cambiar posicion

        int posCarta = indice;
        for (int i = 0; i < 90; i += 15)
        {
            yield return new WaitForSeconds(0.01f);
            if (campoU[indice].GetComponent<carta>().getPos() == 1)
            {
                campoU[indice].transform.Rotate(0f, 0f, 15f);
            }
            else
            {
                campoU[indice].transform.Rotate(0f, 0f, -15);
            }
        }
        if (campoU[posCarta].GetComponent<carta>().getPos() == 1)
        {

            campoU[posCarta].transform.localScale = new Vector3(1.4f, 1.8f, 0.1f);
            campoU[posCarta].GetComponent<carta>().SetPos(0);
        }
        else
        {

            campoU[posCarta].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);
            campoU[posCarta].GetComponent<carta>().SetPos(1);
        }






    }
    //metodos para la cpu
    public void CreasInstanciasCpu(int i)
    {
        indiceMT = -1;
        clonCpu[i] = Instantiate(original, new Vector3(original.transform.position.x + 800, original.transform.position.y, original.transform.position.z), original.transform.rotation);
        clonCpu[i].GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
        string tipoCarta = txt.GetTipoCarta().GetValue(campo.GetManoCpu(i)).ToString().Trim();
        string nombre = (string)txt.getnom().GetValue(campo.GetManoCpu(i));
        clonCpu[i].GetComponent<carta>().SetName(nombre);
        clonCpu[i].GetComponent<carta>().SetTipoCarta(tipoCarta);
        string attribute = txt.GetAttributes().GetValue(campo.GetManoCpu(i)).ToString();
        clonCpu[i].GetComponent<carta>().SetAttribute(attribute);
        if (tipoCarta.Equals("Monstruo"))
        {
            int ataqueconvertidor = int.Parse((string)txt.getatk().GetValue(campo.GetManoCpu(i)));
            int defConvertidor = int.Parse((string)txt.getdef().GetValue(campo.GetManoCpu(i)));
            int atributo = int.Parse((string)txt.GetAtributos1().GetValue(campo.GetManoCpu(i)));
            int tipoAtributo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(campo.GetManoCpu(i)));
            clonCpu[i].GetComponent<carta>().SetAtaque(ataqueconvertidor);
            clonCpu[i].GetComponent<carta>().SetDefensa(defConvertidor);
            clonCpu[i].GetComponent<carta>().SetName(nombre);
            clonCpu[i].GetComponent<carta>().SetGuardianStarA(atributo);
            clonCpu[i].GetComponent<carta>().SetTipoAtributo(tipoAtributo);
            clonCpu[i].GetComponent<muestraCarta>().ataque.text = ataqueconvertidor.ToString();
            clonCpu[i].GetComponent<muestraCarta>().defensa.text = defConvertidor.ToString();
            int stars = int.Parse((string)txt.GetStars().GetValue(campo.GetManoCpu(i)));
            clonCpu[i].GetComponent<carta>().SetStarsNumber(stars);
        }
        else
        {
            clonCpu[i].GetComponent<muestraCarta>().contenedorNormal.SetActive(false);
            clonCpu[i].GetComponent<muestraCarta>().textoMT.text = tipoCarta.ToUpper();
            if (tipoCarta.Equals("Trampa"))
            {
                clonCpu[i].GetComponent<muestraCarta>().panelDatos.texture = clonCpu[i].GetComponent<muestraCarta>().color[2];
            }
            else
            {
                clonCpu[i].GetComponent<muestraCarta>().panelDatos.texture = clonCpu[i].GetComponent<muestraCarta>().color[1];
            }

            clonCpu[i].GetComponent<carta>().SetGuardianStarA(0);
        }
        clonCpu[i].transform.SetParent(contenedorCpu.transform, false);

    }
    public void AnimacionInstanciasCpu(int cont)
    {

        StartCoroutine(TiempoInstanciasCpu(cont));
    }
    IEnumerator TiempoInstanciasCpu(int i)

    {

        bool realizada = false;
        float pos = -2f;
        float espaciado = 160.5f;
        for (i = 5 - i; i < 5; i++)
        {

            realizada = false;
            while (!realizada)
            {
                float posicionar = pos * 3000 * Time.deltaTime;
                if (clonCpu[i] != null)
                {
                    clonCpu[i].transform.Translate(posicionar, 0f, 0f);
                    if (clonCpu[i].transform.localPosition.x < -303 + (i * espaciado))
                    {
                        juego.SetCantDeckCpu();
                        clonCpu[i].transform.localPosition = new Vector3(-303 + (i * espaciado), 77.95f, 0);
                        realizada = true;


                    }
                }
                else
                {
                    realizada = true;
                }

                yield return new WaitForSeconds(0.005f);
            }
            juego.ReproducirRobar();

        }
        if (campo.SinCartasCpu() == true)
        {
            juego.FinJuegoPorCartas();
        }
        else if (campo.ExodiaCpu() == true)
        {
            juego.FinJuegoPorExodia();
        }
        else
        {
            juego.EfectosCartasCampo(juego.GetCampoModificado());
            if (juego.GetEspadasLuzReveladora().Contains("usuario"))
            {
                if (juego.GetEspadasLuzReveladora().Equals(""))
                {
                    intefaz.SetEstadoEspadas(false, "");
                }
                else
                {
                    string numero = "";
                    if (juego.GetEspadasLuzReveladora().Contains("3"))
                    {
                        numero = "3";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else if (juego.GetEspadasLuzReveladora().Contains("2"))
                    {
                        numero = "2";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else if (juego.GetEspadasLuzReveladora().Contains("1"))
                    {
                        numero = "1";
                        intefaz.SetEstadoEspadas(true, numero);
                    }
                    else
                    {
                        juego.SetEspadasLuzReveladora("");
                        intefaz.SetEstadoEspadas(false, numero);
                    }

                }
            }
            if (juego.deckCpu.Count > 0)
            {
                TamañoManoCpu();
            }

            intefaz.SetEstadoFlecha(true);
            juego.LogicaManoCpu();
            controles.SetIndice(0);
        }



    }
    //metodo de ayuda de la cpu derivado del forbidden real
    public void TamañoManoCpu()
    {
        // validar si el tamaño de la mano no es 5
        if (juego.GetTamañoMano() != 5)
        {
            // determinar si la carta cero de la mano cpu no tiene mas ataque
            juego.OrdenarPorAtaqueCpu();
            if (int.Parse((string)txt.getatk().GetValue(campo.GetManoCpu(0))) < juego.ObtenerAtaqueMasAlto())
            {
                int aleatorio = Random.Range(0, Constants.CARDS_IN_DECK);

                if (aleatorio != 1)
                {

                    if (aleatorio < 3)
                    {
                        // ordenar por cartas campo y eso
                        juego.OrdenarPorMT();
                    }
                    else if (aleatorio < 6)
                    {

                        // ordenar por cartas defensa y eso
                        juego.OrdenarPorDefCpu();
                    }
                    else
                    {

                        // ordenar por ataque
                        juego.OrdenarPorAtaqueCpu();
                    }
                    juego.ReemplazarManoCpu();
                    // transformar la carta que se obtuvo del deck
                    //clonCpu[0].GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu(0));
                    string tipoCarta = txt.GetTipoCarta().GetValue(campo.GetManoCpu(0)).ToString().Trim();
                    string nombre = (string)txt.getnom().GetValue(campo.GetManoCpu(0));
                    clonCpu[0].GetComponent<carta>().SetName(nombre);
                    clonCpu[0].GetComponent<carta>().SetTipoCarta(tipoCarta);
                    clonCpu[0].GetComponent<carta>().SetTieneBono(false);
                    clonCpu[0].GetComponent<carta>().SetTieneBonoDesfavorable(false);
                    int stars = int.Parse((string)txt.GetStars().GetValue(campo.GetManoCpu(0)));
                    string attribute = txt.GetAttributes().GetValue(campo.GetManoCpu(0)).ToString();
                    clonCpu[0].GetComponent<carta>().SetAttribute(attribute);
                    if (tipoCarta.Equals("Monstruo"))
                    {
                        int ataqueconvertidor = int.Parse((string)txt.getatk().GetValue(campo.GetManoCpu(0)));
                        int defConvertidor = int.Parse((string)txt.getdef().GetValue(campo.GetManoCpu(0)));
                        int atributo = int.Parse((string)txt.GetAtributos1().GetValue(campo.GetManoCpu(0)));
                        int tipoAtributo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(campo.GetManoCpu(0)));
                        clonCpu[0].GetComponent<carta>().SetAtaque(ataqueconvertidor);
                        clonCpu[0].GetComponent<carta>().SetDefensa(defConvertidor);
                        clonCpu[0].GetComponent<carta>().SetGuardianStarA(atributo);
                        clonCpu[0].GetComponent<carta>().SetTipoAtributo(tipoAtributo);
                        clonCpu[0].GetComponent<carta>().SetStarsNumber(stars);
                    }
                    else
                    {
                        clonCpu[0].GetComponent<carta>().SetGuardianStarA(0);
                    }
                    juego.SetTamañoMano();
                    juego.EfectosCartasCampo(juego.GetCampoModificado());
                }
            }





        }
    }
    public GameObject GetClonCpu(int indice)
    {
        return clonCpu[indice];
    }
    public void SeleccionarCartaCpu(int indice)
    {
        controles.ManoCpu(indice);
    }
    public void SetTransformacionCpu(int indice)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i != indice)
            {
                clonCpu[i].GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                clonCpu[i].GetComponent<muestraCarta>().reverso.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }

        vec = clonCpu[indice].transform.position;
        StartCoroutine(MoverArribaCpu(indice));
        //StartCoroutine(Animacion1Cpu(indice));
    }
    IEnumerator MoverArribaCpu(int indice)
    {

        Vector3 final = new Vector3(18f, 219f, 0f);
        while (Vector3.Distance(clonCpu[indice].transform.localPosition, final) > Time.deltaTime * 1800)
        {
            clonCpu[indice].transform.localPosition = Vector3.MoveTowards(clonCpu[indice].transform.localPosition, final, Time.deltaTime * 1800);
            yield return null;
        }


        clonCpu[indice].transform.localPosition = final;
        //clon[indice].transform.localPosition = new Vector3(0.7f, 2f, 5f);
        StartCoroutine(Animacion1Cpu(indice));

    }
    IEnumerator Animacion1Cpu(int indice)
    {

        int tiempo = 0;
        for (int i = 0; i < 180; i += 15)
        {
            yield return new WaitForSeconds(0.01f);
            clonCpu[indice].transform.Rotate(0f, 15f, 0f);
            tiempo += 15;
        }
        tiempo = 0;
        yield return new WaitForSeconds(0.02f);
        if (clonCpu[indice].GetComponent<carta>().GetDatosCarta() == 1)
        {
            clonCpu[indice].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            clonCpu[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCpu[indice].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu(indice));
            clonCpu[indice].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetManoCpu(indice));
            clonCpu[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            EfectosCartasUsuarioMano(indice);
        }
        else
        {
            //StopAllCoroutines();
            Vector3 final = vec;
            while (Vector3.Distance(clonCpu[indice].transform.position, final) > Time.deltaTime * 1500)
            {
                clonCpu[indice].transform.position = Vector3.MoveTowards(clonCpu[indice].transform.position, final, Time.deltaTime * 1500);
                yield return null;
            }
            clonCpu[indice].transform.position = final;
            pos = indice;
            yield return new WaitForSeconds(0.1f);
            if (clonCpu[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                camara.MoverCamara(false);
            }
            else
            {
                camara.MoverCamara(true);

            }


            yield return AnimacionMoverCamara(false, false);
            int posActualCampo = 0;
            if (clonCpu[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                for (int i = 0; i < 5; i++)
                {
                    if (campo.GetCampoCpu(i) == 0)
                    {
                        posActualCampo = i;
                        break;
                    }
                }
                intefaz.ActualizarUICpu(posActualCampo);

            }
            else
            {

                intefaz.ActualizarUICpu(5);
            }
            VerificarPosCpu(indice);

        }




    }
    public void VerificarPosCpu(int posC)
    {
        if (validadorFusionCampoCpu.Count > 0)
        {
            controles.MoverenCampoCpu(validadorFusionCampoCpu[1], posC, true);
        }
        else if (!clonCpu[posC].GetComponent<carta>().GetTipoCarta().Equals("Monstruo") && controles.GetListaDCartas().Count < 1)
        {
            bool detener = false;
            int indice = 0;
            for (int i = 5; i < 10 && detener == false; i++)
            {
                indice = i;
                if (campoCpu[i] == null)
                {
                    indice = i;

                    detener = true;
                }
            }
            if (detener == true)
            {
                intefaz.UbicrMTCpu("ubicar");
                intefaz.MoverapuntadorporCamanra(true);
                posCampoCpuMT = indice;
                intefaz.MoverApuntador(indice, true);
                controles.MoverenCampoCpu(indice, posC, false);
            }

            if (detener == false)
            {
                intefaz.UbicrMTCpu("ubicar");
                intefaz.MoverapuntadorporCamanra(true);
                intefaz.MoverApuntadorAbajo();
                int rand = Random.Range(5, 10);
                posCampoCpuMT = rand;

                controles.MoverenCampoCpu(rand, posC, true);
            }
        }

        else
        {

            bool detener = false;
            int indice = 0;
            for (int i = 0; i < 5 && detener == false; i++)
            {
                indice = i;
                if (campoCpu[i] == null)
                {
                    indice = i;

                    detener = true;
                }
            }
            if (detener == true)
            {
                intefaz.MoverApuntador(indice, false);
                controles.MoverenCampoCpu(indice, posC, false);
            }

            if (detener == false)
            {
                //logica 9/10 de reemplazar carta ,cambiar y hacer que no cambie la carta mas fuerte en ataque
                // ordenar cartas en ataque del campo usuario
                //organizar los ataques del usuario y ponerlos en un nuevo array temporal
                int[] ataqueTemp = new int[5];
                int[] posTemp = new int[5];
                int[] defensaTemp = new int[5];
                //carta c = GetComponent<carta>();
                for (int i = 0; i < 5; i++)
                {

                    ataqueTemp[i] = campoCpu[i].GetComponent<carta>().getAtaque();
                    defensaTemp[i] = campoCpu[i].GetComponent<carta>().getDefensa();
                    posTemp[i] = i;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4 - i; j++)
                    {
                        if (ataqueTemp[j] > ataqueTemp[j + 1])
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
                if (defensaTemp[0] < 3000)
                {
                    controles.MoverenCampoCpu(posTemp[0], posC, true);
                }
                else
                {
                    controles.MoverenCampoCpu(posTemp[2], posC, true);
                }
            }
        }


    }
    public void InicioColocarCartaCpu(int posCarta, int indice)
    {
        pos = indice;
        ColocarCartaCpu(posCarta, indice);
    }
    IEnumerator AnimacionTransformacion2Cpu(int posCarta, int indice)
    {
        Vector3 final = vec;
        while (Vector3.Distance(clonCpu[indice].transform.position, final) > Time.deltaTime * 50)
        {
            clonCpu[indice].transform.position = Vector3.MoveTowards(clonCpu[indice].transform.position, final, Time.deltaTime * 50);
            yield return null;
        }
        clonCpu[indice].transform.position = final;
        pos = indice;
        StartCoroutine(AnimacionColocarCarta(posCarta, indice));
    }
    IEnumerator AnimacionColocarCarta(int posCarta, int indice)
    {
        yield return null;
        ColocarCartaCpu(posCarta, indice);
        juego.ReproducirEfectoSeleccionar();
    }
    public void ColocarCartaCpu(int posCarta, int indice)
    {

        if (campoCpu[indice] == null && controles.GetListaDCartas().Count < 1 && validadorFusionCampoCpu.Count < 1)
        {

            StartCoroutine(AnimacionColocarCartaCpu(posCarta, indice));
        }
        else
        {
            if (clonCpu[posCarta].GetComponent<carta>().GetTipoCarta().Equals("Monstruo") || controles.GetListaDCartas().Count > 1)
            {
                StartCoroutine(AnimacionColocarCartaDescarteCpu(posCarta, indice));
            }
            else
            {
                StartCoroutine(AnimacionDestruirEquipoCpu(posCarta, indice));
            }

        }

    }
    IEnumerator AnimacionDestruirEquipoCpu(int posCarta, int indice)
    {
        if (intefaz.ObtenerEsMt())
        {
            intefaz.UbicrMTCpu("acabar");
            intefaz.MoverapuntadorporCamanra(false);
        }
        pos = posCarta;
        //yield return AnimacionDevolverCamara(true, false);
        yield return new WaitForSeconds(0.2f);
        //intefaz.ReiniciarApuntador();
        Vector3[] destino = new Vector3[2];
        destino[0] = new Vector3(-3.2021f, 1.51f, -4.89f);
        destino[1] = new Vector3(173f, 208.9f, 0f);
        int realizada = 0;


        campoCpu[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        campoCpu[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);


        clonCpu[pos].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        clonCpu[pos].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu(pos));
        clonCpu[pos].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetManoCpu(pos));
        clonCpu[pos].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
        clonCpu[pos].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        campoCpu[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoCpu[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        while (realizada < 2)
        {


            realizada = 0;
            campoCpu[indice].transform.localPosition = Vector3.MoveTowards(campoCpu[indice].transform.localPosition, destino[0], Time.deltaTime * 1900);
            clonCpu[pos].transform.localPosition = Vector3.MoveTowards(clonCpu[pos].transform.localPosition, destino[1], Time.deltaTime * 1900);


            if (Vector3.Distance(campoCpu[indice].transform.localPosition, destino[0]) < 1)
            {
                realizada++;
            }
            if (Vector3.Distance(clonCpu[pos].transform.localPosition, destino[1]) < 1)
            {
                realizada++;
            }



            yield return null;
        }
        campoCpu[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);

        while (clonCpu[pos].transform.localPosition.x >= -303f)
        {

            clonCpu[pos].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        juego.ReproducirNoFusion();
        while (campoCpu[indice].transform.localPosition.x >= -6)
        {
            campoCpu[indice].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }

        Object.Destroy(campoCpu[indice]);
        campoCpu[indice] = null;
        yield return new WaitForSeconds(0.3f);


        clonCpu[pos].transform.localScale = new Vector3(2.5f, 2.5f, 0f);
        float reductor = 3f;
        while (reductor > 0)
        {
            clonCpu[pos].transform.localScale = new Vector3(2.5f, reductor, 0f);
            reductor -= 0.1f;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        Object.Destroy(clonCpu[pos]);
        clonCpu[pos] = null;
        campo.SetManoCpu(pos, 0);
        camara.MoverCamara(false);
        intefaz.datosCarta.SetActive(true);
        while (intefaz.datosCarta.transform.position.y < 44.1f)
        {
            float posicionar = 600 * Time.deltaTime;

            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        yield return new WaitForSeconds(0.2f);
        //intefaz.ActualizarUi(indice);
        campo.SetZonasMT(indice - 5, 0);
        cuadroUsuario.CuadrosPosBatalla();
        juego.SetPrimerAtaque(false);
        intefaz.ReiniciarApuntadorCpu();
        intefaz.ActualizarUICpuCampo(0);
        intefaz.SetEstadoApuntador(true);
        juego.InicioLogicaCpu();

    }

    IEnumerator AnimacionAumentoCampoCpu(int indice, int fase)
    {

        float rotar = -45f;
        bool realizada = false;
        bool animacion1 = false;

        while (!realizada)
        {

            float rotacion = rotar * 45 * Time.deltaTime;
            campoCpu[indice].transform.Rotate(0f, rotacion, 0f);
            if (campoCpu[indice].transform.eulerAngles.y > 270 && animacion1 == false)
            {
                campoCpu[indice].GetComponent<Transform>().eulerAngles = new Vector3(180f, 270f, 0f);
                if (fase == 1)
                {
                    campoCpu[indice].GetComponent<muestraCarta>().imagenMiniCarta.gameObject.SetActive(false);
                }
                else
                {
                    campoCpu[indice].GetComponent<muestraCarta>().imagenMiniCarta.gameObject.SetActive(true);
                }

                animacion1 = true;


            }

            if (campoCpu[indice].transform.eulerAngles.y > 180 && animacion1 == true)
            {

                campoCpu[indice].GetComponent<Transform>().eulerAngles = new Vector3(180f, 0f, 0f);
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator AnimacionColocarCartaDescarteCpu(int posCarta, int indice)
    {
        if (intefaz.ObtenerEsMt())
        {
            intefaz.UbicrMTCpu("acabar");
            intefaz.MoverapuntadorporCamanra(false);
        }
        pos = posCarta;
        camara.DevolverCamara(false);
        List<int> temp = controles.GetListaDCartas();
        if (controles.GetListaDCartas().Count < 1)
        {
            yield return AnimacionDevolverCamara(false, false);

        }
        else
        {
            yield return AnimacionDevolverCamara(false, true);
        }
        if (controles.GetListaDCartas().Count < 1)
        {
            Vector3[] destino = new Vector3[2];
            destino[0] = new Vector3(-3.2021f, 1.51f, -4.89f);
            destino[1] = new Vector3(303f, 208.9f, 0f);
            int realizada = 0;
            if (campoCpu[indice].GetComponent<carta>().GetDatosCarta() == 0)
            {
                campoCpu[indice].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(indice));
                campoCpu[indice].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoCpu(indice));
                campoCpu[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            }
            if (clonCpu[pos].GetComponent<carta>().GetDatosCarta() == 0)
            {
                clonCpu[pos].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu(pos));
                clonCpu[pos].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetManoCpu(pos));
                clonCpu[pos].GetComponent<muestraCarta>().ataque.text = "" + clonCpu[pos].GetComponent<carta>().getAtaque();
                clonCpu[pos].GetComponent<muestraCarta>().defensa.text = "" + clonCpu[pos].GetComponent<carta>().getDefensa();
                clonCpu[pos].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);

            }
            clonCpu[pos].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            campoCpu[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            campoCpu[indice].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
            while (realizada < 2)
            {


                realizada = 0;
                campoCpu[indice].transform.localPosition = Vector3.MoveTowards(campoCpu[indice].transform.localPosition, destino[0], Time.deltaTime * 190);
                clonCpu[pos].transform.localPosition = Vector3.MoveTowards(clonCpu[pos].transform.localPosition, destino[1], Time.deltaTime * 1900);


                if (Vector3.Distance(campoCpu[indice].transform.localPosition, destino[0]) < 1)
                {
                    realizada++;
                }
                if (Vector3.Distance(clonCpu[pos].transform.localPosition, destino[1]) < 1)
                {
                    realizada++;
                }



                yield return null;
            }
            while (clonCpu[pos].transform.localPosition.x >= -303f)
            {

                clonCpu[pos].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                yield return null;
            }
            clonCpu[pos].transform.localPosition = new Vector3(-303f, clonCpu[pos].transform.localPosition.y, clonCpu[pos].transform.localPosition.z);
            int fusionar = FusionConCampoCpu(indice);
            if (fusionar != 0)
            {
                yield return GetFusion(clonCpu[pos], pos, fusionar, campoCpu[indice], false);

            }
            else
            {
                juego.ReproducirNoFusion();
                while (campoCpu[indice].transform.localPosition.x >= -6)
                {
                    campoCpu[indice].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                    yield return null;
                }
                clonCpu[pos].GetComponent<carta>().SetDatosCarta(1);
                Object.Destroy(campoCpu[indice]);
                campoCpu[indice] = null;
                yield return new WaitForSeconds(0.3f);
            }



            clonCpu[pos].GetComponent<carta>().SetDatosCarta(1);
            juego.EfectosCartasCampo(juego.GetCampoModificado());
            StartCoroutine(AnimacionColocarCartaCpu(pos, indice));
        }
        else
        {
            float posicionX = -303f;

            intefaz.datosCarta.SetActive(false);

            Vector3 final = new Vector3(-3.2021f, 1.51f, -4.89f);




            Vector3[] destino = new Vector3[5];


            destino[0] = new Vector3(-303f, 208.9f, 0f);
            destino[1] = new Vector3(203f, 208.9f, 0f);
            destino[2] = new Vector3(233f, 208.9f, 0f);
            destino[3] = new Vector3(263f, 208.9f, 0f);
            destino[4] = new Vector3(293f, 208.9f, 0f);


            int realizada = 0;


            float posicionar = 0;
            for (int i = 0; i < 5; i++)
            {
                if (temp.Contains(i))
                {
                    clonCpu[i].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetManoCpu(i));
                    clonCpu[i].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetManoCpu(i));
                    if (!clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                    {
                        clonCpu[i].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
                        clonCpu[i].GetComponent<muestraCarta>().textoMT.text = Constants.EQUIP_NAME;
                        clonCpu[i].GetComponent<muestraCarta>().panelDatos.texture = clonCpu[i].GetComponent<muestraCarta>().color[1];
                    }
                    else
                    {
                        clonCpu[i].GetComponent<muestraCarta>().contenedorNormal.gameObject.SetActive(true);
                        clonCpu[i].GetComponent<muestraCarta>().ataque.text = "" + clonCpu[i].GetComponent<carta>().getAtaque();
                        clonCpu[i].GetComponent<muestraCarta>().defensa.text = "" + clonCpu[i].GetComponent<carta>().getDefensa();
                        clonCpu[i].GetComponent<muestraCarta>().panelDatos.texture = clonCpu[i].GetComponent<muestraCarta>().color[0];
                    }
                    clonCpu[i].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                }

            }

            while (realizada < temp.Count)
            {

                for (int i = 0; i < temp.Count; i++)
                {
                    realizada = 0;
                    clonCpu[temp[i]].transform.localPosition = Vector3.MoveTowards(clonCpu[temp[i]].transform.localPosition, destino[i], Time.deltaTime * 1900);

                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (Vector3.Distance(clonCpu[temp[j]].transform.localPosition, destino[j]) < 1)
                        {
                            realizada++;
                        }

                    }
                }
                yield return null;
            }

            clonCpu[temp[0]].transform.localPosition = destino[0];
            for (int i = 1; i < temp.Count; i++)
            {
                clonCpu[temp[i]].transform.localPosition = new Vector3(203f + posicionar, 208.9f, 0f);
                posicionar += 30f;
            }
            yield return null;
            for (int i = 1; i < temp.Count; i++)
            {
                while (clonCpu[temp[i]].transform.localPosition.x >= posicionX)
                {

                    clonCpu[temp[i]].transform.Translate(-2500f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                    yield return null;
                }
                if (clonCpu[temp[i]].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {

                    int aumento;
                    if (campo.GetManoCpu(temp[i]) == 639)
                    {
                        aumento = 1000;
                        reduccion += 1000;
                    }
                    else
                    {
                        aumento = 500;
                        reduccion += 500;
                    }
                    int cambiar = campo.GetManoCpu(temp[i - 1]);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(false);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(cambiar);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(cambiar);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(cambiar);
                    int ataqueconvertidor = clonCpu[temp[i - 1]].GetComponent<carta>().getAtaque();
                    int defConvertidor = clonCpu[temp[i - 1]].GetComponent<carta>().getDefensa();
                    string nombre = (string)txt.getnom().GetValue(cambiar);
                    int atributo1 = int.Parse((string)txt.GetAtributos1().GetValue(cambiar));
                    int atributo2 = int.Parse((string)txt.GetAtributos2().GetValue(cambiar));
                    int tipoMonstruo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(cambiar));
                    clonCpu[temp[i]].GetComponent<carta>().SetAtaque(ataqueconvertidor);
                    clonCpu[temp[i]].GetComponent<carta>().SetDefensa(defConvertidor);
                    clonCpu[temp[i]].GetComponent<carta>().SetName(nombre);
                    clonCpu[temp[i]].GetComponent<carta>().SetGuardianStarA(atributo1);
                    clonCpu[temp[i]].GetComponent<carta>().SetGuardianStar2(atributo2);
                    clonCpu[temp[i]].GetComponent<carta>().SetTipoAtributo(tipoMonstruo);
                    clonCpu[temp[i]].GetComponent<carta>().SetTieneBono(clonCpu[temp[i - 1]].GetComponent<carta>().GetTieneBono());
                    clonCpu[temp[i]].GetComponent<carta>().SetTieneBonoDesfavorable(clonCpu[temp[i - 1]].GetComponent<carta>().GetTieneBonoDesfavorable());
                    clonCpu[temp[i]].GetComponent<carta>().SetPos(clonCpu[temp[i - 1]].GetComponent<carta>().getPos());
                    clonCpu[temp[i]].GetComponent<carta>().SetTipoCarta("Monstruo");
                    clonCpu[temp[i]].GetComponent<muestraCarta>().ataque.text = "" + ataqueconvertidor;
                    clonCpu[temp[i]].GetComponent<muestraCarta>().defensa.text = "" + defConvertidor;


                    intefaz.ColorFlash();
                    intefaz.SetTiempoFlash(2f);
                    intefaz.SetFlash(true);
                    clonCpu[temp[i]].transform.localScale = new Vector3(2.6f, 2.6f, 0f);
                    StartCoroutine(AnimacionFusion(clonCpu[temp[i]]));

                    juego.ReproducirAumento();
                    campo.SetManoCpu(temp[i], campo.GetManoCpu(temp[i - 1]));
                    if (clonCpu[temp[i]].GetComponent<carta>().getAtaque() + aumento >= Constane || clonCpu[temp[i]].GetComponent<carta>().getDefensa() + aumento >= Constane)
                    {
                        if (clonCpu[temp[i]].GetComponent<carta>().getAtaque() + aumento >= Constane)
                        {
                            clonCpu[temp[i]].GetComponent<carta>().SetAtaque(Constane);
                        }
                        else
                        {
                            clonCpu[temp[i]].GetComponent<carta>().SetAtaque(clonCpu[temp[i]].GetComponent<carta>().getAtaque() + aumento);
                        }
                        if (clonCpu[temp[i]].GetComponent<carta>().getDefensa() + aumento >= Constane)
                        {
                            clonCpu[temp[i]].GetComponent<carta>().SetDefensa(Constane);
                        }
                        else
                        {
                            clonCpu[temp[i]].GetComponent<carta>().SetDefensa(clonCpu[temp[i]].GetComponent<carta>().getDefensa() + aumento);
                        }
                    }
                    else
                    {
                        clonCpu[temp[i]].GetComponent<carta>().SetAtaque(clonCpu[temp[i]].GetComponent<carta>().getAtaque() + aumento);
                        clonCpu[temp[i]].GetComponent<carta>().SetDefensa(clonCpu[temp[i]].GetComponent<carta>().getDefensa() + aumento);
                    }
                    clonCpu[temp[i]].GetComponent<muestraCarta>().ataque.text = "" + clonCpu[temp[i]].GetComponent<carta>().getAtaque();
                    clonCpu[temp[i]].GetComponent<muestraCarta>().defensa.text = "" + clonCpu[temp[i]].GetComponent<carta>().getDefensa();
                    clonCpu[temp[i]].GetComponent<muestraCarta>().ataqueB.text = atkText + clonCpu[temp[i]].GetComponent<carta>().getAtaque();
                    clonCpu[temp[i]].GetComponent<muestraCarta>().defensaB.text = defText + clonCpu[temp[i]].GetComponent<carta>().getDefensa();
                    clonCpu[temp[i]].GetComponent<muestraCarta>().nombreCarta.text = clonCpu[temp[i]].GetComponent<carta>().GetName();
                    clonCpu[temp[i]].GetComponent<muestraCarta>().panelDatos.texture = clonCpu[temp[i]].GetComponent<muestraCarta>().color[0];
                    Destroy(clonCpu[temp[i - 1]]);
                    clonCpu[temp[i - 1]] = null;
                    yield return new WaitForSeconds(1.5f);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
                    clonCpu[temp[i]].GetComponent<muestraCarta>().contenedorNormal.SetActive(true);
                    clonCpu[temp[i]].transform.localScale = new Vector3(1.4f, 1.4f, 0f);
                    StartCoroutine(AnimacionFusion(clonCpu[temp[i]]));
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    int fusionar = fusionCartaDesechaCpu(temp[i], temp[i - 1]);
                    if (fusionar != 0)
                    {
                        yield return GetFusion(clonCpu[temp[i]], temp[i], fusionar, clonCpu[temp[i - 1]], false);
                    }
                    else
                    {
                        juego.ReproducirNoFusion();
                        while (clonCpu[temp[i - 1]].transform.localPosition.x >= -500)
                        {
                            clonCpu[temp[i - 1]].transform.Translate(-600f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                            yield return null;

                        }
                        Object.Destroy(clonCpu[temp[i - 1]]);
                        clonCpu[temp[i - 1]] = null;
                        yield return new WaitForSeconds(0.3f);
                    }
                }



            }
            pos = temp[temp.Count - 1];
            clonCpu[pos].GetComponent<carta>().SetDatosCarta(1);
            StartCoroutine(AnimacionColocarCartaCpu(pos, indice));

        }


    }
   
    public float posicionesTableroCpu(int indice)
    {
        float[] x = new float[5];
        x[0] = -3.22f;
        x[1] = -1.6f;
        x[2] = 0.01f;
        x[3] = 1.6f;
        x[4] = 3.24f;
        if (campoCpu[indice] != null)
        {
            if (!campoCpu[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                indice -= 5;

            }
        }

        return x[indice];
    }
    IEnumerator AnimacionColocarCartaCpu(int posCarta, int indice)
    {
        //mover
        if (intefaz.ObtenerEsMt())
        {
            intefaz.UbicrMTCpu("acabar");
            intefaz.MoverapuntadorporCamanra(false);
        }

        pos = posCarta;
        Vector3 final = new Vector3(18f, 310f, 0f);
        while (Vector3.Distance(clonCpu[pos].transform.localPosition, final) > Time.deltaTime * 2000)
        {
            clonCpu[pos].transform.localPosition = Vector3.MoveTowards(clonCpu[pos].transform.localPosition, final, Time.deltaTime * 2000);
            yield return null;
        }
        clonCpu[pos].transform.localPosition = final;


        yield return new WaitForSeconds(0.2f);
        while (clonCpu[pos].transform.localPosition.y < 570)
        {

            clonCpu[pos].transform.Translate(0f * Time.deltaTime, 1200f * Time.deltaTime, 0f * Time.deltaTime);

            yield return null;
        }



        InstantiateCardsField(indice,clonCpu[pos]);
        int indiceCarta = indice;
        indiceCartaMT = indice;
        if (campoCpu[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            primerMT = false;
            juego.SetPrimerAtaque(indiceCarta);
            controles.SetIndiceAtaqueCpu(indiceCarta);
        }
        Object.Destroy(clonCpu[pos]);
        clonCpu[pos] = null;
        pos = indice;
        //escalar
        if (reduccion != 0)
        {
            int activador = 0;
            for (int i = 5; i < 10; i++)
            {
                if (getCartaCampoU(i) != null)
                {
                    if (campo.GetCampoUsuario(i) == 698)
                    {
                        activador = i;
                    }
                }
            }
            if (activador != 0)
            {
                juego.TrampasActiadas++;
                yield return StartCoroutine(ShowCardEffects(getCartaCampoU(activador), activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));
                EfectoReverseTrap(pos);
            }
        }
        reduccion = 0;

        //mover en x selectivo respecto al indice del campo(cuadro verde)
        float posicionarX = posicionesTableroCpu(pos);
        float posicionarZ = -1.5f;
        if (!campoCpu[pos].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            posicionarZ = -3.1f;
        }

        campoCpu[pos].transform.localPosition = new Vector3(posicionarX, 5f, posicionarZ);
        if (campoCpu[pos].GetComponent<carta>().getPos() == 1)
        {
            campoCpu[pos].transform.localScale = new Vector3(1.7f, 1.7f, 0.01f);
            campoCpu[pos].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);
        }
        else
        {
            campoCpu[pos].transform.localScale = new Vector3(1.4f, 1.8f, 0.01f);
            campoCpu[pos].GetComponent<Transform>().eulerAngles = new Vector3(90f, 90f, 0f);
        }

        //bajarCarta hasta 0,071
        Vector3 final1 = new Vector3(campoCpu[pos].transform.localPosition.x, 0.071f, campoCpu[pos].transform.localPosition.z);
        while (Vector3.Distance(campoCpu[pos].transform.localPosition, final1) > Time.deltaTime * 7)
        {
            campoCpu[pos].transform.localPosition = Vector3.MoveTowards(campoCpu[pos].transform.localPosition, final1, Time.deltaTime * 7);
            yield return null;
        }
        campoCpu[pos].transform.localPosition = new Vector3(posicionarX, 0.071f, posicionarZ);
        camara.MoverCamara(false);
        yield return AnimacionMoverCamara(false, true);
        intefaz.ActualizarUICpuCampo(pos);
        pos = posCarta;
        campo.SetCampoCpu(indice, campo.GetManoCpu(pos));
        if (controles.GetListaDCartas().Count > 0)
        {
            List<int> temp = controles.GetListaDCartas();
            for (int i = 0; i < temp.Count; i++)
            {
                campo.SetManoCpu(temp[i], 0);
            }
        }
        else
        {
            campo.SetManoCpu(pos, 0);
        }
        if (campoCpu[indice].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            campo.SetAtaquesCpu(indice, 1);
            controles.SetIndice(indice + 5);

        }
        else
        {

            juego.SetPrimerAtaque(false);
            primerMT = true;
            indiceMT = indice;
            campo.SetZonasMT(indice - 5, 1);
        }

        bool salir = false;
        for (int i = 5; i < 10 && salir == false; i++)
        {
            if (campoCpu[i] != null)
            {
                salir = true;
            }
        }
        if (salir == true)
        {
            intefaz.datosCartaCpu.SetActive(false);
            intefaz.SetEstadoApuntador(true);
            if (juego.GetPrimerAtaque())
            {
                yield return new WaitForSeconds(0.1f);
                juego.ReproducirEfectoMover();
                intefaz.MoverApuntadorAbajo();
            }
            InicioLogicaCpuMT();
        }
        else
        {


            intefaz.SetEstadoApuntador(true);
            juego.InicioLogicaCpu();
        }
        // StopAllCoroutines();



    }

    public void CambiarPosCartaCpu(int indice)
    {

        StartCoroutine(CambiarPosCpu(indice));

    }
    IEnumerator CambiarPosCpu(int indice)
    {
        //cambiar posicion

        int posCarta = indice;
        for (int i = 0; i < 90; i += 15)
        {
            yield return new WaitForSeconds(0.01f);
            if (campoCpu[posCarta].GetComponent<carta>().getPos() == 1)
            {
                campoCpu[posCarta].transform.Rotate(0f, 0f, 15f);
            }
            else
            {
                campoCpu[posCarta].transform.Rotate(0f, 0f, -15);
            }
        }
        if (campoCpu[posCarta].GetComponent<carta>().getPos() == 1)
        {
            campoCpu[posCarta].transform.localScale = new Vector3(1.4f, 1.8f, 0.1f);
            campoCpu[posCarta].GetComponent<carta>().SetPos(0);
        }
        else
        {

            campoCpu[posCarta].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);
            campoCpu[posCarta].GetComponent<carta>().SetPos(1);
        }
    }
    public GameObject GetCartaCpu(int indice)
    {
        return campoCpu[indice];
    }
    public void SetEstadoManos(bool estadoUsuario, bool estadoCpu)
    {
        for (int i = 0; i < 5; i++)
        {
            if (clon[i] != null)
            {
                clon[i].gameObject.SetActive(estadoUsuario);
            }
            if (clonCpu[i] != null)
            {
                clonCpu[i].gameObject.SetActive(estadoCpu);
            }
        }
    }
    public void OrganizarMano()
    {
        int puntero = 0;
        int pos = 0;


        while (clon[puntero] != null)
        {
            puntero++;
        }
        for (int i = puntero + 1; i < 5; i++)
        {

            if (clon[i - 1] == null)
            {
                int aux = i - 1;
                pos = aux;
                if (clon[pos] == null)
                {
                    while (clon[pos] == null && pos < 4)
                    {
                        pos++;
                    }
                }
                if (clon[pos] != null)
                {
                    clon[aux] = clon[pos];
                    campo.SetManoUsuario(aux, campo.GetManoUsuario(pos));
                    float posX = -303f;
                    posX = posX + ((i - 1) * (160.5f));
                    campo.SetManoUsuario(pos, 0);
                    clon[pos].transform.localPosition = new Vector3(posX, 77.95f, 0f);
                    clon[pos] = null;

                }




            }

        }


    }
    public void OrganizarManoCpu()
    {
        int puntero = 0;
        int pos = 0;


        while (clonCpu[puntero] != null)
        {
            puntero++;
        }
        for (int i = puntero + 1; i < 5; i++)
        {

            if (clonCpu[i - 1] == null)
            {
                int aux = i - 1;
                pos = aux;
                if (clonCpu[pos] == null)
                {
                    while (clonCpu[pos] == null && pos < 4)
                    {
                        pos++;
                    }
                }
                if (clonCpu[pos] != null)
                {
                    clonCpu[aux] = clonCpu[pos];
                    campo.SetManoCpu(aux, campo.GetManoCpu(pos));
                    float posX = -303f;
                    posX = posX + ((i - 1) * (160.5f));
                    campo.SetManoCpu(pos, 0);
                    clonCpu[pos].transform.localPosition = new Vector3(posX, 77.95f, 0f);
                    clonCpu[pos] = null;

                }




            }

        }


    }
    public void RetornarACampo(int cartaPos, int cartaCpuPos)
    {

        if (campoU[cartaPos] != null)
        {
            float posicionarX = posicionesTableroUsuario(cartaPos);
            if (juego.GetTurnoUsuario() == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (campoU[i] != null)
                    {
                        campoU[i].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            else
            {
                campoU[cartaPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
            }

            if (juego.GetTurnoUsuario() == true)
            {
                campoU[cartaPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
            }
            if (campoU[cartaPos].GetComponent<carta>().getPos() == 0)
            {
                campoU[cartaPos].transform.localScale = new Vector3(1.4f, 1.8f, 0.1f);
                if (juego.GetTurnoUsuario() == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (campoU[i] != null)
                        {
                            campoU[i].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 90, 0));
                        }
                    }
                }
                else
                {
                    campoU[cartaPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 90, 0));
                }


            }
            else
            {
                campoU[cartaPos].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);
            }
            campoU[cartaPos].transform.localPosition = new Vector3(posicionarX, 0.071f, 1.77f);
        }

        //cpu
        if (campoCpu[cartaCpuPos] != null)
        {

            float posicionarX = posicionesTableroCpu(cartaCpuPos);
            if (juego.GetTurnoUsuario() == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (campoCpu[i] != null)
                    {
                        campoCpu[i].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                    }
                }
            }
            else
            {
                campoCpu[cartaCpuPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
            }

            if (juego.GetTurnoUsuario() == false)
            {
                campoCpu[cartaCpuPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
            }

            if (campoCpu[cartaCpuPos].GetComponent<carta>().getPos() == 0)
            {
                campoCpu[cartaCpuPos].transform.localScale = new Vector3(1.4f, 1.8f, 0.1f);
                if (juego.GetTurnoUsuario() == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (campoCpu[i] != null)
                        {
                            campoCpu[i].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 90f);
                        }
                    }
                }
                else
                {
                    campoCpu[cartaCpuPos].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 90f);
                }

            }
            else
            {
                campoCpu[cartaCpuPos].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);
            }

            campoCpu[cartaCpuPos].transform.localPosition = new Vector3(posicionarX, 0.071f, -1.5f);
        }

    }
    public void SetCartaCampo(int indice)
    {
        campoU[indice] = null;
    }
    public void SetCartaCpuCampo(int indice)
    {
        campoCpu[indice] = null;
    }
    public void RetornarACampoUsuario(int cartaPos)
    {
        float posicionarX = posicionesTableroUsuario(cartaPos);

        campoU[cartaPos].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);

        campoU[cartaPos].GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));

        campoU[cartaPos].transform.localPosition = new Vector3(posicionarX, 0.071f, 1.77f);

    }
    public void RetornarACampoCpu(int cartaCpuPos)
    {

        campoCpu[cartaCpuPos].transform.localScale = new Vector3(1.7f, 1.7f, 0.1f);
        float posicionarX = posicionesTableroCpu(cartaCpuPos);
        campoCpu[cartaCpuPos].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);


        campoCpu[cartaCpuPos].transform.localPosition = new Vector3(posicionarX, 0.071f, -1.5f);

    }
    public void TiempoCambiarPos(int posCartaCpu)
    {

        if (campoCpu[posCartaCpu].GetComponent<carta>().getPos() == 1)
        {
            int ataqueOriginal = int.Parse((string)txt.getatk().GetValue(campo.GetCampoCpu(posCartaCpu)));
            if (ataqueOriginal < 3000)
            {
                CambiarPosCartaCpu(posCartaCpu);
            }
        }

        campoCpu[posCartaCpu].GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 01f);
        campoCpu[posCartaCpu].GetComponent<muestraCarta>().reverso.color = new Color(0.5f, 0.5f, 0.5f, 1f);



        campo.SetAtaquesCpu(posCartaCpu, 0);
        controles.SetIndiceAtaqueCpu(posCartaCpu);
        Invoke("VolverLogicaCpu", 0.2f);
    }
    public void VolverLogicaCpu()
    {

        juego.InicioLogicaCpu();
    }
    public void DescartaUsuario(int indice, bool cancela)
    {
        if (cancela == false)
        {
            clon[indice].transform.Translate(0f, 10f, 0f);
        }
        else
        {
            clon[indice].transform.Translate(0f, -10f, 0f);
        }
    }
    public int FusionConCampo(int indice)
    {
        string carta1 = "(" + campo.GetCampoUsuario(indice) + ")";
        string carta2 = "(" + campo.GetManoUsuario(pos) + ")";
        string[] fusion = txt.GetFusion();
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;
        int destino = 0;
        if (campoU[indice] != null)
        {
            for (int i = 0; i < fusion.Length && salir == false; i++)
            {
                if (fusion[i].Contains(carta1))
                {
                    if (fusionRequerida[i].Contains(carta2))
                    {
                        destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                        salir = true;
                    }
                }
                if (fusion[i].Contains(carta2))
                {
                    if (fusionRequerida[i].Contains(carta1))
                    {
                        destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                        salir = true;
                    }
                }
            }
        }

        return destino;

    }
    public int fusionCartaDesecha(int indice1, int indice2, bool indiceCampo)
    {
        string carta1 = "(" + campo.GetManoUsuario(indice1) + ")";
        if (indiceCampo == true)
        {
            carta1 = "(" + campo.GetCampoUsuario(indice1) + ")";
        }
        string carta2 = "(" + campo.GetManoUsuario(indice2) + ")";
        int destino = 0;
        string[] fusion = txt.GetFusion();
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;

        for (int i = 0; i < fusion.Length && salir == false; i++)
        {
            if (fusion[i].Contains(carta1))
            {
                if (fusionRequerida[i].Contains(carta2))
                {
                    destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                    salir = true;
                }
            }
            if (fusion[i].Contains(carta2))
            {
                if (fusionRequerida[i].Contains(carta1))
                {
                    destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                    salir = true;
                }
            }
        }
        return destino;


    }
    //METODOS DE FUSION ,CARTAS MAGICAS ,TRAMPA Y CAMPO CPU
    public void ValidarSiFusion()
    {

        int destino = 0;
        string[] fusion = txt.GetFusion();
        List<string> defensasAltas = new List<string>();
        defensasAltas.Add("(45)");
        defensasAltas.Add("(47)");
        defensasAltas.Add("(101)");
        defensasAltas.Add("(105)");
        defensasAltas.Add("(292)");
        defensasAltas.Add("(142)");
        defensasAltas.Add("(105)");
        defensasAltas.Add("(301)");
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;
        bool hayFusion = false;
        for (int i = 0; i < 5 && salir == false; i++)
        {
            string carta1 = "(" + destino + ")";
            if (hayFusion == false)
            {

                carta1 = "(" + campo.GetManoCpu(i) + ")";
            }


            for (int j = 0; j < fusion.Length && salir == false; j++)
            {
                if (fusion[j].Contains(carta1) && !defensasAltas.Contains(carta1))
                {
                    for (int k = 0; k < 5 && salir == false; k++)
                    {

                        string carta2 = "(" + campo.GetManoCpu(k) + ")";
                        if (hayFusion == false)
                        {
                            if (fusionRequerida[j].Contains(carta2) && k != i && !defensasAltas.Contains(carta2))
                            {

                                controles.GetListaDCartas().Add(i);
                                controles.GetListaDCartas().Add(k);
                                destino = int.Parse((string)txt.GetDestinoFusion().GetValue(j));
                                juego.SetAtaqueFinalCpu(int.Parse((string)txt.getatk().GetValue(destino)));
                                juego.SetDefensaFinalCpu(int.Parse((string)txt.getdef().GetValue(destino)));
                                carta1 = "(" + destino + ")";
                                j = 0;
                                hayFusion = true;


                            }
                        }
                        else
                        {

                            if (fusion[j].Contains(carta1))
                            {
                                if (fusionRequerida[j].Contains(carta2) && !(controles.GetListaDCartas().Contains(k)) && !defensasAltas.Contains(carta2))
                                {

                                    controles.GetListaDCartas().Add(k);
                                    destino = int.Parse((string)txt.GetDestinoFusion().GetValue(j));
                                    juego.SetAtaqueFinalCpu(int.Parse((string)txt.getatk().GetValue(destino)));
                                    juego.SetDefensaFinalCpu(int.Parse((string)txt.getdef().GetValue(destino)));
                                    salir = true;

                                }
                            }
                        }
                    }
                }
            }
        }
        validarEquipoManoCpu(destino);


    }
    public void validarEquipoManoCpu(int destino)
    {
        string[] equipo = txt.GetEquipos();
        bool salir = false;
        if (controles.GetListaDCartas().Count > 0)
        {

            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    string carta1 = "(" + campo.GetManoCpu(i) + ")";
                    if (campo.GetManoCpu(i) == 639 || campo.GetManoCpu(i) == 640 || campo.GetManoCpu(i) == 721)
                    {
                        controles.GetListaDCartas().Add(i);
                        juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                        juego.SetDefensaFinalCpu(juego.GetDefensaFinalCpu() + 500);

                        salir = true;
                    }
                    else
                    {
                        int carta2 = destino;

                        if (equipo[carta2].Contains(carta1))
                        {
                            controles.GetListaDCartas().Add(i);
                            juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                            juego.SetDefensaFinalCpu(juego.GetDefensaFinalCpu() + 500);
                            salir = true;
                        }
                        else if (equipo[carta2].Contains("(all)"))
                        {
                            controles.GetListaDCartas().Add(i);
                            juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                            juego.SetDefensaFinalCpu(juego.GetDefensaFinalCpu() + 500);
                            salir = true;
                        }
                    }
                }

            }
        }
        else
        {
            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    string carta1 = "(" + campo.GetManoCpu(i) + ")";
                    for (int j = 0; j < 5 && salir == false; j++)
                    {
                        if (clonCpu[j].GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                        {
                            if (campo.GetManoCpu(i) == 639 || campo.GetManoCpu(i) == 640)
                            {
                                controles.GetListaDCartas().Add(j);
                                controles.GetListaDCartas().Add(i);
                                juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                                salir = true;
                            }
                            else
                            {


                                int carta2 = campo.GetManoCpu(j);
                                if (equipo[carta2].Contains(carta1))
                                {
                                    controles.GetListaDCartas().Add(j);
                                    controles.GetListaDCartas().Add(i);
                                    juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                                    salir = true;
                                }
                                else if (equipo[carta2].Contains("(all)"))
                                {
                                    controles.GetListaDCartas().Add(j);
                                    controles.GetListaDCartas().Add(i);
                                    juego.SetAtaqueFinalCpu(juego.GetAtaqueFinalCpu() + 500);
                                    salir = true;
                                }
                            }

                        }
                    }
                }


            }
        }

    }
    public int FusionConCampoCpu(int indice)
    {
        string carta1 = "(" + campo.GetCampoCpu(indice) + ")";
        string carta2 = "(" + campo.GetManoCpu(pos) + ")";
        string[] fusion = txt.GetFusion();
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;
        int destino = 0;
        if (campoCpu[indice] != null)
        {
            for (int i = 0; i < fusion.Length && salir == false; i++)
            {
                if (fusion[i].Contains(carta1))
                {
                    if (fusionRequerida[i].Contains(carta2))
                    {
                        destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                        salir = true;
                    }
                }
                if (fusion[i].Contains(carta2))
                {
                    if (fusionRequerida[i].Contains(carta1))
                    {
                        destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                        salir = true;
                    }
                }
            }
        }

        return destino;

    }
    public int fusionCartaDesechaCpu(int indice1, int indice2)
    {
        string carta1 = "(" + campo.GetManoCpu(indice1) + ")";
        string carta2 = "(" + campo.GetManoCpu(indice2) + ")";
        int destino = 0;
        string[] fusion = txt.GetFusion();
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;

        for (int i = 0; i < fusion.Length && salir == false; i++)
        {
            if (fusion[i].Contains(carta1))
            {
                if (fusionRequerida[i].Contains(carta2))
                {
                    destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                    salir = true;
                }
            }
            if (fusion[i].Contains(carta2))
            {
                if (fusionRequerida[i].Contains(carta1))
                {

                    destino = int.Parse((string)txt.GetDestinoFusion().GetValue(i));
                    salir = true;
                }
            }
        }
        return destino;


    }
    public bool ValidarSiFusionCampoCpu()
    {
        //campo.SetManoCpu(0, 28);
        //campo.SetManoCpu(3, 32);
        //campo.SetManoCpu(4, 32);
        //campo.SetCampoCpu(0, 22);
        string[] fusion = txt.GetFusion();
        List<string> defensasAltas = new List<string>();
        defensasAltas.Add("(45)");
        defensasAltas.Add("(47)");
        defensasAltas.Add("(101)");
        defensasAltas.Add("(105)");
        defensasAltas.Add("(292)");
        defensasAltas.Add("(142)");
        defensasAltas.Add("(105)");
        defensasAltas.Add("(301)");
        string[] fusionRequerida = txt.GetFusionR();
        bool salir = false;
        for (int i = 0; i < 5 && salir == false; i++)
        {


            string carta1 = "(" + campo.GetManoCpu(i) + ")";


            for (int j = 0; j < fusion.Length && salir == false; j++)
            {
                if (fusion[j].Contains(carta1) && !defensasAltas.Contains(carta1))
                {
                    for (int k = 0; k < 5 && salir == false; k++)
                    {

                        string carta2 = "(" + campo.GetCampoCpu(k) + ")";


                        if (fusionRequerida[j].Contains(carta2) && !defensasAltas.Contains(carta2))
                        {
                            salir = true;
                            int ataqueFusion = int.Parse((string)txt.getatk().GetValue(int.Parse((string)txt.GetDestinoFusion().GetValue(j))));
                            int ataqueCampo = int.Parse((string)txt.getatk().GetValue(campo.GetCampoCpu(k)));
                            if (ataqueCampo < ataqueFusion)
                            {
                                validadorFusionCampoCpu.Add(i);
                                validadorFusionCampoCpu.Add(k);
                                validadorFusionCampoCpu.Add(int.Parse((string)txt.GetDestinoFusion().GetValue(j)));
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }


                }
            }
        }
        return false;

    }
    public void LimpiarLista()
    {
        validadorFusionCampoCpu.Clear();
    }

    //efectos cartas magicas y de trmpa
    public void EfectosCartasUsuarioMano(int indice)
    {
        juego.ReproducirActivacion();
        StartCoroutine(AnimacionCartasMagicasTrampa(indice));

    }
    IEnumerator AnimacionCartasMagicasTrampa(int indice)
    {
        intefaz.DesactivarDatosUI();
        intefaz.datosCartaCpu.SetActive(false);
        intefaz.SetEstadoApuntador(false);
        bool isFieldCard = true;
        int activador = 0;
        while (intefaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            for (int i = 0; i < 5; i++)
            {
                if (juego.GetTurnoUsuario())
                {
                    if (i != indice && clon[i] != null)
                        clon[i].transform.Translate(0f, posicionar, 0f);
                }
                else
                {
                    if (i != indice && clonCpu[i] != null)
                        clonCpu[i].transform.Translate(0f, posicionar, 0f);
                }

            }

            yield return null;
        }
        if (juego.GetTurnoUsuario())
        {
            InactivateComponent(clon, indice);
        }
        else
        {
            InactivateComponent(clonCpu, indice);
        }

        intefaz.datosCarta.SetActive(false);
        int efectoMagia = 0;
        int campoCambiar = 0;
        string efectoDe ;
        GameObject obtenedor ;
        int cartaCampo ;
        if (controles.Getfase().Equals("efectoMano"))
        {
            if (clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Campo") || clon[indice].GetComponent<carta>().GetTipoCarta().Equals("Magica"))
            {
                juego.MagicasUsadas++;
            }

            cartaCampo = campo.GetManoUsuario(indice);
            obtenedor = clon[indice];
            isFieldCard = false;
            efectoDe = "usuario";
        }
        else if (controles.Getfase().Equals("efectoCampo"))
        {
            if (campoU[indice].GetComponent<carta>().GetTipoCarta().Equals("Campo") || campoU[indice].GetComponent<carta>().GetTipoCarta().Equals("Magica"))
            {
                juego.MagicasUsadas++;
            }
            intefaz.SetEstadoApuntador(false);
            cartaCampo = campo.GetCampoUsuario(indice);
            obtenedor = campoU[indice];
            yield return new WaitForSeconds(0.4f);
            campoU[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
            efectoDe = "usuario";

        }
        else
        {
            cartaCampo = campo.GetManoCpu(indice);
            obtenedor = clonCpu[indice];
            isFieldCard = false;
            efectoDe = "cpu";
        }

        if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Campo"))
        {
           campoCambiar =  GetFieldEffects(cartaCampo);
        }
        else if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Magica"))
        {

            int inicio = 638;
            bool salir = false;
            int numEfecto = 1;
            for (int i = 1; i < 84 && salir == false; i++)
            {
                if (cartaCampo == inicio)
                {
                    efectoMagia = numEfecto;
                    salir = true;
                }
                string tipoCarta = txt.GetTipoCarta().GetValue(inicio).ToString().Trim();
                if (tipoCarta.Equals("Magica"))
                {
                    numEfecto++;
                }
                inicio++;

            }



        }
        if (juego.GetTurnoUsuario() == true)
        {
            if (controles.Getfase().Equals("efectoCampo"))
            {
                obtenedor.transform.localPosition = new Vector3(0.03f, 2f, 5.42f);
            }
            if (!controles.Getfase().Equals("efectoCampo"))
            {
                obtenedor.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        obtenedor.transform.localScale = new Vector3(1.5f, 1.5f, 0.1f);
        yield return new WaitForSeconds(1f);
        int tiempo = 0;
        for (int i = 0; i < 180; i += 10)
        {
            yield return new WaitForSeconds(0.01f);
            obtenedor.transform.Rotate(0f, -10, 0f);
            tiempo += 10;
            if (tiempo == 90 || tiempo == -90)
            {
                obtenedor.transform.localScale = new Vector3(4f, 3f, 0.1f);

                obtenedor.GetComponent<muestraCarta>().specialContainer.gameObject.SetActive(true);
                string cardName = obtenedor.GetComponent<carta>().GetName();
                obtenedor.GetComponent<muestraCarta>().nombreCarta.text = cardName;
                obtenedor.GetComponent<muestraCarta>().nombreCarta.fontSize = GetFontCardName(cardName,isFieldCard);
                GetAttribute(obtenedor);
                if(obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Trampa")){
                    obtenedor.GetComponent<muestraCarta>().trapContainer.SetActive(true);
                }
                if (juego.GetTurnoUsuario() == true)
                {
                    if (controles.Getfase().Equals("efectoCampo"))
                    {
                        obtenedor.transform.eulerAngles = new Vector3(180, -90, 0);
                        obtenedor.GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetCampoUsuario(indice));
                    }
                    else
                    {
                        obtenedor.transform.eulerAngles = new Vector3(0, 90, 0);
                        obtenedor.GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetManoUsuario(indice));
                    }

                }
                else
                {
                    obtenedor.transform.eulerAngles = new Vector3(0, 90, 0);
                    obtenedor.GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetManoCpu(indice));
                }

                obtenedor.GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
            }
        }
        tiempo = 0;
        yield return new WaitForSeconds(0.5f);
        float reductor = 4f;
        while (reductor > 0)
        {
            obtenedor.transform.localScale = new Vector3(4f, reductor, 0.1f);
            reductor -= 0.1f;
            yield return null;
        }
        intefaz.ColorFlash();
        intefaz.SetFlash(true);
        yield return new WaitForSeconds(0.5f);

        if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Campo"))
        {
            if (juego.GetCampoModificado() != 6)
            {
                intefaz.ActualizarMaterialCampo(campoCambiar);
                juego.EfectosCartasCampo(campoCambiar);
            }
        }
        else if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Magica"))
        {
            if (juego.GetTurnoUsuario())
            {
                activador = juego.IniciarParametrosTrampaReverso(cartaCampo, 0);
            }
            else
            {
                activador = juego.IniciarParametrosTrampaReverso(0, cartaCampo);
            }
            if (activador == 0)
            {
                juego.EfectosCartasMagicas(efectoMagia, efectoDe);
            }



        }
        if (controles.Getfase().Equals("efectoMano"))
        {
            Object.Destroy(clon[indice]);
            clon[indice] = null;
            campo.SetManoUsuario(indice, 0);
        }
        else if (controles.Getfase().Equals("efectoCampo"))
        {
            Object.Destroy(campoU[indice]);
            campoU[indice] = null;
        }
        else
        {
            Object.Destroy(clonCpu[indice]);
            clonCpu[indice] = null;
            campo.SetManoCpu(indice, 0);
        }
        if (activador != 0)
        {
            if (juego.GetTurnoUsuario())
            {
                yield return StartCoroutine(ShowCardEffects(GetCartaCpu(activador),activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));
            }
            else
            {
                yield return StartCoroutine(ShowCardEffects(getCartaCampoU(activador), activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));

            }
            juego.EfectosTrampaReverso(cartaCampo);
        }


        if (!juego.GetFinJuego())
        {
            camara.MoverCamara(false);
            intefaz.datosCarta.SetActive(true);
            while (intefaz.datosCarta.transform.position.y < 44.1f)
            {
                float posicionar = 600 * Time.deltaTime;

                intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

                yield return null;
            }
            intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
            yield return new WaitForSeconds(0.2f);
            if (controles.Getfase().Equals("efectoMano"))
            {
                if (juego.GetVidaCpu() == 0)
                {
                    intefaz.DesactivarTodosComponentes();
                    camara.DevolverCamara(false);
                }
                else
                {
                    controles.SetIndice(0);
                    intefaz.ActivarDatosUI(0);
                    intefaz.SetEstadoApuntador(true);
                    yield return new WaitForSeconds(0.2f);
                    controles.SetFase("acabarTurno");
                }

            }
            else if (controles.Getfase().Equals("efectoCampo"))
            {
                if (juego.GetVidaCpu() == 0)
                {
                    cuadroUsuario.CuadrosPosBatalla();
                    intefaz.DesactivarTodosComponentes();
                    camara.DevolverCamara(false);
                }
                else
                {
                    intefaz.ActivarGuardianStar(true);
                    intefaz.ActivarDatosUI(indice);
                    yield return new WaitForSeconds(0.3f);
                    intefaz.SetEstadoApuntador(true);
                    controles.SetFase("acabarTurno");
                }

            }
            else
            {
                if (juego.GetVidaUsuario() == 0)
                {
                    cuadroUsuario.CuadrosPosBatalla();
                    intefaz.DesactivarTodosComponentes();
                    camara.DevolverCamara(false);
                }
                else
                {
                    cuadroUsuario.CuadrosPosBatalla();
                    juego.SetPrimerAtaque(false);
                    yield return new WaitForSeconds(0.2f);
                    intefaz.SetEstadoApuntador(true);
                    juego.InicioLogicaCpu();

                }

            }


        }
    }

    private int GetFieldEffects(int cartaCampo)
    {
        if (juego.GetCampoModificado() != 6)
        {
            for (int i = 0; i < 5; i++)
            {
                if (clon[i] != null)
                {
                    if (clon[i].GetComponent<carta>().GetTieneBono() == true)
                    {
                        clon[i].GetComponent<carta>().SetAtaque(clon[i].GetComponent<carta>().getAtaque() - 500);
                        clon[i].GetComponent<carta>().SetDefensa(clon[i].GetComponent<carta>().getDefensa() - 500);
                    }
                    if (clon[i].GetComponent<carta>().GetTieneBonoDesfavorable() == true)
                    {
                        clon[i].GetComponent<carta>().SetAtaque(clon[i].GetComponent<carta>().getAtaque() + 500);
                        clon[i].GetComponent<carta>().SetDefensa(clon[i].GetComponent<carta>().getDefensa() + 500);
                    }
                    clon[i].GetComponent<carta>().SetTieneBono(false);
                    clon[i].GetComponent<carta>().SetTieneBonoDesfavorable(false);

                }

                if (clonCpu[i] != null)
                {
                    if (clonCpu[i].GetComponent<carta>().GetTieneBono() == true)
                    {
                        clonCpu[i].GetComponent<carta>().SetAtaque(clonCpu[i].GetComponent<carta>().getAtaque() - 500);
                        clonCpu[i].GetComponent<carta>().SetDefensa(clonCpu[i].GetComponent<carta>().getDefensa() - 500);
                    }
                    if (clonCpu[i].GetComponent<carta>().GetTieneBonoDesfavorable() == true)
                    {
                        clonCpu[i].GetComponent<carta>().SetAtaque(clonCpu[i].GetComponent<carta>().getAtaque() + 500);
                        clonCpu[i].GetComponent<carta>().SetDefensa(clonCpu[i].GetComponent<carta>().getDefensa() + 500);
                    }
                    clonCpu[i].GetComponent<carta>().SetTieneBono(false);
                    clonCpu[i].GetComponent<carta>().SetTieneBonoDesfavorable(false);

                }
                if (campoU[i] != null)
                {
                    if (campoU[i].GetComponent<carta>().GetTieneBono() == true)
                    {
                        campoU[i].GetComponent<carta>().SetAtaque(campoU[i].GetComponent<carta>().getAtaque() - 500);
                        campoU[i].GetComponent<carta>().SetDefensa(campoU[i].GetComponent<carta>().getDefensa() - 500);
                    }
                    if (campoU[i].GetComponent<carta>().GetTieneBonoDesfavorable() == true)
                    {
                        campoU[i].GetComponent<carta>().SetAtaque(campoU[i].GetComponent<carta>().getAtaque() + 500);
                        campoU[i].GetComponent<carta>().SetDefensa(campoU[i].GetComponent<carta>().getDefensa() + 500);
                    }
                    campoU[i].GetComponent<carta>().SetTieneBono(false);
                    campoU[i].GetComponent<carta>().SetTieneBonoDesfavorable(false);

                }
                if (campoCpu[i] != null)
                {
                    if (campoCpu[i].GetComponent<carta>().GetTieneBono() == true)
                    {
                        campoCpu[i].GetComponent<carta>().SetAtaque(campoCpu[i].GetComponent<carta>().getAtaque() - 500);
                        campoCpu[i].GetComponent<carta>().SetDefensa(campoCpu[i].GetComponent<carta>().getDefensa() - 500);
                    }
                    if (campoCpu[i].GetComponent<carta>().GetTieneBonoDesfavorable() == true)
                    {
                        campoCpu[i].GetComponent<carta>().SetAtaque(campoCpu[i].GetComponent<carta>().getAtaque() + 500);
                        campoCpu[i].GetComponent<carta>().SetDefensa(campoCpu[i].GetComponent<carta>().getDefensa() + 500);
                    }
                    campoCpu[i].GetComponent<carta>().SetTieneBono(false);
                    campoCpu[i].GetComponent<carta>().SetTieneBonoDesfavorable(false);

                }
            }
        }

        int campoCambiar;
        if (cartaCampo == 632)
        {
            campoCambiar = 2;
            print("ahor cambio a montaña");
        }
        else if (cartaCampo == 633)
        {
            campoCambiar = 0;
            print("ahor cambio a agua");
        }
        else if (cartaCampo == 634)
        {
            campoCambiar = 3;
            print("ahor cambio a pradera");
        }
        else if ((cartaCampo == 635))
        {
            campoCambiar = 1;
            print("ahor cambio a oscuridad");
        }
        else if ((cartaCampo == 636))
        {
            campoCambiar = 5;
            print("ahor cambio a Yermo");
        }
        else
        {
            campoCambiar = 4;
            print("ahor cambio a bosque");
        }
        return campoCambiar;

    }
    public void Transormacion3(int indice)
    {
        vec = campoU[indice].transform.position;
        campoU[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        campoU[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
    }
    public void CancelarActivacion(int indice)
    {
        campoU[indice].transform.position = vec;
        campoU[indice].GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
        campoU[indice].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(false);
    }
    public int EquipoConCampo(int indice)
    {
        int aumento = 0;
        int refernciaAumento;
        int[] valoresAumento = { 400, 500, 600, 700, 800, 900, 1000 };
        int ataqueOriginal = int.Parse((string)txt.getatk().GetValue(campo.GetCampoUsuario(indice)));
        if (campo.GetManoUsuario(pos) == 639)
        {
            reduccion += 1000;
            aumento = 1000;
            juego.EquiposCorrectos++;
        }
        else if (campo.GetManoUsuario(pos) == 640)
        {
            reduccion += 500;
            aumento = 500;
            juego.EquiposCorrectos++;

        }
        else if (campo.GetManoUsuario(pos) == 721)
        {
            aumento = 0;
            juego.EquiposCorrectos++;
            for (int i = 0; i < 5; i++)
            {
                if (campoU[i] != null)
                {
                    aumento += 500;
                    reduccion += 500;
                }

            }

        }
        else if (campo.GetManoUsuario(pos) == 708 && ataqueOriginal <= 1000)
        {
            juego.EquiposCorrectos++;
            refernciaAumento = Random.Range(0, 6);
            aumento = valoresAumento[refernciaAumento];
            campoU[indice].GetComponent<carta>().esInmortal = true;

        }
        else
        {
            string carta1 = "(" + campo.GetManoUsuario(pos) + ")";
            int carta2 = campo.GetCampoUsuario(indice);
            string[] equipo = txt.GetEquipos();

            if (equipo[carta2].Contains(carta1))
            {
                aumento = 500;
                reduccion += 500;
                juego.EquiposCorrectos++;
            }
            else if (equipo[carta2].Contains("(all)"))
            {
                aumento = 500;
                reduccion += 500;
                juego.EquiposCorrectos++;
            }

        }

        return aumento;
    }
    public int EquipoDecarte(int indice, int indice2, bool campoIndice)
    {
        int aumento = 0;

        int refernciaAumento;
        int[] valoresAumento = { 400, 500, 600, 700, 800, 900, 1000 };

        int ataqueOriginal = int.Parse((string)txt.getatk().GetValue(campo.GetCampoUsuario(indice2)));
        if (campoIndice == false)
        {
            ataqueOriginal = int.Parse((string)txt.getatk().GetValue(campo.GetManoUsuario(indice2)));
        }

        if (campo.GetManoUsuario(indice) == 639)
        {
            aumento = 1000;
            reduccion += 1000;
            juego.EquiposCorrectos++;
        }
        else if (campo.GetManoUsuario(indice) == 640)
        {
            aumento = 500;
            reduccion += 500;
            juego.EquiposCorrectos++;
        }
        else if (campo.GetManoUsuario(indice) == 721)
        {
            aumento = 0;
            juego.EquiposCorrectos++;
            for (int i = 0; i < 5; i++)
            {
                if (campoU[i] != null)
                {
                    aumento += 500;
                    reduccion += 500;
                }

            }

        }
        else if (campo.GetManoUsuario(indice) == 708 && ataqueOriginal <= 1000)
        {
            refernciaAumento = Random.Range(0, 6);
            aumento = valoresAumento[refernciaAumento];
            juego.EquiposCorrectos++;

            if (!campoIndice)
            {
                clon[indice2].GetComponent<carta>().esInmortal = true;
            }
            else
            {
                campoU[indice2].GetComponent<carta>().esInmortal = true;
            }


        }
        else
        {
            int carta2 = campo.GetCampoUsuario(indice2);
            if (campoIndice == false)
            {
                carta2 = campo.GetManoUsuario(indice2);
            }
            string carta1 = "(" + campo.GetManoUsuario(indice) + ")";
            string[] equipo = txt.GetEquipos();

            if (equipo[carta2].Contains(carta1))
            {
                juego.EquiposCorrectos++;
                aumento = 500;
                reduccion += 500;
            }
            else if (equipo[carta2].Contains("(all)"))
            {
                juego.EquiposCorrectos++;
                aumento = 500;
                reduccion += 500;
            }
        }



        return aumento;
    }
    public int EquipoCampo(int indiceEquipo, int indiceMonstruo)
    {
        int aumento = 0;
        int refernciaAumento;
        int[] valoresAumento = { 400, 500, 600, 700, 800, 900, 1000 };
        int ataqueOriginal = int.Parse((string)txt.getatk().GetValue(campo.GetCampoUsuario(indiceMonstruo)));

        if (campo.GetCampoUsuario(indiceEquipo) == 639)
        {
            aumento = 1000;
            reduccion += 1000;
            juego.EquiposCorrectos++;
        }
        else if (campo.GetCampoUsuario(indiceEquipo) == 640)
        {
            aumento = 500;
            reduccion += 500;
            juego.EquiposCorrectos++;
        }
        else if (campo.GetCampoUsuario(indiceEquipo) == 721)
        {
            aumento = 0;
            juego.EquiposCorrectos++;
            for (int i = 0; i < 5; i++)
            {
                if (campoU[i] != null)
                {
                    aumento += 500;
                    reduccion += 500;
                }
            }
        }

        else if (campo.GetCampoUsuario(indiceEquipo) == 708 && ataqueOriginal <= 1000)
        {
            juego.EquiposCorrectos++;
            refernciaAumento = Random.Range(0, 6);
            aumento = valoresAumento[refernciaAumento];
            campoU[indiceMonstruo].GetComponent<carta>().esInmortal = true;

        }
        else
        {
            string carta1 = "(" + campo.GetCampoUsuario(indiceEquipo) + ")";
            int carta2 = campo.GetCampoUsuario(indiceMonstruo);
            string[] equipo = txt.GetEquipos();

            if (equipo[carta2].Contains(carta1))
            {
                juego.EquiposCorrectos++;
                aumento = 500;
                reduccion += 500;
            }
            else if (equipo[carta2].Contains("(all)"))
            {
                juego.EquiposCorrectos++;
                aumento = 500;
                reduccion += 500;
            }
        }

        return aumento;
    }
    public void Transformacion4(int indiceCampo)
    {
        campoU[indiceCampo].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        campoU[indiceCampo].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
    }
    public void CancelarActivacionEquipo(int indiceEquipo)
    {
        campoU[indiceEquipo].GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
        campoU[indiceEquipo].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(false);
    }
    public void ActivarEquipo(int indiceEquipo, int indiceMonstruo)
    {
        juego.ReproducirActivacion();
        StartCoroutine(animacionActivarEquipo(indiceEquipo, indiceMonstruo));
    }
    IEnumerator animacionActivarEquipo(int indiceEquipo, int indiceMonstruo)
    {
        intefaz.DesactivarDatosUI();
        camara.DevolverCamara(false);

        while (intefaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            yield return null;
        }
        intefaz.datosCarta.SetActive(false);
        yield return new WaitForSeconds(0.2f);


        Vector3[] destino = new Vector3[2];
        destino[0] = new Vector3(3.2021f, 1.51f, 4.89f);
        destino[1] = new Vector3(-5.2021f, 1.51f, 4.89f);
        int realizada = 0;
        if (campoU[indiceMonstruo].GetComponent<carta>().GetDatosCarta() == 0)
        {
            campoU[indiceMonstruo].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);

        }



        campoU[indiceMonstruo].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoU[indiceMonstruo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        campoU[indiceEquipo].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoU[indiceEquipo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        while (realizada < 2)
        {


            realizada = 0;
            campoU[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoU[indiceMonstruo].transform.localPosition, destino[0], Time.deltaTime * 30);
            campoU[indiceEquipo].transform.localPosition = Vector3.MoveTowards(campoU[indiceEquipo].transform.localPosition, destino[1], Time.deltaTime * 30);


            if (Vector3.Distance(campoU[indiceMonstruo].transform.localPosition, destino[0]) < 1)
            {
                realizada++;
            }
            if (Vector3.Distance(campoU[indiceEquipo].transform.localPosition, destino[1]) < 1)
            {
                realizada++;
            }



            yield return null;
        }

        campoU[indiceMonstruo].transform.localPosition = destino[0];
        campoU[indiceEquipo].transform.localPosition = destino[1];
        campoU[indiceEquipo].GetComponent<carta>().SetDatosCarta(1);
        campoU[indiceMonstruo].GetComponent<carta>().SetDatosCarta(1);
        while (campoU[indiceEquipo].transform.localPosition.x <= 3.2f)
        {

            campoU[indiceEquipo].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }

        int aumento = EquipoCampo(indiceEquipo, indiceMonstruo);

        if (aumento != 0)
        {
            StartCoroutine(AnimacionAumentoCampo(indiceMonstruo));
            campoU[indiceMonstruo].transform.localScale = new Vector3(3f, 2.1f, 0.01f);
            campoU[indiceMonstruo].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetCampoUsuario(indiceMonstruo));
            yield return GetUpgrade(campoU[indiceMonstruo], aumento, campoU[indiceEquipo],true);         
            StartCoroutine(AnimacionAumentoCampo(indiceMonstruo));
            campoU[indiceMonstruo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            juego.ReproducirNoFusion();
            while (campoU[indiceEquipo].transform.localPosition.x <= 6)
            {
                campoU[indiceEquipo].transform.Translate(15f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
                yield return null;
            }

            Object.Destroy(campoU[indiceEquipo]);
            campoU[indiceEquipo] = null;
            yield return new WaitForSeconds(0.3f);

        }




        Vector3 final = new Vector3(0.4f, 3f, 5f);
        while (Vector3.Distance(campoU[indiceMonstruo].transform.localPosition, final) > Time.deltaTime * 20)
        {
            campoU[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoU[indiceMonstruo].transform.localPosition, final, Time.deltaTime * 20);
            yield return null;
        }
        campoU[indiceMonstruo].transform.localPosition = final;
        yield return new WaitForSeconds(0.2f);
        while (campoU[indiceMonstruo].transform.localPosition.y < 5)
        {

            campoU[indiceMonstruo].transform.Translate(0f * Time.deltaTime, -6f * Time.deltaTime, 0f * Time.deltaTime);

            yield return null;
        }




        //escalar
        if (reduccion != 0)
        {
            int activador = 0;
            for (int i = 5; i < 10; i++)
            {
                if (GetCartaCpu(i) != null)
                {
                    if (campo.GetCampoCpu(i) == 698)
                    {
                        activador = i;
                    }
                }
            }
            if (activador != 0)
            {
                yield return StartCoroutine(ShowCardEffects(GetCartaCpu(activador),activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));
                EfectoReverseTrap(indiceMonstruo);
            }
        }

        reduccion = 0;
        //mover en x selectivo respecto al indice del campo(cuadro verde)
        float posicionarX = posicionesTableroUsuario(indiceMonstruo);


        float posicionarZ = 1.77f;

        campoU[indiceMonstruo].transform.localPosition = new Vector3(posicionarX, 5f, posicionarZ);
        if (campoU[indiceMonstruo].GetComponent<carta>().getPos() == 1)
        {
            campoU[indiceMonstruo].transform.localScale = new Vector3(1.7f, 1.7f, 0.01f);
            campoU[indiceMonstruo].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);

        }
        else
        {
            campoU[indiceMonstruo].transform.localScale = new Vector3(1.4f, 1.8f, 0.01f);
            campoU[indiceMonstruo].GetComponent<Transform>().eulerAngles = new Vector3(90f, -90f, 0f);
        }
        if (campoU[indiceMonstruo].GetComponent<carta>().esInmortal)
        {
            campoU[indiceMonstruo].GetComponent<muestraCarta>().panelDatos.texture = campoU[indiceMonstruo].GetComponent<muestraCarta>().color[3];
        }

        //bajarCarta hasta 0,071
        Vector3 final1 = new Vector3(campoU[indiceMonstruo].transform.localPosition.x, 0.071f, campoU[indiceMonstruo].transform.localPosition.z);
        while (Vector3.Distance(campoU[indiceMonstruo].transform.localPosition, final1) > Time.deltaTime * 7)
        {
            campoU[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoU[indiceMonstruo].transform.localPosition, final1, Time.deltaTime * 7);
            yield return null;
        }
        campoU[indiceMonstruo].transform.localPosition = new Vector3(posicionarX, 0.071f, posicionarZ);
        camara.MoverCamara(false);
        intefaz.datosCarta.SetActive(true);
        while (intefaz.datosCarta.transform.position.y < 44.1f)
        {
            float posicionar = 600 * Time.deltaTime;

            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        yield return new WaitForSeconds(0.2f);
        cuadroUsuario.CuadrosPosBatalla();
        intefaz.ActivarDatosUI(indiceMonstruo);
        controles.SetFase("acabarTurno");
        intefaz.SetEstadoApuntador(true);
        intefaz.ActivarDatosUI(indiceMonstruo);
    }
    public void InicioLogicaCpuMT()
    {

        bool hayCartaMT = false;

        if (!primerMT)
        {
            for (int i = 0; i < 5 && hayCartaMT == false; i++)
            {

                if (campo.GetZonasMT(i) == 1)
                {
                    hayCartaMT = true;
                    indiceMT = i + 5;


                }
            }
        }
        else
        {
            hayCartaMT = true;
            primerMT = false;
        }




        StartCoroutine(AnimacionEmpezarMTCpu(indiceMT, hayCartaMT));


    }
    IEnumerator AnimacionEmpezarMTCpu(int posCartaCpu, bool hayCartaMT)
    {

        if (hayCartaMT == true)
        {
            intefaz.SetEstadoApuntador(true);
            int indice = controles.GetIndice();
            int cuadro = indice;
            yield return new WaitForSeconds(0.1f);

            while (cuadro != posCartaCpu)
            {
                juego.ReproducirEfectoMover();
                if (posCartaCpu > indice)
                {
                    indice++;
                    cuadro++;
                    intefaz.MoverApuntadorDerecha();
                    intefaz.ActualizarUICpuCampo(indice);
                }
                else
                {
                    indice--;
                    cuadro--;
                    intefaz.MoverApuntadorIzquierda();
                    intefaz.ActualizarUICpuCampo(indice);
                }
                yield return new WaitForSeconds(0.09f);
            }

        }


        ValidarEfectosCartasCampoCpu(posCartaCpu, hayCartaMT);
    }
    //validar parametros de activacion cartas magicas de campo ,magicas ,equipo,y colocar trampas
    public int ValidarEfectosManoCpu(string efecto)
    {
        int carta = -1;
        bool salir = false;
        if (efecto.Equals("Campo"))
        {
            int campoCambiar = -1;
            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (campo.GetManoCpu(i) == 632)
                {
                    campoCambiar = 2;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }
                }
                else if (campo.GetManoCpu(i) == 633)
                {
                    campoCambiar = 0;
                    salir = true;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }
                }
                else if (campo.GetManoCpu(i) == 634)
                {
                    campoCambiar = 3;
                    salir = true;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }
                }
                else if (campo.GetManoCpu(i) == 635)
                {
                    campoCambiar = 1;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }
                }
                else if (campo.GetManoCpu(i) == 636)
                {
                    campoCambiar = 5;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }
                }
                else if (campo.GetManoCpu(i) == 637)
                {
                    campoCambiar = 4;
                    if (juego.GetCampoModificado() != campoCambiar)
                    {
                        carta = i;
                        salir = true;
                    }


                }


            }

        }
        else if (efecto.Equals("Magica"))
        {
            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Magica"))
                {
                    if (juego.ValidarParametrosActivacionMagicas(campo.GetManoCpu(i)) == true)
                    {
                        carta = i;
                        salir = true;
                    }
                }
            }
        }
        else if (efecto.Equals("Equipo"))
        {
            string[] equipo = txt.GetEquipos();

            for (int i = 0; i < 5; i++)
            {
                if (clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    if (campo.GetManoCpu(i) == 639)
                    {
                        carta = i;
                        return carta;
                    }
                    else if (campo.GetManoCpu(i) == 640)
                    {
                        carta = i;
                        return carta;
                    }
                    string carta1 = "(" + campo.GetManoCpu(i) + ")";
                    for (int j = 0; j < 5 && salir == false; j++)
                    {
                        int carta2 = campo.GetCampoCpu(j);
                        if (equipo[carta2].Contains(carta1))
                        {
                            carta = i;
                            return carta;
                        }
                        else if (equipo[carta2].Contains("(all)"))
                        {
                            carta = i;
                            return carta;
                        }

                    }
                }

            }


        }
        else if (efecto.Equals("Trampa"))
        {
            for (int i = 0; i < 5 && salir == false; i++)
            {
                if (clonCpu[i].GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                {

                    salir = true;
                    carta = i;
                }

            }
        }
        return carta;
    }
    public bool ValidarCartaCampoCpu(int carta)
    {
        int campoCambiar = -1;
        if (campo.GetCampoCpu(carta) == 632)
        {
            campoCambiar = 2;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }
        }
        else if (campo.GetCampoCpu(carta) == 633)
        {
            campoCambiar = 0;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }
        }
        else if (campo.GetCampoCpu(carta) == 634)
        {
            campoCambiar = 3;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }
        }
        else if (campo.GetCampoCpu(carta) == 635)
        {
            campoCambiar = 1;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }
        }
        else if (campo.GetCampoCpu(carta) == 636)
        {
            campoCambiar = 5;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }
        }
        else if (campo.GetCampoCpu(carta) == 637)
        {
            campoCambiar = 4;
            if (juego.GetCampoModificado() != campoCambiar)
            {
                return true;
            }


        }
        return false;
    }
    public void ValidarEfectosCartasCampoCpu(int carta, bool hayMT)
    {
        int cartaMT = 0;
        if (hayMT == true)
        {
            controles.SetIndice(carta);

            if (campoCpu[carta] != null)
            {
                if (campoCpu[carta].GetComponent<carta>().GetTipoCarta().Equals("Magica"))
                {
                    cartaMT = campo.GetCampoCpu(carta);

                    if (juego.ValidarParametrosActivacionMagicas(cartaMT) == true)
                    {
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(1f, 1f, 1f, 1f);
                        CartasMagicasTrampaCampoCpu(carta);
                        campo.SetZonasMT(carta - 5, 0);
                    }
                    else
                    {
                        campo.SetZonasMT(carta - 5, 0);
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        Invoke("InicioLogicaCpuMT", 0.09f);
                    }

                }
                else if (campoCpu[carta].GetComponent<carta>().GetTipoCarta().Equals("Campo"))
                {
                    if (ValidarCartaCampoCpu(carta) == true)
                    {
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(1f, 1f, 1f, 1f);
                        CartasMagicasTrampaCampoCpu(carta);
                        campo.SetZonasMT(carta - 5, 0);
                    }
                    else
                    {
                        campo.SetZonasMT(carta - 5, 0);
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        Invoke("InicioLogicaCpuMT", 0.09f);
                    }
                }
                else if (campoCpu[carta].GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    //validar si carta equipo le sirve a algun monstruo campo cpu
                    //if no implementado IMPLMENETAR
                    int equipado = ValidarCartasEquipoCpu(carta);
                    if (equipado != -1)
                    {
                        campo.SetZonasMT(carta - 5, 0);
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(1f, 1f, 1f, 1f);
                        StartCoroutine(AnimacionEmpezarEquiparCpu(carta, equipado));
                    }
                    else
                    {
                        campo.SetZonasMT(carta - 5, 0);
                        campoCpu[carta].GetComponent<muestraCarta>().reverso.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        Invoke("InicioLogicaCpuMT", 0.09f);
                    }

                }
                else
                {
                    campo.SetZonasMT(carta - 5, 0);
                    Invoke("InicioLogicaCpuMT", 0.09f);
                }

            }

        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(AnimacionEmpezarLogicaCpu());

        }


    }
    IEnumerator AnimacionEmpezarLogicaCpu()
    {
        bool salir = false;
        for (int i = 5; i < 10 && salir == false; i++)
        {
            if (campo.GetCampoCpu(i) != 0)
            {
                salir = true;
            }
        }
        if (salir == true)
        {
            salir = false;
            int cartaMonstruoCpu = 0;
            if (!juego.GetPrimerAtaque())
            {
                for (int i = 0; i < 5 && salir == false; i++)
                {
                    if (campoCpu[i] != null)
                    {
                        salir = true;
                        cartaMonstruoCpu = i;
                    }
                }
                controles.SetIndiceAtaqueCpu(cartaMonstruoCpu);
            }
            else
            {
                cartaMonstruoCpu = juego.GetIndiePrimerAtaque();
                salir = true;
            }

            if (salir == true)
            {
                intefaz.SetEstadoApuntador(true);
                int cuadro = indiceMT - 5;
                juego.ReproducirEfectoMover();
                intefaz.MoverApuntadorArriba();
                intefaz.ActualizarUICpuCampo(cuadro);
                yield return new WaitForSeconds(0.3f);
                while (cuadro != cartaMonstruoCpu)
                {
                    juego.ReproducirEfectoMover();
                    if (cartaMonstruoCpu > cuadro)
                    {

                        cuadro++;
                        intefaz.MoverApuntadorDerecha();
                        intefaz.ActualizarUICpuCampo(cuadro);
                    }
                    else
                    {

                        cuadro--;
                        intefaz.MoverApuntadorIzquierda();
                        intefaz.ActualizarUICpuCampo(cuadro);
                    }
                    yield return new WaitForSeconds(0.3f);
                }
            }


        }
        juego.InicioLogicaCpu();

    }
    public void CartasMagicasTrampaCampoCpu(int indice)

    {
        juego.ReproducirActivacion();
        StartCoroutine(AnimacionCartasMagicasTrampaCampoCpu(indice));
    }
    IEnumerator AnimacionCartasMagicasTrampaCampoCpu(int indice)
    {



        int activador = 0;
        int efectoMagia = 0;
        int campoCambiar = 0;
        string efectoDe = "";
        GameObject obtenedor = null;
        int cartaCampo = 0;
        cartaCampo = campo.GetCampoCpu(indice);
        obtenedor = campoCpu[indice];

        obtenedor.GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(indice));
        obtenedor.GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoCpu(indice));
        obtenedor.GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        obtenedor.GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        intefaz.SetEstadoApuntador(false);
        camara.DevolverCamara(true);
        while (intefaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            for (int i = 0; i < 5; i++)
            {
                if (i != indice && clonCpu[i] != null)
                    clonCpu[i].transform.Translate(0f, posicionar, 0f);
            }

            yield return null;
        }

        efectoDe = "cpu";

        obtenedor.transform.localScale = new Vector3(1.5f, 1.5f, 0.1f);
        campoCpu[indice].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        obtenedor.transform.localPosition = new Vector3(0.03f, 2f, -5.42f);
        string cardName = obtenedor.GetComponent<carta>().GetName();
        obtenedor.GetComponent<muestraCarta>().nombreCarta.text = cardName;
        obtenedor.GetComponent<muestraCarta>().nombreCarta.fontSize = GetFontCardName(cardName,true);
        int tiempo = 0;
        for (int i = 0; i < 180; i += 10)
        {
            yield return new WaitForSeconds(0.01f);
            obtenedor.transform.Rotate(0f, -10, 0f);
            tiempo += 10;
            if (tiempo == 90 || tiempo == -90)
            {
                obtenedor.transform.localScale = new Vector3(4f, 3f, 0.1f);
                obtenedor.transform.eulerAngles = new Vector3(180, -90, 0);
                obtenedor.GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetCampoCpu(indice));
                obtenedor.GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
                obtenedor.GetComponent<muestraCarta>().specialContainer.SetActive(true);
                if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                {
                    obtenedor.GetComponent<muestraCarta>().trapContainer.SetActive(true);
                }
                GetAttribute(obtenedor);
            }
        }
        tiempo = 0;

        yield return new WaitForSeconds(0.5f);
        if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Campo"))
        {
           campoCambiar= GetFieldEffects(cartaCampo);

        }
        else if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Magica"))
        {

            int inicio = 638;
            bool salir = false;
            int numEfecto = 1;
            for (int i = 1; i < 84 && salir == false; i++)
            {
                if (cartaCampo == inicio)
                {
                    efectoMagia = numEfecto;
                    salir = true;
                }
                string tipoCarta = txt.GetTipoCarta().GetValue(inicio).ToString().Trim();
                if (tipoCarta.Equals("Magica"))
                {
                    numEfecto++;
                }
                inicio++;

            }


        }
        obtenedor.transform.localScale = new Vector3(4f, 3f, 0.1f);
        float reductor = 3f;
        while (reductor > 0)
        {
            obtenedor.transform.localScale = new Vector3(4f, reductor, 0.1f);
            reductor -= 0.1f;
            yield return null;
        }
        intefaz.ColorFlash();
        intefaz.SetFlash(true);
        yield return new WaitForSeconds(0.5f);

        if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Campo"))
        {
            if (juego.GetCampoModificado() != 6)
            {
                intefaz.ActualizarMaterialCampo(campoCambiar);
                juego.EfectosCartasCampo(campoCambiar);
            }
            yield return new WaitForSeconds(0.5f);
        }
        else if (obtenedor.GetComponent<carta>().GetTipoCarta().Equals("Magica"))
        {

            if (juego.GetTurnoUsuario())
            {
                activador = juego.IniciarParametrosTrampaReverso(cartaCampo, 0);
            }
            else
            {
                activador = juego.IniciarParametrosTrampaReverso(0, cartaCampo);
            }
            if (activador == 0)
            {
                juego.EfectosCartasMagicas(efectoMagia, efectoDe);
            }
        }
        Object.Destroy(campoCpu[indice]);
        campoCpu[indice] = null;
        campo.SetCampoCpu(indice, 0);
        if (activador != 0)
        {
            yield return StartCoroutine(ShowCardEffects(getCartaCampoU(activador), activador, new Vector3(0.03f, 2f, -5.42f), Quaternion.Euler(180f, 0f, 0f)));
            juego.EfectosTrampaReverso(cartaCampo);
        }
        if (!juego.GetFinJuego())
        {


            camara.MoverCamara(false);
            intefaz.datosCarta.SetActive(true);
            while (intefaz.datosCarta.transform.position.y < 44.1f)
            {
                float posicionar = 600 * Time.deltaTime;

                intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

                yield return null;
            }
            intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
            yield return new WaitForSeconds(0.2f);
            if (juego.GetVidaUsuario() == 0)
            {
                cuadroUsuario.CuadrosPosBatalla();
                intefaz.DesactivarTodosComponentes();
                camara.DevolverCamara(true);
            }
            else
            {
                intefaz.ActualizarUICpuCampo(0);
                cuadroUsuario.CuadrosPosBatalla();
                juego.InicioLogicaCpu();
            }
        }

        // StopAllCoroutines();
    }
    public int ValidarCartasEquipoCpu(int carta)
    {
        string[] equipo = txt.GetEquipos();

        string carta1 = "(" + campo.GetCampoCpu(carta) + ")";
        bool salir = false;
        for (int i = 0; i < 5 && salir == false; i++)
        {

            if (campoCpu[i] != null)
            {
                if (campo.GetCampoCpu(carta) == 639 || campo.GetCampoCpu(carta) == 640 || campo.GetCampoCpu(carta) == 721)
                {
                    return i;
                }
                else if (campo.GetCampoCpu(carta) == 708)
                {
                   
                    int carta3 = campo.GetCampoCpu(i);
                    int ataqueOriginal = int.Parse((string)txt.getatk().GetValue(carta3));
                    if (ataqueOriginal <= 1000)
                    {
                        return i;
                    }

                }
                int carta2 = campo.GetCampoCpu(i);
                if (equipo[carta2].Contains(carta1))
                {
                    return i;
                }
                else if (equipo[carta2].Contains("(all)"))
                {
                    return i;
                }


            }

        }
        return -1;
    }
    IEnumerator AnimacionEmpezarEquiparCpu(int indiceEquipo, int indiceMonstruo)
    {


        int indice = 0;
        intefaz.ReiniciarApuntadorCpu();
        intefaz.SetEstadoApuntador(true);
        int cuadro = indice;

        yield return new WaitForSeconds(0.15f);
        while (cuadro != indiceMonstruo)
        {

            juego.ReproducirEfectoMover();

            indice++;
            cuadro++;
            intefaz.MoverApuntadorDerecha();

            yield return new WaitForSeconds(0.15f);
        }



        StartCoroutine(ActivarEquipoCpu(indiceEquipo, indiceMonstruo));
    }
    IEnumerator ActivarEquipoCpu(int indiceEquipo, int indiceMonstruo)
    {
        intefaz.DesactivarDatosUI();
        intefaz.SetEstadoApuntador(false);
        camara.DevolverCamara(false);

        while (intefaz.datosCarta.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);
            yield return null;
        }
        intefaz.datosCarta.SetActive(false);
        yield return new WaitForSeconds(0.2f);


        Vector3[] destino = new Vector3[2];
        destino[0] = new Vector3(-3.2021f, 1.51f, -4.89f);
        destino[1] = new Vector3(3.2021f, 1.51f, -4.89f);
        campoCpu[indiceEquipo].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        campoCpu[indiceEquipo].GetComponent<muestraCarta>().textoMT.gameObject.SetActive(true);
        campoCpu[indiceEquipo].GetComponent<muestraCarta>().textoMT.text = campoCpu[indiceEquipo].GetComponent<carta>().GetTipoCarta().ToUpper();
        campoCpu[indiceEquipo].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(indiceEquipo));
        campoCpu[indiceEquipo].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoCpu(indiceEquipo));
        int realizada = 0;

        if (campoCpu[indiceMonstruo].GetComponent<carta>().GetDatosCarta() == 0)
        {
            campoCpu[indiceMonstruo].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoCpu(indiceMonstruo));
            campoCpu[indiceMonstruo].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoCpu(indiceMonstruo));
            campoCpu[indiceMonstruo].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
        }



        campoCpu[indiceMonstruo].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoCpu[indiceMonstruo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        campoCpu[indiceEquipo].transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        campoCpu[indiceEquipo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        while (realizada < 2)
        {


            realizada = 0;
            campoCpu[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoCpu[indiceMonstruo].transform.localPosition, destino[0], Time.deltaTime * 30);
            campoCpu[indiceEquipo].transform.localPosition = Vector3.MoveTowards(campoCpu[indiceEquipo].transform.localPosition, destino[1], Time.deltaTime * 30);


            if (Vector3.Distance(campoCpu[indiceMonstruo].transform.localPosition, destino[0]) < 1)
            {
                realizada++;
            }
            if (Vector3.Distance(campoCpu[indiceEquipo].transform.localPosition, destino[1]) < 1)
            {
                realizada++;
            }



            yield return null;
        }

        campoCpu[indiceMonstruo].transform.localPosition = destino[0];
        campoCpu[indiceEquipo].transform.localPosition = destino[1];
        campoCpu[indiceEquipo].GetComponent<carta>().SetDatosCarta(1);
        campoCpu[indiceMonstruo].GetComponent<carta>().SetDatosCarta(1);

        while (campoCpu[indiceEquipo].transform.localPosition.x >= -3.2f)
        {
            campoCpu[indiceEquipo].transform.Translate(50f * Time.deltaTime, 0f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }

        int aumento = EquipoCampo(indiceEquipo, indiceMonstruo);
        aumento = 1000;
        if (campo.GetCampoCpu(indiceEquipo) == 639)
        {
            reduccion += 1000;
        }
        if (campo.GetCampoCpu(indiceEquipo) != 639)
        {
            aumento = 500;
            reduccion += 500;
            if (campo.GetCampoCpu(indiceEquipo) == 708)
            {
                campoCpu[indiceMonstruo].GetComponent<carta>().esInmortal = true;
            }
            if (campo.GetCampoCpu(indiceEquipo) == 721)
            {
                aumento = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (campoCpu[i] != null)
                    {
                        aumento += 500;
                        reduccion += 500;
                    }
                }
            }
        }

        StartCoroutine(AnimacionAumentoCampoCpu(indiceMonstruo, 1));
        campoCpu[indiceMonstruo].transform.localScale = new Vector3(3f, 2.1f, 0.01f);
        campoCpu[indiceMonstruo].GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(campo.GetCampoCpu(indiceMonstruo));
        yield return GetUpgrade(campoCpu[indiceMonstruo], aumento, campoCpu[indiceEquipo],true);
        StartCoroutine(AnimacionAumentoCampoCpu(indiceMonstruo, 2));
        campoCpu[indiceMonstruo].transform.localScale = new Vector3(1.58f, 1.25f, 0.01f);
        yield return new WaitForSeconds(0.5f);
        Vector3 final = new Vector3(0.4f, 3f, -5f);
        while (Vector3.Distance(campoCpu[indiceMonstruo].transform.localPosition, final) > Time.deltaTime * 20)
        {
            campoCpu[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoCpu[indiceMonstruo].transform.localPosition, final, Time.deltaTime * 20);
            yield return null;
        }
        campoCpu[indiceMonstruo].transform.localPosition = final;
        yield return new WaitForSeconds(0.2f);
        while (campoCpu[indiceMonstruo].transform.localPosition.y < 5)
        {

            campoCpu[indiceMonstruo].transform.Translate(0f * Time.deltaTime, -6f * Time.deltaTime, 0f * Time.deltaTime);

            yield return null;
        }




        //escalar
        if (reduccion != 0)
        {

            int activador = 0;
            for (int i = 5; i < 10; i++)
            {
                if (getCartaCampoU(i) != null)
                {
                    if (campo.GetCampoUsuario(i) == 698)
                    {
                        activador = i;
                    }
                }
            }
            if (activador != 0)
            {
                juego.TrampasActiadas++;
                yield return StartCoroutine(ShowCardEffects(getCartaCampoU(activador), activador, new Vector3(0.03f, 2f, 5.42f), Quaternion.Euler(-180, 0, 0)));               
                EfectoReverseTrap(indiceMonstruo);
            }
        }
        reduccion = 0;
        //mover en x selectivo respecto al indice del campo(cuadro verde)
        float posicionarX = posicionesTableroCpu(indiceMonstruo);


        float posicionarZ = -1.5f;

        campoCpu[indiceMonstruo].transform.localPosition = new Vector3(posicionarX, 5f, posicionarZ);
        if (campoCpu[indiceMonstruo].GetComponent<carta>().getPos() == 1)
        {
            campoCpu[indiceMonstruo].transform.localScale = new Vector3(1.7f, 1.7f, 0.01f);
            campoCpu[indiceMonstruo].GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 0f);

        }
        else
        {
            campoCpu[indiceMonstruo].transform.localScale = new Vector3(1.4f, 1.8f, 0.01f);
            campoCpu[indiceMonstruo].GetComponent<Transform>().eulerAngles = new Vector3(90f, -90f, 0f);
        }
        if (campoCpu[indiceMonstruo].GetComponent<carta>().esInmortal)
        {
            campoCpu[indiceMonstruo].GetComponent<muestraCarta>().panelDatos.texture = campoCpu[indiceMonstruo].GetComponent<muestraCarta>().color[3];
        }

        //bajarCarta hasta 0,071
        Vector3 final1 = new Vector3(campoCpu[indiceMonstruo].transform.localPosition.x, 0.071f, campoCpu[indiceMonstruo].transform.localPosition.z);
        while (Vector3.Distance(campoCpu[indiceMonstruo].transform.localPosition, final1) > Time.deltaTime * 7)
        {
            campoCpu[indiceMonstruo].transform.localPosition = Vector3.MoveTowards(campoCpu[indiceMonstruo].transform.localPosition, final1, Time.deltaTime * 7);
            yield return null;
        }
        campoCpu[indiceMonstruo].transform.localPosition = new Vector3(posicionarX, 0.071f, posicionarZ);
        camara.MoverCamara(false);
        intefaz.datosCarta.SetActive(true);
        while (intefaz.datosCarta.transform.position.y < 44.1f)
        {
            float posicionar = 600 * Time.deltaTime;

            intefaz.datosCarta.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        intefaz.datosCarta.transform.localPosition = new Vector2(-22.9f, -205f);
        yield return new WaitForSeconds(0.2f);
        campoCpu[indiceMonstruo].transform.localPosition = new Vector3(posicionarX, 0.071f, posicionarZ);
        cuadroUsuario.CuadrosPosBatalla();
        intefaz.ActualizarUICpuCampo(indiceMonstruo);
        intefaz.ReiniciarApuntadorCpu();
        intefaz.SetEstadoApuntador(true);
        juego.InicioLogicaCpu();
    }
    public void MirarCampo()
    {
        for (int i = 0; i < 10; i++)
        {

            if (campoU[i] != null)
            {
                if (campoU[i].GetComponent<carta>().GetDatosCarta() == 1)
                {
                    controles.valoresCampo.SetValue(1, i);
                }
                else
                {
                    controles.valoresCampo.SetValue(0, i);
                }

                campoU[i].GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                campoU[i].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoUsuario(i));
                campoU[i].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoUsuario(i));
            }
            else
            {
                controles.valoresCampo.SetValue(0, i);
            }

        }
    }
    public void Mano()
    {
        for (int i = 0; i < 10; i++)
        {
            if (campoU[i] != null)
            {
                campoU[i].GetComponent<carta>().SetDatosCarta(controles.GetValoresCampo(i));
                if (campoU[i].GetComponent<carta>().GetDatosCarta() == 1)
                {
                    campoU[i].GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(campo.GetCampoUsuario(i));
                    campoU[i].GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(campo.GetCampoUsuario(i));
                }
                else
                {
                    campoU[i].GetComponent<muestraCarta>().contenedorReverso.SetActive(true);
                }
            }


        }
        intefaz.ActivarGuardianStar(false);
        controles.SetFase("mano");
        controles.SetEsMt(false);


    }

    private IEnumerator GetFusion(GameObject card, int cardId, int fusion, GameObject cardToDestroy, bool isUser)
    {
        intefaz.ColorFlash();
        intefaz.SetTiempoFlash(0.5f);
        juego.ReproducirFusion();
        intefaz.SetFlash(true);
        StartCoroutine(AnimacionFusion(card));
        card.GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
        card.GetComponent<muestraCarta>().imagenCarta.texture = (Texture2D)txt.cartas.GetValue(fusion);
        card.GetComponent<muestraCarta>().imagenMiniCarta.texture = (Texture2D)txt.miniImagens.GetValue(fusion);
        card.GetComponent<muestraCarta>().imagenCartaB.sprite = (Sprite)txt.cartas1.GetValue(fusion);
        int ataqueconvertidor = int.Parse((string)txt.getatk().GetValue(fusion));
        int defConvertidor = int.Parse((string)txt.getdef().GetValue(fusion));
        string nombre = (string)txt.getnom().GetValue(fusion);
        int atributo1 = int.Parse((string)txt.GetAtributos1().GetValue(fusion));
        int atributo2 = int.Parse((string)txt.GetAtributos2().GetValue(fusion));
        int tipoMonstruo = int.Parse((string)txt.GetNumeroTipoCarta().GetValue(fusion));
        int stars = int.Parse((string)txt.GetStars().GetValue(fusion));
        string attribute = txt.GetAttributes().GetValue(fusion).ToString();
        card.GetComponent<carta>().SetTipoAtributo(tipoMonstruo);
        card.GetComponent<carta>().SetAtaque(ataqueconvertidor);
        card.GetComponent<carta>().SetDefensa(defConvertidor);
        card.GetComponent<carta>().SetName(nombre);
        card.GetComponent<carta>().SetGuardianStar(atributo1);
        card.GetComponent<carta>().SetGuardianStar2(atributo2);
        card.GetComponent<carta>().SetTieneBono(false);
        card.GetComponent<carta>().SetTieneBonoDesfavorable(false);
        card.GetComponent<carta>().SetStarsNumber(stars);
        card.GetComponent<carta>().SetAttribute(attribute);
        GetStars(card);
        GetAttribute(card);
        // actualizar el campo luego de fusionar
        juego.EfectosCartasCampo(juego.GetCampoModificado());
        card.GetComponent<muestraCarta>().ataque.text = "" + card.GetComponent<carta>().getAtaque();
        card.GetComponent<muestraCarta>().defensa.text = "" + card.GetComponent<carta>().getDefensa();
        card.GetComponent<muestraCarta>().ataqueB.text = atkText + card.GetComponent<carta>().getAtaque();
        card.GetComponent<muestraCarta>().nombreCarta.text = nombre;
        card.GetComponent<muestraCarta>().nombreCarta.fontSize = GetFontCardName(nombre,false);
        card.GetComponent<muestraCarta>().defensaB.text = defText + card.GetComponent<carta>().getDefensa();
        card.transform.localScale = new Vector3(2.6f, 2.6f, 0f);
        if (isUser)
        {
            campo.SetManoUsuario(cardId, fusion);
        }
        else
        {
            campo.SetManoCpu(cardId, fusion);
        }
        Destroy(cardToDestroy);
        cardToDestroy = null;
        yield return new WaitForSeconds(2f);
        card.GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
        card.transform.localScale = new Vector3(1.4f, 1.4f, 0f);
        StartCoroutine(AnimacionFusion(card));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator GetUpgrade(GameObject card, int aumento, GameObject cardToDestroy,bool withField)
    {

        intefaz.ColorFlash();
        intefaz.SetTiempoFlash(2f);
        intefaz.SetFlash(true);
        if (!withField)
        {
            StartCoroutine(AnimacionFusion(card));
            card.transform.localScale = new Vector3(2.6f, 2.6f, 0.01f);
        }
        card.GetComponent<muestraCarta>().contenedorBatalla.SetActive(true);
        juego.ReproducirAumento();
        GetStars(card);
        GetAttribute(card);
        if (card.GetComponent<carta>().getAtaque() + aumento >= Constane || card.GetComponent<carta>().getDefensa() + aumento >= Constane)
        {
            if (card.GetComponent<carta>().getAtaque() + aumento >= Constane)
            {

                card.GetComponent<carta>().SetAtaque(Constane);

            }
            else
            {
                card.GetComponent<carta>().SetAtaque(card.GetComponent<carta>().getAtaque() + aumento);
            }
            if (card.GetComponent<carta>().getDefensa() + aumento >= Constane)
            {
                card.GetComponent<carta>().SetDefensa(Constane);

            }
            else
            {
                card.GetComponent<carta>().SetDefensa(card.GetComponent<carta>().getDefensa() + aumento);
            }

        }
        else
        {
            card.GetComponent<carta>().SetAtaque(card.GetComponent<carta>().getAtaque() + aumento);
            card.GetComponent<carta>().SetDefensa(card.GetComponent<carta>().getDefensa() + aumento);
        }

        card.GetComponent<muestraCarta>().ataque.text = "" + card.GetComponent<carta>().getAtaque();
        card.GetComponent<muestraCarta>().defensa.text = "" + card.GetComponent<carta>().getDefensa();
        card.GetComponent<muestraCarta>().ataqueB.text = atkText + card.GetComponent<carta>().getAtaque();
        card.GetComponent<muestraCarta>().defensaB.text = defText + card.GetComponent<carta>().getDefensa();
        string cardName = card.GetComponent<carta>().GetName();
        card.GetComponent<muestraCarta>().nombreCarta.text = cardName;
        Debug.LogError(cardName);
        card.GetComponent<muestraCarta>().nombreCarta.fontSize = GetFontCardName(cardName,withField);
        Destroy(cardToDestroy);
        cardToDestroy = null;
        yield return new WaitForSeconds(1.5f);
        card.GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
        if (!withField)
        {
            card.transform.localScale = new Vector3(1.4f, 1.4f, 0f);
            StartCoroutine(AnimacionFusion(card));
            yield return new WaitForSeconds(0.5f);
        }     

    }

    public void GetStars(GameObject card)
    {
        int numberOfStarsToShow = card.GetComponent<carta>().GetStarsNumber();
        int stars = card.GetComponent<muestraCarta>().starsContainer.transform.childCount;
        for (int i = 0; i < stars; i++)
        {
            Transform star = card.GetComponent<muestraCarta>().starsContainer.transform.GetChild(i);
            star.gameObject.SetActive(i < numberOfStarsToShow);
        }
    }

    public void GetAttribute(GameObject card)
    {
        string name = card.GetComponent<carta>().GetAttribute();
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
            card.GetComponent<muestraCarta>().attribute.sprite = txt.attributeImages[indice];
        }
        else
        {
            Debug.LogError("no encontrado el " + name);
        }

       
    }


    public float GetFontCardName(string name,bool isFieldCard)
    {
        Debug.LogError(name.Length);
        float fontSize;
        if (isFieldCard)
        {
            if (name.Length > 29)
            {
                fontSize = 0.14f;
            }
            else if (name.Length > 20)
            {
                fontSize = 0.16f;
            }
            else if (name.Length > 16)
            {
                fontSize = 0.24f;
            }
            else if (name.Length > 12)
            {
                fontSize = 0.24f;
            }
            else
            {
                fontSize = 0.30f;
            }
        }
        else
        {
            if (name.Length > 29)
            {
                fontSize = 4f;
            }
            else if (name.Length > 20)
            {
                fontSize = 5f;
            }
            else if (name.Length > 16)
            {
                fontSize = 6f;
            }
            else 
            {
                fontSize = 7f;
            }
        }
      
        return fontSize;
    }

}

