using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class muestraCarta : MonoBehaviour
{

    public RawImage imagenCarta;
    public RawImage panelAtaque;
    public RawImage panelDefensa;
    public RawImage panelDatos;
    public RawImage reverso;
    public TextMeshProUGUI ataque;
    public TextMeshProUGUI defensa;
    public TextMeshProUGUI textoMT;
    public Texture2D []color = new Texture2D[3];
    public GameObject contenedorNormal;
    public GameObject contenedorReverso;
    public GameObject contenedorBatalla;
    public Image imagenCartaB;
    public TextMeshProUGUI ataqueB;
    public TextMeshProUGUI defensaB;
    public RawImage panelAtaqueB;
    public RawImage panelDefensaB;
    public RawImage contenedorNombre;
    public TextMeshProUGUI nombreCarta;
    public RawImage imagenMiniCarta;
}
