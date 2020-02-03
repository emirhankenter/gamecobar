using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScript : MonoBehaviour
{
    GameObject jump;
    Rigidbody fizik;
    gameControl gameControl;
    void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("gamecontrol").GetComponent<gameControl>();
        fizik = this.gameObject.GetComponent<Rigidbody>();
        fizik.transform.rotation = Quaternion.Euler (-90,90,0);
    }
    void Update(){
        if (gameControl.isGameOver)
        {
            fizik.velocity = new Vector3(0,0,0);
        }
    }
}
