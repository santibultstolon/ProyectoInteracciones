using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaScriptSuelo : MonoBehaviour
{
    public GameObject[] mmkbo;
    ObjetoServidor server;
    public int connect;

    private void Awake()
    {
        server = GameObject.Find("ServerObject").GetComponent<ObjetoServidor>();
        connect = server.connectedPlayers;
       
    }
    private void Start()
    {
        for (int i = 0; i <connect+1; i++)
        {
            Debug.Log("Se ha activado");
             mmkbo[i].SetActive(true);
        }
    }
}
