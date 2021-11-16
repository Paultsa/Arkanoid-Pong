using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float rightEdge;
    public float leftEdge;
    public static int direction;
    public int shootingTime;
    public int shotAmount;

    public int shieldTime;
    public int expandTime;

    public GameObject shot;
    public GameObject newBall;
    public GameObject shield;
    public GameObject gun;
    public GameManager gM;
    public SoundManager sm;

    public bool shielded = false;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            transform.Translate(Vector2.right * Time.deltaTime * -speed);
        }
        else if ((Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A)))
        {
            direction = 0;
        }

        if (transform.position.x < leftEdge)
        {
            transform.position = new Vector2(leftEdge, transform.position.y);
        }
        if (transform.position.x > rightEdge)
        {
            transform.position = new Vector2(rightEdge, transform.position.y);
        }
    }

    public int getDirection()
    {
        return direction;
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
        gM.UpdateLives(1);
    }

    void Shoot()
    {
        StartCoroutine(Shooting());
    }

    void Shield()
    {
        shield.SetActive(true);
        sm.Shield();
        StartCoroutine(Shielding());
    }

    void Expand()
    {
        sm.Expand();
        StartCoroutine(Expanded());
    }

    void NewBall()
    {
        Instantiate(newBall, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
    }

    IEnumerator Shooting()
    {
        gun.SetActive(true);
        for (int i = 0; i < shotAmount; i++)
        {
            yield return new WaitForSeconds(shootingTime);
            sm.Shot();
            gun.SetActive(true);
            Instantiate(shot, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
        }
        gun.SetActive(false);
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

    IEnumerator Expanded()
    {
        if (!anim.GetBool("Expanded"))
        {
            anim.SetBool("Expanded", true);
            yield return new WaitForSeconds(expandTime);
            anim.SetBool("Expanded", false);
        }
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
