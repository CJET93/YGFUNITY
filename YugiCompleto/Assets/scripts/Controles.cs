using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controles : MonoBehaviour
{
    public int indice = 0;
    public Flecha flecha;
    public CuadroUsuario cuadroUsuario;
    public Apuntador apuntador;
    public ApuntadorAtaque apuntadorAtaque;
    public Interfaz interfaz;
    public ClonCarta clon;
    public string fase = "";
    public Camara camara;
    public Campo campo;
    public Juego juego;
    public int[] valoresCampo = new int[10];
    private bool teclaActivada;
    public int indiceAtaque;
    public int indiceAtaqueCpu;
    private bool indiceGuardian;
    public List<int> validarCarta = new List<int>();
    public int[] descarta = new int[5];
    public int[] numerosDescarte = new int[5];
    private int contadorDescartes = 1;
    public bool esMT;
    private int indiceEquipo;
    private int indiceAtaqueAUsuario;
    public int indiceMirarCampo;

    void Start()
    {
        indiceAtaqueAUsuario = 0;
        indiceAtaque = 0;
        indiceEquipo = 0;
        indiceAtaqueCpu = 0;
        descarta[0] = 0;
        descarta[1] = 0;
        descarta[2] = 0;
        descarta[3] = 0;
        descarta[4] = 0;
        indiceGuardian = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (fase.Equals("mano"))
            {

                if (indice < 4)
                {
                    indice++;
                    flecha.MoverCursorDerecha();
                    interfaz.ActualizarUi(indice);
                    juego.ReproducirEfectoMover();
                }
            }
            else if (fase.Equals("posicionMano"))
            {
                fase = "";
                clon.MostrarCarta(indice);
            }
            else if (fase.Equals("ubicarCarta"))
            {
                if (indice < 4 && esMT == false)
                {
                    indice++;
                    apuntador.MoverCursorDerecha();
                    juego.ReproducirEfectoMover();
                }
                else if (indice > 4 && indice < 9 && esMT == true)
                {
                    indice++;
                    apuntador.MoverCursorDerecha();
                    juego.ReproducirEfectoMover();
                }
                interfaz.ActualizarUIUsuario(indice);

            }
            else if (fase.Equals("acabarTurno") || fase.Equals("activarCartaEquipoCampo")|| fase.Equals("mirarCampo"))
            {

                if (indice < 4 && esMT == false)
                {
                    indice++;
                        interfaz.ActivarDatosUI(indice);
                    apuntador.MoverCursorDerecha();
                    juego.ReproducirEfectoMover();
                }
                else if (indice > 4 && indice < 9 && esMT == true)
                {
                    indice++;
                        interfaz.ActivarDatosUI(indice);
                    apuntador.MoverCursorDerecha();
                    juego.ReproducirEfectoMover();
                }

            }
            else if (fase.Equals("atacar"))
            {

                if (indiceAtaque != 0)
                {
                    juego.ReproducirEfectoMover();
                        indiceAtaque--;
                        if (clon.GetCartaCpu(indiceAtaque) != null && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            juego.ColorAtributo(indice, indiceAtaque);
                        }

                    interfaz.ActualizarUICpu(indiceAtaque);
                        apuntadorAtaque.MoverCursorDerecha();
                    
                }
            }
            else if (fase.Equals("mirarCpu"))
            {
                if (indice > 0)
                {
                    juego.ReproducirEfectoMover();

                    int contador = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (campo.GetCampoCpu(i) == 0)
                        {
                            contador++;
                        }
                    }
                    if (contador < 5)
                    {
                       
                        indice--;
   
                            interfaz.ActualizarUICpuCampo(indice);

                        apuntador.MoverCursorDerecha();
                    }
                    else
                    {
                   
                        indice--;
                        apuntador.MoverCursorDerecha();
                    }
                }
            }
            else if (fase.Equals("resultadoDuelo"))
            {
                if(interfaz.resultadoDUelo.activeInHierarchy)
                {
                    interfaz.resultadoDUelo.SetActive(false);
                    interfaz.resultadoDuelo2.SetActive(true);
                }
                else
                {
                    interfaz.resultadoDUelo.SetActive(true);
                    interfaz.resultadoDuelo2.SetActive(false);
                }
                //cargar transicion
            }



        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (fase.Equals("mano"))
            {
                if (indice > 0)
                {
                    indice--;
                    flecha.MoverCursorIzquierda();
                    interfaz.ActualizarUi(indice);
                    juego.ReproducirEfectoMover();
                }
            }
            else if (fase.Equals("posicionMano"))
            {
                fase = "";
                clon.MostrarCarta(indice);
            }
            else if (fase.Equals("ubicarCarta"))
            {
               
                if (indice > 0 && esMT == false)
                {
                    indice--;
                    apuntador.MoverCursorIzquierda();
                    juego.ReproducirEfectoMover();
                }
                else if (indice > 5 && indice <= 9 && esMT == true)
                {
                    indice--;
                    apuntador.MoverCursorIzquierda();
                    juego.ReproducirEfectoMover();
                }

                interfaz.ActualizarUIUsuario(indice);
            }
            else if (fase.Equals("acabarTurno") || fase.Equals("activarCartaEquipoCampo") || fase.Equals("mirarCampo"))
            {

                if (indice > 0 && esMT == false)
                {
                    indice--;
                        interfaz.ActivarDatosUI(indice);
                    apuntador.MoverCursorIzquierda();
                    juego.ReproducirEfectoMover();
                }
                else if (indice > 5 && indice <= 9 && esMT == true)
                {
                    indice--;               
                        interfaz.ActivarDatosUI(indice);                    
                    apuntador.MoverCursorIzquierda();
                    juego.ReproducirEfectoMover();
                }

            }
            else if (fase.Equals("atacar"))
            {

                if (indiceAtaque != 4)
                {
                    juego.ReproducirEfectoMover();
                        indiceAtaque++;
                        if (clon.GetCartaCpu(indiceAtaque) != null && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            juego.ColorAtributo(indice, indiceAtaque);
                        }
                            interfaz.ActualizarUICpu(indiceAtaque);
                        apuntadorAtaque.MoverCursorIzquierda();
                }
            }
            else if (fase.Equals("mirarCpu"))
            {
                if (indice < 4)
                {

                    juego.ReproducirEfectoMover();
                    int contador = 0;

                    for (int i = 0; i < 5; i++)
                    {
                        if (campo.GetCampoCpu(i) == 0)
                        {
                            contador++;
                        }
                    }
                    if (contador < 5)
                    {
                        
                        indice++;
                            interfaz.ActualizarUICpuCampo(indice);
                        apuntador.MoverCursorIzquierda();
                    }
                    else
                    {
                       
                        indice++;
                        apuntador.MoverCursorIzquierda();

                    }
                }
            }
            else if (fase.Equals("resultadoDuelo"))
            {
                if (interfaz.resultadoDUelo.activeInHierarchy)
                {
                    interfaz.resultadoDUelo.SetActive(false);
                    interfaz.resultadoDuelo2.SetActive(true);
                }
                else
                {
                    interfaz.resultadoDUelo.SetActive(true);
                    interfaz.resultadoDuelo2.SetActive(false);
                }
                //cargar transicion
            }



        }
      
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (fase.Equals("mano"))
            {
                if (validarCarta.Count > 1)
                {
                    indiceMirarCampo = indice;
                    bool hayMonstruo = false;
                    for (int i = 0; i < validarCarta.Count; i++)
                    {
                        if (clon.getClon(validarCarta[i]).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                        {
                            hayMonstruo = true;
                        }
                    }
                    if (hayMonstruo == true)
                    {
                        
                            fase = "";
                        bool salir = false;
                        esMT = false;
                        for (int i = 0; i < 5 && salir == false; i++)
                        {
                            if (clon.getCartaCampoU(i) == null)
                            {
                                indice = i;
                                salir = true;
                            }
                        }
                        apuntador.MoverCursor(indice, esMT);
                        clon.UbicarFusion(indice);
                            flecha.gameObject.SetActive(false);
                            juego.ReproducirEfectoSeleccionar();
                       


                    }
                    else
                    {
                        juego.ReproducirNoFusion();
                    }

                }
                else
                {
                  
                    if (validarCarta.Count == 0)
                    {
                        fase = "";
                        for (int i = 0; i < 5; i++)
                        {
                            if (i != indice)
                            {
                               // clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                //clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                                clon.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(0.5f, 0.5f, 0.5f, 1f);


                            }
                        }
                        clon.SetTransformacion(indice);
                        flecha.gameObject.SetActive(false);
                        juego.ReproducirEfectoSeleccionar();
                    }
                    else
                    {
                        juego.ReproducirNoFusion();
                    }

                }

            }
            else if (fase.Equals("posicionMano")) {
                indiceMirarCampo = indice;
                if (clon.getClon(indice).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                {
                   
                    

                    fase = "";
                    esMT = false;
                    clon.Transformacion2(indice);
                    bool salir = false;
                    for (int i = 0; i < 5 && salir == false; i++)
                    {
                        if (clon.getCartaCampoU(i) == null)
                        {
                            indice = i;
                            salir = true;
                        }
                    }
                    apuntador.MoverCursor(indice, esMT);
                    juego.ReproducirEfectoSeleccionar();
                    apuntador.PosUbicacionPorCarta(false);
                }
                else
                {
                    if (clon.getClon(indice).GetComponent<carta>().GetDatosCarta() == 1)
                    {
                        if (!clon.getClon(indice).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                        {
                            //accionar el efecto correspondiente
                            fase = "efectoMano";
                            clon.EfectosCartasUsuarioMano(indice);
                        }
                        else
                        {
                           
                            esMT = false;
                            clon.Transformacion2(indice);
                            clon.SetPos(indice);
                            indice = 0;
                            //apuntador.OffSetsPorFusion();
                            apuntador.PosUbicacionPorCarta(false);
                        
                            apuntador.MoverCursor(0, esMT);
                            //StartCoroutine(AnimacionEquipoMano());
                           

                        }

                    }
                    else
                    {
                        fase = "";
                       
                    

                        clon.Transformacion2(indice);
                        bool salir = false;
                        esMT = true;
                        for (int i = 5; i < 10 && salir == false; i++)
                        {
                            indice = i;
                            if (clon.getCartaCampoU(i) == null)
                            {
                                indice = i;
                                salir = true;
                            }
                        }
                        apuntador.MoverCursor(indice, true);
                        apuntador.PosUbicacionPorCarta(true);
                        juego.ReproducirEfectoSeleccionar();
                    }
                }


            }
            else if (fase.Equals("ubicarCarta"))
            {
                fase = "";
                interfaz.SetEstadoFlecha(false);
                //interfaz.datosCarta.SetActive(false);
                //interfaz.mano.SetActive(false);

                interfaz.eliminarNumerosDescarte();
                juego.ReproducirEfectoSeleccionar();
                if (esMT == false)
                {
                    clon.ColocarGuardian(indice);
                }
                else
                {
                    clon.ColocarCarta(indice, false);
                }


            }
            else if (fase.Equals("ponerGuardian"))
            {
                fase = "";
                interfaz.EstadoGuardianes(false);
                juego.ReproducirEfectoSeleccionar();
                clon.ColocarCarta(indice, indiceGuardian);

            }


        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (fase.Equals("posicionMano"))
            {
                fase = "";
                clon.CancelarCarta(indice);
                //interfaz.SetEstadoFlecha(true);
                juego.ReproducirCancelarAccion();
            }
            else if (fase.Equals("atacar"))
            {

                apuntadorAtaque.gameObject.SetActive(false);
                fase = "";

                StartCoroutine(AnimacionCancelar());

            }
            else if (fase.Equals("activarEfecto"))
            {
                fase = "acabarTurno";
                clon.CancelarActivacion(indice);
                juego.ReproducirCancelarAccion();
            }
            else if (fase.Equals("activarCartaEquipoCampo"))
            {
                indice = indiceEquipo;
                fase = "acabarTurno";
                esMT = true;
                clon.CancelarActivacionEquipo(indiceEquipo);
                apuntador.CargarPosicion();
                juego.ReproducirCancelarAccion();
                interfaz.ActivarDatosUI(indice);

            }
            else if (fase.Equals("ubicarCarta"))
            {
                fase = "";
                StartCoroutine(AnimacionCancelarUbicacion());
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (fase.Equals("acabarTurno"))
            {
 
                apuntador.gameObject.SetActive(false);
               
                for (int i = 0; i < 5; i++)
                {
                    if (campo.GetCampoCpu(i) != 0)
                    {
                        if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                        {
                            clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                        }
                        else
                        {
                            clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, -90f);
                        }
                    }
                    if (campo.GetCampoCpu(i+5) == 0)
                    {
                        campo.SetZonasMT(i, 0);
                    }

                }

                fase = "";
                for(int i = 0; i < 5; i++)
                {
                    if (clon.getClon(i) != null)
                    {
                        clon.getClon(i).transform.localPosition = new Vector3(clon.getClon(i).transform.localPosition.x, 77.95f, 0f);
                    }
                   
                }
                indiceGuardian = true;
                juego.SetPrimerAtaque(true);
                juego.SetTurnoUsuario(false);
                clon.OrganizarMano();
                interfaz.DesactivarComponentes();
                clon.InactivateComponent(clon.clon);
                camara.DevolverCamara(false);
                cuadroUsuario.ActivarAnimacion(indice);
                indice = 0;
                apuntadorAtaque.posicionX = apuntadorAtaque.transform.position.x;
                flecha.Reiniciar();
                apuntador.ReiniciarCpu();
                if (juego.GetCantTurnos() != 0)
                {
                    apuntadorAtaque.ReiniciarCpu();
                }
                juego.ReproducirCambiarTurno();

            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (fase.Equals("acabarTurno"))
            {
                if (esMT == false)
                {
                    if (campo.GetAtaquesUsuario(indice) == 1)
                    {
                        clon.CambiarPosCarta(indice);
                    }
                }


            }
            else if (fase.Equals("mano"))
            {
                flecha.gameObject.SetActive(false);
                clon.contenedor.SetActive(false);
                teclaActivada = false;
                fase = "devolver";
                indice = 0;
                StartCoroutine(AnimacionVerCartaCpu());


            }

        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (fase.Equals("mirarCpu"))
            {

                fase = "";
                StartCoroutine(AnimacionDevolverAMano());

            }
            else if (fase.Equals("devolver"))
            {
                teclaActivada = true;
            }

        }
        if (fase.Equals("regresarDeCpu")) {
            fase = "";
            StartCoroutine(AnimacionDevolverAMano());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (fase.Equals("mano"))
            {
               
                interfaz.panelMirarTablero.SetActive(true);
                esMT = false;
                indiceMirarCampo= indice;
                indice = 0;
                interfaz.ActivarGuardianStar(true);
                fase = "mirarCampo";

                interfaz.ActivarDatosUI(0);
                
               
                apuntador.gameObject.SetActive(true);
                apuntador.OffSetsPorFusion();
                clon.MirarCampo();
            }
            else if (fase.Equals("mirarCampo"))
            {

                apuntador.gameObject.SetActive(false);
                apuntador.Reiniciar();
                indice = indiceMirarCampo;
                interfaz.ActualizarUi(indice);
                fase = "";
                interfaz.panelMirarTablero.SetActive(false);
                clon.Mano();
                
            }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (fase.Equals("ponerGuardian"))
            {
                if (indiceGuardian == false)
                {

                    indiceGuardian = true;
                    flecha.MoverCursorArriba();
                    juego.ReproducirEfectoMover();
                }
            }
            else if (fase.Equals("mano"))
            {
                if (descarta[indice] == 0)
                {
                    juego.ReproducirDescarte();
                    numerosDescarte[indice] = contadorDescartes;
                    interfaz.actualizarCuadrosDescartes();
                    clon.DescartaUsuario(indice, false);
                    descarta[indice] = 1;
                    validarCarta.Add(indice);
                    ComprobanteDescarte();
                    contadorDescartes++;
                }

            }
            else if ((fase.Equals("acabarTurno") && esMT == true) || (fase.Equals("mirarCampo") && esMT == true))
            {
                juego.ReproducirEfectoMover();
                esMT = false;
                indice = indice - 5;
                if (clon.getCartaCampoU(indice) != null)
                {
                    interfaz.ActivarGuardianStar(true);
                    interfaz.ActivarDatosUI(indice);


                }
                else
                {
                    interfaz.ActivarDatosUI(indice);
                }
                apuntador.MoverCursorArriba();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (fase.Equals("ponerGuardian"))
            {
                if (indiceGuardian == true)
                {
                    indiceGuardian = false;
                    flecha.MoverCursorAbajo();
                    juego.ReproducirEfectoMover();
                }
            }
            else if (fase.Equals("mano"))
            {
                if (descarta[indice] != 0)
                {
                    bool salir = false;
                    for (int i = 0; i < 5 && salir == false; i++)
                    {

                        if (numerosDescarte[i] > numerosDescarte[indice])
                        {
                            numerosDescarte[i] = numerosDescarte[i] - 1;
                        }
                    }
                    for (int i = 0; i < 5 && salir == false; i++)
                    {
                        if (validarCarta[i] == indice)
                        {
                            validarCarta.RemoveAt(i);
                            salir = true;
                        }
                    }
                    numerosDescarte[indice] = 0;
                    interfaz.actualizarCuadrosDescartes();
                    clon.DescartaUsuario(indice, true);
                    descarta[indice] = 0;
                    ComprobanteDescarte();
                    contadorDescartes--;
                }

            }
            else if ((fase.Equals("acabarTurno") && esMT == false) || (fase.Equals("mirarCampo") && esMT == false))
            {
                juego.ReproducirEfectoMover();
                esMT = true;
                indice = indice + 5;
                if (clon.getCartaCampoU(indice) != null)
                {
                    interfaz.ActivarDatosUI(indice);
                }
                else
                {
                    interfaz.ActivarDatosUI(indice);
                }
                apuntador.MoverCursorAbajo();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (fase.Equals("resultadoDuelo"))
            {
                juego.DueloRapido();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            if (fase.Equals("letrasFin"))
            {
                fase = "saltarLetras";
            }
            else if (fase.Equals("resultadoDuelo"))
            {
                cuadroUsuario.terminarAnimacionInfinita = true;
                juego.FinDuelo();
                //cargar transicion
            }
            if (fase.Equals("acabarTurno") && clon.getCartaCampoU(indice) != null && !clon.getCartaCampoU(indice).GetComponent<carta>().GetTipoCarta().Equals("Monstruo")) {
                if (!clon.getCartaCampoU(indice).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                {
                    interfaz.SetEstadoMano(false);
                    fase = "activarEfecto";
                    clon.Transormacion3(indice);
                    //clon.EfectosCartasUsuarioMano(indice);
                }
                else
                {
                    indiceEquipo = indice;
                    indice = 0;
                    esMT = false;
                    interfaz.ActivarGuardianStar(true);
                    apuntador.SetPosicion();
                    interfaz.SetEstadoMano(false);
                    interfaz.ActivarDatosUI(0);
                    apuntador.Reiniciar();
                    fase = "activarCartaEquipoCampo";
                    clon.Transformacion4(indiceEquipo);
                    //para activar los equipos desde el campo

                }


            }
            else if (fase.Equals("activarCartaEquipoCampo")) {
                if (clon.getCartaCampoU(indice) != null)
                {
                    apuntador.gameObject.SetActive(false);
                    fase = "";
                    camara.DevolverCamara(false);

                    clon.ActivarEquipo(indiceEquipo, indice);
                }
                else
                {
                    juego.ReproducirNoFusion();
                }
                //ACTIVAR Y REVISAR CARTA EQUIPO 
            }
            else if (fase.Equals("activarEfecto"))
            {
                fase = "efectoCampo";
                camara.DevolverCamara(false);
                clon.EfectosCartasUsuarioMano(indice);
            }
            else
            {
                if (juego.GetCantTurnos() > 0)
                {
                    if (fase.Equals("acabarTurno") && clon.getCartaCampoU(indice) != null && clon.getCartaCampoU(indice).GetComponent<carta>().getPos() == 1 && campo.GetAtaquesUsuario(indice) == 1)
                    {
                        interfaz.guardianFavorable.enabled = false;
                        interfaz.SetEstadoDatosCartaCpu(true);
                        fase = "";
                        juego.ReproducirEfectoSeleccionar();

                        Color c;
                        int contador = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            if (campo.GetCampoCpu(i) == 0)
                            {
                                contador++;
                            }
                            else
                            {

                                if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                                {
                                    clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
                                }
                                else
                                {
                                    clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 90f);
                                }
                            }
                        }
                        if (contador < 5 && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            c = Color.green;
                            if (campo.GetCampoCpu(indiceAtaque) != 0)
                            {
                                juego.ColorAtributo(indice, indiceAtaque);
                            }
                            if (campo.GetCampoCpu(0) == 0 || juego.GetEspadasLuzReveladora().Contains("cpu"))
                            {
                                c = Color.red;
                            }
                        }
                        else
                        {
                            if (!juego.GetEspadasLuzReveladora().Contains("cpu"))
                            {
                                c = Color.green;
                            }
                            else
                            {
                                c = Color.red;
                            }

                        }
                        cuadroUsuario.TableroBatallaUsuario(true);

                        camara.FijarAtaque();
                       
 
                        apuntadorAtaque.gameObject.SetActive(true);
                        StopAllCoroutines();
                        StartCoroutine(AnimacionCamaraAtaque());
                        //cuadroUsuario.actualizarCuadroCpu(c, 0);
                    }
                    else if (fase.Equals("atacar"))
                    {
                        int contador = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            if (campo.GetCampoCpu(i) == 0)
                            {
                                contador++;
                            }
                        }

                        if (contador < 5)
                        {
                            if (campo.GetCampoCpu(indiceAtaque) != 0 && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                            {
                                fase = "";
                                cuadroUsuario.TableroBatallaUsuario(false);
                                cuadroUsuario.CuadrosPosBatalla();
                                camara.DevolverCamara(false);
                                interfaz.SetEstadoPanel(false);
                                juego.ReproducirEfectoSeleccionar();
                                clon.SetEstadoManos(false,false);
                                interfaz.DesactivarComponentes();
                                interfaz.SetEstadoDatosCartaCpu(false); 
                                campo.SetAtaquesUsuario(indice, 0);
                                interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
                                if (juego.ParametrosActivacionTrampas(indice, 0) == -1)
                                {
                                    clon.GetCartaCpu(indiceAtaque).GetComponent<carta>().SetDatosCarta(1);
                                    juego.InicarBatalla(indice, indiceAtaque, "ataqueUsuario");
                                }
                                else
                                {
                                    juego.InicarBatalla(indice, juego.ParametrosActivacionTrampas(indice, 0), "trampaCpu");
                                }

                            }

                        }
                        else
                        {
                            if (!juego.GetEspadasLuzReveladora().Contains("cpu"))
                            {
                                fase = "";
                                cuadroUsuario.TableroBatallaUsuario(false);
                                cuadroUsuario.CuadrosPosBatalla();
                                camara.DevolverCamara(false);
                                interfaz.SetEstadoPanel(false);
                                juego.ReproducirEfectoSeleccionar();
                                clon.SetEstadoManos(false,false);
                                interfaz.DesactivarComponentes();
                                interfaz.SetEstadoDatosCartaCpu(false);
                                campo.SetAtaquesUsuario(indice, 0);
                                interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
                                if (juego.ParametrosActivacionTrampas(indice, 0) == -1)
                                {
                                    juego.InicarBatalla(indice, 0, "ataqueDirecto");
                                }
                                else
                                {
                                    juego.InicarBatalla(indice, juego.ParametrosActivacionTrampas(indice, 0), "trampaCpu");
                                }

                            }

                        }
                    }

                }

            }




        }



    }
    public int GetIndice()
    {
        return indice;
    }
    public void SetIndice(int i)
    {
        indice = i;
    }
    public void SetFase(string fase)
    {
        this.fase = fase;
    }
    public string Getfase()
    {
        return fase;
    }
    public void ManoCpu(int pos)
    {
        StopAllCoroutines();
        StartCoroutine(SeleccionarCartaCpu(pos));
    }
    IEnumerator SeleccionarCartaCpu(int pos)
    {
        int seleccionada = 0;
        contadorDescartes = 1;
        if (validarCarta.Count > 0)
        {
            int fusiones = validarCarta.Count;
            while (fusiones > 0 && seleccionada < 5)
            {
                if (validarCarta.Contains(seleccionada))
                {
                    clon.GetClonCpu(seleccionada).transform.Translate(0f, 10f, 0f);
                    numerosDescarte[seleccionada] = contadorDescartes;
                    interfaz.actualizarCuadrosDescartes();
                    fusiones--;
                    contadorDescartes++;
                    juego.ReproducirDescarte();
                    yield return new WaitForSeconds(0.2f);
                }

                yield return new WaitForSeconds(0.2f);
                juego.ReproducirEfectoMover();
                seleccionada++;
                flecha.MoverCursorDerecha();
            }
            interfaz.eliminarNumerosDescarte();
            juego.ReproducirEfectoSeleccionar();
            flecha.gameObject.SetActive(false);
            indice = 0;
            clon.VerificarPosCpu(0);
        }
        else if (clon.GetValidadorFusionCpu().Count > 0)
        {
            pos = clon.GetValidadorFusionCpu()[0];

            while (seleccionada - 1 < pos)
            {

                yield return new WaitForSeconds(0.15f);
                indice++;
                juego.ReproducirEfectoMover();
                seleccionada++;
                flecha.MoverCursorDerecha();


            }
            juego.ReproducirEfectoSeleccionar();
            flecha.gameObject.SetActive(false);
            indice = 0;
            clon.SetTransformacionCpu(seleccionada - 1);
        }
        else
        {

            seleccionada = 1;
            flecha.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);



            while (seleccionada - 1 < pos)
            {

                yield return new WaitForSeconds(0.1f);
                indice++;
                juego.ReproducirEfectoMover();
                seleccionada++;
                flecha.MoverCursorDerecha();
            }

            yield return new WaitForSeconds(0.1f);
            flecha.MoverCursorDerecha();
            juego.ReproducirEfectoSeleccionar();
            flecha.gameObject.SetActive(false);
            indice = 0;
            if (!clon.GetClonCpu(seleccionada - 1).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {
                indice = 5;
            }

            clon.SetTransformacionCpu(seleccionada - 1);
        }



        //clon.SetTransformacionCpu(indice);


    }
    public void MoverenCampoCpu(int pos, int posCarta,bool ZonaCampo)
    {
        
        StartCoroutine(AnimacionMoverCuadroCpu(pos, posCarta,ZonaCampo));

    }
    IEnumerator AnimacionMoverCuadroCpu(int pos, int posCarta , bool ZonaCampo)
    {

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(AnimacionMoverCuadroCpu2(pos, posCarta,ZonaCampo));

    }
    IEnumerator AnimacionMoverCuadroCpu2(int pos, int posCarta, bool ZonaCampo)
    {
        if (ZonaCampo == true)
        {
            indice = 0;
            int cuadro = 0;
            if (!clon.GetClonCpu(posCarta).GetComponent<carta>().GetTipoCarta().Equals("Monstruo") && validarCarta.Count < 1)
            {
                indice = 5;
                cuadro = 5;
            }

            while (cuadro != pos)
            {
                indice++;
                cuadro++;
                if (pos != 0)
                {
                    juego.ReproducirEfectoMover();
                }
                
                apuntador.MoverCursorDerecha();
                interfaz.ActualizarUICpu(indice);
                yield return new WaitForSeconds(0.15f);
            }
            if (validarCarta.Count > 0)
            {
                
                camara.MoverCamara(false);
                while (interfaz.datosCarta.transform.position.y < 44.1f)
                {
                    float posicionar = 600 * Time.deltaTime;

                    interfaz.datosCarta.transform.Translate(0f, posicionar, 0f);

                    yield return null;
                }
                yield return new WaitForSeconds(0.2f);
                apuntador.gameObject.SetActive(true);
                juego.ReproducirEfectoSeleccionar();
                clon.ColocarCartaCpu(posCarta, indice);
            }
            else
            {
                interfaz.ActualizarUICpu(indice);
                juego.ReproducirEfectoSeleccionar();
                apuntador.gameObject.SetActive(false);
                interfaz.ActualizarUICpu(indice);
                camara.DevolverCamara(false);
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
                        if (i != posCarta)
                            clon.GetClonCpu(i).transform.Translate(0f, posicionar, 0f);
                    }

                    yield return null;
                }
                clon.InactivateComponent(clon.clonCpu,posCarta);
               
                clon.InicioColocarCartaCpu(posCarta, indice);
            }
          
        }
        else
        {
            indice = pos;

      
            if (validarCarta.Count > 0)
            {
                interfaz.ActualizarUICpu(indice);
                camara.MoverCamara(false);
                while (interfaz.datosCartaCpu.transform.localPosition.y < -60f)
                {
                    float posicionar = 600 * Time.deltaTime;

                    interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

                    yield return null;
                }
                interfaz.datosCartaCpu.transform.localPosition = new Vector2(0, -47f);
                yield return new WaitForSeconds(0.5f);
                apuntador.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                apuntador.gameObject.SetActive(false);


                juego.ReproducirEfectoSeleccionar();
                
                camara.DevolverCamara(false);
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
                        if (!validarCarta.Contains(i))
                            clon.GetClonCpu(i).transform.Translate(0f, posicionar, 0f);
                    }

                    yield return null;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (!validarCarta.Contains(i))
                        clon.GetClonCpu(i).gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(0.2f);
                clon.ColocarCartaCpu(posCarta, indice);
            }
            else
            {
                interfaz.ActualizarUICpu(indice);
                apuntador.gameObject.SetActive(true);
                juego.ReproducirEfectoSeleccionar();
                apuntador.gameObject.SetActive(false);
                camara.DevolverCamara(false);
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
                        if (i != posCarta)
                            clon.GetClonCpu(i).transform.Translate(0f, posicionar, 0f);
                    }

                    yield return null;
                }
                clon.InactivateComponent(clon.clonCpu,posCarta);
                yield return new WaitForSeconds(0.2f);
                clon.InicioColocarCartaCpu(posCarta, indice);
            }
        }

        StopAllCoroutines();

    }
    public void AtaqueDirectoCpu(int posUsuario, int posCpu, string ataque)
    {
        interfaz.guardianFavorable.enabled = false;
        indiceAtaqueCpu = posCpu;
        StartCoroutine(AnimacionAtaqueDirectoCpu(posUsuario, posCpu, ataque));
    }
    IEnumerator AnimacionAtaqueDirectoCpu(int i, int j, string ataque)
    {
        interfaz.ActualizarUIUsuario(0);
        interfaz.datosCartaCpu.SetActive(true);
        while (interfaz.datosCartaCpu.transform.localPosition.y < -60f)
        {
            float posicionar = 1200 * Time.deltaTime;

            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0, -47f);
        apuntadorAtaque.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        while (indiceAtaqueAUsuario != 2)
        {
            juego.ReproducirEfectoMover();
            if (2 > indiceAtaqueAUsuario)
            {
                indiceAtaqueAUsuario++;
                apuntadorAtaque.MoverCursorIzquierda();
            }
            else
            {
                indiceAtaqueAUsuario--;
                apuntadorAtaque.MoverCursorDerecha();
            }

            yield return new WaitForSeconds(0.15f);
        }
        juego.ReproducirEfectoSeleccionar();
        cuadroUsuario.CuadrosPosBatalla();
        cuadroUsuario.TableroBatallaCpu(false);
        camara.DevolverCamara(false);
        interfaz.DesactivarComponentes();
        interfaz.datosCartaCpu.SetActive(false);
        indice = j;
        juego.InicarBatalla(i, j, ataque);
    }
    public void VolverCampo()
    {


        camara.MoverCamara(false);
        if (juego.GetTurnoUsuario() == true)
        {

            StartCoroutine(EsperarFaseBatallaUsuario());
        }
        else
        {

            StartCoroutine(EsperaLogicaCpu());
        }
    }
    IEnumerator EsperaLogicaCpu()
    {
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
        yield return new WaitForSeconds(0.7f);
        apuntador.gameObject.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            if (clon.getCartaCampoU(i) != null)
            {
                if (clon.getCartaCampoU(i).GetComponent<carta>().getPos() == 1)
                {
                    clon.getCartaCampoU(i).GetComponent<Transform>().eulerAngles = new Vector3(90, 0, 180);
                }
                else
                {
                    clon.getCartaCampoU(i).GetComponent<Transform>().eulerAngles = new Vector3(90, 90, 0);
                }

            }
        }
    
        interfaz.SetEstadoDatosC(true);
        interfaz.ActualizarUICpuCampo(indice);
        juego.InicioLogicaCpu();

    }
    IEnumerator EsperarFaseBatallaUsuario()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < 5; i++)
        {
            if (campo.GetCampoCpu(i) != 0)
            {
                if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                {
                    clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                }
                else
                {
                    clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, -20f);
                }

            }

        }
        apuntador.gameObject.SetActive(true);
        fase = "acabarTurno";
        //cuadroUsuario.actualizarCuadro(Color.red, indiceAtaque);
        interfaz.SetEstadoDatosC(true);
        interfaz.ActivarDatosUI(indice);
    }
    public void AcabarTurnoCpu()
    {
        //camara.MoverCamara();
        for (int i = 0; i < 5; i++)
        {
            if (clon.GetClonCpu(i) != null) {
                clon.GetClonCpu(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).GetComponent<muestraCarta>().reverso.color = new Color(1f, 1f, 1f, 1f);
                clon.GetClonCpu(i).transform.localPosition = new Vector3(clon.GetClonCpu(i).transform.localPosition.x, 77.95f, 0f);
            }

        }

        clon.OrganizarManoCpu();
        juego.ReproducirCambiarTurno();
        indice = 0;
        indiceAtaqueCpu = 0;
        cuadroUsuario.CuadrosPosBatalla();
        interfaz.DesactivarComponentes();
        interfaz.SetEstadoMano(false);
        interfaz.SetEstadoPanel(false);
        juego.SetTurnoUsuario(true);
        juego.SetCantTurnos();
        camara.DevolverCamara(false);
        interfaz.datosCartaCpu.SetActive(false);
        apuntadorAtaque.posicionXCpu = apuntadorAtaque.transform.position.x;
        apuntador.Reiniciar();
        apuntadorAtaque.Reiniciar();
        flecha.Reiniciar();
        cuadroUsuario.CampoUsuario(indice);
    }
    public void AtaqueCpu(int posUsuario, int posCpu, int pos, string ataque)
    {
        interfaz.guardianFavorable.enabled = false;
        indiceAtaqueCpu = posCpu;
        StartCoroutine(AnimacionAtaqueCpu(posUsuario, posCpu, pos, ataque));
    }
    IEnumerator AnimacionAtaqueCpu(int i, int j, int pos, string ataque)
    {
        interfaz.datosCartaCpu.SetActive(true);
        juego.ColorAtributo(i, j);
        interfaz.guardianFavorable.enabled = true;
        interfaz.guardianFavorable.color = juego.ColorAtributo();
        while (interfaz.datosCartaCpu.transform.localPosition.y < -60f)
        {
            float posicionar = 1200 * Time.deltaTime;

            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        interfaz.ActualizarUIUsuario(indiceAtaqueAUsuario);
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0, -47f);
        indice = 0;
        apuntadorAtaque.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        while (indiceAtaqueAUsuario != pos) 
           {
                juego.ReproducirEfectoMover();
                if (pos > indiceAtaqueAUsuario)
                {
                indiceAtaqueAUsuario++;
                if (clon.getCartaCampoU(indiceAtaqueAUsuario) != null)
                {
                    juego.ColorAtributo(indiceAtaqueAUsuario, j);
                }
              
                interfaz.ActualizarUIUsuario(indiceAtaque);
                apuntadorAtaque.MoverCursorIzquierda();
            }
                else
                {
                indiceAtaqueAUsuario--;
                if (clon.getCartaCampoU(indiceAtaqueAUsuario) != null)
                {
                    juego.ColorAtributo(indiceAtaqueAUsuario, j);
                }
                interfaz.ActualizarUIUsuario(indiceAtaque);
                apuntadorAtaque.MoverCursorDerecha();
            }
          
            yield return new WaitForSeconds(0.15f);
        }
        juego.ReproducirEfectoSeleccionar();
        cuadroUsuario.CuadrosPosBatalla();
        cuadroUsuario.TableroBatallaCpu(false);
        camara.DevolverCamara(false);
        interfaz.datosCartaCpu.SetActive(false);
        interfaz.SetEstadoPanel(false);
        interfaz.DesactivarComponentes();
        indice = j;
        juego.InicarBatalla(i, j, ataque);
    }
    public void SetIndiceAtaqueCpu(int pos)
    {
        indiceAtaqueCpu = pos;
    }
    public int GetIndiceAtaqueCpu()
    {
        return indiceAtaqueCpu;
    }
    public List<int> GetListaDCartas()
    {
        return validarCarta;
    }
    public int[] GetDescarte()
    {
        return descarta;
    }
    public int[] GetNumDescartes()
    {
        return numerosDescarte;
    }
    public void ReiniciarDescartes()
    {
        for (int i = 0; i < 5; i++)
        {
            numerosDescarte[i] = 0;
            descarta[i] = 0;
        }
    }
    public void ComprobanteDescarte()
    {
        if (validarCarta.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (descarta[i] == 0)
                {
                    clon.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    clon.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                    clon.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                //clon.getClon(i).GetComponent<Renderer>().sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(1f, 1f, 1f, 1f);
                clon.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(1f, 1f, 1f, 1f);
            }
        }


    }
    public void ReinicarContadorDescarte()
    {
        contadorDescartes = 1;
    }
    public void SetEsMt(bool estado)
    {
        esMT = estado;
    }
    public void BorrarElementos()
    {
        validarCarta.Clear();
    }
    IEnumerator AnimacionCancelar()
    {
        juego.ReproducirCancelarAccion();
        camara.CancelarAtaque();
        //cuadroUsuario.actualizarCuadroCpu(Color.gray, indice)
        while (interfaz.datosCartaCpu.transform.position.y > -180)
        {
            float posicionar = -1200 * Time.deltaTime;
            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        for (int i = 0; i < 5; i++)
        {
            if (campo.GetCampoCpu(i) != 0)
            {
                if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                {
                    clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                }
                else
                {
                    clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, -90f);
                }

            }

        }
        cuadroUsuario.TableroBatallaUsuario(false);
        fase = "acabarTurno";
        interfaz.ActivarDatosUI(indice);
    }
    IEnumerator AnimacionVerCartaCpu()
    {
        camara.MirarCampoCpu();
        interfaz.SetEstadoDatosCartaCpu(true);
        //cuadroUsuario.actualizarCuadroCpu(Color.green, indice);
        interfaz.ActualizarUICpuCampo(0);
        yield return new WaitForSeconds(0.5f);
        if (teclaActivada == true)
        {
            fase = "regresarDeCpu";
        }
        else {
            apuntador.ReiniciarMirarCpu();
            
            apuntador.gameObject.SetActive(true);
            fase = "mirarCpu";
        }
        flecha.Reiniciar();

    }
    IEnumerator AnimacionDevolverAMano()
    {
        camara.NoMirarCampoCpu();
        interfaz.SetEstadoDatosCartaCpu(false);
        cuadroUsuario.CuadrosPosBatalla();
        indice = 0;
        yield return new WaitForSeconds(0.3f);
        apuntador.Reiniciar();
        interfaz.ActualizarUi(0);
        flecha.gameObject.SetActive(true);
        clon.contenedor.SetActive(true);
        fase = "mano";
    }
    public int GetValoresCampo(int valor)
    {
        return valoresCampo[valor];
    }
    public int GetIndiceAtaque()
    {
        return indiceAtaque;
    }
    IEnumerator AnimacionFusion()
    {
        //apuntador.SetPos(apuntador.gameObject.transform.position);
        apuntador.OffSetsPorFusion();
        yield return null;
        fase = "ubicarCarta";
       
        bool salir = false;
        esMT = false;
        for (int i = 0; i < 5 && salir == false; i++)
        {
            if (clon.getCartaCampoU(i) == null)
            {
                indice = i;
                salir = true;
            }
        }

        apuntador.MoverCursor(indice, esMT);
        apuntador.gameObject.SetActive(true);
    }
    IEnumerator AnimacionEquipoMano()
    {
        camara.MoverCamara(false);
        yield return new WaitForSeconds(0.6f);
        esMT = false;
        clon.SetPos(indice);
        indice = 0;
        //apuntador.OffSetsPorFusion();
        apuntador.gameObject.SetActive(true);
        fase = "ubicarCarta";
    }
    IEnumerator AnimacionCamaraAtaque()
    {
        interfaz.guardianFavorable.color = juego.ColorAtributo();
        interfaz.ActualizarUICpu(indiceAtaque);
        while (interfaz.datosCartaCpu.transform.localPosition.y < -60f)
        {
            float posicionar = 1200 * Time.deltaTime;

            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
       
        interfaz.datosCartaCpu.transform.localPosition = new Vector2(0, -47f);
        fase = "atacar";
      
    }
    IEnumerator AnimacionCancelarUbicacion()
    {
        juego.ReproducirCancelarAccion();
        indice = indiceMirarCampo;
        camara.DevolverCamara(false);
        interfaz.SetEstadoApuntador(false);
        interfaz.ReiniciarApuntador();
        while (interfaz.datosCartaCpu.transform.position.y > -180)
        {
            float posicionar = -600 * Time.deltaTime;
            interfaz.datosCartaCpu.transform.Translate(0f, posicionar, 0f);

            yield return null;
        }
        if (validarCarta.Count < 1)
        {
            Vector3 final = new Vector3(18f, 219f, 0f);
            while (Vector3.Distance(clon.getClon(indice).transform.localPosition, final) > Time.deltaTime * 1800)
            {
                clon.getClon(indice).transform.localPosition = Vector3.MoveTowards(clon.getClon(indice).transform.localPosition, final, Time.deltaTime * 1500);
                yield return null;
            }
            clon.getClon(indice).transform.localPosition = final;
            fase = "posicionMano";
        }
        else
        {
            flecha.gameObject.SetActive(true);
            fase = "mano";
        }
      
    }

    public void BotonDerecha()
    {
        if (fase.Equals("mano"))
        {

            if (indice < 4)
            {
                indice++;
                flecha.MoverCursorDerecha();
                interfaz.ActualizarUi(indice);
                juego.ReproducirEfectoMover();
            }
        }
        else if (fase.Equals("posicionMano"))
        {
            fase = "";
            clon.MostrarCarta(indice);
        }
        else if (fase.Equals("ubicarCarta"))
        {
            if (indice < 4 && esMT == false)
            {
                indice++;
                apuntador.MoverCursorDerecha();
                juego.ReproducirEfectoMover();
            }
            else if (indice > 4 && indice < 9 && esMT == true)
            {
                indice++;
                apuntador.MoverCursorDerecha();
                juego.ReproducirEfectoMover();
            }
            interfaz.ActualizarUIUsuario(indice);

        }
        else if (fase.Equals("acabarTurno") || fase.Equals("activarCartaEquipoCampo") || fase.Equals("mirarCampo"))
        {

            if (indice < 4 && esMT == false)
            {
                indice++;
                interfaz.ActivarDatosUI(indice);
                apuntador.MoverCursorDerecha();
                juego.ReproducirEfectoMover();
            }
            else if (indice > 4 && indice < 9 && esMT == true)
            {
                indice++;
                interfaz.ActivarDatosUI(indice);
                apuntador.MoverCursorDerecha();
                juego.ReproducirEfectoMover();
            }

        }
        else if (fase.Equals("atacar"))
        {

            if (indiceAtaque != 0)
            {
                juego.ReproducirEfectoMover();
                indiceAtaque--;
                if (clon.GetCartaCpu(indiceAtaque) != null && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                {
                    juego.ColorAtributo(indice, indiceAtaque);
                }

                interfaz.ActualizarUICpu(indiceAtaque);
                apuntadorAtaque.MoverCursorDerecha();

            }
        }
        else if (fase.Equals("mirarCpu"))
        {
            if (indice > 0)
            {
                juego.ReproducirEfectoMover();

                int contador = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (campo.GetCampoCpu(i) == 0)
                    {
                        contador++;
                    }
                }
                if (contador < 5)
                {

                    indice--;

                    interfaz.ActualizarUICpuCampo(indice);

                    apuntador.MoverCursorDerecha();
                }
                else
                {

                    indice--;
                    apuntador.MoverCursorDerecha();
                }
            }
        }
        else if (fase.Equals("resultadoDuelo"))
        {
            if (interfaz.resultadoDUelo.activeInHierarchy)
            {
                interfaz.resultadoDUelo.SetActive(false);
                interfaz.resultadoDuelo2.SetActive(true);
            }
            else
            {
                interfaz.resultadoDUelo.SetActive(true);
                interfaz.resultadoDuelo2.SetActive(false);
            }
            //cargar transicion
        }

    }
    public void BotonIzquierda()
    {
        if (fase.Equals("mano"))
        {
            if (indice > 0)
            {
                indice--;
                flecha.MoverCursorIzquierda();
                interfaz.ActualizarUi(indice);
                juego.ReproducirEfectoMover();
            }
        }
        else if (fase.Equals("posicionMano"))
        {
            fase = "";
            clon.MostrarCarta(indice);
        }
        else if (fase.Equals("ubicarCarta"))
        {

            if (indice > 0 && esMT == false)
            {
                indice--;
                apuntador.MoverCursorIzquierda();
                juego.ReproducirEfectoMover();
            }
            else if (indice > 5 && indice <= 9 && esMT == true)
            {
                indice--;
                apuntador.MoverCursorIzquierda();
                juego.ReproducirEfectoMover();
            }

            interfaz.ActualizarUIUsuario(indice);
        }
        else if (fase.Equals("acabarTurno") || fase.Equals("activarCartaEquipoCampo") || fase.Equals("mirarCampo"))
        {

            if (indice > 0 && esMT == false)
            {
                indice--;
                interfaz.ActivarDatosUI(indice);
                apuntador.MoverCursorIzquierda();
                juego.ReproducirEfectoMover();
            }
            else if (indice > 5 && indice <= 9 && esMT == true)
            {
                indice--;
                interfaz.ActivarDatosUI(indice);
                apuntador.MoverCursorIzquierda();
                juego.ReproducirEfectoMover();
            }

        }
        else if (fase.Equals("atacar"))
        {

            if (indiceAtaque != 4)
            {
                juego.ReproducirEfectoMover();
                indiceAtaque++;
                if (clon.GetCartaCpu(indiceAtaque) != null && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                {
                    juego.ColorAtributo(indice, indiceAtaque);
                }
                interfaz.ActualizarUICpu(indiceAtaque);
                apuntadorAtaque.MoverCursorIzquierda();
            }
        }
        else if (fase.Equals("mirarCpu"))
        {
            if (indice < 4)
            {

                juego.ReproducirEfectoMover();
                int contador = 0;

                for (int i = 0; i < 5; i++)
                {
                    if (campo.GetCampoCpu(i) == 0)
                    {
                        contador++;
                    }
                }
                if (contador < 5)
                {

                    indice++;
                    interfaz.ActualizarUICpuCampo(indice);
                    apuntador.MoverCursorIzquierda();
                }
                else
                {

                    indice++;
                    apuntador.MoverCursorIzquierda();

                }
            }
        }
        else if (fase.Equals("resultadoDuelo"))
        {
            if (interfaz.resultadoDUelo.activeInHierarchy)
            {
                interfaz.resultadoDUelo.SetActive(false);
                interfaz.resultadoDuelo2.SetActive(true);
            }
            else
            {
                interfaz.resultadoDUelo.SetActive(true);
                interfaz.resultadoDuelo2.SetActive(false);
            }
            //cargar transicion
        }
    }
    public void BotonArriba()
    {
        if (fase.Equals("ponerGuardian"))
        {
            if (indiceGuardian == false)
            {

                indiceGuardian = true;
                flecha.MoverCursorArriba();
                juego.ReproducirEfectoMover();
            }
        }
        else if (fase.Equals("mano"))
        {
            if (descarta[indice] == 0)
            {
                juego.ReproducirDescarte();
                numerosDescarte[indice] = contadorDescartes;
                interfaz.actualizarCuadrosDescartes();
                clon.DescartaUsuario(indice, false);
                descarta[indice] = 1;
                validarCarta.Add(indice);
                ComprobanteDescarte();
                contadorDescartes++;
            }

        }
        else if ((fase.Equals("acabarTurno") && esMT == true) || (fase.Equals("mirarCampo") && esMT == true))
        {
            juego.ReproducirEfectoMover();
            esMT = false;
            indice = indice - 5;
            if (clon.getCartaCampoU(indice) != null)
            {
                interfaz.ActivarGuardianStar(true);
                interfaz.ActivarDatosUI(indice);


            }
            else
            {
                interfaz.ActivarDatosUI(indice);
            }
            apuntador.MoverCursorArriba();
        }
    }
    public void BotonAbajo()
    {
        if (fase.Equals("ponerGuardian"))
        {
            if (indiceGuardian == true)
            {
                indiceGuardian = false;
                flecha.MoverCursorAbajo();
                juego.ReproducirEfectoMover();
            }
        }
        else if (fase.Equals("mano"))
        {
            if (descarta[indice] != 0)
            {
                bool salir = false;
                for (int i = 0; i < 5 && salir == false; i++)
                {

                    if (numerosDescarte[i] > numerosDescarte[indice])
                    {
                        numerosDescarte[i] = numerosDescarte[i] - 1;
                    }
                }
                for (int i = 0; i < 5 && salir == false; i++)
                {
                    if (validarCarta[i] == indice)
                    {
                        validarCarta.RemoveAt(i);
                        salir = true;
                    }
                }
                numerosDescarte[indice] = 0;
                interfaz.actualizarCuadrosDescartes();
                clon.DescartaUsuario(indice, true);
                descarta[indice] = 0;
                ComprobanteDescarte();
                contadorDescartes--;
            }

        }
        else if ((fase.Equals("acabarTurno") && esMT == false) || (fase.Equals("mirarCampo") && esMT == false))
        {
            juego.ReproducirEfectoMover();
            esMT = true;
            indice = indice + 5;
            if (clon.getCartaCampoU(indice) != null)
            {
                interfaz.ActivarDatosUI(indice);
            }
            else
            {
                interfaz.ActivarDatosUI(indice);
            }
            apuntador.MoverCursorAbajo();
        }
    }
    public void BotonZ()
    {
        if (fase.Equals("letrasFin"))
        {
            fase = "saltarLetras";
        }
        else if (fase.Equals("resultadoDuelo"))
        {
            cuadroUsuario.terminarAnimacionInfinita = true;
            juego.FinDuelo();
            //cargar transicion
        }
        if (fase.Equals("acabarTurno") && clon.getCartaCampoU(indice) != null && !clon.getCartaCampoU(indice).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
        {
            if (!clon.getCartaCampoU(indice).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
            {
                interfaz.SetEstadoMano(false);
                fase = "activarEfecto";
                clon.Transormacion3(indice);
                //clon.EfectosCartasUsuarioMano(indice);
            }
            else
            {
                indiceEquipo = indice;
                indice = 0;
                esMT = false;
                interfaz.ActivarGuardianStar(true);
                apuntador.SetPosicion();
                interfaz.SetEstadoMano(false);
                interfaz.ActivarDatosUI(0);
                apuntador.Reiniciar();
                fase = "activarCartaEquipoCampo";
                clon.Transformacion4(indiceEquipo);
                //para activar los equipos desde el campo

            }


        }
        else if (fase.Equals("activarCartaEquipoCampo"))
        {
            if (clon.getCartaCampoU(indice) != null)
            {
                apuntador.gameObject.SetActive(false);
                fase = "";
                camara.DevolverCamara(false);

                clon.ActivarEquipo(indiceEquipo, indice);
            }
            else
            {
                juego.ReproducirNoFusion();
            }
            //ACTIVAR Y REVISAR CARTA EQUIPO 
        }
        else if (fase.Equals("activarEfecto"))
        {
            fase = "efectoCampo";
            camara.DevolverCamara(false);
            clon.EfectosCartasUsuarioMano(indice);
        }
        else
        {
            if (juego.GetCantTurnos() > 0)
            {
                if (fase.Equals("acabarTurno") && clon.getCartaCampoU(indice) != null && clon.getCartaCampoU(indice).GetComponent<carta>().getPos() == 1 && campo.GetAtaquesUsuario(indice) == 1)
                {
                    interfaz.guardianFavorable.enabled = false;
                    interfaz.SetEstadoDatosCartaCpu(true);
                    fase = "";
                    juego.ReproducirEfectoSeleccionar();

                    Color c;
                    int contador = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (campo.GetCampoCpu(i) == 0)
                        {
                            contador++;
                        }
                        else
                        {

                            if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                            {
                                clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 0, 0));
                            }
                            else
                            {
                                clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, 90f);
                            }
                        }
                    }
                    if (contador < 5 && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                    {
                        c = Color.green;
                        if (campo.GetCampoCpu(indiceAtaque) != 0)
                        {
                            juego.ColorAtributo(indice, indiceAtaque);
                        }
                        if (campo.GetCampoCpu(0) == 0 || juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            c = Color.red;
                        }
                    }
                    else
                    {
                        if (!juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            c = Color.green;
                        }
                        else
                        {
                            c = Color.red;
                        }

                    }
                    cuadroUsuario.TableroBatallaUsuario(true);

                    camara.FijarAtaque();


                    apuntadorAtaque.gameObject.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(AnimacionCamaraAtaque());
                    //cuadroUsuario.actualizarCuadroCpu(c, 0);
                }
                else if (fase.Equals("atacar"))
                {
                    int contador = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (campo.GetCampoCpu(i) == 0)
                        {
                            contador++;
                        }
                    }

                    if (contador < 5)
                    {
                        if (campo.GetCampoCpu(indiceAtaque) != 0 && !juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            fase = "";
                            cuadroUsuario.TableroBatallaUsuario(false);
                            cuadroUsuario.CuadrosPosBatalla();
                            camara.DevolverCamara(false);
                            interfaz.SetEstadoPanel(false);
                            juego.ReproducirEfectoSeleccionar();
                            clon.SetEstadoManos(false, false);
                            interfaz.DesactivarComponentes();
                            interfaz.SetEstadoDatosCartaCpu(false);
                            campo.SetAtaquesUsuario(indice, 0);
                            interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
                            if (juego.ParametrosActivacionTrampas(indice, 0) == -1)
                            {
                                clon.GetCartaCpu(indiceAtaque).GetComponent<carta>().SetDatosCarta(1);
                                juego.InicarBatalla(indice, indiceAtaque, "ataqueUsuario");
                            }
                            else
                            {
                                juego.InicarBatalla(indice, juego.ParametrosActivacionTrampas(indice, 0), "trampaCpu");
                            }

                        }

                    }
                    else
                    {
                        if (!juego.GetEspadasLuzReveladora().Contains("cpu"))
                        {
                            fase = "";
                            cuadroUsuario.TableroBatallaUsuario(false);
                            cuadroUsuario.CuadrosPosBatalla();
                            camara.DevolverCamara(false);
                            interfaz.SetEstadoPanel(false);
                            juego.ReproducirEfectoSeleccionar();
                            clon.SetEstadoManos(false, false);
                            interfaz.DesactivarComponentes();
                            interfaz.SetEstadoDatosCartaCpu(false);
                            campo.SetAtaquesUsuario(indice, 0);
                            interfaz.datosCartaCpu.transform.localPosition = new Vector2(0f, -204f);
                            if (juego.ParametrosActivacionTrampas(indice, 0) == -1)
                            {
                                juego.InicarBatalla(indice, 0, "ataqueDirecto");
                            }
                            else
                            {
                                juego.InicarBatalla(indice, juego.ParametrosActivacionTrampas(indice, 0), "trampaCpu");
                            }

                        }

                    }
                }

            }

        }
    }
    public void BotonX()
    {
        if (fase.Equals("mano"))
        {
            if (validarCarta.Count > 1)
            {
                indiceMirarCampo = indice;
                bool hayMonstruo = false;
                for (int i = 0; i < validarCarta.Count; i++)
                {
                    if (clon.getClon(validarCarta[i]).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
                    {
                        hayMonstruo = true;
                    }
                }
                if (hayMonstruo == true)
                {

                    fase = "";
                    bool salir = false;
                    esMT = false;
                    for (int i = 0; i < 5 && salir == false; i++)
                    {
                        if (clon.getCartaCampoU(i) == null)
                        {
                            indice = i;
                            salir = true;
                        }
                    }
                    apuntador.MoverCursor(indice, esMT);
                    clon.UbicarFusion(indice);
                    flecha.gameObject.SetActive(false);
                    juego.ReproducirEfectoSeleccionar();



                }
                else
                {
                    juego.ReproducirNoFusion();
                }

            }
            else
            {

                if (validarCarta.Count == 0)
                {
                    fase = "";
                    for (int i = 0; i < 5; i++)
                    {
                        if (i != indice)
                        {
                            // clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            //clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().ataque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().defensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().imagenCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().imagenMiniCarta.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().panelAtaque.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().panelDefensa.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().panelDatos.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            clon.getClon(i).GetComponent<muestraCarta>().textoMT.color = new Color(0.5f, 0.5f, 0.5f, 1f);


                        }
                    }
                    clon.SetTransformacion(indice);
                    flecha.gameObject.SetActive(false);
                    juego.ReproducirEfectoSeleccionar();
                }
                else
                {
                    juego.ReproducirNoFusion();
                }

            }

        }
        else if (fase.Equals("posicionMano"))
        {
            indiceMirarCampo = indice;
            if (clon.getClon(indice).GetComponent<carta>().GetTipoCarta().Equals("Monstruo"))
            {



                fase = "";
                esMT = false;
                clon.Transformacion2(indice);
                bool salir = false;
                for (int i = 0; i < 5 && salir == false; i++)
                {
                    if (clon.getCartaCampoU(i) == null)
                    {
                        indice = i;
                        salir = true;
                    }
                }
                apuntador.MoverCursor(indice, esMT);
                juego.ReproducirEfectoSeleccionar();
                apuntador.PosUbicacionPorCarta(false);
            }
            else
            {
                if (clon.getClon(indice).GetComponent<carta>().GetDatosCarta() == 1)
                {
                    if (!clon.getClon(indice).GetComponent<carta>().GetTipoCarta().Equals("Equipo"))
                    {
                        //accionar el efecto correspondiente
                        fase = "efectoMano";
                        clon.EfectosCartasUsuarioMano(indice);
                    }
                    else
                    {

                        esMT = false;
                        clon.Transformacion2(indice);
                        clon.SetPos(indice);
                        indice = 0;
                        //apuntador.OffSetsPorFusion();
                        apuntador.PosUbicacionPorCarta(false);

                        apuntador.MoverCursor(0, esMT);
                        //StartCoroutine(AnimacionEquipoMano());


                    }

                }
                else
                {
                    fase = "";



                    clon.Transformacion2(indice);
                    bool salir = false;
                    esMT = true;
                    for (int i = 5; i < 10 && salir == false; i++)
                    {
                        indice = i;
                        if (clon.getCartaCampoU(i) == null)
                        {
                            indice = i;
                            salir = true;
                        }
                    }
                    apuntador.MoverCursor(indice, true);
                    apuntador.PosUbicacionPorCarta(true);
                    juego.ReproducirEfectoSeleccionar();
                }
            }


        }
        else if (fase.Equals("ubicarCarta"))
        {
            fase = "";
            interfaz.SetEstadoFlecha(false);
            //interfaz.datosCarta.SetActive(false);
            //interfaz.mano.SetActive(false);

            interfaz.eliminarNumerosDescarte();
            juego.ReproducirEfectoSeleccionar();
            if (esMT == false)
            {
                clon.ColocarGuardian(indice);
            }
            else
            {
                clon.ColocarCarta(indice, false);
            }


        }
        else if (fase.Equals("ponerGuardian"))
        {
            fase = "";
            interfaz.EstadoGuardianes(false);
            juego.ReproducirEfectoSeleccionar();
            clon.ColocarCarta(indice, indiceGuardian);

        }
    }
    public void BotonC()
    {
        if (fase.Equals("posicionMano"))
        {
            fase = "";
            clon.CancelarCarta(indice);
            //interfaz.SetEstadoFlecha(true);
            juego.ReproducirCancelarAccion();
        }
        else if (fase.Equals("atacar"))
        {

            apuntadorAtaque.gameObject.SetActive(false);
            fase = "";

            StartCoroutine(AnimacionCancelar());

        }
        else if (fase.Equals("activarEfecto"))
        {
            fase = "acabarTurno";
            clon.CancelarActivacion(indice);
            juego.ReproducirCancelarAccion();
        }
        else if (fase.Equals("activarCartaEquipoCampo"))
        {
            indice = indiceEquipo;
            fase = "acabarTurno";
            esMT = true;
            clon.CancelarActivacionEquipo(indiceEquipo);
            apuntador.CargarPosicion();
            juego.ReproducirCancelarAccion();
            interfaz.ActivarDatosUI(indice);

        }
        else if (fase.Equals("ubicarCarta"))
        {
            fase = "";
            StartCoroutine(AnimacionCancelarUbicacion());
        }
    }
    public void BotonEspacio()
    {
        if (fase.Equals("acabarTurno"))
        {

            apuntador.gameObject.SetActive(false);

            for (int i = 0; i < 5; i++)
            {
                if (campo.GetCampoCpu(i) != 0)
                {
                    if (clon.GetCartaCpu(i).GetComponent<carta>().getPos() == 1)
                    {
                        clon.GetCartaCpu(i).GetComponent<Transform>().rotation = (Quaternion.Euler(90, 180, 0));
                    }
                    else
                    {
                        clon.GetCartaCpu(i).GetComponent<Transform>().eulerAngles = new Vector3(90f, 0f, -90f);
                    }
                }
                if (campo.GetCampoCpu(i + 5) == 0)
                {
                    campo.SetZonasMT(i, 0);
                }

            }

            fase = "";
            for (int i = 0; i < 5; i++)
            {
                if (clon.getClon(i) != null)
                {
                    clon.getClon(i).transform.localPosition = new Vector3(clon.getClon(i).transform.localPosition.x, 77.95f, 0f);
                }

            }
            indiceGuardian = true;
            juego.SetPrimerAtaque(true);
            juego.SetTurnoUsuario(false);
            clon.OrganizarMano();
            interfaz.DesactivarComponentes();
            clon.InactivateComponent(clon.clon);
            camara.DevolverCamara(false);
            cuadroUsuario.ActivarAnimacion(indice);
            indice = 0;
            apuntadorAtaque.posicionX = apuntadorAtaque.transform.position.x;
            flecha.Reiniciar();
            apuntador.ReiniciarCpu();
            if (juego.GetCantTurnos() != 0)
            {
                apuntadorAtaque.ReiniciarCpu();
            }
            juego.ReproducirCambiarTurno();

        }
    }
    public void Boton1()
    {
        if (fase.Equals("acabarTurno"))
        {
            if (esMT == false)
            {
                if (campo.GetAtaquesUsuario(indice) == 1)
                {
                    clon.CambiarPosCarta(indice);
                }
            }


        }
        else if (fase.Equals("mano"))
        {
            flecha.gameObject.SetActive(false);
            clon.contenedor.SetActive(false);
            teclaActivada = false;
            fase = "devolver";
            indice = 0;
            StartCoroutine(AnimacionVerCartaCpu());


        }
    }
    public void Boton1Liberado()
    {
        if (fase.Equals("mirarCpu"))
        {

            fase = "";
            StartCoroutine(AnimacionDevolverAMano());

        }
        else if (fase.Equals("devolver"))
        {
            teclaActivada = true;
        }
    }
    public void Boton2()
    {
        if (fase.Equals("mano"))
        {

            interfaz.panelMirarTablero.SetActive(true);
            esMT = false;
            indiceMirarCampo = indice;
            indice = 0;
            interfaz.ActivarGuardianStar(true);
            fase = "mirarCampo";

            interfaz.ActivarDatosUI(0);


            apuntador.gameObject.SetActive(true);
            apuntador.OffSetsPorFusion();
            clon.MirarCampo();
        }
        else if (fase.Equals("mirarCampo"))
        {

            apuntador.gameObject.SetActive(false);
            apuntador.Reiniciar();
            indice = indiceMirarCampo;
            interfaz.ActualizarUi(indice);
            fase = "";
            interfaz.panelMirarTablero.SetActive(false);
            clon.Mano();

        }
    }
   

}
