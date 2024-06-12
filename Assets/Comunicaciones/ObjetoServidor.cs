using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp.Server;
using WebSocketSharp;
using UnityEditor;
using System.Xml;
using System.Net.Sockets;
using System.Net;
using UnityEngine.SceneManagement;

public class ObjetoServidor : MonoBehaviour
{

    WebSocketSharp.Server.WebSocketServer wss;

    public PlayerController[] players;
    public PlayerController[] mmkbo;
    public PlayerController player;
    GameManager manager;
    public int counts;
    enum estado { playing, lobby };
    estado estate = estado.lobby;
    public GameObject hostLobby;
   public int connectedPlayers;
    public TextMeshProUGUI connectedText,IPText;
    public ServidorWebSocket server;
    public GameObject startButton, connectButton,floor;
    bool empezarJuego;
    public GameObject gameplayUI;
    
    
    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        string localIPAddress = GetLocalIPAddress();
        IPText.text ="Your IP Address is: "+ localIPAddress+":8080";
    }
    private void Update()
    {
        if(estate == estado.lobby)
        {
            connectedText.text = "CONNECTED PLAYERS: "+ connectedPlayers;
            hostLobby.SetActive(true);
        }
        else if(estate == estado.playing)
        {
            hostLobby.SetActive(false);
        }
        if (empezarJuego)
        {
            StartGame();
            empezarJuego = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            wss.Stop();
            SceneManager.LoadScene("Host");
        }
    }

    public void StartServer()
    {
        wss = new WebSocketServer(8080);
        wss.Start();
        wss.AddWebSocketService<ServidorWebSocket>("/");
        Debug.Log("servidor iniciado...");
        ServidorWebSocket.server = gameObject.GetComponent<ObjetoServidor>();
        Debug.Log("servidor añadidoiniciado...");
        connectButton.SetActive(false);
        startButton.SetActive(true);

    }
    public void MoveRightPlayers(int id,string direction,bool jump)
    {
        /* if (direction =="derecha")
         {
             players[id].derecha = true;
         }
         else if(direction=="izquierda")
         {
             players[id].izquierda = true;
         }    

         if (direction =="nothing")
         {
             players[id].derecha = false;
             players[id].izquierda = false;
         }
         */
        for(int i=1;i<players.Length;i++)
        {
            if (id == players[i].id) {
                players[i].messages = direction;
                players[i].jump = jump;
                
            }
        }

        
    }

    public void StartGame()
    {
        //Por si se bugeara, revisa que la lista de jugadores esté vacía, si no lo está, lo borra todo.
        /* if (players[0] != null)
         {
             for (int i = 0; i < players.Count; i++)
             {
                 players.Remove(players[i]);
             }

         }*/
        /*for(int i =0;i<foundPlayers.Length;i++)
        {
            players[i] = foundPlayers[i];
        }*/
        estate = estado.playing;
        floor.SetActive(true);
        server.MandarMensaje();
        gameplayUI.SetActive(true);
        


    }
    public void PlayyYa()
    {
        empezarJuego = true;
    }
    private void OnDestroy()
    {
      //  wss.Stop();
    }

    string GetLocalIPAddress()
    {
        string localIP = "127.0.0.1";

        try
        {
            // Obtiene todas las direcciones IP asociadas con la máquina
            IPAddress[] localIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());

            // Busca la primera dirección IPv4 válida
            foreach (IPAddress ipAddress in localIPAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ipAddress.ToString();
                    break;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al obtener la dirección IP local: " + e.Message);
        }

        return localIP;
    }

}
