using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cambioFondo : MonoBehaviour
{
    public Sprite spra2;
    public void cambiarImagen()
    {
        GetComponent<Image>().sprite = spra2;
    }
}
