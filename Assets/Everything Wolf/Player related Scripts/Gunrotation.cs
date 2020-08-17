using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gunrotation : MonoBehaviour
{

    private float Rtrigger;
    private float Ltrigger;
    private float reload;

    // gun sounds
    public AudioSource singleshot;
    public AudioSource multishot;
    public AudioSource audiReload;
    public AudioClip multiClip;
    public AudioClip reloadclip;

    GameObject jefe;

    GameObject player;
    GameObject[] myEne;
    float eneDistance;
    float targetDistance;

    public GameObject amountB;
    public Text txt;

    public GameObject amountA;
    public Text multitxt;
    float reloadRate;
    float nextreload;
    bool mustReload;
    bool canShot = true;

    float rateoffire;
    float rateoffire2;
    float nextfire = 0;
    float nextfire2 = 0;
    public float eneHealth;
    public int singlebullet;
    public int multibullet;
    public bool thot = false;
    public float timeleft = 5.0f;

    public GameObject MissionLife;
    public float missionRate;
    public Text txt3;

    [SerializeField]
    //Weapon effects
    GameObject muzzleObject;
    GameObject muzzleObject2;
    ParticleSystem muzzle;
    ParticleSystem muzzle2;

    int i;

    float speed = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        rateoffire = .1f;
        rateoffire2 = 1.0f;

        jefe = GameObject.Find("Jefe");
        player = GameObject.Find("Wolf");

        amountB = GameObject.Find("SingleN");
        txt = amountB.GetComponent<Text>();
        amountA = GameObject.Find("MultiN");
        multitxt = amountA.GetComponent<Text>();

        singleshot = GameObject.Find("Gun").GetComponent<AudioSource>();
        multishot = GameObject.Find("gun").GetComponent<AudioSource>();
        audiReload = GameObject.Find("Cylinder").GetComponent<AudioSource>();

        multishot.Stop();

        if(jefe.gameObject.GetComponent<GameManagerScript>().difiEne == 4)
        {
            singlebullet = 20;
        }
        else
        {singlebullet = 10;}
        multibullet = 200;
        reloadRate = 1.5f;

        MissionLife = GameObject.Find("Mission");
        txt3 = MissionLife.GetComponent<Text>();
        missionRate = 10.0f;

        muzzleObject = GameObject.Find("Flare");
        muzzleObject2 = GameObject.Find("photon");
        muzzle = muzzleObject.GetComponent<ParticleSystem>();
        muzzle2 = muzzleObject2.GetComponent<ParticleSystem>();
        muzzle.Stop();
        muzzle2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        myEne = player.GetComponent<WolfController>().myBadguys;


        Rtrigger = Input.GetAxisRaw("Fire1");
        Ltrigger = Input.GetAxisRaw("Fire2");
        reload = Input.GetAxisRaw("Reload");
        

        
        txt.text = "" + singlebullet;
        

        multitxt.text = "" + multibullet;

   
        if (Time.time > missionRate)
        {
            txt3.text = "";
        }

        //Mini GUn
        if (Rtrigger > 0 )
        {
            if (Time.time > nextfire && canShot == true)
            {
                nextfire = Time.time + rateoffire;
                
                //Barrel Rotation
                transform.Rotate(Vector3.down * speed * Time.deltaTime);

                multibullet = multibullet - 1;

                StartCoroutine(WeaponEffects());
                multishot.PlayOneShot(multiClip);

                //Enemies will hunt you
                for(i = 0; i < myEne.Length; i++)
                {
                    if(myEne[i].GetComponent<ActorScript>().isHunting == false)
                    {
                        eneDistance = Vector3.Distance(player.transform.position, myEne[i].GetComponent<ActorScript>().transform.position);

                        if(eneDistance < 40.0f)
                        {
                            myEne[i].GetComponent<ActorScript>().isHunting = true;
                        }
                    }
                }

                //Raycast shoot
                var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("it hit");
                    targetDistance = Vector3.Distance(player.transform.position, hit.transform.position);
 
                    
                        if (hit.transform.gameObject.tag == "Enemy")
                        {
                            eneHealth = hit.transform.gameObject.GetComponent<ActorScript>().EneHealth;

                            hit.transform.gameObject.GetComponent<ActorScript>().isHunting = true;

                            hit.transform.gameObject.GetComponent<ActorScript>().EneHealth = hit.transform.gameObject.GetComponent<ActorScript>().EneHealth - 14.0f;

                            Debug.Log(hit.transform.gameObject.GetComponent<ActorScript>().EneHealth);
                        }

                        if (hit.transform.gameObject.tag == "WolfLink")
                        {
                            Destroy(hit.transform.gameObject);

                            jefe.gameObject.GetComponent<GameManagerScript>().wolfLink = true;

                            player.gameObject.GetComponent<WolfController>().myxt = "Mission Objective: Escape the enemy base. Follow the road to the airstrip!!";
                            //missionRate = Time.time + 10.0f;
                        }

                    

                }

                
            }
            
        }

        // Reload
        if(reload > 0 && multibullet < 200 || multibullet <= 0)
        {
            canShot = false;
            //mustReload = true;
            nextreload = Time.time + reloadRate;

        }

        if (nextreload > Time.time )
        {
            transform.Rotate(Vector3.down * speed * Time.deltaTime);

            audiReload.Play();
            
            multibullet = 200;

        }

        else if(nextreload < Time.time)
        {
            canShot = true;
            audiReload.Stop();
            
        }

        //Photon gun
        if (Ltrigger == 1 )
        {


            if(singlebullet > 0 && Time.time > nextfire2)
            {
                //Raycast shoot
                nextfire2 = Time.time + rateoffire2;

                singleshot.PlayOneShot(singleshot.clip);

                var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;
                StartCoroutine(WeaponEffects2());
                if (Physics.Raycast(ray, out hit))
                {
                    
                    hit.transform.gameObject.GetComponent<ActorScript>().EneHealth = hit.transform.gameObject.GetComponent<ActorScript>().EneHealth - 150.0f;
                    singlebullet = singlebullet - 1;

                    Debug.Log(hit.transform.gameObject.GetComponent<ActorScript>().EneHealth);

                }

                if (hit.transform.gameObject.tag == "WolfLink")
                {
                    Destroy(hit.transform.gameObject);

                    jefe.gameObject.GetComponent<GameManagerScript>().wolfLink = true;

                }


            }
           
            
        }

       

        IEnumerator WeaponEffects()
        {
            muzzle.Play();
            yield return new WaitForEndOfFrame();
            muzzle.Stop();
        }

        IEnumerator WeaponEffects2()
        {
            muzzle2.Play();
            yield return new WaitForSecondsRealtime(5);
            muzzle2.Stop();
        }
    }
    
    

}
