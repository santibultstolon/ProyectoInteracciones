using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;

public class ServidorWebSocket : WebSocketBehavior
{
    private static int clientIdCounter = 1;
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
        string direcc = message.direction;
        bool jump = message.jump;
      /*  if (direcc == "derecha")
        {

        }  
        if (direcc == "izquierda")
        {

        }*/
        int id = message.id;
        server.MoveRightPlayers(id, direcc,jump);

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


