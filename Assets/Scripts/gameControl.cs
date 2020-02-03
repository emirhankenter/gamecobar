using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameControl : MonoBehaviour
{
    public GameObject road1;
    public GameObject road2;
    public GameObject car;
    public GameObject gasBottle;
    public GameObject jumpBuff;
    public GameObject []engel;
    GameObject[] killEmAll;
    GameObject engelim;
    GameObject jump;
    GameObject gas;
    Rigidbody fizikRoad1;
    Rigidbody fizikRoad2;
    Rigidbody fizikEngel;
    Rigidbody fiz;
    public float score = 0;
    public Vector3 []serit;
    public bool isGameOver = false;
    float uzunluk = 0;
    float spawn = 0;
    float spawnJumpBuff = 0;
    public float CarSpeed = 10f;
    float degisimZaman = 0;
    public Button playAgainButton;
    public Button mainMenuButton;
    public Button tomainMenuButton;
    public Button exitGameButton;
    public Text scoreText;
    public Text jumpText;
    public Text endGameScore;
    public Shader shader;
    int highestScore = 0;
    public Color color;
    public bool pause = false;
    Material []bodyRend;
    void Start()
    {
        fizikRoad1 = road1.GetComponent<Rigidbody>();
        fizikRoad2 = road2.GetComponent<Rigidbody>();    

        fizikRoad1.velocity = new Vector3(0,0,-CarSpeed);
        fizikRoad2.velocity = new Vector3(0,0,-CarSpeed);

        uzunluk = road1.GetComponent<BoxCollider>().size.z;
        highestScore = PlayerPrefs.GetInt("save");
        bodyRend = new Material[5];
        for (int i = 0; i < 5; i++)
        {
            bodyRend = GameObject.FindGameObjectWithTag("body").GetComponent<Renderer>().materials;
            bodyRend[i] = new Material(shader);
        }
        Debug.Log(PlayerPrefs.GetString("SavedColor"));
            bodyRend[1].color = getColorFromString((PlayerPrefs.GetString("SavedColor")));

    }
    void Update()
    {
        if (road1.transform.position.z <= -uzunluk) {
            road1.transform.position += new Vector3(0,0,uzunluk*2);
        }
        if (road2.transform.position.z <= -uzunluk) {
            road2.transform.position += new Vector3(0,0,uzunluk*2);
        }
        if (Input.GetKeyDown("escape") && !isGameOver)
        {
            pause = togglePause();
            Debug.Log(pause);
            tomainMenuButton.gameObject.SetActive(pause);
            exitGameButton.gameObject.SetActive(pause);
        }
        if (!isGameOver)
        {
            score += Time.deltaTime*10;
            scoreText.text = "Score: " +  Mathf.RoundToInt(score);
        }
        spawn += Time.deltaTime;
        if (spawn > Random.Range(10f,15f))
        {
            if (!isGameOver)
            {
                spawn = 0;
                createFuelBottle();
            }
        }

        spawnJumpBuff += Time.deltaTime;
        if (spawnJumpBuff > Random.Range(5f,10f))
        {
            if (!isGameOver)
            {
                spawnJumpBuff = 0;
                createJumpBuff();
            }
        }

        //------------------------------------------------------------------------
        
        degisimZaman += Time.deltaTime;
        if (degisimZaman > Random.Range(1f,1.5f))
        {
            if (!isGameOver)
            {
                degisimZaman = 0;
                createCar();
            }
        }
    }
    public void gameOver() {
        killEmAll = GameObject.FindGameObjectsWithTag("engel");
        if (GameObject.FindGameObjectWithTag("fuel") != null)
        {
            stopVelocity(gas, "fuel");
        }
        if (GameObject.FindGameObjectWithTag("jump") != null)
        {
            stopVelocity(jump, "jump");
        }
        for(int i = 0; i < killEmAll.Length; i++)
        {
        killEmAll[i].GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
        }
            fizikRoad1.velocity = new Vector3(0,0,0);
            fizikRoad2.velocity = new Vector3(0,0,0);
            isGameOver = true;
            playAgainButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        if (Mathf.RoundToInt(score) > highestScore)
        {
            highestScore = Mathf.RoundToInt(score);
            PlayerPrefs.SetInt("save",highestScore);
            endGameScore.gameObject.SetActive(true);
            endGameScore.text = "New Highest Score!: " + highestScore;
        } else
        {
            endGameScore.gameObject.SetActive(true);
            endGameScore.text = "Highest Score was: " + highestScore;
        }
        Debug.Log(highestScore);
    }

    void createCar(){
        Vector3 vec = serit[Random.Range(0,4)];
        engelim = Instantiate ( engel[Random.Range(0,2)],vec,Quaternion.identity);
        fizikEngel = engelim.GetComponent<Rigidbody>();
        fizikEngel.velocity = new Vector3(0,0,-Random.Range(4,8));
    }
    void createFuelBottle(){
        createBuff(gas, gasBottle, fiz, new Vector3(Random.Range(-3.6f,3.6f),0.01f,42f));
    }
    void createJumpBuff(){
        createBuff(jump, jumpBuff, fiz, new Vector3(Random.Range(-3.6f,3.6f),0.06f,42f));
    }
    void createBuff(GameObject go, GameObject buff, Rigidbody rb, Vector3 vec){
        go = Instantiate (buff,vec,Quaternion.identity);
        rb = go.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,-CarSpeed);
        Destroy(go,20f);
    }

    public void playAgain(){
        SceneManager.LoadScene("Game");
    }
    public void goToMain(){
        SceneManager.LoadScene("mainMenu");
    }
    void stopVelocity(GameObject go, string tag)
    {
        go = GameObject.FindGameObjectWithTag(""+tag+"");
        go.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
    }
    bool togglePause()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return(false);
        }
        else
        {
            Time.timeScale = 0f;
            return(true);    
        }
    }
    public void exitGame(){
        Application.Quit();
    }
    // Color ColorGO () {
    //     string str_color;
    //     str_color = PlayerPrefs.GetString("SavecColor");
    //     //Remove the header and brackets
    //     str_color = str_color.Replace("RGBA(", "");
    //     str_color = str_color.Replace(")", "");
        
    //     //Get the individual values (red green blue and alpha)
    //     var strings = str_color.Split(","[0] );
        
    //     Color outputcolor;
    //     outputcolor = Color.black;
    //     for (var i = 0; i < 4; i++) {
    //         outputcolor[i] = System.Single.Parse(strings[i]);
    //     }
    //     //apply the color to a gameobject
    //     return outputcolor;
    // }

    int hexToDec(string hex)
    {
        int dec = System.Convert.ToInt32(hex,16);
        return dec;
    }
    float hexToNormalized(string hex)
    {
        return hexToDec(hex) / 255f;
    }
    public Color getColorFromString(string hex)
    {
        float red = hexToNormalized(hex.Substring(0,2));
        float green = hexToNormalized(hex.Substring(2,2));
        float blue = hexToNormalized(hex.Substring(4,2));
        return new Color(red,green,blue);
    }
}
