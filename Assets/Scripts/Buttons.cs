using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Sprite hover;
    public Sprite normal;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseHover1()
    {
        Debug.Log("Hover");
        button1.GetComponent<Image>().sprite = hover;
    }

    public void MouseHover2()
    {
        button2.GetComponent<Image>().sprite = hover;
    }

    public void MouseHover3()
    {
        button3.GetComponent<Image>().sprite = hover;
    }
}
