using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float xOffset = -162f;
    public float yOffset = 60f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 300f, 0f) * Time.fixedDeltaTime);
    }
    public void MoverCursorDerecha()
    {
        Vector3 pos = transform.localPosition;
        pos.x -= xOffset;
        transform.localPosition = pos;
    }
    public void MoverCursorIzquierda()
    {
        Vector3 pos = transform.localPosition;
        pos.x += xOffset;
        transform.localPosition = pos;
    }
    public void MoverCursorArriba()
    {
        Vector3 pos = transform.localPosition;
        pos.y += yOffset;
        transform.localPosition = pos;
    }
    public void MoverCursorAbajo()
    {
        Vector3 pos = transform.localPosition;
        pos.y -= yOffset;
        transform.localPosition = pos;
    }
    public void Reiniciar()
    {
        Vector3 pos = transform.localPosition;
        pos.x = -416.87f;
        pos.y = -178.95f;
        pos.z = 0f;
        transform.localPosition = pos;
    }
    public void FlechaGuardian()
    {
        Vector3 pos = transform.localPosition;
        pos.x = -170.5f;
        pos.y = -31.6f;
        pos.z = 9.16f;
        transform.localPosition = pos;

    }
    
}
