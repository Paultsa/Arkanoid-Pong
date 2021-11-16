using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public Transform explosion;
    public GameObject powerUp;
    public GameObject ball;
    public GameObject ball1;
    public GameObject ball2;
    public BallScript ballScript;
    public GameManager gm;
    Rigidbody2D rb;
    public float shotForce;

    public GameObject P1;
    public GameObject P2;
    // Start is called before the first frame update
    void Start()
    {
       
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        if (!gm.multiplayer)
        {
            ball = GameObject.FindWithTag("Ball");
            ballScript = ball.GetComponent<BallScript>();
            rb.AddForce(Vector2.up * shotForce);
        }
        else
        {
            ball1 = GameObject.FindWithTag("Ball1");
            ball2 = GameObject.FindWithTag("Ball2");
            P1 = GameObject.FindWithTag("Player1");
            P2 = GameObject.FindWithTag("Player2");

            if (Vector2.Distance(new Vector2(transform.position.x, 0),new Vector2(P1.transform.position.x, 0))
                < Vector2.Distance(new Vector2(transform.position.x,0), new Vector2(P2.transform.position.x,0)))
            {
                // Lähempänä P1
                rb.AddForce(Vector2.right * shotForce);
                ballScript = ball1.GetComponent<BallScript>();
            } else
            {
                rb.AddForce(Vector2.left * shotForce);
                ballScript = ball2.GetComponent<BallScript>();
            }
        }
        Debug.Log(gm.multiplayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Brick"))
        {
            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            if (other.gameObject.GetComponent<BrickScript>().chance == 0)
            {
                Instantiate(powerUp, other.transform.position, other.transform.rotation);
            }
            Destroy(newExplosion.gameObject, 2.5f);
            if (ballScript.getWhoScores() == 0)
            {
                gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            }
            else
            {
                gm.UpdateScoreP2(other.gameObject.GetComponent<BrickScript>().points);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player1"))
        {
            gm.UpdateLives(-1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player2"))
        {
            gm.UpdateLivesP2(-1);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
        
    }
}
