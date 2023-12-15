using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp.Server;


public class ObjetoServidor : MonoBehaviour
{

    WebSocketSharp.Server.WebSocketServer wss;

    public PlayerController[] players;
    public PlayerController player;
    GameManager manager;
    public int counts;
    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log(player.gameObject.name);
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

    }
    public void MoveRightPlayers(int id,float direction)
    {
        Debug.Log("derecha");
        if (direction != 0)
        {
            player.canMove = true;
        }
        else
        {
            player.canMove = false;
        }
        player.directione= new Vector2(direction,0);
    }
}
