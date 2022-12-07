using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SistemaGuardado
{
    
    public static bool prepararGuardado(DatosJuego datosJuego)
    {
        string slot = datosJuego.GetSlot();
       
        string ruta = Application.persistentDataPath + "/" + slot + ".dat";
        if (File.Exists(ruta)){
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PrepararCargado1()
    {
         string ruta = Application.persistentDataPath + "/datos1.dat";
        Debug.Log(ruta);
        if (File.Exists(ruta))
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }
    public static bool PrepararCargado2()
    {
        string ruta = Application.persistentDataPath + "/datos2.dat";
        if (File.Exists(ruta))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public static bool PrepararCargado3()
    {
        string ruta = Application.persistentDataPath + "/datos3.dat";
        if (File.Exists(ruta))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public static void Guardar(DatosJuego datosJuego)
    {
        BinaryFormatter formato = new BinaryFormatter();
        string slot = datosJuego.GetSlot();
        string ruta = Application.persistentDataPath + "/"+slot+".dat";
        Debug.Log(ruta);
        FileStream archivo = new FileStream(ruta,FileMode.Create);
        DatosJugador datos = new DatosJugador(datosJuego);
        formato.Serialize(archivo, datos);
        archivo.Close();
    }
    public static DatosJugador Cargar1()
    {
        
        string ruta = Application.persistentDataPath + "/datos1.dat";

        if (File.Exists(ruta))
        {
            BinaryFormatter formato = new BinaryFormatter();
            FileStream archivo = new FileStream(ruta, FileMode.Open);
            DatosJugador datos = formato.Deserialize(archivo) as DatosJugador;
            archivo.Close();
            return datos;

        }
        else
        {
            return null;
        }


    }
    public static DatosJugador Cargar2()
    {
        string ruta = Application.persistentDataPath + "/datos2.dat";
        if (File.Exists(ruta))
        {
            BinaryFormatter formato = new BinaryFormatter();
            FileStream archivo = new FileStream(ruta, FileMode.Open);
            DatosJugador datos = formato.Deserialize(archivo) as DatosJugador;
            archivo.Close();
            return datos;

        }
        else
        {
            return null;
        }
    }
    public static DatosJugador Cargar3()
    {
        string ruta = Application.persistentDataPath + "/datos3.dat";
        if (File.Exists(ruta))
        {
            BinaryFormatter formato = new BinaryFormatter();
            FileStream archivo = new FileStream(ruta, FileMode.Open);
            DatosJugador datos = formato.Deserialize(archivo) as DatosJugador;
            archivo.Close();
            return datos;

        }
        else
        {
            return null;
        }
    }
}
