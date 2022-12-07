using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ArchivosCargado : MonoBehaviour
{
    public TextMeshProUGUI slot1;
    public TextMeshProUGUI slot2;
    public TextMeshProUGUI slot3;
    public Image completeSlot1;
    public Image completeSlot2;
    public Image completeSlot3;
    private void Start()
    {
        Cursor.visible = true;
        if (SistemaGuardado.PrepararCargado1() == true)
        {
            slot1.fontSize = 50;
            DatosJugador datos = SistemaGuardado.Cargar1();
            string estadoHistoria;
            if (datos.completoModoHistoria)
            {
                completeSlot1.enabled = true;
            }
            if (datos.historia > 33)
            {
                estadoHistoria = "Fase final";
            }
            else if (datos.historia > 22)
            {
                estadoHistoria = "Obtención de artículos ";
            }
            else if (datos.historia > 14)
            {
                estadoHistoria = "Torneo Finales ";
            }
            else if (datos.historia > 4)
            {
                estadoHistoria = "Tornero preliminares ";
            }
            else
            {
                estadoHistoria = "Introducción";
            }
            slot1.text = "Duelista " + datos.nombreJugador + "-Estrellas " + datos.estrellas + "-Estado historia " + estadoHistoria;
        }
        else
        {
            slot1.fontSize = 200;
            slot1.text = "Sin datos";
        }
        if (SistemaGuardado.PrepararCargado2() == true)
        {
            slot2.fontSize = 50;
            DatosJugador datos = SistemaGuardado.Cargar2();
            string estadoHistoria ;
            if (datos.completoModoHistoria)
            {
                completeSlot2.enabled = true;
            }
            if (datos.historia > 33)
            {
                estadoHistoria = "Fase final";
            }
            else if (datos.historia > 22)
            {
                estadoHistoria = "Obtención de artículos ";
            }
            else if (datos.historia > 14)
            {
                estadoHistoria = "Torneo Finales ";
            }
            else if (datos.historia > 4)
            {
                estadoHistoria = "Tornero preliminares ";
            }
            else
            {
                estadoHistoria = "Introducción";
            }
            slot2.text = "Duelista " + datos.nombreJugador + "-Estrellas " + datos.estrellas + "-Estado historia " + estadoHistoria;
        }
        else
        {
            slot2.fontSize = 200;
            slot2.text = "Sin datos";
        }
        if (SistemaGuardado.PrepararCargado3() == true)
        {
            slot3.fontSize = 50;
            DatosJugador datos = SistemaGuardado.Cargar3();
            string estadoHistoria ;
            if (datos.completoModoHistoria)
            {
                completeSlot3.enabled = true;
            }
            if (datos.historia > 33)
            {
                estadoHistoria = "Fase final";
            }
            else if (datos.historia > 22)
            {
                estadoHistoria = "Obtención de artículos ";
            }
            else if (datos.historia > 14)
            {
                estadoHistoria = "Torneo Finales ";
            }
            else if (datos.historia > 4)
            {
                estadoHistoria = "Tornero preliminares ";
            }
            else
            {
                estadoHistoria = "Introducción";
            }
            slot3.text = "Duelista " + datos.nombreJugador + "-Estrellas " + datos.estrellas + "-Estado historia " + estadoHistoria;
        }
        else
        {
            slot3.fontSize = 200;
            slot3.text = "Sin datos";
        }
    }
   
}
