using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleControl : MonoBehaviour
{
    Rigidbody fizik;
    public Transform []tekerler;
    Transform []teker;
    gameControl gameControl;
    void Start()
    {
        gameControl = GameObject.FindGameObjectWithTag("gamecontrol").GetComponent<gameControl>();
        fizik = this.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "engel")
        {
            if (col.gameObject.transform.position.z > this.transform.position.z )
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,-10);
                Invoke("vel", 0.1f);
            }
        }
    }
    void Update(){
        if (!gameControl.isGameOver)
        {
            foreach (Transform tra in tekerler){
                tra.transform.Rotate(-1000,0,0);
            }
            Destroy(gameObject,20f);
        }
        else gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }
    void vel(){
        fizik.velocity = new Vector3 (0,0,-8);
    }
}
