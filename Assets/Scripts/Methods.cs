using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public static class Methods
{
    // Variables est�ticas
    public static int miVariableCompartida = 0;
    public static List<PlayerController> players = new List<PlayerController>();
    // M�todo est�tico
    public static void MovePlayer(int id, Vector2 direction)
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
        serverObject.SpawnPlayer(id);
    }
}