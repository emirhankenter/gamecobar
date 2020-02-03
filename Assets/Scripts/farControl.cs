using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farControl : MonoBehaviour
{
    Light far;
    LightControl lightControl;
    void Start()
    {
        // lightControl = GameObject.FindGameObjectWithTag("far").GetComponent<gameControl>();
        far = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
