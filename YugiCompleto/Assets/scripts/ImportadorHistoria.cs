using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ImportadorHistoria : MonoBehaviour
{
    public string[] probabilidadDropTexto;
    public string[] destinoDeckTexto;
    public string[] dropDeckTexto;
    public string[] sistemaTexto;
    public string[] dialogosTexto;
    public string[] duelistasTexto;
    public string[] deckInicialTexto;
    public string[] imagenesTexto;
    public string[] sonidoTTexto;
    public TextAsset probabilidadDrop;
    public TextAsset destinoDeck;
    public TextAsset dropDeck;
    public TextAsset sistema;
    public TextAsset dialogos;
    public TextAsset duelistas;
    public TextAsset deckInicial;
    public TextAsset imagenesA;
    public TextAsset sonidos;


    public Sprite[] imagenes;
    //inicializa todas las listas ya cargadas
    void Awake()
    {
        if (probabilidadDrop != null)
        {
            probabilidadDropTexto = probabilidadDrop.text.Split('\n');
        }
        if (destinoDeck != null)
        {
            destinoDeckTexto= destinoDeck.text.Split('\n');
        }
        if (dropDeck != null)
        {
            dropDeckTexto = dropDeck.text.Split('\n');
        }
        if (sistema != null)
        {
            sistemaTexto = sistema.text.Split('\n');
        }
        if (dialogos != null)
        {
            dialogosTexto = dialogos.text.Split('\n');
        }
        if (duelistas != null)
        {
            duelistasTexto = duelistas.text.Split('\n');
        }
        if (deckInicial != null)
        {
            deckInicialTexto = deckInicial.text.Split('\n');
        }
        if (imagenesA != null)
        {
            imagenesTexto = imagenesA.text.Split('\n');
        }
        if (sonidos != null)
        {
            sonidoTTexto= sonidos.text.Split('\n');
        }

    }
    public void SetTexto(string[] lineaText)
    {
        lineaText = probabilidadDropTexto;
    }

    public string[] getLista()
    {
        return probabilidadDropTexto;
    }
    public string[] GetDestinoDeck()
    {
        return destinoDeckTexto;
    }
    public string[] GetDropDeck()
    {
        return dropDeckTexto;
    }
    public string[] GetSistema()
    {
        return sistemaTexto;
    }
    public string[] GetDialogos()
    {
        return dialogosTexto;
    }
    public string[] GetDuelistas()
    {
        return duelistasTexto;
    }
    public string[] GetDeckInicial()
    {
        return deckInicialTexto;
    }
    public string[] GetImagenes()
    {
        return imagenesTexto;
    }
    public string[] GetSonidoss()
    {
        return sonidoTTexto;
    }






}

