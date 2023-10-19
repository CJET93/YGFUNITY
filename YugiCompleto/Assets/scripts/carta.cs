using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carta : MonoBehaviour
{

    public int ataque;
    public int defensa;
    public int datosCarta;
    private string nombreCarta;
    public int pos;
    public int guardian;
    public int guardian2;
    public int guardianActivo;
    public Campo campo ;
    public ImportadorTextos txt;
    public ClonCarta clonCarta;
    public bool animacionAcabada =true;
    public bool tieneBono = false;
    public bool tieneBonoDesfavorable = false;
    public string tipoCarta;
    public int tipoAtributo;
    public bool esInmortal = false;
    private int starsNumber;
    private string attribute; // este es el atributo de la carta (no si es pez, roca)sino por ejemplo fueg,agua,viento,magica...
    // Start is called before the first frame update
    public bool GetTieneBono()
    {
        return tieneBono;
    }
    public void SetTieneBono(bool estado)
    {
        tieneBono = estado;
    }
    public bool GetTieneBonoDesfavorable()
    {
        return tieneBonoDesfavorable;
    }
    public void SetTieneBonoDesfavorable(bool estado)
    {
        tieneBonoDesfavorable = estado;
    }
    
    public void SetAtaque(int ataqueCambiar)
    {
        ataque = ataqueCambiar;
    }
    public int getAtaque()
    {
      
        return ataque;
    }
    public int getPos()
    {
        return pos;
    }
    public int getDefensa()
    {
        return defensa;
    }
    public string GetTipoCarta()
    {
        return tipoCarta;
    }
    public void SetTipoCarta(string tipo)
    {
        tipoCarta = tipo;
    }
    //metodo temporal juego con una carta sin mano
   
    public void SetDatosCarta(int datos)
    {
        datosCarta = datos;
    }
    public int GetDatosCarta()
    {
        return datosCarta;
    }

    public void SetPos(int posCarta)
    {
        pos = posCarta;
    }
    public void SetGuardianStar(int atributo)
    {
        guardian = atributo;
    }
    public int GetGuardianStar()
    {
        return guardian;
    }
    public void SetGuardianStar2(int atributo)
    {
        guardian2 = atributo;
    }
    public int GetGuardianStar2()
    {
        return guardian2;
    }
    public void SetGuardianStarA(int atributo)
    {
        guardianActivo = atributo;
    }
    public int GetGuardianStarA()
    {
        return guardianActivo;
    }
    public int GetTipoAtributo()
    {
        return tipoAtributo;
    }
    public void SetTipoAtributo(int atributo)
    {
        tipoAtributo = atributo;
    }
    public void SetDefensa(int def)
    {
        defensa = def;
    }

    public void SetStarsNumber(int stars)
    {
        starsNumber = stars;
    }

    public int GetStarsNumber()
    {
        return starsNumber;
    }

    public void SetAttribute(string attribute)
    {
        this.attribute = attribute;
    }

    public string GetAttribute()
    {
        return attribute;
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }


}
