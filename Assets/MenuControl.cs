using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public Button resetPrefButton;
    public Toggle gameMusicToggle;
    public GameObject car;
    public GameObject picker;
    Material []bodyRend;
    public Shader shader;
    gameControl gameControl;
    int highestScore;
    public Text highestScoreText;
    ColorPickerAdvanced colorPickerAdvanced;
    void Start()
    {
        if (!PlayerPrefs.HasKey("SavedColor"))
        {
            PlayerPrefs.SetString("SavedColor","000000");
        }
        if (!PlayerPrefs.HasKey("save"))
        {
            PlayerPrefs.SetInt("save",0);
        }
        colorPickerAdvanced = gameObject.GetComponent<ColorPickerAdvanced>();
        bodyRend = new Material[5];
        highestScore = PlayerPrefs.GetInt("save");
    }
    public void startGame(){
        SceneManager.LoadScene("Game");
    }
    public void exitGame(){
        Application.Quit();
    }
    public void options(){
        startButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        resetPrefButton.gameObject.SetActive(true);
        highestScoreText.gameObject.SetActive(false);
        // gameMusicToggle.gameObject.SetActive(true);
        car.gameObject.SetActive(true);
        picker.gameObject.SetActive(true);
    }
    public void resetPref(){
        PlayerPrefs.SetInt("save",0);
    }

    void Update()
    {
        highestScoreText.text = "Highest Score: " + highestScore;
        if(Input.GetKeyDown("escape") && !optionsButton.gameObject.activeSelf) {
            startButton.gameObject.SetActive(true);
            optionsButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
            highestScoreText.gameObject.SetActive(true);
            resetPrefButton.gameObject.SetActive(false);
            // gameMusicToggle.gameObject.SetActive(false);
            car.gameObject.SetActive(false);
            picker.gameObject.SetActive(false);
        }
        if (car.gameObject.activeSelf)
        {
            for (int i = 0; i < 5; i++)
            {
                bodyRend = GameObject.FindGameObjectWithTag("body").GetComponent<Renderer>().materials;
                bodyRend[i] = new Material(shader);
            }
            bodyRend[1].color = getColorFromString((PlayerPrefs.GetString("SavedColor")));
        }
    }
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
