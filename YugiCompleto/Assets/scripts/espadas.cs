using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espadas : MonoBehaviour
{
    public float tiempoLerp = 3f;
    public Color[] miAnimacionColor;
    private int indiceColor = 0;
    private float t = 0f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, miAnimacionColor[indiceColor], tiempoLerp * Time.fixedDeltaTime);
        t = Mathf.Lerp(t, 1f, tiempoLerp * Time.fixedDeltaTime);
        if (t > .9f)
        {
            t = 0f;
            indiceColor++;
            indiceColor = (indiceColor >= miAnimacionColor.Length) ? 0 : indiceColor;

        }
    }
}
