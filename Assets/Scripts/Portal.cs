using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string matchBall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == matchBall)
        {

            if (collision.gameObject.GetComponent<BallBehaviour>().player!=null)
            {
                if(collision.gameObject.GetComponent<BallBehaviour>().player.GetComponent<PlayerController>() != null)
                {
                    collision.gameObject.GetComponent<BallBehaviour>().player.GetComponent<PlayerController>().points++;
                }
                else if (collision.gameObject.GetComponent<BallBehaviour>().player.GetComponent<PlayerControllerHost>() != null)
                {
                    collision.gameObject.GetComponent<BallBehaviour>().player.GetComponent<PlayerControllerHost>().points++;
                }

            }

             
            Destroy(collision.gameObject);

        }
    }
}
