using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class encriptado : MonoBehaviour
{
    private string[] atkTexto;
    public TextAsset atk;

    public string[] lineasTexto;
    public string[] lineasAtk;
    public string[] lineasDef;
    public string[] nombresCartas;
    public string[] atributos1;
    public string[] atributos2;
    public string[] fusionTexto;
    public string[] fusionRTexto;
    public string[] destinoFusionTexto;
    private string[] equiposTexto;
    private string[] destinoSATexxto;
    private string[] dropSATexto;
    private string[] destinoBCDTexto;
    private string[] dropBCDTexto;
    private string[] costoTexto;
    private string[] potsTexto;
    private string[] dropSATECTexto;
    private string[] destinoSATECTexto;
    private string[] deckInicialTexto;
    private string[] destinoDeckTexto;
    private string[] probabilidadDropTexto;


    public TextAsset def;
    public TextAsset nombres;
    public TextAsset guardianes1;
    public TextAsset guardianes2;
    public TextAsset fusion;
    public TextAsset fusionRequerida;
    public TextAsset destinoFusion;
    public TextAsset equipos;
    public TextAsset destinoSA;
    public TextAsset dropSA;
    public TextAsset destinoBCD;
    public TextAsset dropBCD;
    public TextAsset costo;
    public TextAsset pots;
    public TextAsset destinoSATEC;
    public TextAsset dropSATEC;
    public TextAsset deckInicial;
    public TextAsset destinoDeck;
    public TextAsset probabilidadDrop;


    public ImportadorTextos txt;
    public ImportadorHistoria txt1;

    string CJETCode = "9876543210ABCDEFGHIJKLMÑNOPQRSTUVWXYZAabcdefghijklmnñopqrstuvwxyz <>?.-_#()'& ";

    void Awake()
    {
      
        if (atk != null)
        {
            atkTexto = atk.text.Split('\n');
        }
        if (def != null)
        {
            lineasDef = def.text.Split('\n');
        }
        if (nombres != null)
        {
            nombresCartas = nombres.text.Split('\n');
        }
        if (guardianes1 != null)
        {
            atributos1 = guardianes1.text.Split('\n');
        }
        if (guardianes2 != null)
        {
            atributos2 = guardianes2.text.Split('\n');
        }
        if (fusion != null)
        {
            fusionTexto = fusion.text.Split('\n');
        }
        if (fusionRequerida != null)
        {
            fusionRTexto = fusionRequerida.text.Split('\n');
        }
        if (destinoFusion != null)
        {
            destinoFusionTexto = destinoFusion.text.Split('\n');
        }
        if (equipos != null)
        {
            equiposTexto = equipos.text.Split('\n');
        }
        if (destinoSA != null)
        {
            destinoSATexxto = destinoSA.text.Split('\n');
        }
        if (destinoBCD != null)
        {
            destinoBCDTexto = destinoBCD.text.Split('\n');
        }
        if (dropSA != null)
        {
            dropSATexto = dropSA.text.Split('\n');
        }
        if (dropBCD != null)
        {
            dropBCDTexto = dropBCD.text.Split('\n');
        }
        if (costo != null)
        {
            costoTexto = costo.text.Split('\n');
        }
        if (pots != null)
        {
            potsTexto = pots.text.Split('\n');
        }
        if (destinoSATEC != null)
        {
            destinoSATECTexto = destinoSATEC.text.Split('\n');
        }
        if (dropSATEC != null)
        {
            dropSATECTexto = dropSATEC.text.Split('\n');
        }
        if (deckInicial != null)
        {
            deckInicialTexto = deckInicial.text.Split('\n');
        }
        if (probabilidadDrop != null)
        {
            probabilidadDropTexto = probabilidadDrop.text.Split('\n');
        }
        if (destinoDeck != null)
        {
            destinoDeckTexto = destinoDeck.text.Split('\n');
        }

    }
    private void Start()
    {
        ComprobarEncriptados();
    }
    public string[] getatk()
    {
        return atkTexto;
    }
    public void ComprobarEncriptados()
    {
        try
        {
            for (int i = 1; i < txt.getatk().Length; i++)
            {
                String llave = Desencriptar(atkTexto[i]);
                if (!llave.Equals(txt.getatk()[i].Trim()))
                {
                    Application.Quit();
                    
                }
            }
            for (int i = 1; i < txt.getdef().Length; i++)
            {
                String llave = Desencriptar(lineasDef[i]);
                if (!llave.Equals(txt.getdef()[i].Trim()))
                {
                    Application.Quit();
                    
                }
            }
            for (int i = 1; i < txt.getnom().Length; i++)
            {
                String llave = Desencriptar(nombresCartas[i]);
                if (!llave.Equals(txt.getnom()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetAtributos1().Length; i++)
            {
                String llave = Desencriptar(atributos1[i]);
                if (!llave.Equals(txt.GetAtributos1()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetAtributos2().Length; i++)
            {
                String llave = Desencriptar(atributos2[i]);
                if (!llave.Equals(txt.GetAtributos2()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDestinoSA().Length; i++)
            {
                String llave = Desencriptar(destinoSATexxto[i]);
                if (!llave.Equals(txt.GetDestinoSA()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDropSA().Length; i++)
            {
                String llave = Desencriptar(dropSATexto[i]);
                if (!llave.Equals(txt.GetDropSA()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDestinoBCD().Length; i++)
            {
                String llave = Desencriptar(destinoBCDTexto[i]);
                if (!llave.Equals(txt.GetDestinoBCD()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDropBCD().Length; i++)
            {
                String llave = Desencriptar(dropBCDTexto[i]);
                if (!llave.Equals(txt.GetDropBCD()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDestionoSATEC().Length; i++)
            {
                String llave = Desencriptar(destinoSATECTexto[i]);
                if (!llave.Equals(txt.GetDestionoSATEC()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDropSATEC().Length; i++)
            {
                String llave = Desencriptar(dropSATECTexto[i]);
                if (!llave.Equals(txt.GetDropSATEC()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetDestinoFusion().Length; i++)
            {
                String llave = Desencriptar(destinoFusionTexto[i]);
                if (!llave.Equals(txt.GetDestinoFusion()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetFusion().Length; i++)
            {
                String llave = Desencriptar(fusionTexto[i]);
                if (!llave.Equals(txt.GetFusion()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetFusionR().Length; i++)
            {
                String llave = Desencriptar(fusionRTexto[i]);
                if (!llave.Equals(txt.GetFusionR()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetEquipos().Length; i++)
            {
                String llave = Desencriptar(equiposTexto[i]);
                if (!llave.Equals(txt.GetEquipos()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetPots().Length; i++)
            {
                String llave = Desencriptar(potsTexto[i]);
                if (!llave.Equals(txt.GetPots()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt.GetCosto().Length; i++)
            {
                String llave = Desencriptar(costoTexto[i]);
                if (!llave.Equals(txt.GetCosto()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt1.GetDeckInicial().Length; i++)
            {

                String llave = Desencriptar(deckInicialTexto[i]);
                if (!llave.Equals(txt1.GetDeckInicial()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt1.GetDestinoDeck().Length; i++)
            {

                String llave = Desencriptar(destinoDeckTexto[i]);
                if (!llave.Equals(txt1.GetDestinoDeck()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }
            for (int i = 1; i < txt1.GetDropDeck().Length; i++)
            {

                String llave = Desencriptar(probabilidadDropTexto[i]);
                if (!llave.Equals(txt1.GetDropDeck()[i].Trim().ToUpper()))
                {
                    Application.Quit();
                   
                }
            }

        }
        catch(Exception i)
        {
            Application.Quit();
        }
      
    }
    public String Desencriptar(string valor)
    {
        int conteoLetras = 0;
        string valorLlave = "";
        for (int i = 0; i < (valor.Length / 2); i++)
        {
            conteoLetras +=2;
            string llave = valor[conteoLetras - 2] +""+ valor[conteoLetras-1];
            int numLetra = Convert.ToInt32(llave);           
            valorLlave += CJETCode[numLetra-1];
        }
        return valorLlave;
    }
}
