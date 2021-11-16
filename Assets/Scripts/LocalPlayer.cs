using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{

    public float topEdge;
    public float botEdge;
    public float speed;
    public float playerNumber;
    public static int direction1;
    public static int direction2;

    public int shotAmount;
    public int shootingTime;

    public int shieldTime;
    public int expandTime;

    public GameObject shield;

    public GameObject shot;
    public GameObject newBall;
    public GameObject gun;

    public Animator anim;

    public GameManager gm;
    public SoundManager sm;

    public bool shielded = false;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction1 = 1;
                transform.Translate(Vector2.right * Time.deltaTime * -speed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction1 = -1;
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else if ((Input.GetKeyUp(KeyCode.W) && !Input.GetKey(KeyCode.S)) || (Input.GetKeyUp(KeyCode.S) && !Input.GetKey(KeyCode.W)))
            {
                direction1 = 0;
            }
            if (transform.position.y < botEdge)
            {
                transform.position = new Vector2(transform.position.x, botEdge);
            }
            if (transform.position.y > topEdge)
            {
                transform.position = new Vector2(transform.position.x, topEdge);
            }

        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction2 = 1;
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction2 = -1;
                transform.Translate(Vector2.right * Time.deltaTime * -speed);
            }
            else if ((Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)))
            {
                direction2 = 0;
            }
            if (transform.position.y < botEdge)
            {
                transform.position = new Vector2(transform.position.x, botEdge);
            }
            if (transform.position.y > topEdge)
            {
                transform.position = new Vector2(transform.position.x, topEdge);
            }

        }
    }

    public int getDirection(bool player1)
    {
        if (player1)
        {
            return direction1;
        }
        else
        {
            return direction2;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            switch (other.GetComponent<PowerUp>().i)
            {
                case 1:
                    //Green (Heal)
                    Heal();
                    break;
                case 2:
                    //Red (Shoot)
                    Shoot();
                    break;
                case 3:
                    //Blue (Shield)
                    Shield();
                    break;
                case 4:
                    //Yellow (Expand)
                    Expand();
                    break;
                case 5:
                    //Magenta (New Ball)
                    NewBall();
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    void Heal()
    {
        if (playerNumber == 1)
        {
            gm.UpdateLives(1);
        }
        else if (playerNumber == 2)
        {
            gm.UpdateLivesP2(1);
        }
    }

    void Shoot()
    {
        StartCoroutine(Shooting());
    }

    void Shield()
    {
        sm.Shield();
        shield.SetActive(true);
        StartCoroutine(Shielding());
    }

    void Expand()
    {
        sm.Expand();
        StartCoroutine(Expanded());
    }

    void NewBall()
    {
        if (playerNumber == 1)
        {
            Instantiate(newBall, new Vector2(transform.position.x + 1.5f, transform.position.y), transform.rotation);
        }
        else
        {
            Instantiate(newBall, new Vector2(transform.position.x - 1.5f, transform.position.y), transform.rotation);
        }

    }


    IEnumerator Shooting()
    {
        gun.SetActive(true);
        for (int i = 0; i < shotAmount; i++)
        {
            yield return new WaitForSeconds(shootingTime);
            if (playerNumber == 1)
            {
                Instantiate(shot, new Vector2(transform.position.x + 1.5f, transform.position.y), transform.rotation);
            }
            else
            {
                Instantiate(shot, new Vector2(transform.position.x - 1.5f, transform.position.y), transform.rotation);
            }
            sm.Shot();
        }
        gun.SetActive(false);
    }

    IEnumerator Expanded()
    {
        if (!anim.GetBool("Expanded"))
        {
            anim.SetBool("Expanded", true);
            yield return new WaitForSeconds(expandTime);
            anim.SetBool("Expanded", false);
        }
    }

    IEnumerator Shielding()
    {
        shielded = true;
        for (int i = 0; i < shieldTime; i++)
        {
            yield return new WaitForSeconds(1);
            if (shieldTime - i == 3)
            {
                StartCoroutine(Blink());
            }
        }
        shield.SetActive(false);
        shielded = false;
    }

    IEnumerator Blink()
    {
        while (shield.activeSelf)
        {
            Color temp = shield.GetComponent<SpriteRenderer>().color;
            temp.a = 0.5f;
            shield.GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.2f);
            temp.a = 1f;
            shield.GetComponent<SpriteRenderer>().color = temp;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

