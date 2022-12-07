using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApuntadorDeck : MonoBehaviour
{
    public float xOffset = 0.88f;
    public float zOffset = -1.24f;
    private float t = 0f;
    private Vector3 posicion;
    public float tiempoLerp = 3f;
    public Color[] miAnimacionColor;
    private int indiceColor = 0;
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color,miAnimacionColor[indiceColor],tiempoLerp*Time.deltaTime);
        t = Mathf.Lerp(t, 1f, tiempoLerp * Time.deltaTime);
        if (t >.9f)
        {
            t = 0f;
            indiceColor++;
            indiceColor = (indiceColor >= miAnimacionColor.Length) ? 0 : indiceColor;

        }
       
    }

  
}
