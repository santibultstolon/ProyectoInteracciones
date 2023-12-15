using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;

public class ClienteWebSocket : MonoBehaviour
{

    WebSocketSharp.WebSocket ws;
    public Vector3 posicionRecibida;
    public bool nuevaPosicionRecibida = false;

    [SerializeField]
    public Mensaje misDatos = new Mensaje();
    public string ipPport;
    public TMP_InputField inputIP;
    public GameObject IPSession,waitingForHost,gamePad;
    bool hasWaited;
    public GameObject ola;
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
        misDatos.id = JsonUtility.FromJson<int>(e.Data);
        Debug.Log(misDatos.id);
        Instantiate(ola, new Vector3(0, 0, 0), transform.rotation);
    }
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (ws.IsAlive)
            {
                misDatos.direction = 1;
                ws.Send(JsonUtility.ToJson(misDatos));
            }
        }
        ipPport = inputIP.text;
        if (hasWaited)
        {
            waitingForHost.SetActive(true);
            gamePad.SetActive(true);
            hasWaited = false;

        }
    }
    public void Right() {
        if (ws.IsAlive)
        {
            misDatos.direction = 0.1f;
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }  
    public void Left() {
        if (ws.IsAlive)
        {
            misDatos.direction = -0.1f;
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
        ipPport= "";

    }
    public void ZeroDirection()
    {
        if (ws.IsAlive)
        {
            misDatos.direction = 0;
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void ExitIP()
    {
        gamePad.SetActive(false);
        IPSession.SetActive(true);
        ws.Close();
    }
}
