using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campo : MonoBehaviour
{
    private int cartaPos;
    public int[] cartaCPUPos;
    public int[] manoCpu;
    public int[] manoUsuario;
    public int[] ataqueUsuario;
    public int[]ataqueCpu;
    public int[] campoUsuario;
    public int[] manoAtaqueUsuario;
    public int[] posCampo;
    public int[] campoCpu;
    public int[] ataquesUsuario;
    public int[] ataquesCpu;
    public int[] defensaUsuario;
    public int[] ZonasMTCpu;
    public int getPos()
    {
        return manoUsuario[cartaPos];
    }
    public void setPos(int pos)
    {
        cartaPos = pos;
    }
    public void setPosCpu(int[] pos)
    {
        cartaCPUPos = pos;
    }
    public int[] GetCpuPos()
    {
        return cartaCPUPos;
    }
    public int GetManoUsuario(int indice)
    {
        return manoUsuario[indice];
    }
    public void SetManoUsuario(int indice,int carta)
    {
        manoUsuario[indice] = carta;
    }
    public int GetAtaqueUsuario(int indice)
    {
        return ataqueUsuario[indice];
    }
    public void SetAtaqueUsuario(int indice, int aCarta)
    {
        ataqueUsuario[indice] = aCarta;
    }
    public int GetCampoUsuario(int indice)
    {
        return campoUsuario[indice];
    }
    public void SetCampoUsuario(int indice,int carta)
    {
        campoUsuario[indice] = carta;
    }
    public int GetPosCampo(int indice)
    {
        return posCampo[indice];
    }
    public void SetPosCampo(int indice, int numeroCarta)
    {
        posCampo[indice] = numeroCarta; 
    }
    public int GetManoCpu(int indice)
    {
        return manoCpu[indice];
    }
    public void SetManoCpu(int indice, int carta)
    {
        manoCpu[indice] = carta;
    }
    public int GetAtaqueCpu(int indice)
    {
        return ataqueCpu[indice];
    }
    public void SetAtaqueCpu(int indice, int aCarta)
    {
        ataqueCpu[indice] = aCarta;
    }
    public int GetCampoCpu(int indice)
    {
        return campoCpu[indice];
    }
    public void SetCampoCpu(int indice, int carta)
    {
        campoCpu[indice] = carta;
    }
    public int GetAtaquesUsuario(int indice)
    {
        return ataquesUsuario[indice];
    }
    public void SetAtaquesUsuario(int indice,int valor)
    {
        ataquesUsuario[indice] = valor;
    }
    public int GetAtaquesCpu(int indice)
    {
        return ataquesCpu[indice];
    }
    public void SetAtaquesCpu(int indice, int valor)
    {
        ataquesCpu[indice] = valor;
    }
    public int GetDefensaUsuario(int indice)
    {
        return defensaUsuario[indice];
    }
    public void SetDefensaUsuario(int indice,int valor)
    {
        defensaUsuario[indice] = valor;
        
    }
    public int[] GetCampoCpu()
    {
        return campoCpu;
    }
    public int GetZonasMT(int indice)
    {
        return ZonasMTCpu[indice];
    }
    public void SetZonasMT(int indice,int valor)
    {
        ZonasMTCpu[indice] = valor;
    }
    //exodia
    public bool ExodiaUsuario()
    {
        int carta = 93;
        int contador = 0;
        List<int> temporal = new List<int>();
        for(int i = 0; i < 5; i++)
        {
            temporal.Add(manoUsuario[i]);
        }
        for(int i = 0; i < 5; i++)
        {
            if (temporal.Contains(carta))
            {
                contador++;
            }
            carta++;
        }
        if (contador == 5)
        {
            return true;
        }
        return false;
    }
    public bool ExodiaCpu()
    {
        int carta = 93;
        int contador = 0;
        List<int> temporal = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            temporal.Add(manoCpu[i]);
        }
        for (int i = 0; i < 5; i++)
        {
            if (temporal.Contains(carta))
            {
                contador++;
            }
            carta++;
        }
        if (contador == 5)
        {
            return true;
        }
        return false;
    }
    //cartas acabadas
    public bool SinCartasUsuario()
    {
        for (int i = 0; i < 5; i++)
        {
            if (manoUsuario[i] == 0)
            {
                return true;
            }
        }
        return false;
    }
    public bool SinCartasCpu()
    {
        for (int i = 0; i < 5; i++)
        {
            if (manoCpu[i] == 0)
            {
                return true;
            }
        }
        return false;
    }
}
