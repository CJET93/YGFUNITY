using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public AudioClip[] musica;
    public AudioClip[] introMusica;
    private AudioSource fuenteAudio;
    public int numero = 0;
    public bool activar = true;
    private void Awake()
    {
        fuenteAudio=GetComponent<AudioSource>();
      
    }
    public void ReproducirIntro(int num)
    {
        numero = num;
        if(num!=-1 && num != -2 &&num!=-3)
        {
            float delay = 0f;
            if(num == 1)
            {
                delay = 0.35f;
            }
            else if (num == 3)
            {
                delay = 0.35f;
            }
            else if (num == 6)
            {
                delay = 11.95f;
            }
            else if (num == 4)
            {
                delay = 1.8f;
            }
            else if (num == 2)
            {
                delay = 3.3f;
            }
            else if (num == 5)
            {
                delay = 2.6f;
            }
            fuenteAudio.clip = introMusica[num];
           
            fuenteAudio.Play();
            Invoke("ReproducirMusica", (fuenteAudio.clip.length+delay));
        }
        else
        {
            ReproducirMusica();
        }
       
    }
    public void ReproducirMusica()
    {
        if (activar == true)
        {
            if (numero == 0)
            {
                numero = 0;
            }
            if (numero == -1)
            {
                numero = 2;
            }
            else if (numero == -2)
            {
                numero = 7;
            }
            else if (numero == -3)
            {
                numero = 4;
            }
            else if (numero == 1)
            {
                numero = 3;
            }
            else if (numero == 2)
            {
                numero = 5;
            }
            else if (numero == 3)
            {
                numero = 9;
            }
            else if (numero == 4)
            {
                numero = 12;
            }
            else if (numero == 5)
            {
                numero = 6;
            }
            else if (numero == 6)
            {
                numero = 11;
            }
            fuenteAudio.clip = musica[numero];
            fuenteAudio.Play();
            fuenteAudio.loop = true;
        }
       
    }
    public void DetenerSonidos()
    {
        fuenteAudio.Stop();
    }
    public void MusicaMenu()
    {
        fuenteAudio.clip = musica[1];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void ReproducirMusicaVictoria()
    {
        fuenteAudio.clip = musica[13];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaDerrota()
    {
        fuenteAudio.clip = musica[14];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaResultados()
    {
        fuenteAudio.clip = musica[15];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaCrearDuelo()
    {
        fuenteAudio.clip = musica[17];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaClaves()
    {
        fuenteAudio.clip = musica[18];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MenuDueloLibre()
    {
        fuenteAudio.clip = musica[16];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaLibreria()
    {
        fuenteAudio.clip = musica[19];
        fuenteAudio.Play();
        fuenteAudio.loop = true;
    }
    public void MusicaGameOver()
    {
        fuenteAudio.clip = musica[20];
        fuenteAudio.Play();
        fuenteAudio.loop = false;
    }

}
