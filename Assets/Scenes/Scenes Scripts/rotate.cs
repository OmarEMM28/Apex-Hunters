using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    float hspeed;

    void Start()
    {
        hspeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, hspeed, 0);
    }
}
