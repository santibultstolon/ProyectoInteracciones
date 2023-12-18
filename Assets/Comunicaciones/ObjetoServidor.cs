using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp.Server;
using WebSocketSharp;


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
    public TextMeshProUGUI connectedText;
    public ServidorWebSocket server;
    public GameObject startButton, connectButton,floor;
    bool empezarJuego;
    
    
    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            StartCoroutine("StartGame");
            empezarJuego = false;
        }
    }


    public void SpawnPlayer()
    {
        Debug.Log("Spawneando");
        Debug.Log("Spawneando2");
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
    public void MoveRightPlayers(int id,float direction)
    {
        if (direction != 0)
        {
            players[id-1].canMove = true;
        }
        else
        {
            players[id - 1].canMove = false;
        }
        players[id - 1].directione = direction;
    }

    public IEnumerator StartGame()
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
        for(int i = 1; i < connectedPlayers; i++)
        {
            Debug.Log("Se ha activado");
           // mmkbo[i].gameObject.SetActive(true);
        }
        estate = estado.playing;
        floor.SetActive(true);
        server.MandarMensaje();
        yield return new WaitForSeconds(0.5f);


    }
    public void PlayyYa()
    {
        empezarJuego = true;
    }
}
