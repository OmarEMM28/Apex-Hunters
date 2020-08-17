using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse_Select : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform selected;

    public GameObject elJefe;

    public GameObject Music;

    private void Start()
    {
        elJefe = GameObject.Find("Jefe");

        Music = GameObject.Find("MenuMusic");
    }



    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "UIH")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                if(Input.GetAxisRaw("Fire1") > 0)
                {
                    SceneManager.LoadScene("MissionSelect", LoadSceneMode.Single);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "UIH2")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    SceneManager.LoadScene("MissionSelect", LoadSceneMode.Single);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "howTo")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    SceneManager.LoadScene("How_To", LoadSceneMode.Single);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "main")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
                    Destroy(Music.gameObject);
                    Destroy(elJefe.gameObject);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "mission")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0 && elJefe.GetComponent<GameManagerScript>().dificultySelec == true)
                {
                    SceneManager.LoadScene("Leve1_Part1", LoadSceneMode.Single);
                    hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                    elJefe.GetComponent<GameManagerScript>().dificulty =  0;
                    Destroy(Music.gameObject);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "mission2")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0 && elJefe.GetComponent<GameManagerScript>().dificultySelec == true)
                {
                    SceneManager.LoadScene("Leve1_Part1", LoadSceneMode.Single);
                    hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                    elJefe.GetComponent<GameManagerScript>().dificulty = 0;
                    Destroy(Music.gameObject);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "mission3")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0 && elJefe.GetComponent<GameManagerScript>().dificultySelec == true)
                {
                    SceneManager.LoadScene("Leve1_Part1", LoadSceneMode.Single);
                    hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                    elJefe.GetComponent<GameManagerScript>().dificulty = 0;
                    Destroy(Music.gameObject);
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "dificulty")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    elJefe.GetComponent<GameManagerScript>().dificulty = 1;
                    elJefe.GetComponent<GameManagerScript>().difiEne = 1;
                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "dificulty2")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    elJefe.GetComponent<GameManagerScript>().dificulty = 2;
                    elJefe.GetComponent<GameManagerScript>().difiEne = 2;

                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "dificulty3")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    elJefe.GetComponent<GameManagerScript>().dificulty = 3;
                    elJefe.GetComponent<GameManagerScript>().difiEne = 3;

                }
                selected = hit.transform;
            }

            else if (hit.transform.gameObject.tag == "dificulty4")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.magenta;
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    elJefe.GetComponent<GameManagerScript>().dificulty = 4;
                    elJefe.GetComponent<GameManagerScript>().difiEne = 4;

                }
                selected = hit.transform;
            }
        }
        else
        {
            if(selected != null)
            {
                selected.GetComponent<TextMesh>().color = Color.white;
                selected = null;
            }

        }
        
    }
}
