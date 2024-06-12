using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public string ballClass;
    public GameObject player;
    private void Start()
    {
        if (gameObject.name == "OrangeBall(Clone)")
        {
            player = GameObject.Find("Player1");
        }       
        if (gameObject.name == "GreenBall(Clone)")
        {
            player = GameObject.Find("Player2");
        }       
        if (gameObject.name == "PinkBall(Clone)")
        {
            player = GameObject.Find("Player3");
        }     
        if (gameObject.name == "BlueBall(Clone)")
        {
            player = GameObject.Find("Player4");
        }
    }
}
