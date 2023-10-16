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
    public void obtenerCarta()
    {
       
        int ataqueconvertidor = int.Parse((string)txt.getatk().GetValue(campo.getPos()));
        ataque = ataqueconvertidor;
        int defconvertidor = int.Parse((string)txt.getdef().GetValue(campo.getPos()));
        defensa = defconvertidor;
        nombreCarta = (string)txt.getnom().GetValue(campo.getPos());
        if (datosCarta == 1)
        {
            GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.getPos());
        }
        else
        {
            GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(110);
        }
       
    }
    public void CambiarPosicion()
    {
        
            
            StartCoroutine(Rotar());
            
        
        
        
         
    }
    
   
    IEnumerator Rotar ()
    {
        if (animacionAcabada == true)
        {
            animacionAcabada = false;
            float grados;
            float rotar;
            bool realizada = false;

            if (pos == 1)
            {
                grados = 90f;
                rotar = -90f;
                while (realizada == false)
                {
                    float rotacion = rotar * 6 * Time.fixedDeltaTime;
                    transform.Rotate(0, 0, rotacion);
                    if (grados > transform.eulerAngles.z)
                    {
                        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);
                        realizada = true;

                    }
                    yield return new WaitForEndOfFrame();
                }
                pos = 0;
                yield return new WaitForSeconds(0.05f);
                animacionAcabada = true;
            }
            else
            {
                grados = 180f;
                rotar = 180f;
                while (realizada == false)
                {
                    float rotacion = rotar * 6 * Time.fixedDeltaTime;
                    transform.Rotate(0, 0, rotacion);
                    if (grados < transform.eulerAngles.z)
                    {
                        GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
                        realizada = true;

                    }
                    yield return new WaitForEndOfFrame();
                }
                pos = 1;
                yield return new WaitForSeconds(0.05f);
                animacionAcabada = true;
                
            }
        
        }
        
       
        
       
        
        
            
            
        
    }
    public void SetDatosCarta(int datos)
    {
        datosCarta = datos;
    }
    public int GetDatosCarta()
    {
        return datosCarta;
    }
    public void EntraBatalla()
    {
        
        transform.Translate(0, 0, -2);
        transform.localScale = new Vector3(8f, 8f, 0);
        StartCoroutine(Batalla());
    }
    //el nombre de este metodo y su comportamiento puede camnbiar
    IEnumerator Batalla()
    {
        float grados=180f;
        float rotar=-45f;
        bool realizada = false;
        bool animacion1 = false;
        while (!realizada)
        {
            float rotacion = rotar * 15 * Time.fixedDeltaTime;
            transform.Rotate(0, rotacion, 0);
            if (grados-135 < transform.eulerAngles.y && animacion1==false)
            {
                GetComponent<Transform>().eulerAngles = new Vector3(0, 225, 180);
                GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)txt.cartas.GetValue(campo.getPos());
                animacion1 = true;
                grados = 225;
                
            }
            if (grados+100 < transform.eulerAngles.y)
            {
                GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
                realizada = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
       
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
