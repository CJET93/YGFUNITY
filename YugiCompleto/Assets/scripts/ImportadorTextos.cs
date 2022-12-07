using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportadorTextos : MonoBehaviour
{
    public string[] lineasTexto;
    public string[] lineasAtk;
    public string[] lineasDef;
    public string[] nombresCartas;
    public string[] atributos1;
    public string[] atributos2;
    public string[] GuardiansStars;
    public string[] fusionTexto;
    public string[] fusionRTexto;
    public string[] destinoFusionTexto;
    private string[] nombresTipoCartaTexto;
    private string[] numeroTipoCartaTexto;
    private string[] tipoCartaTexto;
    private string[] equiposTexto;
    private string[] destinoSATexxto;
    private string[] dropSATexto;
    private string[] destinoBCDTexto;
    private string[] dropBCDTexto;
    private string[] clavesTexto;
    private string[] costoTexto;
    private string[] ordenDuelistasTexto;
    private string[] idDuelistasTexto;
    private string[] potsTexto;
    private string[] suerteCpuTexto;
    private string[] probFusionCpuTexto;
    private string[] dropSATECTexto;
    private string[] destinoSATECTexto;
    public TextAsset fichero;
    public TextAsset atk;
    public TextAsset def;
    public TextAsset nombres;
    public TextAsset guardianes1;
    public TextAsset guardianes2;
    public TextAsset nombresGuardianes;
    public TextAsset fusion;
    public TextAsset fusionRequerida;
    public TextAsset destinoFusion;
    public TextAsset tipoCarta;
    public TextAsset nombresTipoCarta;
    public TextAsset numeroTipoCarta;
    public TextAsset equipos;
    public TextAsset destinoSA;
    public TextAsset dropSA;
    public TextAsset destinoBCD;
    public TextAsset dropBCD;
    public TextAsset claves;
    public TextAsset costo;
    public TextAsset ordenDuelistas;
    public TextAsset idDuelistas;
    public TextAsset pots;
    public TextAsset suerteCpu;
    public TextAsset probFusCpu;
    public TextAsset destinoSATEC;
    public TextAsset dropSATEC;
    public Texture2D[] cartas;
    public Texture2D[] miniImagens;
    public Texture2D[] guardianes;
    public Texture2D[]atirbutos;
    // no supe como pasar de texture2d a sprite por lo que me toco crear una nueva lista de cartas
    public Sprite[] cartasBatalla;
    //inicializa todas las listas ya cargadas
    void Awake()
    {
        if (fichero != null)
        {
            lineasTexto = fichero.text.Split('\n');
        }
        if (atk != null)
        {
            lineasAtk = atk.text.Split('\n');
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
        if (nombresGuardianes != null)
        {
            GuardiansStars = nombresGuardianes.text.Split('\n');
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
        if (tipoCarta != null)
        {
            tipoCartaTexto = tipoCarta.text.Split('\n');
        }
        if (nombresTipoCarta != null)
        {
            nombresTipoCartaTexto = nombresTipoCarta.text.Split('\n');
        }
        if (numeroTipoCarta != null)
        {
            numeroTipoCartaTexto = numeroTipoCarta.text.Split('\n');
        }
        if (equipos != null)
        {
            equiposTexto = equipos.text.Split('\n');
        }
        if(destinoSA != null)
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
        if (claves != null)
        {
            clavesTexto = claves.text.Split('\n');
        }
        if(costo != null)
        {
            costoTexto=costo.text.Split('\n');
        }
        if (ordenDuelistas != null)
        {
            ordenDuelistasTexto = ordenDuelistas.text.Split('\n');
        }
        if (idDuelistas != null)
        {
            idDuelistasTexto = idDuelistas.text.Split('\n');
        }
        if (pots != null)
        {
            potsTexto = pots.text.Split('\n');
        }
        if (probFusCpu != null)
        {
            probFusionCpuTexto = probFusCpu.text.Split('\n');
        }
        if (suerteCpu != null)
        {
            suerteCpuTexto = suerteCpu.text.Split('\n');
        }
        if (destinoSATEC != null)
        {
            destinoSATECTexto = destinoSATEC.text.Split('\n');
        }
        if (dropSATEC != null)
        {
            dropSATECTexto = dropSATEC.text.Split('\n');
        }
    }
    public void SetTexto(string[] lineaText)
    {
        lineaText = lineasTexto;
    }

    public string[] getLista()
    {
        return lineasTexto;
    }
    public string[] getatk()
    {
        return lineasAtk;
    }
    public string[] getdef()
    {
        return lineasDef;
    }
    public string[] getnom()
    {
        return nombresCartas;
    }
    public string[] GetAtributos1()
    {
        return atributos1;
    }
    public string[] GetAtributos2()
    {
        return atributos2;
    }
    public string[] GetNomAtributo()
    {
        return GuardiansStars;
    }
    public string[] GetFusion()
    {
        return fusionTexto;
    }
    public string[] GetFusionR()
    {
        return fusionRTexto;
    }
    public string[] GetDestinoFusion()
    {
        return destinoFusionTexto;
    }
    public string[] GetTipoCarta()
    {
        return tipoCartaTexto;
    }
    public string[] GetNombreTipoCarta()
    {
        return nombresTipoCartaTexto;
    }
    public string [] GetNumeroTipoCarta()
    {
        return numeroTipoCartaTexto;
    }
    public string[] GetEquipos()
    {
        return equiposTexto;
    }
    public string[] GetDestinoSA()
    {
        return destinoSATexxto;
    }
    public string[] GetDestinoBCD()
    {
        return destinoBCDTexto;
    }
    public string[] GetDropSA()
    {
        return dropSATexto;
    }
    public string[] GetDropBCD()
    {
        return dropBCDTexto;
    }
    public string[] GetClaves()
    {
        return clavesTexto;
    }
    public string[] GetCosto()
    {
        return costoTexto;
    }
    public string[] GetOrdenDuelista()
    {
        return ordenDuelistasTexto;
    }
    public string[] GetIdDuelista()
    {
        return idDuelistasTexto;
    }
    public string[] GetPots()
    {
        return potsTexto;
    }
    public string[] GetPFCpu()
    {
        return probFusionCpuTexto;
    }
    public string[] GetSuerteCpu()
    {
        return suerteCpuTexto;
    }
    public string[] GetDestionoSATEC()
    {
        return destinoSATECTexto;
    }
    public string[] GetDropSATEC()
    {
        return dropSATECTexto;
    }



}
