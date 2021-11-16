using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public float speed;
    public Transform explosion;
    public Transform powerUp;
    public GameManager gm;
    public SoundManager sm;
    public bool player1 = true;
    public bool multiplayer;
    SceneManager SceneManager;
    public GameObject P1;
    public GameObject P2;
    public LocalPlayer Player1;
    public LocalPlayer Player2;
    public PlayerScript Player;
    public GameObject ball;
    public int whoScores;

    public int maxSpeed;

    // At start gets the rigidbody for the ball
    void Start()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        int rand = Random.Range(0, 2);
        whoScores = 0;
        rb = GetComponent<Rigidbody2D>();

        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            multiplayer = true;
            ball = GameObject.FindWithTag("Ball1");
        }
        else
        {
            multiplayer = false;
            ball = GameObject.FindWithTag("Ball");
        }
        if (multiplayer)
        {
            speed -= 100;
            P1 = GameObject.FindWithTag("Player1");
            P2 = GameObject.FindWithTag("Player2");
           
            Player1 = P1.GetComponent<LocalPlayer>();
            Player2 = P2.GetComponent<LocalPlayer>();
            if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(P1.transform.position.x, 0))
                < Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(P2.transform.position.x, 0)))
            {
                if (rand == 0)
                {
                    inPlay = true;
                    rb.AddForce(Vector2.right * speed);
                    rb.AddForce(Vector2.up * speed);
                }
                else if (rand == 1)
                {
                    inPlay = true;
                    rb.AddForce(Vector2.right * speed);
                    rb.AddForce(Vector2.down * speed);
                }
            }
            else
            {
                if (rand == 0)
                {
                    inPlay = true;
                    rb.AddForce(Vector2.left * speed);
                    rb.AddForce(Vector2.up * speed);
                }
                else
                {
                    inPlay = true;
                    rb.AddForce(Vector2.left * speed);
                    rb.AddForce(Vector2.down * speed);
                }
            }
        }
        else
        {
            P1 = GameObject.FindWithTag("Player");
            Player = P1.GetComponent<PlayerScript>();
            

            if (rand == 0)
            {
                inPlay = true;
                rb.AddForce(Vector2.up * speed);
                rb.AddForce(Vector2.right * speed);
            }
            else
            {
                inPlay = true;
                rb.AddForce(Vector2.up * speed);
                rb.AddForce(Vector2.left * speed);
            }
        }

        StartCoroutine(speedingUp());
        
        
    }

    void Update()
    {
        if (!inPlay)
        {
            Destroy(gameObject);
        }

        if (inPlay)
        {
            if (rb.velocity.x < 2 && rb.velocity.x > 0)
            {
                rb.AddForce(Vector2.right * 20);
            }
            else if (rb.velocity.x > -2 && rb.velocity.x < 0)
            {
                rb.AddForce(Vector2.left * 20);
            }
            if (rb.velocity.y < 2 && rb.velocity.y > 0)
            {
                rb.AddForce(Vector2.up * 20);
            }
            else if (rb.velocity.y > -2 && rb.velocity.y < 0)
            {
                rb.AddForce(Vector2.down * 20);
            }

            if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)) > ball.GetComponent<BallScript>().maxSpeed)
            {
                rb.velocity -= rb.velocity * 0.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom") && multiplayer)
        {
            Debug.Log("Lose life");
            inPlay = false;
            player1 = true;
            gm.UpdateLives(-1);
        }
        if (other.CompareTag("Player1Goal") && multiplayer)
        {
            Debug.Log("Lose life");
            inPlay = false;
            player1 = false;
            gm.UpdateLivesP2(-1);
        }
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        sm.Hit();
        if (other.transform.CompareTag("Brick"))
        {
            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            if (other.gameObject.GetComponent<BrickScript>().chance == 0)
            {
                Transform PU = Instantiate(powerUp, other.transform.position, other.transform.rotation);
                PU.GetComponent<PowerUp>().newBall = true;
                if (gm.multiplayer)
                {
                    if (whoScores == 1)
                    {
                        PU.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        PU.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * PU.GetComponent<PowerUp>().speed);
                    }
                    else
                    {
                        PU.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        PU.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * PU.GetComponent<PowerUp>().speed);
                    }
                }
            }
            Destroy(newExplosion.gameObject, 2.5f);
            if (whoScores == 0)
            {
                gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            }
            else
            {
                gm.UpdateScoreP2(other.gameObject.GetComponent<BrickScript>().points);
            }
            Destroy(other.gameObject);
        }
        else if (other.transform.CompareTag("Player"))
        {
            rb.AddForce(new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y));
        }
        else if (other.transform.CompareTag("Player1") || other.transform.CompareTag("Shield1"))
        {
            whoScores = 0;
            rb.AddForce(new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y));
        }
        else if (other.transform.CompareTag("Player2") || other.transform.CompareTag("Shield2"))
        {
            whoScores = 1;
            rb.AddForce(new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y));
        }
    }

    public int getWhoScores()
    {
        return whoScores;
    }

    IEnumerator speedingUp()
    {
        while (inPlay)
        {
            yield return new WaitForSeconds(7);
            rb.AddForce(rb.velocity * 3);

        }
    }
}

