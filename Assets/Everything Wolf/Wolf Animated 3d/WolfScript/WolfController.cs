using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WolfController : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody myRig;

    public Animator myAnime;

    bool isrunning = false;
    bool movement = false;

    private float hspeed = 5.0f;

    public float rateofHeal;
    public float nextHeal;
    public bool shouldHeal;

    public GameObject[] myBadguys;
    
    //private float yaw = 0.0f;
    private float speedH = 3.5f;

    public float dogolife = 5.0f;
    public float dogolifetot = 50.0f;
    public Slider healthbar;

    public GameObject jefe;

    public GameObject MissionLife;
    public float missionRate;
    public Text mtxt;
    public string  myxt;

    //On start do shit
    void Start()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();
        myAnime = this.GetComponent<Animator>();

        myBadguys = GameObject.FindGameObjectsWithTag("Enemy");

        Cursor.visible = false;

        //Debug.Log(myBadguys[0].gameObject.GetComponent<ActorScript>().isHunting);

        shouldHeal = false;
        nextHeal = 0;

        MissionLife = GameObject.Find("Mission");
        mtxt = MissionLife.GetComponent<Text>();
        myxt = "Mission Objective: Find the bridge and reach the enemy base";
        //missionRate = 10.0f;

        rateofHeal = 0.09f;

        dogolife = 50.0f;
        hspeed = 10.0f;

        jefe = GameObject.Find("Jefe");

        healthbar = GameObject.Find("Slider").GetComponent<Slider>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        myBadguys = GameObject.FindGameObjectsWithTag("Enemy");

        float horizontal = Input.GetAxisRaw("Horizontal") ;
        float vertical = Input.GetAxisRaw("Vertical");
        float objective = Input.GetAxisRaw("Mission");

        healthbar.value = dogolife;

        //if(Time.time > missionRate)
        //{
        //    mtxt.text = "";
        //}
        
        // ANimation controller
        if(horizontal > 0 || vertical > 0 )
        {
            //YOU ARE MOVING PUT THE WALK ANIMATION
            myAnime.SetBool("ismoving", true);
            if (vertical > 0)
            {
                movement = true;
            }

            if (isrunning == false)
            {
                myAnime.SetInteger("DIR", 1);
            }

            if (myAnime.GetBool("iscreep") == true && movement == true)
            {
                myAnime.enabled = true;
                myAnime.SetInteger("DIR", 3);

            }

        }
        else if (horizontal < 0 || vertical < 0)
        {
            //YOU ARE MOVING PUT THE WALK ANIMATION
            myAnime.SetBool("ismoving", true);
            //movement = true;

            if (isrunning == false)
            {
                myAnime.SetInteger("DIR", 1);
            }

            if (myAnime.GetBool("iscreep") == true && movement == true)
            {
                myAnime.enabled = true;
                myAnime.SetInteger("DIR", 3);

            }

        }
        else if(horizontal == 0 || vertical == 0 )
        {
            // set up idle animation
            //Debug.Log("idle");
            myAnime.SetBool("ismoving", false);
            movement = false;

            if(myAnime.GetBool("iscreep") == false)
            {
                myAnime.SetInteger("DIR", 0);

            }

            
        }

        // is creeping wolf
        if(Input.GetAxisRaw("Creep") > 0)
        {
           
            myAnime.SetBool("iscreep", true);
            
        }
        else if(Input.GetAxisRaw("Creep") <= 0)
        {
            myAnime.SetBool("iscreep", false);
        }

        // is the owlf running
        if(Input.GetAxisRaw("Run") > 0)
        {
           // Debug.Log("shift down");
            isrunning = true;
            if (myAnime.GetBool("ismoving") == true && vertical > 0)
            {
                myAnime.SetInteger("DIR", 2);
            }
        }
        if(Input.GetAxisRaw("Run") <= 0)
        {
            isrunning = false;
        }

        // Movement depending on if its running or no
        if (isrunning == false && vertical > 0 || vertical < 0)
        {
            myRig.velocity = transform.forward * speedH * vertical + new Vector3(0, myRig.velocity.y, 0); 
        }
        else if (isrunning == true && vertical > 0)
        {
            myRig.velocity = transform.forward * hspeed * vertical + new Vector3(0, myRig.velocity.y, 0);

        }
        else if (Input.GetAxisRaw("Creep") > 0 && vertical > 0)
        {
            myRig.velocity = transform.forward * vertical + new Vector3(0, myRig.velocity.y, 0);
        }

        
        transform.Rotate(0, horizontal * 3.0f, 0);

        if(shouldHeal == true && Time.time > nextHeal)
        {

            if(dogolife >= 50.0f)
            {
                shouldHeal = false;
            }

            dogolife = dogolife + rateofHeal;
        }

        if(objective > 0)
        {
            mtxt.text = myxt;
        }
        else
        {
            mtxt.text = "";
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "river")
        {
            Destroy(jefe.gameObject);
            SceneManager.LoadScene("Game_Over", LoadSceneMode.Single);
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Bridge")
        {
            
            SceneManager.LoadScene("Leve1_Part2", LoadSceneMode.Single);
            this.transform.position = new Vector3(198.2f,0, 303.2f);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);

            myxt = "Mission Objective: Find the Wolf-Link inside the Warehouse and escape";
            //missionRate = Time.time + 10.0f;

            
        }

        if(collision.gameObject.tag == "Airstrip" && jefe.gameObject.GetComponent<GameManagerScript>().wolfLink == true)
        {
            jefe.gameObject.GetComponent<GameManagerScript>().difiEne = 4;
            SceneManager.LoadScene("Leve1_Part3", LoadSceneMode.Single);
            this.transform.position = new Vector3(149f, 0, 28.8f);
            this.transform.rotation = Quaternion.Euler(0, 90, 0);

            

            myxt = "Mission Objective: Go the the plane for your extraction";
            //missionRate = Time.time + 10.0f;


            
        }

        if(collision.gameObject.tag == "Finish")
        {
            Destroy(jefe.gameObject);
            SceneManager.LoadScene("Winner", LoadSceneMode.Single);
            Cursor.visible = true;
            Destroy(this.gameObject);
        }
    }
}
