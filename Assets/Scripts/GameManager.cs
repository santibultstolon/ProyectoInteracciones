using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public void GoToHost()
    {
        SceneManager.LoadScene("Host");
    }
    public void GoToClient()
    {
        SceneManager.LoadScene("Client");
    }

    public void PlayerSpawn()
    {
        player.SetActive(false);  
    }
}
