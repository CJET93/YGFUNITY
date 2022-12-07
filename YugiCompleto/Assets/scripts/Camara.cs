using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

 
    public void MoverCamara(bool esMT)
    {
        

        if (esMT)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("moverCamaraMT");
        }
        else{
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("muldock");
        }
       
    }
    public void DevolverCamara(bool esMT)
    {
       
        if (esMT)
        {
            GetComponent<Animator>().Play("devolverCamaraMT");

        }
        else
        {
            GetComponent<Animator>().Play("VolverCamara");

        }

    }
    public void FijarAtaque()
    {
        GetComponent<Animator>().Play("FijarAtaque");
    }
    public void CancelarAtaque()
    {
        GetComponent<Animator>().Play("CancelarAtaque");
    }
    public void MirarCampoCpu()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("mirarCaCpu");
    }
    public void NoMirarCampoCpu()
    {
        GetComponent<Animator>().Play("noMirarCaCpu");
    }
    public void VolverMano()
    {
        GetComponent<Animator>().Play("VolverMano");
    }
   
}
