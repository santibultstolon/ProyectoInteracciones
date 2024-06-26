using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;
using System.Linq.Expressions;
using System;

public class ClienteWebSocket : MonoBehaviour
{

    WebSocketSharp.WebSocket ws;
    public Vector3 posicionRecibida;
    public bool nuevaPosicionRecibida = false;

    [SerializeField]
    public Mensaje misDatos = new Mensaje();
    public string ipPport;
    public TMP_InputField inputIP,inputUsername;
    public GameObject IPSession,waitingForHost,gamePad,exit1;
    public GameObject ola;
    public string username;
    public int id;
    public int contadorMensajes=0;
    public bool canStart;
    Quaternion rotacion = Quaternion.identity;
    public TextMeshProUGUI error;

    //127.0.0.1:8080
    // Start is called before the first frame update
    void Start()
    {
        misDatos = new Mensaje();
        misDatos.id = 0;
    }
    #region SetId
    private void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        //Debug.Log(JsonUtility.FromJson<int>(e.Data));
        //Debug.Log(e.Data);
        if(contadorMensajes == 0)
        {
            id = int.Parse(e.Data);
            misDatos.id= id;
            contadorMensajes++;
        }
        if (e.Data == "Empieza")
        {
            canStart= true;
        }
       
    }
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (ws.IsAlive)
            {
               // misDatos.direction.x = 1;
                ws.Send(JsonUtility.ToJson(misDatos));
            }
        }
        ipPport = inputIP.text;
       // username = inputUsername.text;
        if (canStart)
        {
            waitingForHost.SetActive(false);
            gamePad.SetActive(true);
            exit1.SetActive(false);
            canStart = false;
        }
        
    }
    public void Right() {
        if (ws.IsAlive)
        {
            misDatos.direction = "derecha";
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }  
    public void Left() {
        if (ws.IsAlive)
        {
            misDatos.direction = "izquierda";
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void Jump()
    {
        if (ws.IsAlive)
        {
            misDatos.jump = true;
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void NoJump()
    {
        if (ws.IsAlive)
        {
            misDatos.jump = false;
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void ConnectToServer()
    {
        ws = new WebSocket("ws://" + ipPport);
        ws.OnMessage += Ws_OnMessage;
        ws.Connect();
        IPSession.SetActive(false);
        waitingForHost.SetActive(true);
        exit1.SetActive(true);
        ipPport= "";

    }
    public void ZeroDirection()
    {
        if (ws.IsAlive)
        {
            misDatos.direction="nothing";
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void ExitIP()
    {
        gamePad.SetActive(false);
        waitingForHost.SetActive(false);
        IPSession.SetActive(true);
        ws.Close();
    }
}
