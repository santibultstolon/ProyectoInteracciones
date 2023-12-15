using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ClienteWebSocket : MonoBehaviour
{

    WebSocketSharp.WebSocket ws;
    public Vector3 posicionRecibida;
    public bool nuevaPosicionRecibida = false;

    [SerializeField]
    public Mensaje misDatos = new Mensaje();

    public GameObject ola;

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
                misDatos.direction = new Vector2(1, 0);
                ws.Send(JsonUtility.ToJson(misDatos));
            }
        }
    }
    public void Right() {
        if (ws.IsAlive)
        {
            misDatos.direction = new Vector2(1, 0);
            ws.Send(JsonUtility.ToJson(misDatos));
        }
    }
    public void ConnectToServer()
    {
        ws.Connect();
    }
}
