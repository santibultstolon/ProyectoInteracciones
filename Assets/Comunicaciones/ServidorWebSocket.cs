using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class ServidorWebSocket : WebSocketBehavior
{
    private static int clientIdCounter = 0;
    public static ObjetoServidor server;
    public static bool lanzarMensaje;

    protected override void OnOpen()
    {
        clientIdCounter++;
        server.server = this;
        server.counts++;
        base.OnOpen();
        Debug.Log("++ Alguien se ha conectado. "+Sessions.Count);
        server.connectedPlayers = Sessions.Count;
        Send(clientIdCounter.ToString());
        Debug.Log("Enviado");

        //Methods.SpawnPlayer(clientIdCounter);
    }
    
    protected override void OnClose(CloseEventArgs e)
    {
         base.OnClose(e);
         Debug.Log("-- Se ha desconectado alguien. " + Sessions.Count);
        server.counts--;
        server.connectedPlayers = Sessions.Count;
    }

    protected override void OnMessage(MessageEventArgs e)
    {

        base.OnMessage(e);
        if(e.Data == "Empieza")
        {
            Sessions.Broadcast("Empieza");
        }
        Mensaje message = JsonUtility.FromJson<Mensaje>(e.Data);
       int id = message.id;
        float direction = message.direction;


        server.MoveRightPlayers(id, direction);

        //Methods.MovePlayer(id, direction);
    }
    public void MandarMensaje()
    {
        Debug.Log("He mandado tu puto mensaje");
        Sessions.Broadcast("Empieza");
        lanzarMensaje = false;
        Debug.Log("He mandado tu puto mensaje");
    }


}


