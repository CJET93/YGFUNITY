using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApuntadorAtaque : MonoBehaviour
{
    public float xOffset = 1.03f;
    public float zOffset = -1.15f;
    public float posicionX;
    public float posicionXCpu;
    private float t = 0f;
    private Vector3 posicion;
    public float tiempoLerp = 3f;
    public Color[] miAnimacionColor;
    private int indiceColor = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, miAnimacionColor[indiceColor], tiempoLerp * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, tiempoLerp * Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            indiceColor++;
            indiceColor = (indiceColor >= miAnimacionColor.Length) ? 0 : indiceColor;

        }

    }

    public void MoverCursorDerecha()
    {
        Vector3 pos = transform.position;
        pos.x -= xOffset;
        transform.position = pos;
    }
    public void MoverCursorIzquierda()
    {
        Vector3 pos = transform.position;
        pos.x += xOffset;
        transform.position = pos;
    }
    public void MoverCursor(int indice, bool esMT)
    {


        Vector3 pos = transform.position;
        if (esMT == true)
        {
            indice = indice - 5;
            pos.z -= zOffset;
        }
        pos.x -= xOffset * indice;


        transform.position = pos;
    }
    public void MoverCursorArriba()
    {
        Vector3 pos = transform.position;
        pos.z += zOffset;
        transform.position = pos;
    }
    public void MoverCursorAbajo()
    {
        Vector3 pos = transform.position;
        pos.z -= zOffset;
        transform.position = pos;
    }
    public void Reiniciar()
    {
        Vector3 pos = transform.position;
        pos.x = posicionX;
        pos.y = 1f;
        pos.z = -2.59f;
        transform.position = pos;
    }

    public void OffSetsPorFusion()
    {
        Vector3 pos = transform.position;
        pos.x = 1.74f;
        pos.y = 1f;
        pos.z = 3.47f;
        xOffset = 0.8f;
        transform.position = pos;
    }
    public void ReiniciarCpu()
    {
        Vector3 pos = transform.position;
        pos.x = posicionXCpu;
        pos.y = 1f;
        pos.z = -2.6f;
        transform.position = pos;
    }
}
