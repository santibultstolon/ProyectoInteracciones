using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;


public class ObjetoServidor : MonoBehaviour
{

    WebSocketSharp.Server.WebSocketServer wss;

    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // iniciar el servidor (a mano, para que solo haya uno)
            wss = new WebSocketServer(8080);
            wss.Start();
            wss.AddWebSocketService<ServidorWebSocket>("/");
            Debug.Log("servidor iniciado...");
        }
    }

    public void SpawnPlayer(int id)
    {
        GameObject newPlayer = Instantiate(players[id]);
    }
}
