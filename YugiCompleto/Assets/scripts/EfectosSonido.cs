using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class EfectosSonido : MonoBehaviour
{
    public AudioClip[] efectos;
    private AudioSource fuenteAudio;
    private void Awake()
    {
        fuenteAudio= GetComponent<AudioSource>();
    }
    public void moverCarta()
    {

        fuenteAudio.clip = efectos[0];
        fuenteAudio.Play();

    }
    public void SeleccionarCarta()
    {
        fuenteAudio.clip = efectos[1];
        fuenteAudio.Play();
    }
    public void CancelarAccion()
    {
        fuenteAudio.clip = efectos[8];
        fuenteAudio.Play();
    }
    public void Robar()
    {
        fuenteAudio.clip = efectos[3];
        fuenteAudio.Play();
    }
    public void CambiarPosicionCarta()
    {
        fuenteAudio.clip = efectos[2];
        fuenteAudio.Play();
    }
    public void Atacar()
    {
        fuenteAudio.clip = efectos[4];
        fuenteAudio.Play();
    }
    public void AtacarDIrecto()
    {
        fuenteAudio.clip = efectos[5];
        fuenteAudio.Play();
    }
    public void CambiarTurno()
    {
        fuenteAudio.clip = efectos[7];
        fuenteAudio.Play();
    }
    public void ActivarEfecto()
    {
        fuenteAudio.clip = efectos[12];
        fuenteAudio.Play();
    }
    public void Descarte()
    {
        fuenteAudio.clip = efectos[11];
        fuenteAudio.Play();
    }
    public void Fusion()
    {
        fuenteAudio.clip = efectos[10];
        fuenteAudio.Play();
    }
    public void NoFusion()
    {
        fuenteAudio.clip = efectos[9];
        fuenteAudio.Play();
    }
    public void Aumento()
    {
        fuenteAudio.clip = efectos[6];
        fuenteAudio.Play();
    }
    public void Quemar()
    {
        fuenteAudio.clip = efectos[13];
        fuenteAudio.Play();
    }
    public void GuardianFavorable()
    {
        fuenteAudio.clip = efectos[14];
        fuenteAudio.Play();
    }
    public void obtenerSonidoEfecto(int num)
    {
        fuenteAudio.clip = efectos[num];
        fuenteAudio.Play();
    }
   
}
