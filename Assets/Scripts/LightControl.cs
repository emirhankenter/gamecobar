using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public float theta = 1f;
    gameControl gameControl;
    Light sunLight;
    public GameObject sun;
    public GameObject []far;
    public bool isNight = false;
    void Start()
    {
        sunLight = sun.GetComponent<Light>();
        gameControl = GameObject.FindGameObjectWithTag("gamecontrol").GetComponent<gameControl>();
        if (sunLight.transform.position.y < -1f)
        {
            isNight = true;
        }
        else if (sunLight.transform.position.y > -0.5f)
        {
            isNight = false;
        }
    }
    void Update()
    {
        if(gameControl.isGameOver == false){
            if (!gameControl.pause )
            {
                sunLight.transform.RotateAround(Vector3.zero,Vector3.left, theta);
            }
            if (sunLight.transform.position.y < -2f && sunLight.GetComponent<Light>().enabled)
            {
                isNight = true;
                //Debug.Log(isNight);
                sunLight.GetComponent<Light>().enabled = !sunLight.GetComponent<Light>().enabled;
                for (int i = 0; i < far.Length; i++)
                {
                    far[i].GetComponent<Light>().enabled = !far[i].GetComponent<Light>().enabled;
                }
            }
            else if (sunLight.transform.position.y > 0 && !sunLight.GetComponent<Light>().enabled)
            {
                isNight = false;
                //Debug.Log(isNight);
                sunLight.GetComponent<Light>().enabled = !sunLight.GetComponent<Light>().enabled;
                for (int i = 0; i < far.Length; i++)
                {
                    far[i].GetComponent<Light>().enabled = !far[i].GetComponent<Light>().enabled;
                }
            }
        }
    }
}
