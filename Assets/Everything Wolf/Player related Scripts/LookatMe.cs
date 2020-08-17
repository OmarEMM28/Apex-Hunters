using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatMe : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject focus = null;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(focus != null)
        {
            this.transform.LookAt(focus.transform.position);
        }
    }
}
