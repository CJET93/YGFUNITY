using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntador : MonoBehaviour
{
    public float xOffset = 0.88f;
    public float zOffset = -1.24f;
    private float t = 0f;
    private Vector3 posicion;
    public float tiempoLerp = 3f;
    public Color[] miAnimacionColor;
    private int indiceColor = 0;
    private bool esMT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color,miAnimacionColor[indiceColor],tiempoLerp*Time.deltaTime);
        t = Mathf.Lerp(t, 1f, tiempoLerp * Time.deltaTime);
        if (t >.9f)
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
    public void MoverCursor(int indice ,bool esMT)
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
        pos.x = 2.45f;
        pos.y = 1f;
        pos.z = 1.2f;
        xOffset = 1.03f;
        transform.position = pos;
    }
    public void OffSetsPorFusion()
    {
        Vector3 pos = transform.position;
        pos.x = 2.3f;
        pos.y = 1f;
        pos.z = 3f;
        xOffset = 0.95f;
        transform.position = pos;
    }
    public void ReiniciarCpu()
    {
        Vector3 pos = transform.position;
        pos.x = 2.44f;
        pos.y = 1f;
        pos.z = 0.94f;
        xOffset = 1.015f;
        transform.position = pos;
    }
    public void ReiniciarMirarCpu()
    {
        Vector3 pos = transform.position;
        pos.x = -1.37f;
        pos.y = 1f;
        pos.z = 0.20f;
        xOffset = 0.85f;
        transform.position = pos;
    }
    public void SetPosicion() 
    {
        posicion = transform.position;
    }
    public void CargarPosicion()
    {
        transform.position = posicion;
    }
    public void PosUbicacionPorCarta(bool esMt)
    {
        if (esMt)
        {
            Vector3 pos = transform.position;
            pos.x = transform.position.x;
            pos.y = transform.position.y;
            pos.z = 2.56f;
            transform.position = pos;
        }
    }
    public void PosUbicacionPorCartaCpu(string fase)
    {
        if (fase.Equals("ubicar"))
        {
            Vector3 pos = transform.position;
            pos.x = transform.position.x;
            pos.y = transform.position.y;
            pos.z = transform.position.z + 0.18f;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x = transform.position.x;
            pos.y = transform.position.y;
            pos.z = transform.position.z - 0.18f;
            transform.position = pos;
        }
    }
    public bool GetEsMt()
    {
        return esMT;
    }
    public void SetEsMt(bool esMT)
    {
        this.esMT = esMT;
    }
}
