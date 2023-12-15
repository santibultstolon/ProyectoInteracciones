using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;


public class ObjetoServidor : MonoBehaviour
{

    WebSocketSharp.Server.WebSocketServer wss;

    public GameObject[] players;
    public GameObject player;
    GameManager manager;
    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public void SpawnPlayer()
    {
        Debug.Log("Spawneando");
        manager.PlayerSpawn();
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
    void Spawnn()
    {
        player.SetActive(false);
    }
}
