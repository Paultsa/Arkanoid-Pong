using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform startPoint;
    public Transform startPoint2;
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
    int direction = 0;
    public int whoScores;

    public int maxSpeed;

    public Text spaceText;
    public Text shiftText;

    // At start gets the rigidbody for the ball
    void Start()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        spaceText.text = "(Press Space to Launch)";
        
        whoScores = 0;
        rb = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            multiplayer = true;
            shiftText.text = "(Press Right Shift to Launch)";
        }
        else
        {
            multiplayer = false;
        }
        if (multiplayer)
        {
            Player1 = P1.GetComponent<LocalPlayer>();
            Player2 = P2.GetComponent<LocalPlayer>();
        }
        else
        {
            Player = P1.GetComponent<PlayerScript>();
        }
    }

    void Update()
    {
        if (multiplayer)
        {
            if (player1)
            {
                direction = Player1.getDirection(player1);
            }
            else
            {
                direction = Player2.getDirection(player1);
            }
        }
        else
        {
            direction = Player.getDirection();
        }
        // If the ball is on the paddle, follow paddles position
        if (!inPlay)
        {
            if (multiplayer && player1)
            {
                transform.position = startPoint.position;
            }
            else if (multiplayer && !player1)
            {
                transform.position = startPoint2.position;
            }
            else
            {
                transform.position = startPoint.position;
            }
        }
        // If spacebar is pressed and the ball is on the paddle, add force to ball
        if(Input.GetButtonDown("Jump") && !inPlay && player1)
        {
            spaceText.text = "";
            int rand = Random.Range(0, 2);
            Debug.Log(rand);
            if (multiplayer)
            {
                if (direction == 1)
                {
                    inPlay = true;
                    Debug.Log("direction1");
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.right * speed);
                }
                else if (direction == -1)
                {
                    inPlay = true;
                    Debug.Log("direction2");
                    rb.AddForce(Vector2.down * speed);
                    rb.AddForce(Vector2.right * speed);
                }
                else if (direction == 0 && rand == 0)
                {
                    Debug.Log("rand");
                    inPlay = true;
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.right * speed);
                }
                else
                {
                    Debug.Log("rand2");
                    inPlay = true;
                    rb.AddForce(Vector2.down * speed);
                    rb.AddForce(Vector2.right * speed);
                }
            }
            else
            {
                if (direction == 1)
                {
                    inPlay = true;
                    Debug.Log("direction1");
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.right * speed);
                }
                else if (direction == -1)
                {
                    inPlay = true;
                    Debug.Log("direction2");
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.left * speed);
                }
                else if (direction == 0 && rand == 0)
                {
                    Debug.Log("rand");
                    inPlay = true;
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.right * speed);
                }
                else
                {
                    Debug.Log("rand2");
                    inPlay = true;
                    rb.AddForce(Vector2.up * speed);
                    rb.AddForce(Vector2.left * speed);
                }
            }
            StartCoroutine(speedingUp());
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && !inPlay && !player1)
        {
            shiftText.text = "";
            int rand = Random.Range(0, 2);
            Debug.Log(rand);

            if (direction == 1)
            {
                inPlay = true;
                Debug.Log("direction1");
                rb.AddForce(Vector2.up * speed);
                rb.AddForce(Vector2.left * speed);
            }
            else if (direction == -1)
            {
                inPlay = true;
                Debug.Log("direction2");
                rb.AddForce(Vector2.down * speed);
                rb.AddForce(Vector2.left * speed);
            }
            else if (direction == 0 && rand == 0)
            {
                Debug.Log("rand");
                inPlay = true;
                rb.AddForce(Vector2.up * speed);
                rb.AddForce(Vector2.left * speed);
            }
            else
            {
                Debug.Log("rand2");
                inPlay = true;
                rb.AddForce(Vector2.down * speed);
                rb.AddForce(Vector2.left * speed);
            }
            StartCoroutine(speedingUp());
        }
        if(inPlay)
        {
            Debug.Log("RB.Velocity.x " + rb.velocity.x);
            Debug.Log("RB.Velocity.y " + rb.velocity.y);
            if(rb.velocity.x < 2 && rb.velocity.x > 0)
            {
                rb.AddForce(Vector2.right * 20);
            }
            else if(rb.velocity.x > -2 && rb.velocity.x < 0)
            {
                rb.AddForce(Vector2.left * 20);
            }
            if(rb.velocity.y < 2 && rb.velocity.y > 0)
            {
                rb.AddForce(Vector2.up * 20);
            }
            else if(rb.velocity.y > -2 && rb.velocity.y < 0)
            {
                rb.AddForce(Vector2.down * 20);
            }

            if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)) > maxSpeed)
            {
                rb.velocity -= rb.velocity * 0.1f;
            }
            //Debug.Log(Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If ball collides with bottom line, reset position on the paddle and lose life
        if (other.CompareTag("Bottom"))
        {
            Debug.Log("Lose life");
            rb.velocity = Vector2.zero;
            inPlay = false;
            //player1 = true;
            gm.UpdateLives(-1);
        }
        if (other.CompareTag("Player1Goal"))
        {
            Debug.Log("Lose life");
            rb.velocity = Vector2.zero;
            inPlay = false;
            //player1 = false;
            gm.UpdateLivesP2(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        sm.Hit();
        if (other.transform.CompareTag("Brick"))
        {
            Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
            var main = newExplosion.GetComponent<ParticleSystem>().main;
            main.startColor = other.transform.GetComponent<SpriteRenderer>().color;
            if(other.gameObject.GetComponent<BrickScript>().chance == 0)
            {
                Instantiate(powerUp, other.transform.position, other.transform.rotation);
            }
            Destroy(newExplosion.gameObject, 2.5f);
            if (whoScores == 0)
            {
                gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            } else
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
        else if (other.transform.CompareTag("Player2") ||other.transform.CompareTag("Shield2"))
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

