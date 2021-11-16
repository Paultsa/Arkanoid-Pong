using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject ball;
    public GameObject ball1;
    public GameObject ball2;
    BallScript ballScript;
    public int rand;
    SpriteRenderer spriteRend;
    GameObject player1;
    GameObject player2;
    GameObject player;
    Color color;
    public float speed;
    Rigidbody2D rb;
    public int i;

    public bool newBall = false;

    public GameManager gm;
    public SoundManager sm;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        sm.PowerUp();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();

        rand =  Random.Range(0, 5);
        if (gm.multiplayer)
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            ball1 = GameObject.FindWithTag("Ball1");
            ball2 = GameObject.FindWithTag("Ball2");

            if(Vector2.Distance(ball1.transform.position, transform.position) < Vector2.Distance(ball2.transform.position, transform.position))
            {
                ballScript = ball1.GetComponent<BallScript>();
            }
            else
            {
                ballScript = ball2.GetComponent<BallScript>();
            }
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            ball = GameObject.FindWithTag("Ball");
            ballScript = ball.GetComponent<BallScript>();
            rb.AddForce(new Vector2(0, -1) * speed);
        }
        
        if(ballScript.whoScores == 1 && !newBall)
        {
            rb.AddForce(new Vector2(1,0) * speed);
        }
        else if(ballScript.whoScores == 0 && !newBall)
        {
            if(ballScript.multiplayer)
            {
                rb.AddForce(new Vector2(-1, 0) * speed);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (rand)
        {
            case 0:
                i = 1;
                spriteRend.color = Color.green;
                break;
            case 1:
                i = 2;
                spriteRend.color = Color.red;
                break;
            case 2:
                i = 3;
                if(gm.multiplayer)
                {
                    if (rb.velocity.x < 0)
                    {
                        if(player1.GetComponent<LocalPlayer>().shielded)
                        {
                            rand = 1;
                        }
                        else
                        {
                            spriteRend.color = Color.blue;
                        }
                    }
                    else
                    {
                        if(player2.GetComponent<LocalPlayer>().shielded)
                        {
                            rand = 1;
                        }
                        else
                        {
                            spriteRend.color = Color.blue;
                        }
                    }
                }
                else if (player.GetComponent<PlayerScript>().shielded)
                {
                    rand = 1;
                }
                else
                {
                    spriteRend.color = Color.blue;
                }
                break;
            case 3:
                i = 4;
                if (gm.multiplayer)
                {
                    if (rb.velocity.x < 0)
                    {
                        if (player1.GetComponent<LocalPlayer>().anim.GetBool("Expanded"))
                        {
                            rand = 4;
                        }
                        else
                        {
                            spriteRend.color = Color.yellow;
                        }
                    }
                    else
                    {
                        if (player2.GetComponent<LocalPlayer>().anim.GetBool("Expanded"))
                        {
                            rand = 4;
                        }
                        else
                        {
                            spriteRend.color = Color.yellow;
                        }
                    }
                }
                else if (player.GetComponent<PlayerScript>().anim.GetBool("Expanded"))
                {
                    rand = 4;
                }
                else
                {
                    spriteRend.color = Color.yellow;
                }
                break;
            case 4:
                i = 5;
                spriteRend.color = Color.magenta;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
