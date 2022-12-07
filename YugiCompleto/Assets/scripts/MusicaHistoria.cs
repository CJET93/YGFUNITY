using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaHistoria : MonoBehaviour
{
    private AudioSource fuenteMusica;
    public AudioClip[] musica;
    private int numMusica;
    // Start is called before the first frame update
    void Awake()
    {
        numMusica = -1;
        fuenteMusica = GetComponent<AudioSource>();
    }
    public void reproducirMusica(int numero)
    {
      
        if (numMusica != numero)
        {
            numMusica = numero;
            fuenteMusica.clip = musica[numero];
            fuenteMusica.Play();
        }
       
    }
    public void detenerMusica()
    {
        fuenteMusica.Stop();
    }

   
}
