using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interfaz : MonoBehaviour
{
    public TextMeshProUGUI nombreT;
    public TextMeshProUGUI atkT;
    public TextMeshProUGUI defT;
    public GameObject seleccionarGuardian1;
    public GameObject seleccionarGuardian2;
    public Text vidaUsuarioT;
    public Text vidaCpuT;
    public Image atkicono;
    public Image deficono;
    public RawImage guardian1;
    public RawImage guardian2;
    public RawImage atributo;
    public Text cambiarCampo;
    public Campo campo;
    public ClonCarta clonCarta;
    public Flecha flecha;
    public Apuntador apuntador;
    public GameObject guardianes;
    public GameObject mano;
    public GameObject cartasManoUsuario;
    public GameObject dañoUI;
    public GameObject panelVidaYDeck;
    public CuadroUsuario cuadroUsuario;
    public Juego juego;
    public GameObject datosCarta;
    public ImportadorTextos listas;
    public Text cantDeckUsuario;
    public Text cantDeckCpu;
    public TextMeshProUGUI magicaTrampa;
    public Image Flash;
    public Fade fade;
    public Color colorFlash = new Color(454f, 454f, 454f, 0.60f);
    public bool flashActivado = false;
    public Controles controles;
    public GameObject[] descartes;
    private float tiempoFlash;
    public GameObject espadasLuz;
    public Text espadasLuzTexto;
    public GameObject resultadoDUelo;
    public GameObject resultadoDuelo2;
    public GameObject panelGameOver;
    public GameObject panelMirarTablero;
    public TextMeshProUGUI textoGanar;
    public TextMeshProUGUI textoPerder;
    public GameObject rayo;
    public ParticleSystem particula;
    public ParticleSystem particulaTrampa;
    public ParticleSystem particulaFusion;
    public ParticleSystem particulaAmento;
    public ParticleSystem particulaAtaque;
    //Datos carta cpu
    public GameObject datosCartaCpu;
    public TextMeshProUGUI nombreT1;
    public TextMeshProUGUI atkT1;
    public TextMeshProUGUI defT1;
    public Image atkicono1;
    public Image deficono1;
    public RawImage guardianA;
    public RawImage atributo1;
    public TextMeshProUGUI magicaTrampa1;
    public TextMeshProUGUI guardianFavorable;
    //datos finales duelo
    public TextMeshProUGUI textoDuelista;
    public TextMeshProUGUI textoLpUsuario;
    public TextMeshProUGUI textoLpCpu;
    public TextMeshProUGUI textoEstrellas;
    public TextMeshProUGUI textoRango;
    public TextMeshProUGUI textoNumeroCarta;
    public TextMeshProUGUI textoNombreCarta;
    public TextMeshProUGUI textoNuevaCarta;
    public TextMeshProUGUI textoAtaquePromedioUsuario;
    public TextMeshProUGUI textoAtaquePromedioCpu;
    public TextMeshProUGUI textoCantCartasUsuario;
    public TextMeshProUGUI textoCantCartasCpu;
    public TextMeshProUGUI turnosDUelo;
    public TextMeshProUGUI fusionesCorrectas;
    public TextMeshProUGUI equiposcorrectos;
    public TextMeshProUGUI ataquesEfectivos;
    public TextMeshProUGUI defensaEfectivo;
    public TextMeshProUGUI magicasUsadas;
    public TextMeshProUGUI trampasActivas;
    public TextMeshProUGUI cartasBocaAbajo;
    public TextMeshProUGUI atquePromedioDeck;
    public TextMeshProUGUI defensaPromedioDeck;
    public TextMeshProUGUI condicionesVictoria;
    public GameObject tec;
    public GameObject pow;

    private bool esWoboku;

    void Start()

    {
        tiempoFlash = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (flashActivado)
        {
            Flash.color = colorFlash;
        }
        else
        {
            Flash.color = Color.Lerp(Flash.color, Color.clear, tiempoFlash * Time.deltaTime);
        }
        flashActivado = false;
    }
    public void ColorFlash(int daño)
    {
        if (daño < 1000)
        {
            Flash.color = Color.blue;
        }
        else if (daño < 2000)
        {
            Flash.color = Color.yellow;
        }
        else
        {
            Flash.color = Color.green;
        }

    }
    public void ColorFlash()
    {
        Flash.color = Color.white;
    }
    public void SetEstadoEspadas(bool estado, string numero)
    {

        espadasLuz.SetActive(estado);
        espadasLuzTexto.text = "Swords of revealing light :" + numero;
    }
    public GameObject GetEstadoEspadas()
    {
        return espadasLuz;
    }
    //SOLO PARA LA MANO
    public void ActualizarUi(int pos)
    {

        nombreT.enabled = true;
        atributo.enabled = true;
        string tipo = clonCarta.getClon(pos).GetComponent<carta>().GetTipoCarta();
        if (tipo.Equals("Monstruo"))
        {
            atkicono.enabled = true;
            deficono.enabled = true;
            guardian1.enabled = true;
            guardian2.enabled = true;
            atributo.enabled = true;
            atkT.text = "" + clonCarta.getClon(pos).GetComponent<carta>().getAtaque();
            defT.text = "" + clonCarta.getClon(pos).GetComponent<carta>().getDefensa();
            guardian1.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.getClon(pos).GetComponent<carta>().GetGuardianStar());
            guardian2.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.getClon(pos).GetComponent<carta>().GetGuardianStar2());
            //atributo1.text = clonCarta.GetGuardian(pos);
            //atributo2.text = clonCarta.GetGuardian2(pos);
            atributo.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.getClon(pos).GetComponent<carta>().GetTipoAtributo());
            magicaTrampa.text = "";
        }
        else
        {
            atkicono.enabled = false;
            deficono.enabled = false;
            guardian1.enabled = false;
            guardian2.enabled = false;
            atkT.text = "";
            defT.text = "";
            //atributo1.text = "";
            //atributo2.text = "";
            //tipoAtributo.text = "";
            if (clonCarta.getClon(pos).GetComponent<carta>().GetTipoCarta().Equals("Magica") || clonCarta.getClon(pos).GetComponent<carta>().GetTipoCarta().Equals("Campo"))
            {
                atributo.texture = (Texture2D)listas.atirbutos.GetValue(24);
            }
            else if (clonCarta.getClon(pos).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
            {
                atributo.texture = (Texture2D)listas.atirbutos.GetValue(23);
            }
            else
            {
                atributo.texture = (Texture2D)listas.atirbutos.GetValue(22);
            }

            magicaTrampa.text = clonCarta.getClon(pos).GetComponent<carta>().GetTipoCarta();
        }
        nombreT.text = clonCarta.getClon(pos).GetComponent<carta>().nombreCarta;



    }
    public void DesactivarDatosUI()
    {
        atkicono.enabled = false;
        deficono.enabled = false;
        guardian1.enabled = false;
        guardian2.enabled = false;
        atributo.enabled = false;
        magicaTrampa.text = "";
        nombreT.text = "";
        atkT.text = "";
        defT.text = "";
    }
    // los datos de la carta en el campo al colocar carta o a atacar
    public void DesactivarDatosUICampo()
    {
        atkicono1.enabled = false;
        deficono1.enabled = false;
        guardianA.enabled = false;
        atributo1.enabled = false;
        magicaTrampa1.text = "";
        nombreT1.text = "";
        guardianFavorable.enabled = false;
        atkT1.text = "";
        defT1.text = "";
    }
    //campo del usuario
    public void ActivarDatosUI(int pos)
    {
        DesactivarDatosUI();
        if (clonCarta.getCartaCampoU(pos) != null)
        {
            nombreT.enabled = true;
            atributo.enabled = true;
            string tipo = clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta();
            if (tipo.Equals("Monstruo"))
            {
                atkicono.enabled = true;
                deficono.enabled = true;
                guardian1.enabled = true;
                atkT.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getAtaque();
                defT.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getDefensa();
                guardian1.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetGuardianStarA());

                atributo.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoAtributo());
                magicaTrampa.text = "";
            }
            else
            {

                if (clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Magica") || clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Campo"))
                {
                    atributo.texture = (Texture2D)listas.atirbutos.GetValue(24);
                }
                else if (clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                {
                    atributo.texture = (Texture2D)listas.atirbutos.GetValue(23);
                }
                else
                {
                    atributo.texture = (Texture2D)listas.atirbutos.GetValue(22);
                }

                magicaTrampa.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta();
            }
            nombreT.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().nombreCarta;
        }

    }
    public void SetTiempoFlash(float tiempo)
    {
        tiempoFlash = tiempo;
    }
    public void ActualizarUICpu(int pos)
    {
        // validar si no esta vacio el espacio
        DesactivarDatosUICampo();
        if (juego.GetTurnoUsuario())
        {
            string espadasLuz = juego.GetEspadasLuzReveladora();
            if (espadasLuz.Contains("cpu"))
            {
                nombreT1.enabled = true;
                if (espadasLuz.Contains("3"))
                {
                    nombreT1.text = "Swords of revealing light :3";
                }
                else if (espadasLuz.Contains("2"))
                {
                    nombreT1.text = "Swords of revealing light :2";
                }
                else
                {
                    nombreT1.text = "Swords of revealing light :1";
                }

            }
            else
            {
                if (clonCarta.GetCartaCpu(pos) != null)
                {

                    guardianFavorable.color = juego.ColorAtributo();
                    // validar si la carta tiene los datos visibles y que sea monstruo
                    if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                    {
                        if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetDatosCarta() == 1)
                        {
                            atkT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getAtaque();
                            defT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getDefensa();
                            atkicono1.enabled = true;
                            deficono1.enabled = true;
                            guardianA.enabled = true;
                            atributo1.enabled = true;
                            guardianFavorable.enabled = true;
                            atributo1.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoAtributo());
                            guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());
                            nombreT1.text = clonCarta.GetCartaCpu(pos).GetComponent<carta>().nombreCarta;
                        }
                        else
                        {
                            guardianFavorable.enabled = true;
                            guardianA.enabled = true;
                            guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());

                        }
                    }

                }

            }
        }
        else
        {
            if (clonCarta.GetCartaCpu(pos) != null)
            {
                if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                {
                    if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetDatosCarta() == 1)
                    {
                        atkT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getAtaque();
                        defT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getDefensa();
                        atkicono1.enabled = true;
                        deficono1.enabled = true;
                        guardianA.enabled = true;
                        atributo1.enabled = true;
                        if (juego.GetTurnoUsuario())
                        {
                            guardianFavorable.enabled = true;
                        }

                        atributo1.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoAtributo());
                        guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());
                        nombreT1.text = clonCarta.GetCartaCpu(pos).GetComponent<carta>().nombreCarta;
                    }
                    else
                    {
                        if (juego.GetTurnoUsuario())
                        {
                            guardianFavorable.enabled = true;
                        }
                        guardianA.enabled = true;
                        guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());

                    }
                }

            }

        }




    }
    public void ActualizarUICpuCampo(int pos)
    {
        // validar si no esta vacio el espacio
        DesactivarDatosUI();
        if (clonCarta.GetCartaCpu(pos) != null)
        {

            // validar si la carta tiene los datos visibles y que sea monstruo
            if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetDatosCarta() == 1)
                {
                    atkT.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getAtaque();
                    defT.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getDefensa();
                    atkicono.enabled = true;
                    deficono.enabled = true;
                    guardian1.enabled = true;
                    atributo.enabled = true;
                    atributo.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetTipoAtributo());
                    guardian1.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());
                    nombreT.text = clonCarta.GetCartaCpu(pos).GetComponent<carta>().nombreCarta;
                }
                else
                {
                    guardian1.enabled = true;
                    guardian1.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetGuardianStarA());

                }
            }

        }




    }
    public void ActualizarUIUsuario(int pos)
    {
        DesactivarDatosUICampo();
        if (clonCarta.getCartaCampoU(pos) != null)
        {
            if (!juego.GetTurnoUsuario())
            {
                guardianFavorable.color = juego.ColorAtributo();
            }

            string tipo = clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta();
            if (tipo.Equals("Monstruo"))
            {

                if (!juego.GetTurnoUsuario())
                {

                    if (clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetDatosCarta() == 1)
                    {
                        atkicono1.enabled = true;
                        deficono1.enabled = true;
                        atributo1.enabled = true;
                        nombreT1.enabled = true;
                        nombreT1.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().nombreCarta;
                        atkT1.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getAtaque();
                        defT1.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getDefensa();
                        atributo1.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoAtributo());
                    }
                    guardianA.enabled = true;
                    guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetGuardianStarA());
                    guardianFavorable.enabled = true;
                }
                else
                {
                    atkicono1.enabled = true;
                    deficono1.enabled = true;
                    guardianA.enabled = true;
                    atributo1.enabled = true;
                    nombreT1.enabled = true;
                    nombreT1.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().nombreCarta;
                    atkT1.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getAtaque();
                    defT1.text = "" + clonCarta.getCartaCampoU(pos).GetComponent<carta>().getDefensa();
                    atributo1.texture = (Texture2D)listas.atirbutos.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoAtributo());
                    guardianA.texture = (Texture2D)listas.guardianes.GetValue(clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetGuardianStarA());
                }

            }
            else
            {
                nombreT1.enabled = true;
                nombreT1.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().nombreCarta;
                atributo1.enabled = true;
                if (clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Magica") || clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Campo"))
                {
                    atributo1.texture = (Texture2D)listas.atirbutos.GetValue(24);
                }
                else if (clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta().Equals("Trampa"))
                {
                    atributo1.texture = (Texture2D)listas.atirbutos.GetValue(23);
                }
                else
                {
                    atributo1.texture = (Texture2D)listas.atirbutos.GetValue(22);
                }

                magicaTrampa1.text = clonCarta.getCartaCampoU(pos).GetComponent<carta>().GetTipoCarta();
            }

        }





    }
    public void ActualizarUIMirarCpu(bool vacio, int pos)
    {


        if (vacio == true)
        {
            atkT1.text = "";
            defT1.text = "";
            nombreT1.text = "";
            guardianA.enabled = false;


        }
        else
        {

            guardianFavorable.color = Color.gray;


            if (clonCarta.GetCartaCpu(pos).GetComponent<carta>().GetDatosCarta() == 1)
            {

                atkT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getAtaque();
                defT1.text = "" + clonCarta.GetCartaCpu(pos).GetComponent<carta>().getDefensa();
                nombreT1.text = clonCarta.GetCartaCpu(pos).GetComponent<carta>().nombreCarta;


            }
            else
            {
                atkT1.text = "";
                defT1.text = "";
                nombreT1.text = "";
                ;
            }


        }

    }
    public void SetEstado(bool estado)
    {
        if (estado)
        {
            nombreT.gameObject.SetActive(true);
            atkT.gameObject.SetActive(true);
            defT.gameObject.SetActive(true);
            guardian1.gameObject.SetActive(true);
            guardian2.gameObject.SetActive(true);
            magicaTrampa.gameObject.SetActive(true);
            atkicono.gameObject.SetActive(true);
            deficono.gameObject.SetActive(true);
            atributo.gameObject.SetActive(true);
            // atributo1.gameObject.SetActive(true);
            // atributo2.gameObject.SetActive(true);
            //magicaTrampa.gameObject.SetActive(true);
            //tipoAtributo.gameObject.SetActive(true);
        }
        else
        {
            nombreT.gameObject.SetActive(false);
            atkT.gameObject.SetActive(false);
            defT.gameObject.SetActive(false);
            guardian1.gameObject.SetActive(false);
            guardian2.gameObject.SetActive(false);
            atkicono.gameObject.SetActive(false);
            deficono.gameObject.SetActive(false);
        }

    }
    public void SetEstadoFlecha(bool estado)
    {
        if (estado)
        {
            flecha.gameObject.SetActive(true);
        }
        else
        {
            flecha.gameObject.SetActive(false);
        }
    }
    public void SetEstadoApuntador(bool estado)
    {
        if (estado)
        {
            apuntador.gameObject.SetActive(true);
        }
        else
        {
            apuntador.gameObject.SetActive(false);
        }
    }
    public void MoverApuntador(int indice, bool esMt)
    {
        apuntador.MoverCursor(indice, esMt);
    }
    public void ReiniciarApuntador()
    {
        apuntador.Reiniciar();
    }
    public void ReiniciarApuntadorCpu()
    {
        apuntador.ReiniciarCpu();
    }
    public void MoverApuntadorDerecha()
    {
        apuntador.MoverCursorDerecha();
    }
    public void MoverApuntadorAbajo()
    {
        apuntador.MoverCursorAbajo();
    }
    public void MoverApuntadorArriba()
    {
        apuntador.MoverCursorArriba();
    }
    public void MoverApuntadorIzquierda()
    {
        apuntador.MoverCursorIzquierda();
    }
    public void DesactivarTodosComponentes()
    {
        espadasLuz.SetActive(false);
        cartasManoUsuario.SetActive(false);
        panelVidaYDeck.SetActive(false);
        clonCarta.DesactivarComponentes();
        clonCarta.DesactivarComponentesCpu();
        datosCarta.SetActive(false);
    }
    public void DesactivarComponentes()
    {
        datosCarta.SetActive(false);
        panelVidaYDeck.SetActive(false);
    }
    public void SetEstadoDatosCartaCpu(bool estado)
    {
        datosCartaCpu.SetActive(estado);
    }
    public void ActivarComponentes()
    {
        panelVidaYDeck.SetActive(true);
        clonCarta.SetEstadoManos(false, true);
        juego.NuevaCartaCpu();
    }
    public void ActivarComponentesUsuario()
    {


        datosCarta.SetActive(true);
        panelVidaYDeck.SetActive(true);
        clonCarta.SetEstadoManos(true, false);
        juego.NuevaCartaUsuario();
    }
    IEnumerator ActivarComponenesCpu()
    {
        yield return new WaitForSeconds(3f);
        mano.transform.localPosition = new Vector3(0.087f, 0.5f, -0.55f);
        mano.SetActive(true);
        juego.NuevaCartaCpu();

    }
    public void ActualizarDeckUsuario()
    {
        int cantidad = juego.GetCantDeckUsuario();
        cantDeckUsuario.text = "" + cantidad;
    }
    public void ActualizarDeckCpu()
    {
        int cantidad = juego.GetCantDeckCpu();
        cantDeckCpu.text = "" + cantidad;
    }
    public void InicioDestruirCarta(int cartaPos, int cartaCpuPos, string destnoDaño)
    {
        if (destnoDaño.Equals("cpu") || destnoDaño.Equals("usuario") || destnoDaño.Equals("dos"))
        {
            StartCoroutine(AnimacionInicioDestruirCarta(cartaPos, cartaCpuPos, destnoDaño));
        }

    }
    public void InicioAtaqueCarta(int cartaPos, int cartaCpuPos, string destnoDaño)
    {
        if (!destnoDaño.Contains("trampa"))
        {
            StartCoroutine(AnimacionAtaqueCarta(cartaPos, cartaCpuPos, destnoDaño));
        }

    }
    //corrutinas de contraataque y de destruccion doble
    IEnumerator AnimacionSegundoAtaque(int cartaPos, int cartaCpuPos)
    {
        // destruir la carta del usuario despues
        float destructor = 1f;
        if (juego.GetTurnoUsuario() == true)
        {
            Vector3 originalPos = clonCarta.getCartaCampoU(cartaPos).transform.position;
            Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
            while (destructor > 0)
            {
                nuevaPos.z = clonCarta.getCartaCampoU(cartaPos).transform.position.z;
                clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                destructor -= 0.5f;
            }
        }
        //destruir le carta de la cepu despues
        else
        {

            Vector3 originalPos = clonCarta.GetCartaCpu(cartaCpuPos).transform.position;
            Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
            while (destructor > 0)
            {
                nuevaPos.z = clonCarta.GetCartaCpu(cartaCpuPos).transform.position.z;
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                destructor -= 0.5f;
            }
        }
    }
    IEnumerator AnimacionRayoSegundaCarta(int cartaPos, int cartaCpuPos)
    {

        float corte = -0.75f;
        rayo.gameObject.SetActive(true);
        rayo.transform.position = new Vector3(2.5f, 4.48f, 8f);

        while (corte > -4)
        {
            rayo.transform.localScale = new Vector3(1f, corte, 1f);
            corte -= 0.3f;
            yield return new WaitForSeconds(0.01f);
        }
        rayo.gameObject.SetActive(false);
        rayo.transform.position = new Vector3(-0.17f, 3.72f, 6f);
        rayo.transform.localScale = new Vector3(1f, -0.75f, 1f);
    }
    // fin de las animaciones de contraataque y ataque doble
    IEnumerator AnimacionAtaqueCarta(int cartaPos, int cartaCpuPos, string destinoDaño)
    {

        float corte = -0.75f;
        rayo.transform.position = new Vector3(2.5f, 4.48f, 8f);
        if (juego.GetTurnoUsuario())
        {
            rayo.transform.position = new Vector3(0.32f, 4.48f, 8f);


        }

        if (destinoDaño.Equals("usuario") || destinoDaño.Equals("usuarioDirecto") || destinoDaño.Equals("usuarioNoDestruir") || destinoDaño.Equals("dos"))
        {
            if (destinoDaño.Equals("dos"))
            {
                rayo.transform.position = new Vector3(0.32f, 4.48f, 8f); ;



            }
            else
            {
                rayo.transform.position = new Vector3(0.32f, 4.48f, 8f);
                if (juego.GetTurnoUsuario())
                {
                    rayo.transform.position = new Vector3(2.5f, 4.48f, 8f);

                }

            }

        }
        rayo.gameObject.SetActive(true);
        while (corte > -4)
        {
            rayo.transform.localScale = new Vector3(1f, corte, 1f);
            corte -= 0.3f;
            yield return new WaitForSeconds(0.01f);
        }
        rayo.gameObject.SetActive(false);
        rayo.transform.position = new Vector3(-0.17f, 3.72f, 6f);
        rayo.transform.localScale = new Vector3(1f, -0.75f, 1f);

    }
    IEnumerator AnimacionInicioDestruirCarta(int cartaPos, int cartaCpuPos, string destinoDaño)
    {
        float destructor = 1f;
        if (destinoDaño.Equals("cpu"))
        {
            Vector3 originalPos = clonCarta.GetCartaCpu(cartaCpuPos).transform.position;
            Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
            while (destructor > 0)
            {
                nuevaPos.z = clonCarta.GetCartaCpu(cartaCpuPos).transform.position.z;
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                destructor -= 0.5f;
            }
        }
        else if (destinoDaño.Equals("dos"))
        {
            if (juego.GetTurnoUsuario() == true)
            {
                Vector3 originalPos = clonCarta.GetCartaCpu(cartaCpuPos).transform.position;
                Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
                while (destructor > 0)
                {
                    nuevaPos.z = clonCarta.GetCartaCpu(cartaCpuPos).transform.position.z;
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.position = originalPos;
                    yield return new WaitForSeconds(0.05f);
                    destructor -= 0.5f;
                }
            }
            else
            {
                Vector3 originalPos = clonCarta.getCartaCampoU(cartaPos).transform.position;
                Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
                while (destructor > 0)
                {
                    nuevaPos.z = clonCarta.getCartaCampoU(cartaPos).transform.position.z;
                    clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                    yield return new WaitForSeconds(0.05f);
                    clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                    yield return new WaitForSeconds(0.05f);
                    destructor -= 0.5f;
                }
            }
        }
        else
        {
            Vector3 originalPos = clonCarta.getCartaCampoU(cartaPos).transform.position;
            Vector3 nuevaPos = Random.insideUnitSphere * (Time.deltaTime * -2f);
            while (destructor > 0)
            {
                nuevaPos.z = clonCarta.getCartaCampoU(cartaPos).transform.position.z;
                clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x - 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = new Vector3(originalPos.x + 0.02f, originalPos.y, originalPos.z);
                yield return new WaitForSeconds(0.05f);
                clonCarta.getCartaCampoU(cartaPos).transform.position = originalPos;
                yield return new WaitForSeconds(0.05f);
                destructor -= 0.5f;
            }
        }

    }
    public void MostrarDaño(int daño, string destinoDaño, int cartaUsuario, int cartaCpu)
    {


        //logica para mostrar el daño
        if (!destinoDaño.Equals("cpuDirecto") || !destinoDaño.Equals("usuarioDirecto"))
        {
            juego.ReproducirAtaque();
        }

        if (destinoDaño.Equals("cpu") || destinoDaño.Equals("cpuDirecto") || destinoDaño.Equals("cpuNoDestruir"))
        {
            particulaAtaque.transform.localPosition = new Vector3(1.56f, 3.26f, 7.04f);
            dañoUI.transform.localPosition = new Vector3(-243.3f, 10.69f, 0);
            if (juego.GetTurnoUsuario())
            {
                dañoUI.transform.localPosition = new Vector3(243.3f, 10.69f, 0);
                particulaAtaque.transform.localPosition = new Vector3(-0.7f, 3.26f, 7.04f);
            }

            particulaAtaque.transform.localScale = new Vector3(1f, 1f, 1f);
            if (daño > 0)
            {
                if (destinoDaño.Equals("cpuDirecto"))
                {
                    juego.ReproducirAtaqueDirecto();
                }
                dañoUI.GetComponent<TextMeshProUGUI>().text = "- " + daño;
                if (daño < 1000)
                {
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.blue;
                }
                else if (daño < 2000)
                {
                    particulaAtaque.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                }
                else
                {
                    particulaAtaque.transform.localScale = new Vector3(2f, 2f, 2f);
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.red;
                }

                dañoUI.SetActive(true);
            }



        }
        else if (destinoDaño.Equals("usuario") || destinoDaño.Equals("usuarioDirecto") || destinoDaño.Equals("usuarioNoDestruir"))
        {
            dañoUI.transform.localPosition = new Vector3(243.3f, 10.69f, 0);
            particulaAtaque.transform.localPosition = new Vector3(-0.7f, 3.26f, 7.04f);

            if (juego.GetTurnoUsuario())
            {
                particulaAtaque.transform.localPosition = new Vector3(1.56f, 3.26f, 7.04f);
                dañoUI.transform.localPosition = new Vector3(-243.3f, 10.69f, 0);
            }

            particulaAtaque.transform.localScale = new Vector3(1f, 1f, 1f);
            if (daño > 0)
            {
                if (destinoDaño.Equals("usuarioDirecto"))
                {
                    juego.ReproducirAtaqueDirecto();
                }
                dañoUI.GetComponent<TextMeshProUGUI>().text = "- " + daño;
                if (daño < 1000)
                {
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.blue;
                }
                else if (daño < 2000)
                {
                    particulaAtaque.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                }
                else
                {
                    particulaAtaque.transform.localScale = new Vector3(2f, 2f, 2f);
                    dañoUI.GetComponent<TextMeshProUGUI>().color = Color.red;
                }

                dañoUI.SetActive(true);
            }




            //StartCoroutine(esperarAnimacionVida());
        }
        //EmpezarAnimacionVida(dañoTotal, posUsuario, posCpu, destinoDaño);
        if (clonCarta.getCartaCampoU(cartaUsuario) != null && clonCarta.GetCartaCpu(cartaCpu) != null)
        {
            if (clonCarta.getCartaCampoU(cartaUsuario).GetComponent<carta>().GetTipoCarta().Equals("Monstruo") && clonCarta.GetCartaCpu(cartaCpu).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                if (daño > 0)
                {
                    particulaAtaque.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            particulaAtaque.gameObject.SetActive(true);
        }


    }
    public void desvanecerDaño()
    {
        StartCoroutine(animacionDesvanecerDaño());
    }
    IEnumerator animacionDesvanecerDaño()
    {
        float desaparecer = 1f;
        yield return new WaitForSeconds(0.5f);
        if (dañoUI.gameObject.activeSelf == true)
        {
            while (desaparecer > 0)
            {
                dañoUI.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, desaparecer);
                desaparecer -= 0.1f;
                yield return null;
            }
            dañoUI.SetActive(false);
        }

        else
        {
            yield return null;
        }
        particulaAtaque.gameObject.SetActive(false);
    }
    public void SetFlash(bool activar)
    {
        flashActivado = activar;
    }
    public void EmpezarAnimacionVida(int daño, string destinoDaño)
    {
        StartCoroutine(AnimacionVida(daño, destinoDaño));
    }
    public void EmpezarAnimacionVidaAumento(int vida, string vidaDe)
    {
        StartCoroutine(AnimacionVidaAumwento(vida, vidaDe));
    }
    public void EmpezarAnimacionAtaque(int vida, string vidaDe)
    {
        StartCoroutine(AnimacionVidaAumwento(vida, vidaDe));
    }
    IEnumerator AnimacionVida(int daño, string destinoDaño)
    {
        //si el ataque del usuario es mayor al de la cpu
        int constante = 50;
        if (destinoDaño.Equals("usuario") || destinoDaño.Equals("usuarioDirecto") || destinoDaño.Equals("usuarioNoDestruir"))
        {

            while (daño > 0 && juego.GetVidaUsuario() > 0)
            {
                if (daño - constante < 0)
                {
                    constante = daño;
                }
                juego.SetVidaUsuario(constante);
                vidaUsuarioT.text = "" + juego.GetVidaUsuario();
                daño = daño - constante;
                yield return null;
            }
        }
        else
        {
            while (daño > 0 && juego.GetVidaCpu() > 0)
            {
                if (daño - constante < 0)
                {
                    constante = daño;
                }
                juego.SetVidaCpu(constante);
                vidaCpuT.text = "" + juego.GetVidaCpu();
                daño = daño - constante;
                yield return null;
            }
        }

    }
    IEnumerator AnimacionVidaAumwento(int vida, string vidaDe)
    {
        //si el ataque del usuario es mayor al de la cpu
        if (vidaDe.Equals("usuario"))
        {

            while (vida > 0)
            {
                juego.SetVidaUsuario(-50);
                vidaUsuarioT.text = "" + juego.GetVidaUsuario();
                vida = vida - 50;
                yield return null;
            }
        }
        else
        {
            while (vida > 0)
            {
                juego.SetVidaCpu(-50);
                vidaCpuT.text = "" + juego.GetVidaCpu();
                vida = vida - 50;
                yield return null;
            }
        }

    }
    IEnumerator AnimacionVidaAumwentoMonstruo(int vida, string vidaDe)
    {
        //si el ataque del usuario es mayor al de la cpu
        int valorVida = 0;
        if (vidaDe.Equals("usuario"))
        {
            valorVida = juego.GetVidaUsuario() + vida;
            while (vida > 0)
            {
                juego.SetVidaUsuario(-1);
                vidaUsuarioT.text = "" + juego.GetVidaUsuario();
                vida = vida - 50;
                yield return null;
            }
        }
        else
        {
            valorVida = juego.GetVidaCpu() + vida;
            while (vida > 0)
            {
                juego.SetVidaCpu(-50);
                vidaCpuT.text = "" + juego.GetVidaCpu();
                vida = vida - 50;
                yield return null;
            }
        }
        if (vidaDe.Equals("usuario"))
        {
            juego.SetVidaUsuarioAumento(valorVida);
            vidaUsuarioT.text = "" + juego.GetVidaUsuario();
        }
        else
        {
            juego.SetVidaCpuAumento(valorVida);
            vidaCpuT.text = "" + juego.GetVidaCpu();
        }



    }
    public void FinBatalla(int daño, string destinoDaño, int cartaPos, int cartaCpuPos)
    {
        // falta animacion cuando se destruye una carta ,por ahora solo es ataque directo ,cambiar 
        //este codigo
        //fade.QuitarFade();
        StartCoroutine(animacionFinBatalla(daño, destinoDaño, cartaPos, cartaCpuPos));


    }
    IEnumerator animacionFinBatalla(int daño, string destinoDaño, int cartaPos, int cartaCpuPos)
    {
        esWoboku = false;
        particula.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
        float reductor = 3f;
        bool desaparecerPaneles = false;
        bool acabarDuelo;
        bool usuarioPierde = true;
        if (destinoDaño.Contains("usuario"))
        {
            if (juego.GetVidaUsuario() > daño)
            {
                acabarDuelo = false;
            }
            else
            {

                acabarDuelo = true;
            }
        }


        else if (destinoDaño.Contains("cpu"))
        {
            if (juego.GetVidaCpu() > daño)
            {
                acabarDuelo = false;
            }
            else
            {
                usuarioPierde = false;
                acabarDuelo = true;
            }
        }
        else
        {
            acabarDuelo = false;
        }
        if (destinoDaño.Equals("usuario"))
        {

            juego.contadorCartasDestruidas++;
            juego.ReproducirQuemar();
            particula.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
            if (juego.GetTurnoUsuario())
            {
                particula.transform.position = new Vector3(2.32f, 1.4f, 8.4f);
            }

            particula.gameObject.SetActive(true);
            while (reductor > 0)
            {
                particula.transform.Translate(0f, 0f, 0.1f);
                reductor -= 0.1f;
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                if (desaparecerPaneles == false && reductor < 0.9f)
                {
                    desaparecerPaneles = true;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.01f);
            }
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
            particula.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
           
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
            //dañoUsuario.SetActive(false);
            //StopAllCoroutines();
            GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
            clonCarta.SetCartaCampo(cartaPos);
            campo.SetCampoUsuario(cartaPos, 0);
            clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            EmpezarAnimacionVida(daño, destinoDaño);
            yield return new WaitForSeconds(0.5f);
            //yield return new WaitForSeconds(3f);
        }
        else if (destinoDaño.Equals("cpu"))
        {
            particula.transform.position = new Vector3(2.32f, 1.4f, 8.4f);
            if (juego.GetTurnoUsuario())
            {
                particula.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);

            }
            particula.gameObject.SetActive(true);
            juego.ReproducirQuemar();

            while (reductor > 0)
            {
                particula.transform.Translate(0f, 0f, 0.1f);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                reductor -= 0.1f;
                if (desaparecerPaneles == false && reductor < 0.9f)
                {
                    desaparecerPaneles = true;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(0.01f);
            }
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
            particula.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            //dañoUsuario.SetActive(false);
            //StopAllCoroutines();
            GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
            clonCarta.SetCartaCpuCampo(cartaCpuPos);
            campo.SetCampoCpu(cartaCpuPos, 0);
            clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            EmpezarAnimacionVida(daño, destinoDaño);
            yield return new WaitForSeconds(0.5f);
            //yield return new WaitForSeconds(3f);
        }
        else if (destinoDaño.Equals("dos"))
        {
            juego.contadorCartasDestruidas++;
            particula.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
            if (juego.GetTurnoUsuario() == true)
            {


                particula.gameObject.SetActive(true);
                juego.ReproducirQuemar();
                while (reductor > 0)
                {
                    particula.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
            }
            else
            {

                particula.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
                juego.ReproducirQuemar();
                particula.gameObject.SetActive(true);
                while (reductor > 0)
                {
                    particula.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
            }
          
            particula.gameObject.SetActive(false);
            reductor = 3f;
            SetFlash(true);
            desaparecerPaneles = false;
            juego.ReproducirAtaque();
            StartCoroutine(AnimacionRayoSegundaCarta(cartaPos, cartaCpuPos));
            StartCoroutine(AnimacionSegundoAtaque(cartaPos, cartaCpuPos));
            yield return new WaitForSeconds(0.5f);
            if (juego.GetTurnoUsuario() == true)
            {

                particula.transform.position = new Vector3(2.32f, 1.4f, 8.4f);
                juego.ReproducirQuemar();
                particula.gameObject.SetActive(true);

                while (reductor > 0)
                {

                    particula.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);

            }
            else
            {

                particula.transform.position = new Vector3(2.32f, 1.4f, 8.4f);
                particula.gameObject.SetActive(true);
                juego.ReproducirQuemar();
                while (reductor > 0)
                {
                    particula.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);

            }
           
            particula.gameObject.SetActive(false);

            GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
            GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
            clonCarta.SetCartaCampo(cartaPos);
            clonCarta.SetCartaCpuCampo(cartaCpuPos);
            campo.SetPosCampo(cartaPos, 0);
            campo.SetCampoCpu(cartaCpuPos, 0);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
        else if (destinoDaño.Equals("usuarioDirecto"))
        {

            yield return new WaitForSeconds(0.5f);

            //dañoUsuario.SetActive(false);
            //StopAllCoroutines();
            clonCarta.RetornarACampoCpu(cartaCpuPos);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            EmpezarAnimacionVida(daño, destinoDaño);
            yield return new WaitForSeconds(0.2f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);

            //yield return new WaitForSeconds(3f);
        }
        else if (destinoDaño.Equals("cpuDirecto"))
        {

            yield return new WaitForSeconds(0.2f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
            clonCarta.RetornarACampoUsuario(cartaPos);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            //EmpezarAnimacionVida(daño, destinoDaño);
            StartCoroutine(AnimacionVida(daño, destinoDaño));
            if (daño > juego.GetVidaCpu())
            {
                //controles.SetEstadoJuego(true);
            }

            yield return new WaitForSeconds(0.5f);
        }
        else if (destinoDaño.Equals("trampaACpu"))
        {
            juego.TrampasActiadas++;
            // fake trap 
            particulaTrampa.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
            if (campo.GetCampoUsuario(cartaPos) == 682 || campo.GetCampoUsuario(cartaPos) == 686 || campo.GetCampoUsuario(cartaPos) == 687 || campo.GetCampoUsuario(cartaPos) == 695 || campo.GetCampoUsuario(cartaPos) == 700)
            {

                juego.ReproducirQuemar();
                particulaTrampa.gameObject.SetActive(true);

                while (reductor > 0)
                {

                    particulaTrampa.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                if (campo.GetCampoUsuario(cartaPos) == 687)
                {
                    StartCoroutine(AnimacionVidaAumwentoMonstruo((clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque()) / 2, "usuario"));
                }
                if (campo.GetCampoUsuario(cartaPos) == 695)
                {
                    StartCoroutine(AnimacionVidaAumwentoMonstruo((clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque()), "usuario"));
                }
                if (campo.GetCampoUsuario(cartaPos) == 686)
                {
                    daño = clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque();
                    if (juego.GetVidaCpu() - daño <= 0)
                    {
                        acabarDuelo = true;
                        usuarioPierde = false;
                    }
                }
                if (campo.GetCampoUsuario(cartaPos) == 700)
                {
                    esWoboku = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (clonCarta.GetCartaCpu(i) != null)
                        {
                            campo.SetAtaquesCpu(i, 0);
                        }
                    }
                }
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
                particulaTrampa.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
                GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
                clonCarta.SetCartaCampo(cartaPos);
                campo.SetCampoUsuario(cartaPos, 0);
                clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                EmpezarAnimacionVida(daño, destinoDaño);
                yield return new WaitForSeconds(0.5f);
            }
            //mirror wall
            else if (campo.GetCampoUsuario(cartaPos) == 684)
            {
                while (reductor > 0)
                {
                    clonCarta.getCartaCampoU(cartaPos).transform.localScale = new Vector3(4f, reductor, 0f);
                    reductor -= 0.1f;
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.2f);

                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().SetAtaque(clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() / 2);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().SetDefensa(clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getDefensa() / 2);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataque.text = "" + clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque();
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensa.text = "" + clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getDefensa();
                GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
                clonCarta.SetCartaCampo(cartaPos);
                campo.SetCampoUsuario(cartaPos, 0);
                clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                EmpezarAnimacionVida(daño, destinoDaño);
                yield return new WaitForSeconds(0.5f);
            }
            //destruir carta
            else
            {
                particulaTrampa.transform.position = new Vector3(2.32f, 1.4f, 8.4f);

                particulaTrampa.gameObject.SetActive(true);
                juego.ReproducirQuemar();

                while (reductor > 0)
                {
                    particulaTrampa.transform.Translate(0f, 0f, 0.1f);
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    reductor -= 0.1f;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }

                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
                particulaTrampa.gameObject.SetActive(false);
                reductor = 3f;
                while (reductor > 0)
                {
                    clonCarta.getCartaCampoU(cartaPos).transform.localScale = new Vector3(4f, reductor, 0f);
                    reductor -= 0.1f;
                    yield return new WaitForSeconds(0.01f);
                }
                if (campo.GetCampoUsuario(cartaPos) == 689)
                {
                    int valor = 350;
                    if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 5000)
                    {
                        valor *= 10;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 3500)
                    {
                        valor *= 9;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 3000)
                    {
                        valor *= 8;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 2500)
                    {
                        valor *= 7;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 2000)
                    {
                        valor *= 6;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 1500)
                    {
                        valor *= 5;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 1000)
                    {
                        valor *= 4;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() >= 500)
                    {
                        valor *= 3;
                    }
                    else if (clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque() > 0)
                    {
                        valor *= 2;
                    }

                    StartCoroutine(AnimacionVidaAumwento(valor, "usuario"));
                }
                GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
                GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
                clonCarta.SetCartaCampo(cartaPos);
                clonCarta.SetCartaCpuCampo(cartaCpuPos);
                campo.SetCampoCpu(cartaCpuPos, 0);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if ((destinoDaño.Equals("trampaAUsuario")))
        {
            if (campo.GetCampoCpu(cartaCpuPos) == 682 || campo.GetCampoCpu(cartaCpuPos) == 686 || campo.GetCampoCpu(cartaCpuPos) == 687 || campo.GetCampoCpu(cartaCpuPos) == 695 || campo.GetCampoCpu(cartaCpuPos) == 700)
            {

                particulaTrampa.transform.position = new Vector3(-0.77f, 1.4f, 8.4f);
                particulaTrampa.gameObject.SetActive(true);
                juego.ReproducirQuemar();

                while (reductor > 0)
                {
                    particulaTrampa.transform.Translate(0f, 0f, 0.1f);
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    reductor -= 0.1f;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                    }

                    yield return new WaitForSeconds(0.01f);
                }
                if (campo.GetCampoCpu(cartaCpuPos) == 687)
                {
                    StartCoroutine(AnimacionVidaAumwentoMonstruo((clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque()) / 2, "cpu"));
                }
                if (campo.GetCampoCpu(cartaCpuPos) == 695)
                {
                    StartCoroutine(AnimacionVidaAumwentoMonstruo((clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque()), "cpu"));
                }
                if (campo.GetCampoCpu(cartaCpuPos) == 686)
                {
                    daño = clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque();
                    if (juego.GetVidaUsuario() - daño <= 0)
                    {
                        acabarDuelo = true;
                        usuarioPierde = true;
                    }
                }
                if (campo.GetCampoCpu(cartaCpuPos) == 700)
                {
                    esWoboku = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (clonCarta.getCartaCampoU(i) != null)
                        {
                            campo.SetAtaquesUsuario(i, 0);
                        }
                    }
                }
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
                particulaTrampa.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
                //dañoUsuario.SetActive(false);
                //StopAllCoroutines();
                GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
                clonCarta.SetCartaCpuCampo(cartaCpuPos);
                campo.SetCampoCpu(cartaCpuPos, 0);
                clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                EmpezarAnimacionVida(daño, "usuario");
                yield return new WaitForSeconds(0.5f);
            }
            else if (campo.GetCampoCpu(cartaCpuPos) == 684)
            {
                while (reductor > 0)
                {
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.localScale = new Vector3(4f, reductor, 0f);
                    reductor -= 0.1f;
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.2f);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().SetAtaque(clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() / 2);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().SetDefensa(clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getDefensa() / 2);
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataque.text = "" + clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque();
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensa.text = "" + clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getDefensa();
                GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
                clonCarta.SetCartaCpuCampo(cartaCpuPos);
                campo.SetCampoCpu(cartaCpuPos, 0);
                clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                particulaTrampa.transform.position = new Vector3(2.32f, 1.4f, 8.4f);
                juego.ReproducirQuemar();
                particulaTrampa.gameObject.SetActive(true);

                while (reductor > 0)
                {

                    particulaTrampa.transform.Translate(0f, 0f, 0.1f);
                    reductor -= 0.1f;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCartaB.fillAmount = reductor;
                    if (desaparecerPaneles == false && reductor < 0.9f)
                    {
                        desaparecerPaneles = true;
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(false);
                        clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(false);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorNombre.gameObject.SetActive(false);
                particulaTrampa.gameObject.SetActive(false);
                reductor = 3f;
                while (reductor > 0)
                {
                    clonCarta.GetCartaCpu(cartaCpuPos).transform.localScale = new Vector3(4f, reductor, 0f);
                    reductor -= 0.1f;
                    yield return new WaitForSeconds(0.01f);
                }
                if (campo.GetCampoCpu(cartaCpuPos) == 689)
                {
                    int valor = 350;
                    if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 5000)
                    {
                        valor *= 10;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 3500)
                    {
                        valor *= 9;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 3000)
                    {
                        valor *= 8;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 2500)
                    {
                        valor *= 7;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 2000)
                    {
                        valor *= 6;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 1500)
                    {
                        valor *= 5;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 1000)
                    {
                        valor *= 4;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() >= 500)
                    {
                        valor *= 3;
                    }
                    else if (clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque() > 0)
                    {
                        valor *= 2;
                    }

                    StartCoroutine(AnimacionVidaAumwento(valor, "cpu"));
                }
                GameObject.Destroy(clonCarta.getCartaCampoU(cartaPos));
                GameObject.Destroy(clonCarta.GetCartaCpu(cartaCpuPos));
                clonCarta.SetCartaCampo(cartaPos);
                clonCarta.SetCartaCpuCampo(cartaCpuPos);
                campo.SetCampoCpu(cartaCpuPos, 0);
                fade.QuitarFade();
                panelVidaYDeck.SetActive(true);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.2f);

            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorBatalla.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().contenedorReverso.SetActive(false);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.gameObject.SetActive(true);
            clonCarta.RetornarACampo(cartaPos, cartaCpuPos);
            fade.QuitarFade();
            panelVidaYDeck.SetActive(true);
            EmpezarAnimacionVida(daño, destinoDaño);
            yield return new WaitForSeconds(0.5f);
        }


        if (clonCarta.GetCartaCpu(cartaCpuPos) != null && juego.GetTurnoUsuario() == false)
        {

            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(true);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(true);

            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
            clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);

        }
        if (clonCarta.getCartaCampoU(cartaPos) != null && juego.GetTurnoUsuario() == true)
        {
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(true);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(true);

            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
            clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);

        }
        if (esWoboku)
        {
            if (juego.GetTurnoUsuario() == true)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clonCarta.getCartaCampoU(i) != null)
                    {
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(true);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(true);

                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                        clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (clonCarta.GetCartaCpu(i) != null)
                    {


                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelAtaqueB.gameObject.SetActive(true);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelDefensaB.gameObject.SetActive(true);

                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().ataqueB.color = new Color(1f, 1f, 1f, 1f);
                        clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().defensaB.color = new Color(1f, 1f, 1f, 1f);
                    }
                }

            }
        }
        if (acabarDuelo == true)
        {
            juego.DetenerMusica();
            yield return new WaitForSeconds(1f);
            DesactivarTodosComponentes();
            juego.FinJuego(usuarioPierde);
        }
        else
        {
            controles.VolverCampo();
        }

    }
    public void SetEstadoPanel(bool estado)
    {
        panelVidaYDeck.SetActive(estado);
    }
    public void SetEstadoMano(bool estado)
    {


    }
    public void SetEstadoDatosC(bool estado)
    {
        datosCarta.SetActive(estado);
    }
    public void Comprobante()
    {
        controles.SetEsMt(false);
        tiempoFlash = 5F;
        for (int i = 0; i < 5; i++)
        {
            campo.SetAtaquesCpu(i, 0);
            campo.SetAtaquesUsuario(i, 0);
            if (clonCarta.GetCartaCpu(i + 5) != null)
            {
                campo.SetZonasMT(i, 1);
            }


            if (clonCarta.getCartaCampoU(i) != null)
            {
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getCartaCampoU(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);

            }


            if (clonCarta.getClon(i) != null)
            {

                clonCarta.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(1f, 1f, 1f, 1f);


            }
            controles.GetListaDCartas().Clear();
            controles.ReiniciarDescartes();
            clonCarta.LimpiarLista();
            controles.ReinicarContadorDescarte();
            if (clonCarta.GetCartaCpu(i) != null)
            {
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().textoMT.color = new Color(1f, 1f, 1f, 1f);
                clonCarta.GetCartaCpu(i).GetComponent<muestraCarta>().reverso.color = new Color(1f, 1f, 1f, 1f);
                campo.SetAtaquesCpu(i, 1);
            }
            if (clonCarta.getCartaCampoU(i) != null)
            {
                campo.SetAtaquesUsuario(i, 1);
            }
        }
    }
    public void ActivarGuardianStar(bool estado)
    {
        if (estado)
        {
            // atributo1.gameObject.SetActive(false);
            // atributo2.gameObject.SetActive(false);
            // atributoA.gameObject.SetActive(true);
        }
        else
        {
            // atributo1.gameObject.SetActive(true);
            // atributo2.gameObject.SetActive(true);
            // atributoA.gameObject.SetActive(false);
        }

    }
    public void EstadoGuardianes(bool estado)
    {
        if (estado)
        {
            guardianes.gameObject.SetActive(true);
            seleccionarGuardian1.GetComponentInChildren<Text>().text = clonCarta.guardian1Seleccionar();
            seleccionarGuardian1.GetComponentInChildren<RawImage>().texture = (Texture2D)listas.guardianes.GetValue(clonCarta.guardian1SeleccionarImagen());
            seleccionarGuardian2.GetComponentInChildren<Text>().text = clonCarta.guardian2Seleccionar();
            seleccionarGuardian2.GetComponentInChildren<RawImage>().texture = (Texture2D)listas.guardianes.GetValue(clonCarta.guardian2SeleccionarImagen());
            flecha.FlechaGuardian();
            flecha.gameObject.SetActive(true);
        }
        else
        {
            guardianes.gameObject.SetActive(false);
            flecha.gameObject.SetActive(false);
        }
    }
    public void MostrarModificadores(int cartaPos, int cartaCpuPos)
    {
        StartCoroutine(AnimacionMostrarModificadores(cartaPos, cartaCpuPos));
    }
    IEnumerator AnimacionMostrarModificadores(int cartaPos, int cartaCpuPos)
    {

        string favorable = juego.LogicaAtributo(cartaPos, cartaCpuPos);
        int ataqueUsuario = clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque();
        int defensaUsuario = clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getDefensa();
        int ataqueCpu = clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque();
        int defensaCpu = clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getDefensa();
        int posUsuario = clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getPos();
        int posCpu = clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getPos();
        string opc = "";
        int temporal = 0;
        if (posUsuario == 1 && posCpu == 1)
        {
            opc = "dosAtaques";
        }
        else if (posUsuario == 1 && posCpu == 0)
        {
            opc = "usuarioAtaque";
        }
        else
        {
            opc = "cpuAtaque";
        }
        if (favorable.Equals("atributoUsuario"))
        {
            particulaAmento.transform.localPosition = new Vector3(-0.85f, 2.64f, 6.7f);

            if (juego.GetTurnoUsuario())
            {
                particulaAmento.transform.localPosition = new Vector3(1.64f, 2.64f, 6.7f);
            }
            if (opc.Equals("dosAtaques"))
            {


                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueUsuario += 50;
                    defensaUsuario += 50;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueUsuario;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaUsuario;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);


            }
            else if (opc.Equals("usuarioAtaque"))
            {
                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueUsuario += 50;
                    defensaUsuario += 50;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueUsuario;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaUsuario;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);
            }
            else
            {

                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueUsuario += 50;
                    defensaUsuario += 50;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueUsuario;
                    clonCarta.getCartaCampoU(cartaPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaUsuario;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);
            }

        }
        else if (favorable.Equals("atributoCpu"))
        {
            particulaAmento.transform.localPosition = new Vector3(1.64f, 2.64f, 6.7f);
            if (juego.GetTurnoUsuario())
            {
                particulaAmento.transform.localPosition = new Vector3(-0.85f, 2.64f, 6.7f);
            }

            if (opc.Equals("dosAtaques"))
            {


                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueCpu += 50;
                    defensaCpu += 50;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueCpu;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaCpu;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);
            }
            else if (opc.Equals("usuarioAtaque"))
            {

                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueCpu += 50;
                    defensaCpu += 50;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueCpu;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaCpu;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);
            }
            else
            {

                juego.ReproducirGuardianFavorable();
                particulaAmento.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                while (temporal < 500)
                {
                    temporal += 50;
                    ataqueCpu += 50;
                    defensaCpu += 50;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().ataqueB.text = "" + ataqueCpu;
                    clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<muestraCarta>().defensaB.text = "" + defensaCpu;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.5f);
                particulaAmento.gameObject.SetActive(false);
            }
        }

    }
    public void mostrarModificadorDirecto(int cartaPos, int cartaCpuPos, string destinoDaño)
    {
        int ataqueUsuario = 0;
        int ataqueCpu = 0;

        if (clonCarta.getCartaCampoU(cartaPos) != null)
        {
            ataqueUsuario = clonCarta.getCartaCampoU(cartaPos).GetComponent<carta>().getAtaque();
        }
        if (clonCarta.GetCartaCpu(cartaCpuPos) != null)
        {
            ataqueCpu = clonCarta.GetCartaCpu(cartaCpuPos).GetComponent<carta>().getAtaque();
        }
    }
    public void actualizarCuadrosDescartes()
    {

        int[] temporal = controles.GetNumDescartes();
        for (int i = 0; i < 5; i++)
        {
            if (temporal[i] != 0)
            {
                descartes[i].GetComponentInChildren<Text>().text = "" + temporal[i];
                descartes[i].gameObject.SetActive(true);
            }
            else
            {
                descartes[i].gameObject.SetActive(false);
            }
        }
    }
    public void eliminarNumerosDescarte()
    {
        for (int i = 0; i < 5; i++)
        {
            descartes[i].gameObject.SetActive(false);
        }
    }
    public void ActualizarMaterialCampo(int campo)
    {
        juego.SetCampoModificado(campo);
        string[] campos = { "Agua", "Oscuridad", "Montaña", "Pradera", "Bosque", "Yermo", "Oricalcos" };
        cambiarCampo.text = campos[campo];
        int cuadro1 = 0;
        int cuadro2 = 0;
        if (campo == 0)
        {
            cuadro1 = 0;

        }
        else if (campo == 1)
        {
            cuadro1 = 2;
        }
        else if (campo == 2)
        {
            cuadro1 = 4;

        }
        else if (campo == 3)
        {
            cuadro1 = 6;
        }
        else if (campo == 4)
        {
            cuadro1 = 8;
        }
        else if (campo == 5)
        {
            cuadro1 = 10;
        }
        else
        {
            cuadro1 = 12;
        }
        cuadro2 = cuadro1 + 1;
        cuadroUsuario.CambiarMaterial(campo, cuadro1, cuadro2);
    }
    //resulados del duelo
    public void MostrarTextoGanaPierde(string perdio)
    {
        int cantDeckUsuario = 40 - juego.GetCantDeckUsuario();
        ataquesEfectivos.text = juego.AtaquesEfectivos.ToString();
        defensaEfectivo.text = juego.DefensasEfectivas.ToString();
        fusionesCorrectas.text = juego.FusionCorrecta.ToString();
        equiposcorrectos.text = juego.EquiposCorrectos.ToString();
        magicasUsadas.text = juego.MagicasUsadas.ToString();
        trampasActivas.text = juego.TrampasActiadas.ToString();
        cartasBocaAbajo.text = juego.CartasBocaAbajo.ToString();
        turnosDUelo.text = juego.GetCantTurnos().ToString();
        atquePromedioDeck.text = juego.AtaquePromedioDeck.ToString();
        defensaPromedioDeck.text = juego.DefensaPromedioDeck.ToString();
        condicionesVictoria.text = juego.CondicionVictoria;
        int cantDeckCpu = 40 - juego.GetCantDeckCpu();
        textoAtaquePromedioCpu.text = "" + juego.ataquePromedioCpu;
        textoAtaquePromedioUsuario.text = "" + juego.ataquePromedio;
        textoCantCartasUsuario.text = "" + cantDeckUsuario;
        textoCantCartasCpu.text = "" + cantDeckCpu;
        textoDuelista.text = juego.GetDuelistaDuelo();
        textoEstrellas.text = "Estrellas " + juego.GetEstrellas();
        textoRango.text = juego.GetRango();
        string numeroCarta = juego.GetIdCarta().ToString();
        if (numeroCarta.Length == 1)
        {
            textoNumeroCarta.text = "00" + juego.GetIdCarta();
        }
        else if (numeroCarta.Length == 2)
        {

            textoNumeroCarta.text = "0" + juego.GetIdCarta();
        }
        else
        {
            textoNumeroCarta.text = "" + juego.GetIdCarta();
        }
        textoNombreCarta.text = juego.GetnombreCarta();
        //rangos
        string rangos = juego.GetRango();
        if(juego.rankPoints >= 50)
        {
            pow.gameObject.SetActive(true);
        }
        else
        {
            tec.gameObject.SetActive(true);
        }
        if (rangos.Equals("S"))
        {
            textoRango.color = new Color(0.6f, 0.2f, 0.7f);
        }
        else if (rangos.Equals("A"))
        {
            textoRango.color = new Color(0.9f, 0.02f, 0.08f);
        }
        else if (rangos.Equals("B"))
        {
            textoRango.color = new Color(0.9f, 0.8f, 0.02f);
        }
        else if (rangos.Equals("C"))
        {
            textoRango.color = new Color(0.2f, 0.7f, 0.02f);
        }
        else if (rangos.Equals("D"))
        {
            textoRango.color = new Color(0.02f, 0.07f, 0.7f);
        }
        else
        {
            tec.gameObject.SetActive(false);
            pow.gameObject.SetActive(false);
            condicionesVictoria.text = "Sin Victoria";
            textoNumeroCarta.text = "";
            textoNombreCarta.text = "GANADOR " + juego.GetDuelistaDuelo();
            textoRango.color = new Color(0.4f, 0.22f, 0.19f);
        }
        if (perdio.Equals("usuario"))
        {
            StartCoroutine(AnimacionMostrarGanar());
        }
        else
        {
            StartCoroutine(AnimacionMostrarPerder());
        }
    }
    IEnumerator AnimacionMostrarGanar()
    {
        juego.DetenerMusica();
        juego.ReproducirGanar();
        textoGanar.gameObject.SetActive(true);
        while (textoGanar.GetComponent<Transform>().localPosition.y <= 180)
        {
            textoGanar.gameObject.transform.Translate(0f * Time.deltaTime, 350f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        while (controles.Getfase().Equals("letrasFin"))
        {
            yield return new WaitForSeconds(5f);
            controles.SetFase("resultadoDUelo");
        }
        if (controles.Getfase().Equals("saltarLetras"))
        {
            juego.DetenerMusica();
        }
        while (textoGanar.GetComponent<Transform>().localPosition.y >= -280)
        {
            textoGanar.gameObject.transform.Translate(0f * Time.deltaTime, -400f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        textoLpUsuario.text = "" + juego.GetVidaUsuario();
        textoLpCpu.text = "" + juego.GetVidaCpu();
        juego.DetenerMusica();
        juego.ReproducirFinDUelo();
        controles.SetFase("resultadoDuelo");
        resultadoDUelo.SetActive(true);
        panelGameOver.SetActive(true);
        if (juego.nuevaCarta == true)
        {
            textoNuevaCarta.gameObject.SetActive(true);
        }
        cuadroUsuario.EmpezarAnimacionInfinita();
        DesactivarTodosComponentes();
    }
    IEnumerator AnimacionMostrarPerder()
    {
        juego.DetenerMusica();
        juego.ReproducirPerder();
        textoPerder.gameObject.SetActive(true);
        while (textoPerder.GetComponent<Transform>().localPosition.y <= 180)
        {
            textoPerder.gameObject.transform.Translate(0f * Time.deltaTime, 300f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        while (controles.Getfase().Equals("letrasFin"))
        {
            yield return new WaitForSeconds(6f);
            controles.SetFase("resultadoDUelo");
        }
        if (controles.Getfase().Equals("saltarLetras"))
        {
            juego.DetenerMusica();

        }
        while (textoPerder.GetComponent<Transform>().localPosition.y >= -280)
        {
            textoPerder.gameObject.transform.Translate(0f * Time.deltaTime, -400f * Time.deltaTime, 0f * Time.deltaTime);
            yield return null;
        }
        if (controles.Getfase().Equals("saltarLetras"))
        {
            juego.ReproducirFinDUelo();
        }
        textoLpUsuario.text = "" + juego.GetVidaUsuario();
        textoLpCpu.text = "" + juego.GetVidaCpu();
        juego.DetenerMusica();
        juego.ReproducirFinDUelo();
        DesactivarTodosComponentes();
        controles.SetFase("resultadoDuelo");
        resultadoDUelo.SetActive(true);
        panelGameOver.SetActive(true);
        cuadroUsuario.EmpezarAnimacionInfinita();
    }
   
    public void UbicrMTCpu(string fase)
    {
        apuntador.PosUbicacionPorCartaCpu(fase);
    }
    public void MoverapuntadorporCamanra(bool estado)
    {
        apuntador.SetEsMt(estado);
    }
    public bool ObtenerEsMt()
    {
        return apuntador.GetEsMt();
    }



}




