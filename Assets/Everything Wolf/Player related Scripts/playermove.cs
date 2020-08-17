using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody myRig;

    public float hspeed = 3.0f;

    void Start()
    {
        myRig = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * hspeed;
        float vertical = Input.GetAxisRaw("Vertical") * hspeed;

        myRig.velocity =  hspeed * new Vector3(horizontal, 0, vertical);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "evil")
        {
            this.transform.position = new Vector3(0.1f, 1.36f, -12.67f);
        }

        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("its colliding");
            SceneManager.LoadScene("Maze");
        }
    }
}
