using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carControl : MonoBehaviour
{
    Rigidbody fizik;
    float horizontal = 0;
    float vertical = 0;
    Vector3 vec;
    public float carSpeed;
    float maxFuel = 100;
    float currentFuel;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    bool isGameOver = false;
    gameControl gameControl;
    public GameObject playerExplosion;
    GameObject []wheels;
    // Renderer []bodyRend;
    // Material []mat;
    AudioSource audiom;
    bool ct = true;
    bool yukarda = false;
    int jumpBuff = 0;
    public UnityEngine.UI.Slider fuelBar;
    // public Shader shader;
    // public Texture texture;
    // public Color color;
    void Start()
    {
        fizik = GetComponent<Rigidbody>();
        gameControl = GameObject.FindGameObjectWithTag("gamecontrol").GetComponent<gameControl>();
        wheels = GameObject.FindGameObjectsWithTag("wheel");
        audiom = GetComponent<AudioSource>();
        currentFuel = maxFuel;
        fuelBar.value = calculateFuel();
        // bodyRend = new Renderer[5];
        // for (int i = 0; i < 5; i++)
        // {
        //     bodyRend[i] = GameObject.FindGameObjectWithTag("body").GetComponent<Renderer>();
        //     bodyRend[i].material = new Material(shader);
        //     bodyRend[i].material.mainTexture = texture;
        //     bodyRend[i].material.color = color;
        //     // mat[i] = bodyRend.material;
        // }
        // mat[1].shader = Shader.Find("_LATABlack");
        // mat[1].SetColor("_LATABlack", Color.green);
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            vec = new Vector3(horizontal,0,0);
            fizik.velocity = vec*carSpeed;
            fizik.transform.position = new Vector3 (
                Mathf.Clamp(fizik.transform.position.x,minX,maxX),
                Mathf.Clamp(fizik.transform.position.y,-10f,10f),
                Mathf.Clamp(fizik.transform.position.z,minZ,maxZ)
            );
            fizik.rotation = Quaternion.Euler (Mathf.Abs(fizik.velocity.x*0.15f),fizik.velocity.x*1.2f,0);
            wheels[2].transform.rotation = Quaternion.Euler (0,fizik.velocity.x*5f,0);
            wheels[3].transform.rotation = Quaternion.Euler (0,fizik.velocity.x*5f,0);
        }
        else
        {
            fizik.velocity = new Vector3(0,0,0);
        }
    }
    void Update()
    {
        // Debug.Log(Mathf.RoundToInt(score));
        if (!isGameOver && !gameControl.pause)
        {
            if (currentFuel > maxFuel)
            {
                currentFuel = 100;
            }
            currentFuel -= Time.deltaTime*2;
            fuelBar.value = calculateFuel();
            // Debug.Log(Mathf.RoundToInt(currentFuel));
            if (currentFuel <= 0)
            {
                //TEXT EKLELELELE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                gameControl.gameOver();
            }
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].transform.Rotate(1000,0,0);
            }   
        }
        if (Input.GetKeyDown("space") && ct && !yukarda && jumpBuff > 0)
        {
            jumpBuff--;
            ct = false;
            gameControl.jumpText.text = ""+jumpBuff;
        }
        if (!ct)
        {
            fizik.transform.position = Vector3.MoveTowards(fizik.transform.position,new Vector3(fizik.transform.position.x,5f,0),0.1f);
            if (fizik.transform.position.y >= 5f)
            {
                yukarda = true;
                ct = true;
            }
        }
        if (yukarda)
        {
            fizik.transform.position = Vector3.MoveTowards(fizik.transform.position,new Vector3(fizik.transform.position.x,0,0),0.1f);
            if (fizik.transform.position.y <= 0)
            {
                yukarda = false;
            }
            
        }
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "engel")
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].transform.Rotate(0,0,0);
            }
            isGameOver = true;
            // Debug.Log("bitti");
            gameControl.gameOver();
            audiom.Play();

        }
        if (col.gameObject.tag == "fuel")
        {
            currentFuel += Random.Range(20,30);
            AudioSource fuelAudio = col.gameObject.GetComponent<AudioSource>();
            fuelAudio.Play();
            col.gameObject.GetComponent<Renderer>().enabled = false;
            col.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(col.gameObject,3);
        }
        if (col.gameObject.tag == "jump")
        {
            jumpBuff++;
            gameControl.jumpText.text = ""+jumpBuff;
            AudioSource jumpAudio = col.gameObject.GetComponent<AudioSource>();
            jumpAudio.Play();
            Debug.Log(jumpBuff);
            col.gameObject.GetComponent<Renderer>().enabled = false;
            col.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(col.gameObject,3);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "bonus")
        {
            gameControl.score += 20;
            AudioSource bonusAudio = col.gameObject.GetComponent<AudioSource>();
            bonusAudio.Play();
            // Debug.Log(score);
        }
    }
    float calculateFuel(){
        return currentFuel / maxFuel;
    }
}
