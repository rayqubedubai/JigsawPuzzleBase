using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Toggle showWarning;
    public TMP_Dropdown timeDD;
    public TMP_Dropdown orientation;
    public TMP_Text passwordText;
    public GameObject keyboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickSubmitSettings()
    {
        if (passwordText.text.Equals("RayQube"))
        {
            PlayerPrefs.SetInt("Orientation", orientation.value);
            PlayerPrefs.SetInt("Time", timeDD.value);
            if (showWarning.isOn)
            {
                PlayerPrefs.SetInt("ShowWarning", 1);
            }
            else
            {
                PlayerPrefs.SetInt("ShowWarning", 0);
            }
        SceneManager.LoadScene(1);
        }
    }

    public void onClickTextBox(TMP_Text field)
    {
        keyboard.GetComponent<KeyBoardManager>().textBox = field;
    }
}
