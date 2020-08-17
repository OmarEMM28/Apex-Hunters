using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class ActorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator myAnime;

    public GameObject spot1;
    public GameObject spot2;
    public GameObject player;
    public GameObject aim;

    public GameObject death;
    

    public float lookRadious = 25.0f;
    float distance;

    Vector3 preray;
    Vector3 randVec;
    Vector3 tots;

    Vector3 point1;
    Vector3 point2;

    Vector3 repawn;

    NavMeshAgent myNav;

    public bool isHunting;

    public int dificulty;
    public float damage;
    public GameObject elJefe;

    Transform target;

    public float EneHealth = 200.0f;

    public float playerHelth;

    private int altT = 0;

    public AudioSource eneSound;
    public AudioClip eneClip;

    float rateoffire;
    float nextfire = 0;

    void Start()
    {
        rateoffire = .2f;

        eneSound = GameObject.Find("EnemyAim").GetComponent<AudioSource>();
        eneSound.Stop();

        isHunting = false;

        myAnime = this.GetComponent<Animator>();

        elJefe = GameObject.Find("Jefe");

        myNav = this.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Wolf");

        aim = GameObject.Find("Soldier");

        dificulty = elJefe.GetComponent<GameManagerScript>().difiEne;
        

        target = player.transform;

        point1 = spot1.transform.position;
        point2 = spot2.transform.position;

        myNav.destination = point1;

        playerHelth = player.GetComponent<WolfController>().dogolife;

        EneHealth = 100.0f;
        

        if (dificulty == 1)
        {
            lookRadious = 20.0f;
            damage = .19025f;
        }
        else if (dificulty == 2)
        {
            lookRadious = 23.0f;
            damage = .19350f;
        }
        else if (dificulty == 3)
        {
            lookRadious = 28.0f;
            damage = .19652f;
        }
        else if (dificulty == 4)
        {
            lookRadious = 30.0f;
            damage = .19905f;

            
        }


    }



    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(target.position, this.transform.position);

        death.transform.position = this.transform.position;

        point1 = spot1.transform.position;
        point2 = spot2.transform.position;

        repawn = this.transform.position;

        // Debug.Log(myNav.destination);
        if (isHunting == true)
        {

            if (distance <= lookRadious)
            {
                this.transform.LookAt(target.position);

                if (distance < 15)
                {
                    myNav.destination = this.transform.position + new Vector3(0, 0, 12);
                    myAnime.SetBool("Shoot", false);
                }
                else
                {
                    this.transform.LookAt(target.position);
                    myAnime.SetBool("Shoot", true);
                    myNav.destination = this.transform.position;

                    if (Time.time >= nextfire)
                    {
                        nextfire = Time.time + rateoffire;
                        eneSound.PlayOneShot(eneClip);
                        //Debug.Log("fire is go");
                    }

                    preray = this.transform.position + this.transform.up + this.transform.forward;


                    if (this.gameObject.tag == "Enemy")
                    {
                        randVec = this.transform.forward + new Vector3(Random.Range(-1.2f, 1.2f), Random.Range(-1.2f, 1.2f), 0);
                    }

                    var ray = new Ray(preray, randVec);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {


                        if (this.gameObject.tag == "Enemy")
                        {
                            player.GetComponent<WolfController>().dogolife = player.GetComponent<WolfController>().dogolife - damage;
                            player.GetComponent<WolfController>().shouldHeal = true;
                            player.GetComponent<WolfController>().nextHeal = Time.time;
                        }



                    }
                }
            }
            else if (distance > lookRadious)
            {
                myNav.destination = target.position;
                myAnime.SetBool("Shoot", false);
            }

        }
        else if (isHunting == false)
        {

            if (distance <= lookRadious)
            {
                this.transform.LookAt(target.position);

                isHunting = true;

            }

            //Debug.Log(myNav.destination);
            if (myNav.remainingDistance <= 1.5f && altT == 0)//&& distance >= lookRadious)
            {
                myAnime.SetBool("Shoot", false);

                altT++;
                myNav.destination = point2;

            }
            else if (myNav.remainingDistance <= 1.5f && altT == 1)//&& distance >= lookRadious)
            {
                myAnime.SetBool("Shoot", false);

                altT--;
                myNav.destination = point1;

            }
        }

        if(EneHealth <= 0.0f)
        {
            Destroy(this.gameObject);
            death.gameObject.SetActive(true);
            
        }

        



    }



}
