using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int possibility = 10;
    public int points;
    public int chance;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //if (!gm.multiplayer)
        //{
        gm.maxScore+=points;
        //}
        chance = Random.Range(0, possibility);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
