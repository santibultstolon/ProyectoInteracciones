using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Clientepruebas : MonoBehaviour
{
    WebSocketSharp.WebSocket ws;
    public Vector3 posicionRecibida;
    public bool nuevaPosicionRecibida = false;

    [SerializeField]
    public Mensaje misDatos = new Mensaje();

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://127.0.0.1:8080");
        ws.OnMessage += Ws_OnMessage;
        misDatos = new Mensaje();
        misDatos.id = 0;
    }
    #region SetId
    private void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        int idDatos = JsonUtility.FromJson<int>(e.Data);
        misDatos.id = int.Parse(e.Data);
    }
    #endregion
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)&&ws.IsAlive)
        {
            Debug.Log("Sigues conectao pisha");
        }
    }

    public void Right()
    {
        if (ws.IsAlive)
        {
           // misDatos.direction.x =1;
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void ConnectServer()
    {
        
            ws.Connect();
        
    }
}
