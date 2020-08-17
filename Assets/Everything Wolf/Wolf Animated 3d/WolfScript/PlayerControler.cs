using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    Rigidbody myRig;

    public int test = 0;
    public int souls = 0;
    public int life = 100;

    public Text txt;
    public Text txt2;


    //MainMenu title;

    GameObject canvas;
    GameObject canvas2;
    GameObject menu;

    public float hspeed = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();

        canvas = GameObject.Find("life");
        canvas2 = GameObject.Find("souls");

        txt2 = canvas2.GetComponent<Text>();
        txt2.text = "Souls Collected: " + souls;

        txt = canvas.GetComponent<Text>();
        txt.text = "Life Points: " + life;

    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal") * hspeed;
        float vertical = Input.GetAxisRaw("Vertical") * hspeed;

        myRig.velocity = transform.forward * hspeed * vertical + new Vector3(0,myRig.velocity.y,0);
        transform.Rotate(0, horizontal * 0.8f, 0);

        if(souls >= 10)
        {
            SceneManager.LoadScene(sceneName: "Winner");

            Destroy(this.gameObject);
        }

        if(life <= 0)
        {
            SceneManager.LoadScene(sceneName: "GameOver");

            Destroy(this.gameObject);
        }

    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "soul")
        {
            souls = souls + 1;

            Debug.Log("Soul taken");

            txt2.text = "Souls Collected: " + souls;
        }

        if(collision.gameObject.tag == "bullet")
        {
            life = life - 10;

            txt.text = "Life Points: " + life;
        }

        if(collision.gameObject.tag == "badguy")
        {
            life = life - 17;
            txt.text = "Life Points: " + life;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "badguy")
        {
            life = life - 7;
            txt.text = "Life Points: " + life;
        }

    }
}
