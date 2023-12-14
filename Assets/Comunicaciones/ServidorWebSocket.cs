using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class ServidorWebSocket : WebSocketBehavior
{
    private static int clientIdCounter = 0;
    protected override void OnOpen()
    {
        clientIdCounter++;
        base.OnOpen();
        Debug.Log("++ Alguien se ha conectado. "+Sessions.Count);
        
        Send(clientIdCounter.ToString());
    }

    
    protected override void OnClose(CloseEventArgs e)
    {
         base.OnClose(e);
         Debug.Log("-- Se ha desconectado alguien. " + Sessions.Count);
    }

    protected override void OnMessage(MessageEventArgs e)
    {

        base.OnMessage(e);
        Mensaje message = JsonUtility.FromJson<Mensaje>(e.Data);
        int id = message.id;
        Vector2 direction = message.direction;

        Methods.MovePlayer(id, direction);
        

    }

}


