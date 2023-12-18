using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PruebaScriptSuelo : MonoBehaviour
{
    public PlayerController[] mmkbo;
    ObjetoServidor server;
    int connect;

    private void Awake()
    {
        server = GameObject.Find("ServerObject").GetComponent<ObjetoServidor>();
        connect = server.connectedPlayers;
        server = null;

    }
    private void Start()
    {
        for (int i = 0; i == connect-1; i++)
        {
            Debug.Log("Se ha activado");
             mmkbo[i].gameObject.SetActive(true);
        }
    }
}
