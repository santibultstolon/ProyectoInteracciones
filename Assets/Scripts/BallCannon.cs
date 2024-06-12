using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCannon : MonoBehaviour
{
    public GameObject[] balls;
    public Transform shotPoint;
    void Start()
    {
        InvokeRepeating("SpawnBall", 5, Random.Range(5, 10));  
    }

public void SpawnBall()
    {
        GameObject newBall = Instantiate(balls[Random.Range(0, balls.Length)],shotPoint.position,transform.rotation);
        newBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 100));
    }
}
