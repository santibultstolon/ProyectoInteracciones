using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Methods: MonoBehaviour
{
    // Variables estáticas
    public int miVariableCompartida = 0;
    public List<PlayerController> players = new List<PlayerController>();
    public static Methods Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    // Método estático
    public void MovePlayer(int id, Vector2 direction)
    {
        for(int i = 0; i < players.Count; i++) {
            if(id == players[i].id)
            {
                players[i].MovePlayer(direction);
                break;
            }  
        }
    }
    public static void SpawnPlayer(int id)
    {
        ObjetoServidor serverObject = GameObject.Find("ServerObject").GetComponent<ObjetoServidor>();
    }
}