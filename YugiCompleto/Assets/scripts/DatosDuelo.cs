using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosDuelo : MonoBehaviour
{
    public static DatosDuelo datosDuelo;
    //datos solo necesarios para el duelo
    // nombre duelista cpu
    public string duelistaCpu ="";
    //deck del duelista cpu
    public List<int> deckCpuTemporal;
    // si suma historia
    public bool siSuma = true;
    //rango obenidp
    public int rango;
    //musica a sonar
    public int musica;
    //campo inicial
    public int campo;
    // si es historia
    public bool modoHistoria;
    // Start is called before the first frame update
    public int idDuelista;
    //posiciones y id de cuadro en free duel
    public int idCuadro = 0;
    public float posX = -270;
    public float posY = 100f;
    public int idScroll = 0;
    private int musicaDueloLibre = 0;

    private void Awake()
    {
        if (datosDuelo == null)
        {
            datosDuelo = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void SetDeckCpu(List<int> deck)
    {
        deckCpuTemporal = deck;
    }
    public List<int> GetDeckCpu()
    {
        return deckCpuTemporal;
    }
    public void SetDuelistaCpu(string nombre)
    {
        duelistaCpu = nombre;
    }
    public string GetDuelistaCpu()
    {
        return duelistaCpu;
    }
    public void SetCampo(int nombre)
    {
        campo = nombre;
    }
    public int GetCampo()
    {
        return campo;
    }
    public void SetModoHistoria(bool modo)
    {
        modoHistoria = modo;
    }
    public bool GetModoHistoria()
    {
        return modoHistoria;
    }
    public int GetIdDuelista()
    {
        return idDuelista;
    }
    public void SetIdDuelista(int id)
    {
        idDuelista = id;
    }
    public void SetMusicaDueloLibre(int numero)
    {
        musicaDueloLibre = numero;
    }
    public int GetMusicaDueloLibre()
    {
        return musicaDueloLibre;
    }

}
