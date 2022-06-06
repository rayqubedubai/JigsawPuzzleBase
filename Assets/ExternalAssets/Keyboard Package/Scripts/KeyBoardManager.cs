using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class KeyBoardManager : MonoBehaviour
{
    public static KeyBoardManager Instance;
    public TMP_Text textBox;
    //[SerializeField] TMP_InputField textBox;
    //[SerializeField] TextMeshProUGUI printBox;

    private void Start()
    {
        Instance = this;
        //printBox.text = "";
        if(textBox != null)
            textBox.text = "";
        //textBox.MoveTextEnd(true);
        //textBox.caretWidth = 2;
    }

    private void LateUpdate()
    {
        //textBox.ActivateInputField();
        //textBox.caretPosition = textBox.text.Length;
    }

    private void Update()
    {
    }

    public void DeleteLetter()
    {
        if (textBox == null)
            return;
        if (textBox.text.Length != 0)
        {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
            //SceneHandler.Instance.menuManager.updateLists();
        }
        //if (textBox.text.Length == 0)
        //{
            //SceneHandler.Instance.menuManager.clearBtn.gameObject.SetActive(false);
        //}
        //timer = 0;
        //SceneHandler.Instance.timer = 0;
    }

    public void AddLetter(string letter)
    {
        if (textBox == null)
            return;
        //textBox.ActivateInputField();
        //textBox.Select();
        //textBox.caretPosition = textBox.text.Length;
        //textBox.caretBlinkRate = 1;

        textBox.text = textBox.text + letter;

        //StartCoroutine(showCaret());
    }

    IEnumerator showCaret()
    {
      yield return new WaitForEndOfFrame();
        //    textBox.MoveTextEnd(true);
        //textBox.ActivateInputField();
        //textBox.MoveTextEnd(true);
        //textBox.caretPosition = textBox.text.Length;
    }

    public void SubmitWord()
    {
        //printBox.text = textBox.text;
        //textBox.text = "";
        // Debug.Log("Text submitted successfully!");
        gameObject.SetActive(false);
    }
}
