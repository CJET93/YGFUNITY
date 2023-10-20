using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Conexion : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    [SerializeField]
    private TextMeshProUGUI textoConexion;

    [SerializeField]
   
    private TextMeshProUGUI nickUsuario1;

    [SerializeField]

    private TextMeshProUGUI nickUsuario2;



    private GameObject btnLoad;

    [SerializeField]
    private List<int> juador1;
    [SerializeField]
    private List<int> juador2;

    private GameObject objetoDatosJuego;
    private GameObject objetoDatosDuelo;
    private DatosJuego datosJuego;
    private DatosDuelo datosDuelo;
    public transicion transicion;
    private Player player;

    private ExitGames.Client.Photon.Hashtable customPropeties = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable customPropeties1 = new ExitGames.Client.Photon.Hashtable();
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        textoConexion.text = "ESPERANDO OPONENTE";
        PhotonNetwork.JoinRandomOrCreateRoom();

        //PhotonNetwork.JoinRandomRoom();
 

    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public void Conext(){
    
        PhotonNetwork.JoinRoom("prueba");
    }

    public override void OnJoinedRoom()
    {


   
        photonView.RPC("ActualizarLobby", RpcTarget.All);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if(targetPlayer!=null && targetPlayer == player)
        {
            if (changedProps.ContainsKey("deck"))
            {
                Debug.LogWarning("entra aca");


                setrarLista(targetPlayer);
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        player = newPlayer;
        //player.SetCustomProperties(customPropeties);
        nickUsuario2.text = player.NickName;
    }

    public void setrarLista(Player player)
    {
        //fdfddf
        Debug.Log("lidrock "+player.CustomProperties["deck"]);
    }
    [PunRPC]
    public void ActualizarLobby()
    {
 
        nickUsuario1.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        PhotonNetwork.LocalPlayer.CustomProperties = customPropeties;
       
        if (!PhotonNetwork.IsMasterClient)
        {
            datosJuego.esJugadorUno = false;
            Debug.Log("No soy el jege maestro");
            PhotonNetwork.LocalPlayer.CustomProperties = customPropeties;
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("deck"))
        {
            Debug.Log("tengo acceso ak deck de local");
            Debug.Log("su respuesta es " + PhotonNetwork.LocalPlayer.CustomProperties["deck"]);
        }
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            customPropeties["dos"] = "noMaster";
            PhotonNetwork.CurrentRoom.GetPlayer(2).CustomProperties=customPropeties;
            textoConexion.text = "OPONENTE " + nickUsuario2.text + " Encontrado";
            Invoke("cargarescena", 2f);
        }
    }

    public void pruebaboton()
    {
        cargarJuego("juegoOnline");
    }

    public void seteo()
    {
      
    }



    public void cargarescena()
    {
        
       photonView.RPC("cargarJuego",RpcTarget.All,"JuegoOnline");
    }
    [PunRPC]
    public void cargarJuego(string escena)
    {
        //transicion.CargarEscena(escena);
        PhotonNetwork.LoadLevel(escena);
       
    }

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        objetoDatosJuego = GameObject.Find("DatosJuego");
        datosJuego = objetoDatosJuego.GetComponent<DatosJuego>();
        PhotonNetwork.AutomaticallySyncScene = true;
        ArrayList prueba = new ArrayList();
        Debug.LogWarning(prueba);
        customPropeties["deck"] = datosJuego.GetDeckUsuario();
        PhotonNetwork.NickName = datosJuego.nombreJugador;
    
      



        Connect();
    }

    void Start()
    {
      
    }  

    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
  
         

            //PhotonNetwork.JoinRandomRoom();
  

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            //PhotonNetwork.GameVersion = gameVersion;
        }
    }
}
