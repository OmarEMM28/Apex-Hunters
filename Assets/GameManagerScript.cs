using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public int dificulty;
    public int difiEne;


    public bool dificultySelec;

    public GameObject easy;
    public GameObject medium;
    public GameObject hard;
    public GameObject imposible;

    public GameObject wolf;

    public bool wolfLink;

    private void Start()
    {
        dificulty = 0;

        wolfLink = false;

        easy = GameObject.Find("Easy");
        medium = GameObject.Find("Medium");
        hard = GameObject.Find("Hard");
        imposible = GameObject.Find("Predator");

        wolf = GameObject.Find("Wolf");
    
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        wolf = GameObject.Find("Wolf");

        if (dificulty >= 1)
        {
            dificultySelec = true;
        }

        if(dificulty == 1)
        {
            easy.transform.gameObject.GetComponent<TextMesh>().color = Color.red;

            imposible.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            medium.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            hard.transform.gameObject.GetComponent<TextMesh>().color = Color.white;

        }
        else if(dificulty == 2)
        {
            medium.transform.gameObject.GetComponent<TextMesh>().color = Color.red;

            easy.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            imposible.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            hard.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
        }
        else if (dificulty == 3)
        {
            hard.transform.gameObject.GetComponent<TextMesh>().color = Color.red;

            easy.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            medium.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            imposible.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
        }
        else if (dificulty == 4)
        {
            imposible.transform.gameObject.GetComponent<TextMesh>().color = Color.red;

            easy.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            medium.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
            hard.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
        }

        if (wolf.gameObject.GetComponent<WolfController>().dogolife <= 0)
        {
            
            Destroy(wolf.gameObject);
            SceneManager.LoadScene("Game_Over", LoadSceneMode.Single);
            Cursor.visible = true;
            Destroy(this.gameObject);
        }
    }

}
